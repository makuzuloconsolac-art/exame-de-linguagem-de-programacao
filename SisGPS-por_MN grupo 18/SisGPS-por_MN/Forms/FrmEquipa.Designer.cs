namespace SisGPS_por_MN.Forms
{
    partial class FrmEquipa
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
            // Equipas
            grpEquipas = new GroupBox();
            dgvEquipas = new DataGridView();
            btnNovo = new Button();
            btnEditarEquipa = new Button();
            // Filtros membros
            grpFiltros = new GroupBox();
            label1 = new Label();
            cmbPapelFiltro = new ComboBox();
            lblProgresso = new Label();
            progressBar1 = new ProgressBar();
            // Membros
            grpMembros = new GroupBox();
            dgvMembros = new DataGridView();
            btnNovoMembro = new Button();
            btnEditarMembro = new Button();
            btnToggleDisponivel = new Button();
            btnCancelar = new Button();
            btnExportarCsv = new Button();
            btnExportarPdf = new Button();

            grpEquipas.SuspendLayout();
            grpFiltros.SuspendLayout();
            grpMembros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEquipas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMembros).BeginInit();
            SuspendLayout();

            // ── grpEquipas ──────────────────────────────────────────────────
            grpEquipas.Controls.Add(dgvEquipas);
            grpEquipas.Controls.Add(btnNovo);
            grpEquipas.Controls.Add(btnEditarEquipa);
            grpEquipas.Location = new Point(12, 12);
            grpEquipas.Name = "grpEquipas";
            grpEquipas.Size = new Size(380, 390);
            grpEquipas.TabIndex = 0;
            grpEquipas.Text = "Equipas";
            // dgvEquipas
            dgvEquipas.AllowUserToAddRows = false;
            dgvEquipas.AllowUserToDeleteRows = false;
            dgvEquipas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEquipas.Location = new Point(10, 55);
            dgvEquipas.MultiSelect = false;
            dgvEquipas.Name = "dgvEquipas";
            dgvEquipas.ReadOnly = true;
            dgvEquipas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEquipas.Size = new Size(356, 320);
            dgvEquipas.TabIndex = 0;
            dgvEquipas.SelectionChanged += dgvProjectos_SelectionChanged;
            // btnNovo
            btnNovo.Location = new Point(10, 22);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(120, 28);
            btnNovo.TabIndex = 1;
            btnNovo.Text = "➕ Nova Equipa";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // btnEditarEquipa
            btnEditarEquipa.Location = new Point(145, 22);
            btnEditarEquipa.Name = "btnEditarEquipa";
            btnEditarEquipa.Size = new Size(110, 28);
            btnEditarEquipa.TabIndex = 2;
            btnEditarEquipa.Text = "✏ Editar Equipa";
            btnEditarEquipa.UseVisualStyleBackColor = true;
            btnEditarEquipa.Click += btnEditarEquipa_Click;

            // ── grpFiltros ──────────────────────────────────────────────────
            grpFiltros.Controls.Add(label1);
            grpFiltros.Controls.Add(cmbPapelFiltro);
            grpFiltros.Controls.Add(lblProgresso);
            grpFiltros.Controls.Add(progressBar1);
            grpFiltros.Location = new Point(407, 12);
            grpFiltros.Name = "grpFiltros";
            grpFiltros.Size = new Size(430, 70);
            grpFiltros.TabIndex = 1;
            grpFiltros.Text = "Filtros & Disponibilidade";
            // label1
            label1.AutoSize = true;
            label1.Location = new Point(10, 28);
            label1.Name = "label1";
            label1.Text = "Papel:";
            // cmbPapelFiltro
            cmbPapelFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPapelFiltro.FormattingEnabled = true;
            cmbPapelFiltro.Location = new Point(55, 24);
            cmbPapelFiltro.Name = "cmbPapelFiltro";
            cmbPapelFiltro.Size = new Size(130, 23);
            cmbPapelFiltro.TabIndex = 0;
            cmbPapelFiltro.SelectedIndexChanged += cmbPapelFiltro_SelectedIndexChanged;
            // lblProgresso
            lblProgresso.AutoSize = true;
            lblProgresso.Location = new Point(200, 28);
            lblProgresso.Name = "lblProgresso";
            lblProgresso.Text = "Disponíveis:";
            // progressBar1
            progressBar1.Location = new Point(285, 24);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(130, 23);
            progressBar1.TabIndex = 1;

            // ── grpMembros ──────────────────────────────────────────────────
            grpMembros.Controls.Add(dgvMembros);
            grpMembros.Controls.Add(btnNovoMembro);
            grpMembros.Controls.Add(btnEditarMembro);
            grpMembros.Controls.Add(btnToggleDisponivel);
            grpMembros.Location = new Point(407, 95);
            grpMembros.Name = "grpMembros";
            grpMembros.Size = new Size(430, 307);
            grpMembros.TabIndex = 2;
            grpMembros.Text = "Membros da Equipa Seleccionada";
            // dgvMembros
            dgvMembros.AllowUserToAddRows = false;
            dgvMembros.AllowUserToDeleteRows = false;
            dgvMembros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMembros.Location = new Point(10, 60);
            dgvMembros.MultiSelect = false;
            dgvMembros.Name = "dgvMembros";
            dgvMembros.ReadOnly = true;
            dgvMembros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembros.Size = new Size(406, 232);
            dgvMembros.TabIndex = 0;
            // btnNovoMembro
            btnNovoMembro.Location = new Point(10, 22);
            btnNovoMembro.Name = "btnNovoMembro";
            btnNovoMembro.Size = new Size(120, 28);
            btnNovoMembro.TabIndex = 1;
            btnNovoMembro.Text = "➕ Novo Membro";
            btnNovoMembro.UseVisualStyleBackColor = true;
            btnNovoMembro.Click += btnNovoMembro_Click;
            // btnEditarMembro
            btnEditarMembro.Location = new Point(145, 22);
            btnEditarMembro.Name = "btnEditarMembro";
            btnEditarMembro.Size = new Size(100, 28);
            btnEditarMembro.TabIndex = 2;
            btnEditarMembro.Text = "✏ Editar";
            btnEditarMembro.UseVisualStyleBackColor = true;
            btnEditarMembro.Click += btnEditarMembro_Click;
            // btnToggleDisponivel
            btnToggleDisponivel.Location = new Point(260, 22);
            btnToggleDisponivel.Name = "btnToggleDisponivel";
            btnToggleDisponivel.Size = new Size(140, 28);
            btnToggleDisponivel.TabIndex = 3;
            btnToggleDisponivel.Text = "⇄ Toggle Disponível";
            btnToggleDisponivel.UseVisualStyleBackColor = true;
            btnToggleDisponivel.Click += btnToggleDisponivel_Click;

            // btnCancelar (fechar)
            btnCancelar.Location = new Point(762, 412);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 28);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Fechar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;

            // btnExportarCsv
            btnExportarCsv.Location = new Point(500, 412);
            btnExportarCsv.Name = "btnExportarCsv";
            btnExportarCsv.Size = new Size(115, 28);
            btnExportarCsv.TabIndex = 4;
            btnExportarCsv.Text = "📄 Exportar CSV";
            btnExportarCsv.UseVisualStyleBackColor = true;
            btnExportarCsv.Click += btnExportarCsv_Click;

            // btnExportarPdf
            btnExportarPdf.Location = new Point(625, 412);
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(115, 28);
            btnExportarPdf.TabIndex = 5;
            btnExportarPdf.Text = "📕 Exportar PDF";
            btnExportarPdf.UseVisualStyleBackColor = true;
            btnExportarPdf.Click += btnExportarPdf_Click;

            // FrmEquipa
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 450);
            Controls.Add(grpMembros);
            Controls.Add(grpFiltros);
            Controls.Add(grpEquipas);
            Controls.Add(btnCancelar);
            Controls.Add(btnExportarCsv);
            Controls.Add(btnExportarPdf);
            Name = "FrmEquipa";
            Text = "Gestão de Equipas & Membros";
            Load += FrmEquipa_Load;
            grpEquipas.ResumeLayout(false);
            grpFiltros.ResumeLayout(false);
            grpFiltros.PerformLayout();
            grpMembros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEquipas).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMembros).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpEquipas;
        private DataGridView dgvEquipas;
        private Button btnNovo;
        private Button btnEditarEquipa;
        private GroupBox grpFiltros;
        private Label label1;
        private ComboBox cmbPapelFiltro;
        private Label lblProgresso;
        private ProgressBar progressBar1;
        private GroupBox grpMembros;
        private DataGridView dgvMembros;
        private Button btnNovoMembro;
        private Button btnEditarMembro;
        private Button btnToggleDisponivel;
        private Button btnCancelar;
        private Button btnExportarCsv;
        private Button btnExportarPdf;
    }
}