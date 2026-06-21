using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmKanban : Form
    {
        private readonly ProjectoRepository _projRepo   = new();
        private readonly SprintRepository   _spRepo     = new();
        private readonly TarefaRepository   _tarefaRepo = new();

        private List<KanbanItem> _backlog   = new();
        private List<KanbanItem> _progresso = new();
        private List<KanbanItem> _concluida = new();
        private List<KanbanItem> _bloqueada = new();

        public FrmKanban()
        {
            InitializeComponent();
            CarregarProjectos();
        }

        private void CarregarProjectos()
        {
            var projs = _projRepo.ObterTodos().ToList();
            var lista = new List<object> { new { Id = 0, Nome = "(Seleccione...)" } };
            lista.AddRange(projs.Cast<object>());
            cmbProjecto.DataSource    = lista;
            cmbProjecto.DisplayMember = "Nome";
            cmbProjecto.ValueMember   = "Id";
        }

        private void cmbProjecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSprint.DataSource = null;
            cmbSprint.Items.Clear();

            if (cmbProjecto.SelectedValue is int pid && pid > 0)
            {
                var sprints = _spRepo.ObterPorProjecto(pid).ToList();
                var listaSp = new List<object> { new { Id = 0, Nome = "(Todos os Sprints)" } };
                listaSp.AddRange(sprints.Cast<object>());
                cmbSprint.DataSource    = listaSp;
                cmbSprint.DisplayMember = "Nome";
                cmbSprint.ValueMember   = "Id";
            }
            AtualizarBoard();
        }

        private void cmbSprint_SelectedIndexChanged(object sender, EventArgs e) =>
            AtualizarBoard();

        private void btnAtualizar_Click(object sender, EventArgs e) =>
            AtualizarBoard();

        private void AtualizarBoard()
        {
            List<KanbanItem> items;

            if (cmbSprint.SelectedValue is int sid && sid > 0)
                items = _tarefaRepo.ObterKanban(sprintId: sid).ToList();
            else if (cmbProjecto.SelectedValue is int pid && pid > 0)
                items = _tarefaRepo.ObterKanban(projectoId: pid).ToList();
            else
                items = new List<KanbanItem>();

            _backlog   = items.Where(t => t.Estado == EstadoTarefa.Backlog).ToList();
            _progresso = items.Where(t => t.Estado == EstadoTarefa.EmProgresso).ToList();
            _concluida = items.Where(t => t.Estado == EstadoTarefa.Concluida).ToList();
            _bloqueada = items.Where(t => t.Estado == EstadoTarefa.Bloqueada).ToList();

            PreencherListBox(lstBacklog,   _backlog);
            PreencherListBox(lstProgresso, _progresso);
            PreencherListBox(lstConcluida, _concluida);
            PreencherListBox(lstBloqueada, _bloqueada);

            lblTotal.Text = $"Total: {items.Count} tarefa(s) | ✔ {_concluida.Count} concluída(s)";
        }

        private static void PreencherListBox(ListBox lst, List<KanbanItem> tarefas)
        {
            lst.Items.Clear();
            foreach (var t in tarefas)
                lst.Items.Add($"[{t.Prioridade}] {t.Titulo}");
        }

        private void lst_DoubleClick(object? sender, EventArgs e)
        {
            if (sender is not ListBox lst) return;

            List<KanbanItem> fonte = lst.Name switch
            {
                "lstBacklog"   => _backlog,
                "lstProgresso" => _progresso,
                "lstConcluida" => _concluida,
                "lstBloqueada" => _bloqueada,
                _              => new List<KanbanItem>()
            };

            if (lst.SelectedIndex < 0 || lst.SelectedIndex >= fonte.Count) return;
            var item = fonte[lst.SelectedIndex];

            var tarefa = _tarefaRepo.ObterPorId(item.TarefaId);
            if (tarefa == null) return;

            using var frm = new FrmAlterarEstado(tarefa);
            if (frm.ShowDialog() == DialogResult.OK)
                AtualizarBoard();
        }
    }
}
