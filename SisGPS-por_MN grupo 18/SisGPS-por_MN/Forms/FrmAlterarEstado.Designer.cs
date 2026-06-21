namespace SisGPS_por_MN.Forms
{
    partial class FrmAlterarEstado
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
            lblTarefaInfo   = new Label();
            lblEstadoActual = new Label();
            lblValorActual  = new Label();
            lblNovoEstado   = new Label();
            cmbNovoEstado   = new ComboBox();
            lblObservacao   = new Label();
            txtObservacao   = new TextBox();
            btnConfirmar    = new Button();
            btnCancelar     = new Button();
            SuspendLayout();

            // lblTarefaInfo
            lblTarefaInfo.AutoSize  = false;
            lblTarefaInfo.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTarefaInfo.Location  = new Point(15, 15);
            lblTarefaInfo.Name      = "lblTarefaInfo";
            lblTarefaInfo.Size      = new Size(420, 30);
            lblTarefaInfo.Text      = "Tarefa: ...";

            // lblEstadoActual
            lblEstadoActual.AutoSize = true;
            lblEstadoActual.Location = new Point(15, 60);
            lblEstadoActual.Text     = "Estado actual:";

            // lblValorActual
            lblValorActual.AutoSize   = false;
            lblValorActual.Font       = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblValorActual.ForeColor  = System.Drawing.Color.DarkBlue;
            lblValorActual.Location   = new Point(130, 60);
            lblValorActual.Name       = "lblValorActual";
            lblValorActual.Size       = new Size(200, 20);
            lblValorActual.Text       = "—";

            // lblNovoEstado
            lblNovoEstado.AutoSize = true;
            lblNovoEstado.Location = new Point(15, 100);
            lblNovoEstado.Text     = "Novo estado *:";

            // cmbNovoEstado
            cmbNovoEstado.DropDownStyle    = ComboBoxStyle.DropDownList;
            cmbNovoEstado.FormattingEnabled = true;
            cmbNovoEstado.Location         = new Point(130, 96);
            cmbNovoEstado.Name             = "cmbNovoEstado";
            cmbNovoEstado.Size             = new Size(200, 23);
            cmbNovoEstado.TabIndex         = 0;

            // lblObservacao
            lblObservacao.AutoSize = true;
            lblObservacao.Location = new Point(15, 140);
            lblObservacao.Text     = "Observação:";

            // txtObservacao
            txtObservacao.Location  = new Point(15, 160);
            txtObservacao.Multiline = true;
            txtObservacao.Name      = "txtObservacao";
            txtObservacao.Size      = new Size(420, 70);
            txtObservacao.TabIndex  = 1;

            // btnConfirmar
            btnConfirmar.Location             = new Point(130, 250);
            btnConfirmar.Name                 = "btnConfirmar";
            btnConfirmar.Size                 = new Size(130, 32);
            btnConfirmar.TabIndex             = 2;
            btnConfirmar.Text                 = "✔ Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click               += btnConfirmar_Click;

            // btnCancelar
            btnCancelar.DialogResult          = DialogResult.Cancel;
            btnCancelar.Location              = new Point(275, 250);
            btnCancelar.Name                  = "btnCancelar";
            btnCancelar.Size                  = new Size(100, 32);
            btnCancelar.TabIndex              = 3;
            btnCancelar.Text                  = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click                += btnCancelar_Click;

            // FrmAlterarEstado
            AcceptButton          = btnConfirmar;
            CancelButton          = btnCancelar;
            AutoScaleDimensions   = new SizeF(7F, 15F);
            AutoScaleMode         = AutoScaleMode.Font;
            ClientSize            = new Size(455, 300);
            FormBorderStyle       = FormBorderStyle.FixedDialog;
            MaximizeBox           = false;
            Controls.Add(lblTarefaInfo);
            Controls.Add(lblEstadoActual);
            Controls.Add(lblValorActual);
            Controls.Add(lblNovoEstado);
            Controls.Add(cmbNovoEstado);
            Controls.Add(lblObservacao);
            Controls.Add(txtObservacao);
            Controls.Add(btnConfirmar);
            Controls.Add(btnCancelar);
            Name = "FrmAlterarEstado";
            Text = "Alterar Estado da Tarefa";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label       lblTarefaInfo;
        private Label       lblEstadoActual;
        private Label       lblValorActual;
        private Label       lblNovoEstado;
        private ComboBox    cmbNovoEstado;
        private Label       lblObservacao;
        private TextBox     txtObservacao;
        private Button      btnConfirmar;
        private Button      btnCancelar;
    }
}