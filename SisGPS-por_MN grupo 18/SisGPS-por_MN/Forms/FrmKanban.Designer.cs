namespace SisGPS_por_MN.Forms
{
    partial class FrmKanban
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
            grpFiltros   = new GroupBox();
            lblProjeto   = new Label();
            cmbProjecto  = new ComboBox();
            lblSprint    = new Label();
            cmbSprint    = new ComboBox();
            btnAtualizar = new Button();
            lblTotal     = new Label();

            // 4 colunas Kanban
            pnlBacklog     = new Panel();
            lblBacklog     = new Label();
            lstBacklog     = new ListBox();
            pnlProgresso   = new Panel();
            lblProgresso   = new Label();
            lstProgresso   = new ListBox();
            pnlConcluida   = new Panel();
            lblConcluida   = new Label();
            lstConcluida   = new ListBox();
            pnlBloqueada   = new Panel();
            lblBloqueada   = new Label();
            lstBloqueada   = new ListBox();

            grpFiltros.SuspendLayout();
            SuspendLayout();

            // ── grpFiltros ───────────────────────────────────────────────
            grpFiltros.Controls.Add(lblProjeto);
            grpFiltros.Controls.Add(cmbProjecto);
            grpFiltros.Controls.Add(lblSprint);
            grpFiltros.Controls.Add(cmbSprint);
            grpFiltros.Controls.Add(btnAtualizar);
            grpFiltros.Controls.Add(lblTotal);
            grpFiltros.Location = new Point(10, 10);
            grpFiltros.Name     = "grpFiltros";
            grpFiltros.Size     = new Size(960, 65);
            grpFiltros.Text     = "Filtros do Board";
            // lblProjeto
            lblProjeto.AutoSize = true;
            lblProjeto.Location = new Point(10, 30);
            lblProjeto.Text     = "Projecto:";
            // cmbProjecto
            cmbProjecto.DropDownStyle    = ComboBoxStyle.DropDownList;
            cmbProjecto.FormattingEnabled = true;
            cmbProjecto.Location         = new Point(75, 26);
            cmbProjecto.Name             = "cmbProjecto";
            cmbProjecto.Size             = new Size(200, 23);
            cmbProjecto.TabIndex         = 0;
            cmbProjecto.SelectedIndexChanged += cmbProjecto_SelectedIndexChanged;
            // lblSprint
            lblSprint.AutoSize = true;
            lblSprint.Location = new Point(292, 30);
            lblSprint.Text     = "Sprint:";
            // cmbSprint
            cmbSprint.DropDownStyle    = ComboBoxStyle.DropDownList;
            cmbSprint.FormattingEnabled = true;
            cmbSprint.Location         = new Point(340, 26);
            cmbSprint.Name             = "cmbSprint";
            cmbSprint.Size             = new Size(200, 23);
            cmbSprint.TabIndex         = 1;
            cmbSprint.SelectedIndexChanged += cmbSprint_SelectedIndexChanged;
            // btnAtualizar
            btnAtualizar.Location = new Point(555, 24);
            btnAtualizar.Name     = "btnAtualizar";
            btnAtualizar.Size     = new Size(100, 28);
            btnAtualizar.Text     = "🔄 Atualizar";
            btnAtualizar.UseVisualStyleBackColor = true;
            btnAtualizar.Click   += btnAtualizar_Click;
            // lblTotal
            lblTotal.AutoSize   = true;
            lblTotal.Location   = new Point(670, 30);
            lblTotal.Name       = "lblTotal";
            lblTotal.Text       = "";

            // ── helper local para criar coluna ───────────────────────────
            void CriarColuna(Panel pnl, Label lbl, ListBox lst, string titulo,
                             System.Drawing.Color cor, int x)
            {
                // Label cabeçalho
                lbl.AutoSize  = false;
                lbl.BackColor = cor;
                lbl.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
                lbl.Location  = new Point(0, 0);
                lbl.Name      = lbl.Name;
                lbl.Size      = new Size(225, 32);
                lbl.Text      = titulo;
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // ListBox
                lst.BorderStyle    = BorderStyle.None;
                lst.Location       = new Point(0, 32);
                lst.Name           = lst.Name;
                lst.Size           = new Size(225, 400);
                lst.TabIndex       = 0;
                lst.DoubleClick   += lst_DoubleClick;
                // Panel
                pnl.BorderStyle = BorderStyle.FixedSingle;
                pnl.Controls.Add(lbl);
                pnl.Controls.Add(lst);
                pnl.Location    = new Point(x, 90);
                pnl.Name        = pnl.Name;
                pnl.Size        = new Size(225, 432);
            }

            // Nomes para os controlos
            pnlBacklog.Name   = "pnlBacklog";   lblBacklog.Name   = "lblBacklog";   lstBacklog.Name   = "lstBacklog";
            pnlProgresso.Name = "pnlProgresso"; lblProgresso.Name = "lblProgresso"; lstProgresso.Name = "lstProgresso";
            pnlConcluida.Name = "pnlConcluida"; lblConcluida.Name = "lblConcluida"; lstConcluida.Name = "lstConcluida";
            pnlBloqueada.Name = "pnlBloqueada"; lblBloqueada.Name = "lblBloqueada"; lstBloqueada.Name = "lstBloqueada";

            CriarColuna(pnlBacklog,   lblBacklog,   lstBacklog,   "📥 BACKLOG",       System.Drawing.Color.LightGray,   10);
            CriarColuna(pnlProgresso, lblProgresso, lstProgresso, "⚙ EM PROGRESSO",   System.Drawing.Color.LightBlue,  245);
            CriarColuna(pnlConcluida, lblConcluida, lstConcluida, "✔ CONCLUÍDA",       System.Drawing.Color.LightGreen, 480);
            CriarColuna(pnlBloqueada, lblBloqueada, lstBloqueada, "🚫 BLOQUEADA",      System.Drawing.Color.LightCoral, 715);

            // FrmKanban
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(980, 540);
            Controls.Add(pnlBloqueada);
            Controls.Add(pnlConcluida);
            Controls.Add(pnlProgresso);
            Controls.Add(pnlBacklog);
            Controls.Add(grpFiltros);
            Name = "FrmKanban";
            Text = "Board Kanban";
            grpFiltros.ResumeLayout(false);
            grpFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpFiltros;
        private Label    lblProjeto;
        private ComboBox cmbProjecto;
        private Label    lblSprint;
        private ComboBox cmbSprint;
        private Button   btnAtualizar;
        private Label    lblTotal;
        private Panel    pnlBacklog;
        private Label    lblBacklog;
        private ListBox  lstBacklog;
        private Panel    pnlProgresso;
        private Label    lblProgresso;
        private ListBox  lstProgresso;
        private Panel    pnlConcluida;
        private Label    lblConcluida;
        private ListBox  lstConcluida;
        private Panel    pnlBloqueada;
        private Label    lblBloqueada;
        private ListBox  lstBloqueada;
    }
}