using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Enums;
using SisGPS_por_MN.Modelos;
using SisGPS_por_MN.Servicos;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmProjectos : Form
    {
        private readonly ProjectoRepository _repo = new();
        private readonly GestorProjectos _gestor = new();

        public FrmProjectos()
        {
            InitializeComponent();
            CarregarFiltroEstado();
            CarregarProjectos();
            dgvProjectos.SelectionChanged += _dgvProjectos_SelectionChanged;
        }

        private void FrmProjectos_Load(object sender, EventArgs e) { }

        private void CarregarFiltroEstado()
        {
            cmbFiltroEstado.Items.Clear();
            cmbFiltroEstado.Items.Add("(Todos)");
            foreach (EstadoProjecto e in Enum.GetValues(typeof(EstadoProjecto)))
                cmbFiltroEstado.Items.Add(e.ToString());
            cmbFiltroEstado.SelectedIndex = 0;
        }

        private void CarregarProjectos()
        {
            dgvProjectos.DataSource = null;

            if (cmbFiltroEstado.SelectedIndex <= 0)
            {
                dgvProjectos.DataSource = _repo.ObterTodos();
            }
            else
            {
                var estadoSel = (EstadoProjecto)(cmbFiltroEstado.SelectedIndex - 1);
                var todos = _repo.ObterTodos();
                var filtrado = new System.Collections.Generic.List<Projecto>();
                foreach (var p in todos)
                    if (p.Estado == estadoSel) filtrado.Add(p);
                dgvProjectos.DataSource = filtrado;
            }
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e) =>
            CarregarProjectos();

        private void _dgvProjectos_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProjectos.CurrentRow?.DataBoundItem is not Projecto p) return;
            _gestor.ProjectoId = p.Id;
            try
            {
                double prog = _gestor.CalcularProgresso();
                lblProgresso.Text = $"Progresso: {prog}%";
                progressBar1.Value = (int)Math.Min(prog, 100);
            }
            catch { /* sem BD disponível não bloqueia UI */ }
        }

        private void dgvProjectos_SelectionChanged(object sender, EventArgs e) { }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            using var frm = new FrmCadProjecto();
            if (frm.ShowDialog() == DialogResult.OK) CarregarProjectos();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (dgvProjectos.CurrentRow?.DataBoundItem is not Projecto p) return;
            using var frm = new FrmCadProjecto(p);
            if (frm.ShowDialog() == DialogResult.OK) CarregarProjectos();
        }

        private void btnEncerrar_Click_1(object sender, EventArgs e)
        {
            if (dgvProjectos.CurrentRow?.DataBoundItem is not Projecto p) return;
            if (MessageBox.Show($"Encerrar '{p.Nome}'?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            _repo.AlterarEstado(p.Id, EstadoProjecto.Concluido);
            CarregarProjectos();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            if (dgvProjectos.CurrentRow?.DataBoundItem is not Projecto p) return;
            if (MessageBox.Show($"Cancelar '{p.Nome}'?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            _repo.AlterarEstado(p.Id, EstadoProjecto.Cancelado);
            CarregarProjectos();
        }

        private DataTable GerarTabelaExportacao()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Orcamento");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Data Inicio");
            dt.Columns.Add("Data Fim");

            var fonte = dgvProjectos.DataSource as System.Collections.Generic.List<Projecto>;
            if (fonte == null && dgvProjectos.DataSource is IEnumerable<Projecto> enumP)
                fonte = enumP.ToList();

            if (fonte != null)
            {
                foreach (var p in fonte)
                {
                    dt.Rows.Add(
                        p.Id,
                        p.Nome,
                        p.ClienteNome ?? "",
                        p.Orcamento?.ToString("F2") ?? "0.00",
                        p.Estado.ToString(),
                        p.DataInicio.ToString("dd/MM/yyyy"),
                        p.DataFim?.ToString("dd/MM/yyyy") ?? ""
                    );
                }
            }
            return dt;
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog { Filter = "CSV Files|*.csv", FileName = "projectos.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exp = new ServicoExportacao();
                        exp.ExportarCsv(GerarTabelaExportacao(), sfd.FileName);
                        MessageBox.Show("Projectos exportados em CSV.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao exportar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog { Filter = "PDF Files|*.pdf", FileName = "projectos.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exp = new ServicoExportacao();
                        exp.ExportarPdf(GerarTabelaExportacao(), "Relatório de Projectos SisGPS", sfd.FileName);
                        MessageBox.Show("Projectos exportados em PDF.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao exportar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
