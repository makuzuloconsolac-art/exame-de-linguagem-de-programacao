namespace SisGPS_por_MN.Forms
{
    partial class FrmEmails
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpNovo = new GroupBox();
            lblDest = new Label();
            txtDestinatario = new TextBox();
            lblAss = new Label();
            txtAssunto = new TextBox();
            lblCorpo = new Label();
            txtCorpo = new TextBox();
            btnNovo = new Button();
            grpLista = new GroupBox();
            txtBusca = new TextBox();
            btnBuscar = new Button();
            btnEnviar = new Button();
            btnEliminar = new Button();
            dgvEmails = new DataGridView();
            grpNovo.SuspendLayout();
            grpLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmails).BeginInit();
            SuspendLayout();

            grpNovo.Location = new Point(10, 10);
            grpNovo.Size = new Size(760, 160);
            grpNovo.Text = "Novo E-mail";
            grpNovo.Controls.Add(lblDest);
            grpNovo.Controls.Add(txtDestinatario);
            grpNovo.Controls.Add(lblAss);
            grpNovo.Controls.Add(txtAssunto);
            grpNovo.Controls.Add(lblCorpo);
            grpNovo.Controls.Add(txtCorpo);
            grpNovo.Controls.Add(btnNovo);

            lblDest.AutoSize = true;
            lblDest.Location = new Point(10, 28);
            lblDest.Text = "Destinatário:";

            txtDestinatario.Location = new Point(90, 24);
            txtDestinatario.Size = new Size(300, 23);

            lblAss.AutoSize = true;
            lblAss.Location = new Point(410, 28);
            lblAss.Text = "Assunto:";

            txtAssunto.Location = new Point(470, 24);
            txtAssunto.Size = new Size(270, 23);

            lblCorpo.AutoSize = true;
            lblCorpo.Location = new Point(10, 58);
            lblCorpo.Text = "Corpo:";

            txtCorpo.Location = new Point(90, 55);
            txtCorpo.Multiline = true;
            txtCorpo.Size = new Size(650, 60);

            btnNovo.Location = new Point(640, 120);
            btnNovo.Size = new Size(100, 28);
            btnNovo.Text = "Guardar";
            btnNovo.Click += btnNovo_Click;

            grpLista.Location = new Point(10, 180);
            grpLista.Size = new Size(760, 320);
            grpLista.Text = "Histórico de E-mails";

            txtBusca.Location = new Point(10, 25);
            txtBusca.Size = new Size(250, 23);

            btnBuscar.Location = new Point(270, 23);
            btnBuscar.Size = new Size(80, 26);
            btnBuscar.Text = "Buscar";
            btnBuscar.Click += btnBuscar_Click;

            btnEnviar.Location = new Point(360, 23);
            btnEnviar.Size = new Size(100, 26);
            btnEnviar.Text = "Enviar";
            btnEnviar.Click += btnEnviar_Click;

            btnEliminar.Location = new Point(470, 23);
            btnEliminar.Size = new Size(90, 26);
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;

            dgvEmails.Location = new Point(10, 55);
            dgvEmails.Size = new Size(740, 255);
            dgvEmails.ReadOnly = true;
            dgvEmails.AllowUserToAddRows = false;
            dgvEmails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmails.SelectionChanged += dgvEmails_SelectionChanged;

            grpLista.Controls.Add(txtBusca);
            grpLista.Controls.Add(btnBuscar);
            grpLista.Controls.Add(btnEnviar);
            grpLista.Controls.Add(btnEliminar);
            grpLista.Controls.Add(dgvEmails);

            ClientSize = new Size(784, 512);
            Controls.Add(grpNovo);
            Controls.Add(grpLista);
            Text = "Gestão de E-mails";
            grpNovo.ResumeLayout(false);
            grpNovo.PerformLayout();
            grpLista.ResumeLayout(false);
            grpLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmails).EndInit();
            ResumeLayout(false);
        }

        private GroupBox grpNovo;
        private Label lblDest;
        private TextBox txtDestinatario;
        private Label lblAss;
        private TextBox txtAssunto;
        private Label lblCorpo;
        private TextBox txtCorpo;
        private Button btnNovo;
        private GroupBox grpLista;
        private TextBox txtBusca;
        private Button btnBuscar;
        private Button btnEnviar;
        private Button btnEliminar;
        private DataGridView dgvEmails;
    }
}
