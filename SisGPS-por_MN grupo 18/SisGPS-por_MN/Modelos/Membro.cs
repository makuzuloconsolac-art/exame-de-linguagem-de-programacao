using SisGPS_por_MN.Enums;

namespace SisGPS_por_MN.Modelos
{
    public class Membro
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public PapelMembro Papel { get; set; } = PapelMembro.Developer;
        public int EquipaId { get; set; }
        public bool Disponivel { get; set; } = true;
        public Equipa? Equipa { get; set; }

        public string ObterPapelTexto() => Papel switch
        {
            PapelMembro.Developer => "Developer",
            PapelMembro.QA => "QA",
            PapelMembro.ProjectManager => "Project Manager",
            PapelMembro.Designer => "Designer",
            PapelMembro.DevOps => "DevOps",
            _ => "Desconhecido"
        };
    }
}
