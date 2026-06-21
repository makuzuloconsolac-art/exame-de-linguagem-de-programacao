using MySqlConnector;
using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Utils;

namespace SisGPS_por_MN.Servicos
{
    public class ServicoAutenticacao
    {
        private readonly UtilizadorRepository _repo = new();

        public Utilizador? Login(string username, string password)
        {
            var u = _repo.Autenticar(username, password);
            if (u != null)
            {
                Sessao.Iniciar(u);
                try
                {
                    using var con = ConexaoBD.ObterLigacao();
                    con.Open();
                    
                    using (var cmd = new MySqlCommand("UPDATE utilizadores SET ultimo_acesso = @now WHERE id = @uid", con))
                    {
                        cmd.Parameters.AddWithValue("@now", DateTime.Now);
                        cmd.Parameters.AddWithValue("@uid", u.Id);
                        cmd.ExecuteNonQuery();
                    }
                    
                    using (var cmd = new MySqlCommand("INSERT INTO registos_acesso (utilizador_id, data_acesso, ip_endereco) VALUES (@uid, @now, '127.0.0.1')", con))
                    {
                        cmd.Parameters.AddWithValue("@now", DateTime.Now);
                        cmd.Parameters.AddWithValue("@uid", u.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch
                {
                    // Falhas de registo secundárias não impedem a autenticação
                }
            }
            return u;
        }

        public void Logout() => Sessao.Terminar();
    }
}
