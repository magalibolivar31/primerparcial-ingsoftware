namespace GUI
{
    partial class FrmNuevoLote
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNombre      = new System.Windows.Forms.Label();
            this.txtNombre      = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblLotePadre   = new System.Windows.Forms.Label();
            this.cboLotePadre   = new System.Windows.Forms.ComboBox();
            this.btnGuardar     = new System.Windows.Forms.Button();
            this.btnCancelar    = new System.Windows.Forms.Button();

            this.SuspendLayout();

            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont = new System.Drawing.Font("Segoe UI", 9.5F);
            int lx = 20, tx = 150, lw = 125, tw = 240, y = 20, gap = 36;

            this.lblNombre.Text = "Nombre *:";       this.lblNombre.Font = boldFont; this.lblNombre.Location = new System.Drawing.Point(lx, y + 3); this.lblNombre.Size = new System.Drawing.Size(lw, 20);
            this.txtNombre.Font = normFont;           this.txtNombre.Location = new System.Drawing.Point(tx, y); this.txtNombre.Size = new System.Drawing.Size(tw, 24); this.txtNombre.TabIndex = 0;
            y += gap;
            this.lblDescripcion.Text = "Descripción:"; this.lblDescripcion.Font = boldFont; this.lblDescripcion.Location = new System.Drawing.Point(lx, y + 3); this.lblDescripcion.Size = new System.Drawing.Size(lw, 20);
            this.txtDescripcion.Font = normFont;        this.txtDescripcion.Location = new System.Drawing.Point(tx, y); this.txtDescripcion.Size = new System.Drawing.Size(tw, 24); this.txtDescripcion.TabIndex = 1;
            y += gap;
            this.lblLotePadre.Text = "Lote Padre:"; this.lblLotePadre.Font = boldFont; this.lblLotePadre.Location = new System.Drawing.Point(lx, y + 3); this.lblLotePadre.Size = new System.Drawing.Size(lw, 20);
            this.cboLotePadre.Font = normFont;         this.cboLotePadre.Location = new System.Drawing.Point(tx, y); this.cboLotePadre.Size = new System.Drawing.Size(tw, 24);
            this.cboLotePadre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboLotePadre.TabIndex = 2;
            y += gap + 10;

            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(tx, y);
            this.btnGuardar.Size     = new System.Drawing.Size(115, 32);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Click   += new System.EventHandler(this.btnGuardar_Click);

            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location  = new System.Drawing.Point(tx + 125, y);
            this.btnCancelar.Size      = new System.Drawing.Size(115, 32);
            this.btnCancelar.TabIndex  = 4;
            this.btnCancelar.Click    += new System.EventHandler(this.btnCancelar_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(420, y + 52);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblNombre, this.txtNombre, this.lblDescripcion, this.txtDescripcion,
                this.lblLotePadre, this.cboLotePadre, this.btnGuardar, this.btnCancelar
            });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Nuevo Lote";
            this.AcceptButton    = this.btnGuardar;
            this.CancelButton    = this.btnCancelar;

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label    lblNombre;
        private System.Windows.Forms.TextBox  txtNombre;
        private System.Windows.Forms.Label    lblDescripcion;
        private System.Windows.Forms.TextBox  txtDescripcion;
        private System.Windows.Forms.Label    lblLotePadre;
        private System.Windows.Forms.ComboBox cboLotePadre;
        private System.Windows.Forms.Button   btnGuardar;
        private System.Windows.Forms.Button   btnCancelar;
    }
}
