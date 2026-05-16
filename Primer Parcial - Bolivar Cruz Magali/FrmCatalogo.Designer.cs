namespace GUI
{
    partial class FrmCatalogo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.toolStrip        = new System.Windows.Forms.ToolStrip();
            this.btnNuevoArticulo = new System.Windows.Forms.ToolStripButton();
            this.btnNuevoLote     = new System.Windows.Forms.ToolStripButton();
            this.sep1             = new System.Windows.Forms.ToolStripSeparator();
            this.btnBaja          = new System.Windows.Forms.ToolStripButton();
            this.btnRefrescar     = new System.Windows.Forms.ToolStripButton();
            this.splitContainer   = new System.Windows.Forms.SplitContainer();
            this.treeView         = new System.Windows.Forms.TreeView();
            this.pnlDetalle       = new System.Windows.Forms.Panel();
            this.lblTipoLbl       = new System.Windows.Forms.Label();
            this.lblTipoVal       = new System.Windows.Forms.Label();
            this.lblNombreLbl     = new System.Windows.Forms.Label();
            this.lblNombreVal     = new System.Windows.Forms.Label();
            this.lblPrecioLbl     = new System.Windows.Forms.Label();
            this.lblPrecioVal     = new System.Windows.Forms.Label();
            this.lblDescLbl       = new System.Windows.Forms.Label();
            this.lblDescVal       = new System.Windows.Forms.Label();
            this.lblFechaLbl      = new System.Windows.Forms.Label();
            this.lblFechaVal      = new System.Windows.Forms.Label();
            this.lblExtraLbl      = new System.Windows.Forms.Label();
            this.lblExtraVal      = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)this.splitContainer).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();

            // toolStrip
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.btnNuevoArticulo, this.btnNuevoLote, this.sep1, this.btnBaja, this.btnRefrescar
            });
            this.toolStrip.Dock      = System.Windows.Forms.DockStyle.Top;
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(235, 237, 255);

            this.btnNuevoArticulo.Text         = "Nuevo Artículo";
            this.btnNuevoArticulo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNuevoArticulo.Click       += new System.EventHandler(this.btnNuevoArticulo_Click);

            this.btnNuevoLote.Text         = "Nuevo Lote";
            this.btnNuevoLote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNuevoLote.Click       += new System.EventHandler(this.btnNuevoLote_Click);

            this.btnBaja.Text         = "Dar de Baja";
            this.btnBaja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBaja.ForeColor    = System.Drawing.Color.DarkRed;
            this.btnBaja.Enabled      = false;
            this.btnBaja.Click       += new System.EventHandler(this.btnBaja_Click);

            this.btnRefrescar.Text         = "Refrescar";
            this.btnRefrescar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefrescar.Click       += new System.EventHandler(this.btnRefrescar_Click);

            // splitContainer
            this.splitContainer.Dock            = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.SplitterDistance = 330;
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            this.splitContainer.Panel2.Controls.Add(this.pnlDetalle);

            // treeView
            this.treeView.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font      = new System.Drawing.Font("Consolas", 9F);
            this.treeView.BackColor = System.Drawing.Color.FromArgb(250, 250, 255);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);

            // pnlDetalle
            this.pnlDetalle.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetalle.BackColor = System.Drawing.Color.White;

            // Helper positions
            var boldFont  = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont  = new System.Drawing.Font("Segoe UI", 9F);
            int lx = 20, vx = 140, lw = 115, vw = 450, gap = 34;
            int y = 20;

            this.lblTipoLbl.Text = "Tipo:";           this.lblTipoLbl.Font = boldFont; this.lblTipoLbl.Location = new System.Drawing.Point(lx, y); this.lblTipoLbl.Size = new System.Drawing.Size(lw, 20);
            this.lblTipoVal.Font = normFont;           this.lblTipoVal.ForeColor = System.Drawing.Color.FromArgb(20, 20, 130); this.lblTipoVal.Location = new System.Drawing.Point(vx, y); this.lblTipoVal.Size = new System.Drawing.Size(vw, 20);
            y += gap;
            this.lblNombreLbl.Text = "Nombre:";       this.lblNombreLbl.Font = boldFont; this.lblNombreLbl.Location = new System.Drawing.Point(lx, y); this.lblNombreLbl.Size = new System.Drawing.Size(lw, 20);
            this.lblNombreVal.Font = normFont;         this.lblNombreVal.Location = new System.Drawing.Point(vx, y); this.lblNombreVal.Size = new System.Drawing.Size(vw, 20);
            y += gap;
            this.lblPrecioLbl.Text = "Precio Base:";  this.lblPrecioLbl.Font = boldFont; this.lblPrecioLbl.Location = new System.Drawing.Point(lx, y); this.lblPrecioLbl.Size = new System.Drawing.Size(lw, 20);
            this.lblPrecioVal.Font = normFont;         this.lblPrecioVal.ForeColor = System.Drawing.Color.DarkGreen; this.lblPrecioVal.Location = new System.Drawing.Point(vx, y); this.lblPrecioVal.Size = new System.Drawing.Size(vw, 20);
            y += gap;
            this.lblDescLbl.Text = "Descripción:";    this.lblDescLbl.Font = boldFont; this.lblDescLbl.Location = new System.Drawing.Point(lx, y); this.lblDescLbl.Size = new System.Drawing.Size(lw, 20);
            this.lblDescVal.Font = normFont;           this.lblDescVal.Location = new System.Drawing.Point(vx, y); this.lblDescVal.Size = new System.Drawing.Size(vw, 40);
            y += 46;
            this.lblFechaLbl.Text = "Fecha Alta:";    this.lblFechaLbl.Font = boldFont; this.lblFechaLbl.Location = new System.Drawing.Point(lx, y); this.lblFechaLbl.Size = new System.Drawing.Size(lw, 20);
            this.lblFechaVal.Font = normFont;          this.lblFechaVal.Location = new System.Drawing.Point(vx, y); this.lblFechaVal.Size = new System.Drawing.Size(vw, 20);
            y += gap;
            this.lblExtraLbl.Text = "Detalle:";       this.lblExtraLbl.Font = boldFont; this.lblExtraLbl.Location = new System.Drawing.Point(lx, y); this.lblExtraLbl.Size = new System.Drawing.Size(lw, 20);
            this.lblExtraVal.Font = normFont;          this.lblExtraVal.Location = new System.Drawing.Point(vx, y); this.lblExtraVal.Size = new System.Drawing.Size(vw + 60, 40);

            this.pnlDetalle.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTipoLbl, this.lblTipoVal,
                this.lblNombreLbl, this.lblNombreVal,
                this.lblPrecioLbl, this.lblPrecioVal,
                this.lblDescLbl, this.lblDescVal,
                this.lblFechaLbl, this.lblFechaVal,
                this.lblExtraLbl, this.lblExtraVal
            });

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(920, 580);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip);
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmCatalogo";
            this.Text  = "Catálogo de Unidades de Venta";

            ((System.ComponentModel.ISupportInitialize)this.splitContainer).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStrip              toolStrip;
        private System.Windows.Forms.ToolStripButton        btnNuevoArticulo;
        private System.Windows.Forms.ToolStripButton        btnNuevoLote;
        private System.Windows.Forms.ToolStripSeparator     sep1;
        private System.Windows.Forms.ToolStripButton        btnBaja;
        private System.Windows.Forms.ToolStripButton        btnRefrescar;
        private System.Windows.Forms.SplitContainer         splitContainer;
        private System.Windows.Forms.TreeView               treeView;
        private System.Windows.Forms.Panel                  pnlDetalle;
        private System.Windows.Forms.Label                  lblTipoLbl;
        private System.Windows.Forms.Label                  lblTipoVal;
        private System.Windows.Forms.Label                  lblNombreLbl;
        private System.Windows.Forms.Label                  lblNombreVal;
        private System.Windows.Forms.Label                  lblPrecioLbl;
        private System.Windows.Forms.Label                  lblPrecioVal;
        private System.Windows.Forms.Label                  lblDescLbl;
        private System.Windows.Forms.Label                  lblDescVal;
        private System.Windows.Forms.Label                  lblFechaLbl;
        private System.Windows.Forms.Label                  lblFechaVal;
        private System.Windows.Forms.Label                  lblExtraLbl;
        private System.Windows.Forms.Label                  lblExtraVal;
    }
}
