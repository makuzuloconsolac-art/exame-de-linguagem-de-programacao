using SisGPS_por_MN.Servicos;
using SisGPS_por_MN.Utils;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmLogin : Form
    {
        private readonly ServicoAutenticacao _auth = new();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            lblErro.Text = string.Empty;
            var user = txtUsername.Text.Trim();
            var pass = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                lblErro.Text = "Preencha utilizador e palavra-passe.";
                return;
            }

            try
            {
                var u = _auth.Login(user, pass);
                if (u == null)
                {
                    lblErro.Text = "Credenciais inválidas.";
                    return;
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnEntrar_Click(sender, e);
        }

        private void lnkRegistar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var reg = new FrmRegistoUtilizador())
            {
                reg.ShowDialog();
            }
        }

        private void lnkRecuperar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var rec = new FrmRecuperarSenha())
            {
                rec.ShowDialog();
            }
        }
    }
}
