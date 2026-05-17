using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    // RF-13: Reporte consolidado de jornada — recorre el catálogo de manera transparente (Composite).
    public partial class FrmReporteJornada : Form
    {
        private readonly Servicios.ReporteJornada _servicio  = new Servicios.ReporteJornada();
        private readonly BLL.SubastaBLL           _bll       = new BLL.SubastaBLL();
        private readonly BLL.PostorBLL            _bllPostor = new BLL.PostorBLL();

        private BE.UnidadDeVenta                 _arbol;
        private Dictionary<int, BE.Adjudicacion> _mapaAdj;
        private Dictionary<int, BE.Subasta>      _mapaSubastas;
        private readonly List<FilaJornada>        _filas = new List<FilaJornada>();

        public FrmReporteJornada()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ActualizarStatus();
            Generar();
        }

        private void btnGenerar_Click(object sender, EventArgs e) => Generar();

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cboEstado.SelectedIndex = 0;
            cboTipo.SelectedIndex   = 0;
            chkSoloRaiz.Checked     = false;
            Generar();
        }

        private void Generar()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _servicio.ObtenerDatos(out _arbol, out _mapaAdj, out _mapaSubastas);

                _filas.Clear();
                RecorrerCatalogo(_arbol, 0);

                PoblarArbol();
                ActualizarTotales();
                ActualizarStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // ── Recorrido para KPIs (transparente por Composite) ─────────────────

        private void RecorrerCatalogo(BE.UnidadDeVenta nodo, int nivel)
        {
            if (nodo == null) return;

            if (nodo.Id == -1)
            {
                var hijosRaiz = nodo.ObtenerHijos();
                if (hijosRaiz != null)
                    foreach (var hijo in hijosRaiz)
                        RecorrerCatalogo(hijo, 0);
                return;
            }

            _mapaAdj.TryGetValue(nodo.Id, out BE.Adjudicacion adj);
            _mapaSubastas.TryGetValue(nodo.Id, out BE.Subasta subasta);

            int notificados = 0;
            if (subasta != null && subasta.EstaCerrada)
            {
                try
                {
                    // TODO: Use _bll.ObtenerNotificados(subasta.Id) for exact count at close time
                    notificados = _bllPostor.ObtenerSuscripcionesActivas(subasta.Id).Count;
                }
                catch { }
            }

            _filas.Add(new FilaJornada
            {
                Nivel         = nivel,
                Unidad        = nodo,
                Adj           = adj,
                Subasta       = subasta,
                EsLote        = nodo is BE.Lote,
                EstadoBadge   = adj != null ? "ADJUDICADA" : "DESIERTA",
                Adjudicatario = adj?.NombreGanador ?? "—",
                Notificados   = notificados
            });

            var hijos = nodo.ObtenerHijos();
            if (hijos != null)
                foreach (var hijo in hijos)
                    RecorrerCatalogo(hijo, nivel + 1);
        }

        // ── TreeView — recorrido jerárquico transparente ──────────────────────

        private void PoblarArbol()
        {
            tvCatalogo.BeginUpdate();
            tvCatalogo.Nodes.Clear();
            AgregarNodo(tvCatalogo.Nodes, _arbol, 0);
            tvCatalogo.ExpandAll();
            tvCatalogo.EndUpdate();
            lblConteo.Text = $"{ContarNodos(tvCatalogo.Nodes)} elemento(s) mostrado(s)";
        }

        private void AgregarNodo(TreeNodeCollection destino, BE.UnidadDeVenta nodo, int nivel)
        {
            if (nodo == null) return;

            if (nodo.Id == -1)
            {
                var hijos = nodo.ObtenerHijos();
                if (hijos != null)
                    foreach (var hijo in hijos)
                        AgregarNodo(destino, hijo, 0);
                return;
            }

            bool esLote = nodo is BE.Lote;
            _mapaAdj.TryGetValue(nodo.Id, out BE.Adjudicacion adj);
            _mapaSubastas.TryGetValue(nodo.Id, out BE.Subasta subasta);
            string estadoBadge = adj != null ? "ADJUDICADA" : "DESIERTA";

            bool soloLotes     = cboTipo.SelectedIndex == 1;
            bool soloArticulos = cboTipo.SelectedIndex == 2;

            if (soloArticulos && esLote)
            {
                if (!chkSoloRaiz.Checked)
                {
                    var h = nodo.ObtenerHijos();
                    if (h != null)
                        foreach (var hijo in h)
                            AgregarNodo(destino, hijo, nivel);
                }
                return;
            }
            if (soloLotes && !esLote) return;

            decimal precioBase = nodo.ObtenerPrecioBase();
            string prefijo = esLote ? "[LOTE]  " : "";
            string texto = adj != null
                ? $"{prefijo}{nodo.Nombre}  —  Base: ${precioBase:N2}  →  Final: ${adj.PrecioFinal:N2}  ({adj.NombreGanador})"
                : $"{prefijo}{nodo.Nombre}  —  Base: ${precioBase:N2}  [DESIERTA]";

            var tn = new TreeNode(texto);
            tn.ForeColor = adj != null ? Color.DarkGreen : Color.Gray;
            if (esLote)
                tn.NodeFont = new Font(tvCatalogo.Font, FontStyle.Bold);

            tn.Tag = new FilaJornada
            {
                Nivel         = nivel,
                Unidad        = nodo,
                Adj           = adj,
                Subasta       = subasta,
                EsLote        = esLote,
                EstadoBadge   = estadoBadge,
                Adjudicatario = adj?.NombreGanador ?? "—",
                Notificados   = 0
            };

            if (esLote && !chkSoloRaiz.Checked)
            {
                var hijos = nodo.ObtenerHijos();
                if (hijos != null)
                    foreach (var hijo in hijos)
                        AgregarNodo(tn.Nodes, hijo, nivel + 1);
            }

            string filtroEstado = cboEstado.SelectedIndex == 1 ? "ADJUDICADA" :
                                  cboEstado.SelectedIndex == 2 ? "DESIERTA"   : null;
            bool matchEstado = filtroEstado == null || estadoBadge == filtroEstado;

            if (matchEstado || (esLote && tn.Nodes.Count > 0))
                destino.Add(tn);
        }

        private int ContarNodos(TreeNodeCollection nodos)
        {
            int total = 0;
            foreach (TreeNode n in nodos) { total++; total += ContarNodos(n.Nodes); }
            return total;
        }

        // ── KPIs ──────────────────────────────────────────────────────────────

        private void ActualizarTotales()
        {
            int     total            = _filas.Count;
            int     adjudicados      = 0;
            decimal monto            = 0;
            int     totalNotificados = 0;

            var subastasContadas = new HashSet<int>();
            foreach (var f in _filas)
            {
                if (f.Adj != null) { adjudicados++; monto += f.Adj.PrecioFinal; }
                if (f.Subasta != null && f.Subasta.EstaCerrada && subastasContadas.Add(f.Subasta.Id))
                    totalNotificados += f.Notificados;
            }

            lblTotalItems.Text     = total.ToString();
            lblAdjudicados.Text    = adjudicados.ToString();
            lblDesiertos.Text      = (total - adjudicados).ToString();
            lblMontoRecaudado.Text = $"$ {monto:N2}";
            lblNotificados.Text    = totalNotificados.ToString();
        }

        // ── Exportar PDF ──────────────────────────────────────────────────────

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dlg = new SaveFileDialog())
                {
                    dlg.Filter   = "Archivo de texto|*.txt";
                    dlg.FileName = $"ReporteJornada_{DateTime.Today:yyyyMMdd}.txt";
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    GenerarTXT(dlg.FileName);
                    MessageBox.Show("Reporte exportado correctamente.", "Exportar TXT",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarTXT(string ruta)
        {
            var u  = Seguridad.SessionManager.GetInstance.Usuario;
            var sb = new StringBuilder();
            string sep = new string('=', 80);
            string lin = new string('-', 80);

            sb.AppendLine(sep);
            sb.AppendLine("  REPORTE CONSOLIDADO DE JORNADA");
            sb.AppendLine(sep);
            sb.AppendLine($"  Usuario : {u.NombreCompleto}");
            sb.AppendLine($"  Rol     : {u.Rol}");
            sb.AppendLine($"  Jornada : {DateTime.Today:dd/MM/yyyy}");
            sb.AppendLine($"  Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine(lin);
            sb.AppendLine($"  Total: {lblTotalItems.Text}   Adjudicados: {lblAdjudicados.Text}   " +
                          $"Desiertos: {lblDesiertos.Text}   Recaudado: {lblMontoRecaudado.Text}   " +
                          $"Notificados: {lblNotificados.Text}");
            sb.AppendLine(sep);
            sb.AppendLine();

            // Catálogo — refleja exactamente lo visible en pantalla (respeta filtros)
            EscribirNodos(tvCatalogo.Nodes, sb);

            sb.AppendLine();
            sb.AppendLine(sep);

            File.WriteAllText(ruta, sb.ToString(), Encoding.UTF8);
        }

        private void EscribirNodos(TreeNodeCollection nodos, StringBuilder sb)
        {
            foreach (TreeNode tn in nodos)
            {
                int   nivel  = NivelNodo(tn);
                bool  esLote = tn.Tag is FilaJornada f && f.EsLote;
                string indent = new string(' ', nivel * 4);
                string marca  = tn.ForeColor == Color.DarkGreen ? "[ADJ]" : "[DES]";

                if (esLote)
                    sb.AppendLine($"{indent}{marca} {tn.Text}");
                else
                    sb.AppendLine($"{indent}  {marca} {tn.Text}");

                EscribirNodos(tn.Nodes, sb);
            }
        }

        private int NivelNodo(TreeNode tn)
        {
            int n = 0;
            var p = tn.Parent;
            while (p != null) { n++; p = p.Parent; }
            return n;
        }

        // ── Status ────────────────────────────────────────────────────────────

        private void ActualizarStatus()
        {
            var u = Seguridad.SessionManager.GetInstance.Usuario;
            lblStatus.Text = $"Usuario: {u.NombreCompleto}  |  Rol: {u.Rol}  |  Jornada: {DateTime.Today:dd/MM/yyyy}  |  Generado: {DateTime.Now:HH:mm}";
        }

        private class FilaJornada
        {
            public int              Nivel         { get; set; }
            public BE.UnidadDeVenta Unidad        { get; set; }
            public BE.Adjudicacion  Adj           { get; set; }
            public BE.Subasta       Subasta       { get; set; }
            public bool             EsLote        { get; set; }
            public string           EstadoBadge   { get; set; }
            public string           Adjudicatario { get; set; }
            public int              Notificados   { get; set; }
        }
    }
}
