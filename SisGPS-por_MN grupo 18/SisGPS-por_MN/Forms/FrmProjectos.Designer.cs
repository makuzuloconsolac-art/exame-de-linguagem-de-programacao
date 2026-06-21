namespace SisGPS_por_MN.Forms
{
    partial class FrmProjectos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btnNovo = new Button();
            btnEditar = new Button();
            btnEncerrar = new Button();
            btnCancelar = new Button();
            btnExportarCsv = new Button();
            btnExportarPdf = new Button();
            cmbFiltroEstado = new ComboBox();
            label1 = new Label();
            dgvProjectos = new DataGridView();
            lblProgresso = new Label();
            progressBar1 = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)dgvProjectos).BeginInit();
            SuspendLayout();
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(12, 43);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(100, 28);
            btnNovo.TabIndex = 0;
            btnNovo.Text = "➕ Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click_1;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(12, 83);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 28);
            btnEditar.TabIndex = 1;
            btnEditar.Text = "✏ Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click_1;
            // 
            // btnEncerrar
            // 
            btnEncerrar.Location = new Point(12, 123);
            btnEncerrar.Name = "btnEncerrar";
            btnEncerrar.Size = new Size(100, 28);
            btnEncerrar.TabIndex = 2;
            btnEncerrar.Text = "✔ Encerrar";
            btnEncerrar.UseVisualStyleBackColor = true;
            btnEncerrar.Click += btnEncerrar_Click_1;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(12, 163);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 28);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "✖ Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click_1;
            // 
            // btnExportarCsv
            // 
            btnExportarCsv.Location = new Point(12, 203);
            btnExportarCsv.Name = "btnExportarCsv";
            btnExportarCsv.Size = new Size(100, 28);
            btnExportarCsv.TabIndex = 9;
            btnExportarCsv.Text = "📄 Exportar CSV";
            btnExportarCsv.UseVisualStyleBackColor = true;
            btnExportarCsv.Click += btnExportarCsv_Click;
            // 
            // btnExportarPdf
            // 
            btnExportarPdf.Location = new Point(12, 243);
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(100, 28);
            btnExportarPdf.TabIndex = 10;
            btnExportarPdf.Text = "📕 Exportar PDF";
            btnExportarPdf.UseVisualStyleBackColor = true;
            btnExportarPdf.Click += btnExportarPdf_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(125, 15);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 5;
            label1.Text = "Filtrar por Estado:";
            // 
            // cmbFiltroEstado
            // 
            cmbFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroEstado.FormattingEnabled = true;
            cmbFiltroEstado.Location = new Point(125, 38);
            cmbFiltroEstado.Name = "cmbFiltroEstado";
            cmbFiltroEstado.Size = new Size(150, 23);
            cmbFiltroEstado.TabIndex = 4;
            cmbFiltroEstado.SelectedIndexChanged += cmbFiltroEstado_SelectedIndexChanged;
            // 
            // lblProgresso
            // 
            lblProgresso.AutoSize = true;
            lblProgresso.Location = new Point(290, 15);
            lblProgresso.Name = "lblProgresso";
            lblProgresso.Size = new Size(59, 15);
            lblProgresso.TabIndex = 7;
            lblProgresso.Text = "Progresso:";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(290, 38);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(200, 23);
            progressBar1.TabIndex = 8;
            // 
            // dgvProjectos
            // 
            dgvProjectos.AllowUserToAddRows = false;
            dgvProjectos.AllowUserToDeleteRows = false;
            dgvProjectos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProjectos.Location = new Point(125, 75);
            dgvProjectos.MultiSelect = false;
            dgvProjectos.Name = "dgvProjectos";
            dgvProjectos.ReadOnly = true;
            dgvProjectos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProjectos.Size = new Size(590, 320);
            dgvProjectos.TabIndex = 6;
            // 
            // FrmProjectos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 415);
            Controls.Add(progressBar1);
            Controls.Add(lblProgresso);
            Controls.Add(dgvProjectos);
            Controls.Add(label1);
            Controls.Add(cmbFiltroEstado);
            Controls.Add(btnCancelar);
            Controls.Add(btnEncerrar);
            Controls.Add(btnEditar);
            Controls.Add(btnNovo);
            Controls.Add(btnExportarCsv);
            Controls.Add(btnExportarPdf);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FrmProjectos";
            Text = "Gestão de Projectos";
            Load += FrmProjectos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProjectos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnNovo;
        private Button btnEditar;
        private Button btnEncerrar;
        private Button btnCancelar;
        private Button btnExportarCsv;
        private Button btnExportarPdf;
        private ComboBox cmbFiltroEstado;
        private Label label1;
        private DataGridView dgvProjectos;
        private Label lblProgresso;
        private ProgressBar progressBar1;
    }
}