namespace SisGPS_por_MN.Forms
{
    partial class FrmCadTarefa
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
            txtTitulo    = new TextBox();
            lblDescricao = new Label();
            txtDescricao = new TextBox();
            lblSprint    = new Label();
            cmbSprint    = new ComboBox();
            lblMembro    = new Label();
            cmbMembro    = new ComboBox();
            lblPrioridade = new Label();
            cmbPrioridade = new ComboBox();
            lblEstado    = new Label();
            cmbEstado    = new ComboBox();
            lblHoras     = new Label();
            numHorasEst  = new NumericUpDown();
            lblPrazo     = new Label();
            chkPrazo     = new CheckBox();
            dtpPrazo     = new DateTimePicker();
            btnSalvar    = new Button();
            btnCancelar  = new Button();
            ((System.ComponentModel.ISupportInitialize)numHorasEst).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(15, 20);
            lblTitulo.Text = "Título *:";
            // txtTitulo
            txtTitulo.Location = new Point(115, 17);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(350, 23);
            txtTitulo.TabIndex = 0;

            // lblDescricao
            lblDescricao.AutoSize = true;
            lblDescricao.Location = new Point(15, 60);
            lblDescricao.Text = "Descrição:";
            // txtDescricao
            txtDescricao.Location = new Point(115, 57);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(350, 60);
            txtDescricao.TabIndex = 1;

            // lblSprint
            lblSprint.AutoSize = true;
            lblSprint.Location = new Point(15, 138);
            lblSprint.Text = "Sprint *:";
            // cmbSprint
            cmbSprint.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSprint.FormattingEnabled = true;
            cmbSprint.Location = new Point(115, 134);
            cmbSprint.Name = "cmbSprint";
            cmbSprint.Size = new Size(200, 23);
            cmbSprint.TabIndex = 2;

            // lblMembro
            lblMembro.AutoSize = true;
            lblMembro.Location = new Point(15, 175);
            lblMembro.Text = "Membro:";
            // cmbMembro
            cmbMembro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMembro.FormattingEnabled = true;
            cmbMembro.Location = new Point(115, 171);
            cmbMembro.Name = "cmbMembro";
            cmbMembro.Size = new Size(200, 23);
            cmbMembro.TabIndex = 3;

            // lblPrioridade
            lblPrioridade.AutoSize = true;
            lblPrioridade.Location = new Point(15, 212);
            lblPrioridade.Text = "Prioridade:";
            // cmbPrioridade
            cmbPrioridade.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrioridade.FormattingEnabled = true;
            cmbPrioridade.Location = new Point(115, 208);
            cmbPrioridade.Name = "cmbPrioridade";
            cmbPrioridade.Size = new Size(150, 23);
            cmbPrioridade.TabIndex = 4;

            // lblEstado
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(15, 250);
            lblEstado.Text = "Estado:";
            // cmbEstado
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(115, 246);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(150, 23);
            cmbEstado.TabIndex = 5;

            // lblHoras
            lblHoras.AutoSize = true;
            lblHoras.Location = new Point(15, 288);
            lblHoras.Text = "Horas Est.:";
            // numHorasEst
            numHorasEst.DecimalPlaces = 1;
            numHorasEst.Increment = new decimal(new int[] { 5, 0, 0, 65536 }); // 0.5
            numHorasEst.Location = new Point(115, 284);
            numHorasEst.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            numHorasEst.Name = "numHorasEst";
            numHorasEst.Size = new Size(100, 23);
            numHorasEst.TabIndex = 6;

            // lblPrazo
            lblPrazo.AutoSize = true;
            lblPrazo.Location = new Point(15, 325);
            lblPrazo.Text = "Data Prazo:";
            // chkPrazo
            chkPrazo.AutoSize = true;
            chkPrazo.Location = new Point(115, 325);
            chkPrazo.Name = "chkPrazo";
            chkPrazo.Text = "Definir prazo";
            chkPrazo.TabIndex = 7;
            chkPrazo.CheckedChanged += chkPrazo_CheckedChanged;
            // dtpPrazo
            dtpPrazo.Format = DateTimePickerFormat.Short;
            dtpPrazo.Location = new Point(220, 321);
            dtpPrazo.Name = "dtpPrazo";
            dtpPrazo.Size = new Size(130, 23);
            dtpPrazo.TabIndex = 8;
            dtpPrazo.Enabled = false;

            // btnSalvar
            btnSalvar.Location = new Point(115, 368);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(120, 30);
            btnSalvar.TabIndex = 9;
            btnSalvar.Text = "💾 Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // btnCancelar
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(255, 368);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 30);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;

            // FrmCadTarefa
            AcceptButton = btnSalvar;
            CancelButton = btnCancelar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 415);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Controls.Add(lblTitulo);
            Controls.Add(txtTitulo);
            Controls.Add(lblDescricao);
            Controls.Add(txtDescricao);
            Controls.Add(lblSprint);
            Controls.Add(cmbSprint);
            Controls.Add(lblMembro);
            Controls.Add(cmbMembro);
            Controls.Add(lblPrioridade);
            Controls.Add(cmbPrioridade);
            Controls.Add(lblEstado);
            Controls.Add(cmbEstado);
            Controls.Add(lblHoras);
            Controls.Add(numHorasEst);
            Controls.Add(lblPrazo);
            Controls.Add(chkPrazo);
            Controls.Add(dtpPrazo);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            Name = "FrmCadTarefa";
            Text = "Cadastro de Tarefa";
            ((System.ComponentModel.ISupportInitialize)numHorasEst).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private TextBox txtTitulo;
        private Label lblDescricao;
        private TextBox txtDescricao;
        private Label lblSprint;
        private ComboBox cmbSprint;
        private Label lblMembro;
        private ComboBox cmbMembro;
        private Label lblPrioridade;
        private ComboBox cmbPrioridade;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private Label lblHoras;
        private NumericUpDown numHorasEst;
        private Label lblPrazo;
        private CheckBox chkPrazo;
        private DateTimePicker dtpPrazo;
        private Button btnSalvar;
        private Button btnCancelar;
    }
}