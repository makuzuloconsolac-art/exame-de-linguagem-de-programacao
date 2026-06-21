namespace SisGPS_por_MN.Forms
{
    partial class FrmHistorico
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
            lblTitulo    = new Label();
            dgvHistorico = new DataGridView();
            colData      = new DataGridViewTextBoxColumn();
            colAnterior  = new DataGridViewTextBoxColumn();
            colNovo      = new DataGridViewTextBoxColumn();
            colObs       = new DataGridViewTextBoxColumn();
            btnFechar    = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHistorico).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.AutoSize  = false;
            lblTitulo.Font      = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.Location  = new Point(12, 12);
            lblTitulo.Name      = "lblTitulo";
            lblTitulo.Size      = new Size(660, 28);
            lblTitulo.Text      = "Histórico da Tarefa";

            // dgvHistorico
            dgvHistorico.AllowUserToAddRows    = false;
            dgvHistorico.AllowUserToDeleteRows = false;
            dgvHistorico.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorico.Columns.AddRange(colData, colAnterior, colNovo, colObs);
            dgvHistorico.Location      = new Point(12, 50);
            dgvHistorico.MultiSelect   = false;
            dgvHistorico.Name          = "dgvHistorico";
            dgvHistorico.ReadOnly      = true;
            dgvHistorico.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorico.Size          = new Size(660, 340);
            dgvHistorico.TabIndex      = 0;

            // colData
            colData.HeaderText = "Data / Hora";
            colData.Name       = "colData";
            colData.Width      = 150;
            // colAnterior
            colAnterior.HeaderText = "Estado Anterior";
            colAnterior.Name       = "colAnterior";
            colAnterior.Width      = 130;
            // colNovo
            colNovo.HeaderText = "Novo Estado";
            colNovo.Name       = "colNovo";
            colNovo.Width      = 130;
            // colObs
            colObs.HeaderText  = "Observação";
            colObs.Name        = "colObs";
            colObs.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // btnFechar
            btnFechar.DialogResult          = DialogResult.Cancel;
            btnFechar.Location              = new Point(587, 405);
            btnFechar.Name                  = "btnFechar";
            btnFechar.Size                  = new Size(85, 30);
            btnFechar.TabIndex              = 1;
            btnFechar.Text                  = "Fechar";
            btnFechar.UseVisualStyleBackColor = true;
            btnFechar.Click                += (s, e) => Close();

            // FrmHistorico
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(686, 447);
            FormBorderStyle     = FormBorderStyle.FixedDialog;
            MaximizeBox         = false;
            Controls.Add(lblTitulo);
            Controls.Add(dgvHistorico);
            Controls.Add(btnFechar);
            Name = "FrmHistorico";
            Text = "Histórico de Alterações";
            ((System.ComponentModel.ISupportInitialize)dgvHistorico).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label                       lblTitulo;
        private DataGridView                dgvHistorico;
        private DataGridViewTextBoxColumn   colData;
        private DataGridViewTextBoxColumn   colAnterior;
        private DataGridViewTextBoxColumn   colNovo;
        private DataGridViewTextBoxColumn   colObs;
        private Button                      btnFechar;
    }
}