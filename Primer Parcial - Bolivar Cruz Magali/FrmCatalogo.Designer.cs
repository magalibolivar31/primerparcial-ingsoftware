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
            this.dgvDetalle       = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)this.splitContainer).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();

            // ---- toolStrip ----
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

            // ---- splitContainer ----
            this.splitContainer.Dock             = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.SplitterDistance = 320;
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            this.splitContainer.Panel2.Controls.Add(this.dgvDetalle);

            // ---- treeView ----
            this.treeView.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font      = new System.Drawing.Font("Consolas", 9F);
            this.treeView.BackColor = System.Drawing.Color.FromArgb(250, 250, 255);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);

            // ---- dgvDetalle ----
            this.dgvDetalle.Dock                  = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.ReadOnly               = true;
            this.dgvDetalle.AllowUserToAddRows     = false;
            this.dgvDetalle.AllowUserToDeleteRows  = false;
            this.dgvDetalle.AutoGenerateColumns    = false;
            this.dgvDetalle.SelectionMode          = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.MultiSelect            = false;
            this.dgvDetalle.RowHeadersVisible      = false;
            this.dgvDetalle.BackgroundColor        = System.Drawing.Color.White;
            this.dgvDetalle.BorderStyle            = System.Windows.Forms.BorderStyle.None;
            this.dgvDetalle.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvDetalle.DefaultCellStyle.Font                   = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvDetalle.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 246, 255);

            var colTipo     = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colTipo",     HeaderText = "Tipo",          Width = 80,  ReadOnly = true };
            var colNombre   = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colNombre",   HeaderText = "Nombre",         Width = 230, ReadOnly = true };
            var colPrecio   = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colPrecio",   HeaderText = "Precio / Valor", Width = 120, ReadOnly = true, DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight, ForeColor = System.Drawing.Color.DarkGreen } };
            var colCategoria= new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colCategoria",HeaderText = "Categoría",      Width = 110, ReadOnly = true };
            var colEstado   = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colEstado",   HeaderText = "Estado físico",  Width = 100, ReadOnly = true };
            var colUbicacion= new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colUbicacion",HeaderText = "Ubicación",      Width = 200, ReadOnly = true };
            var colFecha    = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colFecha",    HeaderText = "Fecha alta",     Width = 90,  ReadOnly = true };

            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colTipo, colNombre, colPrecio, colCategoria, colEstado, colUbicacion, colFecha
            });

            // ---- Form ----
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1050, 580);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip);
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmCatalogo";
            this.Text  = "Catálogo de Unidades de Venta";

            ((System.ComponentModel.ISupportInitialize)this.splitContainer).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStrip          toolStrip;
        private System.Windows.Forms.ToolStripButton    btnNuevoArticulo;
        private System.Windows.Forms.ToolStripButton    btnNuevoLote;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripButton    btnBaja;
        private System.Windows.Forms.ToolStripButton    btnRefrescar;
        private System.Windows.Forms.SplitContainer     splitContainer;
        private System.Windows.Forms.TreeView           treeView;
        private System.Windows.Forms.DataGridView       dgvDetalle;
    }
}
