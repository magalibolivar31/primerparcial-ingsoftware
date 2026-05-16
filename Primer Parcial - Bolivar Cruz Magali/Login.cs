using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class Login : Form
    {
        private readonly BLL.UsuarioBLL _usuarioBLL = new BLL.UsuarioBLL();

        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnIngresar;
        }

        // ── Eventos ──────────────────────────────────────────────────────────

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblError.Text    = string.Empty;
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MostrarError("Ingresá tu email.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MostrarError("Ingresá tu contraseña.");
                txtPassword.Focus();
                return;
            }

            try
            {
                btnIngresar.Enabled = false;
                _usuarioBLL.Login(txtEmail.Text.Trim(), txtPassword.Text);

                // Login exitoso: cierra el dialogo y Program.cs abre el Menu.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                txtPassword.Clear();

                // Si la cuenta quedo bloqueada, deshabilitar campos.
                if (ex.Message.Contains("bloqueada") || ex.Message.Contains("bloqueado"))
                {
                    txtEmail.Enabled    = false;
                    txtPassword.Enabled = false;
                }
                else
                {
                    btnIngresar.Enabled = true;
                    txtPassword.Focus();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private void MostrarError(string mensaje)
        {
            lblError.Text    = mensaje;
            lblError.Visible = true;
        }
    }
}
