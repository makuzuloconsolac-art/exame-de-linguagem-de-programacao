namespace SisGPS_por_MN.Forms
{
    partial class FrmRegistoUtilizador
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
            lblNome = new Label();
            txtNome = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblPergunta = new Label();
            txtPergunta = new TextBox();
            lblResposta = new Label();
            txtResposta = new TextBox();
            lblMembro = new Label();
            cmbMembro = new ComboBox();
            lblNivel = new Label();
            cmbNivelAcesso = new ComboBox();
            btnGravar = new Button();
            btnCancelar = new Button();
            SuspendLayout();

            // lblTitulo
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 20);
            lblTitulo.Size = new Size(330, 25);
            lblTitulo.Text = "Registo de Utilizador";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // lblNome
            lblNome.AutoSize = true;
            lblNome.Location = new Point(25, 60);
            lblNome.Text = "Nome Completo:";

            // txtNome
            txtNome.Location = new Point(25, 78);
            txtNome.Size = new Size(330, 23);
            txtNome.TabIndex = 0;

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(25, 110);
            lblUsername.Text = "Nome de Utilizador:";

            // txtUsername
            txtUsername.Location = new Point(25, 128);
            txtUsername.Size = new Size(330, 23);
            txtUsername.TabIndex = 1;

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(25, 160);
            lblPassword.Text = "Palavra-passe:";

            // txtPassword
            txtPassword.Location = new Point(25, 178);
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(330, 23);
            txtPassword.TabIndex = 2;

            // lblPergunta
            lblPergunta.AutoSize = true;
            lblPergunta.Location = new Point(25, 210);
            lblPergunta.Text = "Pergunta de Segurança (ex: Nome do primeiro cão?):";

            // txtPergunta
            txtPergunta.Location = new Point(25, 228);
            txtPergunta.Size = new Size(330, 23);
            txtPergunta.TabIndex = 3;

            // lblResposta
            lblResposta.AutoSize = true;
            lblResposta.Location = new Point(25, 260);
            lblResposta.Text = "Resposta de Segurança:";

            // txtResposta
            txtResposta.Location = new Point(25, 278);
            txtResposta.Size = new Size(330, 23);
            txtResposta.TabIndex = 4;

            // lblMembro
            lblMembro.AutoSize = true;
            lblMembro.Location = new Point(25, 310);
            lblMembro.Text = "Membro da Equipa (Opcional):";

            // cmbMembro
            cmbMembro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMembro.FormattingEnabled = true;
            cmbMembro.Location = new Point(25, 328);
            cmbMembro.Size = new Size(330, 23);
            cmbMembro.TabIndex = 5;

            // lblNivel
            lblNivel.AutoSize = true;
            lblNivel.Location = new Point(25, 360);
            lblNivel.Text = "Nível de Acesso:";

            // cmbNivelAcesso
            cmbNivelAcesso.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNivelAcesso.FormattingEnabled = true;
            cmbNivelAcesso.Location = new Point(25, 378);
            cmbNivelAcesso.Size = new Size(330, 23);
            cmbNivelAcesso.TabIndex = 6;

            // btnGravar
            btnGravar.Location = new Point(25, 425);
            btnGravar.Size = new Size(150, 32);
            btnGravar.Text = "Gravar";
            btnGravar.UseVisualStyleBackColor = true;
            btnGravar.Click += btnGravar_Click;

            // btnCancelar
            btnCancelar.Location = new Point(205, 425);
            btnCancelar.Size = new Size(150, 32);
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;

            // FrmRegistoUtilizador
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 480);
            Controls.Add(lblTitulo);
            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPergunta);
            Controls.Add(txtPergunta);
            Controls.Add(lblResposta);
            Controls.Add(txtResposta);
            Controls.Add(lblMembro);
            Controls.Add(cmbMembro);
            Controls.Add(lblNivel);
            Controls.Add(cmbNivelAcesso);
            Controls.Add(btnGravar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registo — SisGPS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblPergunta;
        private TextBox txtPergunta;
        private Label lblResposta;
        private TextBox txtResposta;
        private Label lblMembro;
        private ComboBox cmbMembro;
        private Label lblNivel;
        private ComboBox cmbNivelAcesso;
        private Button btnGravar;
        private Button btnCancelar;
    }
}
