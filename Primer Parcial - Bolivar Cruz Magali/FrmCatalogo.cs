using System;
using System.Collections.Generic;
using System.Linq;
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
            btnBaja.Enabled = false;

            try
            {
                var todas     = _bll.ObtenerTodos();
                var subastas  = _bllSubasta.ObtenerTodas();

                // idUnidad → estado operativo
                var estadoPorUnidad = new Dictionary<int, string>();
                foreach (var s in subastas)
                {
                    string est = s.Estado == BE.EstadoSubasta.Activa    ? "En Subasta"
                               : s.Estado == BE.EstadoSubasta.Cerrada && s.IdGanador.HasValue ? "Adjudicado"
                               : null;
                    if (est != null && !estadoPorUnidad.ContainsKey(s.IdUnidad))
                        estadoPorUnidad[s.IdUnidad] = est;
                    else if (est == "En Subasta") // activa tiene prioridad
                        estadoPorUnidad[s.IdUnidad] = est;
                }

                var nombrePorId = todas.ToDictionary(u => u.Id, u => u.Nombre);

                foreach (var u in todas)
                {
                    string tipo, precio, categoria, estadoFis, ubicacion;

                    if (u is BE.ArticuloIndividual a)
                    {
                        tipo      = "Artículo";
                        precio    = $"$ {a.ValorDeclarado:N2}";
                        categoria = a.Categoria    ?? "—";
                        estadoFis = a.EstadoFisico ?? "—";
                        ubicacion = a.Ubicacion    ?? "—";
                    }
                    else
                    {
                        tipo      = "Lote";
                        precio    = $"$ {u.PrecioBase:N2}";
                        categoria = "—";
                        estadoFis = "—";
                        ubicacion = "—";
                    }

                    string estadoOp = !u.Activo ? "Dado de Baja"
                                    : estadoPorUnidad.ContainsKey(u.Id) ? estadoPorUnidad[u.Id]
                                    : "Disponible";

                    string lotePadre = u.IdLotePadre.HasValue && nombrePorId.ContainsKey(u.IdLotePadre.Value)
                        ? nombrePorId[u.IdLotePadre.Value]
                        : "—";

                    int idx = dgv.Rows.Add(tipo, u.Nombre, precio, estadoOp, categoria, estadoFis,
                                           ubicacion, lotePadre, u.FechaAlta.ToString("dd/MM/yyyy"));
                    dgv.Rows[idx].Tag = u;

                    // Colorear fila según estado operativo
                    var color = estadoOp == "En Subasta"   ? System.Drawing.Color.FromArgb(255, 248, 220)
                              : estadoOp == "Adjudicado"   ? System.Drawing.Color.FromArgb(220, 240, 220)
                              : estadoOp == "Dado de Baja" ? System.Drawing.Color.FromArgb(240, 220, 220)
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
            btnBaja.Enabled = dgv.SelectedRows.Count > 0;
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
