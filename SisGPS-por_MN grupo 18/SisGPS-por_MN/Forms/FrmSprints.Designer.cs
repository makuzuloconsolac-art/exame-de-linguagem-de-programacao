namespace SisGPS_por_MN.Forms
{
    partial class FrmSprints
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
            dgvSprints = new DataGridView();
            grpFiltros = new GroupBox();
            label1 = new Label();
            cmbProjecto = new ComboBox();
            grpNovo = new GroupBox();
            lblNome = new Label();
            txtNome = new TextBox();
            lblInicio = new Label();
            dtpInicio = new DateTimePicker();
            lblFim = new Label();
            dtpFim = new DateTimePicker();
            btnNovo = new Button();
            btnEditarMembro = new Button();
            btnEncerrar = new Button();
            btnCancelar = new Button();
            btnExportarCsv = new Button();
            btnExportarPdf = new Button();
            lblTarefasPendentes = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvSprints).BeginInit();
            grpFiltros.SuspendLayout();
            grpNovo.SuspendLayout();
            SuspendLayout();
            // grpFiltros
            grpFiltros.Controls.Add(label1);
            grpFiltros.Controls.Add(cmbProjecto);
            grpFiltros.Location = new Point(12, 12);
            grpFiltros.Name = "grpFiltros";
            grpFiltros.Size = new Size(280, 70);
            grpFiltros.TabIndex = 0;
            grpFiltros.Text = "Projecto";
            // label1
            label1.AutoSize = true;
            label1.Location = new Point(10, 22);
            label1.Name = "label1";
            label1.Text = "Seleccionar:";
            // cmbProjecto
            cmbProjecto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProjecto.FormattingEnabled = true;
            cmbProjecto.Location = new Point(90, 18);
            cmbProjecto.Name = "cmbProjecto";
            cmbProjecto.Size = new Size(170, 23);
            cmbProjecto.TabIndex = 0;
            cmbProjecto.SelectedIndexChanged += cmbProjecto_SelectedIndexChanged;
            // grpNovo
            grpNovo.Controls.Add(lblNome);
            grpNovo.Controls.Add(txtNome);
            grpNovo.Controls.Add(lblInicio);
            grpNovo.Controls.Add(dtpInicio);
            grpNovo.Controls.Add(lblFim);
            grpNovo.Controls.Add(dtpFim);
            grpNovo.Location = new Point(12, 95);
            grpNovo.Name = "grpNovo";
            grpNovo.Size = new Size(280, 160);
            grpNovo.TabIndex = 1;
            grpNovo.Text = "Dados do Sprint";
            // lblNome
            lblNome.AutoSize = true;
            lblNome.Location = new Point(10, 28);
            lblNome.Text = "Nome:";
            // txtNome
            txtNome.Location = new Point(80, 24);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(185, 23);
            txtNome.TabIndex = 0;
            // lblInicio
            lblInicio.AutoSize = true;
            lblInicio.Location = new Point(10, 65);
            lblInicio.Text = "Início:";
            // dtpInicio
            dtpInicio.Format = DateTimePickerFormat.Short;
            dtpInicio.Location = new Point(80, 60);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(185, 23);
            dtpInicio.TabIndex = 1;
            // lblFim
            lblFim.AutoSize = true;
            lblFim.Location = new Point(10, 105);
            lblFim.Text = "Fim:";
            // dtpFim
            dtpFim.Format = DateTimePickerFormat.Short;
            dtpFim.Location = new Point(80, 100);
            dtpFim.Name = "dtpFim";
            dtpFim.Size = new Size(185, 23);
            dtpFim.TabIndex = 2;
            dtpFim.Value = DateTime.Today.AddDays(14);
            // btnNovo
            btnNovo.Location = new Point(12, 270);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(130, 30);
            btnNovo.TabIndex = 2;
            btnNovo.Text = "➕ Novo Sprint";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // btnEditarMembro
            btnEditarMembro.Location = new Point(162, 270);
            btnEditarMembro.Name = "btnEditarMembro";
            btnEditarMembro.Size = new Size(130, 30);
            btnEditarMembro.TabIndex = 3;
            btnEditarMembro.Text = "✏ Editar";
            btnEditarMembro.UseVisualStyleBackColor = true;
            btnEditarMembro.Click += btnEditarMembro_Click;
            // btnEncerrar
            btnEncerrar.Location = new Point(12, 315);
            btnEncerrar.Name = "btnEncerrar";
            btnEncerrar.Size = new Size(130, 30);
            btnEncerrar.TabIndex = 4;
            btnEncerrar.Text = "✔ Encerrar";
            btnEncerrar.UseVisualStyleBackColor = true;
            btnEncerrar.Click += btnEncerrar_Click;
            // btnCancelar
            btnCancelar.Location = new Point(162, 315);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 30);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Fechar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // btnExportarCsv
            btnExportarCsv.Location = new Point(12, 360);
            btnExportarCsv.Name = "btnExportarCsv";
            btnExportarCsv.Size = new Size(130, 30);
            btnExportarCsv.TabIndex = 7;
            btnExportarCsv.Text = "📄 Exportar CSV";
            btnExportarCsv.UseVisualStyleBackColor = true;
            btnExportarCsv.Click += btnExportarCsv_Click;
            // btnExportarPdf
            btnExportarPdf.Location = new Point(162, 360);
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(130, 30);
            btnExportarPdf.TabIndex = 8;
            btnExportarPdf.Text = "📕 Exportar PDF";
            btnExportarPdf.UseVisualStyleBackColor = true;
            btnExportarPdf.Click += btnExportarPdf_Click;
            // lblTarefasPendentes
            lblTarefasPendentes.AutoSize = true;
            lblTarefasPendentes.Location = new Point(305, 15);
            lblTarefasPendentes.Name = "lblTarefasPendentes";
            lblTarefasPendentes.Size = new Size(100, 15);
            lblTarefasPendentes.Text = "";
            // dgvSprints
            dgvSprints.AllowUserToAddRows = false;
            dgvSprints.AllowUserToDeleteRows = false;
            dgvSprints.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSprints.Location = new Point(305, 40);
            dgvSprints.MultiSelect = false;
            dgvSprints.Name = "dgvSprints";
            dgvSprints.ReadOnly = true;
            dgvSprints.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSprints.Size = new Size(470, 350);
            dgvSprints.TabIndex = 6;
            // FrmSprints
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 415);
            Controls.Add(lblTarefasPendentes);
            Controls.Add(dgvSprints);
            Controls.Add(btnCancelar);
            Controls.Add(btnEncerrar);
            Controls.Add(btnEditarMembro);
            Controls.Add(btnNovo);
            Controls.Add(btnExportarCsv);
            Controls.Add(btnExportarPdf);
            Controls.Add(grpNovo);
            Controls.Add(grpFiltros);
            Name = "FrmSprints";
            Text = "Gestão de Sprints";
            Load += FrmSprints_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSprints).EndInit();
            grpFiltros.ResumeLayout(false);
            grpFiltros.PerformLayout();
            grpNovo.ResumeLayout(false);
            grpNovo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvSprints;
        private GroupBox grpFiltros;
        private Label label1;
        private ComboBox cmbProjecto;
        private GroupBox grpNovo;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblInicio;
        private DateTimePicker dtpInicio;
        private Label lblFim;
        private DateTimePicker dtpFim;
        private Button btnNovo;
        private Button btnEditarMembro;
        private Button btnEncerrar;
        private Button btnCancelar;
        private Button btnExportarCsv;
        private Button btnExportarPdf;
        private Label lblTarefasPendentes;
    }
}