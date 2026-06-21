using MySqlConnector;
using SisGPS_por_MN.Dall;
using System;

namespace SisGPS_por_MN.Servicos
{
    public class ResumoDashboard
    {
        public int TotalProjectos { get; set; }
        public int ProjectosActivos { get; set; }
        public int ProjectosConcluidos { get; set; }
        public int TotalEquipas { get; set; }
        public int TotalMembros { get; set; }
        public int SprintsAbertos { get; set; }
        public int TotalTarefas { get; set; }
        public int TarefasPendentes { get; set; }
        public int TarefasEmProgresso { get; set; }
        public int TarefasConcluidas { get; set; }
        public int EmailsEnviados { get; set; }
        public int MinhasTarefas { get; set; }
    }

    public class ServicoDashboard
    {
        public ResumoDashboard ObterResumo(int? membroId = null)
        {
            var r = new ResumoDashboard();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();

            r.TotalProjectos = ScalarInt(con, "SELECT COUNT(*) FROM projectos");
            r.ProjectosActivos = ScalarInt(con, "SELECT COUNT(*) FROM projectos WHERE estado NOT IN (3,4)");
            r.ProjectosConcluidos = ScalarInt(con, "SELECT COUNT(*) FROM projectos WHERE estado = 3");
            
            r.TotalEquipas = ScalarInt(con, "SELECT COUNT(*) FROM equipas");
            r.TotalMembros = ScalarInt(con, "SELECT COUNT(*) FROM membros");
            r.SprintsAbertos = ScalarInt(con, "SELECT COUNT(*) FROM sprints WHERE encerrado=0");
            
            r.TotalTarefas = ScalarInt(con, "SELECT COUNT(*) FROM tarefas");
            r.TarefasPendentes = ScalarInt(con, "SELECT COUNT(*) FROM tarefas WHERE estado != 2");
            r.TarefasEmProgresso = ScalarInt(con, "SELECT COUNT(*) FROM tarefas WHERE estado = 1");
            r.TarefasConcluidas = ScalarInt(con, "SELECT COUNT(*) FROM tarefas WHERE estado = 2");

            try
            {
                r.EmailsEnviados = ScalarInt(con, "SELECT COUNT(*) FROM emails WHERE enviado=1");
            }
            catch
            {
                r.EmailsEnviados = 0;
            }

            if (membroId.HasValue)
            {
                using var cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM tarefas WHERE membro_id=@mid AND estado != 2", con);
                cmd.Parameters.AddWithValue("@mid", membroId.Value);
                r.MinhasTarefas = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return r;
        }

        private static int ScalarInt(MySqlConnection con, string sql)
        {
            using var cmd = new MySqlCommand(sql, con);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
