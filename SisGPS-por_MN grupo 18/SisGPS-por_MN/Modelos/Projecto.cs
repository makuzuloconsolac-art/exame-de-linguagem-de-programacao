using SisGPS_por_MN.Enums;

namespace SisGPS_por_MN.Modelos
{
    public class Projecto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public decimal? Orcamento { get; set; }
        public EstadoProjecto Estado { get; set; } = EstadoProjecto.Planeamento;
        public string? ClienteNome { get; set; }
        public int EquipaId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool EstaActivo() =>
            Estado != EstadoProjecto.Concluido && Estado != EstadoProjecto.Cancelado;
    }
}
