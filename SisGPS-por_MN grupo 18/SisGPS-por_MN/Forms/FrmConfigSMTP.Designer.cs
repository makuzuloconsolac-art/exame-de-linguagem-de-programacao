namespace SisGPS_por_MN.Forms
{
    partial class FrmConfigSMTP
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
            lblTitulo = new Label();
            lblServidor = new Label();
            txtServidor = new TextBox();
            lblPorta = new Label();
            txtPorta = new TextBox();
            lblUtilizador = new Label();
            txtUtilizador = new TextBox();
            lblSenha = new Label();
            txtSenha = new TextBox();
            chkSSL = new CheckBox();
            btnGravar = new Button();
            btnCancelar = new Button();
            SuspendLayout();

            // lblTitulo
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 20);
            lblTitulo.Size = new Size(330, 25);
            lblTitulo.Text = "Configuração do Servidor SMTP";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // lblServidor
            lblServidor.AutoSize = true;
            lblServidor.Location = new Point(25, 65);
            lblServidor.Text = "Servidor SMTP (ex: smtp.gmail.com):";

            // txtServidor
            txtServidor.Location = new Point(25, 83);
            txtServidor.Size = new Size(330, 23);
            txtServidor.TabIndex = 0;

            // lblPorta
            lblPorta.AutoSize = true;
            lblPorta.Location = new Point(25, 115);
            lblPorta.Text = "Porta SMTP (ex: 587):";

            // txtPorta
            txtPorta.Location = new Point(25, 133);
            txtPorta.Size = new Size(330, 23);
            txtPorta.TabIndex = 1;

            // lblUtilizador
            lblUtilizador.AutoSize = true;
            lblUtilizador.Location = new Point(25, 165);
            lblUtilizador.Text = "Utilizador (E-mail):";

            // txtUtilizador
            txtUtilizador.Location = new Point(25, 183);
            txtUtilizador.Size = new Size(330, 23);
            txtUtilizador.TabIndex = 2;

            // lblSenha
            lblSenha.AutoSize = true;
            lblSenha.Location = new Point(25, 215);
            lblSenha.Text = "Palavra-passe do E-mail:";

            // txtSenha
            txtSenha.Location = new Point(25, 233);
            txtSenha.PasswordChar = '*';
            txtSenha.Size = new Size(330, 23);
            txtSenha.TabIndex = 3;

            // chkSSL
            chkSSL.AutoSize = true;
            chkSSL.Location = new Point(25, 270);
            chkSSL.Size = new Size(111, 19);
            chkSSL.Text = "Activar SSL/TLS";
            chkSSL.UseVisualStyleBackColor = true;
            chkSSL.TabIndex = 4;

            // btnGravar
            btnGravar.Location = new Point(25, 310);
            btnGravar.Size = new Size(150, 32);
            btnGravar.Text = "Gravar";
            btnGravar.UseVisualStyleBackColor = true;
            btnGravar.Click += btnGravar_Click;

            // btnCancelar
            btnCancelar.Location = new Point(205, 310);
            btnCancelar.Size = new Size(150, 32);
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;

            // FrmConfigSMTP
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 365);
            Controls.Add(lblTitulo);
            Controls.Add(lblServidor);
            Controls.Add(txtServidor);
            Controls.Add(lblPorta);
            Controls.Add(txtPorta);
            Controls.Add(lblUtilizador);
            Controls.Add(txtUtilizador);
            Controls.Add(lblSenha);
            Controls.Add(txtSenha);
            Controls.Add(chkSSL);
            Controls.Add(btnGravar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Configurar SMTP — SisGPS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblServidor;
        private TextBox txtServidor;
        private Label lblPorta;
        private TextBox txtPorta;
        private Label lblUtilizador;
        private TextBox txtUtilizador;
        private Label lblSenha;
        private TextBox txtSenha;
        private CheckBox chkSSL;
        private Button btnGravar;
        private Button btnCancelar;
    }
}
