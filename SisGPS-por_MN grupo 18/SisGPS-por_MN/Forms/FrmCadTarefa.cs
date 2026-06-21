using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Servicos;
using SisGPS_por_MN.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmCadTarefa : Form
    {
        private readonly TarefaRepository _repo   = new();
        private readonly SprintRepository _spRepo  = new();
        private readonly MembroRepository _mbRepo  = new();
        private readonly ProjectoRepository _pjRepo = new();
        private Tarefa? _tarefa;

        // ── Novo ──────────────────────────────────────────────────────────
        public FrmCadTarefa()
        {
            InitializeComponent();
            CarregarCombos();
        }

        // ── Editar ────────────────────────────────────────────────────────
        public FrmCadTarefa(Tarefa t) : this()
        {
            _tarefa = t;
            Text = "Editar Tarefa";
            txtTitulo.Text    = t.Titulo;
            txtDescricao.Text = t.Descricao ?? "";
            numHorasEst.Value = (decimal)t.HorasEstimadas;

            // Seleccionar sprint
            foreach (var item in cmbSprint.Items)
                if (item is Sprint s && s.Id == t.SprintId)
                { cmbSprint.SelectedItem = s; break; }

            // Seleccionar membro
            cmbMembro.SelectedIndex = 0; // "(Nenhum)"
            if (t.MembroId.HasValue)
                foreach (var item in cmbMembro.Items)
                    if (item is Membro m && m.Id == t.MembroId.Value)
                    { cmbMembro.SelectedItem = m; break; }

            cmbPrioridade.SelectedIndex = (int)t.Prioridade;
            cmbEstado.SelectedIndex     = (int)t.Estado;

            if (t.DataPrazo.HasValue)
            {
                chkPrazo.Checked = true;
                dtpPrazo.Value   = t.DataPrazo.Value;
            }
        }

        private void CarregarCombos()
        {
            // Sprints (todos os abertos de projectos activos)
            var sprints = _spRepo.ObterTodos().ToList();
            cmbSprint.DataSource    = sprints;
            cmbSprint.DisplayMember = "Nome";
            cmbSprint.ValueMember   = "Id";

            // Membros — primeiro item: (Nenhum)
            var membros = _mbRepo.ObterDisponiveis().ToList();
            var lista   = new System.Collections.Generic.List<object> { new { Id = (int?)null, Nome = "(Nenhum)" } };
            lista.AddRange(membros.Cast<object>());
            cmbMembro.DataSource    = lista;
            cmbMembro.DisplayMember = "Nome";
            cmbMembro.ValueMember   = "Id";

            // Prioridade
            cmbPrioridade.DataSource = Enum.GetNames(typeof(Prioridade));
            cmbPrioridade.SelectedIndex = 1; // Média

            // Estado
            cmbEstado.DataSource = Enum.GetNames(typeof(EstadoTarefa));
            cmbEstado.SelectedIndex = 0; // Backlog
        }

        private void chkPrazo_CheckedChanged(object sender, EventArgs e) =>
            dtpPrazo.Enabled = chkPrazo.Checked;

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("O título é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Focus();
                return;
            }
            if (cmbSprint.SelectedItem is not Sprint sprint)
            {
                MessageBox.Show("Seleccione um sprint.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ValidadorNegocio.ValidarHoras((decimal)numHorasEst.Value);

                int? membroId = null;
                Membro? assignedMembro = null;
                if (cmbMembro.SelectedItem is Membro mb)
                {
                    membroId = mb.Id;
                    assignedMembro = mb;
                }

                if (_tarefa == null)
                {
                    _tarefa = new Tarefa
                    {
                        Titulo          = txtTitulo.Text.Trim(),
                        Descricao       = string.IsNullOrWhiteSpace(txtDescricao.Text) ? null : txtDescricao.Text.Trim(),
                        SprintId        = sprint.Id,
                        MembroId        = membroId,
                        Prioridade      = (Prioridade)cmbPrioridade.SelectedIndex,
                        Estado          = (EstadoTarefa)cmbEstado.SelectedIndex,
                        HorasEstimadas  = (decimal)numHorasEst.Value,
                        HorasRegistadas = 0m,
                        DataPrazo       = chkPrazo.Checked ? dtpPrazo.Value : null
                    };
                    _repo.Inserir(_tarefa);
                }
                else
                {
                    _tarefa.Titulo         = txtTitulo.Text.Trim();
                    _tarefa.Descricao      = string.IsNullOrWhiteSpace(txtDescricao.Text) ? null : txtDescricao.Text.Trim();
                    _tarefa.SprintId       = sprint.Id;
                    _tarefa.MembroId       = membroId;
                    _tarefa.Prioridade     = (Prioridade)cmbPrioridade.SelectedIndex;
                    _tarefa.Estado         = (EstadoTarefa)cmbEstado.SelectedIndex;
                    _tarefa.HorasEstimadas = (decimal)numHorasEst.Value;
                    _tarefa.DataPrazo      = chkPrazo.Checked ? dtpPrazo.Value : null;
                    _repo.Actualizar(_tarefa);
                }

                // Enviar notificação ao membro se estiver atribuído e possuir e-mail
                if (assignedMembro != null && !string.IsNullOrEmpty(assignedMembro.Email))
                {
                    try
                    {
                        var servEmail = new ServicoEmail();
                        string assunto = $"Tarefa Atribuída: {_tarefa.Titulo}";
                        string corpo = $"Olá {assignedMembro.Nome},\n\nFoi-lhe atribuída a tarefa '{_tarefa.Titulo}' no projecto SisGPS.\nEstado: {Formatador.FormatarEstado(_tarefa.Estado)}\nPrioridade: {_tarefa.Prioridade}\nPrazo: {_tarefa.DataPrazo?.ToString("dd/MM/yyyy") ?? "Sem prazo"}\n\nBom trabalho!";
                        servEmail.EnviarENotificarTarefa(assignedMembro.Email, assunto, corpo, _tarefa.Id);
                    }
                    catch
                    {
                        // falhas de e-mail não bloqueiam a gravação
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
