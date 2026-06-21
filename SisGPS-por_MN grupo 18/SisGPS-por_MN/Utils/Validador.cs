using System.Text;
using System.Text.RegularExpressions;

namespace SisGPS_por_MN.Utils
{
    public static class Validador
    {
        public static bool CampoObrigatorio(string valor) =>
            !string.IsNullOrWhiteSpace(valor);

        public static bool EmailValido(string email) =>
            Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public static string CapturarErros(Control[] campos)
        {
            var sb = new StringBuilder();
            foreach (var c in campos)
            {
                if (c is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
                    sb.AppendLine($"• O campo '{c.Tag ?? c.Name}' é obrigatório.");
            }
            return sb.ToString();
        }
    }
}
