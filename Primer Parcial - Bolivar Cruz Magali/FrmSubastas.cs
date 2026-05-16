using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmSubastas : Form
    {
        private readonly BLL.SubastaBLL  _bllSubasta  = new BLL.SubastaBLL();
        private readonly BLL.CatalogoBLL _bllCatalogo = new BLL.CatalogoBLL();

        public FrmSubastas()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarUnidadesParaAbrir();
            CargarSubastasActivas();
        }

        private void CargarUnidadesParaAbrir()
        {
            try
            {
                cboUnidad.Items.Clear();
                var unidades = _bllCatalogo.ObtenerTodos();
                foreach (var u in unidades)
                    cboUnidad.Items.Add(new ItemCombo { Id = u.Id, Texto = $"{u.Nombre}  [$ {u.PrecioBase:N2}]", Precio = u.PrecioBase });
                if (cboUnidad.Items.Count > 0)
                    cboUnidad.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarSubastasActivas()
        {
            try
            {
                var activas = _bllSubasta.ObtenerActivas();

                // Tab Cerrar
                dgvCerrar.AutoGenerateColumns = false;
                if (dgvCerrar.Columns.Count == 0) AgregarColumnas(dgvCerrar);
                dgvCerrar.DataSource = null;
                dgvCerrar.DataSource = activas;

                // Tab Ver
                dgvVer.AutoGenerateColumns = false;
                if (dgvVer.Columns.Count == 0) AgregarColumnas(dgvVer);
                dgvVer.DataSource = null;
                dgvVer.DataSource = activas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AgregarColumnas(DataGridView dgv)
        {
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id",             HeaderText = "ID",            Width = 45  });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreUnidad",   HeaderText = "Unidad",        Width = 220 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioInicial",  HeaderText = "P. Inicial",    Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioVigente",  HeaderText = "P. Vigente",    Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaApertura",  HeaderText = "Apertura",      Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreMartillero", HeaderText = "Martillero",  Width = 140 });
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void cboUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUnidad.SelectedItem is ItemCombo item)
                lblPrecioBase.Text = $"Precio base calculado: $ {item.Precio:N2}";
            else
                lblPrecioBase.Text = string.Empty;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (!(cboUnidad.SelectedItem is ItemCombo item))
            {
                MessageBox.Show("Seleccione una unidad de venta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idMartillero = Seguridad.SessionManager.GetInstance.Usuario.Id;
            try
            {
                int idSubasta = _bllSubasta.AbrirSubasta(item.Id, idMartillero);
                MessageBox.Show($"Subasta #{idSubasta} abierta.\nPrecio inicial: $ {item.Precio:N2}",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUnidadesParaAbrir();
                CargarSubastasActivas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (!(dgvCerrar.CurrentRow?.DataBoundItem is BE.Subasta s))
            {
                MessageBox.Show("Seleccione una subasta de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(
                    $"¿Cerrar la subasta #{s.Id} — «{s.NombreUnidad}»?\nPrecio vigente: $ {s.PrecioVigente:N2}",
                    "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            int idMartillero = Seguridad.SessionManager.GetInstance.Usuario.Id;
            string obs = txtObservaciones.Text.Trim();
            try
            {
                _bllSubasta.CerrarSubasta(s.Id, idMartillero, obs.Length > 0 ? obs : null);
                MessageBox.Show("Subasta cerrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtObservaciones.Clear();
                CargarSubastasActivas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarSubastasActivas();

        private class ItemCombo
        {
            public int     Id     { get; set; }
            public string  Texto  { get; set; }
            public decimal Precio { get; set; }
            public override string ToString() => Texto;
        }
    }
}
