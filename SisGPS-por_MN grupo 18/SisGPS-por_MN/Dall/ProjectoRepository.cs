using System;
using System.Collections.Generic;
using System.Text;

using MySqlConnector;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Interfaces;
using SisGPS_por_MN.Modelos;
namespace SisGPS_por_MN.Dall
{
    public class ProjectoRepository : IRepositorio<Projecto>
    {
        public void Inserir(Projecto p)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"INSERT INTO projectos
                (nome, descricao, data_inicio, data_fim, orcamento, estado, cliente_nome, equipa_id)
                VALUES (@nome, @desc, @ini, @fim, @orc, @est, @cli, @eq)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", p.Nome);
            cmd.Parameters.AddWithValue("@desc", (object?)p.Descricao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ini", p.DataInicio);
            cmd.Parameters.AddWithValue("@fim", p.DataFim.HasValue ? (object)p.DataFim.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@orc", (object?)p.Orcamento ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@est", (int)p.Estado);
            cmd.Parameters.AddWithValue("@cli", (object?)p.ClienteNome ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@eq", p.EquipaId);
            cmd.ExecuteNonQuery();
            p.Id = (int)cmd.LastInsertedId;
        }

        public Projecto? ObterPorId(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = "SELECT * FROM projectos WHERE id = @id";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            using var rdr = cmd.ExecuteReader();
            return rdr.Read() ? MapearProjecto(rdr) : null;
        }

        public IEnumerable<Projecto> ObterTodos()
        {
            var lista = new List<Projecto>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM projectos ORDER BY nome", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearProjecto(rdr));
            return lista;
        }

        public void Actualizar(Projecto p)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"UPDATE projectos SET
                nome=@nome, descricao=@desc, data_inicio=@ini, data_fim=@fim,
                orcamento=@orc, estado=@est, cliente_nome=@cli, equipa_id=@eq
                WHERE id=@id";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", p.Nome);
            cmd.Parameters.AddWithValue("@desc", (object?)p.Descricao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ini", p.DataInicio);
            cmd.Parameters.AddWithValue("@fim", p.DataFim.HasValue ? (object)p.DataFim.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@orc", (object?)p.Orcamento ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@est", (int)p.Estado);
            cmd.Parameters.AddWithValue("@cli", (object?)p.ClienteNome ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@eq", p.EquipaId);
            cmd.Parameters.AddWithValue("@id", p.Id);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("DELETE FROM projectos WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Projecto> ObterPorEquipa(int equipaId)
        {
            var lista = new List<Projecto>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM projectos WHERE equipa_id=@eq ORDER BY nome", con);
            cmd.Parameters.AddWithValue("@eq", equipaId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearProjecto(rdr));
            return lista;
        }

        public IEnumerable<Projecto> ObterActivos()
        {
            var lista = new List<Projecto>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM projectos WHERE estado NOT IN (3,4) ORDER BY nome", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(MapearProjecto(rdr));
            return lista;
        }

        public void AlterarEstado(int id, EstadoProjecto novoEstado)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("UPDATE projectos SET estado=@est WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@est", (int)novoEstado);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private static Projecto MapearProjecto(MySqlDataReader r) => new Projecto
        {
            Id = r.GetInt32("id"),
            Nome = r.GetString("nome"),
            Descricao = r.IsDBNull(r.GetOrdinal("descricao")) ? null : r.GetString("descricao"),
            DataInicio = r.GetDateTime("data_inicio"),
            DataFim = r.IsDBNull(r.GetOrdinal("data_fim")) ? null : r.GetDateTime("data_fim"),
            Orcamento = r.IsDBNull(r.GetOrdinal("orcamento")) ? null : r.GetDecimal("orcamento"),
            Estado = (EstadoProjecto)r.GetByte("estado"),
            ClienteNome = r.IsDBNull(r.GetOrdinal("cliente_nome")) ? null : r.GetString("cliente_nome"),
            EquipaId = r.GetInt32("equipa_id"),
            CreatedAt = r.GetDateTime("created_at")
        };
    }
    }

