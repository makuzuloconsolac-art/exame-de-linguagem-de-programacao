namespace SisGPS_por_MN.Modelos
{
    public class EmailMensagem
    {
        public int Id { get; set; }
        public string Destinatario { get; set; } = string.Empty;
        public string Assunto { get; set; } = string.Empty;
        public string Corpo { get; set; } = string.Empty;
        public string Remetente { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; } = DateTime.Now;
        public bool Enviado { get; set; }
        public int? TarefaId { get; set; }
    }
}
