using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Servicos;
using SisGPS_por_MN.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmPrincipal : Form
    {
        private readonly ServicoDashboard _dashboard = new();

        public FrmPrincipal()
        {
            InitializeComponent();
            AplicarPermissoes();
        }

        private void AplicarPermissoes()
        {
            var u = Sessao.UtilizadorActual;
            if (u == null) return;

            bool admin = u.EhAdministrador;

            // Configurar visibilidade de menus superiores
            menuProjectos.Visible = admin;
            menuEquipas.Visible = admin;
            menuSprints.Visible = admin;
            menuRelatorios.Visible = admin;
            menuEmails.Visible = admin;
            menuUtilizadores.Visible = admin;

            // Configurar visibilidade e posição dos atalhos rápidos
            if (!admin)
            {
                // Organizar os botões do funcionário
                btnAtalhoTarefas.Location = new Point(15, 30);
                btnAtalhoKanban.Location = new Point(195, 30);
                btnAtalhoSair.Location = new Point(15, 90);

                btnAtalhoProjectos.Visible = false;
                btnAtalhoEquipas.Visible = false;
                btnAtalhoSprints.Visible = false;
                btnAtalhoRelatorios.Visible = false;
                btnAtalhoEmails.Visible = false;
                btnAtalhoUtilizadores.Visible = false;
                btnAtalhoConfigSMTP.Visible = false;
                
                // Redimensionar painel de atalhos para funcionário
                grpAtalhos.Size = new Size(380, 160);
            }
            else
            {
                // Restaurar posições normais para administrador
                btnAtalhoProjectos.Location = new Point(15, 30);
                btnAtalhoEquipas.Location = new Point(195, 30);
                btnAtalhoSprints.Location = new Point(15, 90);
                btnAtalhoTarefas.Location = new Point(195, 90);
                btnAtalhoKanban.Location = new Point(15, 150);
                btnAtalhoRelatorios.Location = new Point(195, 150);
                btnAtalhoEmails.Location = new Point(15, 210);
                btnAtalhoUtilizadores.Location = new Point(195, 210);
                btnAtalhoConfigSMTP.Location = new Point(15, 270);
                btnAtalhoSair.Location = new Point(195, 270);

                btnAtalhoProjectos.Visible = true;
                btnAtalhoEquipas.Visible = true;
                btnAtalhoSprints.Visible = true;
                btnAtalhoRelatorios.Visible = true;
                btnAtalhoEmails.Visible = true;
                btnAtalhoUtilizadores.Visible = true;
                btnAtalhoConfigSMTP.Visible = true;
                
                grpAtalhos.Size = new Size(380, 340);
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Text = "SisGPS — Sistema de Gestão de Projectos de Software";

            bool ok = ConexaoBD.TestarConexao();
            var u = Sessao.UtilizadorActual;
            var perfil = u?.EhAdministrador == true ? "Administrador" : "Funcionário";

            lblUtilizador.Text = ok
                ? $"Utilizador: {u?.Nome} | Perfil: {perfil} | Base de Dados ligada"
                : $"Utilizador: {u?.Nome} | Sem ligação à Base de Dados";

            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.BringToFront();
            CarregarDashboard();
        }



        private void CarregarDashboard()
        {
            try
            {
                int? membroId = Sessao.UtilizadorActual?.MembroId;
                var r = _dashboard.ObterResumo(membroId);

                lblProjTotaisVal.Text = r.TotalProjectos.ToString();
                lblProjActivosVal.Text = r.ProjectosActivos.ToString();
                lblProjConcluidosVal.Text = r.ProjectosConcluidos.ToString();

                lblEquipasVal.Text = r.TotalEquipas.ToString();
                lblMembrosVal.Text = r.TotalMembros.ToString();
                lblSprintsAbertosVal.Text = r.SprintsAbertos.ToString();

                lblTarefasPendVal.Text = r.TarefasPendentes.ToString();
                lblTarefasProgVal.Text = r.TarefasEmProgresso.ToString();
                lblTarefasConclVal.Text = r.TarefasConcluidas.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro ao carregar Dashboard: " + ex.Message);
            }
        }

        private void menuProjectos_Click(object sender, EventArgs e) =>
            AbrirFilho(new FrmProjectos());

        private void menuEquipas_Click(object sender, EventArgs e) =>
            AbrirFilho(new FrmEquipa());

        private void menuSprints_Click(object sender, EventArgs e) =>
            AbrirFilho(new FrmSprints());

        private void menuTarefas_Click(object sender, EventArgs e) =>
            AbrirFilho(new FrmTarefas());

        private void menuKanban_Click(object sender, EventArgs e) =>
            AbrirFilho(new FrmKanban());

        private void menuRelatorios_Click(object sender, EventArgs e) =>
            AbrirFilho(new FrmRelatorio());

        private void menuEmails_Click(object sender, EventArgs e) =>
            AbrirFilho(new FrmEmails());

        private void menuUtilizadores_Click(object sender, EventArgs e) =>
            AbrirFilho(new FrmUtilizadores());

        private void btnAtalhoConfigSMTP_Click(object sender, EventArgs e)
        {
            using (var smtpForm = new FrmConfigSMTP())
            {
                smtpForm.ShowDialog();
            }
        }

        private void menuSair_Click(object sender, EventArgs e)
        {
            Sessao.Terminar();
            Application.Restart(); // Reiniciar aplicação para voltar ao ecrã de login
        }

        private void AbrirFilho(Form filho)
        {
            filho.MdiParent = this;
            filho.Show();
            pnlDashboard.SendToBack();
            filho.FormClosed += (s, args) => {
                if (MdiChildren.Length == 0)
                {
                    pnlDashboard.BringToFront();
                    CarregarDashboard();
                }
            };
        }

        protected override void OnMdiChildActivate(EventArgs e)
        {
            base.OnMdiChildActivate(e);
            if (MdiChildren.Length == 0)
            {
                pnlDashboard.BringToFront();
                CarregarDashboard();
            }
        }
    }
}
