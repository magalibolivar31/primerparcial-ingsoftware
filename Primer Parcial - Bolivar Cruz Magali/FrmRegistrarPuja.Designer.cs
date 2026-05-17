namespace GUI
{
    partial class FrmRegistrarPuja
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop            = new System.Windows.Forms.Panel();
            this.lblSubastaLbl     = new System.Windows.Forms.Label();
            this.cboSubasta        = new System.Windows.Forms.ComboBox();
            this.lblPrecioVigente  = new System.Windows.Forms.Label();
            this.lblObserverStatus = new System.Windows.Forms.Label();
            this.lblPostorLbl      = new System.Windows.Forms.Label();
            this.cboPostor         = new System.Windows.Forms.ComboBox();
            this.lblMontoLbl       = new System.Windows.Forms.Label();
            this.nudMonto          = new System.Windows.Forms.NumericUpDown();
            this.btnRegistrar      = new System.Windows.Forms.Button();
            this.btnActualizar     = new System.Windows.Forms.Button();
            this.pnlAlerta         = new System.Windows.Forms.Panel();
            this.lblAlerta         = new System.Windows.Forms.Label();
            this.lblHistLbl        = new System.Windows.Forms.Label();
            this.dgvHistorial      = new System.Windows.Forms.DataGridView();

            this.pnlTop.SuspendLayout();
            this.pnlAlerta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudMonto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvHistorial).BeginInit();
            this.SuspendLayout();

            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont = new System.Drawing.Font("Segoe UI", 9.5F);
            int lx = 20, vx = 185, lw = 160, vw = 400, y = 18, gap = 34;

            // ── pnlTop ────────────────────────────────────────────────────────
            this.pnlTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height    = 205;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(248, 248, 255);

            // Fila 1: Subasta
            this.lblSubastaLbl.Text = "Subasta activa:";
            this.lblSubastaLbl.Font = boldFont;
            this.lblSubastaLbl.Location  = new System.Drawing.Point(lx, y + 3);
            this.lblSubastaLbl.Size      = new System.Drawing.Size(lw, 20);
            this.lblSubastaLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cboSubasta.Font         = normFont;
            this.cboSubasta.Location     = new System.Drawing.Point(vx, y);
            this.cboSubasta.Size         = new System.Drawing.Size(vw, 26);
            this.cboSubasta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubasta.SelectedIndexChanged += new System.EventHandler(this.cboSubasta_SelectedIndexChanged);
            y += gap;

            // Precio vigente
            this.lblPrecioVigente.Text      = "Precio vigente: —";
            this.lblPrecioVigente.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblPrecioVigente.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPrecioVigente.Location  = new System.Drawing.Point(vx, y);
            this.lblPrecioVigente.Size      = new System.Drawing.Size(vw, 20);
            y += 24;

            // Observer status (RF-05 / RF-08)
            this.lblObserverStatus.Text      = "Sin suscripción activa";
            this.lblObserverStatus.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblObserverStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblObserverStatus.Location  = new System.Drawing.Point(vx, y);
            this.lblObserverStatus.Size      = new System.Drawing.Size(vw, 18);
            y += 26;

            // Fila 2: Postor
            this.lblPostorLbl.Text = "Postor:";
            this.lblPostorLbl.Font = boldFont;
            this.lblPostorLbl.Location  = new System.Drawing.Point(lx, y + 3);
            this.lblPostorLbl.Size      = new System.Drawing.Size(lw, 20);
            this.lblPostorLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cboPostor.Font         = normFont;
            this.cboPostor.Location     = new System.Drawing.Point(vx, y);
            this.cboPostor.Size         = new System.Drawing.Size(vw, 26);
            this.cboPostor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            y += gap;

            // Fila 3: Monto
            this.lblMontoLbl.Text = "Monto de oferta ($):";
            this.lblMontoLbl.Font = boldFont;
            this.lblMontoLbl.Location  = new System.Drawing.Point(lx, y + 4);
            this.lblMontoLbl.Size      = new System.Drawing.Size(lw, 20);
            this.lblMontoLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.nudMonto.Font          = normFont;
            this.nudMonto.Location      = new System.Drawing.Point(vx, y);
            this.nudMonto.Size          = new System.Drawing.Size(165, 28);
            this.nudMonto.Minimum       = 0.01m;
            this.nudMonto.Maximum       = 99999999m;
            this.nudMonto.DecimalPlaces = 2;
            y += gap;

            // Fila 4: Botones
            this.btnRegistrar.Text      = "Registrar Puja";
            this.btnRegistrar.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRegistrar.BackColor = System.Drawing.Color.FromArgb(30, 100, 30);
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.FlatAppearance.BorderSize = 0;
            this.btnRegistrar.Location  = new System.Drawing.Point(vx, y);
            this.btnRegistrar.Size      = new System.Drawing.Size(155, 34);
            this.btnRegistrar.Click    += new System.EventHandler(this.btnRegistrar_Click);

            this.btnActualizar.Text      = "Actualizar";
            this.btnActualizar.Font      = normFont;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Location  = new System.Drawing.Point(vx + 165, y);
            this.btnActualizar.Size      = new System.Drawing.Size(100, 34);
            this.btnActualizar.Click    += new System.EventHandler(this.btnActualizar_Click);

            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSubastaLbl, this.cboSubasta,
                this.lblPrecioVigente, this.lblObserverStatus,
                this.lblPostorLbl, this.cboPostor,
                this.lblMontoLbl, this.nudMonto,
                this.btnRegistrar, this.btnActualizar
            });

            // ── pnlAlerta — banner de notificación Observer (RF-06 / RF-07) ──
            this.pnlAlerta.Dock    = System.Windows.Forms.DockStyle.Top;
            this.pnlAlerta.Height  = 32;
            this.pnlAlerta.Visible = false;

            this.lblAlerta.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblAlerta.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlerta.ForeColor = System.Drawing.Color.White;
            this.lblAlerta.Text      = "";
            this.lblAlerta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.pnlAlerta.Controls.Add(this.lblAlerta);

            // ── Historial ─────────────────────────────────────────────────────
            this.lblHistLbl.Text    = "Historial de pujas de la subasta seleccionada:";
            this.lblHistLbl.Font    = boldFont;
            this.lblHistLbl.Dock    = System.Windows.Forms.DockStyle.Top;
            this.lblHistLbl.Height  = 22;
            this.lblHistLbl.Padding = new System.Windows.Forms.Padding(5, 4, 0, 0);

            this.dgvHistorial.Dock              = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorial.ReadOnly          = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.BackgroundColor   = System.Drawing.Color.White;

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(880, 580);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.lblHistLbl);
            this.Controls.Add(this.pnlAlerta);
            this.Controls.Add(this.pnlTop);
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmRegistrarPuja";
            this.Text  = "Registrar Puja";

            this.pnlTop.ResumeLayout(false);
            this.pnlAlerta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.nudMonto).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvHistorial).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel          pnlTop;
        private System.Windows.Forms.Label          lblSubastaLbl;
        private System.Windows.Forms.ComboBox       cboSubasta;
        private System.Windows.Forms.Label          lblPrecioVigente;
        private System.Windows.Forms.Label          lblObserverStatus;
        private System.Windows.Forms.Label          lblPostorLbl;
        private System.Windows.Forms.ComboBox       cboPostor;
        private System.Windows.Forms.Label          lblMontoLbl;
        private System.Windows.Forms.NumericUpDown  nudMonto;
        private System.Windows.Forms.Button         btnRegistrar;
        private System.Windows.Forms.Button         btnActualizar;
        private System.Windows.Forms.Panel          pnlAlerta;
        private System.Windows.Forms.Label          lblAlerta;
        private System.Windows.Forms.Label          lblHistLbl;
        private System.Windows.Forms.DataGridView   dgvHistorial;
    }
}
