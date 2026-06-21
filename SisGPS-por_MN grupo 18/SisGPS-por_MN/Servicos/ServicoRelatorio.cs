using SisGPS_por_MN.Dall;
using MySqlConnector;
using System.Data;

namespace SisGPS_por_MN.Servicos
{
    public class ServicoRelatorio
    {
        public DataTable RelatorioProgressoProjecto(int projectoId)
        {
            var dt = new DataTable();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = "SELECT * FROM vw_progresso_projecto WHERE projecto_id = @pid";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@pid", projectoId);
            using var rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            return dt;
        }

        public DataTable RelatorioVelocidadeSprint(int projectoId)
        {
            var dt = new DataTable();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"SELECT vs.* FROM vw_velocidade_sprint vs
                JOIN sprints s ON s.id = vs.sprint_id
                WHERE s.projecto_id = @pid ORDER BY vs.data_inicio";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@pid", projectoId);
            using var rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            return dt;
        }

        public DataTable RelatorioHorasMembro(int equipaId)
        {
            var dt = new DataTable();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"SELECT hm.* FROM vw_horas_membro hm
                JOIN membros m ON m.id = hm.membro_id
                WHERE m.equipa_id = @eid ORDER BY hm.horas_registadas DESC";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@eid", equipaId);
            using var rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            return dt;
        }
    }
}
