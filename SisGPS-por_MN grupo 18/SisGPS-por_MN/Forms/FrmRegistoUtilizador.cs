using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmRegistoUtilizador : Form
    {
        private readonly MembroRepository _membroRepo = new();
        private readonly UtilizadorRepository _userRepo = new();
        private readonly Utilizador? _edicaoUser = null;

        public FrmRegistoUtilizador()
        {
            InitializeComponent();
            CarregarMembros();
            CarregarNiveisAcesso();
        }

        public FrmRegistoUtilizador(Utilizador u) : this()
        {
            _edicaoUser = u;
            Text = "Editar Utilizador — SisGPS";
            lblTitulo.Text = "Editar Utilizador";
            
            txtNome.Text = u.Nome;
            txtUsername.Text = u.Username;
            txtUsername.Enabled = false; // Não permitir alterar username
            txtPassword.Text = ""; // Senha fica vazia se não for alterar
            txtPergunta.Text = u.PerguntaSeguranca;
            txtResposta.Text = u.RespostaSeguranca;

            if (u.MembroId.HasValue)
                cmbMembro.SelectedValue = u.MembroId.Value;
            else
                cmbMembro.SelectedIndex = 0;

            cmbNivelAcesso.SelectedItem = u.NivelAcesso;
        }

        private void CarregarMembros()
        {
            var membros = _membroRepo.ObterTodos().ToList();
            var lista = new List<object> { new { Id = 0, Nome = "(Nenhum)" } };
            lista.AddRange(membros.Select(m => new { Id = m.Id, Nome = m.Nome }));
            cmbMembro.DataSource = lista;
            cmbMembro.DisplayMember = "Nome";
            cmbMembro.ValueMember = "Id";
        }

        private void CarregarNiveisAcesso()
        {
            cmbNivelAcesso.Items.Clear();
            foreach (NivelAcesso n in Enum.GetValues(typeof(NivelAcesso)))
                cmbNivelAcesso.Items.Add(n);
            cmbNivelAcesso.SelectedIndex = 1; // Default: Funcionário
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;
            string perg = txtPergunta.Text.Trim();
            string resp = txtResposta.Text.Trim();

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(user) ||
                (string.IsNullOrWhiteSpace(pass) && _edicaoUser == null) ||
                string.IsNullOrWhiteSpace(perg) || string.IsNullOrWhiteSpace(resp))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.", "Validação", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_edicaoUser == null)
                {
                    // Validar se utilizador já existe
                    var todos = _userRepo.ObterTodos();
                    if (todos.Any(x => x.Username.Equals(user, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Este nome de utilizador já está registado.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var u = new Utilizador
                    {
                        Nome = nome,
                        Username = user,
                        PasswordHash = Criptografia.HashPassword(pass),
                        PerguntaSeguranca = perg,
                        RespostaSeguranca = resp,
                        NivelAcesso = cmbNivelAcesso.SelectedItem is NivelAcesso nivel ? nivel : NivelAcesso.Funcionario,
                        MembroId = (cmbMembro.SelectedValue is int mid && mid > 0) ? mid : null,
                        Activo = true
                    };
                    _userRepo.Inserir(u);
                    MessageBox.Show("Utilizador registado com sucesso.", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _edicaoUser.Nome = nome;
                    _edicaoUser.PerguntaSeguranca = perg;
                    _edicaoUser.RespostaSeguranca = resp;
                    _edicaoUser.NivelAcesso = cmbNivelAcesso.SelectedItem is NivelAcesso nivelEdicao ? nivelEdicao : NivelAcesso.Funcionario;
                    _edicaoUser.MembroId = (cmbMembro.SelectedValue is int mid && mid > 0) ? mid : null;

                    if (!string.IsNullOrEmpty(pass))
                        _edicaoUser.PasswordHash = Criptografia.HashPassword(pass);

                    _userRepo.Actualizar(_edicaoUser);
                    MessageBox.Show("Utilizador actualizado com sucesso.", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar utilizador: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
