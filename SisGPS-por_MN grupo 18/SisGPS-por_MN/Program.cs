using SisGPS_por_MN.Forms;

namespace SisGPS_por_MN
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            // Inicializar base de dados (criação de tabelas em falta e sementeira)
            Dall.ConexaoBD.InicializarBD();

            using (var login = new FrmLogin())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new FrmPrincipal());
                }
            }
        }
    }
}
