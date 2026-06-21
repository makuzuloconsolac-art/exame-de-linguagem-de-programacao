using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Exceptions;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Servicos;
using SisGPS_por_MN.Utils;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmAlterarEstado : Form
    {
        private readonly TarefaRepository _repo = new();
        private readonly Tarefa _tarefa;

        public FrmAlterarEstado(Tarefa t)
        {
            InitializeComponent();
            _tarefa = t;
            CarregarForm();
        }

        private void CarregarForm()
        {
            lblTarefaInfo.Text = $"Tarefa #{_tarefa.Id}: {_tarefa.Titulo}";
            lblValorActual.Text = Formatador.FormatarEstado(_tarefa.Estado);

            cmbNovoEstado.Items.Clear();
            foreach (EstadoTarefa est in Enum.GetValues(typeof(EstadoTarefa)))
            {
                if (est != _tarefa.Estado)
                    cmbNovoEstado.Items.Add(new EstadoItem(est));
            }
            if (cmbNovoEstado.Items.Count > 0)
                cmbNovoEstado.SelectedIndex = 0;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (cmbNovoEstado.SelectedItem is not EstadoItem item)
            {
                MessageBox.Show("Seleccione o novo estado.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_tarefa.Estado == EstadoTarefa.Backlog && !_tarefa.MembroId.HasValue)
            {
                MessageBox.Show(
                    "A tarefa não tem membro atribuído.\nAtribua um membro antes de alterar o estado.",
                    "Membro Necessário", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string obs = txtObservacao.Text.Trim();
                _repo.AlterarEstado(_tarefa.Id, item.Estado, obs);
                
                if (_tarefa.MembroId.HasValue)
                {
                    try
                    {
                        var mRepo = new MembroRepository();
                        var m = mRepo.ObterPorId(_tarefa.MembroId.Value);
                        if (m != null && !string.IsNullOrEmpty(m.Email))
                        {
                            var servEmail = new ServicoEmail();
                            string assunto = $"Alteração de Estado: {_tarefa.Titulo}";
                            string corpo = $"Olá {m.Nome},\n\nO estado da tarefa '{_tarefa.Titulo}' foi alterado de {Formatador.FormatarEstado(_tarefa.Estado)} para {Formatador.FormatarEstado(item.Estado)}.\nObservação: {(string.IsNullOrEmpty(obs) ? "Nenhuma" : obs)}\n\nSisGPS.";
                            servEmail.EnviarENotificarTarefa(m.Email, assunto, corpo, _tarefa.Id);
                        }
                    }
                    catch
                    {
                        // falhas no envio não bloqueiam o fluxo
                    }
                }

                _tarefa.Estado = item.Estado;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (TarefaNaoAtribuidaException ex)
            {
                MessageBox.Show(ex.Message, "Tarefa não atribuída",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private sealed class EstadoItem
        {
            public EstadoTarefa Estado { get; }
            public EstadoItem(EstadoTarefa e) => Estado = e;
            public override string ToString() => Formatador.FormatarEstado(Estado);
        }
    }
}
