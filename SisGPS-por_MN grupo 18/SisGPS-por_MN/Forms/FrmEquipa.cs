using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Servicos;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmEquipa : Form
    {
        private readonly EquipaRepository _eqRepo = new();
        private readonly MembroRepository _mbRepo = new();

        public FrmEquipa()
        {
            InitializeComponent();
            CarregarFiltros();
            CarregarEquipas();
        }

        private void FrmEquipa_Load(object sender, EventArgs e) { }

        private void CarregarFiltros()
        {
            cmbPapelFiltro.Items.Clear();
            cmbPapelFiltro.Items.Add("(Todos)");
            foreach (PapelMembro p in Enum.GetValues(typeof(PapelMembro)))
                cmbPapelFiltro.Items.Add(p.ToString());
            cmbPapelFiltro.SelectedIndex = 0;
        }

        private void CarregarEquipas()
        {
            dgvEquipas.DataSource = null;
            dgvEquipas.DataSource = _eqRepo.ObterTodos();
        }

        private void CarregarMembros(int equipaId)
        {
            var todos = _mbRepo.ObterPorEquipa(equipaId);

            if (cmbPapelFiltro.SelectedIndex > 0)
            {
                var papel = (PapelMembro)(cmbPapelFiltro.SelectedIndex - 1);
                var filtrado = new System.Collections.Generic.List<Membro>();
                foreach (var m in todos)
                    if (m.Papel == papel) filtrado.Add(m);
                dgvMembros.DataSource = filtrado;
            }
            else
            {
                dgvMembros.DataSource = null;
                dgvMembros.DataSource = todos;
            }

            int total = dgvMembros.Rows.Count;
            if (total > 0)
            {
                int disp = 0;
                foreach (var m in _mbRepo.ObterPorEquipa(equipaId))
                    if (m.Disponivel) disp++;
                int pct = (int)Math.Round((double)disp / total * 100);
                progressBar1.Value = Math.Min(pct, 100);
                lblProgresso.Text = $"Disponíveis: {disp}/{total}";
            }
            else
            {
                progressBar1.Value = 0;
                lblProgresso.Text = "Sem membros";
            }
        }

        private void dgvProjectos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEquipas.CurrentRow?.DataBoundItem is Equipa eq)
                CarregarMembros(eq.Id);
        }

        private void cmbPapelFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvEquipas.CurrentRow?.DataBoundItem is Equipa eq)
                CarregarMembros(eq.Id);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            string nome = Microsoft.VisualBasic.Interaction.InputBox(
                "Nome da nova equipa:", "Nova Equipa");
            if (string.IsNullOrWhiteSpace(nome)) return;
            _eqRepo.Inserir(new Equipa { Nome = nome.Trim() });
            CarregarEquipas();
        }

        private void btnEditarEquipa_Click(object sender, EventArgs e)
        {
            if (dgvEquipas.CurrentRow?.DataBoundItem is not Equipa eq) return;
            string novoNome = Microsoft.VisualBasic.Interaction.InputBox(
                "Novo nome da equipa:", "Editar Equipa", eq.Nome);
            if (string.IsNullOrWhiteSpace(novoNome)) return;
            eq.Nome = novoNome.Trim();
            _eqRepo.Actualizar(eq);
            CarregarEquipas();
        }

        private void btnNovoMembro_Click(object sender, EventArgs e)
        {
            if (dgvEquipas.CurrentRow?.DataBoundItem is not Equipa eq)
            {
                MessageBox.Show("Seleccione uma equipa primeiro.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nome = Microsoft.VisualBasic.Interaction.InputBox(
                "Nome do novo membro:", "Novo Membro");
            if (string.IsNullOrWhiteSpace(nome)) return;

            string email = Microsoft.VisualBasic.Interaction.InputBox(
                "E-mail (opcional):", "Novo Membro");

            string papelStr = Microsoft.VisualBasic.Interaction.InputBox(
                "Papel (0=Developer, 1=QA, 2=ProjectManager, 3=Designer, 4=DevOps):",
                "Novo Membro", "0");
            if (!int.TryParse(papelStr, out int papelInt) || papelInt < 0 || papelInt > 4)
                papelInt = 0;

            var membro = new Membro
            {
                Nome = nome.Trim(),
                Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim(),
                Papel = (PapelMembro)papelInt,
                EquipaId = eq.Id,
                Disponivel = true
            };

            _mbRepo.Inserir(membro);
            CarregarMembros(eq.Id);
            MessageBox.Show($"Membro '{membro.Nome}' adicionado com sucesso!", "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditarMembro_Click(object sender, EventArgs e)
        {
            if (dgvMembros.CurrentRow?.DataBoundItem is not Membro mb) return;
            if (dgvEquipas.CurrentRow?.DataBoundItem is not Equipa eq) return;

            string novoNome = Microsoft.VisualBasic.Interaction.InputBox(
                "Nome do membro:", "Editar Membro", mb.Nome);
            if (string.IsNullOrWhiteSpace(novoNome)) return;

            mb.Nome = novoNome.Trim();
            _mbRepo.Actualizar(mb);
            CarregarMembros(eq.Id);
        }

        private void btnToggleDisponivel_Click(object sender, EventArgs e)
        {
            if (dgvMembros.CurrentRow?.DataBoundItem is not Membro mb) return;
            if (dgvEquipas.CurrentRow?.DataBoundItem is not Equipa eq) return;

            bool novoEstado = !mb.Disponivel;
            _mbRepo.AlterarDisponibilidade(mb.Id, novoEstado);
            CarregarMembros(eq.Id);

            string msg = novoEstado ? "marcado como Disponível" : "marcado como Indisponível";
            MessageBox.Show($"'{mb.Nome}' {msg}.", "Disponibilidade",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click(object sender, EventArgs e) => Close();

        private DataTable GerarTabelaExportacao()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Email");
            dt.Columns.Add("Papel");
            dt.Columns.Add("Disponibilidade");

            var fonte = dgvMembros.DataSource as System.Collections.Generic.List<Membro>;
            if (fonte == null && dgvMembros.DataSource is IEnumerable<Membro> enumM)
                fonte = enumM.ToList();

            if (fonte != null)
            {
                foreach (var m in fonte)
                {
                    dt.Rows.Add(
                        m.Id,
                        m.Nome,
                        m.Email ?? "",
                        m.ObterPapelTexto(),
                        m.Disponivel ? "Disponível" : "Indisponível"
                    );
                }
            }
            return dt;
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            if (dgvMembros.Rows.Count == 0)
            {
                MessageBox.Show("Não há membros listados para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "CSV Files|*.csv", FileName = "membros.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exp = new ServicoExportacao();
                        exp.ExportarCsv(GerarTabelaExportacao(), sfd.FileName);
                        MessageBox.Show("Membros exportados em CSV.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao exportar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            if (dgvMembros.Rows.Count == 0)
            {
                MessageBox.Show("Não há membros listados para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "PDF Files|*.pdf", FileName = "membros.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exp = new ServicoExportacao();
                        string titulo = "Relatório de Membros da Equipa";
                        if (dgvEquipas.CurrentRow?.DataBoundItem is Equipa eq)
                            titulo += " " + eq.Nome;

                        exp.ExportarPdf(GerarTabelaExportacao(), titulo, sfd.FileName);
                        MessageBox.Show("Membros exportados em PDF.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao exportar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
