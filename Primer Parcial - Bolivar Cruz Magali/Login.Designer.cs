namespace GUI
{
    partial class Login
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlFondo        = new System.Windows.Forms.Panel();
            this.pnlCard         = new System.Windows.Forms.Panel();
            this.lblTitulo       = new System.Windows.Forms.Label();
            this.lblSubtitulo    = new System.Windows.Forms.Label();
            this.lblEmail        = new System.Windows.Forms.Label();
            this.txtEmail        = new System.Windows.Forms.TextBox();
            this.lblPassword     = new System.Windows.Forms.Label();
            this.txtPassword     = new System.Windows.Forms.TextBox();
            this.lblError        = new System.Windows.Forms.Label();
            this.btnIngresar     = new System.Windows.Forms.Button();
            this.btnSalir        = new System.Windows.Forms.Button();

            this.pnlFondo.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();

            // ── pnlFondo (fondo oscuro de pantalla completa) ─────────────────
            this.pnlFondo.BackColor = System.Drawing.Color.FromArgb(20, 20, 35);
            this.pnlFondo.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.pnlFondo.Controls.Add(this.pnlCard);

            // ── pnlCard (tarjeta central blanca) ─────────────────────────────
            this.pnlCard.BackColor  = System.Drawing.Color.White;
            this.pnlCard.Size       = new System.Drawing.Size(360, 400);
            this.pnlCard.Location   = new System.Drawing.Point(50, 60);
            this.pnlCard.Padding    = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.pnlCard.Controls.Add(this.lblTitulo);
            this.pnlCard.Controls.Add(this.lblSubtitulo);
            this.pnlCard.Controls.Add(this.lblEmail);
            this.pnlCard.Controls.Add(this.txtEmail);
            this.pnlCard.Controls.Add(this.lblPassword);
            this.pnlCard.Controls.Add(this.txtPassword);
            this.pnlCard.Controls.Add(this.lblError);
            this.pnlCard.Controls.Add(this.btnIngresar);
            this.pnlCard.Controls.Add(this.btnSalir);

            // ── lblTitulo ────────────────────────────────────────────────────
            this.lblTitulo.Text      = "LA ALMONEDA NACIONAL";
            this.lblTitulo.Font      = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(30, 30, 60);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitulo.Size      = new System.Drawing.Size(300, 36);
            this.lblTitulo.Location  = new System.Drawing.Point(30, 24);

            // ── lblSubtitulo ─────────────────────────────────────────────────
            this.lblSubtitulo.Text      = "Sistema de Gestión de Subastas";
            this.lblSubtitulo.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSubtitulo.Size      = new System.Drawing.Size(300, 20);
            this.lblSubtitulo.Location  = new System.Drawing.Point(30, 64);

            // ── lblEmail ─────────────────────────────────────────────────────
            this.lblEmail.Text      = "Email:";
            this.lblEmail.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(50, 50, 80);
            this.lblEmail.Size      = new System.Drawing.Size(300, 18);
            this.lblEmail.Location  = new System.Drawing.Point(30, 106);

            // ── txtEmail ─────────────────────────────────────────────────────
            this.txtEmail.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Size      = new System.Drawing.Size(300, 26);
            this.txtEmail.Location  = new System.Drawing.Point(30, 126);
            this.txtEmail.TabIndex  = 0;

            // ── lblPassword ──────────────────────────────────────────────────
            this.lblPassword.Text      = "Contraseña:";
            this.lblPassword.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(50, 50, 80);
            this.lblPassword.Size      = new System.Drawing.Size(300, 18);
            this.lblPassword.Location  = new System.Drawing.Point(30, 168);

            // ── txtPassword ──────────────────────────────────────────────────
            this.txtPassword.Font         = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size         = new System.Drawing.Size(300, 26);
            this.txtPassword.Location     = new System.Drawing.Point(30, 188);
            this.txtPassword.TabIndex     = 1;

            // ── lblError ─────────────────────────────────────────────────────
            this.lblError.Text      = string.Empty;
            this.lblError.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(180, 30, 30);
            this.lblError.Size      = new System.Drawing.Size(300, 36);
            this.lblError.Location  = new System.Drawing.Point(30, 224);
            this.lblError.Visible   = false;

            // ── btnIngresar ──────────────────────────────────────────────────
            this.btnIngresar.Text      = "INGRESAR";
            this.btnIngresar.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(30, 30, 100);
            this.btnIngresar.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.Size      = new System.Drawing.Size(300, 36);
            this.btnIngresar.Location  = new System.Drawing.Point(30, 270);
            this.btnIngresar.TabIndex  = 2;
            this.btnIngresar.FlatAppearance.BorderSize = 0;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);

            // ── btnSalir ─────────────────────────────────────────────────────
            this.btnSalir.Text      = "Salir";
            this.btnSalir.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.Gray;
            this.btnSalir.BackColor = System.Drawing.Color.White;
            this.btnSalir.Size      = new System.Drawing.Size(300, 28);
            this.btnSalir.Location  = new System.Drawing.Point(30, 318);
            this.btnSalir.TabIndex  = 3;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);

            // ── Form Login ───────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(460, 520);
            this.Controls.Add(this.pnlFondo);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox         = false;
            this.MinimizeBox         = false;
            this.Name                = "Login";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "La Almoneda Nacional — Acceso";

            this.pnlCard.ResumeLayout(false);
            this.pnlFondo.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Controles ────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlFondo;
        private System.Windows.Forms.Panel    pnlCard;
        private System.Windows.Forms.Label    lblTitulo;
        private System.Windows.Forms.Label    lblSubtitulo;
        private System.Windows.Forms.Label    lblEmail;
        private System.Windows.Forms.TextBox  txtEmail;
        private System.Windows.Forms.Label    lblPassword;
        private System.Windows.Forms.TextBox  txtPassword;
        private System.Windows.Forms.Label    lblError;
        private System.Windows.Forms.Button   btnIngresar;
        private System.Windows.Forms.Button   btnSalir;
    }
}
