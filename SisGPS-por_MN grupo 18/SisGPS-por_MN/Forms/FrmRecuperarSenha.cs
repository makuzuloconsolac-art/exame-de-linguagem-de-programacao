using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmRecuperarSenha : Form
    {
        private readonly UtilizadorRepository _userRepo = new();
        private Utilizador? _userEncontrado = null;

        public FrmRecuperarSenha()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Introduza o nome de utilizador.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var userObj = _userRepo.ObterTodos()
                    .FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (userObj == null)
                {
                    MessageBox.Show("Nome de utilizador não encontrado.", "Erro", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(userObj.PerguntaSeguranca))
                {
                    MessageBox.Show("Este utilizador não tem pergunta de segurança configurada.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _userEncontrado = userObj;
                lblPergunta.Text = userObj.PerguntaSeguranca;
                
                // Activar controlos seguintes
                txtResposta.Enabled = true;
                txtNovaSenha.Enabled = true;
                btnRecuperar.Enabled = true;
                txtUsername.Enabled = false;
                btnBuscar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao procurar utilizador: {ex.Message}", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            if (_userEncontrado == null) return;

            string respostaInput = txtResposta.Text.Trim();
            string novaSenha = txtNovaSenha.Text;

            if (string.IsNullOrWhiteSpace(respostaInput) || string.IsNullOrWhiteSpace(novaSenha))
            {
                MessageBox.Show("Preencha a resposta e a nova palavra-passe.", "Validação", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Comparação simples sem distinção de maiúsculas/minúsculas
            if (!respostaInput.Equals(_userEncontrado.RespostaSeguranca.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Resposta de segurança incorreta.", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _userEncontrado.PasswordHash = Criptografia.HashPassword(novaSenha);
                _userRepo.Actualizar(_userEncontrado);

                MessageBox.Show("Palavra-passe recuperada com sucesso.", "Sucesso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar palavra-passe: {ex.Message}", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
