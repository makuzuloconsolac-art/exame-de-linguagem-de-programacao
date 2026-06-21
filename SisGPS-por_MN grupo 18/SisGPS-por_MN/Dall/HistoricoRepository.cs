using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Interfaces;
using SisGPS_por_MN.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;
namespace SisGPS_por_MN.Dall
{
    public class HistoricoRepository : IRepositorio<HistoricoTarefa>
    {
        public void Inserir(HistoricoTarefa h)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"INSERT INTO historico_tarefas
                (tarefa_id, estado_anterior, estado_novo, observacao)
                VALUES (@tid, @ant, @novo, @obs)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@tid", h.TarefaId);
            cmd.Parameters.AddWithValue("@ant", (int)h.EstadoAnterior);
            cmd.Parameters.AddWithValue("@novo", (int)h.EstadoNovo);
            cmd.Parameters.AddWithValue("@obs", (object?)h.Observacao ?? DBNull.Value);
            cmd.ExecuteNonQuery();
            h.Id = (int)cmd.LastInsertedId;
        }

        public HistoricoTarefa? ObterPorId(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM historico_tarefas WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var rdr = cmd.ExecuteReader();
            return rdr.Read() ? MapearHistorico(rdr) : null;
        }

        public IEnumerable<HistoricoTarefa> ObterTodos()
        {
            var lista = new List<HistoricoTarefa>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand(
                "SELECT * FROM historico_tarefas ORDER BY data_mudanca DESC", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearHistorico(rdr));
            return lista;
        }

        public void Actualizar(HistoricoTarefa h)
        {
            throw new NotSupportedException("O histórico de tarefa é imutável e não pode ser atualizado.");
        }

        public void Eliminar(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("DELETE FROM historico_tarefas WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<HistoricoTarefa> ObterPorTarefa(int tarefaId)
        {
            var lista = new List<HistoricoTarefa>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand(
                "SELECT * FROM historico_tarefas WHERE tarefa_id=@tid ORDER BY data_mudanca ASC", con);
            cmd.Parameters.AddWithValue("@tid", tarefaId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearHistorico(rdr));
            return lista;
        }

        private static HistoricoTarefa MapearHistorico(MySqlDataReader r) => new HistoricoTarefa
        {
            Id = r.GetInt32("id"),
            TarefaId = r.GetInt32("tarefa_id"),
            EstadoAnterior = (EstadoTarefa)r.GetByte("estado_anterior"),
            EstadoNovo = (EstadoTarefa)r.GetByte("estado_novo"),
            DataMudanca = r.GetDateTime("data_mudanca"),
            Observacao = r.IsDBNull(r.GetOrdinal("observacao")) ? null : r.GetString("observacao")
        };
    }
}
