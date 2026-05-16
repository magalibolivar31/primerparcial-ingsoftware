using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmCatalogo : Form
    {
        private readonly BLL.CatalogoBLL _bll = new BLL.CatalogoBLL();
        private List<BE.UnidadDeVenta> _todas;

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
                _todas = _bll.ObtenerTodos();

                // Índice nombre de lotes para columna "Lote padre"
                var nombrePorId = _todas.ToDictionary(u => u.Id, u => u.Nombre);

                foreach (var u in _todas)
                {
                    string tipo, precio, categoria, estado, ubicacion;

                    if (u is BE.ArticuloIndividual a)
                    {
                        tipo      = "Artículo";
                        precio    = $"$ {a.ValorDeclarado:N2}";
                        categoria = a.Categoria    ?? "—";
                        estado    = a.EstadoFisico ?? "—";
                        ubicacion = a.Ubicacion    ?? "—";
                    }
                    else
                    {
                        tipo      = "Lote";
                        precio    = $"$ {u.PrecioBase:N2}";
                        categoria = "—";
                        estado    = "—";
                        ubicacion = "—";
                    }

                    string lotePadre = u.IdLotePadre.HasValue && nombrePorId.ContainsKey(u.IdLotePadre.Value)
                        ? nombrePorId[u.IdLotePadre.Value]
                        : "—";

                    int idx = dgv.Rows.Add(tipo, u.Nombre, precio, categoria, estado, ubicacion,
                                           lotePadre, u.FechaAlta.ToString("dd/MM/yyyy"));
                    dgv.Rows[idx].Tag = u;
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
            if (!(dgv.SelectedRows[0]?.Tag is BE.UnidadDeVenta u)) return;
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
