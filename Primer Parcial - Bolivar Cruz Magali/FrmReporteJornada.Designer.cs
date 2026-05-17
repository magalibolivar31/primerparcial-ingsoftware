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
            this.statusStrip      = new System.Windows.Forms.StatusStrip();
            this.lblStatus        = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpFiltros       = new System.Windows.Forms.GroupBox();
            this.lblEstado        = new System.Windows.Forms.Label();
            this.cboEstado        = new System.Windows.Forms.ComboBox();
            this.lblTipo          = new System.Windows.Forms.Label();
            this.cboTipo          = new System.Windows.Forms.ComboBox();
            this.chkSoloRaiz      = new System.Windows.Forms.CheckBox();
            this.btnGenerar       = new System.Windows.Forms.Button();
            this.btnLimpiar       = new System.Windows.Forms.Button();
            this.btnExportarPDF   = new System.Windows.Forms.Button();
            this.lblConteo        = new System.Windows.Forms.Label();
            this.pnlTotales       = new System.Windows.Forms.Panel();
            this.pnlCard1         = new System.Windows.Forms.Panel();
            this.lblTitleCard1    = new System.Windows.Forms.Label();
            this.lblTotalItems    = new System.Windows.Forms.Label();
            this.pnlCard2         = new System.Windows.Forms.Panel();
            this.lblTitleCard2    = new System.Windows.Forms.Label();
            this.lblAdjudicados   = new System.Windows.Forms.Label();
            this.pnlCard3         = new System.Windows.Forms.Panel();
            this.lblTitleCard3    = new System.Windows.Forms.Label();
            this.lblDesiertos     = new System.Windows.Forms.Label();
            this.pnlCard4         = new System.Windows.Forms.Panel();
            this.lblTitleCard4    = new System.Windows.Forms.Label();
            this.lblMontoRecaudado = new System.Windows.Forms.Label();
            this.pnlCard5         = new System.Windows.Forms.Panel();
            this.lblTitleCard5    = new System.Windows.Forms.Label();
            this.lblNotificados   = new System.Windows.Forms.Label();
            this.grpCatalogo      = new System.Windows.Forms.GroupBox();
            this.tvCatalogo       = new System.Windows.Forms.TreeView();

            this.statusStrip.SuspendLayout();
            this.grpFiltros.SuspendLayout();
            this.pnlTotales.SuspendLayout();
            this.pnlCard1.SuspendLayout();
            this.pnlCard2.SuspendLayout();
            this.pnlCard3.SuspendLayout();
            this.pnlCard4.SuspendLayout();
            this.pnlCard5.SuspendLayout();
            this.grpCatalogo.SuspendLayout();
            this.SuspendLayout();

            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont = new System.Drawing.Font("Segoe UI", 9.5F);
            var darkNavy = System.Drawing.Color.FromArgb(26, 35, 64);

            // ── statusStrip ────────────────────────────────────────────────────
            this.statusStrip.BackColor = darkNavy;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblStatus });
            this.statusStrip.Location  = new System.Drawing.Point(0, 858);
            this.statusStrip.Name      = "statusStrip";
            this.statusStrip.Size      = new System.Drawing.Size(1200, 22);
            this.statusStrip.TabIndex  = 0;

            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Name      = "lblStatus";
            this.lblStatus.Size      = new System.Drawing.Size(0, 15);

            // ── grpFiltros (una sola fila de botones + conteo) ─────────────────
            this.grpFiltros.Text     = "Filtros";
            this.grpFiltros.Font     = boldFont;
            this.grpFiltros.Location = new System.Drawing.Point(5, 5);
            this.grpFiltros.Size     = new System.Drawing.Size(1185, 65);
            this.grpFiltros.Anchor   = System.Windows.Forms.AnchorStyles.Top
                                     | System.Windows.Forms.AnchorStyles.Left
                                     | System.Windows.Forms.AnchorStyles.Right;

            this.lblEstado.Text      = "Estado:";
            this.lblEstado.Font      = boldFont;
            this.lblEstado.Location  = new System.Drawing.Point(10, 26);
            this.lblEstado.Size      = new System.Drawing.Size(55, 20);
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cboEstado.Font          = normFont;
            this.cboEstado.Location      = new System.Drawing.Point(70, 24);
            this.cboEstado.Size          = new System.Drawing.Size(148, 26);
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Items.AddRange(new object[] { "Todos", "Solo Adjudicadas", "Solo Desiertas" });
            this.cboEstado.SelectedIndex = 0;

            this.lblTipo.Text      = "Tipo:";
            this.lblTipo.Font      = boldFont;
            this.lblTipo.Location  = new System.Drawing.Point(228, 26);
            this.lblTipo.Size      = new System.Drawing.Size(40, 20);
            this.lblTipo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cboTipo.Font          = normFont;
            this.cboTipo.Location      = new System.Drawing.Point(273, 24);
            this.cboTipo.Size          = new System.Drawing.Size(140, 26);
            this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipo.Items.AddRange(new object[] { "Todos", "Solo Lotes", "Solo Artículos" });
            this.cboTipo.SelectedIndex = 0;

            this.chkSoloRaiz.Text     = "Solo nivel raíz";
            this.chkSoloRaiz.Font     = boldFont;
            this.chkSoloRaiz.Location = new System.Drawing.Point(423, 26);
            this.chkSoloRaiz.Size     = new System.Drawing.Size(130, 22);

            this.btnGenerar.Text      = "Generar";
            this.btnGenerar.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGenerar.BackColor = darkNavy;
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.Location  = new System.Drawing.Point(565, 22);
            this.btnGenerar.Size      = new System.Drawing.Size(90, 28);
            this.btnGenerar.Click    += new System.EventHandler(this.btnGenerar_Click);

            this.btnLimpiar.Text      = "Limpiar";
            this.btnLimpiar.Font      = normFont;
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.Location  = new System.Drawing.Point(663, 22);
            this.btnLimpiar.Size      = new System.Drawing.Size(80, 28);
            this.btnLimpiar.Click    += new System.EventHandler(this.btnLimpiar_Click);

            this.btnExportarPDF.Text      = "Exportar TXT";
            this.btnExportarPDF.Font      = normFont;
            this.btnExportarPDF.BackColor = System.Drawing.Color.FromArgb(180, 50, 50);
            this.btnExportarPDF.ForeColor = System.Drawing.Color.White;
            this.btnExportarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarPDF.FlatAppearance.BorderSize = 0;
            this.btnExportarPDF.Location  = new System.Drawing.Point(751, 22);
            this.btnExportarPDF.Size      = new System.Drawing.Size(105, 28);
            this.btnExportarPDF.Click    += new System.EventHandler(this.btnExportarPDF_Click);

            this.lblConteo.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblConteo.ForeColor = System.Drawing.Color.DimGray;
            this.lblConteo.Location  = new System.Drawing.Point(870, 28);
            this.lblConteo.Size      = new System.Drawing.Size(300, 18);
            this.lblConteo.Text      = "";

            this.grpFiltros.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblEstado, this.cboEstado, this.lblTipo, this.cboTipo,
                this.chkSoloRaiz,
                this.btnGenerar, this.btnLimpiar, this.btnExportarPDF,
                this.lblConteo
            });

            // ── pnlTotales — 5 tarjetas de métricas ───────────────────────────
            this.pnlTotales.Location  = new System.Drawing.Point(5, 75);
            this.pnlTotales.Size      = new System.Drawing.Size(1185, 72);
            this.pnlTotales.BackColor = System.Drawing.Color.FromArgb(240, 242, 250);
            this.pnlTotales.Anchor    = System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right;

            this.pnlCard1.Location  = new System.Drawing.Point(5, 4);
            this.pnlCard1.Size      = new System.Drawing.Size(205, 64);
            this.pnlCard1.BackColor = System.Drawing.Color.White;
            this.lblTitleCard1.Text      = "Total Ítems";
            this.lblTitleCard1.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblTitleCard1.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleCard1.Location  = new System.Drawing.Point(8, 6);
            this.lblTitleCard1.Size      = new System.Drawing.Size(100, 16);
            this.lblTotalItems.Text      = "0";
            this.lblTotalItems.Font      = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalItems.ForeColor = darkNavy;
            this.lblTotalItems.Location  = new System.Drawing.Point(8, 22);
            this.lblTotalItems.Size      = new System.Drawing.Size(190, 38);
            this.pnlCard1.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitleCard1, this.lblTotalItems });

            this.pnlCard2.Location  = new System.Drawing.Point(215, 4);
            this.pnlCard2.Size      = new System.Drawing.Size(205, 64);
            this.pnlCard2.BackColor = System.Drawing.Color.FromArgb(230, 255, 230);
            this.lblTitleCard2.Text      = "Adjudicados";
            this.lblTitleCard2.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblTitleCard2.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleCard2.Location  = new System.Drawing.Point(8, 6);
            this.lblTitleCard2.Size      = new System.Drawing.Size(100, 16);
            this.lblAdjudicados.Text      = "0";
            this.lblAdjudicados.Font      = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblAdjudicados.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblAdjudicados.Location  = new System.Drawing.Point(8, 22);
            this.lblAdjudicados.Size      = new System.Drawing.Size(190, 38);
            this.pnlCard2.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitleCard2, this.lblAdjudicados });

            this.pnlCard3.Location  = new System.Drawing.Point(425, 4);
            this.pnlCard3.Size      = new System.Drawing.Size(205, 64);
            this.pnlCard3.BackColor = System.Drawing.Color.FromArgb(252, 252, 252);
            this.lblTitleCard3.Text      = "Desiertos";
            this.lblTitleCard3.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblTitleCard3.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleCard3.Location  = new System.Drawing.Point(8, 6);
            this.lblTitleCard3.Size      = new System.Drawing.Size(100, 16);
            this.lblDesiertos.Text      = "0";
            this.lblDesiertos.Font      = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblDesiertos.ForeColor = System.Drawing.Color.Gray;
            this.lblDesiertos.Location  = new System.Drawing.Point(8, 22);
            this.lblDesiertos.Size      = new System.Drawing.Size(190, 38);
            this.pnlCard3.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitleCard3, this.lblDesiertos });

            this.pnlCard4.Location  = new System.Drawing.Point(635, 4);
            this.pnlCard4.Size      = new System.Drawing.Size(255, 64);
            this.pnlCard4.BackColor = System.Drawing.Color.FromArgb(230, 240, 255);
            this.lblTitleCard4.Text      = "Monto Recaudado";
            this.lblTitleCard4.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblTitleCard4.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleCard4.Location  = new System.Drawing.Point(8, 6);
            this.lblTitleCard4.Size      = new System.Drawing.Size(160, 16);
            this.lblMontoRecaudado.Text      = "$ 0,00";
            this.lblMontoRecaudado.Font      = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMontoRecaudado.ForeColor = darkNavy;
            this.lblMontoRecaudado.Location  = new System.Drawing.Point(8, 22);
            this.lblMontoRecaudado.Size      = new System.Drawing.Size(240, 38);
            this.pnlCard4.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitleCard4, this.lblMontoRecaudado });

            this.pnlCard5.Location  = new System.Drawing.Point(895, 4);
            this.pnlCard5.Size      = new System.Drawing.Size(283, 64);
            this.pnlCard5.BackColor = System.Drawing.Color.FromArgb(210, 235, 255);
            this.lblTitleCard5.Text      = "Suscriptores Notificados";
            this.lblTitleCard5.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblTitleCard5.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleCard5.Location  = new System.Drawing.Point(8, 6);
            this.lblTitleCard5.Size      = new System.Drawing.Size(200, 16);
            this.lblNotificados.Text      = "0";
            this.lblNotificados.Font      = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblNotificados.ForeColor = System.Drawing.Color.FromArgb(0, 80, 160);
            this.lblNotificados.Location  = new System.Drawing.Point(8, 22);
            this.lblNotificados.Size      = new System.Drawing.Size(268, 38);
            this.pnlCard5.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitleCard5, this.lblNotificados });

            this.pnlTotales.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pnlCard1, this.pnlCard2, this.pnlCard3, this.pnlCard4, this.pnlCard5
            });

            // ── grpCatalogo — TreeView jerárquico (ocupa todo el espacio restante)
            this.grpCatalogo.Text     = "Catálogo — Unidades de Venta";
            this.grpCatalogo.Font     = boldFont;
            this.grpCatalogo.Location = new System.Drawing.Point(5, 152);
            this.grpCatalogo.Size     = new System.Drawing.Size(1185, 684);
            this.grpCatalogo.Anchor   = System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right
                                      | System.Windows.Forms.AnchorStyles.Bottom;

            this.tvCatalogo.Location      = new System.Drawing.Point(8, 20);
            this.tvCatalogo.Size          = new System.Drawing.Size(1167, 654);
            this.tvCatalogo.Anchor        = System.Windows.Forms.AnchorStyles.Top
                                          | System.Windows.Forms.AnchorStyles.Left
                                          | System.Windows.Forms.AnchorStyles.Right
                                          | System.Windows.Forms.AnchorStyles.Bottom;
            this.tvCatalogo.Font          = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tvCatalogo.ShowLines     = true;
            this.tvCatalogo.ShowPlusMinus = true;
            this.tvCatalogo.FullRowSelect = true;
            this.tvCatalogo.HideSelection = false;
            this.tvCatalogo.ItemHeight    = 20;

            this.grpCatalogo.Controls.Add(this.tvCatalogo);

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1200, 880);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.grpFiltros,
                this.pnlTotales,
                this.grpCatalogo,
                this.statusStrip
            });
            this.Font          = new System.Drawing.Font("Segoe UI", 9F);
            this.Name          = "FrmReporteJornada";
            this.Text          = "Reporte Consolidado de Jornada (RF-13)";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState   = System.Windows.Forms.FormWindowState.Maximized;

            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.grpFiltros.ResumeLayout(false);
            this.pnlTotales.ResumeLayout(false);
            this.pnlCard1.ResumeLayout(false);
            this.pnlCard2.ResumeLayout(false);
            this.pnlCard3.ResumeLayout(false);
            this.pnlCard4.ResumeLayout(false);
            this.pnlCard5.ResumeLayout(false);
            this.grpCatalogo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.StatusStrip           statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel  lblStatus;
        private System.Windows.Forms.GroupBox              grpFiltros;
        private System.Windows.Forms.Label                 lblEstado;
        private System.Windows.Forms.ComboBox              cboEstado;
        private System.Windows.Forms.Label                 lblTipo;
        private System.Windows.Forms.ComboBox              cboTipo;
        private System.Windows.Forms.CheckBox              chkSoloRaiz;
        private System.Windows.Forms.Button                btnGenerar;
        private System.Windows.Forms.Button                btnLimpiar;
        private System.Windows.Forms.Button                btnExportarPDF;
        private System.Windows.Forms.Label                 lblConteo;
        private System.Windows.Forms.Panel                 pnlTotales;
        private System.Windows.Forms.Panel                 pnlCard1;
        private System.Windows.Forms.Label                 lblTitleCard1;
        private System.Windows.Forms.Label                 lblTotalItems;
        private System.Windows.Forms.Panel                 pnlCard2;
        private System.Windows.Forms.Label                 lblTitleCard2;
        private System.Windows.Forms.Label                 lblAdjudicados;
        private System.Windows.Forms.Panel                 pnlCard3;
        private System.Windows.Forms.Label                 lblTitleCard3;
        private System.Windows.Forms.Label                 lblDesiertos;
        private System.Windows.Forms.Panel                 pnlCard4;
        private System.Windows.Forms.Label                 lblTitleCard4;
        private System.Windows.Forms.Label                 lblMontoRecaudado;
        private System.Windows.Forms.Panel                 pnlCard5;
        private System.Windows.Forms.Label                 lblTitleCard5;
        private System.Windows.Forms.Label                 lblNotificados;
        private System.Windows.Forms.GroupBox              grpCatalogo;
        private System.Windows.Forms.TreeView              tvCatalogo;
    }
}
