namespace GUI
{
    partial class FrmNuevoUsuario
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNombre   = new System.Windows.Forms.Label();
            this.txtNombre   = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblEmail    = new System.Windows.Forms.Label();
            this.txtEmail    = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblRol      = new System.Windows.Forms.Label();
            this.cboRol      = new System.Windows.Forms.ComboBox();
            this.btnGuardar  = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont = new System.Drawing.Font("Segoe UI", 9.5F);
            int lx = 20, tx = 140, lw = 115, tw = 240, y = 20, gap = 36;

            this.lblNombre.Text = "Nombre *:";    this.lblNombre.Font = boldFont; this.lblNombre.Location = new System.Drawing.Point(lx, y + 3); this.lblNombre.Size = new System.Drawing.Size(lw, 20);
            this.txtNombre.Font = normFont;        this.txtNombre.Location = new System.Drawing.Point(tx, y); this.txtNombre.Size = new System.Drawing.Size(tw, 24); this.txtNombre.TabIndex = 0;
            y += gap;
            this.lblApellido.Text = "Apellido *:"; this.lblApellido.Font = boldFont; this.lblApellido.Location = new System.Drawing.Point(lx, y + 3); this.lblApellido.Size = new System.Drawing.Size(lw, 20);
            this.txtApellido.Font = normFont;       this.txtApellido.Location = new System.Drawing.Point(tx, y); this.txtApellido.Size = new System.Drawing.Size(tw, 24); this.txtApellido.TabIndex = 1;
            y += gap;
            this.lblEmail.Text = "Email *:";      this.lblEmail.Font = boldFont; this.lblEmail.Location = new System.Drawing.Point(lx, y + 3); this.lblEmail.Size = new System.Drawing.Size(lw, 20);
            this.txtEmail.Font = normFont;          this.txtEmail.Location = new System.Drawing.Point(tx, y); this.txtEmail.Size = new System.Drawing.Size(tw, 24); this.txtEmail.TabIndex = 2;
            y += gap;
            this.lblPassword.Text = "Contraseña *:"; this.lblPassword.Font = boldFont; this.lblPassword.Location = new System.Drawing.Point(lx, y + 3); this.lblPassword.Size = new System.Drawing.Size(lw, 20);
            this.txtPassword.Font = normFont;         this.txtPassword.PasswordChar = '●'; this.txtPassword.Location = new System.Drawing.Point(tx, y); this.txtPassword.Size = new System.Drawing.Size(tw, 24); this.txtPassword.TabIndex = 3;
            y += gap;
            this.lblRol.Text = "Rol *:";           this.lblRol.Font = boldFont; this.lblRol.Location = new System.Drawing.Point(lx, y + 3); this.lblRol.Size = new System.Drawing.Size(lw, 20);
            this.cboRol.Font = normFont;             this.cboRol.Location = new System.Drawing.Point(tx, y); this.cboRol.Size = new System.Drawing.Size(tw, 24);
            this.cboRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRol.Items.AddRange(new object[] { "Martillero", "Operador" });
            this.cboRol.SelectedIndex = 1; this.cboRol.TabIndex = 4;
            y += gap + 10;

            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(tx, y);
            this.btnGuardar.Size     = new System.Drawing.Size(115, 32);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Click   += new System.EventHandler(this.btnGuardar_Click);

            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location  = new System.Drawing.Point(tx + 125, y);
            this.btnCancelar.Size      = new System.Drawing.Size(115, 32);
            this.btnCancelar.TabIndex  = 6;
            this.btnCancelar.Click    += new System.EventHandler(this.btnCancelar_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(415, y + 52);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblNombre, this.txtNombre, this.lblApellido, this.txtApellido,
                this.lblEmail, this.txtEmail, this.lblPassword, this.txtPassword,
                this.lblRol, this.cboRol, this.btnGuardar, this.btnCancelar
            });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Nuevo Usuario";
            this.AcceptButton    = this.btnGuardar;
            this.CancelButton    = this.btnCancelar;

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label    lblNombre;
        private System.Windows.Forms.TextBox  txtNombre;
        private System.Windows.Forms.Label    lblApellido;
        private System.Windows.Forms.TextBox  txtApellido;
        private System.Windows.Forms.Label    lblEmail;
        private System.Windows.Forms.TextBox  txtEmail;
        private System.Windows.Forms.Label    lblPassword;
        private System.Windows.Forms.TextBox  txtPassword;
        private System.Windows.Forms.Label    lblRol;
        private System.Windows.Forms.ComboBox cboRol;
        private System.Windows.Forms.Button   btnGuardar;
        private System.Windows.Forms.Button   btnCancelar;
    }
}
