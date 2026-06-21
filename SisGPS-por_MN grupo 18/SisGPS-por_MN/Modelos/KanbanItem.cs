using SisGPS_por_MN.Enums;

namespace SisGPS_por_MN.Modelos
{
    public class KanbanItem
    {
        public int TarefaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public EstadoTarefa Estado { get; set; }
        public Prioridade Prioridade { get; set; }
        public decimal HorasEstimadas { get; set; }
        public decimal HorasRegistadas { get; set; }
        public DateTime? DataPrazo { get; set; }
        public string? Membro { get; set; }
        public int SprintId { get; set; }
        public string Sprint { get; set; } = string.Empty;
        public int ProjectoId { get; set; }
        public string Projecto { get; set; } = string.Empty;
    }
}
