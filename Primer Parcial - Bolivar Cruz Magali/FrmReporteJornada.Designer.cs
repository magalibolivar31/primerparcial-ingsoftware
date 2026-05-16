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
            this.pnlTop    = new System.Windows.Forms.Panel();
            this.lblFecha  = new System.Windows.Forms.Label();
            this.dtpFecha  = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.rtbReporte = new System.Windows.Forms.RichTextBox();

            this.pnlTop.SuspendLayout();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height    = 54;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(235, 237, 255);
            this.pnlTop.Padding   = new System.Windows.Forms.Padding(12, 10, 12, 10);

            this.lblFecha.Text     = "Fecha de jornada:";
            this.lblFecha.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFecha.Location = new System.Drawing.Point(15, 16);
            this.lblFecha.Size     = new System.Drawing.Size(140, 20);

            this.dtpFecha.Font     = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFecha.Format   = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(162, 13);
            this.dtpFecha.Size     = new System.Drawing.Size(130, 26);

            this.btnGenerar.Text      = "Generar Reporte";
            this.btnGenerar.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGenerar.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.Location  = new System.Drawing.Point(305, 11);
            this.btnGenerar.Size      = new System.Drawing.Size(160, 30);
            this.btnGenerar.Click    += new System.EventHandler(this.btnGenerar_Click);

            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFecha, this.dtpFecha, this.btnGenerar
            });

            // rtbReporte
            this.rtbReporte.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.rtbReporte.ReadOnly  = true;
            this.rtbReporte.Font      = new System.Drawing.Font("Consolas", 9.5F);
            this.rtbReporte.BackColor = System.Drawing.Color.FromArgb(250, 250, 255);
            this.rtbReporte.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.rtbReporte.WordWrap  = false;

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.rtbReporte);
            this.Controls.Add(this.pnlTop);
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmReporteJornada";
            this.Text  = "Reporte de Jornada";

            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel         pnlTop;
        private System.Windows.Forms.Label         lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button        btnGenerar;
        private System.Windows.Forms.RichTextBox   rtbReporte;
    }
}
