namespace SisGPS_por_MN.Forms
{
    partial class FrmRelatorio
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpFiltros = new GroupBox();
            lblProjeto = new Label();
            cmbProjecto = new ComboBox();
            lblEquipa = new Label();
            cmbEquipa = new ComboBox();
            btnGerar = new Button();
            btnExportarCsv = new Button();
            btnExportarPdf = new Button();
            tabRelatorio = new TabControl();
            tabProgresso = new TabPage();
            dgvProgresso = new DataGridView();
            tabVelocidade = new TabPage();
            dgvVelocidade = new DataGridView();
            tabHoras = new TabPage();
            dgvHoras = new DataGridView();
            lblStatus = new Label();
            grpFiltros.SuspendLayout();
            tabRelatorio.SuspendLayout();
            tabProgresso.SuspendLayout();
            tabVelocidade.SuspendLayout();
            tabHoras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProgresso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvVelocidade).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHoras).BeginInit();
            SuspendLayout();

            grpFiltros.Controls.Add(lblProjeto);
            grpFiltros.Controls.Add(cmbProjecto);
            grpFiltros.Controls.Add(lblEquipa);
            grpFiltros.Controls.Add(cmbEquipa);
            grpFiltros.Controls.Add(btnGerar);
            grpFiltros.Controls.Add(btnExportarCsv);
            grpFiltros.Controls.Add(btnExportarPdf);
            grpFiltros.Location = new Point(10, 10);
            grpFiltros.Size = new Size(760, 65);
            grpFiltros.Text = "Filtros do Relatório";

            lblProjeto.AutoSize = true;
            lblProjeto.Location = new Point(10, 30);
            lblProjeto.Text = "Projecto:";

            cmbProjecto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProjecto.Location = new Point(75, 26);
            cmbProjecto.Size = new Size(180, 23);

            lblEquipa.AutoSize = true;
            lblEquipa.Location = new Point(270, 30);
            lblEquipa.Text = "Equipa:";

            cmbEquipa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipa.Location = new Point(325, 26);
            cmbEquipa.Size = new Size(180, 23);

            btnGerar.Location = new Point(520, 24);
            btnGerar.Size = new Size(70, 28);
            btnGerar.Text = "Gerar";
            btnGerar.Click += btnGerar_Click;

            btnExportarCsv.Location = new Point(598, 24);
            btnExportarCsv.Size = new Size(70, 28);
            btnExportarCsv.Text = "CSV";
            btnExportarCsv.Click += btnExportarCsv_Click;

            btnExportarPdf.Location = new Point(676, 24);
            btnExportarPdf.Size = new Size(70, 28);
            btnExportarPdf.Text = "PDF";
            btnExportarPdf.Click += btnExportarPdf_Click;

            tabRelatorio.Controls.Add(tabProgresso);
            tabRelatorio.Controls.Add(tabVelocidade);
            tabRelatorio.Controls.Add(tabHoras);
            tabRelatorio.Location = new Point(10, 85);
            tabRelatorio.Size = new Size(760, 380);

            tabProgresso.Controls.Add(dgvProgresso);
            tabProgresso.Text = "Progresso";
            tabProgresso.Size = new Size(752, 352);
            dgvProgresso.Dock = DockStyle.Fill;
            dgvProgresso.ReadOnly = true;
            dgvProgresso.AllowUserToAddRows = false;

            tabVelocidade.Controls.Add(dgvVelocidade);
            tabVelocidade.Text = "Velocidade";
            tabVelocidade.Size = new Size(752, 352);
            dgvVelocidade.Dock = DockStyle.Fill;
            dgvVelocidade.ReadOnly = true;
            dgvVelocidade.AllowUserToAddRows = false;

            tabHoras.Controls.Add(dgvHoras);
            tabHoras.Text = "Horas";
            tabHoras.Size = new Size(752, 352);
            dgvHoras.Dock = DockStyle.Fill;
            dgvHoras.ReadOnly = true;
            dgvHoras.AllowUserToAddRows = false;

            lblStatus.Location = new Point(10, 472);
            lblStatus.Size = new Size(760, 20);

            ClientSize = new Size(784, 502);
            Controls.Add(lblStatus);
            Controls.Add(tabRelatorio);
            Controls.Add(grpFiltros);
            Text = "Relatórios do Sistema";
            grpFiltros.ResumeLayout(false);
            grpFiltros.PerformLayout();
            tabRelatorio.ResumeLayout(false);
            tabProgresso.ResumeLayout(false);
            tabVelocidade.ResumeLayout(false);
            tabHoras.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProgresso).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvVelocidade).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHoras).EndInit();
            ResumeLayout(false);
        }

        private GroupBox grpFiltros;
        private Label lblProjeto;
        private ComboBox cmbProjecto;
        private Label lblEquipa;
        private ComboBox cmbEquipa;
        private Button btnGerar;
        private Button btnExportarCsv;
        private Button btnExportarPdf;
        private TabControl tabRelatorio;
        private TabPage tabProgresso;
        private DataGridView dgvProgresso;
        private TabPage tabVelocidade;
        private DataGridView dgvVelocidade;
        private TabPage tabHoras;
        private DataGridView dgvHoras;
        private Label lblStatus;
    }
}
