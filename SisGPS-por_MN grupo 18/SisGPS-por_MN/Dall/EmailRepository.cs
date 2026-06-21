using MySqlConnector;
using SisGPS_por_MN.Interfaces;
using SisGPS_por_MN.Modelos;

namespace SisGPS_por_MN.Dall
{
    public class EmailRepository : IRepositorio<EmailMensagem>
    {
        public void Inserir(EmailMensagem e)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"INSERT INTO emails
                (destinatario, assunto, corpo, remetente, enviado, tarefa_id)
                VALUES (@dest, @ass, @corpo, @rem, @env, @tid)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@dest", e.Destinatario);
            cmd.Parameters.AddWithValue("@ass", e.Assunto);
            cmd.Parameters.AddWithValue("@corpo", e.Corpo);
            cmd.Parameters.AddWithValue("@rem", e.Remetente);
            cmd.Parameters.AddWithValue("@env", e.Enviado ? 1 : 0);
            cmd.Parameters.AddWithValue("@tid", (object?)e.TarefaId ?? DBNull.Value);
            cmd.ExecuteNonQuery();
            e.Id = (int)cmd.LastInsertedId;
        }

        public EmailMensagem? ObterPorId(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM emails WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var rdr = cmd.ExecuteReader();
            return rdr.Read() ? Mapear(rdr) : null;
        }

        public IEnumerable<EmailMensagem> ObterTodos()
        {
            var lista = new List<EmailMensagem>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("SELECT * FROM emails ORDER BY data_envio DESC", con);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(Mapear(rdr));
            return lista;
        }

        public void Actualizar(EmailMensagem e)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"UPDATE emails SET destinatario=@dest, assunto=@ass, corpo=@corpo,
                remetente=@rem, enviado=@env, tarefa_id=@tid WHERE id=@id";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@dest", e.Destinatario);
            cmd.Parameters.AddWithValue("@ass", e.Assunto);
            cmd.Parameters.AddWithValue("@corpo", e.Corpo);
            cmd.Parameters.AddWithValue("@rem", e.Remetente);
            cmd.Parameters.AddWithValue("@env", e.Enviado ? 1 : 0);
            cmd.Parameters.AddWithValue("@tid", (object?)e.TarefaId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", e.Id);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand("DELETE FROM emails WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<EmailMensagem> Buscar(string termo)
        {
            var lista = new List<EmailMensagem>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            const string sql = @"SELECT * FROM emails
                WHERE destinatario LIKE @t OR assunto LIKE @t OR corpo LIKE @t
                ORDER BY data_envio DESC";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@t", $"%{termo.Trim()}%");
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(Mapear(rdr));
            return lista;
        }

        public IEnumerable<EmailMensagem> ListarPorDestinatario(string email)
        {
            var lista = new List<EmailMensagem>();
            using var con = ConexaoBD.ObterLigacao();
            con.Open();
            using var cmd = new MySqlCommand(
                "SELECT * FROM emails WHERE destinatario=@dest ORDER BY data_envio DESC", con);
            cmd.Parameters.AddWithValue("@dest", email);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) lista.Add(Mapear(rdr));
            return lista;
        }

        private static EmailMensagem Mapear(MySqlDataReader r) => new EmailMensagem
        {
            Id = r.GetInt32("id"),
            Destinatario = r.GetString("destinatario"),
            Assunto = r.GetString("assunto"),
            Corpo = r.GetString("corpo"),
            Remetente = r.GetString("remetente"),
            DataEnvio = r.GetDateTime("data_envio"),
            Enviado = r.GetBoolean("enviado"),
            TarefaId = r.IsDBNull(r.GetOrdinal("tarefa_id")) ? null : r.GetInt32("tarefa_id")
        };
    }
}
