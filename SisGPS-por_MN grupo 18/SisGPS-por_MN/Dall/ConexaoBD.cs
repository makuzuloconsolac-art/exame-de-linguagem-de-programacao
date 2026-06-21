using MySqlConnector;
using System;

namespace SisGPS_por_MN.Dall
{
    internal static class ConexaoBD
    {
        private static readonly string DefaultConnectionString = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Port = 3306,
            Database = "gestao_projectos",
            UserID = "root",
            Password ="root123",
        }.ConnectionString;

        private static string ConnectionString =>
            Environment.GetEnvironmentVariable("SISGPS_CONNECTION_STRING") ?? DefaultConnectionString;

        public static MySqlConnection ObterLigacao() => new MySqlConnection(ConnectionString);

        public static bool TestarConexao()
        {
            try
            {
                using var conn = ObterLigacao();
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void InicializarBD()
        {
            try
            {
                using var con = ObterLigacao();
                con.Open();

                // Garantir que as colunas novas existem mesmo em tabelas pré-existentes
                var alteracoes = new[]
                {
                    "ALTER TABLE utilizadores ADD COLUMN IF NOT EXISTS ultimo_acesso DATETIME NULL",
                    "ALTER TABLE utilizadores ADD COLUMN IF NOT EXISTS pergunta_seguranca VARCHAR(255) NULL",
                    "ALTER TABLE utilizadores ADD COLUMN IF NOT EXISTS resposta_seguranca VARCHAR(255) NULL"
                };
                foreach (var sql in alteracoes)
                {
                    try
                    {
                        using var cmd = new MySqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                    catch { /* coluna pode já existir em versões antigas do MySQL sem suporte a IF NOT EXISTS */ }
                }

                // Criar tabela de registos de acesso se não existir
                string sqlAcessos = @"
                    CREATE TABLE IF NOT EXISTS registos_acesso (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        utilizador_id INT NOT NULL,
                        data_acesso DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        ip_endereco VARCHAR(50) NULL,
                        FOREIGN KEY (utilizador_id) REFERENCES utilizadores(id) ON DELETE CASCADE
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;";
                using (var cmd = new MySqlCommand(sqlAcessos, con))
                    cmd.ExecuteNonQuery();

                // Criar tabela de configurações SMTP se não existir
                string sqlSMTP = @"
                    CREATE TABLE IF NOT EXISTS configuracao_smtp (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        smtp_servidor VARCHAR(100) NOT NULL,
                        smtp_porta INT NOT NULL DEFAULT 587,
                        smtp_utilizador VARCHAR(100) NOT NULL,
                        smtp_senha VARCHAR(100) NOT NULL,
                        usar_ssl TINYINT(1) NOT NULL DEFAULT 1
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;";
                using (var cmd = new MySqlCommand(sqlSMTP, con))
                    cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro ao inicializar BD: " + ex.Message);
            }
        }
    }
}
