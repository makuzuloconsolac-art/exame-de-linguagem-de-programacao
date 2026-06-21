using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Utils;

namespace SisGPS_por_MN.Servicos
{
    public class ServicoEmail
    {
        private readonly EmailRepository _repo = new();

        private class ConfigSMTP
        {
            public string Host { get; set; } = "smtp.gmail.com";
            public int Port { get; set; } = 587;
            public string User { get; set; } = "sisgps@empresa.ao";
            public string Pass { get; set; } = string.Empty;
            public bool Ssl { get; set; } = true;
        }

        private ConfigSMTP CarregarConfiguracao()
        {
            var config = new ConfigSMTP();
            try
            {
                using var con = ConexaoBD.ObterLigacao();
                con.Open();
                const string sql = "SELECT * FROM configuracao_smtp LIMIT 1";
                using var cmd = new MySqlCommand(sql, con);
                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    config.Host = rdr.GetString("smtp_servidor");
                    config.Port = rdr.GetInt32("smtp_porta");
                    config.User = rdr.GetString("smtp_utilizador");
                    config.Pass = rdr.GetString("smtp_senha");
                    config.Ssl = rdr.GetBoolean("usar_ssl");
                    return config;
                }
            }
            catch
            {
                // Fallback para variáveis de ambiente se falhar a BD
            }

            config.Host = Environment.GetEnvironmentVariable("SISGPS_SMTP_HOST") ?? "smtp.gmail.com";
            config.Port = int.TryParse(Environment.GetEnvironmentVariable("SISGPS_SMTP_PORT"), out var p) ? p : 587;
            config.User = Environment.GetEnvironmentVariable("SISGPS_SMTP_USER") ?? "sisgps@empresa.ao";
            config.Pass = Environment.GetEnvironmentVariable("SISGPS_SMTP_PASS") ?? string.Empty;
            config.Ssl = true;

            return config;
        }

        public EmailMensagem Criar(EmailMensagem msg)
        {
            if (string.IsNullOrWhiteSpace(msg.Destinatario))
                throw new ArgumentException("Destinatário obrigatório.");
            if (!Validador.EmailValido(msg.Destinatario))
                throw new ArgumentException("E-mail inválido.");
            if (string.IsNullOrWhiteSpace(msg.Assunto))
                throw new ArgumentException("Assunto obrigatório.");

            msg.Remetente = string.IsNullOrWhiteSpace(msg.Remetente)
                ? CarregarConfiguracao().User
                : msg.Remetente;
            _repo.Inserir(msg);
            return msg;
        }

        public EmailMensagem? Ler(int id) => _repo.ObterPorId(id);

        public IEnumerable<EmailMensagem> Listar() => _repo.ObterTodos();

        public IEnumerable<EmailMensagem> Buscar(string termo) => _repo.Buscar(termo);

        public void Actualizar(EmailMensagem msg) => _repo.Actualizar(msg);

        public void Eliminar(int id) => _repo.Eliminar(id);

        public bool Enviar(EmailMensagem msg)
        {
            bool enviado = false;
            try
            {
                var cfg = CarregarConfiguracao();
                if (!string.IsNullOrWhiteSpace(cfg.Pass))
                {
                    using var client = new SmtpClient(cfg.Host, cfg.Port)
                    {
                        EnableSsl = cfg.Ssl,
                        Credentials = new NetworkCredential(cfg.User, cfg.Pass)
                    };
                    using var mail = new MailMessage(msg.Remetente, msg.Destinatario, msg.Assunto, msg.Corpo);
                    client.Send(mail);
                    enviado = true;
                }
            }
            catch
            {
                enviado = false;
            }

            msg.Enviado = enviado;
            if (msg.Id == 0)
                _repo.Inserir(msg);
            else
                _repo.Actualizar(msg);

            return enviado;
        }

        public bool EnviarENotificarTarefa(string destinatario, string assunto, string corpo, int? tarefaId)
        {
            var msg = new EmailMensagem
            {
                Destinatario = destinatario,
                Assunto = assunto,
                Corpo = corpo,
                TarefaId = tarefaId,
                Remetente = CarregarConfiguracao().User
            };
            return Enviar(msg);
        }
    }
}
