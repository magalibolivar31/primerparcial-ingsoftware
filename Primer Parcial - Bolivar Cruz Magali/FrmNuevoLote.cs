using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmNuevoLote : Form
    {
        private readonly BLL.CatalogoBLL _bll = new BLL.CatalogoBLL();

        public FrmNuevoLote()
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
                MessageBox.Show("El nombre del lote es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                var lote = new BE.Lote
                {
                    Nombre      = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Activo      = true
                };

                int id = _bll.AltaLote(lote);

                if (cboLotePadre.SelectedItem is ItemCombo item && item.Id.HasValue)
                    _bll.AgregarALote(item.Id.Value, id);

                MessageBox.Show($"Lote «{lote.Nombre}» creado correctamente (ID: {id}).",
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
