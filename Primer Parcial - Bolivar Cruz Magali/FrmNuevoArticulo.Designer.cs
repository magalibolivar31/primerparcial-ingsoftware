namespace GUI
{
    partial class FrmNuevoArticulo
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
            this.lblCategoria   = new System.Windows.Forms.Label();
            this.txtCategoria   = new System.Windows.Forms.TextBox();
            this.lblEstado      = new System.Windows.Forms.Label();
            this.txtEstado      = new System.Windows.Forms.TextBox();
            this.lblUbicacion   = new System.Windows.Forms.Label();
            this.txtUbicacion   = new System.Windows.Forms.TextBox();
            this.lblValor       = new System.Windows.Forms.Label();
            this.nudValor       = new System.Windows.Forms.NumericUpDown();
            this.lblLotePadre   = new System.Windows.Forms.Label();
            this.cboLotePadre   = new System.Windows.Forms.ComboBox();
            this.btnGuardar     = new System.Windows.Forms.Button();
            this.btnCancelar    = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)this.nudValor).BeginInit();
            this.SuspendLayout();

            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont = new System.Drawing.Font("Segoe UI", 9.5F);
            int lx = 20, tx = 160, lw = 135, tw = 240, y = 20, gap = 36;

            // Nombre
            this.lblNombre.Text = "Nombre *:";      this.lblNombre.Font = boldFont;    this.lblNombre.Location = new System.Drawing.Point(lx, y + 3);   this.lblNombre.Size = new System.Drawing.Size(lw, 20);
            this.txtNombre.Font = normFont;          this.txtNombre.Location = new System.Drawing.Point(tx, y);  this.txtNombre.Size = new System.Drawing.Size(tw, 24); this.txtNombre.TabIndex = 0;
            y += gap;
            // Descripción
            this.lblDescripcion.Text = "Descripción:"; this.lblDescripcion.Font = boldFont; this.lblDescripcion.Location = new System.Drawing.Point(lx, y + 3); this.lblDescripcion.Size = new System.Drawing.Size(lw, 20);
            this.txtDescripcion.Font = normFont;        this.txtDescripcion.Location = new System.Drawing.Point(tx, y); this.txtDescripcion.Size = new System.Drawing.Size(tw, 24); this.txtDescripcion.TabIndex = 1;
            y += gap;
            // Categoría
            this.lblCategoria.Text = "Categoría:"; this.lblCategoria.Font = boldFont; this.lblCategoria.Location = new System.Drawing.Point(lx, y + 3); this.lblCategoria.Size = new System.Drawing.Size(lw, 20);
            this.txtCategoria.Font = normFont;      this.txtCategoria.Location = new System.Drawing.Point(tx, y); this.txtCategoria.Size = new System.Drawing.Size(tw, 24); this.txtCategoria.TabIndex = 2;
            y += gap;
            // Estado Físico
            this.lblEstado.Text = "Estado Físico:"; this.lblEstado.Font = boldFont; this.lblEstado.Location = new System.Drawing.Point(lx, y + 3); this.lblEstado.Size = new System.Drawing.Size(lw, 20);
            this.txtEstado.Font = normFont;          this.txtEstado.Location = new System.Drawing.Point(tx, y); this.txtEstado.Size = new System.Drawing.Size(tw, 24); this.txtEstado.TabIndex = 3;
            y += gap;
            // Ubicación
            this.lblUbicacion.Text = "Ubicación:"; this.lblUbicacion.Font = boldFont; this.lblUbicacion.Location = new System.Drawing.Point(lx, y + 3); this.lblUbicacion.Size = new System.Drawing.Size(lw, 20);
            this.txtUbicacion.Font = normFont;       this.txtUbicacion.Location = new System.Drawing.Point(tx, y); this.txtUbicacion.Size = new System.Drawing.Size(tw, 24); this.txtUbicacion.TabIndex = 4;
            y += gap;
            // Valor Declarado
            this.lblValor.Text = "Valor Declarado *:"; this.lblValor.Font = boldFont; this.lblValor.Location = new System.Drawing.Point(lx, y + 3); this.lblValor.Size = new System.Drawing.Size(lw, 20);
            this.nudValor.Font = normFont; this.nudValor.Location = new System.Drawing.Point(tx, y); this.nudValor.Size = new System.Drawing.Size(tw, 24);
            this.nudValor.Minimum = 0.01m; this.nudValor.Maximum = 99999999m; this.nudValor.DecimalPlaces = 2; this.nudValor.TabIndex = 5;
            y += gap;
            // Lote Padre
            this.lblLotePadre.Text = "Lote Padre:"; this.lblLotePadre.Font = boldFont; this.lblLotePadre.Location = new System.Drawing.Point(lx, y + 3); this.lblLotePadre.Size = new System.Drawing.Size(lw, 20);
            this.cboLotePadre.Font = normFont; this.cboLotePadre.Location = new System.Drawing.Point(tx, y); this.cboLotePadre.Size = new System.Drawing.Size(tw, 24);
            this.cboLotePadre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboLotePadre.TabIndex = 6;
            y += gap + 10;

            // Buttons
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(tx, y);
            this.btnGuardar.Size     = new System.Drawing.Size(115, 32);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Click   += new System.EventHandler(this.btnGuardar_Click);

            this.btnCancelar.Text      = "Cancelar";
            this.btnCancelar.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location  = new System.Drawing.Point(tx + 125, y);
            this.btnCancelar.Size      = new System.Drawing.Size(115, 32);
            this.btnCancelar.TabIndex  = 8;
            this.btnCancelar.Click    += new System.EventHandler(this.btnCancelar_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(430, y + 52);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblNombre, this.txtNombre, this.lblDescripcion, this.txtDescripcion,
                this.lblCategoria, this.txtCategoria, this.lblEstado, this.txtEstado,
                this.lblUbicacion, this.txtUbicacion, this.lblValor, this.nudValor,
                this.lblLotePadre, this.cboLotePadre, this.btnGuardar, this.btnCancelar
            });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Nuevo Artículo Individual";
            this.AcceptButton    = this.btnGuardar;
            this.CancelButton    = this.btnCancelar;

            ((System.ComponentModel.ISupportInitialize)this.nudValor).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label          lblNombre;
        private System.Windows.Forms.TextBox        txtNombre;
        private System.Windows.Forms.Label          lblDescripcion;
        private System.Windows.Forms.TextBox        txtDescripcion;
        private System.Windows.Forms.Label          lblCategoria;
        private System.Windows.Forms.TextBox        txtCategoria;
        private System.Windows.Forms.Label          lblEstado;
        private System.Windows.Forms.TextBox        txtEstado;
        private System.Windows.Forms.Label          lblUbicacion;
        private System.Windows.Forms.TextBox        txtUbicacion;
        private System.Windows.Forms.Label          lblValor;
        private System.Windows.Forms.NumericUpDown  nudValor;
        private System.Windows.Forms.Label          lblLotePadre;
        private System.Windows.Forms.ComboBox       cboLotePadre;
        private System.Windows.Forms.Button         btnGuardar;
        private System.Windows.Forms.Button         btnCancelar;
    }
}
