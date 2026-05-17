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
            // ── Declaraciones ──────────────────────────────────────────────────
            this.toolStrip          = new System.Windows.Forms.ToolStrip();
            this.btnNuevoArticulo   = new System.Windows.Forms.ToolStripButton();
            this.btnNuevoLote       = new System.Windows.Forms.ToolStripButton();
            this.sep1               = new System.Windows.Forms.ToolStripSeparator();
            this.btnBaja            = new System.Windows.Forms.ToolStripButton();
            this.btnModificar       = new System.Windows.Forms.ToolStripButton();
            this.btnVerDetalle      = new System.Windows.Forms.ToolStripButton();
            this.sep2               = new System.Windows.Forms.ToolStripSeparator();
            this.btnIniciarSubasta  = new System.Windows.Forms.ToolStripButton();
            this.btnVerEnBitacora   = new System.Windows.Forms.ToolStripButton();
            this.btnRefrescar       = new System.Windows.Forms.ToolStripButton();

            this.pnlFiltros         = new System.Windows.Forms.Panel();
            this.lblFiltroEstado    = new System.Windows.Forms.Label();
            this.cmbFiltroEstado    = new System.Windows.Forms.ComboBox();
            this.lblFiltroTipo      = new System.Windows.Forms.Label();
            this.cmbFiltroTipo      = new System.Windows.Forms.ComboBox();
            this.lblFiltroBuscar    = new System.Windows.Forms.Label();
            this.txtBusqueda        = new System.Windows.Forms.TextBox();
            this.btnLimpiarFiltros  = new System.Windows.Forms.Button();
            this.lblContador        = new System.Windows.Forms.Label();

            this.dgv                = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)this.dgv).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.SuspendLayout();

            // ── toolStrip ──────────────────────────────────────────────────────
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.btnNuevoArticulo, this.btnNuevoLote,
                this.sep1,
                this.btnBaja, this.btnModificar, this.btnVerDetalle,
                this.sep2,
                this.btnIniciarSubasta, this.btnVerEnBitacora,
                this.btnRefrescar
            });
            this.toolStrip.Dock      = System.Windows.Forms.DockStyle.Top;
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(235, 237, 255);

            this.btnNuevoArticulo.Text         = "Nuevo Artículo";
            this.btnNuevoArticulo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNuevoArticulo.Click       += new System.EventHandler(this.btnNuevoArticulo_Click);

            this.btnNuevoLote.Text         = "Nuevo Lote";
            this.btnNuevoLote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNuevoLote.Click       += new System.EventHandler(this.btnNuevoLote_Click);

            this.btnBaja.Text         = "Retirar";
            this.btnBaja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBaja.ForeColor    = System.Drawing.Color.DarkRed;
            this.btnBaja.Enabled      = false;
            this.btnBaja.Click       += new System.EventHandler(this.btnBaja_Click);

            this.btnModificar.Text         = "Modificar";
            this.btnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnModificar.ForeColor    = System.Drawing.Color.FromArgb(0, 100, 180);
            this.btnModificar.Enabled      = false;
            this.btnModificar.Click       += new System.EventHandler(this.btnModificar_Click);

            this.btnVerDetalle.Text         = "Ver Detalle (Composite)";
            this.btnVerDetalle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnVerDetalle.ForeColor    = System.Drawing.Color.FromArgb(0, 80, 160);
            this.btnVerDetalle.Font         = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVerDetalle.Enabled      = false;
            this.btnVerDetalle.Click       += new System.EventHandler(this.btnVerDetalle_Click);

            this.btnIniciarSubasta.Text         = "Iniciar Subasta";
            this.btnIniciarSubasta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnIniciarSubasta.ForeColor    = System.Drawing.Color.FromArgb(0, 120, 50);
            this.btnIniciarSubasta.Font         = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnIniciarSubasta.Visible      = false;
            this.btnIniciarSubasta.Click       += new System.EventHandler(this.btnIniciarSubasta_Click);

            this.btnVerEnBitacora.Text         = "Ver en Bitácora";
            this.btnVerEnBitacora.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnVerEnBitacora.ForeColor    = System.Drawing.Color.FromArgb(80, 0, 140);
            this.btnVerEnBitacora.Visible      = false;
            this.btnVerEnBitacora.Click       += new System.EventHandler(this.btnVerEnBitacora_Click);

            this.btnRefrescar.Text         = "Refrescar";
            this.btnRefrescar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefrescar.Click       += new System.EventHandler(this.btnRefrescar_Click);

            // ── pnlFiltros ─────────────────────────────────────────────────────
            this.pnlFiltros.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Height    = 38;
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            this.pnlFiltros.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFiltroEstado, this.cmbFiltroEstado,
                this.lblFiltroTipo,   this.cmbFiltroTipo,
                this.lblFiltroBuscar, this.txtBusqueda,
                this.btnLimpiarFiltros, this.lblContador
            });

            this.lblFiltroEstado.Text      = "Estado:";
            this.lblFiltroEstado.AutoSize  = true;
            this.lblFiltroEstado.Location  = new System.Drawing.Point(8, 12);
            this.lblFiltroEstado.Font      = new System.Drawing.Font("Segoe UI", 9F);

            this.cmbFiltroEstado.Location      = new System.Drawing.Point(58, 8);
            this.cmbFiltroEstado.Size          = new System.Drawing.Size(112, 23);
            this.cmbFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroEstado.Font          = new System.Drawing.Font("Segoe UI", 9F);

            this.lblFiltroTipo.Text     = "Tipo:";
            this.lblFiltroTipo.AutoSize = true;
            this.lblFiltroTipo.Location = new System.Drawing.Point(180, 12);
            this.lblFiltroTipo.Font     = new System.Drawing.Font("Segoe UI", 9F);

            this.cmbFiltroTipo.Location      = new System.Drawing.Point(212, 8);
            this.cmbFiltroTipo.Size          = new System.Drawing.Size(90, 23);
            this.cmbFiltroTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTipo.Font          = new System.Drawing.Font("Segoe UI", 9F);

            this.lblFiltroBuscar.Text     = "Buscar:";
            this.lblFiltroBuscar.AutoSize = true;
            this.lblFiltroBuscar.Location = new System.Drawing.Point(314, 12);
            this.lblFiltroBuscar.Font     = new System.Drawing.Font("Segoe UI", 9F);

            this.txtBusqueda.Location = new System.Drawing.Point(360, 8);
            this.txtBusqueda.Size     = new System.Drawing.Size(200, 23);
            this.txtBusqueda.Font     = new System.Drawing.Font("Segoe UI", 9F);

            this.btnLimpiarFiltros.Text      = "Limpiar";
            this.btnLimpiarFiltros.Location  = new System.Drawing.Point(570, 7);
            this.btnLimpiarFiltros.Size      = new System.Drawing.Size(65, 24);
            this.btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarFiltros.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnLimpiarFiltros.Click    += new System.EventHandler(this.btnLimpiarFiltros_Click);

            this.lblContador.Text      = "0 registro(s)";
            this.lblContador.AutoSize  = false;
            this.lblContador.Size      = new System.Drawing.Size(160, 38);
            this.lblContador.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblContador.ForeColor = System.Drawing.Color.Gray;
            this.lblContador.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblContador.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblContador.Location  = new System.Drawing.Point(720, 0);

            // ── dgv ────────────────────────────────────────────────────────────
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
            this.dgv.RowTemplate.Height     = 26;
            this.dgv.ColumnHeadersDefaultCellStyle.Font       = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgv.DefaultCellStyle.Font                    = new System.Drawing.Font("Segoe UI", 9F);
            this.dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 246, 255);
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);

            var colMonedaDerecha = new System.Windows.Forms.DataGridViewCellStyle {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
            };

            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                new System.Windows.Forms.DataGridViewTextBoxColumn {
                    Name = "colTipo", HeaderText = "Tipo", Width = 75
                },
                new System.Windows.Forms.DataGridViewTextBoxColumn {
                    Name = "colNombre", HeaderText = "Nombre", Width = 210
                },
                new System.Windows.Forms.DataGridViewTextBoxColumn {
                    Name = "colPrecio", HeaderText = "Precio Base", Width = 110,
                    DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle {
                        Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight,
                        ForeColor = System.Drawing.Color.DarkGreen
                    }
                },
                new System.Windows.Forms.DataGridViewTextBoxColumn {
                    Name = "colPrecioVigente", HeaderText = "Precio Vigente", Width = 115,
                    DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle {
                        Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight,
                        ForeColor = System.Drawing.Color.FromArgb(180, 80, 0)
                    }
                },
                new System.Windows.Forms.DataGridViewTextBoxColumn {
                    Name = "colCantItems", HeaderText = "Cant. Ítems", Width = 85,
                    DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle {
                        Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
                    }
                },
                new System.Windows.Forms.DataGridViewTextBoxColumn {
                    Name = "colEstado", HeaderText = "Estado", Width = 105
                },
            });

            // ── Form ───────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(900, 520);
            // Orden en Controls: dgv primero (Fill), luego pnlFiltros (Top), luego toolStrip (Top)
            // WinForms apila Top-docked de atrás hacia adelante, así quedan: toolStrip → pnlFiltros → dgv
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.toolStrip);
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmCatalogo";
            this.Text  = "Catálogo — Patrón Composite";

            ((System.ComponentModel.ISupportInitialize)this.dgv).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ── Campos ─────────────────────────────────────────────────────────────
        private System.Windows.Forms.ToolStrip          toolStrip;
        private System.Windows.Forms.ToolStripButton    btnNuevoArticulo;
        private System.Windows.Forms.ToolStripButton    btnNuevoLote;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripButton    btnBaja;
        private System.Windows.Forms.ToolStripButton    btnModificar;
        private System.Windows.Forms.ToolStripButton    btnVerDetalle;
        private System.Windows.Forms.ToolStripSeparator sep2;
        private System.Windows.Forms.ToolStripButton    btnIniciarSubasta;
        private System.Windows.Forms.ToolStripButton    btnVerEnBitacora;
        private System.Windows.Forms.ToolStripButton    btnRefrescar;

        private System.Windows.Forms.Panel              pnlFiltros;
        private System.Windows.Forms.Label              lblFiltroEstado;
        private System.Windows.Forms.ComboBox           cmbFiltroEstado;
        private System.Windows.Forms.Label              lblFiltroTipo;
        private System.Windows.Forms.ComboBox           cmbFiltroTipo;
        private System.Windows.Forms.Label              lblFiltroBuscar;
        private System.Windows.Forms.TextBox            txtBusqueda;
        private System.Windows.Forms.Button             btnLimpiarFiltros;
        private System.Windows.Forms.Label              lblContador;

        private System.Windows.Forms.DataGridView       dgv;
    }
}
