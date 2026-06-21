namespace SisGPS_por_MN.Forms
{
    partial class FrmUtilizadores
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpFiltros = new GroupBox();
            lblBusca = new Label();
            txtBusca = new TextBox();
            lblNivel = new Label();
            cmbNivelFiltro = new ComboBox();
            lblEstado = new Label();
            cmbEstadoFiltro = new ComboBox();
            btnFiltrar = new Button();
            dgvUtilizadores = new DataGridView();
            pnlAccoes = new Panel();
            btnNovo = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnEstadoToggle = new Button();
            btnExportarCsv = new Button();
            btnExportarPdf = new Button();
            lblInfo = new Label();
            grpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUtilizadores).BeginInit();
            pnlAccoes.SuspendLayout();
            SuspendLayout();

            // grpFiltros
            grpFiltros.Controls.Add(lblBusca);
            grpFiltros.Controls.Add(txtBusca);
            grpFiltros.Controls.Add(lblNivel);
            grpFiltros.Controls.Add(cmbNivelFiltro);
            grpFiltros.Controls.Add(lblEstado);
            grpFiltros.Controls.Add(cmbEstadoFiltro);
            grpFiltros.Controls.Add(btnFiltrar);
            grpFiltros.Location = new Point(12, 12);
            grpFiltros.Size = new Size(860, 65);
            grpFiltros.Text = "Filtros de Pesquisa";

            // lblBusca
            lblBusca.AutoSize = true;
            lblBusca.Location = new Point(10, 30);
            lblBusca.Text = "Pesquisar:";

            // txtBusca
            txtBusca.Location = new Point(70, 26);
            txtBusca.Size = new Size(200, 23);

            // lblNivel
            lblNivel.AutoSize = true;
            lblNivel.Location = new Point(290, 30);
            lblNivel.Text = "Nível:";

            // cmbNivelFiltro
            cmbNivelFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNivelFiltro.FormattingEnabled = true;
            cmbNivelFiltro.Location = new Point(330, 26);
            cmbNivelFiltro.Size = new Size(130, 23);

            // lblEstado
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(480, 30);
            lblEstado.Text = "Estado:";

            // cmbEstadoFiltro
            cmbEstadoFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadoFiltro.FormattingEnabled = true;
            cmbEstadoFiltro.Location = new Point(530, 26);
            cmbEstadoFiltro.Size = new Size(110, 23);

            // btnFiltrar
            btnFiltrar.Location = new Point(660, 24);
            btnFiltrar.Size = new Size(100, 26);
            btnFiltrar.Text = "🔍 Filtrar";
            btnFiltrar.Click += btnFiltrar_Click;

            // dgvUtilizadores
            dgvUtilizadores.AllowUserToAddRows = false;
            dgvUtilizadores.AllowUserToDeleteRows = false;
            dgvUtilizadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUtilizadores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUtilizadores.Location = new Point(12, 85);
            dgvUtilizadores.ReadOnly = true;
            dgvUtilizadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUtilizadores.Size = new Size(860, 310);
            dgvUtilizadores.TabIndex = 1;

            // pnlAccoes
            pnlAccoes.Controls.Add(btnNovo);
            pnlAccoes.Controls.Add(btnEditar);
            pnlAccoes.Controls.Add(btnEliminar);
            pnlAccoes.Controls.Add(btnEstadoToggle);
            pnlAccoes.Controls.Add(btnExportarCsv);
            pnlAccoes.Controls.Add(btnExportarPdf);
            pnlAccoes.Location = new Point(12, 405);
            pnlAccoes.Size = new Size(860, 45);

            // btnNovo
            btnNovo.Location = new Point(0, 5);
            btnNovo.Size = new Size(110, 32);
            btnNovo.Text = "➕ Novo";
            btnNovo.Click += btnNovo_Click;

            // btnEditar
            btnEditar.Location = new Point(120, 5);
            btnEditar.Size = new Size(110, 32);
            btnEditar.Text = "✏ Editar";
            btnEditar.Click += btnEditar_Click;

            // btnEliminar
            btnEliminar.Location = new Point(240, 5);
            btnEliminar.Size = new Size(110, 32);
            btnEliminar.Text = "❌ Eliminar";
            btnEliminar.Click += btnEliminar_Click;

            // btnEstadoToggle
            btnEstadoToggle.Location = new Point(360, 5);
            btnEstadoToggle.Size = new Size(140, 32);
            btnEstadoToggle.Text = "🔒 Activar/Desactivar";
            btnEstadoToggle.Click += btnEstadoToggle_Click;

            // btnExportarCsv
            btnExportarCsv.Location = new Point(610, 5);
            btnExportarCsv.Size = new Size(110, 32);
            btnExportarCsv.Text = "📄 Exportar CSV";
            btnExportarCsv.Click += btnExportarCsv_Click;

            // btnExportarPdf
            btnExportarPdf.Location = new Point(730, 5);
            btnExportarPdf.Size = new Size(110, 32);
            btnExportarPdf.Text = "📕 Exportar PDF";
            btnExportarPdf.Click += btnExportarPdf_Click;

            // lblInfo
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(12, 455);
            lblInfo.Text = "0 utilizador(es) encontrado(s).";

            // FrmUtilizadores
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 485);
            Controls.Add(lblInfo);
            Controls.Add(pnlAccoes);
            Controls.Add(dgvUtilizadores);
            Controls.Add(grpFiltros);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmUtilizadores";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestão de Utilizadores";
            grpFiltros.ResumeLayout(false);
            grpFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUtilizadores).EndInit();
            pnlAccoes.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpFiltros;
        private Label lblBusca;
        private TextBox txtBusca;
        private Label lblNivel;
        private ComboBox cmbNivelFiltro;
        private Label lblEstado;
        private ComboBox cmbEstadoFiltro;
        private Button btnFiltrar;
        private DataGridView dgvUtilizadores;
        private Panel pnlAccoes;
        private Button btnNovo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnEstadoToggle;
        private Button btnExportarCsv;
        private Button btnExportarPdf;
        private Label lblInfo;
    }
}
