using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Servicos;
using SisGPS_por_MN.Utils;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmEmails : Form
    {
        private readonly ServicoEmail _servico = new();

        public FrmEmails()
        {
            InitializeComponent();
            Listar();
        }

        private void Listar()
        {
            dgvEmails.DataSource = null;
            dgvEmails.DataSource = _servico.Listar().ToList();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            var dest = txtDestinatario.Text.Trim();
            var ass = txtAssunto.Text.Trim();
            var corpo = txtCorpo.Text.Trim();

            if (!Validador.EmailValido(dest))
            {
                MessageBox.Show("E-mail inválido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ass) || string.IsNullOrWhiteSpace(corpo))
            {
                MessageBox.Show("Assunto e corpo são obrigatórios.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var msg = new EmailMensagem
                {
                    Destinatario = dest,
                    Assunto = ass,
                    Corpo = corpo,
                    Remetente = Sessao.UtilizadorActual?.Nome ?? "sisgps@empresa.ao"
                };
                _servico.Criar(msg);
                txtDestinatario.Clear();
                txtAssunto.Clear();
                txtCorpo.Clear();
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (dgvEmails.CurrentRow?.DataBoundItem is not EmailMensagem msg) return;
            try
            {
                bool ok = _servico.Enviar(msg);
                Listar();
                MessageBox.Show(ok
                    ? "E-mail enviado com sucesso."
                    : "Registo guardado. Configure SISGPS_SMTP_PASS para envio real.",
                    "E-mail", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var termo = txtBusca.Text.Trim();
            dgvEmails.DataSource = null;
            dgvEmails.DataSource = string.IsNullOrWhiteSpace(termo)
                ? _servico.Listar().ToList()
                : _servico.Buscar(termo).ToList();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEmails.CurrentRow?.DataBoundItem is not EmailMensagem msg) return;
            if (MessageBox.Show("Eliminar este registo?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            _servico.Eliminar(msg.Id);
            Listar();
        }

        private void dgvEmails_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmails.CurrentRow?.DataBoundItem is not EmailMensagem msg) return;
            txtDestinatario.Text = msg.Destinatario;
            txtAssunto.Text = msg.Assunto;
            txtCorpo.Text = msg.Corpo;
        }
    }
}
