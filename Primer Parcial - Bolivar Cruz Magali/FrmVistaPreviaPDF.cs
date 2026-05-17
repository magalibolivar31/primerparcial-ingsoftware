using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmVistaPreviaPDF : Form
    {
        private readonly string[] _lineas;
        private PrintDocument    _doc;
        private int              _lineaActual;

        public FrmVistaPreviaPDF(string[] lineas)
        {
            InitializeComponent();
            _lineas = lineas;
            ConfigurarDocumento();
            printPreview.Document = _doc;
        }

        private void ConfigurarDocumento()
        {
            _lineaActual = 0;
            _doc = new PrintDocument();
            _doc.DocumentName = $"BitacoraSubastas_{DateTime.Today:yyyyMMdd}";
            _doc.PrintPage   += ImprimirPagina;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs ev)
        {
            using (var fuente = new System.Drawing.Font("Courier New", 7.5f))
            {
                float altLinea = fuente.GetHeight(ev.Graphics);
                float y        = ev.MarginBounds.Top;
                while (_lineaActual < _lineas.Length)
                {
                    ev.Graphics.DrawString(_lineas[_lineaActual], fuente,
                        System.Drawing.Brushes.Black, ev.MarginBounds.Left, y);
                    y += altLinea;
                    _lineaActual++;
                    if (y + altLinea > ev.MarginBounds.Bottom)
                    { ev.HasMorePages = _lineaActual < _lineas.Length; break; }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            using (var dlg = new PrintDialog { Document = _doc, UseEXDialog = true })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ConfigurarDocumento();
                    _doc.Print();
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();
    }
}
