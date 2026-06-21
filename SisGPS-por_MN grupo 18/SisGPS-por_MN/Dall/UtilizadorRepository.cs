using MySqlConnector;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Interfaces;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Utils;

namespace SisGPS_por_MN.Dall
{
    public class UtilizadorRepository : IRepositorio<Utilizador>
    {
        public void Inserir(Utilizador u)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"INSERT INTO utilizadores
                (username, password_hash, nome, nivel_acesso, membro_id, activo, pergunta_seguranca, resposta_seguranca)
                VALUES (@user, @hash, @nome, @nivel, @mid, @act, @perg, @resp)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@user", u.Username);
            cmd.Parameters.AddWithValue("@hash", u.PasswordHash);
            cmd.Parameters.AddWithValue("@nome", u.Nome);
            cmd.Parameters.AddWithValue("@nivel", (int)u.NivelAcesso);
            cmd.Parameters.AddWithValue("@mid", (object?)u.MembroId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@act", u.Activo ? 1 : 0);
            cmd.Parameters.AddWithValue("@perg", u.PerguntaSeguranca);
            cmd.Parameters.AddWithValue("@resp", u.RespostaSeguranca);
            cmd.ExecuteNonQuery();
            u.Id = (int)cmd.LastInsertedId;
        }

        public Utilizador? ObterPorId(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM utilizadores WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var rdr = cmd.ExecuteReader();
            return rdr.Read() ? Mapear(rdr) : null;
        }

        public IEnumerable<Utilizador> ObterTodos()
        {
            var lista = new List<Utilizador>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM utilizadores ORDER BY nome", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(Mapear(rdr));
            return lista;
        }

        public void Actualizar(Utilizador u)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"UPDATE utilizadores SET username=@user, password_hash=@hash,
                nome=@nome, nivel_acesso=@nivel, membro_id=@mid, activo=@act,
                pergunta_seguranca=@perg, resposta_seguranca=@resp WHERE id=@id";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@user", u.Username);
            cmd.Parameters.AddWithValue("@hash", u.PasswordHash);
            cmd.Parameters.AddWithValue("@nome", u.Nome);
            cmd.Parameters.AddWithValue("@nivel", (int)u.NivelAcesso);
            cmd.Parameters.AddWithValue("@mid", (object?)u.MembroId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@act", u.Activo ? 1 : 0);
            cmd.Parameters.AddWithValue("@perg", (object?)u.PerguntaSeguranca ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@resp", (object?)u.RespostaSeguranca ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", u.Id);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("DELETE FROM utilizadores WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public Utilizador? Autenticar(string username, string password)
        {
            var hash = Criptografia.HashPassword(password);
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"SELECT * FROM utilizadores
                WHERE username=@user AND password_hash=@hash AND activo=1";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@user", username.Trim());
            cmd.Parameters.AddWithValue("@hash", hash);
            using var rdr = cmd.ExecuteReader();
            return rdr.Read() ? Mapear(rdr) : null;
        }

        public IEnumerable<Utilizador> Buscar(string termo)
        {
            var lista = new List<Utilizador>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"SELECT * FROM utilizadores
                WHERE username LIKE @t OR nome LIKE @t ORDER BY nome";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@t", $"%{termo.Trim()}%");
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(Mapear(rdr));
            return lista;
        }

        private static Utilizador Mapear(MySqlDataReader r) => new Utilizador
        {
            Id             = r.GetInt32("id"),
            Username       = r.GetString("username"),
            PasswordHash   = r.GetString("password_hash"),
            Nome           = r.GetString("nome"),
            NivelAcesso    = (NivelAcesso)r.GetByte("nivel_acesso"),
            MembroId       = r.IsDBNull(r.GetOrdinal("membro_id")) ? null : r.GetInt32("membro_id"),
            Activo         = r.GetBoolean("activo"),
            CreatedAt      = r.GetDateTime("created_at"),
            UltimoAcesso      = ColExists(r, "ultimo_acesso")      && !r.IsDBNull(r.GetOrdinal("ultimo_acesso"))      ? r.GetDateTime("ultimo_acesso")      : null,
            PerguntaSeguranca = ColExists(r, "pergunta_seguranca") && !r.IsDBNull(r.GetOrdinal("pergunta_seguranca")) ? r.GetString("pergunta_seguranca")  : string.Empty,
            RespostaSeguranca = ColExists(r, "resposta_seguranca") && !r.IsDBNull(r.GetOrdinal("resposta_seguranca")) ? r.GetString("resposta_seguranca")  : string.Empty
        };

        private static bool ColExists(MySqlDataReader r, string colName)
        {
            for (int i = 0; i < r.FieldCount; i++)
                if (r.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }
}
