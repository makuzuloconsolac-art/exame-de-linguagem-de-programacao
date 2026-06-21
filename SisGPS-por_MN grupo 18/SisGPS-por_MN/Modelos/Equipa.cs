namespace SisGPS_por_MN.Modelos
{
    public class Equipa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Membro> Membros { get; set; } = new List<Membro>();
    }
}
