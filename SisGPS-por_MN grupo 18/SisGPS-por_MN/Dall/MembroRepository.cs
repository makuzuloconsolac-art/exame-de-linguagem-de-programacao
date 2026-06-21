using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Interfaces;
using SisGPS_por_MN.Modelos;
namespace SisGPS_por_MN.Dall
{
    public class MembroRepository
    {
        public void Inserir(Membro m)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"INSERT INTO membros (nome, email, telefone, papel, equipa_id, disponivel)
                                  VALUES (@nome, @email, @tel, @papel, @eq, @disp)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", m.Nome);
            cmd.Parameters.AddWithValue("@email", (object?)m.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tel", (object?)m.Telefone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@papel", (int)m.Papel);
            cmd.Parameters.AddWithValue("@eq", m.EquipaId);
            cmd.Parameters.AddWithValue("@disp", m.Disponivel ? 1 : 0);
            cmd.ExecuteNonQuery();
            m.Id = (int)cmd.LastInsertedId;
        }

        public Membro? ObterPorId(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM membros WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var rdr = cmd.ExecuteReader();
            return rdr.Read() ? MapearMembro(rdr) : null;
        }

        public IEnumerable<Membro> ObterTodos()
        {
            var lista = new List<Membro>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM membros ORDER BY nome", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearMembro(rdr));
            return lista;
        }

        public void Actualizar(Membro m)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"UPDATE membros SET nome=@nome, email=@email, telefone=@tel,
                                  papel=@papel, equipa_id=@eq, disponivel=@disp WHERE id=@id";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", m.Nome);
            cmd.Parameters.AddWithValue("@email", (object?)m.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tel", (object?)m.Telefone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@papel", (int)m.Papel);
            cmd.Parameters.AddWithValue("@eq", m.EquipaId);
            cmd.Parameters.AddWithValue("@disp", m.Disponivel ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", m.Id);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("DELETE FROM membros WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Membro> ObterPorEquipa(int equipaId)
        {
            var lista = new List<Membro>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM membros WHERE equipa_id=@eq ORDER BY nome", con);
            cmd.Parameters.AddWithValue("@eq", equipaId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearMembro(rdr));
            return lista;
        }

        public void AlterarDisponibilidade(int id, bool disponivel)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("UPDATE membros SET disponivel=@d WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@d", disponivel ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Membro> ObterDisponiveis()
        {
            var lista = new List<Membro>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM membros WHERE disponivel=1 ORDER BY nome", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearMembro(rdr));
            return lista;
        }

        private static Membro MapearMembro(MySqlDataReader r) => new Membro
        {
            Id = r.GetInt32("id"),
            Nome = r.GetString("nome"),
            Email = r.IsDBNull(r.GetOrdinal("email")) ? null : r.GetString("email"),
            Telefone = r.IsDBNull(r.GetOrdinal("telefone")) ? null : r.GetString("telefone"),
            Papel = (PapelMembro)r.GetByte("papel"),
            EquipaId = r.GetInt32("equipa_id"),
            Disponivel = r.GetBoolean("disponivel")
        };
    
}
}
