using SisGPS_por_MN.Dall;
using SisGPS_por_MN.Servicos;
using System.Data;

namespace SisGPS_por_MN.Forms
{
    public partial class FrmRelatorio : Form
    {
        private readonly ProjectoRepository _projRepo = new();
        private readonly EquipaRepository _eqRepo = new();
        private readonly ServicoRelatorio _relServ = new();
        private readonly ServicoExportacao _export = new();

        private DataTable? _dtProgresso;
        private DataTable? _dtVelocidade;
        private DataTable? _dtHoras;

        public FrmRelatorio()
        {
            InitializeComponent();
            CarregarFiltros();
        }

        private void CarregarFiltros()
        {
            try
            {
                var projs = _projRepo.ObterTodos().ToList();
                var listaProj = new List<object> { new { Id = 0, Nome = "(Seleccione um Projecto)" } };
                listaProj.AddRange(projs.Cast<object>());
                cmbProjecto.DataSource = listaProj;
                cmbProjecto.DisplayMember = "Nome";
                cmbProjecto.ValueMember = "Id";

                var eqs = _eqRepo.ObterTodos().ToList();
                var listaEq = new List<object> { new { Id = 0, Nome = "(Seleccione uma Equipa)" } };
                listaEq.AddRange(eqs.Cast<object>());
                cmbEquipa.DataSource = listaEq;
                cmbEquipa.DisplayMember = "Nome";
                cmbEquipa.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar filtros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            int projId = cmbProjecto.SelectedValue is int pId ? pId : 0;
            int eqId = cmbEquipa.SelectedValue is int eId ? eId : 0;

            if (projId <= 0 && eqId <= 0)
            {
                MessageBox.Show("Seleccione um Projecto ou uma Equipa.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                lblStatus.Text = "A gerar relatórios...";
                Application.DoEvents();

                if (projId > 0)
                {
                    _dtProgresso = _relServ.RelatorioProgressoProjecto(projId);
                    _dtVelocidade = _relServ.RelatorioVelocidadeSprint(projId);
                    dgvProgresso.DataSource = _dtProgresso;
                    dgvVelocidade.DataSource = _dtVelocidade;
                }
                else
                {
                    _dtProgresso = null;
                    _dtVelocidade = null;
                    dgvProgresso.DataSource = null;
                    dgvVelocidade.DataSource = null;
                }

                if (eqId > 0)
                {
                    _dtHoras = _relServ.RelatorioHorasMembro(eqId);
                    dgvHoras.DataSource = _dtHoras;
                }
                else
                {
                    _dtHoras = null;
                    dgvHoras.DataSource = null;
                }

                lblStatus.Text = $"Relatórios gerados às {DateTime.Now:HH:mm:ss}.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Erro ao gerar relatórios.";
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            if (!TemDados()) return;
            using var dlg = new FolderBrowserDialog { Description = "Seleccione pasta para CSV" };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                var tabelas = ObterTabelasComDados();
                _export.ExportarMultiplosCsv(tabelas, dlg.SelectedPath);
                MessageBox.Show("Ficheiros CSV exportados.", "Exportação",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            if (!TemDados()) return;
            using var dlg = new SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = $"relatorio_sisgps_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                var tabelas = ObterTabelasComDados();
                var primeira = tabelas.First();
                _export.ExportarPdf(primeira.Value, primeira.Key, dlg.FileName);

                if (tabelas.Count > 1)
                {
                    var pasta = Path.GetDirectoryName(dlg.FileName)!;
                    var baseNome = Path.GetFileNameWithoutExtension(dlg.FileName);
                    int i = 1;
                    foreach (var par in tabelas.Skip(1))
                    {
                        _export.ExportarPdf(par.Value, par.Key,
                            Path.Combine(pasta, $"{baseNome}_{i++}.pdf"));
                    }
                }

                MessageBox.Show("PDF exportado com sucesso.", "Exportação",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TemDados()
        {
            if (_dtProgresso == null && _dtVelocidade == null && _dtHoras == null)
            {
                MessageBox.Show("Gere os relatórios primeiro.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private Dictionary<string, DataTable> ObterTabelasComDados()
        {
            var d = new Dictionary<string, DataTable>();
            if (_dtProgresso?.Rows.Count > 0) d["Progresso_Projecto"] = _dtProgresso;
            if (_dtVelocidade?.Rows.Count > 0) d["Velocidade_Sprint"] = _dtVelocidade;
            if (_dtHoras?.Rows.Count > 0) d["Horas_Membro"] = _dtHoras;
            if (d.Count == 0)
                throw new InvalidOperationException("Não há dados para exportar.");
            return d;
        }
    }
}
