using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmBitacoraSubastas : Form
    {
        private readonly BLL.SubastaBLL _bll       = new BLL.SubastaBLL();
        private readonly BLL.PostorBLL  _bllPostor = new BLL.PostorBLL();

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

        private void btnBuscar_Click(object sender, EventArgs e) => Buscar();

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
                    case 1:  estado = "ACTIVA";  break;
                    case 2:  estado = "CERRADA"; break;
                    default: estado = null;      break;
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
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId",       HeaderText = "ID",         Width = 45  });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre",   HeaderText = "Producto",   Width = 200 });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTipo",     HeaderText = "Tipo",       Width = 75  });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado",   HeaderText = "Estado",     Width = 75  });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBase",     HeaderText = "P. Base",    Width = 85,  DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVigente",  HeaderText = "P. Vigente", Width = 85,  DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFinal",    HeaderText = "P. Final",   Width = 85,  DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGanador",  HeaderText = "Ganador",    Width = 145 });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colApertura", HeaderText = "Apertura",   Width = 125, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCierre",   HeaderText = "Cierre",     Width = 125, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNotif",    HeaderText = "Notificados",Width = 115 });
                    dgvSubastas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvSubastas.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                }

                dgvSubastas.Rows.Clear();

                foreach (BE.Subasta s in lista)
                {
                    // TODO: Use _bll.ObtenerNotificados(s.Id) for exact count of notified subscribers
                    int notificados = 0;
                    try { notificados = _bllPostor.ObtenerSuscripcionesActivas(s.Id).Count; } catch { }

                    string notifStr = $"{notificados} suscriptor(es)";

                    int idx = dgvSubastas.Rows.Add(
                        s.Id,
                        s.NombreUnidad,
                        s.TipoUnidad,
                        s.Estado.ToString(),
                        s.PrecioInicial,
                        s.PrecioVigente,
                        s.PrecioFinal.HasValue ? (object)s.PrecioFinal.Value : (object)"",
                        s.NombreGanador ?? "",
                        s.FechaApertura,
                        s.FechaCierre.HasValue ? (object)s.FechaCierre.Value : (object)"",
                        notifStr
                    );

                    DataGridViewRow row = dgvSubastas.Rows[idx];
                    row.Tag = s;

                    // CORRECCIÓN 4 — colores según estado y notificados
                    if (s.EstaCerrada)
                    {
                        if (s.IdGanador.HasValue && notificados > 0)
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(220, 240, 220);
                        else if (s.IdGanador.HasValue)
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 200);
                        else
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
                    }
                }

                lblConteo.Text = $"{lista.Count} subasta(s) encontrada(s).";
                LimpiarOfertas();
                LimpiarSuscriptores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSubastas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSubastas.CurrentRow?.Tag is BE.Subasta s)
            {
                CargarOfertas(s);
                CargarSuscriptores(s);
            }
            else
            {
                LimpiarOfertas();
                LimpiarSuscriptores();
            }
        }

        // ── Panel Ofertas ────────────────────────────────────────────────────────

        private void CargarOfertas(BE.Subasta subasta)
        {
            try
            {
                List<BE.Puja> pujas = _bll.ObtenerHistorialPujas(subasta.Id);

                // TODO: Use _bll.ObtenerNotificadosPorPuja(puja.Id) for exact per-puja count (RF-06)
                int suscActivos = 0;
                try { suscActivos = _bllPostor.ObtenerSuscripcionesActivas(subasta.Id).Count; } catch { }

                dgvPujas.AutoGenerateColumns = false;
                if (dgvPujas.Columns.Count == 0)
                {
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPostor", HeaderText = "Ofertante",      Width = 180 });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMonto",  HeaderText = "Monto",          Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado",         Width = 100 });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFecha",  HeaderText = "Fecha / Hora",   Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" } });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMotivo", HeaderText = "Motivo Rechazo", Width = 200 });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNotif",  HeaderText = "Notificados",    Width = 110 });
                    dgvPujas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvPujas.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                    dgvPujas.RowHeadersVisible   = false;
                }

                dgvPujas.Rows.Clear();

                decimal maxAceptada = 0;
                foreach (BE.Puja p in pujas)
                    if (p.Estado == BE.EstadoPuja.Aceptada && p.Monto > maxAceptada)
                        maxAceptada = p.Monto;

                foreach (BE.Puja p in pujas)
                {
                    // CORRECCIÓN 3 — para aceptadas, mostrar cantidad de suscriptores notificados
                    string notifStr = p.Estado == BE.EstadoPuja.Aceptada
                        ? suscActivos.ToString()
                        : "—";

                    int idx = dgvPujas.Rows.Add(
                        p.NombrePostor,
                        p.Monto,
                        p.Estado == BE.EstadoPuja.Aceptada ? "Aceptada" : "Rechazada",
                        p.FechaHora,
                        p.MotivoRechazo ?? "",
                        notifStr
                    );

                    DataGridViewRow row = dgvPujas.Rows[idx];

                    if (p.Estado == BE.EstadoPuja.Aceptada && p.Monto == maxAceptada)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
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
            dgvPujas.Rows.Clear();
            lblSumario.Text = "Seleccione una subasta para ver las ofertas.";
            grpOfertas.Text = "Ofertas";
        }

        // ── Panel Suscriptores (RF-05, RF-08) ────────────────────────────────────

        private void CargarSuscriptores(BE.Subasta subasta)
        {
            try
            {
                // TODO: Para trazabilidad completa, usar un método que devuelva activos + históricos
                // Actualmente solo se muestran suscripciones activas (Activa = 1)
                List<BE.Suscripcion> suscs = _bllPostor.ObtenerSuscripcionesActivas(subasta.Id);

                grpSuscriptores.Text   = $"Suscriptores — #{subasta.Id}: {subasta.NombreUnidad}";
                lblSuscSubtitulo.Text  = $"{suscs.Count} suscriptor(es) activo(s)";

                dgvSuscriptores.AutoGenerateColumns = false;
                if (dgvSuscriptores.Columns.Count == 0)
                {
                    dgvSuscriptores.Columns.Add(new DataGridViewTextBoxColumn
                        { DataPropertyName = "NombrePostor",      HeaderText = "Postor",             Width = 180 });
                    dgvSuscriptores.Columns.Add(new DataGridViewTextBoxColumn
                        { Name = "colEstadoSusc",                  HeaderText = "Estado",             Width = 100 });
                    dgvSuscriptores.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "FechaSuscripcion", HeaderText = "Suscripción", Width = 140,
                        DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
                    });
                    dgvSuscriptores.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "FechaBaja", HeaderText = "Desuscripción", Width = 140,
                        DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
                    });
                    dgvSuscriptores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvSuscriptores.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                    dgvSuscriptores.RowHeadersVisible   = false;
                }

                dgvSuscriptores.DataSource = null;
                dgvSuscriptores.DataSource = suscs;

                foreach (DataGridViewRow row in dgvSuscriptores.Rows)
                {
                    if (!(row.DataBoundItem is BE.Suscripcion s)) continue;
                    row.Cells["colEstadoSusc"].Value       = s.Activa ? "Activo" : "Desuscripto";
                    row.DefaultCellStyle.BackColor = s.Activa
                        ? System.Drawing.Color.FromArgb(220, 240, 220)
                        : System.Drawing.Color.FromArgb(225, 225, 225);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarSuscriptores()
        {
            dgvSuscriptores.DataSource = null;
            lblSuscSubtitulo.Text      = "";
            grpSuscriptores.Text       = "Suscriptores";
        }
    }
}
