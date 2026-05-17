using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public class FrmDetalle : Form
    {
        private Point _drag;

        public FrmDetalle(string nombre, decimal precio, string descripcion)
        {
            const int W = 420;
            const int H = 260;

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
                Text      = nombre,
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

            // Arrastre desde el header y el título
            header.MouseDown   += (s, e) => { if (e.Button == MouseButtons.Left) _drag = e.Location; };
            header.MouseMove   += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _drag.X, Location.Y + e.Y - _drag.Y); };
            lblTitulo.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _drag = e.Location; };
            lblTitulo.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _drag.X, Location.Y + e.Y - _drag.Y); };

            // ── Precio ────────────────────────────────────────────────────────
            var lblPrecio = new Label
            {
                Text      = $"$ {precio:N2}",
                AutoSize  = true,
                Location  = new Point(18, 58),
                ForeColor = Color.FromArgb(39, 174, 96),
                Font      = new Font("Segoe UI", 18f, FontStyle.Bold)
            };

            // ── Descripción ───────────────────────────────────────────────────
            var txtDesc = new TextBox
            {
                Text        = descripcion,
                Multiline   = true,
                ReadOnly    = true,
                BorderStyle = BorderStyle.None,
                ScrollBars  = ScrollBars.Vertical,
                BackColor   = Color.White,
                ForeColor   = Color.FromArgb(60, 60, 60),
                Font        = new Font("Segoe UI", 9.5f),
                Location    = new Point(20, 112),
                Size        = new Size(W - 40, H - 130),
                TabStop     = false
            };

            Controls.Add(header);
            Controls.Add(lblPrecio);
            Controls.Add(txtDesc);
        }

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

        public static void Mostrar(Form padre, string nombre, decimal precio, string descripcion)
        {
            using (var frm = new FrmDetalle(nombre, precio, descripcion))
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
