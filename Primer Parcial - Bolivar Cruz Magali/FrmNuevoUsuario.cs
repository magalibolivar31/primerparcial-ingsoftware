using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmNuevoUsuario : Form
    {
        private readonly BLL.UsuarioBLL _bll = new BLL.UsuarioBLL();

        public FrmNuevoUsuario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text)  ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                var rol = (BE.RolUsuario)Enum.Parse(typeof(BE.RolUsuario), cboRol.SelectedItem.ToString());
                var usuario = new BE.Usuario
                {
                    Nombre   = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email    = txtEmail.Text.Trim(),
                    Rol      = rol,
                    Activo   = true
                };
                _bll.Alta(usuario, txtPassword.Text);
                MessageBox.Show($"Usuario «{usuario.NombreCompleto}» creado correctamente.",
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
    }
}
