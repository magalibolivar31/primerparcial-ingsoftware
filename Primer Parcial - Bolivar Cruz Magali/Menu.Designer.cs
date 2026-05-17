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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuCatalogo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNuevoArticulo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNuevoLote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVerCatalogo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSubastas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGestionSubastas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPostores = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGestionPostores = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteJornada = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHistorialSubastas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblUsuarioStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(80)))));
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.menuStrip.ForeColor = System.Drawing.Color.White;
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSesion,
            this.menuCatalogo,
            this.menuSubastas,
            this.menuPostores,
            this.menuReportes});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1650, 35);
            this.menuStrip.TabIndex = 1;
            // 
            // menuCatalogo
            // 
            this.menuCatalogo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNuevoArticulo,
            this.menuNuevoLote,
            this.menuVerCatalogo});
            this.menuCatalogo.ForeColor = System.Drawing.Color.White;
            this.menuCatalogo.Name = "menuCatalogo";
            this.menuCatalogo.Size = new System.Drawing.Size(104, 29);
            this.menuCatalogo.Text = "Catálogo";
            // 
            // menuNuevoArticulo
            // 
            this.menuNuevoArticulo.Name = "menuNuevoArticulo";
            this.menuNuevoArticulo.Size = new System.Drawing.Size(252, 34);
            this.menuNuevoArticulo.Text = "Nuevo Artículo...";
            this.menuNuevoArticulo.Click += new System.EventHandler(this.menuNuevoArticulo_Click);
            // 
            // menuNuevoLote
            // 
            this.menuNuevoLote.Name = "menuNuevoLote";
            this.menuNuevoLote.Size = new System.Drawing.Size(252, 34);
            this.menuNuevoLote.Text = "Nuevo Lote...";
            this.menuNuevoLote.Click += new System.EventHandler(this.menuNuevoLote_Click);
            // 
            // menuVerCatalogo
            // 
            this.menuVerCatalogo.Name = "menuVerCatalogo";
            this.menuVerCatalogo.Size = new System.Drawing.Size(252, 34);
            this.menuVerCatalogo.Text = "Ver Catálogo";
            this.menuVerCatalogo.Click += new System.EventHandler(this.menuVerCatalogo_Click);
            // 
            // menuSubastas
            // 
            this.menuSubastas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGestionSubastas});
            this.menuSubastas.ForeColor = System.Drawing.Color.White;
            this.menuSubastas.Name = "menuSubastas";
            this.menuSubastas.Size = new System.Drawing.Size(102, 29);
            this.menuSubastas.Text = "Subastas";
            //
            // menuGestionSubastas
            //
            this.menuGestionSubastas.Name = "menuGestionSubastas";
            this.menuGestionSubastas.Size = new System.Drawing.Size(288, 34);
            this.menuGestionSubastas.Text = "Gestión de Subastas...";
            this.menuGestionSubastas.Click += new System.EventHandler(this.menuGestionSubastas_Click);
            //
            // menuPostores
            // 
            this.menuPostores.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGestionPostores});
            this.menuPostores.ForeColor = System.Drawing.Color.White;
            this.menuPostores.Name = "menuPostores";
            this.menuPostores.Size = new System.Drawing.Size(99, 29);
            this.menuPostores.Text = "Postores";
            // 
            // menuGestionPostores
            // 
            this.menuGestionPostores.Name = "menuGestionPostores";
            this.menuGestionPostores.Size = new System.Drawing.Size(292, 34);
            this.menuGestionPostores.Text = "Gestión de Postores...";
            this.menuGestionPostores.Click += new System.EventHandler(this.menuGestionPostores_Click);
            //
            // menuReportes
            //
            this.menuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReporteJornada,
            this.menuHistorialSubastas});
            this.menuReportes.ForeColor = System.Drawing.Color.White;
            this.menuReportes.Name = "menuReportes";
            this.menuReportes.Size = new System.Drawing.Size(98, 29);
            this.menuReportes.Text = "Reportes";
            //
            // menuReporteJornada
            //
            this.menuReporteJornada.Name = "menuReporteJornada";
            this.menuReporteJornada.Size = new System.Drawing.Size(260, 34);
            this.menuReporteJornada.Text = "Reporte de Jornada (RF-13)";
            this.menuReporteJornada.Click += new System.EventHandler(this.menuReporteJornada_Click);
            //
            // menuHistorialSubastas
            //
            this.menuHistorialSubastas.Name = "menuHistorialSubastas";
            this.menuHistorialSubastas.Size = new System.Drawing.Size(260, 34);
            this.menuHistorialSubastas.Text = "Historial de Subastas";
            this.menuHistorialSubastas.Click += new System.EventHandler(this.menuHistorialSubastas_Click);
            //
            // menuSesion
            //
            this.menuSesion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCerrarSesion});
            this.menuSesion.ForeColor = System.Drawing.Color.White;
            this.menuSesion.Name = "menuSesion";
            this.menuSesion.Size = new System.Drawing.Size(83, 29);
            this.menuSesion.Text = "Sesión";
            // 
            // menuCerrarSesion
            // 
            this.menuCerrarSesion.Name = "menuCerrarSesion";
            this.menuCerrarSesion.Size = new System.Drawing.Size(270, 34);
            this.menuCerrarSesion.Text = "Cerrar Sesión";
            this.menuCerrarSesion.Click += new System.EventHandler(this.menuCerrarSesion_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(80)))));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUsuarioStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 978);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(1650, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // lblUsuarioStatus
            // 
            this.lblUsuarioStatus.ForeColor = System.Drawing.Color.White;
            this.lblUsuarioStatus.Name = "lblUsuarioStatus";
            this.lblUsuarioStatus.Size = new System.Drawing.Size(0, 15);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1650, 1000);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "La Almoneda Nacional";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private System.Windows.Forms.ToolStripMenuItem     menuGestionSubastas;
        private System.Windows.Forms.ToolStripMenuItem     menuPostores;
        private System.Windows.Forms.ToolStripMenuItem     menuGestionPostores;
        private System.Windows.Forms.ToolStripMenuItem     menuReportes;
        private System.Windows.Forms.ToolStripMenuItem     menuReporteJornada;
        private System.Windows.Forms.ToolStripMenuItem     menuHistorialSubastas;
        private System.Windows.Forms.ToolStripMenuItem     menuSesion;
        private System.Windows.Forms.ToolStripMenuItem     menuCerrarSesion;
        private System.Windows.Forms.StatusStrip           statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel  lblUsuarioStatus;
    }
}
