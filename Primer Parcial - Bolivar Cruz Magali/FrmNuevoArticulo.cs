using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmNuevoArticulo : Form
    {
        private readonly BLL.CatalogoBLL _bll = new BLL.CatalogoBLL();

        public FrmNuevoArticulo()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarLotes();
        }

        private void CargarLotes()
        {
            cboLotePadre.Items.Clear();
            cboLotePadre.Items.Add(new ItemCombo { Id = null, Texto = "(Sin lote padre)" });

            var todas = _bll.ObtenerTodos();
            foreach (var u in todas)
                if (u is BE.Lote l)
                    cboLotePadre.Items.Add(new ItemCombo { Id = l.Id, Texto = l.Nombre });

            cboLotePadre.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }
            if (nudValor.Value <= 0)
            {
                MessageBox.Show("El valor declarado debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudValor.Focus();
                return;
            }

            try
            {
                var articulo = new BE.ArticuloIndividual
                {
                    Nombre         = txtNombre.Text.Trim(),
                    Descripcion    = txtDescripcion.Text.Trim(),
                    Categoria      = txtCategoria.Text.Trim(),
                    EstadoFisico   = txtEstado.Text.Trim(),
                    Ubicacion      = txtUbicacion.Text.Trim(),
                    ValorDeclarado = nudValor.Value,
                    Activo         = true
                };

                int id = _bll.AltaArticulo(articulo);

                // Asignar al lote padre si se seleccionó uno.
                if (cboLotePadre.SelectedItem is ItemCombo item && item.Id.HasValue)
                    _bll.AgregarALote(item.Id.Value, id);

                MessageBox.Show($"Artículo «{articulo.Nombre}» agregado correctamente (ID: {id}).",
                    "Alta exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private class ItemCombo
        {
            public int?   Id    { get; set; }
            public string Texto { get; set; }
            public override string ToString() => Texto;
        }
    }
}
