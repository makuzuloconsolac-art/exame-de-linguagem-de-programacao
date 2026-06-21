namespace SisGPS_por_MN.Forms
{
    partial class FrmTarefas
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
            grpFiltros      = new GroupBox();
            lblProjeto      = new Label();
            cmbProjecto     = new ComboBox();
            lblSprint       = new Label();
            cmbSprint       = new ComboBox();
            lblEstado       = new Label();
            cmbFiltroEstado = new ComboBox();
            btnFiltrar      = new Button();
            dgvTarefas      = new DataGridView();
            grpAcoes        = new GroupBox();
            btnNovo         = new Button();
            btnEditar       = new Button();
            btnAlterarEst   = new Button();
            btnAtribuir     = new Button();
            btnRegistarH    = new Button();
            btnHistorico    = new Button();
            btnEliminar     = new Button();
            btnExportarCsv   = new Button();
            btnExportarPdf   = new Button();
            lblInfo         = new Label();

            grpFiltros.SuspendLayout();
            grpAcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTarefas).BeginInit();
            SuspendLayout();

            // ── grpFiltros ──────────────────────────────────────────────
            grpFiltros.Controls.Add(lblProjeto);
            grpFiltros.Controls.Add(cmbProjecto);
            grpFiltros.Controls.Add(lblSprint);
            grpFiltros.Controls.Add(cmbSprint);
            grpFiltros.Controls.Add(lblEstado);
            grpFiltros.Controls.Add(cmbFiltroEstado);
            grpFiltros.Controls.Add(btnFiltrar);
            grpFiltros.Location = new Point(12, 12);
            grpFiltros.Name     = "grpFiltros";
            grpFiltros.Size     = new Size(760, 65);
            grpFiltros.Text     = "Filtros";
            grpFiltros.TabIndex = 0;
            // lblProjeto
            lblProjeto.AutoSize = true;
            lblProjeto.Location = new Point(10, 28);
            lblProjeto.Text     = "Projecto:";
            // cmbProjecto
            cmbProjecto.DropDownStyle    = ComboBoxStyle.DropDownList;
            cmbProjecto.FormattingEnabled = true;
            cmbProjecto.Location         = new Point(70, 24);
            cmbProjecto.Name             = "cmbProjecto";
            cmbProjecto.Size             = new Size(170, 23);
            cmbProjecto.TabIndex         = 0;
            cmbProjecto.SelectedIndexChanged += cmbProjecto_SelectedIndexChanged;
            // lblSprint
            lblSprint.AutoSize = true;
            lblSprint.Location = new Point(255, 28);
            lblSprint.Text     = "Sprint:";
            // cmbSprint
            cmbSprint.DropDownStyle    = ComboBoxStyle.DropDownList;
            cmbSprint.FormattingEnabled = true;
            cmbSprint.Location         = new Point(305, 24);
            cmbSprint.Name             = "cmbSprint";
            cmbSprint.Size             = new Size(170, 23);
            cmbSprint.TabIndex         = 1;
            cmbSprint.SelectedIndexChanged += cmbSprint_SelectedIndexChanged;
            // lblEstado
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(492, 28);
            lblEstado.Text     = "Estado:";
            // cmbFiltroEstado
            cmbFiltroEstado.DropDownStyle    = ComboBoxStyle.DropDownList;
            cmbFiltroEstado.FormattingEnabled = true;
            cmbFiltroEstado.Location         = new Point(542, 24);
            cmbFiltroEstado.Name             = "cmbFiltroEstado";
            cmbFiltroEstado.Size             = new Size(140, 23);
            cmbFiltroEstado.TabIndex         = 2;
            cmbFiltroEstado.SelectedIndexChanged += cmbFiltroEstado_SelectedIndexChanged;
            // btnFiltrar
            btnFiltrar.Location = new Point(698, 22);
            btnFiltrar.Name     = "btnFiltrar";
            btnFiltrar.Size     = new Size(50, 28);
            btnFiltrar.Text     = "🔍";
            btnFiltrar.UseVisualStyleBackColor = true;
            btnFiltrar.Click   += btnFiltrar_Click;

            // ── dgvTarefas ──────────────────────────────────────────────
            dgvTarefas.AllowUserToAddRows    = false;
            dgvTarefas.AllowUserToDeleteRows = false;
            dgvTarefas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTarefas.Location      = new Point(12, 90);
            dgvTarefas.MultiSelect   = false;
            dgvTarefas.Name          = "dgvTarefas";
            dgvTarefas.ReadOnly      = true;
            dgvTarefas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTarefas.Size          = new Size(640, 380);
            dgvTarefas.TabIndex      = 1;
            dgvTarefas.CellFormatting += dgvTarefas_CellFormatting;

            // ── grpAcoes ────────────────────────────────────────────────
            grpAcoes.Controls.Add(btnNovo);
            grpAcoes.Controls.Add(btnEditar);
            grpAcoes.Controls.Add(btnAlterarEst);
            grpAcoes.Controls.Add(btnAtribuir);
            grpAcoes.Controls.Add(btnRegistarH);
            grpAcoes.Controls.Add(btnHistorico);
            grpAcoes.Controls.Add(btnEliminar);
            grpAcoes.Controls.Add(btnExportarCsv);
            grpAcoes.Controls.Add(btnExportarPdf);
            grpAcoes.Location = new Point(665, 90);
            grpAcoes.Name     = "grpAcoes";
            grpAcoes.Size     = new Size(130, 420);
            grpAcoes.Text     = "Acções";
            grpAcoes.TabIndex = 2;
            // btnNovo
            btnNovo.Location = new Point(10, 25);
            btnNovo.Name     = "btnNovo";
            btnNovo.Size     = new Size(110, 32);
            btnNovo.Text     = "➕ Nova";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click   += btnNovo_Click;
            // btnEditar
            btnEditar.Location = new Point(10, 68);
            btnEditar.Name     = "btnEditar";
            btnEditar.Size     = new Size(110, 32);
            btnEditar.Text     = "✏ Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click   += btnEditar_Click;
            // btnAlterarEst
            btnAlterarEst.Location = new Point(10, 111);
            btnAlterarEst.Name     = "btnAlterarEst";
            btnAlterarEst.Size     = new Size(110, 32);
            btnAlterarEst.Text     = "🔄 Estado";
            btnAlterarEst.UseVisualStyleBackColor = true;
            btnAlterarEst.Click   += btnAlterarEst_Click;
            // btnAtribuir
            btnAtribuir.Location = new Point(10, 154);
            btnAtribuir.Name     = "btnAtribuir";
            btnAtribuir.Size     = new Size(110, 32);
            btnAtribuir.Text     = "👤 Atribuir";
            btnAtribuir.UseVisualStyleBackColor = true;
            btnAtribuir.Click   += btnAtribuir_Click;
            // btnRegistarH
            btnRegistarH.Location = new Point(10, 197);
            btnRegistarH.Name     = "btnRegistarH";
            btnRegistarH.Size     = new Size(110, 32);
            btnRegistarH.Text     = "⏱ Horas";
            btnRegistarH.UseVisualStyleBackColor = true;
            btnRegistarH.Click   += btnRegistarH_Click;
            // btnHistorico
            btnHistorico.Location = new Point(10, 240);
            btnHistorico.Name     = "btnHistorico";
            btnHistorico.Size     = new Size(110, 32);
            btnHistorico.Text     = "📋 Histórico";
            btnHistorico.UseVisualStyleBackColor = true;
            btnHistorico.Click   += btnHistorico_Click;
            // btnEliminar
            btnEliminar.Location = new Point(10, 283);
            btnEliminar.Name     = "btnEliminar";
            btnEliminar.Size     = new Size(110, 32);
            btnEliminar.Text     = "🗑 Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.ForeColor = System.Drawing.Color.DarkRed;
            btnEliminar.Click   += btnEliminar_Click;
            // btnExportarCsv
            btnExportarCsv.Location = new Point(10, 326);
            btnExportarCsv.Name     = "btnExportarCsv";
            btnExportarCsv.Size     = new Size(110, 32);
            btnExportarCsv.Text     = "📄 CSV";
            btnExportarCsv.UseVisualStyleBackColor = true;
            btnExportarCsv.Click   += btnExportarCsv_Click;
            // btnExportarPdf
            btnExportarPdf.Location = new Point(10, 369);
            btnExportarPdf.Name     = "btnExportarPdf";
            btnExportarPdf.Size     = new Size(110, 32);
            btnExportarPdf.Text     = "📕 PDF";
            btnExportarPdf.UseVisualStyleBackColor = true;
            btnExportarPdf.Click   += btnExportarPdf_Click;

            // lblInfo
            lblInfo.AutoSize  = false;
            lblInfo.Location  = new Point(12, 478);
            lblInfo.Name      = "lblInfo";
            lblInfo.Size      = new Size(640, 20);
            lblInfo.Text      = "";

            // FrmTarefas
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(808, 540);
            Controls.Add(lblInfo);
            Controls.Add(grpAcoes);
            Controls.Add(dgvTarefas);
            Controls.Add(grpFiltros);
            Name = "FrmTarefas";
            Text = "Gestão de Tarefas";
            grpFiltros.ResumeLayout(false);
            grpFiltros.PerformLayout();
            grpAcoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTarefas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox  grpFiltros;
        private Label     lblProjeto;
        private ComboBox  cmbProjecto;
        private Label     lblSprint;
        private ComboBox  cmbSprint;
        private Label     lblEstado;
        private ComboBox  cmbFiltroEstado;
        private Button    btnFiltrar;
        private DataGridView dgvTarefas;
        private GroupBox  grpAcoes;
        private Button    btnNovo;
        private Button    btnEditar;
        private Button    btnAlterarEst;
        private Button    btnAtribuir;
        private Button    btnRegistarH;
        private Button    btnHistorico;
        private Button    btnEliminar;
        private Button    btnExportarCsv;
        private Button    btnExportarPdf;
        private Label     lblInfo;
    }
}