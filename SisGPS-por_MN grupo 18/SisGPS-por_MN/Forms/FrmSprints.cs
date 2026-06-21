using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Exceptions;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Servicos;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmSprints : Form
    {
        private readonly ProjectoRepository _projRepo = new();
        private readonly SprintRepository _sprintRepo = new();
        private readonly GestorProjectos _gestor = new();

        public FrmSprints()
        {
            InitializeComponent();
            CarregarProjectos();
        }

        private void FrmSprints_Load(object sender, EventArgs e) { }

        private void CarregarProjectos()
        {
            cmbProjecto.DataSource = _projRepo.ObterActivos();
            cmbProjecto.DisplayMember = "Nome";
            cmbProjecto.ValueMember = "Id";
            CarregarSprints();
        }

        private void CarregarSprints()
        {
            if (cmbProjecto.SelectedValue is not int pid) return;
            dgvSprints.DataSource = null;
            dgvSprints.DataSource = _sprintRepo.ObterPorProjecto(pid);
        }

        private void cmbProjecto_SelectedIndexChanged(object sender, EventArgs e) =>
            CarregarSprints();

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (cmbProjecto.SelectedValue is not int pid) return;

            string nome = txtNome.Text.Trim();
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Insira o nome do sprint.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpFim.Value < dtpInicio.Value)
            {
                MessageBox.Show("A data de fim não pode ser anterior à de início.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var sprint = new Sprint
            {
                Nome = nome,
                ProjectoId = pid,
                DataInicio = dtpInicio.Value,
                DataFim = dtpFim.Value
            };

            try
            {
                _gestor.ProjectoId = pid;
                _gestor.CriarSprint(sprint);
                txtNome.Clear();
                CarregarSprints();
                MessageBox.Show("Sprint criado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ProjectoEncerradoException ex)
            {
                MessageBox.Show(ex.Message, "Projecto Encerrado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarMembro_Click(object sender, EventArgs e)
        {
            if (dgvSprints.CurrentRow?.DataBoundItem is not Sprint s) return;

            string novoNome = Microsoft.VisualBasic.Interaction.InputBox(
                "Nome do sprint:", "Editar Sprint", s.Nome);
            if (string.IsNullOrWhiteSpace(novoNome)) return;

            s.Nome = novoNome.Trim();
            s.DataInicio = dtpInicio.Value;
            s.DataFim = dtpFim.Value;
            _sprintRepo.Actualizar(s);
            CarregarSprints();
        }

        private void btnEncerrar_Click(object sender, EventArgs e)
        {
            if (dgvSprints.CurrentRow?.DataBoundItem is not Sprint s) return;
            if (MessageBox.Show($"Encerrar o sprint '{s.Nome}'?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                int pendentes = _gestor.EncerrarSprint(s.Id);
                lblTarefasPendentes.Text = pendentes > 0
                    ? $"Sprint encerrado. {pendentes} tarefa(s) pendente(s)."
                    : "✔ Sprint encerrado com sucesso — todas as tarefas concluídas.";
                CarregarSprints();
            }
            catch (SprintFechadoException ex)
            {
                MessageBox.Show(ex.Message, "Sprint Fechado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private DataTable GerarTabelaExportacao()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Objectivo");
            dt.Columns.Add("Data Inicio");
            dt.Columns.Add("Data Fim");
            dt.Columns.Add("Encerrado");

            var fonte = dgvSprints.DataSource as System.Collections.Generic.List<Sprint>;
            if (fonte == null && dgvSprints.DataSource is IEnumerable<Sprint> enumSp)
                fonte = enumSp.ToList();

            if (fonte != null)
            {
                foreach (var s in fonte)
                {
                    dt.Rows.Add(
                        s.Id,
                        s.Nome,
                        s.Objectivo ?? "",
                        s.DataInicio.ToString("dd/MM/yyyy"),
                        s.DataFim.ToString("dd/MM/yyyy"),
                        s.Encerrado ? "Sim" : "Não"
                    );
                }
            }
            return dt;
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            if (dgvSprints.Rows.Count == 0)
            {
                MessageBox.Show("Não há sprints listados para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "CSV Files|*.csv", FileName = "sprints.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exp = new ServicoExportacao();
                        exp.ExportarCsv(GerarTabelaExportacao(), sfd.FileName);
                        MessageBox.Show("Sprints exportados em CSV.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dgvSprints.Rows.Count == 0)
            {
                MessageBox.Show("Não há sprints listados para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "PDF Files|*.pdf", FileName = "sprints.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exp = new ServicoExportacao();
                        exp.ExportarPdf(GerarTabelaExportacao(), "Relatório de Sprints SisGPS", sfd.FileName);
                        MessageBox.Show("Sprints exportados em PDF.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
