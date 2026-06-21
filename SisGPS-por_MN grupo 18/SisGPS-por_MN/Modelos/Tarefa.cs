using SisGPS_por_MN.Enums;

namespace SisGPS_por_MN.Modelos
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public EstadoTarefa Estado { get; set; } = EstadoTarefa.Backlog;
        public Prioridade Prioridade { get; set; } = Prioridade.Media;
        public decimal HorasEstimadas { get; set; } = 0m;
        public decimal HorasRegistadas { get; set; } = 0m;
        public DateTime? DataPrazo { get; set; }
        public int? MembroId { get; set; }
        public int SprintId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Membro? Membro { get; set; }
        public Sprint? Sprint { get; set; }
    }
}
