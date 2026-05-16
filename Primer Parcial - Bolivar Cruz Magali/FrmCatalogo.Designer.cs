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
            this.dgv              = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)this.dgv).BeginInit();
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

            // ---- dgv ----
            this.dgv.Dock                  = System.Windows.Forms.DockStyle.Fill;
            this.dgv.ReadOnly               = true;
            this.dgv.AllowUserToAddRows     = false;
            this.dgv.AllowUserToDeleteRows  = false;
            this.dgv.AutoGenerateColumns    = false;
            this.dgv.SelectionMode          = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.MultiSelect            = false;
            this.dgv.RowHeadersVisible      = false;
            this.dgv.BackgroundColor        = System.Drawing.Color.White;
            this.dgv.BorderStyle            = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersDefaultCellStyle.Font       = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgv.DefaultCellStyle.Font                    = new System.Drawing.Font("Segoe UI", 9F);
            this.dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 246, 255);
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);

            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colTipo",      HeaderText = "Tipo",           Width = 80  },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colNombre",    HeaderText = "Nombre",          Width = 230 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colPrecio",    HeaderText = "Precio / Valor",  Width = 130,
                    DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle {
                        Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight,
                        ForeColor = System.Drawing.Color.DarkGreen } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colEstadoOp",  HeaderText = "Estado",          Width = 110 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colCategoria", HeaderText = "Categoría",      Width = 100 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colEstadoFis", HeaderText = "Estado físico",  Width = 100 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colUbicacion", HeaderText = "Ubicación",      Width = 190 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colLotePadre", HeaderText = "Lote padre",     Width = 150 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colFecha",     HeaderText = "Fecha alta",     Width = 90  },
            });

            // ---- Form ----
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1220, 540);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.toolStrip);
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmCatalogo";
            this.Text  = "Catálogo de Unidades de Venta";

            ((System.ComponentModel.ISupportInitialize)this.dgv).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStrip          toolStrip;
        private System.Windows.Forms.ToolStripButton    btnNuevoArticulo;
        private System.Windows.Forms.ToolStripButton    btnNuevoLote;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripButton    btnBaja;
        private System.Windows.Forms.ToolStripButton    btnRefrescar;
        private System.Windows.Forms.DataGridView       dgv;
    }
}
