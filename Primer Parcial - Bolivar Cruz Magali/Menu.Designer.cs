namespace GUI
{
    partial class Menu
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
            this.menuStrip         = new System.Windows.Forms.MenuStrip();
            this.menuCatalogo      = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNuevoArticulo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNuevoLote     = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVerCatalogo   = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSubastas      = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbrirSubasta  = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCerrarSubasta = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVerSubastas   = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRegistrarPuja    = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBitacoraSubastas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPostores      = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGestionPostores = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportes           = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteJornada     = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHistorialSubastas  = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdmin         = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGestionUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSesion        = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCerrarSesion  = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip       = new System.Windows.Forms.StatusStrip();
            this.lblUsuarioStatus  = new System.Windows.Forms.ToolStripStatusLabel();

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // ── menuStrip ────────────────────────────────────────────────────
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuCatalogo,
                this.menuSubastas,
                this.menuPostores,
                this.menuReportes,
                this.menuAdmin,
                this.menuSesion
            });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name     = "menuStrip";
            this.menuStrip.Size     = new System.Drawing.Size(1100, 24);
            this.menuStrip.Font     = new System.Drawing.Font("Segoe UI", 9.5F);
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(30, 30, 80);
            this.menuStrip.ForeColor = System.Drawing.Color.White;

            // ── Catalogo ─────────────────────────────────────────────────────
            this.menuCatalogo.Text = "Catálogo";
            this.menuCatalogo.ForeColor = System.Drawing.Color.White;
            this.menuCatalogo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuNuevoArticulo,
                this.menuNuevoLote,
                new System.Windows.Forms.ToolStripSeparator(),
                this.menuVerCatalogo
            });

            this.menuNuevoArticulo.Text   = "Nuevo Artículo...";
            this.menuNuevoArticulo.Click += new System.EventHandler(this.menuNuevoArticulo_Click);

            this.menuNuevoLote.Text   = "Nuevo Lote...";
            this.menuNuevoLote.Click += new System.EventHandler(this.menuNuevoLote_Click);

            this.menuVerCatalogo.Text   = "Ver Catálogo";
            this.menuVerCatalogo.Click += new System.EventHandler(this.menuVerCatalogo_Click);

            // ── Subastas ─────────────────────────────────────────────────────
            this.menuSubastas.Text      = "Subastas";
            this.menuSubastas.ForeColor = System.Drawing.Color.White;
            this.menuSubastas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuAbrirSubasta,
                this.menuCerrarSubasta,
                new System.Windows.Forms.ToolStripSeparator(),
                this.menuVerSubastas,
                this.menuRegistrarPuja,
                new System.Windows.Forms.ToolStripSeparator(),
                this.menuBitacoraSubastas
            });

            this.menuAbrirSubasta.Text   = "Abrir Subasta...";
            this.menuAbrirSubasta.Click += new System.EventHandler(this.menuAbrirSubasta_Click);

            this.menuCerrarSubasta.Text   = "Cerrar Subasta...";
            this.menuCerrarSubasta.Click += new System.EventHandler(this.menuCerrarSubasta_Click);

            this.menuVerSubastas.Text   = "Ver Subastas Activas";
            this.menuVerSubastas.Click += new System.EventHandler(this.menuVerSubastas_Click);

            this.menuRegistrarPuja.Text   = "Registrar Puja...";
            this.menuRegistrarPuja.Click += new System.EventHandler(this.menuRegistrarPuja_Click);

            this.menuBitacoraSubastas.Text   = "Bitácora de Subastas";
            this.menuBitacoraSubastas.Click += new System.EventHandler(this.menuBitacoraSubastas_Click);

            // ── Postores ─────────────────────────────────────────────────────
            this.menuPostores.Text      = "Postores";
            this.menuPostores.ForeColor = System.Drawing.Color.White;
            this.menuPostores.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuGestionPostores
            });

            this.menuGestionPostores.Text   = "Gestión de Postores...";
            this.menuGestionPostores.Click += new System.EventHandler(this.menuGestionPostores_Click);

            // ── Reportes ─────────────────────────────────────────────────────
            this.menuReportes.Text      = "Reportes";
            this.menuReportes.ForeColor = System.Drawing.Color.White;
            this.menuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuReporteJornada,
                new System.Windows.Forms.ToolStripSeparator(),
                this.menuHistorialSubastas
            });

            this.menuReporteJornada.Text   = "Reporte de Jornada";
            this.menuReporteJornada.Click += new System.EventHandler(this.menuReporteJornada_Click);

            this.menuHistorialSubastas.Text   = "Historial de Subastas";
            this.menuHistorialSubastas.Click += new System.EventHandler(this.menuHistorialSubastas_Click);

            // ── Administracion ───────────────────────────────────────────────
            this.menuAdmin.Text      = "Administración";
            this.menuAdmin.ForeColor = System.Drawing.Color.White;
            this.menuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuGestionUsuarios
            });

            this.menuGestionUsuarios.Text   = "Gestión de Usuarios...";
            this.menuGestionUsuarios.Click += new System.EventHandler(this.menuGestionUsuarios_Click);

            // ── Sesion ───────────────────────────────────────────────────────
            this.menuSesion.Text      = "Sesión";
            this.menuSesion.ForeColor = System.Drawing.Color.White;
            this.menuSesion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuCerrarSesion
            });

            this.menuCerrarSesion.Text   = "Cerrar Sesión";
            this.menuCerrarSesion.Click += new System.EventHandler(this.menuCerrarSesion_Click);

            // ── statusStrip ──────────────────────────────────────────────────
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblUsuarioStatus
            });
            this.statusStrip.Location  = new System.Drawing.Point(0, 628);
            this.statusStrip.Name      = "statusStrip";
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(30, 30, 80);

            this.lblUsuarioStatus.ForeColor = System.Drawing.Color.White;
            this.lblUsuarioStatus.Name      = "lblUsuarioStatus";
            this.lblUsuarioStatus.Text      = "";

            // ── Form Menu (MDI) ───────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer      = true;
            this.MainMenuStrip       = this.menuStrip;
            this.Name                = "Menu";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "La Almoneda Nacional";
            this.WindowState         = System.Windows.Forms.FormWindowState.Maximized;

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ── Controles ────────────────────────────────────────────────────────
        private System.Windows.Forms.MenuStrip             menuStrip;
        private System.Windows.Forms.ToolStripMenuItem     menuCatalogo;
        private System.Windows.Forms.ToolStripMenuItem     menuNuevoArticulo;
        private System.Windows.Forms.ToolStripMenuItem     menuNuevoLote;
        private System.Windows.Forms.ToolStripMenuItem     menuVerCatalogo;
        private System.Windows.Forms.ToolStripMenuItem     menuSubastas;
        private System.Windows.Forms.ToolStripMenuItem     menuAbrirSubasta;
        private System.Windows.Forms.ToolStripMenuItem     menuCerrarSubasta;
        private System.Windows.Forms.ToolStripMenuItem     menuVerSubastas;
        private System.Windows.Forms.ToolStripMenuItem     menuRegistrarPuja;
        private System.Windows.Forms.ToolStripMenuItem     menuBitacoraSubastas;
        private System.Windows.Forms.ToolStripMenuItem     menuPostores;
        private System.Windows.Forms.ToolStripMenuItem     menuGestionPostores;
        private System.Windows.Forms.ToolStripMenuItem     menuReportes;
        private System.Windows.Forms.ToolStripMenuItem     menuReporteJornada;
        private System.Windows.Forms.ToolStripMenuItem     menuHistorialSubastas;
        private System.Windows.Forms.ToolStripMenuItem     menuAdmin;
        private System.Windows.Forms.ToolStripMenuItem     menuGestionUsuarios;
        private System.Windows.Forms.ToolStripMenuItem     menuSesion;
        private System.Windows.Forms.ToolStripMenuItem     menuCerrarSesion;
        private System.Windows.Forms.StatusStrip           statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel  lblUsuarioStatus;
    }
}
