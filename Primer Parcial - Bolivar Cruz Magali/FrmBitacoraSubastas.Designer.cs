namespace GUI
{
    partial class FrmBitacoraSubastas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpFiltros        = new System.Windows.Forms.GroupBox();
            this.lblDesde          = new System.Windows.Forms.Label();
            this.dtpDesde          = new System.Windows.Forms.DateTimePicker();
            this.lblHasta          = new System.Windows.Forms.Label();
            this.dtpHasta          = new System.Windows.Forms.DateTimePicker();
            this.lblEstado         = new System.Windows.Forms.Label();
            this.cboEstado         = new System.Windows.Forms.ComboBox();
            this.lblTipo           = new System.Windows.Forms.Label();
            this.cboTipo           = new System.Windows.Forms.ComboBox();
            this.btnBuscar         = new System.Windows.Forms.Button();
            this.btnLimpiar        = new System.Windows.Forms.Button();
            this.btnDescargarPDF   = new System.Windows.Forms.Button();
            this.lblProducto       = new System.Windows.Forms.Label();
            this.txtProducto       = new System.Windows.Forms.TextBox();
            this.lblIdProducto     = new System.Windows.Forms.Label();
            this.nudIdProducto     = new System.Windows.Forms.NumericUpDown();
            this.lblOfertante      = new System.Windows.Forms.Label();
            this.txtOfertante      = new System.Windows.Forms.TextBox();
            this.lblIdOfertante    = new System.Windows.Forms.Label();
            this.nudIdOfertante    = new System.Windows.Forms.NumericUpDown();
            this.lblMontoMin       = new System.Windows.Forms.Label();
            this.nudMontoMin       = new System.Windows.Forms.NumericUpDown();
            this.lblMontoMax       = new System.Windows.Forms.Label();
            this.nudMontoMax       = new System.Windows.Forms.NumericUpDown();
            this.chkSoloGanadores  = new System.Windows.Forms.CheckBox();
            this.lblConteo         = new System.Windows.Forms.Label();
            this.grpSubastas       = new System.Windows.Forms.GroupBox();
            this.dgvSubastas       = new System.Windows.Forms.DataGridView();
            this.grpOfertas        = new System.Windows.Forms.GroupBox();
            this.lblSumario        = new System.Windows.Forms.Label();
            this.dgvPujas          = new System.Windows.Forms.DataGridView();

            this.grpFiltros.SuspendLayout();
            this.grpSubastas.SuspendLayout();
            this.grpOfertas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudIdProducto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudIdOfertante).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMontoMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMontoMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvSubastas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvPujas).BeginInit();
            this.SuspendLayout();

            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont = new System.Drawing.Font("Segoe UI", 9.5F);

            // ── grpFiltros ────────────────────────────────────────────────────
            this.grpFiltros.Text     = "Filtros de búsqueda";
            this.grpFiltros.Font     = boldFont;
            this.grpFiltros.Location = new System.Drawing.Point(5, 5);
            this.grpFiltros.Size     = new System.Drawing.Size(1045, 162);
            this.grpFiltros.Anchor   = System.Windows.Forms.AnchorStyles.Top
                                     | System.Windows.Forms.AnchorStyles.Left
                                     | System.Windows.Forms.AnchorStyles.Right;

            // ── Row 1: Desde · Hasta · Estado · Tipo · Botones ────────────────
            this.lblDesde.Text      = "Desde:";
            this.lblDesde.Font      = boldFont;
            this.lblDesde.Location  = new System.Drawing.Point(10, 26);
            this.lblDesde.Size      = new System.Drawing.Size(50, 20);
            this.lblDesde.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.dtpDesde.Format   = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Font     = normFont;
            this.dtpDesde.Location = new System.Drawing.Point(63, 24);
            this.dtpDesde.Size     = new System.Drawing.Size(105, 22);

            this.lblHasta.Text      = "Hasta:";
            this.lblHasta.Font      = boldFont;
            this.lblHasta.Location  = new System.Drawing.Point(178, 26);
            this.lblHasta.Size      = new System.Drawing.Size(48, 20);
            this.lblHasta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.dtpHasta.Format   = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Font     = normFont;
            this.dtpHasta.Location = new System.Drawing.Point(229, 24);
            this.dtpHasta.Size     = new System.Drawing.Size(105, 22);

            this.lblEstado.Text      = "Estado:";
            this.lblEstado.Font      = boldFont;
            this.lblEstado.Location  = new System.Drawing.Point(344, 26);
            this.lblEstado.Size      = new System.Drawing.Size(54, 20);
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cboEstado.Font          = normFont;
            this.cboEstado.Location      = new System.Drawing.Point(401, 24);
            this.cboEstado.Size          = new System.Drawing.Size(110, 26);
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Items.AddRange(new object[] { "Todos", "Activa", "Cerrada" });
            this.cboEstado.SelectedIndex = 0;

            this.lblTipo.Text      = "Tipo:";
            this.lblTipo.Font      = boldFont;
            this.lblTipo.Location  = new System.Drawing.Point(521, 26);
            this.lblTipo.Size      = new System.Drawing.Size(40, 20);
            this.lblTipo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cboTipo.Font          = normFont;
            this.cboTipo.Location      = new System.Drawing.Point(564, 24);
            this.cboTipo.Size          = new System.Drawing.Size(120, 26);
            this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipo.Items.AddRange(new object[] { "Todos", "Artículo", "Lote" });
            this.cboTipo.SelectedIndex = 0;

            this.btnBuscar.Text      = "Buscar";
            this.btnBuscar.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Location  = new System.Drawing.Point(700, 22);
            this.btnBuscar.Size      = new System.Drawing.Size(90, 28);
            this.btnBuscar.Click    += new System.EventHandler(this.btnBuscar_Click);

            this.btnLimpiar.Text      = "Limpiar";
            this.btnLimpiar.Font      = normFont;
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.Location  = new System.Drawing.Point(798, 22);
            this.btnLimpiar.Size      = new System.Drawing.Size(90, 28);
            this.btnLimpiar.Click    += new System.EventHandler(this.btnLimpiar_Click);

            this.btnDescargarPDF.Text      = "⬇ Descargar PDF";
            this.btnDescargarPDF.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDescargarPDF.BackColor = System.Drawing.Color.FromArgb(180, 30, 30);
            this.btnDescargarPDF.ForeColor = System.Drawing.Color.White;
            this.btnDescargarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargarPDF.FlatAppearance.BorderSize = 0;
            this.btnDescargarPDF.Location  = new System.Drawing.Point(620, 128);
            this.btnDescargarPDF.Size      = new System.Drawing.Size(150, 28);
            this.btnDescargarPDF.Anchor    = System.Windows.Forms.AnchorStyles.Bottom
                                           | System.Windows.Forms.AnchorStyles.Right;
            this.btnDescargarPDF.Click    += new System.EventHandler(this.btnDescargarPDF_Click);

            // ── Row 2: Producto · ID · Ofertante · ID ─────────────────────────
            this.lblProducto.Text      = "Producto:";
            this.lblProducto.Font      = boldFont;
            this.lblProducto.Location  = new System.Drawing.Point(10, 62);
            this.lblProducto.Size      = new System.Drawing.Size(64, 20);
            this.lblProducto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtProducto.Font     = normFont;
            this.txtProducto.Location = new System.Drawing.Point(77, 60);
            this.txtProducto.Size     = new System.Drawing.Size(185, 22);

            this.lblIdProducto.Text      = "ID:";
            this.lblIdProducto.Font      = boldFont;
            this.lblIdProducto.Location  = new System.Drawing.Point(268, 62);
            this.lblIdProducto.Size      = new System.Drawing.Size(24, 20);
            this.lblIdProducto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.nudIdProducto.Font          = normFont;
            this.nudIdProducto.Location      = new System.Drawing.Point(295, 60);
            this.nudIdProducto.Size          = new System.Drawing.Size(65, 22);
            this.nudIdProducto.Minimum       = 0;
            this.nudIdProducto.Maximum       = 999999;
            this.nudIdProducto.Value         = 0;
            this.nudIdProducto.DecimalPlaces = 0;

            this.lblOfertante.Text      = "Ofertante:";
            this.lblOfertante.Font      = boldFont;
            this.lblOfertante.Location  = new System.Drawing.Point(370, 62);
            this.lblOfertante.Size      = new System.Drawing.Size(72, 20);
            this.lblOfertante.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtOfertante.Font     = normFont;
            this.txtOfertante.Location = new System.Drawing.Point(445, 60);
            this.txtOfertante.Size     = new System.Drawing.Size(185, 22);

            this.lblIdOfertante.Text      = "ID:";
            this.lblIdOfertante.Font      = boldFont;
            this.lblIdOfertante.Location  = new System.Drawing.Point(636, 62);
            this.lblIdOfertante.Size      = new System.Drawing.Size(24, 20);
            this.lblIdOfertante.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.nudIdOfertante.Font          = normFont;
            this.nudIdOfertante.Location      = new System.Drawing.Point(663, 60);
            this.nudIdOfertante.Size          = new System.Drawing.Size(65, 22);
            this.nudIdOfertante.Minimum       = 0;
            this.nudIdOfertante.Maximum       = 999999;
            this.nudIdOfertante.Value         = 0;
            this.nudIdOfertante.DecimalPlaces = 0;

            // ── Row 3: Monto mín · Monto máx · Solo ganadores ─────────────────
            this.lblMontoMin.Text      = "Monto mín.:";
            this.lblMontoMin.Font      = boldFont;
            this.lblMontoMin.Location  = new System.Drawing.Point(10, 98);
            this.lblMontoMin.Size      = new System.Drawing.Size(80, 20);
            this.lblMontoMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.nudMontoMin.Font                = normFont;
            this.nudMontoMin.Location            = new System.Drawing.Point(93, 96);
            this.nudMontoMin.Size                = new System.Drawing.Size(120, 22);
            this.nudMontoMin.Minimum             = 0;
            this.nudMontoMin.Maximum             = 99999999;
            this.nudMontoMin.Value               = 0;
            this.nudMontoMin.DecimalPlaces       = 2;
            this.nudMontoMin.ThousandsSeparator  = true;

            this.lblMontoMax.Text      = "Monto máx.:";
            this.lblMontoMax.Font      = boldFont;
            this.lblMontoMax.Location  = new System.Drawing.Point(223, 98);
            this.lblMontoMax.Size      = new System.Drawing.Size(82, 20);
            this.lblMontoMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.nudMontoMax.Font                = normFont;
            this.nudMontoMax.Location            = new System.Drawing.Point(308, 96);
            this.nudMontoMax.Size                = new System.Drawing.Size(120, 22);
            this.nudMontoMax.Minimum             = 0;
            this.nudMontoMax.Maximum             = 99999999;
            this.nudMontoMax.Value               = 0;
            this.nudMontoMax.DecimalPlaces       = 2;
            this.nudMontoMax.ThousandsSeparator  = true;

            this.chkSoloGanadores.Text     = "Solo ganadores";
            this.chkSoloGanadores.Font     = boldFont;
            this.chkSoloGanadores.Location = new System.Drawing.Point(440, 97);
            this.chkSoloGanadores.Size     = new System.Drawing.Size(145, 22);

            // ── Row 4: Conteo ─────────────────────────────────────────────────
            this.lblConteo.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblConteo.ForeColor = System.Drawing.Color.DimGray;
            this.lblConteo.Location  = new System.Drawing.Point(10, 132);
            this.lblConteo.Size      = new System.Drawing.Size(600, 18);
            this.lblConteo.Text      = "";

            this.grpFiltros.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblDesde,  this.dtpDesde,  this.lblHasta,  this.dtpHasta,
                this.lblEstado, this.cboEstado, this.lblTipo,   this.cboTipo,
                this.btnBuscar, this.btnLimpiar, this.btnDescargarPDF,
                this.lblProducto,  this.txtProducto,  this.lblIdProducto,  this.nudIdProducto,
                this.lblOfertante, this.txtOfertante,  this.lblIdOfertante, this.nudIdOfertante,
                this.lblMontoMin,  this.nudMontoMin,   this.lblMontoMax,    this.nudMontoMax,
                this.chkSoloGanadores, this.lblConteo
            });

            // ── grpSubastas ───────────────────────────────────────────────────
            this.grpSubastas.Text     = "Subastas";
            this.grpSubastas.Font     = boldFont;
            this.grpSubastas.Location = new System.Drawing.Point(5, 172);
            this.grpSubastas.Size     = new System.Drawing.Size(1045, 220);
            this.grpSubastas.Anchor   = System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right;

            this.dgvSubastas.Location          = new System.Drawing.Point(8, 20);
            this.dgvSubastas.Size              = new System.Drawing.Size(1027, 190);
            this.dgvSubastas.Anchor            = System.Windows.Forms.AnchorStyles.Top
                                               | System.Windows.Forms.AnchorStyles.Left
                                               | System.Windows.Forms.AnchorStyles.Right;
            this.dgvSubastas.ReadOnly          = true;
            this.dgvSubastas.RowHeadersVisible = false;
            this.dgvSubastas.Font              = normFont;
            this.dgvSubastas.SelectionMode     = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubastas.SelectionChanged += new System.EventHandler(this.dgvSubastas_SelectionChanged);

            this.grpSubastas.Controls.Add(this.dgvSubastas);

            // ── grpOfertas ────────────────────────────────────────────────────
            this.grpOfertas.Text     = "Ofertas";
            this.grpOfertas.Font     = boldFont;
            this.grpOfertas.Location = new System.Drawing.Point(5, 397);
            this.grpOfertas.Size     = new System.Drawing.Size(1045, 280);
            this.grpOfertas.Anchor   = System.Windows.Forms.AnchorStyles.Top
                                     | System.Windows.Forms.AnchorStyles.Left
                                     | System.Windows.Forms.AnchorStyles.Right
                                     | System.Windows.Forms.AnchorStyles.Bottom;

            this.lblSumario.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblSumario.ForeColor = System.Drawing.Color.DimGray;
            this.lblSumario.Location  = new System.Drawing.Point(8, 20);
            this.lblSumario.Size      = new System.Drawing.Size(1027, 18);
            this.lblSumario.Text      = "Seleccione una subasta para ver las ofertas.";
            this.lblSumario.Anchor    = System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right;

            this.dgvPujas.Location          = new System.Drawing.Point(8, 42);
            this.dgvPujas.Size              = new System.Drawing.Size(1027, 228);
            this.dgvPujas.Anchor            = System.Windows.Forms.AnchorStyles.Top
                                            | System.Windows.Forms.AnchorStyles.Left
                                            | System.Windows.Forms.AnchorStyles.Right
                                            | System.Windows.Forms.AnchorStyles.Bottom;
            this.dgvPujas.ReadOnly          = true;
            this.dgvPujas.RowHeadersVisible = false;
            this.dgvPujas.Font              = normFont;

            this.grpOfertas.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSumario, this.dgvPujas
            });

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1060, 690);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.grpFiltros, this.grpSubastas, this.grpOfertas
            });
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmBitacoraSubastas";
            this.Text  = "Bitácora de Subastas";

            this.grpFiltros.ResumeLayout(false);
            this.grpSubastas.ResumeLayout(false);
            this.grpOfertas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.nudIdProducto).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudIdOfertante).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMontoMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMontoMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvSubastas).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvPujas).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox         grpFiltros;
        private System.Windows.Forms.Label            lblDesde;
        private System.Windows.Forms.DateTimePicker   dtpDesde;
        private System.Windows.Forms.Label            lblHasta;
        private System.Windows.Forms.DateTimePicker   dtpHasta;
        private System.Windows.Forms.Label            lblEstado;
        private System.Windows.Forms.ComboBox         cboEstado;
        private System.Windows.Forms.Label            lblTipo;
        private System.Windows.Forms.ComboBox         cboTipo;
        private System.Windows.Forms.Button           btnBuscar;
        private System.Windows.Forms.Button           btnLimpiar;
        private System.Windows.Forms.Button           btnDescargarPDF;
        private System.Windows.Forms.Label            lblProducto;
        private System.Windows.Forms.TextBox          txtProducto;
        private System.Windows.Forms.Label            lblIdProducto;
        private System.Windows.Forms.NumericUpDown    nudIdProducto;
        private System.Windows.Forms.Label            lblOfertante;
        private System.Windows.Forms.TextBox          txtOfertante;
        private System.Windows.Forms.Label            lblIdOfertante;
        private System.Windows.Forms.NumericUpDown    nudIdOfertante;
        private System.Windows.Forms.Label            lblMontoMin;
        private System.Windows.Forms.NumericUpDown    nudMontoMin;
        private System.Windows.Forms.Label            lblMontoMax;
        private System.Windows.Forms.NumericUpDown    nudMontoMax;
        private System.Windows.Forms.CheckBox         chkSoloGanadores;
        private System.Windows.Forms.Label            lblConteo;
        private System.Windows.Forms.GroupBox         grpSubastas;
        private System.Windows.Forms.DataGridView     dgvSubastas;
        private System.Windows.Forms.GroupBox         grpOfertas;
        private System.Windows.Forms.Label            lblSumario;
        private System.Windows.Forms.DataGridView     dgvPujas;
    }
}
