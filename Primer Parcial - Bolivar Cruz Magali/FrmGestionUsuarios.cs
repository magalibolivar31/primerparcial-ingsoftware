using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmGestionUsuarios : Form
    {
        private readonly BLL.UsuarioBLL _bll = new BLL.UsuarioBLL();

        public FrmGestionUsuarios()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                var usuarios = _bll.ObtenerTodos();
                dgvUsuarios.AutoGenerateColumns = false;
                if (dgvUsuarios.Columns.Count == 0)
                {
                    dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id",               HeaderText = "ID",          Width = 40  });
                    dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreCompleto",   HeaderText = "Nombre",       Width = 180 });
                    dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email",            HeaderText = "Email",        Width = 200 });
                    dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Rol",              HeaderText = "Rol",          Width = 100 });
                    dgvUsuarios.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "Bloqueado",       HeaderText = "Bloqueado",    Width = 75  });
                    dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IntentosFallidos", HeaderText = "Intentos",     Width = 65  });
                    dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaAlta",        HeaderText = "Fecha Alta",   Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
                    dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvUsuarios.ReadOnly = true;
                }
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmNuevoUsuario())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                    CargarUsuarios();
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (!(dgvUsuarios.CurrentRow?.DataBoundItem is BE.Usuario u)) return;
            if (!u.Bloqueado)
            {
                MessageBox.Show("El usuario no está bloqueado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show($"¿Desbloquear la cuenta de «{u.NombreCompleto}»?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                _bll.Desbloquear(u.Id);
                MessageBox.Show("Cuenta desbloqueada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (!(dgvUsuarios.CurrentRow?.DataBoundItem is BE.Usuario u)) return;

            string nueva = MostrarInputContrasena(
                $"Nueva contraseña para «{u.NombreCompleto}» (mínimo 6 caracteres):");

            if (string.IsNullOrWhiteSpace(nueva)) return;

            try
            {
                _bll.ResetearPassword(u.Id, nueva);
                MessageBox.Show("Contraseña reseteada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarUsuarios();

        private static string MostrarInputContrasena(string prompt)
        {
            string resultado = null;
            using (var dlg = new Form())
            {
                dlg.Text            = "Resetear contraseña";
                dlg.ClientSize      = new System.Drawing.Size(370, 118);
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition   = FormStartPosition.CenterParent;
                dlg.MaximizeBox     = false;
                dlg.MinimizeBox     = false;

                var lbl = new Label { Text = prompt, Location = new System.Drawing.Point(12, 12), Size = new System.Drawing.Size(345, 36), Font = new System.Drawing.Font("Segoe UI", 9F) };
                var txt = new TextBox { PasswordChar = '●', Location = new System.Drawing.Point(12, 52), Size = new System.Drawing.Size(345, 24), Font = new System.Drawing.Font("Segoe UI", 9.5F) };
                var btnOk  = new Button { Text = "Aceptar",  DialogResult = DialogResult.OK,     Location = new System.Drawing.Point(175, 82), Size = new System.Drawing.Size(90, 28) };
                var btnNO  = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(270, 82), Size = new System.Drawing.Size(90, 28) };

                dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnNO });
                dlg.AcceptButton = btnOk;
                dlg.CancelButton = btnNO;

                if (dlg.ShowDialog() == DialogResult.OK)
                    resultado = txt.Text;
            }
            return resultado;
        }
    }
}
