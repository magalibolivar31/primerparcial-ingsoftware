namespace GUI
{
    partial class FrmReporteJornada
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop     = new System.Windows.Forms.Panel();
            this.lblFecha   = new System.Windows.Forms.Label();
            this.dtpFecha   = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.txtReporte = new System.Windows.Forms.RichTextBox();

            this.pnlTop.SuspendLayout();
            this.SuspendLayout();

            // ── pnlTop ────────────────────────────────────────────────────────
            this.pnlTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height    = 50;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(235, 237, 255);
            this.pnlTop.Padding   = new System.Windows.Forms.Padding(8);

            this.lblFecha.Text      = "Fecha de jornada:";
            this.lblFecha.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFecha.Location  = new System.Drawing.Point(10, 14);
            this.lblFecha.Size      = new System.Drawing.Size(130, 22);
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.dtpFecha.Format   = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Font     = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFecha.Location = new System.Drawing.Point(148, 12);
            this.dtpFecha.Size     = new System.Drawing.Size(120, 22);
            this.dtpFecha.Value    = System.DateTime.Today;

            this.btnGenerar.Text      = "Generar Reporte";
            this.btnGenerar.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGenerar.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.Location  = new System.Drawing.Point(280, 10);
            this.btnGenerar.Size      = new System.Drawing.Size(140, 30);
            this.btnGenerar.Click    += new System.EventHandler(this.btnGenerar_Click);

            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFecha, this.dtpFecha, this.btnGenerar
            });

            // ── txtReporte ────────────────────────────────────────────────────
            this.txtReporte.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.txtReporte.Font      = new System.Drawing.Font("Courier New", 9.5F);
            this.txtReporte.ReadOnly  = true;
            this.txtReporte.BackColor = System.Drawing.Color.White;
            this.txtReporte.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.txtReporte);
            this.Controls.Add(this.pnlTop);
            this.Font          = new System.Drawing.Font("Segoe UI", 9F);
            this.Name          = "FrmReporteJornada";
            this.Text          = "Reporte de Jornada — Catalogo Completo (RF-13)";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState   = System.Windows.Forms.FormWindowState.Maximized;

            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel       pnlTop;
        private System.Windows.Forms.Label       lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button      btnGenerar;
        private System.Windows.Forms.RichTextBox txtReporte;
    }
}
