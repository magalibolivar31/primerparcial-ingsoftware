using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    // RF-13: Reporte visual de jornada.
    // Recorre el árbol Composite (Servicios.ReporteJornada.ObtenerDatos) y muestra
    // cada unidad con su estado de adjudicación, filtros combinables y detalle de ofertas.
    public partial class FrmReporteJornada : Form
    {
        private readonly Servicios.ReporteJornada _servicio = new Servicios.ReporteJornada();
        private readonly BLL.SubastaBLL           _bll      = new BLL.SubastaBLL();

        private BE.UnidadDeVenta                 _arbol;
        private Dictionary<int, BE.Adjudicacion> _mapaAdj;
        private Dictionary<int, BE.Subasta>      _mapaSubastas;
        private readonly List<FilaJornada>        _filas = new List<FilaJornada>();

        public FrmReporteJornada()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ActualizarStatus();
            Generar();
        }

        private void btnGenerar_Click(object sender, EventArgs e) => Generar();

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cboEstado.SelectedIndex = 0;
            cboTipo.SelectedIndex   = 0;
            chkSoloRaiz.Checked     = false;
            Generar();
        }

        private void Generar()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _servicio.ObtenerDatos(out _arbol, out _mapaAdj, out _mapaSubastas);

                _filas.Clear();
                RecorrerCatalogo(_arbol, 0);

                AplicarFiltrosYMostrar();
                ActualizarTotales();
                LimpiarOfertas();
                ActualizarStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // DFS sobre el árbol Composite — construye lista plana de FilaJornada.
        private void RecorrerCatalogo(BE.UnidadDeVenta nodo, int nivel)
        {
            if (nodo == null) return;

            if (nodo.Id == -1) // nodo raíz virtual, no se muestra
            {
                var hijosRaiz = nodo.ObtenerHijos();
                if (hijosRaiz != null)
                    foreach (var hijo in hijosRaiz)
                        RecorrerCatalogo(hijo, 0);
                return;
            }

            _mapaAdj.TryGetValue(nodo.Id, out BE.Adjudicacion adj);
            _mapaSubastas.TryGetValue(nodo.Id, out BE.Subasta subasta);

            _filas.Add(new FilaJornada
            {
                Nivel         = nivel,
                Unidad        = nodo,
                Adj           = adj,
                Subasta       = subasta,
                EsLote        = nodo is BE.Lote,
                EstadoBadge   = adj != null ? "ADJUDICADA" : "DESIERTA",
                Adjudicatario = adj?.NombreGanador ?? "—"
            });

            var hijos = nodo.ObtenerHijos();
            if (hijos != null)
                foreach (var hijo in hijos)
                    RecorrerCatalogo(hijo, nivel + 1);
        }

        private void AplicarFiltrosYMostrar()
        {
            if (dgvCatalogo.Columns.Count == 0)
            {
                dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNivel",   HeaderText = "Niv",          Width = 35 });
                dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTipo",    HeaderText = "Tipo",         Width = 80 });
                dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDesc",    HeaderText = "Descripción",  Width = 300 });
                dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colBase", HeaderText = "P. Base", Width = 95,
                    DefaultCellStyle = new DataGridViewCellStyle
                        { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" }
                });
                dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colFinal", HeaderText = "P. Final", Width = 95,
                    DefaultCellStyle = new DataGridViewCellStyle
                        { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" }
                });
                dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado",  HeaderText = "Estado",        Width = 100 });
                dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGanador", HeaderText = "Adjudicatario", Width = 180 });
                dgvCatalogo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvCatalogo.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                dgvCatalogo.RowHeadersVisible   = false;
            }

            dgvCatalogo.Rows.Clear();

            string filtroEstado = cboEstado.SelectedIndex == 1 ? "ADJUDICADA" :
                                  cboEstado.SelectedIndex == 2 ? "DESIERTA"   : null;
            bool soloLotes     = cboTipo.SelectedIndex == 1;
            bool soloArticulos = cboTipo.SelectedIndex == 2;

            var boldFont = new Font(dgvCatalogo.Font, FontStyle.Bold);

            foreach (var fila in _filas)
            {
                if (chkSoloRaiz.Checked && fila.Nivel != 0) continue;
                if (filtroEstado != null && fila.EstadoBadge != filtroEstado) continue;
                if (soloLotes     && !fila.EsLote) continue;
                if (soloArticulos &&  fila.EsLote) continue;

                string sangria = new string(' ', fila.Nivel * 4);
                string desc    = $"{sangria}{fila.Unidad.Nombre}";

                int idx = dgvCatalogo.Rows.Add(
                    fila.Nivel,
                    fila.EsLote ? "LOTE" : "ARTÍCULO",
                    desc,
                    fila.Unidad.ObtenerPrecioBase(),
                    fila.Adj != null ? (object)fila.Adj.PrecioFinal : (object)string.Empty,
                    fila.EstadoBadge,
                    fila.Adjudicatario
                );

                var row = dgvCatalogo.Rows[idx];
                row.Tag = fila;

                if (fila.EstadoBadge == "ADJUDICADA")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 250, 220);
                    row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242);
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }

                if (fila.EsLote)
                    row.DefaultCellStyle.Font = boldFont;
            }

            lblConteo.Text = $"{dgvCatalogo.Rows.Count} elemento(s) mostrado(s)";
        }

        private void ActualizarTotales()
        {
            int     total       = _filas.Count;
            int     adjudicados = 0;
            decimal monto       = 0;

            foreach (var f in _filas)
                if (f.Adj != null) { adjudicados++; monto += f.Adj.PrecioFinal; }

            lblTotalItems.Text     = total.ToString();
            lblAdjudicados.Text    = adjudicados.ToString();
            lblDesiertos.Text      = (total - adjudicados).ToString();
            lblMontoRecaudado.Text = $"$ {monto:N2}";
        }

        private void dgvCatalogo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCatalogo.CurrentRow?.Tag is FilaJornada fila && fila.Subasta != null)
                CargarOfertas(fila);
            else
                LimpiarOfertas();
        }

        private void CargarOfertas(FilaJornada fila)
        {
            try
            {
                List<BE.Puja> pujas = _bll.ObtenerHistorialPujas(fila.Subasta.Id);

                lblSelectedTitle.Text = $"  {(fila.EsLote ? "LOTE" : "ARTÍCULO")}:  {fila.Unidad.Nombre}";
                lblSelectedSub.Text   = $"  Subasta #{fila.Subasta.Id}  ·  {fila.EstadoBadge}  ·  {pujas.Count} oferta(s)";
                grpOfertas.Text       = $"Ofertas — {fila.Unidad.Nombre}";

                dgvPujas.AutoGenerateColumns = false;
                if (dgvPujas.Columns.Count == 0)
                {
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn
                        { DataPropertyName = "NombrePostor",  HeaderText = "Ofertante",    Width = 180 });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Monto", HeaderText = "Monto", Width = 110,
                        DefaultCellStyle = new DataGridViewCellStyle
                            { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
                    });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn
                        { DataPropertyName = "Estado",        HeaderText = "Estado",       Width = 100 });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "FechaHora", HeaderText = "Fecha / Hora", Width = 150,
                        DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" }
                    });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn
                        { DataPropertyName = "MotivoRechazo", HeaderText = "Motivo",       Width = 250 });
                    dgvPujas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvPujas.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                    dgvPujas.RowHeadersVisible   = false;
                }

                dgvPujas.DataSource = null;
                dgvPujas.DataSource = pujas;

                // Resaltar la oferta ganadora en azul
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
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                            row.DefaultCellStyle.ForeColor = Color.DarkBlue;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarOfertas()
        {
            dgvPujas.DataSource   = null;
            lblSelectedTitle.Text = "  Seleccione un elemento del catálogo para ver sus ofertas";
            lblSelectedSub.Text   = string.Empty;
            grpOfertas.Text       = "Ofertas";
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
            => MessageBox.Show("Exportación a PDF disponible en versión completa.", "PDF",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void btnExportarExcel_Click(object sender, EventArgs e)
            => MessageBox.Show("Exportación a Excel disponible en versión completa.", "Excel",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void btnImprimir_Click(object sender, EventArgs e)
            => MessageBox.Show("Impresión disponible en versión completa.", "Imprimir",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void ActualizarStatus()
        {
            var u = Seguridad.SessionManager.GetInstance.Usuario;
            lblStatus.Text = $"Usuario: {u.NombreCompleto}  |  Rol: {u.Rol}  |  Jornada: {DateTime.Today:dd/MM/yyyy}  |  Generado: {DateTime.Now:HH:mm}";
        }

        private class FilaJornada
        {
            public int              Nivel         { get; set; }
            public BE.UnidadDeVenta Unidad        { get; set; }
            public BE.Adjudicacion  Adj           { get; set; }
            public BE.Subasta       Subasta       { get; set; }
            public bool             EsLote        { get; set; }
            public string           EstadoBadge   { get; set; }
            public string           Adjudicatario { get; set; }
        }
    }
}
