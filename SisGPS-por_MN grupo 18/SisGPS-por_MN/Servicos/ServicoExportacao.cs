using System.Data;
using System.Text;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace SisGPS_por_MN.Servicos
{
    public class ServicoExportacao
    {
        public void ExportarCsv(DataTable dt, string caminho)
        {
            using var writer = new StreamWriter(caminho, false, new UTF8Encoding(true));
            var cols = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
            writer.WriteLine(string.Join(";", cols.Select(EscaparCsv)));

            foreach (DataRow row in dt.Rows)
            {
                var vals = dt.Columns.Cast<DataColumn>()
                    .Select(c => EscaparCsv(row[c]?.ToString() ?? string.Empty));
                writer.WriteLine(string.Join(";", vals));
            }
        }

        public void ExportarPdf(DataTable dt, string titulo, string caminho)
        {
            var doc = new PdfDocument();
            doc.Info.Title = titulo;

            var page = doc.AddPage();
            page.Width = XUnit.FromPoint(842);
            page.Height = XUnit.FromPoint(595);
            var gfx = XGraphics.FromPdfPage(page);
            var fontTitulo = new XFont("Arial", 14, XFontStyle.Bold);
            var fontHeader = new XFont("Arial", 8, XFontStyle.Bold);
            var fontCell = new XFont("Arial", 7, XFontStyle.Regular);

            double y = 30;
            gfx.DrawString(titulo, fontTitulo, XBrushes.Black, new XPoint(30, y));
            y += 25;
            gfx.DrawString($"Gerado: {DateTime.Now:dd/MM/yyyy HH:mm}", fontCell, XBrushes.Gray, new XPoint(30, y));
            y += 20;

            if (dt.Columns.Count == 0)
            {
                gfx.DrawString("Sem dados.", fontCell, XBrushes.Black, new XPoint(30, y));
            }
            else
            {
                double colWidth = (page.Width.Point - 60) / dt.Columns.Count;
                double x = 30;

                foreach (DataColumn col in dt.Columns)
                {
                    gfx.DrawRectangle(XPens.LightGray, x, y, colWidth, 16);
                    gfx.DrawString(col.ColumnName, fontHeader, XBrushes.Black,
                        new XRect(x + 2, y + 2, colWidth - 4, 14), XStringFormats.TopLeft);
                    x += colWidth;
                }
                y += 16;

                foreach (DataRow row in dt.Rows)
                {
                    if (y > page.Height.Point - 30)
                    {
                        page = doc.AddPage();
                        page.Width = XUnit.FromPoint(842);
                        page.Height = XUnit.FromPoint(595);
                        gfx = XGraphics.FromPdfPage(page);
                        y = 30;
                    }

                    x = 30;
                    foreach (DataColumn col in dt.Columns)
                    {
                        var val = row[col]?.ToString() ?? string.Empty;
                        if (val.Length > 40) val = val[..37] + "...";
                        gfx.DrawRectangle(XPens.LightGray, x, y, colWidth, 14);
                        gfx.DrawString(val, fontCell, XBrushes.Black,
                            new XRect(x + 2, y + 2, colWidth - 4, 12), XStringFormats.TopLeft);
                        x += colWidth;
                    }
                    y += 14;
                }
            }

            doc.Save(caminho);
        }

        public void ExportarMultiplosCsv(Dictionary<string, DataTable> tabelas, string pasta)
        {
            Directory.CreateDirectory(pasta);
            foreach (var par in tabelas)
            {
                var nome = SanitizarNomeFicheiro(par.Key) + ".csv";
                ExportarCsv(par.Value, Path.Combine(pasta, nome));
            }
        }

        private static string EscaparCsv(string valor)
        {
            if (valor.Contains(';') || valor.Contains('"') || valor.Contains('\n'))
                return $"\"{valor.Replace("\"", "\"\"")}\"";
            return valor;
        }

        private static string SanitizarNomeFicheiro(string nome)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                nome = nome.Replace(c, '_');
            return nome;
        }
    }
}
