using SisGPS_por_MN.Enums;

namespace SisGPS_por_MN.Modelos
{
    public class HistoricoTarefa
    {
        public int Id { get; set; }
        public int TarefaId { get; set; }
        public EstadoTarefa EstadoAnterior { get; set; }
        public EstadoTarefa EstadoNovo { get; set; }
        public DateTime DataMudanca { get; set; } = DateTime.Now;
        public string? Observacao { get; set; }
        public Tarefa? Tarefa { get; set; }
    }
}
