using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmCatalogo : Form
    {
        private readonly BLL.CatalogoBLL _bll = new BLL.CatalogoBLL();

        public FrmCatalogo()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarArbol();
        }

        private void CargarArbol()
        {
            treeView.Nodes.Clear();
            dgvDetalle.Rows.Clear();
            btnBaja.Enabled = false;

            try
            {
                var todas = _bll.ObtenerTodos();
                var mapa  = new Dictionary<int, TreeNode>();

                foreach (var u in todas)
                {
                    string texto = u is BE.Lote
                        ? $"[Lote] {u.Nombre}"
                        : $" [Art] {u.Nombre}";
                    mapa[u.Id] = new TreeNode(texto) { Tag = u };
                }

                foreach (var u in todas)
                {
                    var nodo = mapa[u.Id];
                    if (u.IdLotePadre.HasValue && mapa.ContainsKey(u.IdLotePadre.Value))
                        mapa[u.IdLotePadre.Value].Nodes.Add(nodo);
                    else
                        treeView.Nodes.Add(nodo);
                }

                treeView.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar catálogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!(e.Node?.Tag is BE.UnidadDeVenta seleccionado)) return;

            btnBaja.Enabled = true;

            if (seleccionado is BE.Lote)
            {
                // Mostrar hijos directos del lote en la grilla
                var hijos = new List<BE.UnidadDeVenta>();
                foreach (TreeNode hijo in e.Node.Nodes)
                    if (hijo.Tag is BE.UnidadDeVenta u) hijos.Add(u);

                MostrarEnGrilla(hijos, $"Contenido de lote: {seleccionado.Nombre}");
            }
            else
            {
                // Mostrar el artículo individual solo
                MostrarEnGrilla(new[] { seleccionado }, null);
            }
        }

        private void MostrarEnGrilla(IEnumerable<BE.UnidadDeVenta> items, string titulo)
        {
            dgvDetalle.Rows.Clear();

            if (titulo != null)
                this.Text = $"Catálogo — {titulo}";
            else
                this.Text = "Catálogo de Unidades de Venta";

            foreach (var u in items)
            {
                string tipo, precio, categoria, estado, ubicacion;

                if (u is BE.ArticuloIndividual a)
                {
                    tipo      = "Artículo";
                    precio    = $"$ {a.ValorDeclarado:N2}";
                    categoria = a.Categoria   ?? "—";
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

                dgvDetalle.Rows.Add(tipo, u.Nombre, precio, categoria, estado, ubicacion,
                                    u.FechaAlta.ToString("dd/MM/yyyy"));
            }
        }

        private void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmNuevoArticulo())
                if (frm.ShowDialog(this) == DialogResult.OK) CargarArbol();
        }

        private void btnNuevoLote_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmNuevoLote())
                if (frm.ShowDialog(this) == DialogResult.OK) CargarArbol();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (!(treeView.SelectedNode?.Tag is BE.UnidadDeVenta u)) return;
            if (MessageBox.Show(
                    $"¿Dar de baja «{u.Nombre}»?\nNo se puede dar de baja si tiene una subasta activa.",
                    "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                _bll.BajaUnidad(u.Id);
                CargarArbol();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarArbol();
    }
}
