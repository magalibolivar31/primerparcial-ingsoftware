using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmCatalogo : Form
    {
        private readonly BLL.CatalogoBLL _bll        = new BLL.CatalogoBLL();
        private readonly BLL.SubastaBLL  _bllSubasta = new BLL.SubastaBLL();

        private List<FilaCatalogo> _filas = new List<FilaCatalogo>();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);
        private const int EM_SETCUEBANNER = 0x1501;

        public FrmCatalogo()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InicializarFiltros();
            CargarGrilla();
        }

        // ── Inicialización ────────────────────────────────────────────────────

        private void InicializarFiltros()
        {
            cmbFiltroEstado.Items.AddRange(new object[] {
                "(Todos)", "Disponible", "En Subasta", "Adjudicado", "Retirado"
            });
            cmbFiltroEstado.SelectedIndex = 0;

            cmbFiltroTipo.Items.AddRange(new object[] { "(Todos)", "Artículo", "Lote" });
            cmbFiltroTipo.SelectedIndex = 0;

            cmbFiltroEstado.SelectedIndexChanged += (s, e) => AplicarFiltros();
            cmbFiltroTipo.SelectedIndexChanged   += (s, e) => AplicarFiltros();
            txtBusqueda.TextChanged              += (s, e) => AplicarFiltros();

            SendMessage(txtBusqueda.Handle, EM_SETCUEBANNER, (IntPtr)1, "nombre, tipo…");
        }

        // ── Carga de datos ────────────────────────────────────────────────────

        private void CargarGrilla()
        {
            _filas.Clear();

            try
            {
                // ObtenerTodosConBaja incluye items con Activo = 0 (Retirados)
                var todas    = _bll.ObtenerTodosConBaja();
                var subastas = _bllSubasta.ObtenerTodas();

                var subastaPorUnidad = new Dictionary<int, BE.Subasta>();
                foreach (var s in subastas)
                {
                    if (s.Estado == BE.EstadoSubasta.Activa)
                        subastaPorUnidad[s.IdUnidad] = s;
                    else if (s.Estado == BE.EstadoSubasta.Cerrada && s.IdGanador.HasValue
                             && !subastaPorUnidad.ContainsKey(s.IdUnidad))
                        subastaPorUnidad[s.IdUnidad] = s;
                }

                foreach (var u in todas)
                {
                    string tipo       = u is BE.Lote ? "Lote" : "Artículo";
                    string precioBase = u is BE.ArticuloIndividual art
                        ? $"$ {art.ValorDeclarado:N2}"
                        : $"$ {u.PrecioBase:N2}";

                    string precioVigente;
                    string estado;

                    if (!u.Activo)
                    {
                        estado        = "Retirado";
                        precioVigente = "—";
                    }
                    else if (subastaPorUnidad.TryGetValue(u.Id, out BE.Subasta sub))
                    {
                        if (sub.Estado == BE.EstadoSubasta.Activa)
                        {
                            estado        = "En Subasta";
                            precioVigente = $"$ {sub.PrecioVigente:N2}";
                        }
                        else
                        {
                            estado        = "Adjudicado";
                            precioVigente = sub.PrecioFinal.HasValue
                                ? $"$ {sub.PrecioFinal.Value:N2}"
                                : "—";
                        }
                    }
                    else
                    {
                        estado        = "Disponible";
                        precioVigente = "—";
                    }

                    string cantItems = u is BE.Lote
                        ? todas.FindAll(x => x.IdLotePadre == u.Id).Count.ToString()
                        : "—";

                    _filas.Add(new FilaCatalogo
                    {
                        Unidad        = u,
                        Tipo          = tipo,
                        Nombre        = u.Nombre,
                        PrecioBase    = precioBase,
                        PrecioVigente = precioVigente,
                        CantItems     = cantItems,
                        Estado        = estado
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar catálogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            AplicarFiltros();
        }

        // ── Filtrado ──────────────────────────────────────────────────────────

        private void AplicarFiltros()
        {
            string filtroEstado = cmbFiltroEstado.SelectedIndex > 0
                ? cmbFiltroEstado.SelectedItem?.ToString() : null;
            string filtroTipo = cmbFiltroTipo.SelectedIndex > 0
                ? cmbFiltroTipo.SelectedItem?.ToString() : null;
            string busqueda = txtBusqueda.Text.Trim().ToLowerInvariant();

            dgv.Rows.Clear();

            foreach (var f in _filas)
            {
                if (filtroEstado != null && f.Estado != filtroEstado) continue;
                if (filtroTipo   != null && f.Tipo   != filtroTipo)   continue;
                if (busqueda.Length > 0  &&
                    !f.Nombre.ToLowerInvariant().Contains(busqueda)   &&
                    !f.Tipo.ToLowerInvariant().Contains(busqueda))     continue;

                int idx = dgv.Rows.Add(f.Tipo, f.Nombre, f.PrecioBase,
                                       f.PrecioVigente, f.CantItems, f.Estado);
                dgv.Rows[idx].Tag = f.Unidad;

                var color = f.Estado == "En Subasta" ? System.Drawing.Color.FromArgb(255, 248, 220)
                          : f.Estado == "Adjudicado" ? System.Drawing.Color.FromArgb(220, 240, 220)
                          : f.Estado == "Retirado"   ? System.Drawing.Color.FromArgb(240, 220, 220)
                          : System.Drawing.Color.Empty;

                if (color != System.Drawing.Color.Empty)
                    dgv.Rows[idx].DefaultCellStyle.BackColor = color;
            }

            lblContador.Text = $"{dgv.Rows.Count} registro(s)";
            ActualizarBotones();
        }

        // ── Estado de botones ──────────────────────────────────────────────────

        private void ActualizarBotones()
        {
            bool haySeleccion = dgv.SelectedRows.Count > 0;

            btnVerDetalle.Enabled = haySeleccion;

            if (!haySeleccion)
            {
                btnBaja.Enabled       = false;
                btnModificar.Enabled  = false;
                btnIniciarSubasta.Visible = false;
                btnVerEnBitacora.Visible  = false;
                return;
            }

            string estado = dgv.SelectedRows[0].Cells["colEstado"].Value?.ToString() ?? "";

            // Solo se puede retirar y modificar cuando está Disponible
            btnBaja.Enabled      = (estado == "Disponible");
            btnModificar.Enabled = (estado == "Disponible");

            btnIniciarSubasta.Visible = (estado == "Disponible");
            btnVerEnBitacora.Visible  = (estado == "En Subasta" || estado == "Adjudicado");
        }

        // ── Eventos de selección y filtros ────────────────────────────────────

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cmbFiltroEstado.SelectedIndex = 0;
            cmbFiltroTipo.SelectedIndex   = 0;
            txtBusqueda.Clear();
        }

        // ── Handlers de la barra de herramientas ──────────────────────────────

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            if (!(dgv.SelectedRows[0].Tag is BE.UnidadDeVenta u)) return;
            try
            {
                BE.UnidadDeVenta arbol = _bll.ObtenerArbol(u.Id);
                FrmDetalle.Mostrar(this, arbol);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            var tag = dgv.SelectedRows[0].Tag;

            if (tag is BE.ArticuloIndividual art)
            {
                using (var frm = new FrmNuevoArticulo(art))
                    if (frm.ShowDialog(this) == DialogResult.OK) CargarGrilla();
            }
            else if (tag is BE.Lote lote)
            {
                using (var frm = new FrmNuevoLote(lote))
                    if (frm.ShowDialog(this) == DialogResult.OK) CargarGrilla();
            }
        }

        private void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmNuevoArticulo())
                if (frm.ShowDialog(this) == DialogResult.OK) CargarGrilla();
        }

        private void btnNuevoLote_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmNuevoLote())
                if (frm.ShowDialog(this) == DialogResult.OK) CargarGrilla();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            if (!(dgv.SelectedRows[0].Tag is BE.UnidadDeVenta u)) return;
            if (MessageBox.Show(
                    $"¿Retirar «{u.Nombre}»?\nNo se puede retirar si tiene una subasta activa.",
                    "Confirmar retiro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                _bll.BajaUnidad(u.Id);
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIniciarSubasta_Click(object sender, EventArgs e)
        {
            AbrirHijoMdi<FrmSubastas>(refrescarAlCerrar: true);
        }

        private void btnVerEnBitacora_Click(object sender, EventArgs e)
        {
            AbrirHijoMdi<FrmBitacoraSubastas>(refrescarAlCerrar: false);
        }

        private void AbrirHijoMdi<T>(bool refrescarAlCerrar) where T : Form, new()
        {
            Form contenedor = this.MdiParent;
            if (contenedor == null) return;

            foreach (Form hijo in contenedor.MdiChildren)
            {
                if (hijo is T) { hijo.Activate(); return; }
            }

            var frm = new T { MdiParent = contenedor };
            if (refrescarAlCerrar)
                frm.FormClosed += (s, e) => CargarGrilla();
            frm.Show();
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarGrilla();

        // ── DTO interno ───────────────────────────────────────────────────────

        private class FilaCatalogo
        {
            public BE.UnidadDeVenta Unidad        { get; set; }
            public string           Tipo          { get; set; }
            public string           Nombre        { get; set; }
            public string           PrecioBase    { get; set; }
            public string           PrecioVigente { get; set; }
            public string           CantItems     { get; set; }
            public string           Estado        { get; set; }
        }
    }
}
