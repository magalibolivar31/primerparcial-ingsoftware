using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmCatalogo : Form
    {
        private readonly BLL.CatalogoBLL _bll        = new BLL.CatalogoBLL();
        private readonly BLL.SubastaBLL  _bllSubasta = new BLL.SubastaBLL();

        public FrmCatalogo()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            dgv.Rows.Clear();
            btnBaja.Enabled      = false;
            btnVerDetalle.Enabled = false;

            try
            {
                var todas    = _bll.ObtenerTodos();
                var subastas = _bllSubasta.ObtenerTodas();

                // idUnidad → estado operativo (Activa tiene prioridad sobre Cerrada)
                var estadoPorUnidad = new Dictionary<int, string>();
                foreach (var s in subastas)
                {
                    if (s.Estado == BE.EstadoSubasta.Activa)
                        estadoPorUnidad[s.IdUnidad] = "En Subasta";
                    else if (s.Estado == BE.EstadoSubasta.Cerrada && s.IdGanador.HasValue
                             && !estadoPorUnidad.ContainsKey(s.IdUnidad))
                        estadoPorUnidad[s.IdUnidad] = "Adjudicado";
                }

                foreach (var u in todas)
                {
                    string tipo   = u is BE.Lote ? "Lote" : "Artículo";
                    string precio = u is BE.ArticuloIndividual a
                        ? $"$ {a.ValorDeclarado:N2}"
                        : $"$ {u.PrecioBase:N2}";

                    string estado = !u.Activo                           ? "Dado de Baja"
                                  : estadoPorUnidad.ContainsKey(u.Id)  ? estadoPorUnidad[u.Id]
                                  : "Disponible";

                    int idx = dgv.Rows.Add(tipo, u.Nombre, precio, estado);
                    dgv.Rows[idx].Tag = u;

                    var color = estado == "En Subasta"   ? System.Drawing.Color.FromArgb(255, 248, 220)
                              : estado == "Adjudicado"   ? System.Drawing.Color.FromArgb(220, 240, 220)
                              : estado == "Dado de Baja" ? System.Drawing.Color.FromArgb(240, 220, 220)
                              : System.Drawing.Color.Empty;
                    if (color != System.Drawing.Color.Empty)
                        dgv.Rows[idx].DefaultCellStyle.BackColor = color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar catálogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            bool haySeleccion    = dgv.SelectedRows.Count > 0;
            btnBaja.Enabled      = haySeleccion;
            btnVerDetalle.Enabled = haySeleccion;
        }

        // Demuestra el patrón Composite: recorre el árbol recursivamente
        // y muestra la descripción jerárquica completa de la unidad seleccionada.
        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (!(dgv.SelectedRows[0]?.Tag is BE.UnidadDeVenta u)) return;
            try
            {
                string descripcion = _bll.ObtenerDescripcionCompleta(u.Id);
                FrmDetalle.Mostrar(this, u.Nombre, u.ObtenerPrecioBase(), descripcion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    $"¿Dar de baja «{u.Nombre}»?\nNo se puede dar de baja si tiene una subasta activa.",
                    "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
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

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarGrilla();
    }
}
