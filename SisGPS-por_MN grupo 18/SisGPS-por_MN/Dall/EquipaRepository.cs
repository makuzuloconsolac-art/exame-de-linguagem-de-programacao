using SisGPS_por_MN.Interfaces;
using SisGPS_por_MN.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;


namespace SisGPS_por_MN.Dall
{
    public class EquipaRepository : IRepositorio<Equipa>
    {
        public void Inserir(Equipa e)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = "INSERT INTO equipas (nome, descricao) VALUES (@nome, @desc)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@desc", (object?)e.Descricao ?? DBNull.Value);
            cmd.ExecuteNonQuery();
            e.Id = (int)cmd.LastInsertedId;
        }

        public Equipa? ObterPorId(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM equipas WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var rdr = cmd.ExecuteReader();
            return rdr.Read() ? MapearEquipa(rdr) : null;
        }

        public IEnumerable<Equipa> ObterTodos()
        {
            var lista = new List<Equipa>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM equipas ORDER BY nome", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearEquipa(rdr));
            return lista;
        }

        public void Actualizar(Equipa e)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("UPDATE equipas SET nome=@nome, descricao=@desc WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@desc", (object?)e.Descricao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", e.Id);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("DELETE FROM equipas WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Equipa> ObterComMembros()
        {
            var equipas = new List<Equipa>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();

            using (var cmdE = new MySqlCommand("SELECT * FROM equipas ORDER BY nome", con))
            using (var rdrE = cmdE.ExecuteReader())
                while (rdrE.Read()) equipas.Add(MapearEquipa(rdrE));

            var membroRepo = new MembroRepository();
            foreach (var eq in equipas)
                eq.Membros.AddRange(membroRepo.ObterPorEquipa(eq.Id));

            return equipas;
        }

        private static Equipa MapearEquipa(MySqlDataReader r) => new Equipa
        {
            Id = r.GetInt32("id"),
            Nome = r.GetString("nome"),
            Descricao = r.IsDBNull(r.GetOrdinal("descricao")) ? null : r.GetString("descricao"),
            CreatedAt = r.GetDateTime("created_at")
        };
    }
}
