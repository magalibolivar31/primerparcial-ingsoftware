using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmReporteJornada : Form
    {
        private readonly Servicios.ReporteJornada _reporte = new Servicios.ReporteJornada();

        public FrmReporteJornada()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dtpFecha.Value = DateTime.Today;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                btnGenerar.Enabled = false;
                rtbReporte.Text    = "Generando reporte...";
                string texto = _reporte.GenerarReporte(dtpFecha.Value.Date);
                rtbReporte.Text = texto;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al generar reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtbReporte.Text = string.Empty;
            }
            finally
            {
                btnGenerar.Enabled = true;
            }
        }
    }
}
