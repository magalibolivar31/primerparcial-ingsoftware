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
            LimpiarDetalle();
            try
            {
                var todas = _bll.ObtenerTodos();
                var mapa  = new Dictionary<int, TreeNode>();

                foreach (var u in todas)
                {
                    string texto = u is BE.Lote
                        ? $"[Lote] {u.Nombre}  —  $ {u.PrecioBase:N2}"
                        : $" [Art] {u.Nombre}  —  $ {((BE.ArticuloIndividual)u).ValorDeclarado:N2}";
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

        private void LimpiarDetalle()
        {
            lblTipoVal.Text   = string.Empty;
            lblNombreVal.Text = string.Empty;
            lblPrecioVal.Text = string.Empty;
            lblDescVal.Text   = string.Empty;
            lblFechaVal.Text  = string.Empty;
            lblExtraVal.Text  = string.Empty;
            btnBaja.Enabled   = false;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!(e.Node?.Tag is BE.UnidadDeVenta u)) return;

            lblTipoVal.Text   = u is BE.Lote ? "Lote" : "Artículo Individual";
            lblNombreVal.Text = u.Nombre;
            lblDescVal.Text   = u.Descripcion ?? "—";
            lblFechaVal.Text  = u.FechaAlta.ToString("dd/MM/yyyy");
            btnBaja.Enabled   = true;

            if (u is BE.ArticuloIndividual a)
            {
                lblPrecioVal.Text = $"$ {a.ValorDeclarado:N2}";
                lblExtraVal.Text  = $"Categoría: {a.Categoria}  |  Estado físico: {a.EstadoFisico}  |  Ubicación: {a.Ubicacion}";
            }
            else
            {
                lblPrecioVal.Text = $"$ {u.PrecioBase:N2}  (suma de componentes)";
                lblExtraVal.Text  = $"Componentes directos: {e.Node.Nodes.Count}";
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
