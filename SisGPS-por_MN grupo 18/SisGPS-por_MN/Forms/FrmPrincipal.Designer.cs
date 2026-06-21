namespace SisGPS_por_MN.Forms
{
    partial class FrmPrincipal
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
            menuStrip1 = new MenuStrip();
            menuProjectos = new ToolStripMenuItem();
            menuEquipas = new ToolStripMenuItem();
            menuSprints = new ToolStripMenuItem();
            menuTarefas = new ToolStripMenuItem();
            menuKanban = new ToolStripMenuItem();
            menuRelatorios = new ToolStripMenuItem();
            menuEmails = new ToolStripMenuItem();
            menuUtilizadores = new ToolStripMenuItem();
            menuSair = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblUtilizador = new ToolStripStatusLabel();
            pnlDashboard = new Panel();
            lblDashTitulo = new Label();
            
            // 9 KPI Cards
            pnlProjTotais = new Panel();
            lblProjTotaisVal = new Label();
            lblProjTotaisTitle = new Label();
            
            pnlProjActivos = new Panel();
            lblProjActivosVal = new Label();
            lblProjActivosTitle = new Label();
            
            pnlProjConcluidos = new Panel();
            lblProjConcluidosVal = new Label();
            lblProjConcluidosTitle = new Label();
            
            pnlEquipas = new Panel();
            lblEquipasVal = new Label();
            lblEquipasTitle = new Label();
            
            pnlMembros = new Panel();
            lblMembrosVal = new Label();
            lblMembrosTitle = new Label();
            
            pnlSprintsAbertos = new Panel();
            lblSprintsAbertosVal = new Label();
            lblSprintsAbertosTitle = new Label();
            
            pnlTarefasPend = new Panel();
            lblTarefasPendVal = new Label();
            lblTarefasPendTitle = new Label();
            
            pnlTarefasProg = new Panel();
            lblTarefasProgVal = new Label();
            lblTarefasProgTitle = new Label();
            
            pnlTarefasConcl = new Panel();
            lblTarefasConclVal = new Label();
            lblTarefasConclTitle = new Label();

            // Shortcuts GroupBox
            grpAtalhos = new GroupBox();
            btnAtalhoProjectos = new Button();
            btnAtalhoEquipas = new Button();
            btnAtalhoSprints = new Button();
            btnAtalhoTarefas = new Button();
            btnAtalhoKanban = new Button();
            btnAtalhoRelatorios = new Button();
            btnAtalhoEmails = new Button();
            btnAtalhoUtilizadores = new Button();
            btnAtalhoConfigSMTP = new Button();
            btnAtalhoSair = new Button();

            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            pnlDashboard.SuspendLayout();
            
            pnlProjTotais.SuspendLayout();
            pnlProjActivos.SuspendLayout();
            pnlProjConcluidos.SuspendLayout();
            pnlEquipas.SuspendLayout();
            pnlMembros.SuspendLayout();
            pnlSprintsAbertos.SuspendLayout();
            pnlTarefasPend.SuspendLayout();
            pnlTarefasProg.SuspendLayout();
            pnlTarefasConcl.SuspendLayout();
            grpAtalhos.SuspendLayout();
            SuspendLayout();

            // menuStrip1
            menuStrip1.Items.AddRange(new ToolStripItem[]
            {
                menuProjectos, menuEquipas, menuSprints, menuTarefas,
                menuKanban, menuRelatorios, menuEmails, menuUtilizadores, menuSair
            });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1008, 24);
            menuStrip1.TabIndex = 0;

            // Items do Menu
            menuProjectos.Text = "Projectos";
            menuProjectos.Click += menuProjectos_Click;
            menuEquipas.Text = "Equipas";
            menuEquipas.Click += menuEquipas_Click;
            menuSprints.Text = "Sprints";
            menuSprints.Click += menuSprints_Click;
            menuTarefas.Text = "Tarefas";
            menuTarefas.Click += menuTarefas_Click;
            menuKanban.Text = "Kanban";
            menuKanban.Click += menuKanban_Click;
            menuRelatorios.Text = "Relatórios";
            menuRelatorios.Click += menuRelatorios_Click;
            menuEmails.Text = "E-mails";
            menuEmails.Click += menuEmails_Click;
            menuUtilizadores.Text = "Utilizadores";
            menuUtilizadores.Click += menuUtilizadores_Click;
            menuSair.Text = "Terminar Sessão";
            menuSair.Click += menuSair_Click;

            // statusStrip1
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblUtilizador });
            statusStrip1.Location = new Point(0, 707);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1008, 22);
            statusStrip1.TabIndex = 1;

            // lblUtilizador
            lblUtilizador.Name = "lblUtilizador";
            lblUtilizador.Size = new Size(130, 17);
            lblUtilizador.Text = "Utilizador: Administrador";

            // pnlDashboard
            pnlDashboard.BackColor = Color.WhiteSmoke;
            pnlDashboard.Controls.Add(lblDashTitulo);
            pnlDashboard.Controls.Add(pnlProjTotais);
            pnlDashboard.Controls.Add(pnlProjActivos);
            pnlDashboard.Controls.Add(pnlProjConcluidos);
            pnlDashboard.Controls.Add(pnlEquipas);
            pnlDashboard.Controls.Add(pnlMembros);
            pnlDashboard.Controls.Add(pnlSprintsAbertos);
            pnlDashboard.Controls.Add(pnlTarefasPend);
            pnlDashboard.Controls.Add(pnlTarefasProg);
            pnlDashboard.Controls.Add(pnlTarefasConcl);
            pnlDashboard.Controls.Add(grpAtalhos);
            pnlDashboard.Location = new Point(0, 24);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Size = new Size(1008, 680);
            pnlDashboard.TabIndex = 2;

            // lblDashTitulo
            lblDashTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblDashTitulo.ForeColor = Color.DarkSlateBlue;
            lblDashTitulo.Location = new Point(20, 15);
            lblDashTitulo.Name = "lblDashTitulo";
            lblDashTitulo.Size = new Size(960, 30);
            lblDashTitulo.Text = "Dashboard do Sistema";

            // Função local para configurar cards KPI
            void ConfigurarCard(Panel pnl, Label val, Label tit, string tText, int x, int y)
            {
                pnl.BackColor = Color.White;
                pnl.BorderStyle = BorderStyle.FixedSingle;
                pnl.Controls.Add(val);
                pnl.Controls.Add(tit);
                pnl.Location = new Point(x, y);
                pnl.Size = new Size(180, 80);

                val.AutoSize = true;
                val.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
                val.ForeColor = Color.SteelBlue;
                val.Location = new Point(15, 10);
                val.Text = "0";

                tit.AutoSize = true;
                tit.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
                tit.ForeColor = Color.DimGray;
                tit.Location = new Point(15, 50);
                tit.Text = tText;
            }

            ConfigurarCard(pnlProjTotais, lblProjTotaisVal, lblProjTotaisTitle, "Projectos Totais", 20, 60);
            ConfigurarCard(pnlProjActivos, lblProjActivosVal, lblProjActivosTitle, "Projectos Activos", 210, 60);
            ConfigurarCard(pnlProjConcluidos, lblProjConcluidosVal, lblProjConcluidosTitle, "Projectos Concluídos", 400, 60);

            ConfigurarCard(pnlEquipas, lblEquipasVal, lblEquipasTitle, "Total de Equipas", 20, 150);
            ConfigurarCard(pnlMembros, lblMembrosVal, lblMembrosTitle, "Total de Membros", 210, 150);
            ConfigurarCard(pnlSprintsAbertos, lblSprintsAbertosVal, lblSprintsAbertosTitle, "Sprints Abertos", 400, 150);

            ConfigurarCard(pnlTarefasPend, lblTarefasPendVal, lblTarefasPendTitle, "Tarefas Pendentes", 20, 240);
            ConfigurarCard(pnlTarefasProg, lblTarefasProgVal, lblTarefasProgTitle, "Tarefas em Progresso", 210, 240);
            ConfigurarCard(pnlTarefasConcl, lblTarefasConclVal, lblTarefasConclTitle, "Tarefas Concluídas", 400, 240);

            // grpAtalhos
            grpAtalhos.Controls.Add(btnAtalhoProjectos);
            grpAtalhos.Controls.Add(btnAtalhoEquipas);
            grpAtalhos.Controls.Add(btnAtalhoSprints);
            grpAtalhos.Controls.Add(btnAtalhoTarefas);
            grpAtalhos.Controls.Add(btnAtalhoKanban);
            grpAtalhos.Controls.Add(btnAtalhoRelatorios);
            grpAtalhos.Controls.Add(btnAtalhoEmails);
            grpAtalhos.Controls.Add(btnAtalhoUtilizadores);
            grpAtalhos.Controls.Add(btnAtalhoConfigSMTP);
            grpAtalhos.Controls.Add(btnAtalhoSair);
            grpAtalhos.Location = new Point(600, 60);
            grpAtalhos.Name = "grpAtalhos";
            grpAtalhos.Size = new Size(380, 400);
            grpAtalhos.Text = "Atalhos Rápidos";

            // Helper local para botões
            void ConfigurarBotao(Button btn, string txt, int x, int y, EventHandler clickHandler)
            {
                btn.Location = new Point(x, y);
                btn.Size = new Size(160, 45);
                btn.Text = txt;
                btn.UseVisualStyleBackColor = true;
                btn.Click += clickHandler;
            }

            ConfigurarBotao(btnAtalhoProjectos, "Projectos", 15, 30, menuProjectos_Click);
            ConfigurarBotao(btnAtalhoEquipas, "Equipas", 195, 30, menuEquipas_Click);
            ConfigurarBotao(btnAtalhoSprints, "Sprints", 15, 90, menuSprints_Click);
            ConfigurarBotao(btnAtalhoTarefas, "Tarefas", 195, 90, menuTarefas_Click);
            ConfigurarBotao(btnAtalhoKanban, "Kanban", 15, 150, menuKanban_Click);
            ConfigurarBotao(btnAtalhoRelatorios, "Relatórios", 195, 150, menuRelatorios_Click);
            ConfigurarBotao(btnAtalhoEmails, "E-mails", 15, 210, menuEmails_Click);
            ConfigurarBotao(btnAtalhoUtilizadores, "Utilizadores", 195, 210, menuUtilizadores_Click);
            ConfigurarBotao(btnAtalhoConfigSMTP, "Configuração SMTP", 15, 270, btnAtalhoConfigSMTP_Click);
            ConfigurarBotao(btnAtalhoSair, "Terminar Sessão", 195, 270, menuSair_Click);

            // FrmPrincipal
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 729);
            Controls.Add(pnlDashboard);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SisGPS — Gestão de Projectos de Software";
            WindowState = FormWindowState.Maximized;
            Load += FrmPrincipal_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            pnlDashboard.ResumeLayout(false);
            pnlProjTotais.ResumeLayout(false);
            pnlProjTotais.PerformLayout();
            pnlProjActivos.ResumeLayout(false);
            pnlProjActivos.PerformLayout();
            pnlProjConcluidos.ResumeLayout(false);
            pnlProjConcluidos.PerformLayout();
            pnlEquipas.ResumeLayout(false);
            pnlEquipas.PerformLayout();
            pnlMembros.ResumeLayout(false);
            pnlMembros.PerformLayout();
            pnlSprintsAbertos.ResumeLayout(false);
            pnlSprintsAbertos.PerformLayout();
            pnlTarefasPend.ResumeLayout(false);
            pnlTarefasPend.PerformLayout();
            pnlTarefasProg.ResumeLayout(false);
            pnlTarefasProg.PerformLayout();
            pnlTarefasConcl.ResumeLayout(false);
            pnlTarefasConcl.PerformLayout();
            grpAtalhos.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuProjectos;
        private ToolStripMenuItem menuEquipas;
        private ToolStripMenuItem menuSprints;
        private ToolStripMenuItem menuTarefas;
        private ToolStripMenuItem menuKanban;
        private ToolStripMenuItem menuRelatorios;
        private ToolStripMenuItem menuEmails;
        private ToolStripMenuItem menuUtilizadores;
        private ToolStripMenuItem menuSair;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblUtilizador;
        
        private Panel pnlDashboard;
        private Label lblDashTitulo;
        
        // 9 KPI panels & labels
        private Panel pnlProjTotais;
        private Label lblProjTotaisVal;
        private Label lblProjTotaisTitle;

        private Panel pnlProjActivos;
        private Label lblProjActivosVal;
        private Label lblProjActivosTitle;

        private Panel pnlProjConcluidos;
        private Label lblProjConcluidosVal;
        private Label lblProjConcluidosTitle;

        private Panel pnlEquipas;
        private Label lblEquipasVal;
        private Label lblEquipasTitle;

        private Panel pnlMembros;
        private Label lblMembrosVal;
        private Label lblMembrosTitle;

        private Panel pnlSprintsAbertos;
        private Label lblSprintsAbertosVal;
        private Label lblSprintsAbertosTitle;

        private Panel pnlTarefasPend;
        private Label lblTarefasPendVal;
        private Label lblTarefasPendTitle;

        private Panel pnlTarefasProg;
        private Label lblTarefasProgVal;
        private Label lblTarefasProgTitle;

        private Panel pnlTarefasConcl;
        private Label lblTarefasConclVal;
        private Label lblTarefasConclTitle;

        private GroupBox grpAtalhos;
        private Button btnAtalhoProjectos;
        private Button btnAtalhoEquipas;
        private Button btnAtalhoSprints;
        private Button btnAtalhoTarefas;
        private Button btnAtalhoKanban;
        private Button btnAtalhoRelatorios;
        private Button btnAtalhoEmails;
        private Button btnAtalhoUtilizadores;
        private Button btnAtalhoConfigSMTP;
        private Button btnAtalhoSair;
    }
}
