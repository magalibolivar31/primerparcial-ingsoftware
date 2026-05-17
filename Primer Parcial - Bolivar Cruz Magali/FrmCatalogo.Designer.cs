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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNuevoArticulo = new System.Windows.Forms.ToolStripButton();
            this.btnNuevoLote = new System.Windows.Forms.ToolStripButton();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBaja = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnVerDetalle = new System.Windows.Forms.ToolStripButton();
            this.sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIniciarSubasta = new System.Windows.Forms.ToolStripButton();
            this.btnVerEnBitacora = new System.Windows.Forms.ToolStripButton();
            this.btnRefrescar = new System.Windows.Forms.ToolStripButton();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.lblFiltroEstado = new System.Windows.Forms.Label();
            this.cmbFiltroEstado = new System.Windows.Forms.ComboBox();
            this.lblFiltroTipo = new System.Windows.Forms.Label();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.lblFiltroBuscar = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.lblContador = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecioVigente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevoArticulo,
            this.btnNuevoLote,
            this.sep1,
            this.btnBaja,
            this.btnModificar,
            this.btnVerDetalle,
            this.sep2,
            this.btnIniciarSubasta,
            this.btnVerEnBitacora,
            this.btnRefrescar});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(900, 34);
            this.toolStrip.TabIndex = 2;
            // 
            // btnNuevoArticulo
            // 
            this.btnNuevoArticulo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNuevoArticulo.Name = "btnNuevoArticulo";
            this.btnNuevoArticulo.Size = new System.Drawing.Size(134, 29);
            this.btnNuevoArticulo.Text = "Nuevo Artículo";
            this.btnNuevoArticulo.Click += new System.EventHandler(this.btnNuevoArticulo_Click);
            // 
            // btnNuevoLote
            // 
            this.btnNuevoLote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNuevoLote.Name = "btnNuevoLote";
            this.btnNuevoLote.Size = new System.Drawing.Size(107, 29);
            this.btnNuevoLote.Text = "Nuevo Lote";
            this.btnNuevoLote.Click += new System.EventHandler(this.btnNuevoLote_Click);
            // 
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(6, 34);
            // 
            // btnBaja
            // 
            this.btnBaja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBaja.Enabled = false;
            this.btnBaja.ForeColor = System.Drawing.Color.DarkRed;
            this.btnBaja.Name = "btnBaja";
            this.btnBaja.Size = new System.Drawing.Size(66, 29);
            this.btnBaja.Text = "Retirar";
            this.btnBaja.Click += new System.EventHandler(this.btnBaja_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnModificar.Enabled = false;
            this.btnModificar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(91, 29);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnVerDetalle
            // 
            this.btnVerDetalle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnVerDetalle.Enabled = false;
            this.btnVerDetalle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVerDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(160)))));
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(109, 29);
            this.btnVerDetalle.Text = "Ver Detalle";
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            // 
            // sep2
            // 
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(6, 34);
            // 
            // btnIniciarSubasta
            // 
            this.btnIniciarSubasta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnIniciarSubasta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnIniciarSubasta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(50)))));
            this.btnIniciarSubasta.Name = "btnIniciarSubasta";
            this.btnIniciarSubasta.Size = new System.Drawing.Size(141, 29);
            this.btnIniciarSubasta.Text = "Iniciar Subasta";
            this.btnIniciarSubasta.Visible = false;
            this.btnIniciarSubasta.Click += new System.EventHandler(this.btnIniciarSubasta_Click);
            // 
            // btnVerEnBitacora
            // 
            this.btnVerEnBitacora.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnVerEnBitacora.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(140)))));
            this.btnVerEnBitacora.Name = "btnVerEnBitacora";
            this.btnVerEnBitacora.Size = new System.Drawing.Size(133, 29);
            this.btnVerEnBitacora.Text = "Ver en Bitácora";
            this.btnVerEnBitacora.Visible = false;
            this.btnVerEnBitacora.Click += new System.EventHandler(this.btnVerEnBitacora_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(87, 29);
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.pnlFiltros.Controls.Add(this.lblFiltroEstado);
            this.pnlFiltros.Controls.Add(this.cmbFiltroEstado);
            this.pnlFiltros.Controls.Add(this.lblFiltroTipo);
            this.pnlFiltros.Controls.Add(this.cmbFiltroTipo);
            this.pnlFiltros.Controls.Add(this.lblFiltroBuscar);
            this.pnlFiltros.Controls.Add(this.txtBusqueda);
            this.pnlFiltros.Controls.Add(this.btnLimpiarFiltros);
            this.pnlFiltros.Controls.Add(this.lblContador);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 34);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(900, 38);
            this.pnlFiltros.TabIndex = 1;
            // 
            // lblFiltroEstado
            // 
            this.lblFiltroEstado.AutoSize = true;
            this.lblFiltroEstado.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFiltroEstado.Location = new System.Drawing.Point(8, 12);
            this.lblFiltroEstado.Name = "lblFiltroEstado";
            this.lblFiltroEstado.Size = new System.Drawing.Size(70, 25);
            this.lblFiltroEstado.TabIndex = 0;
            this.lblFiltroEstado.Text = "Estado:";
            // 
            // cmbFiltroEstado
            // 
            this.cmbFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroEstado.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFiltroEstado.Location = new System.Drawing.Point(58, 8);
            this.cmbFiltroEstado.Name = "cmbFiltroEstado";
            this.cmbFiltroEstado.Size = new System.Drawing.Size(112, 33);
            this.cmbFiltroEstado.TabIndex = 1;
            // 
            // lblFiltroTipo
            // 
            this.lblFiltroTipo.AutoSize = true;
            this.lblFiltroTipo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFiltroTipo.Location = new System.Drawing.Point(180, 12);
            this.lblFiltroTipo.Name = "lblFiltroTipo";
            this.lblFiltroTipo.Size = new System.Drawing.Size(51, 25);
            this.lblFiltroTipo.TabIndex = 2;
            this.lblFiltroTipo.Text = "Tipo:";
            // 
            // cmbFiltroTipo
            // 
            this.cmbFiltroTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTipo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFiltroTipo.Location = new System.Drawing.Point(212, 8);
            this.cmbFiltroTipo.Name = "cmbFiltroTipo";
            this.cmbFiltroTipo.Size = new System.Drawing.Size(90, 33);
            this.cmbFiltroTipo.TabIndex = 3;
            // 
            // lblFiltroBuscar
            // 
            this.lblFiltroBuscar.AutoSize = true;
            this.lblFiltroBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFiltroBuscar.Location = new System.Drawing.Point(314, 12);
            this.lblFiltroBuscar.Name = "lblFiltroBuscar";
            this.lblFiltroBuscar.Size = new System.Drawing.Size(67, 25);
            this.lblFiltroBuscar.TabIndex = 4;
            this.lblFiltroBuscar.Text = "Buscar:";
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBusqueda.Location = new System.Drawing.Point(360, 8);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(200, 31);
            this.txtBusqueda.TabIndex = 5;
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarFiltros.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(570, 7);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(65, 24);
            this.btnLimpiarFiltros.TabIndex = 6;
            this.btnLimpiarFiltros.Text = "Limpiar";
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // lblContador
            // 
            this.lblContador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblContador.ForeColor = System.Drawing.Color.Gray;
            this.lblContador.Location = new System.Drawing.Point(1420, 0);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(160, 38);
            this.lblContador.TabIndex = 7;
            this.lblContador.Text = "0 registro(s)";
            this.lblContador.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.ColumnHeadersHeight = 34;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTipo,
            this.colNombre,
            this.colPrecio,
            this.colPrecioVigente,
            this.colCantItems,
            this.colEstado});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 72);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 62;
            this.dgv.RowTemplate.Height = 26;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(900, 448);
            this.dgv.TabIndex = 0;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // colTipo
            // 
            this.colTipo.HeaderText = "Tipo";
            this.colTipo.MinimumWidth = 8;
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;
            this.colTipo.Width = 75;
            // 
            // colNombre
            // 
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 8;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            this.colNombre.Width = 210;
            // 
            // colPrecio
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkGreen;
            this.colPrecio.DefaultCellStyle = dataGridViewCellStyle3;
            this.colPrecio.HeaderText = "Precio Base";
            this.colPrecio.MinimumWidth = 8;
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.ReadOnly = true;
            this.colPrecio.Width = 110;
            // 
            // colPrecioVigente
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.colPrecioVigente.DefaultCellStyle = dataGridViewCellStyle4;
            this.colPrecioVigente.HeaderText = "Precio Vigente";
            this.colPrecioVigente.MinimumWidth = 8;
            this.colPrecioVigente.Name = "colPrecioVigente";
            this.colPrecioVigente.ReadOnly = true;
            this.colPrecioVigente.Width = 115;
            // 
            // colCantItems
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colCantItems.DefaultCellStyle = dataGridViewCellStyle5;
            this.colCantItems.HeaderText = "Cant. Ítems";
            this.colCantItems.MinimumWidth = 8;
            this.colCantItems.Name = "colCantItems";
            this.colCantItems.ReadOnly = true;
            this.colCantItems.Width = 85;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 8;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            this.colEstado.Width = 105;
            // 
            // FrmCatalogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 520);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmCatalogo";
            this.Text = "Catálogo — Patrón Composite";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecioVigente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
    }
}
