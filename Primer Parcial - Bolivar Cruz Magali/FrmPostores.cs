using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmPostores : Form
    {
        private readonly BLL.PostorBLL  _bllPostor  = new BLL.PostorBLL();
        private readonly BLL.SubastaBLL _bllSubasta = new BLL.SubastaBLL();

        public FrmPostores()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarPostores();
            CargarSubastasActivas();
        }

        // ── Tab Postores ──────────────────────────────────────────────────────

        private void CargarPostores()
        {
            try
            {
                var postores = _bllPostor.ObtenerTodos();
                dgvPostores.AutoGenerateColumns = false;
                if (dgvPostores.Columns.Count == 0)
                {
                    dgvPostores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id",       HeaderText = "ID",        Width = 45 });
                    dgvPostores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre",   HeaderText = "Nombre",    Width = 180 });
                    dgvPostores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DniCuit",  HeaderText = "DNI/CUIT",  Width = 110 });
                    dgvPostores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email",    HeaderText = "Email",     Width = 180 });
                    dgvPostores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Telefono", HeaderText = "Teléfono",  Width = 110 });
                    dgvPostores.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Canal",    HeaderText = "Canal",     Width = 80  });
                    dgvPostores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvPostores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
                dgvPostores.DataSource = null;
                dgvPostores.DataSource = postores;
                LimpiarFormPostor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormPostor()
        {
            txtNombrePostor.Clear();
            txtDniCuit.Clear();
            txtEmailPostor.Clear();
            txtTelefono.Clear();
            cboCanal.SelectedIndex = 0;
        }

        private void dgvPostores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPostores.CurrentRow?.DataBoundItem is BE.Postor p)
            {
                txtNombrePostor.Text     = p.Nombre;
                txtDniCuit.Text          = p.DniCuit;
                txtEmailPostor.Text      = p.Email;
                txtTelefono.Text         = p.Telefono ?? string.Empty;
                cboCanal.SelectedItem    = p.Canal.ToString();
            }
        }

        private void btnNuevoPostor_Click(object sender, EventArgs e)
        {
            dgvPostores.ClearSelection();
            LimpiarFormPostor();
            txtNombrePostor.Focus();
        }

        private void btnGuardarPostor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombrePostor.Text) ||
                string.IsNullOrWhiteSpace(txtDniCuit.Text)      ||
                string.IsNullOrWhiteSpace(txtEmailPostor.Text))
            {
                MessageBox.Show("Nombre, DNI/CUIT y Email son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esNuevo = dgvPostores.CurrentRow?.DataBoundItem == null;

            try
            {
                if (esNuevo)
                {
                    var p = new BE.Postor
                    {
                        Nombre   = txtNombrePostor.Text.Trim(),
                        DniCuit  = txtDniCuit.Text.Trim(),
                        Email    = txtEmailPostor.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        Canal    = (BE.CanalNotificacion)Enum.Parse(typeof(BE.CanalNotificacion), cboCanal.SelectedItem?.ToString() ?? "Web", true),
                        Activo   = true
                    };
                    _bllPostor.Alta(p);
                    MessageBox.Show("Postor agregado correctamente.", "Alta exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var p = (BE.Postor)dgvPostores.CurrentRow.DataBoundItem;
                    p.Nombre   = txtNombrePostor.Text.Trim();
                    p.DniCuit  = txtDniCuit.Text.Trim();
                    p.Email    = txtEmailPostor.Text.Trim();
                    p.Telefono = txtTelefono.Text.Trim();
                    p.Canal    = (BE.CanalNotificacion)Enum.Parse(typeof(BE.CanalNotificacion), cboCanal.SelectedItem?.ToString() ?? "Web", true);
                    _bllPostor.Modificar(p);
                    MessageBox.Show("Postor actualizado correctamente.", "Modificación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                CargarPostores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBajaPostor_Click(object sender, EventArgs e)
        {
            if (!(dgvPostores.CurrentRow?.DataBoundItem is BE.Postor p)) return;
            if (MessageBox.Show($"¿Dar de baja al postor «{p.Nombre}»?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                _bllPostor.Baja(p.Id);
                CargarPostores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Tab Suscripciones ─────────────────────────────────────────────────

        private void CargarSubastasActivas()
        {
            try
            {
                var activas = _bllSubasta.ObtenerActivas();
                cboSubastaSusc.DataSource    = activas;
                cboSubastaSusc.DisplayMember = "NombreUnidad";
                cboSubastaSusc.ValueMember   = "Id";
                cboSubastaSusc.SelectedIndex = activas.Count > 0 ? 0 : -1;

                var postores = _bllPostor.ObtenerTodos();
                cboPostorSusc.DataSource    = postores;
                cboPostorSusc.DisplayMember = "Nombre";
                cboPostorSusc.ValueMember   = "Id";
                cboPostorSusc.SelectedIndex = postores.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboSubastaSusc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSubastaSusc.SelectedItem is BE.Subasta s)
                CargarSuscripciones(s.Id);
        }

        private void CargarSuscripciones(int idSubasta)
        {
            try
            {
                var suscs = _bllPostor.ObtenerSuscripcionesActivas(idSubasta);
                dgvSuscs.AutoGenerateColumns = false;
                if (dgvSuscs.Columns.Count == 0)
                {
                    dgvSuscs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdPostor",         HeaderText = "ID",          Width = 45 });
                    dgvSuscs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombrePostor",     HeaderText = "Postor",      Width = 180 });
                    dgvSuscs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaSuscripcion", HeaderText = "Fecha Susc.", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
                    dgvSuscs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvSuscs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
                dgvSuscs.DataSource = null;
                dgvSuscs.DataSource = suscs;
            }
            catch { }
        }

        private void btnSuscribir_Click(object sender, EventArgs e)
        {
            if (!(cboSubastaSusc.SelectedItem is BE.Subasta s) || !(cboPostorSusc.SelectedItem is BE.Postor p))
            {
                MessageBox.Show("Seleccione una subasta y un postor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _bllPostor.Suscribir(p.Id, s.Id);
                MessageBox.Show($"Postor «{p.Nombre}» suscripto a la subasta #{s.Id}.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarSuscripciones(s.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesuscribir_Click(object sender, EventArgs e)
        {
            if (!(cboSubastaSusc.SelectedItem is BE.Subasta s) || !(cboPostorSusc.SelectedItem is BE.Postor p))
            {
                MessageBox.Show("Seleccione una subasta y un postor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _bllPostor.Desuscribir(p.Id, s.Id);
                MessageBox.Show($"Suscripción eliminada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarSuscripciones(s.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
