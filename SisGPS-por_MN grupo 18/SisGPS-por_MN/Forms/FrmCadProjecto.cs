using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmCadProjecto : Form
    {
        private readonly ProjectoRepository _repo = new();
        private readonly EquipaRepository _eqRepo = new();
        private Projecto? _projecto;

        // ── Novo projecto ─────────────────────────────────────────────────
        public FrmCadProjecto() { InitializeComponent(); CarregarCombos(); }

    

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

      

        private void FrmCadProjecto_Load(object sender, EventArgs e)
        {

        }
      

            // ── Editar projecto existente ─────────────────────────────────────
            public FrmCadProjecto(Projecto p) : this()
            {
                _projecto = p;
                txtNome.Text = p.Nome;
                txtDescricao.Text = p.Descricao;
                txtCliente.Text = p.ClienteNome;
                dtpInicio.Value = p.DataInicio;
                dtpFim.Value = p.DataFim ?? DateTime.Today;
                txtOrcamento.Text = p.Orcamento?.ToString("F2");
                cmbEstado.SelectedIndex = (int)p.Estado;
                // seleccionar equipa
                foreach (var item in cmbEquipa.Items)
                    if (item is Equipa eq && eq.Id == p.EquipaId)
                    { cmbEquipa.SelectedItem = eq; break; }
            }

            private void CarregarCombos()
            {
                // Equipas
                cmbEquipa.DataSource = _eqRepo.ObterTodos();
                cmbEquipa.DisplayMember = "Nome";
                cmbEquipa.ValueMember = "Id";

                // Estados
                cmbEstado.DataSource = Enum.GetNames(typeof(EstadoProjecto));
            }

            private void btnSalvar_Click(object sender, EventArgs e)
            {
                // Validações
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                { MessageBox.Show("O nome é obrigatório.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (cmbEquipa.SelectedItem is not Equipa eq)
                { MessageBox.Show("Seleccione uma equipa.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                try
                {
                    ValidadorNegocio.ValidarDatas(dtpInicio.Value, dtpFim.Value);

                    decimal? orc = null;
                    if (!string.IsNullOrWhiteSpace(txtOrcamento.Text))
                        orc = decimal.Parse(txtOrcamento.Text);

                    if (_projecto == null)
                    {
                        _projecto = new Projecto
                        {
                            Nome = txtNome.Text.Trim(),
                            Descricao = txtDescricao.Text,
                            ClienteNome = txtCliente.Text,
                            DataInicio = dtpInicio.Value,
                            DataFim = dtpFim.Value,
                            Orcamento = orc,
                            Estado = (EstadoProjecto)cmbEstado.SelectedIndex,
                            EquipaId = eq.Id
                        };
                        _repo.Inserir(_projecto);
                    }
                    else
                    {
                        _projecto.Nome = txtNome.Text.Trim();
                        _projecto.Descricao = txtDescricao.Text;
                        _projecto.ClienteNome = txtCliente.Text;
                        _projecto.DataInicio = dtpInicio.Value;
                        _projecto.DataFim = dtpFim.Value;
                        _projecto.Orcamento = orc;
                        _projecto.Estado = (EstadoProjecto)cmbEstado.SelectedIndex;
                        _projecto.EquipaId = eq.Id;
                        _repo.Actualizar(_projecto);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void btnCancelar_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }

