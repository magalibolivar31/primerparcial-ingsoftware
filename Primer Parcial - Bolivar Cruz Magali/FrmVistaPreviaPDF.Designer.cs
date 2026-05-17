namespace GUI
{
    partial class FrmVistaPreviaPDF
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop       = new System.Windows.Forms.Panel();
            this.btnImprimir  = new System.Windows.Forms.Button();
            this.btnCerrar    = new System.Windows.Forms.Button();
            this.printPreview = new System.Windows.Forms.PrintPreviewControl();

            this.pnlTop.SuspendLayout();
            this.SuspendLayout();

            // ── pnlTop ────────────────────────────────────────────────────────
            this.pnlTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height    = 48;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(235, 237, 255);
            this.pnlTop.Padding   = new System.Windows.Forms.Padding(8, 8, 8, 8);

            this.btnImprimir.Text      = "Imprimir / Guardar como PDF";
            this.btnImprimir.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(180, 30, 30);
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.Location  = new System.Drawing.Point(10, 9);
            this.btnImprimir.Size      = new System.Drawing.Size(230, 30);
            this.btnImprimir.Click    += new System.EventHandler(this.btnImprimir_Click);

            this.btnCerrar.Text      = "Cerrar";
            this.btnCerrar.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.Location  = new System.Drawing.Point(250, 9);
            this.btnCerrar.Size      = new System.Drawing.Size(90, 30);
            this.btnCerrar.Click    += new System.EventHandler(this.btnCerrar_Click);

            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnImprimir, this.btnCerrar
            });

            // ── printPreview ──────────────────────────────────────────────────
            this.printPreview.Dock       = System.Windows.Forms.DockStyle.Fill;
            this.printPreview.BackColor  = System.Drawing.Color.FromArgb(60, 60, 60);
            this.printPreview.Zoom       = 1.0;
            this.printPreview.AutoZoom   = true;

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(950, 720);
            this.Controls.Add(this.printPreview);
            this.Controls.Add(this.pnlTop);
            this.Font        = new System.Drawing.Font("Segoe UI", 9F);
            this.Name        = "FrmVistaPreviaPDF";
            this.Text        = "Vista Previa — Bitácora de Subastas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel               pnlTop;
        private System.Windows.Forms.Button              btnImprimir;
        private System.Windows.Forms.Button              btnCerrar;
        private System.Windows.Forms.PrintPreviewControl printPreview;
    }
}
