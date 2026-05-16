namespace GUI
{
    partial class FrmSubastas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl       = new System.Windows.Forms.TabControl();
            this.tabAbrir         = new System.Windows.Forms.TabPage();
            this.lblUnidad        = new System.Windows.Forms.Label();
            this.cboUnidad        = new System.Windows.Forms.ComboBox();
            this.lblPrecioBase    = new System.Windows.Forms.Label();
            this.btnAbrir         = new System.Windows.Forms.Button();
            this.tabCerrar        = new System.Windows.Forms.TabPage();
            this.dgvCerrar        = new System.Windows.Forms.DataGridView();
            this.lblObsLbl        = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.btnCerrar        = new System.Windows.Forms.Button();
            this.tabVer           = new System.Windows.Forms.TabPage();
            this.dgvVer           = new System.Windows.Forms.DataGridView();
            this.btnRefrescar     = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabAbrir.SuspendLayout();
            this.tabCerrar.SuspendLayout();
            this.tabVer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvCerrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvVer).BeginInit();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.TabPages.Add(this.tabAbrir);
            this.tabControl.TabPages.Add(this.tabCerrar);
            this.tabControl.TabPages.Add(this.tabVer);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9.5F);

            // ── Tab Abrir ──────────────────────────────────────────────────────
            this.tabAbrir.Text    = "Abrir Subasta";
            this.tabAbrir.Padding = new System.Windows.Forms.Padding(10);

            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont = new System.Drawing.Font("Segoe UI", 9.5F);

            this.lblUnidad.Text     = "Unidad de venta:";
            this.lblUnidad.Font     = boldFont;
            this.lblUnidad.Location = new System.Drawing.Point(20, 30);
            this.lblUnidad.Size     = new System.Drawing.Size(140, 20);

            this.cboUnidad.Font          = normFont;
            this.cboUnidad.Location      = new System.Drawing.Point(170, 28);
            this.cboUnidad.Size          = new System.Drawing.Size(400, 26);
            this.cboUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnidad.SelectedIndexChanged += new System.EventHandler(this.cboUnidad_SelectedIndexChanged);

            this.lblPrecioBase.Font     = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblPrecioBase.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPrecioBase.Location = new System.Drawing.Point(170, 62);
            this.lblPrecioBase.Size     = new System.Drawing.Size(350, 20);

            this.btnAbrir.Text      = "Abrir Subasta";
            this.btnAbrir.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAbrir.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnAbrir.ForeColor = System.Drawing.Color.White;
            this.btnAbrir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrir.FlatAppearance.BorderSize = 0;
            this.btnAbrir.Location  = new System.Drawing.Point(170, 96);
            this.btnAbrir.Size      = new System.Drawing.Size(160, 34);
            this.btnAbrir.Click    += new System.EventHandler(this.btnAbrir_Click);

            this.tabAbrir.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblUnidad, this.cboUnidad, this.lblPrecioBase, this.btnAbrir
            });

            // ── Tab Cerrar ─────────────────────────────────────────────────────
            this.tabCerrar.Text = "Cerrar Subasta";

            this.dgvCerrar.Location  = new System.Drawing.Point(10, 10);
            this.dgvCerrar.Size      = new System.Drawing.Size(850, 330);
            this.dgvCerrar.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvCerrar.ReadOnly  = true;
            this.dgvCerrar.RowHeadersVisible = false;

            this.lblObsLbl.Text     = "Observaciones:";
            this.lblObsLbl.Font     = boldFont;
            this.lblObsLbl.Location = new System.Drawing.Point(10, 352);
            this.lblObsLbl.Size     = new System.Drawing.Size(120, 20);

            this.txtObservaciones.Location   = new System.Drawing.Point(135, 348);
            this.txtObservaciones.Size       = new System.Drawing.Size(400, 26);
            this.txtObservaciones.Font       = normFont;

            this.btnCerrar.Text      = "Cerrar Subasta Seleccionada";
            this.btnCerrar.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(140, 30, 30);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.Location  = new System.Drawing.Point(135, 385);
            this.btnCerrar.Size      = new System.Drawing.Size(240, 34);
            this.btnCerrar.Click    += new System.EventHandler(this.btnCerrar_Click);

            this.tabCerrar.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvCerrar, this.lblObsLbl, this.txtObservaciones, this.btnCerrar
            });

            // ── Tab Ver ────────────────────────────────────────────────────────
            this.tabVer.Text = "Ver Activas";

            this.dgvVer.Location         = new System.Drawing.Point(10, 10);
            this.dgvVer.Size             = new System.Drawing.Size(850, 390);
            this.dgvVer.Anchor           = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.dgvVer.ReadOnly         = true;
            this.dgvVer.RowHeadersVisible = false;

            this.btnRefrescar.Text      = "Actualizar";
            this.btnRefrescar.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Location  = new System.Drawing.Point(10, 410);
            this.btnRefrescar.Size      = new System.Drawing.Size(100, 28);
            this.btnRefrescar.Click    += new System.EventHandler(this.btnRefrescar_Click);

            this.tabVer.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvVer, this.btnRefrescar
            });

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(900, 520);
            this.Controls.Add(this.tabControl);
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmSubastas";
            this.Text  = "Gestión de Subastas";

            this.tabControl.ResumeLayout(false);
            this.tabAbrir.ResumeLayout(false);
            this.tabCerrar.ResumeLayout(false);
            this.tabVer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvCerrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvVer).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl     tabControl;
        private System.Windows.Forms.TabPage        tabAbrir;
        private System.Windows.Forms.Label          lblUnidad;
        private System.Windows.Forms.ComboBox       cboUnidad;
        private System.Windows.Forms.Label          lblPrecioBase;
        private System.Windows.Forms.Button         btnAbrir;
        private System.Windows.Forms.TabPage        tabCerrar;
        private System.Windows.Forms.DataGridView   dgvCerrar;
        private System.Windows.Forms.Label          lblObsLbl;
        private System.Windows.Forms.TextBox        txtObservaciones;
        private System.Windows.Forms.Button         btnCerrar;
        private System.Windows.Forms.TabPage        tabVer;
        private System.Windows.Forms.DataGridView   dgvVer;
        private System.Windows.Forms.Button         btnRefrescar;
    }
}
