namespace SisGPS_por_MN.Forms
{
    partial class FrmCadProjecto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnSalvar = new Button();
            btnCancelarx = new Button();
            txtNome = new TextBox();
            txtDescricao = new TextBox();
            txtCliente = new TextBox();
            txtOrcamento = new TextBox();
            cmbEquipa = new ComboBox();
            cmbEstado = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            dtpInicio = new DateTimePicker();
            dtpFim = new DateTimePicker();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 23);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 0;
            label1.Text = "Nome:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(254, 20);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 1;
            label2.Text = "Equipa";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 65);
            label3.Name = "label3";
            label3.Size = new Size(61, 15);
            label3.TabIndex = 2;
            label3.Text = "Descrição:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(255, 95);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 3;
            label4.Text = "Estado";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 111);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 4;
            label5.Text = "Cliente";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(11, 155);
            label6.Name = "label6";
            label6.Size = new Size(67, 15);
            label6.TabIndex = 5;
            label6.Text = "Orcamento";
            // 
            // btnSalvar
            // 
            btnSalvar.DialogResult = DialogResult.OK;
            btnSalvar.Location = new Point(37, 236);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(75, 23);
            btnSalvar.TabIndex = 7;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelarx
            // 
            btnCancelarx.DialogResult = DialogResult.Cancel;
            btnCancelarx.Location = new Point(149, 236);
            btnCancelarx.Name = "btnCancelarx";
            btnCancelarx.Size = new Size(75, 23);
            btnCancelarx.TabIndex = 8;
            btnCancelarx.Text = "Cancelar";
            btnCancelarx.UseVisualStyleBackColor = true;
            btnCancelarx.Click += btnCancelar_Click;
            // 
            // txtNome
            // 
            txtNome.Location = new Point(74, 20);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(150, 23);
            txtNome.TabIndex = 9;
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(80, 65);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(154, 23);
            txtDescricao.TabIndex = 10;
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(80, 108);
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(154, 23);
            txtCliente.TabIndex = 11;
            // 
            // txtOrcamento
            // 
            txtOrcamento.Location = new Point(84, 147);
            txtOrcamento.Name = "txtOrcamento";
            txtOrcamento.Size = new Size(150, 23);
            txtOrcamento.TabIndex = 12;
            txtOrcamento.TextChanged += textBox4_TextChanged;
            // 
            // cmbEquipa
            // 
            cmbEquipa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipa.FormattingEnabled = true;
            cmbEquipa.Location = new Point(254, 47);
            cmbEquipa.Name = "cmbEquipa";
            cmbEquipa.Size = new Size(121, 23);
            cmbEquipa.TabIndex = 13;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(263, 122);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(121, 23);
            cmbEstado.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(429, 23);
            label7.Name = "label7";
            label7.Size = new Size(79, 15);
            label7.TabIndex = 15;
            label7.Text = "Data de Inicio";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(429, 95);
            label8.Name = "label8";
            label8.Size = new Size(70, 15);
            label8.TabIndex = 16;
            label8.Text = "Data de Fim";
            // 
            // dtpInicio
            // 
            dtpInicio.Format = DateTimePickerFormat.Short;
            dtpInicio.Location = new Point(429, 43);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(122, 23);
            dtpInicio.TabIndex = 17;
            // 
            // dtpFim
            // 
            dtpFim.Format = DateTimePickerFormat.Short;
            dtpFim.Location = new Point(429, 119);
            dtpFim.Name = "dtpFim";
            dtpFim.Size = new Size(112, 23);
            dtpFim.TabIndex = 18;
            // 
            // FrmCadProjecto
            // 
            AcceptButton = btnSalvar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelarx;
            ClientSize = new Size(600, 306);
            Controls.Add(dtpFim);
            Controls.Add(dtpInicio);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(cmbEstado);
            Controls.Add(cmbEquipa);
            Controls.Add(txtOrcamento);
            Controls.Add(txtCliente);
            Controls.Add(txtDescricao);
            Controls.Add(txtNome);
            Controls.Add(btnCancelarx);
            Controls.Add(btnSalvar);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FrmCadProjecto";
            Text = "FrmCadProjecto";
            Load += FrmCadProjecto_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnSalvar;
        private Button btnCancelarx;
        private TextBox txtNome;
        private TextBox txtDescricao;
        private TextBox txtCliente;
        private TextBox txtOrcamento;
        private ComboBox cmbEquipa;
        private ComboBox cmbEstado;
        private Label label7;
        private Label label8;
        private DateTimePicker dtpInicio;
        private DateTimePicker dtpFim;
    }
}