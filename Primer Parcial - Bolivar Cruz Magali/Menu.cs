using System;
using System.Windows.Forms;

namespace GUI
{
    // MDI principal. Cada formulario hijo se abre dentro de esta ventana.
    public partial class Menu : Form
    {
        private readonly BLL.UsuarioBLL _usuarioBLL = new BLL.UsuarioBLL();

        public Menu()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var usuario = Seguridad.SessionManager.GetInstance.Usuario;
            this.Text = $"La Almoneda Nacional  —  {usuario.NombreCompleto}  [{usuario.Rol}]";
            lblUsuarioStatus.Text = $"Usuario: {usuario.NombreCompleto}  |  Rol: {usuario.Rol}";

            ConfigurarMenuPorRol(usuario);
        }

        // Muestra u oculta opciones del menu segun el rol del usuario autenticado.
        private void ConfigurarMenuPorRol(BE.Usuario usuario)
        {
            // Catalogo: solo Martillero y Administrador
            menuCatalogo.Visible = usuario.EsMartillero || usuario.EsAdministrador;

            // Subastas: Martillero y Operador
            menuSubastas.Visible = usuario.EsMartillero || usuario.EsOperador || usuario.EsAdministrador;

            // Postores: Martillero y Operador
            menuPostores.Visible = usuario.EsMartillero || usuario.EsOperador || usuario.EsAdministrador;

            // Reportes: todos los roles
            menuReportes.Visible = true;

            // Administracion: solo Administrador
            menuAdmin.Visible = usuario.EsAdministrador;

            // Subopciones de Subastas segun rol
            menuAbrirSubasta.Visible  = usuario.EsMartillero || usuario.EsAdministrador;
            menuCerrarSubasta.Visible = usuario.EsMartillero || usuario.EsAdministrador;
            menuRegistrarPuja.Visible = usuario.EsOperador   || usuario.EsAdministrador;
        }

        // Abre una instancia única del formulario hijo MDI del tipo T.
        // Si ya está abierto, lo activa en lugar de crear uno nuevo.
        private void AbrirHijo<T>() where T : Form, new()
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is T) { child.Activate(); return; }
            }
            var frm = new T { MdiParent = this };
            frm.Show();
        }

        // ── Catalogo ─────────────────────────────────────────────────────────

        private void menuNuevoArticulo_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmNuevoArticulo())
                frm.ShowDialog(this);
        }

        private void menuNuevoLote_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmNuevoLote())
                frm.ShowDialog(this);
        }

        private void menuVerCatalogo_Click(object sender, EventArgs e)
            => AbrirHijo<FrmCatalogo>();

        // ── Subastas ─────────────────────────────────────────────────────────

        private void menuAbrirSubasta_Click(object sender, EventArgs e)
            => AbrirHijo<FrmSubastas>();

        private void menuCerrarSubasta_Click(object sender, EventArgs e)
            => AbrirHijo<FrmSubastas>();

        private void menuVerSubastas_Click(object sender, EventArgs e)
            => AbrirHijo<FrmSubastas>();

        private void menuRegistrarPuja_Click(object sender, EventArgs e)
            => AbrirHijo<FrmRegistrarPuja>();

        // ── Postores ─────────────────────────────────────────────────────────

        private void menuGestionPostores_Click(object sender, EventArgs e)
            => AbrirHijo<FrmPostores>();

        // ── Reportes ─────────────────────────────────────────────────────────

        private void menuReporteJornada_Click(object sender, EventArgs e)
            => AbrirHijo<FrmReporteJornada>();

        // ── Administracion ───────────────────────────────────────────────────

        private void menuGestionUsuarios_Click(object sender, EventArgs e)
            => AbrirHijo<FrmGestionUsuarios>();

        // ── Sesion ───────────────────────────────────────────────────────────

        private void menuCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar la sesión?", "Cerrar Sesión",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _usuarioBLL.Logout();
                this.Close();
            }
        }
    }
}
