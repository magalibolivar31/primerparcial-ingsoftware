using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public class FrmDetalle : Form
    {
        private Point _drag;

        public FrmDetalle(BE.UnidadDeVenta arbol)
        {
            const int W = 460;
            const int H = 340;

            FormBorderStyle = FormBorderStyle.None;
            Size            = new Size(W, H);
            BackColor       = Color.White;
            StartPosition   = FormStartPosition.CenterParent;

            // ── Header ────────────────────────────────────────────────────────
            var header = new Panel
            {
                Location  = new Point(0, 0),
                Size      = new Size(W, 46),
                BackColor = Color.FromArgb(30, 30, 30)
            };

            var lblTitulo = new Label
            {
                Text      = arbol.Nombre,
                Location  = new Point(14, 0),
                Size      = new Size(W - 60, 46),
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 11f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var btnX = new Button
            {
                Text      = "✕",
                Location  = new Point(W - 44, 4),
                Size      = new Size(38, 38),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Cursor    = Cursors.Hand,
                TabStop   = false
            };
            btnX.FlatAppearance.BorderSize         = 0;
            btnX.FlatAppearance.MouseOverBackColor = Color.FromArgb(196, 43, 28);
            btnX.Click += (s, e) => Close();

            header.Controls.Add(lblTitulo);
            header.Controls.Add(btnX);
            btnX.BringToFront();

            header.MouseDown    += (s, e) => { if (e.Button == MouseButtons.Left) _drag = e.Location; };
            header.MouseMove    += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _drag.X, Location.Y + e.Y - _drag.Y); };
            lblTitulo.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _drag = e.Location; };
            lblTitulo.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _drag.X, Location.Y + e.Y - _drag.Y); };

            // ── Precio ────────────────────────────────────────────────────────
            // ObtenerPrecioBase() funciona correctamente porque el árbol
            // fue construido por CatalogoBLL.ObtenerArbol(), con todos los hijos cargados.
            var lblPrecio = new Label
            {
                Text      = $"$ {arbol.ObtenerPrecioBase():N2}",
                AutoSize  = true,
                Location  = new Point(18, 58),
                ForeColor = Color.FromArgb(39, 174, 96),
                Font      = new Font("Segoe UI", 18f, FontStyle.Bold)
            };

            // ── TreeView ──────────────────────────────────────────────────────
            var trv = new TreeView
            {
                Location     = new Point(18, 108),
                Size         = new Size(W - 36, H - 126),
                BorderStyle  = BorderStyle.None,
                Font         = new Font("Segoe UI", 9.5f),
                BackColor    = Color.White,
                ForeColor    = Color.FromArgb(50, 50, 50),
                ShowLines    = true,
                ShowPlusMinus = true,
                Anchor       = AnchorStyles.Left | AnchorStyles.Right |
                               AnchorStyles.Top  | AnchorStyles.Bottom
            };

            trv.Nodes.Add(CrearNodo(arbol));
            trv.ExpandAll();

            Controls.Add(header);
            Controls.Add(lblPrecio);
            Controls.Add(trv);
        }

        // ── Construcción recursiva del TreeView ───────────────────────────────

        private static TreeNode CrearNodo(BE.UnidadDeVenta unidad)
        {
            string etiqueta = unidad is BE.Lote
                ? $"[Lote] {unidad.Nombre}  —  $ {unidad.ObtenerPrecioBase():N2}"
                : $"[Artículo] {unidad.Nombre}  —  $ {((BE.ArticuloIndividual)unidad).ValorDeclarado:N2}";

            var nodo = new TreeNode(etiqueta);

            var hijos = unidad.ObtenerHijos();
            if (hijos != null)
                foreach (var hijo in hijos)
                    nodo.Nodes.Add(CrearNodo(hijo));

            return nodo;
        }

        // ── Infraestructura ───────────────────────────────────────────────────

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(200, 200, 200)), 0, 0, Width - 1, Height - 1);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) { Close(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static void Mostrar(Form padre, BE.UnidadDeVenta arbol)
        {
            using (var frm = new FrmDetalle(arbol))
            {
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location      = new Point(
                    padre.Location.X + (padre.Width  - frm.Width)  / 2,
                    padre.Location.Y + (padre.Height - frm.Height) / 2);
                frm.ShowDialog(padre);
            }
        }
    }
}
