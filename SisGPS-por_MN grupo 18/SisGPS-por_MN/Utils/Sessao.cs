using SisGPS_por_MN.Modelos;

namespace SisGPS_por_MN.Utils
{
    public static class Sessao
    {
        public static Utilizador? UtilizadorActual { get; private set; }

        public static void Iniciar(Utilizador u) => UtilizadorActual = u;

        public static void Terminar() => UtilizadorActual = null;

        public static bool EstaAutenticado => UtilizadorActual != null;

        public static bool EhAdmin => UtilizadorActual?.EhAdministrador == true;
    }
}
