using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Interfaces;
using SisGPS_por_MN.Modelos;
using System.Text;
using MySqlConnector;

namespace SisGPS_por_MN.Dall
{
    public class TarefaRepository : IRepositorio<Tarefa>
    {
        public void Inserir(Tarefa t)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"INSERT INTO tarefas
                (titulo, descricao, estado, prioridade, horas_estimadas, horas_registadas, data_prazo, membro_id, sprint_id)
                VALUES (@tit, @desc, @est, @prio, @he, @hr, @prazo, @mid, @sid)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@tit", t.Titulo);
            cmd.Parameters.AddWithValue("@desc", (object?)t.Descricao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@est", (int)t.Estado);
            cmd.Parameters.AddWithValue("@prio", (int)t.Prioridade);
            cmd.Parameters.AddWithValue("@he", t.HorasEstimadas);
            cmd.Parameters.AddWithValue("@hr", t.HorasRegistadas);
            cmd.Parameters.AddWithValue("@prazo", t.DataPrazo.HasValue ? (object)t.DataPrazo.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@mid", (object?)t.MembroId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sid", t.SprintId);
            cmd.ExecuteNonQuery();
            t.Id = (int)cmd.LastInsertedId;
        }

        public Tarefa? ObterPorId(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM tarefas WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var rdr = cmd.ExecuteReader();
            return rdr.Read() ? MapearTarefa(rdr) : null;
        }

        public IEnumerable<Tarefa> ObterTodos()
        {
            var lista = new List<Tarefa>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM tarefas ORDER BY prioridade DESC", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearTarefa(rdr));
            return lista;
        }

        public void Actualizar(Tarefa t)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"UPDATE tarefas SET titulo=@tit, descricao=@desc, estado=@est,
                prioridade=@prio, horas_estimadas=@he, horas_registadas=@hr,
                data_prazo=@prazo, membro_id=@mid, sprint_id=@sid WHERE id=@id";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@tit", t.Titulo);
            cmd.Parameters.AddWithValue("@desc", (object?)t.Descricao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@est", (int)t.Estado);
            cmd.Parameters.AddWithValue("@prio", (int)t.Prioridade);
            cmd.Parameters.AddWithValue("@he", t.HorasEstimadas);
            cmd.Parameters.AddWithValue("@hr", t.HorasRegistadas);
            cmd.Parameters.AddWithValue("@prazo", t.DataPrazo.HasValue ? (object)t.DataPrazo.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@mid", (object?)t.MembroId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sid", t.SprintId);
            cmd.Parameters.AddWithValue("@id", t.Id);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("DELETE FROM tarefas WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Tarefa> ObterPorSprint(int sprintId)
        {
            var lista = new List<Tarefa>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand(
                "SELECT * FROM tarefas WHERE sprint_id=@sid ORDER BY prioridade DESC", con);
            cmd.Parameters.AddWithValue("@sid", sprintId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearTarefa(rdr));
            return lista;
        }

        public IEnumerable<Tarefa> ObterPorMembro(int membroId)
        {
            var lista = new List<Tarefa>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand(
                "SELECT * FROM tarefas WHERE membro_id=@mid ORDER BY prioridade DESC", con);
            cmd.Parameters.AddWithValue("@mid", membroId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearTarefa(rdr));
            return lista;
        }

        public IEnumerable<Tarefa> ObterPorEstado(int sprintId, EstadoTarefa estado)
        {
            var lista = new List<Tarefa>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand(
                "SELECT * FROM tarefas WHERE sprint_id=@sid AND estado=@est ORDER BY prioridade DESC", con);
            cmd.Parameters.AddWithValue("@sid", sprintId);
            cmd.Parameters.AddWithValue("@est", (int)estado);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearTarefa(rdr));
            return lista;
        }

        public IEnumerable<KanbanItem> ObterKanban(int? projectoId = null, int? sprintId = null)
        {
            var lista = new List<KanbanItem>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();

            var sql = new StringBuilder("SELECT * FROM vw_kanban WHERE 1=1");
            if (sprintId is > 0)
                sql.Append(" AND sprint_id = @sid");
            else if (projectoId is > 0)
                sql.Append(" AND projecto_id = @pid");

            using var cmd = new MySqlCommand(sql.ToString(), con);
            if (sprintId is > 0)
                cmd.Parameters.AddWithValue("@sid", sprintId.Value);
            else if (projectoId is > 0)
                cmd.Parameters.AddWithValue("@pid", projectoId.Value);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearKanban(rdr));
            return lista;
        }

        public void AlterarEstado(int id, EstadoTarefa novoEstado, string obs)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("CALL sp_alterar_estado_tarefa(@tid, @est, @obs)", con);
            cmd.Parameters.AddWithValue("@tid", id);
            cmd.Parameters.AddWithValue("@est", (int)novoEstado);
            cmd.Parameters.AddWithValue("@obs", (object?)obs ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void AtribuirMembro(int tarefaId, int membroId)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("UPDATE tarefas SET membro_id=@mid WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@mid", membroId);
            cmd.Parameters.AddWithValue("@id", tarefaId);
            cmd.ExecuteNonQuery();
        }

        public void RegistarHoras(int tarefaId, decimal horas)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand(
                "UPDATE tarefas SET horas_registadas = horas_registadas + @h WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@h", horas);
            cmd.Parameters.AddWithValue("@id", tarefaId);
            cmd.ExecuteNonQuery();
        }

        private static KanbanItem MapearKanban(MySqlDataReader r) => new KanbanItem
        {
            TarefaId = r.GetInt32("tarefa_id"),
            Titulo = r.GetString("titulo"),
            Estado = (EstadoTarefa)r.GetByte("estado"),
            Prioridade = (Prioridade)r.GetByte("prioridade"),
            HorasEstimadas = r.GetDecimal("horas_estimadas"),
            HorasRegistadas = r.GetDecimal("horas_registadas"),
            DataPrazo = r.IsDBNull(r.GetOrdinal("data_prazo")) ? null : r.GetDateTime("data_prazo"),
            Membro = r.IsDBNull(r.GetOrdinal("membro")) ? null : r.GetString("membro"),
            SprintId = r.GetInt32("sprint_id"),
            Sprint = r.GetString("sprint"),
            ProjectoId = r.GetInt32("projecto_id"),
            Projecto = r.GetString("projecto")
        };

        private static Tarefa MapearTarefa(MySqlDataReader r) => new Tarefa
        {
            Id = r.GetInt32("id"),
            Titulo = r.GetString("titulo"),
            Descricao = r.IsDBNull(r.GetOrdinal("descricao")) ? null : r.GetString("descricao"),
            Estado = (EstadoTarefa)r.GetByte("estado"),
            Prioridade = (Prioridade)r.GetByte("prioridade"),
            HorasEstimadas = r.GetDecimal("horas_estimadas"),
            HorasRegistadas = r.GetDecimal("horas_registadas"),
            DataPrazo = r.IsDBNull(r.GetOrdinal("data_prazo")) ? null : r.GetDateTime("data_prazo"),
            MembroId = r.IsDBNull(r.GetOrdinal("membro_id")) ? null : r.GetInt32("membro_id"),
            SprintId = r.GetInt32("sprint_id"),
            CreatedAt = r.GetDateTime("created_at")
        };
    }
}
