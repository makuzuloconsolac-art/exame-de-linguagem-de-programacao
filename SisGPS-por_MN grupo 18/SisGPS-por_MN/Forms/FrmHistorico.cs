using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Utils;
using System;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmHistorico : Form
    {
        private readonly HistoricoRepository _repo = new();
        private readonly int _tarefaId;
        private readonly string _tarefaTitulo;

        public FrmHistorico(Tarefa tarefa)
        {
            InitializeComponent();
            _tarefaId     = tarefa.Id;
            _tarefaTitulo = tarefa.Titulo;
            CarregarHistorico();
        }

        private void CarregarHistorico()
        {
            lblTitulo.Text = $"Histórico da tarefa #{_tarefaId}: {_tarefaTitulo}";
            dgvHistorico.Rows.Clear();

            foreach (var h in _repo.ObterPorTarefa(_tarefaId))
            {
                dgvHistorico.Rows.Add(
                    h.DataMudanca.ToString("dd/MM/yyyy HH:mm"),
                    Formatador.FormatarEstado(h.EstadoAnterior),
                    Formatador.FormatarEstado(h.EstadoNovo),
                    h.Observacao ?? ""
                );
            }
        }
    }
}
