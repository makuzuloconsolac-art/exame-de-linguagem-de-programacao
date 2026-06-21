namespace SisGPS_por_MN.Servicos
{
    public static class ValidadorNegocio
    {
        public static void ValidarDatas(DateTime inicio, DateTime? fim)
        {
            if (fim.HasValue && fim.Value < inicio)
                throw new ArgumentException("A data de fim não pode ser anterior à data de início.");
        }

        public static void ValidarHoras(decimal horas)
        {
            if (horas < 0)
                throw new ArgumentException("As horas não podem ser negativas.");
        }

        public static void ValidarNome(string nome, int maxLen)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome é obrigatório.");
            if (nome.Trim().Length > maxLen)
                throw new ArgumentException($"O nome não pode exceder {maxLen} caracteres.");
        }
    }
}
