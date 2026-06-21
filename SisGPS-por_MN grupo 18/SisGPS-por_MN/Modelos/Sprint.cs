using SisGPS_por_MN.Exceptions;

namespace SisGPS_por_MN.Modelos
{
    public class Sprint
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Objectivo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Encerrado { get; set; } = false;
        public int ProjectoId { get; set; }
        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

        public void Encerrar()
        {
            if (Encerrado)
                throw new SprintFechadoException($"O sprint '{Nome}' já está encerrado.");
            Encerrado = true;
        }
    }
}
