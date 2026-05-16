namespace GUI
{
    partial class FrmPostores
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl      = new System.Windows.Forms.TabControl();
            this.tabPostores     = new System.Windows.Forms.TabPage();
            this.splitPostores   = new System.Windows.Forms.SplitContainer();
            this.dgvPostores     = new System.Windows.Forms.DataGridView();
            this.pnlFormPostor   = new System.Windows.Forms.Panel();
            this.lblNombreP      = new System.Windows.Forms.Label();
            this.txtNombrePostor = new System.Windows.Forms.TextBox();
            this.lblDniCuit      = new System.Windows.Forms.Label();
            this.txtDniCuit      = new System.Windows.Forms.TextBox();
            this.lblEmailP       = new System.Windows.Forms.Label();
            this.txtEmailPostor  = new System.Windows.Forms.TextBox();
            this.lblTelefono     = new System.Windows.Forms.Label();
            this.txtTelefono     = new System.Windows.Forms.TextBox();
            this.lblCanal        = new System.Windows.Forms.Label();
            this.cboCanal        = new System.Windows.Forms.ComboBox();
            this.btnNuevoPostor  = new System.Windows.Forms.Button();
            this.btnGuardarPostor = new System.Windows.Forms.Button();
            this.btnBajaPostor   = new System.Windows.Forms.Button();
            this.tabSuscs        = new System.Windows.Forms.TabPage();
            this.lblSubastaS     = new System.Windows.Forms.Label();
            this.cboSubastaSusc  = new System.Windows.Forms.ComboBox();
            this.lblPostorS      = new System.Windows.Forms.Label();
            this.cboPostorSusc   = new System.Windows.Forms.ComboBox();
            this.btnSuscribir    = new System.Windows.Forms.Button();
            this.btnDesuscribir  = new System.Windows.Forms.Button();
            this.dgvSuscs        = new System.Windows.Forms.DataGridView();

            this.tabControl.SuspendLayout();
            this.tabPostores.SuspendLayout();
            this.tabSuscs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.splitPostores).BeginInit();
            this.splitPostores.Panel1.SuspendLayout();
            this.splitPostores.Panel2.SuspendLayout();
            this.splitPostores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvPostores).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvSuscs).BeginInit();
            this.SuspendLayout();

            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            var normFont = new System.Drawing.Font("Segoe UI", 9.5F);

            // tabControl
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabControl.TabPages.Add(this.tabPostores);
            this.tabControl.TabPages.Add(this.tabSuscs);

            // ── Tab Postores ───────────────────────────────────────────────────
            this.tabPostores.Text = "Postores";
            this.splitPostores.Dock            = System.Windows.Forms.DockStyle.Fill;
            this.splitPostores.SplitterDistance = 520;
            this.splitPostores.Panel1.Controls.Add(this.dgvPostores);
            this.splitPostores.Panel2.Controls.Add(this.pnlFormPostor);

            this.dgvPostores.Dock            = System.Windows.Forms.DockStyle.Fill;
            this.dgvPostores.ReadOnly         = true;
            this.dgvPostores.RowHeadersVisible = false;
            this.dgvPostores.SelectionMode    = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPostores.SelectionChanged += new System.EventHandler(this.dgvPostores_SelectionChanged);

            this.pnlFormPostor.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormPostor.BackColor = System.Drawing.Color.FromArgb(248, 248, 255);
            int lx = 10, vx = 110, lw = 95, vw = 170, y = 14, gap = 32;

            // Form fields inside pnlFormPostor
            this.lblNombreP.Text = "Nombre:";   this.lblNombreP.Font = boldFont; this.lblNombreP.Location = new System.Drawing.Point(lx, y + 3); this.lblNombreP.Size = new System.Drawing.Size(lw, 18);
            this.txtNombrePostor.Font = normFont; this.txtNombrePostor.Location = new System.Drawing.Point(vx, y); this.txtNombrePostor.Size = new System.Drawing.Size(vw, 24); this.txtNombrePostor.TabIndex = 0;
            y += gap;
            this.lblDniCuit.Text = "DNI/CUIT:"; this.lblDniCuit.Font = boldFont; this.lblDniCuit.Location = new System.Drawing.Point(lx, y + 3); this.lblDniCuit.Size = new System.Drawing.Size(lw, 18);
            this.txtDniCuit.Font = normFont;      this.txtDniCuit.Location = new System.Drawing.Point(vx, y); this.txtDniCuit.Size = new System.Drawing.Size(vw, 24); this.txtDniCuit.TabIndex = 1;
            y += gap;
            this.lblEmailP.Text = "Email:";     this.lblEmailP.Font = boldFont; this.lblEmailP.Location = new System.Drawing.Point(lx, y + 3); this.lblEmailP.Size = new System.Drawing.Size(lw, 18);
            this.txtEmailPostor.Font = normFont;  this.txtEmailPostor.Location = new System.Drawing.Point(vx, y); this.txtEmailPostor.Size = new System.Drawing.Size(vw, 24); this.txtEmailPostor.TabIndex = 2;
            y += gap;
            this.lblTelefono.Text = "Teléfono:"; this.lblTelefono.Font = boldFont; this.lblTelefono.Location = new System.Drawing.Point(lx, y + 3); this.lblTelefono.Size = new System.Drawing.Size(lw, 18);
            this.txtTelefono.Font = normFont;      this.txtTelefono.Location = new System.Drawing.Point(vx, y); this.txtTelefono.Size = new System.Drawing.Size(vw, 24); this.txtTelefono.TabIndex = 3;
            y += gap;
            this.lblCanal.Text = "Canal:";       this.lblCanal.Font = boldFont; this.lblCanal.Location = new System.Drawing.Point(lx, y + 3); this.lblCanal.Size = new System.Drawing.Size(lw, 18);
            this.cboCanal.Font = normFont;        this.cboCanal.Location = new System.Drawing.Point(vx, y); this.cboCanal.Size = new System.Drawing.Size(vw, 24);
            this.cboCanal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCanal.Items.AddRange(new object[] { "Email", "Sms", "Push" });
            this.cboCanal.SelectedIndex = 0; this.cboCanal.TabIndex = 4;
            y += gap + 8;

            this.btnNuevoPostor.Text = "Nuevo";
            this.btnNuevoPostor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNuevoPostor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoPostor.Location = new System.Drawing.Point(vx, y);
            this.btnNuevoPostor.Size     = new System.Drawing.Size(80, 28);
            this.btnNuevoPostor.TabIndex = 5;
            this.btnNuevoPostor.Click   += new System.EventHandler(this.btnNuevoPostor_Click);

            this.btnGuardarPostor.Text = "Guardar";
            this.btnGuardarPostor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardarPostor.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnGuardarPostor.ForeColor = System.Drawing.Color.White;
            this.btnGuardarPostor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarPostor.FlatAppearance.BorderSize = 0;
            this.btnGuardarPostor.Location = new System.Drawing.Point(vx, y + 34);
            this.btnGuardarPostor.Size     = new System.Drawing.Size(80, 28);
            this.btnGuardarPostor.TabIndex = 6;
            this.btnGuardarPostor.Click   += new System.EventHandler(this.btnGuardarPostor_Click);

            this.btnBajaPostor.Text = "Dar de Baja";
            this.btnBajaPostor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBajaPostor.ForeColor = System.Drawing.Color.DarkRed;
            this.btnBajaPostor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBajaPostor.Location  = new System.Drawing.Point(vx, y + 68);
            this.btnBajaPostor.Size      = new System.Drawing.Size(90, 28);
            this.btnBajaPostor.TabIndex  = 7;
            this.btnBajaPostor.Click    += new System.EventHandler(this.btnBajaPostor_Click);

            this.pnlFormPostor.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblNombreP, this.txtNombrePostor, this.lblDniCuit, this.txtDniCuit,
                this.lblEmailP, this.txtEmailPostor, this.lblTelefono, this.txtTelefono,
                this.lblCanal, this.cboCanal, this.btnNuevoPostor, this.btnGuardarPostor, this.btnBajaPostor
            });
            this.tabPostores.Controls.Add(this.splitPostores);

            // ── Tab Suscripciones ──────────────────────────────────────────────
            this.tabSuscs.Text    = "Suscripciones";
            this.tabSuscs.Padding = new System.Windows.Forms.Padding(8);

            this.lblSubastaS.Text = "Subasta:"; this.lblSubastaS.Font = boldFont; this.lblSubastaS.Location = new System.Drawing.Point(12, 18); this.lblSubastaS.Size = new System.Drawing.Size(80, 20);
            this.cboSubastaSusc.Font = normFont; this.cboSubastaSusc.Location = new System.Drawing.Point(100, 16); this.cboSubastaSusc.Size = new System.Drawing.Size(320, 26);
            this.cboSubastaSusc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubastaSusc.SelectedIndexChanged += new System.EventHandler(this.cboSubastaSusc_SelectedIndexChanged);

            this.lblPostorS.Text = "Postor:"; this.lblPostorS.Font = boldFont; this.lblPostorS.Location = new System.Drawing.Point(12, 54); this.lblPostorS.Size = new System.Drawing.Size(80, 20);
            this.cboPostorSusc.Font = normFont; this.cboPostorSusc.Location = new System.Drawing.Point(100, 52); this.cboPostorSusc.Size = new System.Drawing.Size(320, 26);
            this.cboPostorSusc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnSuscribir.Text = "Suscribir";
            this.btnSuscribir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSuscribir.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnSuscribir.ForeColor = System.Drawing.Color.White;
            this.btnSuscribir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuscribir.FlatAppearance.BorderSize = 0;
            this.btnSuscribir.Location = new System.Drawing.Point(100, 88);
            this.btnSuscribir.Size     = new System.Drawing.Size(100, 30);
            this.btnSuscribir.Click   += new System.EventHandler(this.btnSuscribir_Click);

            this.btnDesuscribir.Text = "Desuscribir";
            this.btnDesuscribir.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDesuscribir.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDesuscribir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesuscribir.Location  = new System.Drawing.Point(210, 88);
            this.btnDesuscribir.Size      = new System.Drawing.Size(110, 30);
            this.btnDesuscribir.Click    += new System.EventHandler(this.btnDesuscribir_Click);

            this.dgvSuscs.Location         = new System.Drawing.Point(12, 130);
            this.dgvSuscs.Size             = new System.Drawing.Size(840, 320);
            this.dgvSuscs.Anchor           = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.dgvSuscs.ReadOnly          = true;
            this.dgvSuscs.RowHeadersVisible = false;

            this.tabSuscs.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSubastaS, this.cboSubastaSusc, this.lblPostorS, this.cboPostorSusc,
                this.btnSuscribir, this.btnDesuscribir, this.dgvSuscs
            });

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(900, 540);
            this.Controls.Add(this.tabControl);
            this.Font  = new System.Drawing.Font("Segoe UI", 9F);
            this.Name  = "FrmPostores";
            this.Text  = "Gestión de Postores";

            this.tabControl.ResumeLayout(false);
            this.tabPostores.ResumeLayout(false);
            this.tabSuscs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.splitPostores).EndInit();
            this.splitPostores.Panel1.ResumeLayout(false);
            this.splitPostores.Panel2.ResumeLayout(false);
            this.splitPostores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvPostores).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvSuscs).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl      tabControl;
        private System.Windows.Forms.TabPage         tabPostores;
        private System.Windows.Forms.SplitContainer  splitPostores;
        private System.Windows.Forms.DataGridView    dgvPostores;
        private System.Windows.Forms.Panel           pnlFormPostor;
        private System.Windows.Forms.Label           lblNombreP;
        private System.Windows.Forms.TextBox         txtNombrePostor;
        private System.Windows.Forms.Label           lblDniCuit;
        private System.Windows.Forms.TextBox         txtDniCuit;
        private System.Windows.Forms.Label           lblEmailP;
        private System.Windows.Forms.TextBox         txtEmailPostor;
        private System.Windows.Forms.Label           lblTelefono;
        private System.Windows.Forms.TextBox         txtTelefono;
        private System.Windows.Forms.Label           lblCanal;
        private System.Windows.Forms.ComboBox        cboCanal;
        private System.Windows.Forms.Button          btnNuevoPostor;
        private System.Windows.Forms.Button          btnGuardarPostor;
        private System.Windows.Forms.Button          btnBajaPostor;
        private System.Windows.Forms.TabPage         tabSuscs;
        private System.Windows.Forms.Label           lblSubastaS;
        private System.Windows.Forms.ComboBox        cboSubastaSusc;
        private System.Windows.Forms.Label           lblPostorS;
        private System.Windows.Forms.ComboBox        cboPostorSusc;
        private System.Windows.Forms.Button          btnSuscribir;
        private System.Windows.Forms.Button          btnDesuscribir;
        private System.Windows.Forms.DataGridView    dgvSuscs;
    }
}
