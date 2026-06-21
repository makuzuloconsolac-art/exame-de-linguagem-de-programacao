using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Exceptions;
using SisGPS_por_MN.Interfaces;
using SisGPS_por_MN.Modelos;

namespace SisGPS_por_MN.Servicos
{
    public class GestorProjectos : IProgressavel
    {
        private readonly ProjectoRepository _projectoRepo;
        private readonly SprintRepository _sprintRepo;
        private readonly TarefaRepository _tarefaRepo;
        private readonly MembroRepository _membroRepo;
        private readonly EquipaRepository _equipaRepo;

        public int ProjectoId { get; set; }

        public GestorProjectos(int projectoId = 0)
        {
            ProjectoId = projectoId;
            _projectoRepo = new ProjectoRepository();
            _sprintRepo = new SprintRepository();
            _tarefaRepo = new TarefaRepository();
            _membroRepo = new MembroRepository();
            _equipaRepo = new EquipaRepository();
        }

        public double CalcularProgresso()
        {
            var sprints = _sprintRepo.ObterPorProjecto(ProjectoId);
            int total = 0, concluidas = 0;
            foreach (var s in sprints)
            {
                var tarefas = _tarefaRepo.ObterPorSprint(s.Id).ToList();
                total += tarefas.Count;
                concluidas += tarefas.Count(t => t.Estado == EstadoTarefa.Concluida);
            }
            return total == 0 ? 0.0 : Math.Round((double)concluidas / total * 100, 1);
        }

        public string ObterResumo()
        {
            var p = _projectoRepo.ObterPorId(ProjectoId);
            if (p == null) return "Projecto não encontrado.";
            double prog = CalcularProgresso();
            return $"Projecto: {p.Nome} | Estado: {p.Estado} | Progresso: {prog}%";
        }

        public void CriarSprint(Sprint s)
        {
            var projecto = _projectoRepo.ObterPorId(s.ProjectoId)
                ?? throw new ArgumentException($"Projecto {s.ProjectoId} não existe.");

            if (!projecto.EstaActivo())
                throw new ProjectoEncerradoException(
                    $"Não é possível criar um sprint no projecto '{projecto.Nome}' com estado {projecto.Estado}.");

            _sprintRepo.Inserir(s);
        }

        public void AtribuirTarefa(int tarefaId, int membroId)
        {
            var membro = _membroRepo.ObterPorId(membroId)
                ?? throw new ArgumentException($"Membro {membroId} não existe.");

            if (!membro.Disponivel)
                throw new MembroNaoDisponivelException(membro.Nome);

            _tarefaRepo.AtribuirMembro(tarefaId, membroId);
        }

        public decimal CalcularVelocidadeSprint(int sprintId)
        {
            var tarefas = _tarefaRepo.ObterPorSprint(sprintId);
            return tarefas
                .Where(t => t.Estado == EstadoTarefa.Concluida)
                .Sum(t => t.HorasEstimadas);
        }

        public int EncerrarSprint(int sprintId)
        {
            var sprint = _sprintRepo.ObterPorId(sprintId)
                ?? throw new ArgumentException($"Sprint {sprintId} não existe.");

            if (sprint.Encerrado)
                throw new SprintFechadoException($"O sprint '{sprint.Nome}' já está encerrado.");

            return _sprintRepo.Encerrar(sprintId);
        }
    }
}
