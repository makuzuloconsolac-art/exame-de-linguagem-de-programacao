using SisGPS_por_MN.Enums;

namespace SisGPS_por_MN.Modelos
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public NivelAcesso NivelAcesso { get; set; } = NivelAcesso.Funcionario;
        public int? MembroId { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UltimoAcesso { get; set; }
        public string PerguntaSeguranca { get; set; } = string.Empty;
        public string RespostaSeguranca { get; set; } = string.Empty;

        public bool EhAdministrador => NivelAcesso == NivelAcesso.Administrador;
    }
}
