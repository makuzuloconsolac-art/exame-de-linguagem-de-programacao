namespace SisGPS_por_MN.Forms
{
    partial class FrmRecuperarSenha
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
            lblUsername = new Label();
            txtUsername = new TextBox();
            btnBuscar = new Button();
            lblPerguntaLabel = new Label();
            lblPergunta = new Label();
            lblResposta = new Label();
            txtResposta = new TextBox();
            lblNovaSenha = new Label();
            txtNovaSenha = new TextBox();
            btnRecuperar = new Button();
            btnCancelar = new Button();
            SuspendLayout();

            // lblTitulo
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 20);
            lblTitulo.Size = new Size(330, 25);
            lblTitulo.Text = "Recuperar Palavra-passe";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(25, 60);
            lblUsername.Text = "Nome de Utilizador:";

            // txtUsername
            txtUsername.Location = new Point(25, 78);
            txtUsername.Size = new Size(220, 23);
            txtUsername.TabIndex = 0;

            // btnBuscar
            btnBuscar.Location = new Point(255, 76);
            btnBuscar.Size = new Size(100, 26);
            btnBuscar.Text = "Procurar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;

            // lblPerguntaLabel
            lblPerguntaLabel.AutoSize = true;
            lblPerguntaLabel.Location = new Point(25, 120);
            lblPerguntaLabel.Text = "Pergunta de Segurança:";

            // lblPergunta
            lblPergunta.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblPergunta.ForeColor = Color.DarkSlateBlue;
            lblPergunta.Location = new Point(25, 140);
            lblPergunta.Size = new Size(330, 35);
            lblPergunta.Text = "(Introduza o utilizador e procure)";

            // lblResposta
            lblResposta.AutoSize = true;
            lblResposta.Location = new Point(25, 190);
            lblResposta.Text = "Resposta de Segurança:";

            // txtResposta
            txtResposta.Enabled = false;
            txtResposta.Location = new Point(25, 208);
            txtResposta.Size = new Size(330, 23);
            txtResposta.TabIndex = 1;

            // lblNovaSenha
            lblNovaSenha.AutoSize = true;
            lblNovaSenha.Location = new Point(25, 245);
            lblNovaSenha.Text = "Nova Palavra-passe:";

            // txtNovaSenha
            txtNovaSenha.Enabled = false;
            txtNovaSenha.Location = new Point(25, 263);
            txtNovaSenha.PasswordChar = '*';
            txtNovaSenha.Size = new Size(330, 23);
            txtNovaSenha.TabIndex = 2;

            // btnRecuperar
            btnRecuperar.Enabled = false;
            btnRecuperar.Location = new Point(25, 315);
            btnRecuperar.Size = new Size(150, 32);
            btnRecuperar.Text = "Confirmar";
            btnRecuperar.UseVisualStyleBackColor = true;
            btnRecuperar.Click += btnRecuperar_Click;

            // btnCancelar
            btnCancelar.Location = new Point(205, 315);
            btnCancelar.Size = new Size(150, 32);
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;

            // FrmRecuperarSenha
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 375);
            Controls.Add(lblTitulo);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(btnBuscar);
            Controls.Add(lblPerguntaLabel);
            Controls.Add(lblPergunta);
            Controls.Add(lblResposta);
            Controls.Add(txtResposta);
            Controls.Add(lblNovaSenha);
            Controls.Add(txtNovaSenha);
            Controls.Add(btnRecuperar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Recuperar Senha — SisGPS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblUsername;
        private TextBox txtUsername;
        private Button btnBuscar;
        private Label lblPerguntaLabel;
        private Label lblPergunta;
        private Label lblResposta;
        private TextBox txtResposta;
        private Label lblNovaSenha;
        private TextBox txtNovaSenha;
        private Button btnRecuperar;
        private Button btnCancelar;
    }
}
