using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmBitacoraSubastas : Form
    {
        private readonly BLL.SubastaBLL _bll = new BLL.SubastaBLL();

        public FrmBitacoraSubastas()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
            Buscar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)  => Buscar();

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpDesde.Value           = DateTime.Today.AddMonths(-1);
            dtpHasta.Value           = DateTime.Today;
            cboEstado.SelectedIndex  = 0;
            cboTipo.SelectedIndex    = 0;
            txtProducto.Clear();
            nudIdProducto.Value      = 0;
            txtOfertante.Clear();
            nudIdOfertante.Value     = 0;
            nudMontoMin.Value        = 0;
            nudMontoMax.Value        = 0;
            chkSoloGanadores.Checked = false;
            Buscar();
        }

        private void Buscar()
        {
            try
            {
                string estado;
                switch (cboEstado.SelectedIndex)
                {
                    case 1:  estado = "ACTIVA";   break;
                    case 2:  estado = "CERRADA";  break;
                    default: estado = null;       break;
                }

                string tipo;
                switch (cboTipo.SelectedIndex)
                {
                    case 1:  tipo = "ARTICULO"; break;
                    case 2:  tipo = "LOTE";     break;
                    default: tipo = null;       break;
                }

                int?     idUnidad = nudIdProducto.Value  > 0 ? (int?)(int)nudIdProducto.Value  : null;
                int?     idPostor = nudIdOfertante.Value > 0 ? (int?)(int)nudIdOfertante.Value : null;
                decimal? montoMin = nudMontoMin.Value    > 0 ? (decimal?)nudMontoMin.Value     : null;
                decimal? montoMax = nudMontoMax.Value    > 0 ? (decimal?)nudMontoMax.Value     : null;

                List<BE.Subasta> lista = _bll.ObtenerBitacora(
                    dtpDesde.Value.Date,
                    dtpHasta.Value.Date,
                    estado,
                    idUnidad, txtProducto.Text.Trim(),
                    tipo,
                    idPostor, txtOfertante.Text.Trim(),
                    chkSoloGanadores.Checked,
                    montoMin, montoMax);

                dgvSubastas.AutoGenerateColumns = false;
                if (dgvSubastas.Columns.Count == 0)
                {
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id",               HeaderText = "ID",         Width = 45  });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreUnidad",     HeaderText = "Producto",   Width = 210 });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TipoUnidad",       HeaderText = "Tipo",       Width = 75  });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado",           HeaderText = "Estado",     Width = 75  });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioInicial",    HeaderText = "P. Base",    Width = 90,  DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioVigente",    HeaderText = "P. Vigente", Width = 90,  DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioFinal",      HeaderText = "P. Final",   Width = 90,  DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreGanador",    HeaderText = "Ganador",    Width = 150 });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaApertura",    HeaderText = "Apertura",   Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaCierre",      HeaderText = "Cierre",     Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
                    dgvSubastas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvSubastas.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                }

                dgvSubastas.DataSource = null;
                dgvSubastas.DataSource = lista;
                lblConteo.Text         = $"{lista.Count} subasta(s) encontrada(s).";
                LimpiarOfertas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSubastas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSubastas.CurrentRow?.DataBoundItem is BE.Subasta s)
                CargarOfertas(s);
            else
                LimpiarOfertas();
        }

        private void CargarOfertas(BE.Subasta subasta)
        {
            try
            {
                List<BE.Puja> pujas = _bll.ObtenerHistorialPujas(subasta.Id);

                dgvPujas.AutoGenerateColumns = false;
                if (dgvPujas.Columns.Count == 0)
                {
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombrePostor",  HeaderText = "Ofertante",      Width = 180 });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Monto",         HeaderText = "Monto",          Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado",        HeaderText = "Estado",         Width = 100 });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaHora",     HeaderText = "Fecha / Hora",   Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" } });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MotivoRechazo", HeaderText = "Motivo Rechazo", Width = 250 });
                    dgvPujas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvPujas.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                }

                dgvPujas.DataSource = null;
                dgvPujas.DataSource = pujas;

                decimal maxAceptada = 0;
                foreach (BE.Puja p in pujas)
                    if (p.Estado == BE.EstadoPuja.Aceptada && p.Monto > maxAceptada)
                        maxAceptada = p.Monto;

                if (maxAceptada > 0)
                {
                    foreach (DataGridViewRow row in dgvPujas.Rows)
                    {
                        if (row.DataBoundItem is BE.Puja p &&
                            p.Estado == BE.EstadoPuja.Aceptada &&
                            p.Monto  == maxAceptada)
                        {
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                            break;
                        }
                    }
                }

                var postoresUnicos = new HashSet<int>();
                int aceptadas = 0;
                foreach (BE.Puja p in pujas)
                {
                    postoresUnicos.Add(p.IdPostor);
                    if (p.Estado == BE.EstadoPuja.Aceptada) aceptadas++;
                }

                string ganador = subasta.NombreGanador != null
                    ? $"Ganador: {subasta.NombreGanador} — $ {subasta.PrecioFinal:N2}"
                    : subasta.EstaActiva ? "Subasta en curso" : "Desierta (sin ganador)";

                lblSumario.Text = $"{pujas.Count} oferta(s)  ·  {aceptadas} aceptada(s)  ·  {postoresUnicos.Count} ofertante(s)  ·  {ganador}";
                grpOfertas.Text = $"Ofertas — #{subasta.Id}: {subasta.NombreUnidad}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarOfertas()
        {
            dgvPujas.DataSource = null;
            lblSumario.Text     = "Seleccione una subasta para ver las ofertas.";
            grpOfertas.Text     = "Ofertas";
        }
    }
}
