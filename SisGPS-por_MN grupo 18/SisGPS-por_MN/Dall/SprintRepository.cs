using SisGPS_por_MN.Interfaces;
using SisGPS_por_MN.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;

namespace SisGPS_por_MN.Dall
{
    public class SprintRepository : IRepositorio<Sprint>
    {
        public void Inserir(Sprint s)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"INSERT INTO sprints (nome, objectivo, data_inicio, data_fim, encerrado, projecto_id)
                                  VALUES (@nome, @obj, @ini, @fim, @enc, @pid)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", s.Nome);
            cmd.Parameters.AddWithValue("@obj", (object?)s.Objectivo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ini", s.DataInicio);
            cmd.Parameters.AddWithValue("@fim", s.DataFim);
            cmd.Parameters.AddWithValue("@enc", s.Encerrado ? 1 : 0);
            cmd.Parameters.AddWithValue("@pid", s.ProjectoId);
            cmd.ExecuteNonQuery();
            s.Id = (int)cmd.LastInsertedId;
        }

        public Sprint? ObterPorId(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM sprints WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var rdr = cmd.ExecuteReader();
            return rdr.Read() ? MapearSprint(rdr) : null;
        }

        public IEnumerable<Sprint> ObterTodos()
        {
            var lista = new List<Sprint>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM sprints ORDER BY data_inicio DESC", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearSprint(rdr));
            return lista;
        }

        public void Actualizar(Sprint s)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"UPDATE sprints SET nome=@nome, objectivo=@obj,
                                  data_inicio=@ini, data_fim=@fim, encerrado=@enc WHERE id=@id";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", s.Nome);
            cmd.Parameters.AddWithValue("@obj", (object?)s.Objectivo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ini", s.DataInicio);
            cmd.Parameters.AddWithValue("@fim", s.DataFim);
            cmd.Parameters.AddWithValue("@enc", s.Encerrado ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", s.Id);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("DELETE FROM sprints WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Sprint> ObterPorProjecto(int projectoId)
        {
            var lista = new List<Sprint>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM sprints WHERE projecto_id=@pid ORDER BY data_inicio", con);
            cmd.Parameters.AddWithValue("@pid", projectoId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearSprint(rdr));
            return lista;
        }

        public IEnumerable<Sprint> ObterAbertos(int projectoId)
        {
            var lista = new List<Sprint>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand(
                "SELECT * FROM sprints WHERE projecto_id=@pid AND encerrado=0 ORDER BY data_inicio", con);
            cmd.Parameters.AddWithValue("@pid", projectoId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearSprint(rdr));
            return lista;
        }

        public int Encerrar(int sprintId)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("CALL sp_encerrar_sprint(@sid, @pend)", con);
            cmd.Parameters.AddWithValue("@sid", sprintId);
            var pPend = new MySqlParameter("@pend", MySqlDbType.Int32)
            { Direction = System.Data.ParameterDirection.Output };
            cmd.Parameters.Add(pPend);
            cmd.ExecuteNonQuery();
            return Convert.ToInt32(pPend.Value);
        }

        private static Sprint MapearSprint(MySqlDataReader r) => new Sprint
        {
            Id = r.GetInt32("id"),
            Nome = r.GetString("nome"),
            Objectivo = r.IsDBNull(r.GetOrdinal("objectivo")) ? null : r.GetString("objectivo"),
            DataInicio = r.GetDateTime("data_inicio"),
            DataFim = r.GetDateTime("data_fim"),
            Encerrado = r.GetBoolean("encerrado"),
            ProjectoId = r.GetInt32("projecto_id")
        };
    }
}
