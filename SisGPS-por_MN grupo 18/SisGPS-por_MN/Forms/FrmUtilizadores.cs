using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Servicos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmUtilizadores : Form
    {
        private readonly UtilizadorRepository _userRepo = new();
        private readonly ServicoExportacao _exportServ = new();
        private List<Utilizador> _utilizadores = new();

        public FrmUtilizadores()
        {
            InitializeComponent();
            CarregarFiltros();
            ListarUtilizadores();
        }

        private void CarregarFiltros()
        {
            cmbNivelFiltro.Items.Clear();
            cmbNivelFiltro.Items.Add("(Todos)");
            foreach (NivelAcesso n in Enum.GetValues(typeof(NivelAcesso)))
                cmbNivelFiltro.Items.Add(n);
            cmbNivelFiltro.SelectedIndex = 0;

            cmbEstadoFiltro.Items.Clear();
            cmbEstadoFiltro.Items.Add("(Todos)");
            cmbEstadoFiltro.Items.Add("Activos");
            cmbEstadoFiltro.Items.Add("Inactivos");
            cmbEstadoFiltro.SelectedIndex = 0;
        }

        private void ListarUtilizadores()
        {
            try
            {
                string busca = txtBusca.Text.Trim();
                
                if (!string.IsNullOrEmpty(busca))
                    _utilizadores = _userRepo.Buscar(busca).ToList();
                else
                    _utilizadores = _userRepo.ObterTodos().ToList();

                // Filtrar Nível
                if (cmbNivelFiltro.SelectedIndex > 0 && cmbNivelFiltro.SelectedItem is NivelAcesso nivelSel)
                {
                    _utilizadores = _utilizadores.Where(u => u.NivelAcesso == nivelSel).ToList();
                }

                // Filtrar Estado
                if (cmbEstadoFiltro.SelectedIndex == 1) // Activos
                    _utilizadores = _utilizadores.Where(u => u.Activo).ToList();
                else if (cmbEstadoFiltro.SelectedIndex == 2) // Inactivos
                    _utilizadores = _utilizadores.Where(u => !u.Activo).ToList();

                dgvUtilizadores.DataSource = null;
                dgvUtilizadores.DataSource = _utilizadores;

                // Format Columns
                var colHash = dgvUtilizadores.Columns["PasswordHash"];
                if (colHash != null) colHash.Visible = false;

                var colPerg = dgvUtilizadores.Columns["PerguntaSeguranca"];
                if (colPerg != null) colPerg.Visible = false;

                var colResp = dgvUtilizadores.Columns["RespostaSeguranca"];
                if (colResp != null) colResp.Visible = false;

                lblInfo.Text = $"{_utilizadores.Count} utilizador(es) encontrado(s).";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar utilizadores: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            ListarUtilizadores();
        }

        private Utilizador? ObterSeleccionado()
        {
            if (dgvUtilizadores.CurrentRow?.DataBoundItem is Utilizador u)
                return u;
            return null;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using (var reg = new FrmRegistoUtilizador())
            {
                if (reg.ShowDialog() == DialogResult.OK)
                    ListarUtilizadores();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var u = ObterSeleccionado();
            if (u == null)
            {
                MessageBox.Show("Seleccione um utilizador na lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var reg = new FrmRegistoUtilizador(u))
            {
                if (reg.ShowDialog() == DialogResult.OK)
                    ListarUtilizadores();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var u = ObterSeleccionado();
            if (u == null)
            {
                MessageBox.Show("Seleccione um utilizador na lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Não deixar o admin se eliminar a si mesmo
            if (Utils.Sessao.UtilizadorActual != null && Utils.Sessao.UtilizadorActual.Id == u.Id)
            {
                MessageBox.Show("Não pode eliminar a sua própria conta.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Deseja mesmo eliminar o utilizador '{u.Nome}'?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _userRepo.Eliminar(u.Id);
                    ListarUtilizadores();
                    MessageBox.Show("Utilizador eliminado com sucesso.", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao eliminar: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEstadoToggle_Click(object sender, EventArgs e)
        {
            var u = ObterSeleccionado();
            if (u == null)
            {
                MessageBox.Show("Seleccione um utilizador na lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Utils.Sessao.UtilizadorActual != null && Utils.Sessao.UtilizadorActual.Id == u.Id)
            {
                MessageBox.Show("Não pode desactivar a sua própria conta.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                u.Activo = !u.Activo;
                _userRepo.Actualizar(u);
                ListarUtilizadores();
                string acao = u.Activo ? "activado" : "desactivado";
                MessageBox.Show($"Utilizador {acao} com sucesso.", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao alterar estado: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GerarTabelaExportacao()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Username");
            dt.Columns.Add("Nivel");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Ultimo Acesso");

            foreach (var u in _utilizadores)
            {
                dt.Rows.Add(
                    u.Id,
                    u.Nome,
                    u.Username,
                    u.NivelAcesso.ToString(),
                    u.Activo ? "Activo" : "Inactivo",
                    u.UltimoAcesso.HasValue ? u.UltimoAcesso.Value.ToString("dd/MM/yyyy HH:mm") : "Nunca"
                );
            }
            return dt;
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog { Filter = "CSV Files|*.csv", FileName = "utilizadores.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var dt = GerarTabelaExportacao();
                        _exportServ.ExportarCsv(dt, sfd.FileName);
                        MessageBox.Show("Ficheiro CSV exportado com sucesso.", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao exportar CSV: {ex.Message}", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog { Filter = "PDF Files|*.pdf", FileName = "utilizadores.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var dt = GerarTabelaExportacao();
                        _exportServ.ExportarPdf(dt, "Relatório de Utilizadores do Sistema", sfd.FileName);
                        MessageBox.Show("Ficheiro PDF exportado com sucesso.", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao exportar PDF: {ex.Message}", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
