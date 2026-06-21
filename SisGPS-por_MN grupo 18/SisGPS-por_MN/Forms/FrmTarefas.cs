using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Servicos;
using SisGPS_por_MN.Utils;
using System.Data;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmTarefas : Form
    {
        private readonly ProjectoRepository _projRepo = new();
        private readonly SprintRepository _spRepo = new();
        private readonly TarefaRepository _tarefaRepo = new();
        private readonly MembroRepository _mbRepo = new();
        private readonly GestorProjectos _gestor = new();
        private List<Tarefa> _tarefas = new();

        public FrmTarefas()
        {
            InitializeComponent();
            AplicarPermissoes();
            CarregarFiltros();
        }

        private void AplicarPermissoes()
        {
            if (Sessao.EhAdmin) return;
            btnNovo.Visible = false;
            btnEliminar.Visible = false;
            btnAtribuir.Visible = false;
        }

        private void CarregarFiltros()
        {
            var projs = _projRepo.ObterTodos().ToList();
            var listProj = new List<object> { new { Id = 0, Nome = "(Todos)" } };
            listProj.AddRange(projs.Cast<object>());
            cmbProjecto.DataSource = listProj;
            cmbProjecto.DisplayMember = "Nome";
            cmbProjecto.ValueMember = "Id";

            cmbFiltroEstado.Items.Clear();
            cmbFiltroEstado.Items.Add("(Todos)");
            foreach (EstadoTarefa e in Enum.GetValues(typeof(EstadoTarefa)))
                cmbFiltroEstado.Items.Add(Formatador.FormatarEstado(e));
            cmbFiltroEstado.SelectedIndex = 0;
        }

        private void cmbProjecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSprint.DataSource = null;
            if (cmbProjecto.SelectedValue is int pid && pid > 0)
            {
                var sprints = _spRepo.ObterPorProjecto(pid).ToList();
                var listSp = new List<object> { new { Id = 0, Nome = "(Todos)" } };
                listSp.AddRange(sprints.Cast<object>());
                cmbSprint.DataSource = listSp;
                cmbSprint.DisplayMember = "Nome";
                cmbSprint.ValueMember = "Id";
            }
            else
            {
                cmbSprint.Items.Clear();
                cmbSprint.Items.Add(new { Id = 0, Nome = "(Todos)" });
                cmbSprint.DisplayMember = "Nome";
                cmbSprint.ValueMember = "Id";
                cmbSprint.SelectedIndex = 0;
            }
            CarregarTarefas();
        }

        private void cmbSprint_SelectedIndexChanged(object sender, EventArgs e) => CarregarTarefas();
        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e) => CarregarTarefas();
        private void btnFiltrar_Click(object sender, EventArgs e) => CarregarTarefas();

        private void CarregarTarefas()
        {
            _tarefas.Clear();

            if (cmbSprint.SelectedValue is int sid && sid > 0)
                _tarefas = _tarefaRepo.ObterPorSprint(sid).ToList();
            else if (cmbProjecto.SelectedValue is int pid && pid > 0)
            {
                foreach (var sp in _spRepo.ObterPorProjecto(pid))
                    _tarefas.AddRange(_tarefaRepo.ObterPorSprint(sp.Id));
            }
            else if (!Sessao.EhAdmin && Sessao.UtilizadorActual?.MembroId is int mid)
                _tarefas = _tarefaRepo.ObterPorMembro(mid).ToList();
            else
                _tarefas = _tarefaRepo.ObterTodos().ToList();

            if (!Sessao.EhAdmin && Sessao.UtilizadorActual?.MembroId is int membroId)
                _tarefas = _tarefas.Where(t => t.MembroId == membroId).ToList();

            if (cmbFiltroEstado.SelectedIndex > 0)
            {
                var estadoSel = (EstadoTarefa)(cmbFiltroEstado.SelectedIndex - 1);
                _tarefas = _tarefas.Where(t => t.Estado == estadoSel).ToList();
            }

            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            dgvTarefas.DataSource = null;
            dgvTarefas.DataSource = _tarefas;
            lblInfo.Text = $"{_tarefas.Count} tarefa(s) encontrada(s)";
        }

        private void dgvTarefas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTarefas.Rows[e.RowIndex].DataBoundItem is Tarefa t)
                dgvTarefas.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    Formatador.CorPorPrioridade(t.Prioridade);
        }

        private Tarefa? TarefaSelecionada() =>
            dgvTarefas.CurrentRow?.DataBoundItem as Tarefa;

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using var frm = new FrmCadTarefa();
            if (frm.ShowDialog() == DialogResult.OK) CarregarTarefas();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var t = TarefaSelecionada();
            if (t == null) { MsgSelecioneTarefa(); return; }
            using var frm = new FrmCadTarefa(t);
            if (frm.ShowDialog() == DialogResult.OK) CarregarTarefas();
        }

        private void btnAlterarEst_Click(object sender, EventArgs e)
        {
            var t = TarefaSelecionada();
            if (t == null) { MsgSelecioneTarefa(); return; }
            using var frm = new FrmAlterarEstado(t);
            if (frm.ShowDialog() == DialogResult.OK) CarregarTarefas();
        }

        private void btnAtribuir_Click(object sender, EventArgs e)
        {
            var t = TarefaSelecionada();
            if (t == null) { MsgSelecioneTarefa(); return; }

            var membros = _mbRepo.ObterDisponiveis().ToList();
            if (!membros.Any())
            {
                MessageBox.Show("Não há membros disponíveis.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lista = string.Join("\n", membros.Select((m, i) => $"{i}: {m.Nome} [{m.ObterPapelTexto()}]"));
            string resposta = Microsoft.VisualBasic.Interaction.InputBox(
                $"Índice do membro a atribuir:\n\n{lista}", "Atribuir Membro", "0");

            if (int.TryParse(resposta, out int idx) && idx >= 0 && idx < membros.Count)
            {
                try
                {
                    var membro = membros[idx];
                    _gestor.AtribuirTarefa(t.Id, membro.Id);
                    
                    // Notificar por e-mail se houver email do membro configurado
                    if (!string.IsNullOrEmpty(membro.Email))
                    {
                        try
                        {
                            var servEmail = new ServicoEmail();
                            string assunto = $"Nova Tarefa Atribuída: {t.Titulo}";
                            string corpo = $"Olá {membro.Nome},\n\nFoi-lhe atribuída a tarefa '{t.Titulo}' no projecto SisGPS.\nDescrição: {t.Descricao ?? "Nenhuma"}\nPrioridade: {t.Prioridade}\nPrazo: {t.DataPrazo?.ToString("dd/MM/yyyy") ?? "Sem prazo"}\n\nBom trabalho!";
                            servEmail.EnviarENotificarTarefa(membro.Email, assunto, corpo, t.Id);
                        }
                        catch
                        {
                            // falhas de e-mail não interferem na atribuição
                        }
                    }

                    CarregarTarefas();
                    MessageBox.Show($"Tarefa atribuída a '{membro.Nome}'.", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRegistarH_Click(object sender, EventArgs e)
        {
            var t = TarefaSelecionada();
            if (t == null) { MsgSelecioneTarefa(); return; }

            string resposta = Microsoft.VisualBasic.Interaction.InputBox(
                $"Horas a registar na tarefa '{t.Titulo}'\n(já registadas: {t.HorasRegistadas}h):",
                "Registar Horas", "1");

            if (decimal.TryParse(resposta, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture, out decimal h) && h > 0)
            {
                try
                {
                    ValidadorNegocio.ValidarHoras(h);
                    _tarefaRepo.RegistarHoras(t.Id, h);
                    CarregarTarefas();
                    MessageBox.Show($"{h}h registadas com sucesso.", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            var t = TarefaSelecionada();
            if (t == null) { MsgSelecioneTarefa(); return; }
            using var frm = new FrmHistorico(t);
            frm.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var t = TarefaSelecionada();
            if (t == null) { MsgSelecioneTarefa(); return; }
            if (MessageBox.Show($"Eliminar a tarefa '{t.Titulo}'?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            _tarefaRepo.Eliminar(t.Id);
            CarregarTarefas();
        }

        private DataTable GerarTabelaExportacao()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Titulo");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Prioridade");
            dt.Columns.Add("Horas Estimadas");
            dt.Columns.Add("Horas Registadas");
            dt.Columns.Add("Prazo");

            foreach (var t in _tarefas)
            {
                dt.Rows.Add(
                    t.Id,
                    t.Titulo,
                    t.Estado.ToString(),
                    t.Prioridade.ToString(),
                    t.HorasEstimadas.ToString("F1"),
                    t.HorasRegistadas.ToString("F1"),
                    t.DataPrazo?.ToString("dd/MM/yyyy") ?? ""
                );
            }
            return dt;
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog { Filter = "CSV Files|*.csv", FileName = "tarefas.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exp = new ServicoExportacao();
                        exp.ExportarCsv(GerarTabelaExportacao(), sfd.FileName);
                        MessageBox.Show("Tarefas exportadas em CSV.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            using (var sfd = new SaveFileDialog { Filter = "PDF Files|*.pdf", FileName = "tarefas.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exp = new ServicoExportacao();
                        exp.ExportarPdf(GerarTabelaExportacao(), "Relatório de Tarefas SisGPS", sfd.FileName);
                        MessageBox.Show("Tarefas exportadas em PDF.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao exportar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private static void MsgSelecioneTarefa() =>
            MessageBox.Show("Seleccione uma tarefa na lista.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
