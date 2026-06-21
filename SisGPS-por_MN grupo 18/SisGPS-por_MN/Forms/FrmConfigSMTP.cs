using MySqlConnector;
using SisGPS_por_MN.Dall;
using System;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmConfigSMTP : Form
    {
        public FrmConfigSMTP()
        {
            InitializeComponent();
            CarregarConfiguracao();
        }

        private void CarregarConfiguracao()
        {
            try
            {
                using var con = ConexaoBD.ObterLigacao();
                con.Open();
                const string sql = "SELECT * FROM configuracao_smtp LIMIT 1";
                using var cmd = new MySqlCommand(sql, con);
                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    txtServidor.Text = rdr.GetString("smtp_servidor");
                    txtPorta.Text = rdr.GetInt32("smtp_porta").ToString();
                    txtUtilizador.Text = rdr.GetString("smtp_utilizador");
                    txtSenha.Text = rdr.GetString("smtp_senha");
                    chkSSL.Checked = rdr.GetBoolean("usar_ssl");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar configurações: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            string host = txtServidor.Text.Trim();
            string portStr = txtPorta.Text.Trim();
            string user = txtUtilizador.Text.Trim();
            string pass = txtSenha.Text;
            bool ssl = chkSSL.Checked;

            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(portStr) ||
                string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(portStr, out int port) || port <= 0)
            {
                MessageBox.Show("A porta SMTP deve ser um número inteiro válido.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var con = ConexaoBD.ObterLigacao();
                con.Open();

                // Verificar se já existe registo
                string sqlCheck = "SELECT COUNT(*) FROM configuracao_smtp";
                using var cmdCheck = new MySqlCommand(sqlCheck, con);
                int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                string sqlSave;
                if (count == 0)
                {
                    sqlSave = @"INSERT INTO configuracao_smtp 
                        (smtp_servidor, smtp_porta, smtp_utilizador, smtp_senha, usar_ssl)
                        VALUES (@host, @port, @user, @pass, @ssl)";
                }
                else
                {
                    sqlSave = @"UPDATE configuracao_smtp SET
                        smtp_servidor=@host, smtp_porta=@port, smtp_utilizador=@user, smtp_senha=@pass, usar_ssl=@ssl
                        WHERE id > 0";
                }

                using var cmdSave = new MySqlCommand(sqlSave, con);
                cmdSave.Parameters.AddWithValue("@host", host);
                cmdSave.Parameters.AddWithValue("@port", port);
                cmdSave.Parameters.AddWithValue("@user", user);
                cmdSave.Parameters.AddWithValue("@pass", pass);
                cmdSave.Parameters.AddWithValue("@ssl", ssl ? 1 : 0);

                cmdSave.ExecuteNonQuery();

                MessageBox.Show("Configurações SMTP gravadas com sucesso.", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gravar configurações: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
