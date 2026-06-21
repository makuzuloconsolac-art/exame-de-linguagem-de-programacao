namespace SisGPS_por_MN.Forms
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnEntrar = new Button();
            btnCancelar = new Button();
            lblErro = new Label();
            lblInfo = new Label();
            SuspendLayout();

            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(40, 20);
            lblTitulo.Size = new Size(320, 35);
            lblTitulo.Text = "SisGPS — Iniciar Sessão";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(40, 75);
            lblUsername.Text = "Utilizador:";

            txtUsername.Location = new Point(40, 95);
            txtUsername.Size = new Size(320, 23);
            txtUsername.TabIndex = 0;

            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(40, 130);
            lblPassword.Text = "Palavra-passe:";

            txtPassword.Location = new Point(40, 150);
            txtPassword.Size = new Size(320, 23);
            txtPassword.PasswordChar = '*';
            txtPassword.TabIndex = 1;
            txtPassword.KeyDown += txtPassword_KeyDown;

            btnCancelar.Location = new Point(210, 210);
            btnCancelar.Size = new Size(150, 32);
            btnCancelar.Text = "Cancelar";
            btnCancelar.TabIndex = 3;
            btnCancelar.Click += btnCancelar_Click;

            // lnkRegistar
            lnkRegistar = new LinkLabel();
            lnkRegistar.AutoSize = true;
            lnkRegistar.Location = new Point(40, 182);
            lnkRegistar.Size = new Size(68, 15);
            lnkRegistar.Text = "Criar Conta";
            lnkRegistar.LinkClicked += lnkRegistar_LinkClicked;

            // lnkRecuperar
            lnkRecuperar = new LinkLabel();
            lnkRecuperar.AutoSize = true;
            lnkRecuperar.Location = new Point(245, 182);
            lnkRecuperar.Size = new Size(115, 15);
            lnkRecuperar.Text = "Esqueci-me da senha";
            lnkRecuperar.LinkClicked += lnkRecuperar_LinkClicked;

            btnEntrar.Location = new Point(40, 210);
            btnEntrar.Size = new Size(150, 32);
            btnEntrar.Text = "Entrar";
            btnEntrar.TabIndex = 2;
            btnEntrar.Click += btnEntrar_Click;

            lblErro.ForeColor = Color.DarkRed;
            lblErro.Location = new Point(40, 250);
            lblErro.Size = new Size(320, 30);
            lblErro.Text = string.Empty;

            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(40, 290);
            lblInfo.Size = new Size(320, 45);
            lblInfo.Text = "Admin: admin / admin123\r\nFuncionário: bruno / bruno123";
            lblInfo.TextAlign = ContentAlignment.TopCenter;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 350);
            Controls.Add(lblTitulo);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lnkRegistar);
            Controls.Add(lnkRecuperar);
            Controls.Add(btnEntrar);
            Controls.Add(btnCancelar);
            Controls.Add(lblErro);
            Controls.Add(lblInfo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login — SisGPS";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private LinkLabel lnkRegistar;
        private LinkLabel lnkRecuperar;
        private Button btnEntrar;
        private Button btnCancelar;
        private Label lblErro;
        private Label lblInfo;
    }
}
