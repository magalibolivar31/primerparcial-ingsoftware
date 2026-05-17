using System;
using System.Windows.Forms;

namespace GUI
{
    // RF-13: Reporte de jornada.
    // Llama a Servicios.ReporteJornada.GenerarReporte() que recorre el arbol
    // Composite de forma recursiva y lista todas las unidades con su resultado.
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
            Generar();
        }

        private void btnGenerar_Click(object sender, EventArgs e) => Generar();

        private void Generar()
        {
            try
            {
                txtReporte.Text = _reporte.GenerarReporte(dtpFecha.Value.Date);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
