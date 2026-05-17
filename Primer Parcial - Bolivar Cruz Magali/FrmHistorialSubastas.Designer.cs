namespace GUI
{
    partial class FrmHistorialSubastas
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
            this.lblResultado      = new System.Windows.Forms.Label();
            this.cboResultado      = new System.Windows.Forms.ComboBox();
            this.lblFiltroUnidad   = new System.Windows.Forms.Label();
            this.txtFiltroUnidad   = new System.Windows.Forms.TextBox();
            this.lblFiltroGanador  = new System.Windows.Forms.Label();
            this.txtFiltroGanador  = new System.Windows.Forms.TextBox();
            this.btnBuscar         = new System.Windows.Forms.Button();
            this.btnLimpiar        = new System.Windows.Forms.Button();
            this.lblConteoSubastas = new System.Windows.Forms.Label();
            this.grpSubastas       = new System.Windows.Forms.GroupBox();
            this.dgvSubastas       = new System.Windows.Forms.DataGridView();
            this.grpPujas          = new System.Windows.Forms.GroupBox();
            this.lblSumarioPujas   = new System.Windows.Forms.Label();
            this.dgvPujas          = new System.Windows.Forms.DataGridView();

            this.grpFiltros.SuspendLayout();
            this.grpSubastas.SuspendLayout();
            this.grpPujas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvSubastas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvPujas).BeginInit();
            this.SuspendLayout();

            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont = new System.Drawing.Font("Segoe UI", 9.5F);

            // ── grpFiltros ────────────────────────────────────────────────────
            this.grpFiltros.Text     = "Filtros de búsqueda";
            this.grpFiltros.Font     = boldFont;
            this.grpFiltros.Location = new System.Drawing.Point(5, 5);
            this.grpFiltros.Size     = new System.Drawing.Size(990, 100);
            this.grpFiltros.Anchor   = System.Windows.Forms.AnchorStyles.Top
                                     | System.Windows.Forms.AnchorStyles.Left
                                     | System.Windows.Forms.AnchorStyles.Right;

            // Row 1 — Desde / Hasta / Resultado / Botones
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

            this.lblResultado.Text      = "Resultado:";
            this.lblResultado.Font      = boldFont;
            this.lblResultado.Location  = new System.Drawing.Point(344, 26);
            this.lblResultado.Size      = new System.Drawing.Size(72, 20);
            this.lblResultado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cboResultado.Font          = normFont;
            this.cboResultado.Location      = new System.Drawing.Point(419, 24);
            this.cboResultado.Size          = new System.Drawing.Size(130, 26);
            this.cboResultado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResultado.Items.AddRange(new object[] { "Todas", "Adjudicadas", "Desiertas" });
            this.cboResultado.SelectedIndex = 0;

            this.btnBuscar.Text      = "Buscar";
            this.btnBuscar.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Location  = new System.Drawing.Point(562, 22);
            this.btnBuscar.Size      = new System.Drawing.Size(90, 28);
            this.btnBuscar.Click    += new System.EventHandler(this.btnBuscar_Click);

            this.btnLimpiar.Text      = "Limpiar";
            this.btnLimpiar.Font      = normFont;
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.Location  = new System.Drawing.Point(660, 22);
            this.btnLimpiar.Size      = new System.Drawing.Size(90, 28);
            this.btnLimpiar.Click    += new System.EventHandler(this.btnLimpiar_Click);

            // Row 2 — Unidad / Ganador
            this.lblFiltroUnidad.Text      = "Unidad:";
            this.lblFiltroUnidad.Font      = boldFont;
            this.lblFiltroUnidad.Location  = new System.Drawing.Point(10, 62);
            this.lblFiltroUnidad.Size      = new System.Drawing.Size(50, 20);
            this.lblFiltroUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtFiltroUnidad.Font     = normFont;
            this.txtFiltroUnidad.Location = new System.Drawing.Point(63, 60);
            this.txtFiltroUnidad.Size     = new System.Drawing.Size(280, 22);

            this.lblFiltroGanador.Text      = "Ganador:";
            this.lblFiltroGanador.Font      = boldFont;
            this.lblFiltroGanador.Location  = new System.Drawing.Point(358, 62);
            this.lblFiltroGanador.Size      = new System.Drawing.Size(58, 20);
            this.lblFiltroGanador.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtFiltroGanador.Font     = normFont;
            this.txtFiltroGanador.Location = new System.Drawing.Point(419, 60);
            this.txtFiltroGanador.Size     = new System.Drawing.Size(280, 22);

            this.grpFiltros.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblDesde, this.dtpDesde, this.lblHasta, this.dtpHasta,
                this.lblResultado, this.cboResultado, this.btnBuscar, this.btnLimpiar,
                this.lblFiltroUnidad, this.txtFiltroUnidad,
                this.lblFiltroGanador, this.txtFiltroGanador
            });

            // ── lblConteoSubastas ─────────────────────────────────────────────
            this.lblConteoSubastas.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblConteoSubastas.ForeColor = System.Drawing.Color.DimGray;
            this.lblConteoSubastas.Location  = new System.Drawing.Point(10, 112);
            this.lblConteoSubastas.Size      = new System.Drawing.Size(400, 18);
            this.lblConteoSubastas.Text      = "";

            // ── grpSubastas ───────────────────────────────────────────────────
            this.grpSubastas.Text     = "Subastas Cerradas";
            this.grpSubastas.Font     = boldFont;
            this.grpSubastas.Location = new System.Drawing.Point(5, 133);
            this.grpSubastas.Size     = new System.Drawing.Size(990, 230);
            this.grpSubastas.Anchor   = System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right;

            this.dgvSubastas.Location          = new System.Drawing.Point(8, 20);
            this.dgvSubastas.Size              = new System.Drawing.Size(972, 200);
            this.dgvSubastas.Anchor            = System.Windows.Forms.AnchorStyles.Top
                                               | System.Windows.Forms.AnchorStyles.Left
                                               | System.Windows.Forms.AnchorStyles.Right;
            this.dgvSubastas.ReadOnly          = true;
            this.dgvSubastas.RowHeadersVisible = false;
            this.dgvSubastas.Font              = normFont;
            this.dgvSubastas.SelectionMode     = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubastas.SelectionChanged += new System.EventHandler(this.dgvSubastas_SelectionChanged);

            this.grpSubastas.Controls.Add(this.dgvSubastas);

            // ── grpPujas ──────────────────────────────────────────────────────
            this.grpPujas.Text     = "Detalle de Pujas";
            this.grpPujas.Font     = boldFont;
            this.grpPujas.Location = new System.Drawing.Point(5, 368);
            this.grpPujas.Size     = new System.Drawing.Size(990, 265);
            this.grpPujas.Anchor   = System.Windows.Forms.AnchorStyles.Top
                                   | System.Windows.Forms.AnchorStyles.Left
                                   | System.Windows.Forms.AnchorStyles.Right
                                   | System.Windows.Forms.AnchorStyles.Bottom;

            this.lblSumarioPujas.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblSumarioPujas.ForeColor = System.Drawing.Color.DimGray;
            this.lblSumarioPujas.Location  = new System.Drawing.Point(8, 20);
            this.lblSumarioPujas.Size      = new System.Drawing.Size(968, 18);
            this.lblSumarioPujas.Text      = "Seleccione una subasta para ver el detalle de pujas.";
            this.lblSumarioPujas.Anchor    = System.Windows.Forms.AnchorStyles.Top
                                           | System.Windows.Forms.AnchorStyles.Left
                                           | System.Windows.Forms.AnchorStyles.Right;

            this.dgvPujas.Location          = new System.Drawing.Point(8, 42);
            this.dgvPujas.Size              = new System.Drawing.Size(972, 212);
            this.dgvPujas.Anchor            = System.Windows.Forms.AnchorStyles.Top
                                            | System.Windows.Forms.AnchorStyles.Left
                                            | System.Windows.Forms.AnchorStyles.Right
                                            | System.Windows.Forms.AnchorStyles.Bottom;
            this.dgvPujas.ReadOnly          = true;
            this.dgvPujas.RowHeadersVisible = false;
            this.dgvPujas.Font              = normFont;

            this.grpPujas.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSumarioPujas, this.dgvPujas
            });

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1000, 645);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.grpFiltros, this.lblConteoSubastas, this.grpSubastas, this.grpPujas
            });
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmHistorialSubastas";
            this.Text  = "Historial de Subastas";

            this.grpFiltros.ResumeLayout(false);
            this.grpSubastas.ResumeLayout(false);
            this.grpPujas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvSubastas).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvPujas).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox        grpFiltros;
        private System.Windows.Forms.Label           lblDesde;
        private System.Windows.Forms.DateTimePicker  dtpDesde;
        private System.Windows.Forms.Label           lblHasta;
        private System.Windows.Forms.DateTimePicker  dtpHasta;
        private System.Windows.Forms.Label           lblResultado;
        private System.Windows.Forms.ComboBox        cboResultado;
        private System.Windows.Forms.Label           lblFiltroUnidad;
        private System.Windows.Forms.TextBox         txtFiltroUnidad;
        private System.Windows.Forms.Label           lblFiltroGanador;
        private System.Windows.Forms.TextBox         txtFiltroGanador;
        private System.Windows.Forms.Button          btnBuscar;
        private System.Windows.Forms.Button          btnLimpiar;
        private System.Windows.Forms.Label           lblConteoSubastas;
        private System.Windows.Forms.GroupBox        grpSubastas;
        private System.Windows.Forms.DataGridView    dgvSubastas;
        private System.Windows.Forms.GroupBox        grpPujas;
        private System.Windows.Forms.Label           lblSumarioPujas;
        private System.Windows.Forms.DataGridView    dgvPujas;
    }
}
