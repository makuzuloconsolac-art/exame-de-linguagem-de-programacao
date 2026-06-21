using System.Security.Cryptography;
using System.Text;

namespace SisGPS_por_MN.Utils
{
    public static class Criptografia
    {
        public static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
