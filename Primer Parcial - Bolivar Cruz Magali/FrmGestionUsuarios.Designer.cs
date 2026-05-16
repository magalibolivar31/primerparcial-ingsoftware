namespace GUI
{
    partial class FrmGestionUsuarios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvUsuarios      = new System.Windows.Forms.DataGridView();
            this.pnlBotones       = new System.Windows.Forms.Panel();
            this.btnNuevoUsuario  = new System.Windows.Forms.Button();
            this.btnDesbloquear   = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnRefrescar     = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)this.dgvUsuarios).BeginInit();
            this.pnlBotones.SuspendLayout();
            this.SuspendLayout();

            // dgvUsuarios
            this.dgvUsuarios.Dock            = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsuarios.ReadOnly         = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.SelectionMode    = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.BackgroundColor  = System.Drawing.Color.White;

            // pnlBotones
            this.pnlBotones.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotones.Height    = 52;
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(235, 237, 255);
            this.pnlBotones.Padding   = new System.Windows.Forms.Padding(10, 10, 10, 10);

            var font = new System.Drawing.Font("Segoe UI", 9.5F);
            int bx = 12, by = 11, bw = 150, bh = 30, gap = 160;

            this.btnNuevoUsuario.Text = "Nuevo Usuario";
            this.btnNuevoUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevoUsuario.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnNuevoUsuario.ForeColor = System.Drawing.Color.White;
            this.btnNuevoUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoUsuario.FlatAppearance.BorderSize = 0;
            this.btnNuevoUsuario.Location = new System.Drawing.Point(bx, by);
            this.btnNuevoUsuario.Size     = new System.Drawing.Size(bw, bh);
            this.btnNuevoUsuario.Click   += new System.EventHandler(this.btnNuevoUsuario_Click);

            this.btnDesbloquear.Text = "Desbloquear Cuenta";
            this.btnDesbloquear.Font = font;
            this.btnDesbloquear.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnDesbloquear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesbloquear.Location  = new System.Drawing.Point(bx + gap, by);
            this.btnDesbloquear.Size      = new System.Drawing.Size(bw + 20, bh);
            this.btnDesbloquear.Click    += new System.EventHandler(this.btnDesbloquear_Click);

            this.btnResetPassword.Text = "Resetear Contraseña";
            this.btnResetPassword.Font = font;
            this.btnResetPassword.ForeColor = System.Drawing.Color.FromArgb(120, 60, 0);
            this.btnResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPassword.Location  = new System.Drawing.Point(bx + gap * 2 + 20, by);
            this.btnResetPassword.Size      = new System.Drawing.Size(bw + 30, bh);
            this.btnResetPassword.Click    += new System.EventHandler(this.btnResetPassword_Click);

            this.btnRefrescar.Text = "Actualizar";
            this.btnRefrescar.Font = font;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Location  = new System.Drawing.Point(bx + gap * 3 + 50, by);
            this.btnRefrescar.Size      = new System.Drawing.Size(100, bh);
            this.btnRefrescar.Click    += new System.EventHandler(this.btnRefrescar_Click);

            this.pnlBotones.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnNuevoUsuario, this.btnDesbloquear, this.btnResetPassword, this.btnRefrescar
            });

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.pnlBotones);
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmGestionUsuarios";
            this.Text  = "Gestión de Usuarios";

            ((System.ComponentModel.ISupportInitialize)this.dgvUsuarios).EndInit();
            this.pnlBotones.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Panel        pnlBotones;
        private System.Windows.Forms.Button       btnNuevoUsuario;
        private System.Windows.Forms.Button       btnDesbloquear;
        private System.Windows.Forms.Button       btnResetPassword;
        private System.Windows.Forms.Button       btnRefrescar;
    }
}
