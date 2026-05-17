using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmGestionSubastas : Form, Servicios.IObserverPostor
    {
        private readonly BLL.SubastaBLL  _bllSubasta  = new BLL.SubastaBLL();
        private readonly BLL.PostorBLL   _bllPostor   = new BLL.PostorBLL();
        private readonly BLL.CatalogoBLL _bllCatalogo = new BLL.CatalogoBLL();

        private int _subastaObservadaId = -1;

        public FrmGestionSubastas()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarColumnasTab1();
            ConfigurarColumnasHistorial();
            RefrescarTab1();
            CargarUnidadesDisponibles();
            CargarSubastasParaCerrar();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (_subastaObservadaId != -1)
                _bllSubasta.DesuscribirObserver(_subastaObservadaId, this);
        }

        // ── Observer ─────────────────────────────────────────────────────────────

        public void Actualizar(BE.Subasta subasta)
        {
            if (InvokeRequired) { Invoke(new Action<BE.Subasta>(Actualizar), subasta); return; }

            lblPrecioVigenteActual.Text = $"$ {subasta.PrecioVigente:N2}";
            nudMonto.Minimum = subasta.PrecioVigente + 1;
            if (nudMonto.Value < nudMonto.Minimum)
                nudMonto.Value = nudMonto.Minimum;

            CargarHistorialPujas(subasta.Id);

            foreach (DataGridViewRow row in dgvSubastasActivas.Rows)
            {
                if (row.Tag is BE.Subasta s && s.Id == subasta.Id)
                {
                    row.Cells["colPrecioVigente"].Value = $"$ {subasta.PrecioVigente:N2}";
                    row.Tag = subasta;
                    break;
                }
            }
        }

        // ── Tab 1: Subastas Activas ──────────────────────────────────────────────

        private void ConfigurarColumnasTab1()
        {
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "colId"; colId.HeaderText = "ID"; colId.Width = 45; colId.ReadOnly = true;

            DataGridViewTextBoxColumn colProducto = new DataGridViewTextBoxColumn();
            colProducto.Name = "colProducto"; colProducto.HeaderText = "Producto"; colProducto.Width = 175; colProducto.ReadOnly = true;

            DataGridViewTextBoxColumn colTipo = new DataGridViewTextBoxColumn();
            colTipo.Name = "colTipo"; colTipo.HeaderText = "Tipo"; colTipo.Width = 75; colTipo.ReadOnly = true;

            DataGridViewTextBoxColumn colPrecioBase = new DataGridViewTextBoxColumn();
            colPrecioBase.Name = "colPrecioBase"; colPrecioBase.HeaderText = "P. Base"; colPrecioBase.Width = 100; colPrecioBase.ReadOnly = true;

            DataGridViewTextBoxColumn colPrecioVigente = new DataGridViewTextBoxColumn();
            colPrecioVigente.Name = "colPrecioVigente"; colPrecioVigente.HeaderText = "P. Vigente"; colPrecioVigente.Width = 110; colPrecioVigente.ReadOnly = true;

            DataGridViewTextBoxColumn colSuscriptores = new DataGridViewTextBoxColumn();
            colSuscriptores.Name = "colSuscriptores"; colSuscriptores.HeaderText = "Suscriptores"; colSuscriptores.Width = 95; colSuscriptores.ReadOnly = true;

            DataGridViewTextBoxColumn colPujas = new DataGridViewTextBoxColumn();
            colPujas.Name = "colPujas"; colPujas.HeaderText = "Pujas"; colPujas.Width = 60; colPujas.ReadOnly = true;

            DataGridViewTextBoxColumn colApertura = new DataGridViewTextBoxColumn();
            colApertura.Name = "colApertura"; colApertura.HeaderText = "Apertura"; colApertura.Width = 130; colApertura.ReadOnly = true;

            dgvSubastasActivas.Columns.AddRange(colId, colProducto, colTipo,
                colPrecioBase, colPrecioVigente, colSuscriptores, colPujas, colApertura);
        }

        private void ConfigurarColumnasHistorial()
        {
            DataGridViewTextBoxColumn colPostor = new DataGridViewTextBoxColumn();
            colPostor.Name = "colPostor"; colPostor.HeaderText = "Postor"; colPostor.Width = 155; colPostor.ReadOnly = true;

            DataGridViewTextBoxColumn colMonto = new DataGridViewTextBoxColumn();
            colMonto.Name = "colMonto"; colMonto.HeaderText = "Monto"; colMonto.Width = 110; colMonto.ReadOnly = true;

            DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
            colEstado.Name = "colEstado"; colEstado.HeaderText = "Estado"; colEstado.Width = 85; colEstado.ReadOnly = true;

            DataGridViewTextBoxColumn colFecha = new DataGridViewTextBoxColumn();
            colFecha.Name = "colFecha"; colFecha.HeaderText = "Fecha/Hora"; colFecha.Width = 115; colFecha.ReadOnly = true;

            DataGridViewTextBoxColumn colMotivo = new DataGridViewTextBoxColumn();
            colMotivo.Name = "colMotivo"; colMotivo.HeaderText = "Motivo Rechazo";
            colMotivo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; colMotivo.ReadOnly = true;

            dgvHistorialPujas.Columns.AddRange(colPostor, colMonto, colEstado, colFecha, colMotivo);
        }

        private void RefrescarTab1()
        {
            if (_subastaObservadaId != -1)
            {
                _bllSubasta.DesuscribirObserver(_subastaObservadaId, this);
                _subastaObservadaId = -1;
            }

            dgvSubastasActivas.Rows.Clear();
            try
            {
                List<BE.Subasta> activas = _bllSubasta.ObtenerActivas();
                foreach (BE.Subasta s in activas)
                {
                    List<BE.Suscripcion> suscs = _bllPostor.ObtenerSuscripcionesActivas(s.Id);
                    List<BE.Puja>        pujas = _bllSubasta.ObtenerHistorialPujas(s.Id);
                    int idx = dgvSubastasActivas.Rows.Add(
                        s.Id,
                        s.NombreUnidad,
                        s.TipoUnidad,
                        $"$ {s.PrecioInicial:N2}",
                        $"$ {s.PrecioVigente:N2}",
                        suscs.Count,
                        pujas.Count,
                        s.FechaApertura.ToString("dd/MM/yyyy HH:mm")
                    );
                    dgvSubastasActivas.Rows[idx].Tag = s;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar subastas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LimpiarPanelDerecho();
        }

        private void dgvSubastasActivas_SelectionChanged(object sender, EventArgs e)
        {
            if (_subastaObservadaId != -1)
            {
                _bllSubasta.DesuscribirObserver(_subastaObservadaId, this);
                _subastaObservadaId = -1;
            }

            if (dgvSubastasActivas.SelectedRows.Count == 0 ||
                !(dgvSubastasActivas.SelectedRows[0].Tag is BE.Subasta sub))
            {
                LimpiarPanelDerecho();
                return;
            }

            _subastaObservadaId = sub.Id;
            _bllSubasta.SuscribirObserver(sub.Id, this);
            CargarPanelDerecho(sub);
        }

        private void CargarPanelDerecho(BE.Subasta sub)
        {
            lblPrecioVigenteActual.Text = $"$ {sub.PrecioVigente:N2}";

            cmbPostorPuja.Items.Clear();
            cmbPostorSuscribir.Items.Clear();
            try
            {
                List<BE.Postor> postores = _bllPostor.ObtenerTodos();
                foreach (BE.Postor p in postores)
                {
                    ItemCombo ic = new ItemCombo();
                    ic.Id   = p.Id;
                    ic.Texto = p.Nombre;
                    cmbPostorPuja.Items.Add(ic);

                    ItemCombo ic2 = new ItemCombo();
                    ic2.Id   = p.Id;
                    ic2.Texto = p.Nombre;
                    cmbPostorSuscribir.Items.Add(ic2);
                }
                if (cmbPostorPuja.Items.Count      > 0) cmbPostorPuja.SelectedIndex      = 0;
                if (cmbPostorSuscribir.Items.Count > 0) cmbPostorSuscribir.SelectedIndex = 0;
            }
            catch { }

            nudMonto.Minimum = sub.PrecioVigente + 1;
            nudMonto.Maximum = 99999999;
            nudMonto.Value   = sub.PrecioVigente + 1;
            lblValidacionMonto.Text = "";

            CargarSuscriptores(sub.Id);
            CargarHistorialPujas(sub.Id);

            grpPuja.Enabled         = true;
            grpSuscriptores.Enabled = true;
        }

        private void LimpiarPanelDerecho()
        {
            lblPrecioVigenteActual.Text = "—";
            cmbPostorPuja.Items.Clear();
            nudMonto.Minimum = 1;
            nudMonto.Maximum = 99999999;
            nudMonto.Value   = 1;
            lblValidacionMonto.Text = "";
            cmbPostorSuscribir.Items.Clear();
            lstSuscriptores.Items.Clear();
            dgvHistorialPujas.Rows.Clear();
            grpPuja.Enabled         = false;
            grpSuscriptores.Enabled = false;
        }

        private void CargarSuscriptores(int idSubasta)
        {
            lstSuscriptores.Items.Clear();
            try
            {
                List<BE.Suscripcion> suscs = _bllPostor.ObtenerSuscripcionesActivas(idSubasta);
                foreach (BE.Suscripcion s in suscs)
                {
                    ItemCombo ic = new ItemCombo();
                    ic.Id   = s.IdPostor;
                    ic.Texto = s.NombrePostor;
                    lstSuscriptores.Items.Add(ic);
                }
            }
            catch { }
        }

        private void CargarHistorialPujas(int idSubasta)
        {
            dgvHistorialPujas.Rows.Clear();
            try
            {
                List<BE.Puja> pujas = _bllSubasta.ObtenerHistorialPujas(idSubasta);
                foreach (BE.Puja p in pujas)
                {
                    int idx = dgvHistorialPujas.Rows.Add(
                        p.NombrePostor,
                        $"$ {p.Monto:N2}",
                        p.Estado == BE.EstadoPuja.Aceptada ? "Aceptada" : "Rechazada",
                        p.FechaHora.ToString("dd/MM HH:mm:ss"),
                        p.MotivoRechazo ?? ""
                    );
                    dgvHistorialPujas.Rows[idx].DefaultCellStyle.BackColor =
                        p.Estado == BE.EstadoPuja.Aceptada
                            ? Color.FromArgb(220, 240, 220)
                            : Color.FromArgb(240, 220, 220);
                }
            }
            catch { }
        }

        private void btnRegistrarPuja_Click(object sender, EventArgs e)
        {
            if (dgvSubastasActivas.SelectedRows.Count == 0) return;
            if (!(dgvSubastasActivas.SelectedRows[0].Tag is BE.Subasta sub)) return;
            if (!(cmbPostorPuja.SelectedItem is ItemCombo postor))
            {
                MessageBox.Show("Seleccione un postor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BE.Puja puja = _bllSubasta.RegistrarPuja(sub.Id, postor.Id.Value, nudMonto.Value);
                if (puja.Estado == BE.EstadoPuja.Aceptada)
                {
                    lblValidacionMonto.ForeColor = Color.Green;
                    lblValidacionMonto.Text = $"Puja aceptada: $ {puja.Monto:N2}";
                }
                else
                {
                    lblValidacionMonto.ForeColor = Color.Red;
                    lblValidacionMonto.Text = $"Rechazada: {puja.MotivoRechazo}";
                }
            }
            catch (Exception ex)
            {
                lblValidacionMonto.ForeColor = Color.Red;
                lblValidacionMonto.Text = ex.Message;
            }
        }

        private void btnSuscribir_Click(object sender, EventArgs e)
        {
            if (dgvSubastasActivas.SelectedRows.Count == 0) return;
            if (!(dgvSubastasActivas.SelectedRows[0].Tag is BE.Subasta sub)) return;
            if (!(cmbPostorSuscribir.SelectedItem is ItemCombo postor)) return;

            try
            {
                _bllPostor.Suscribir(postor.Id.Value, sub.Id);
                CargarSuscriptores(sub.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesuscribir_Click(object sender, EventArgs e)
        {
            if (dgvSubastasActivas.SelectedRows.Count == 0) return;
            if (!(dgvSubastasActivas.SelectedRows[0].Tag is BE.Subasta sub)) return;
            if (!(lstSuscriptores.SelectedItem is ItemCombo postor))
            {
                MessageBox.Show("Seleccione un suscriptor de la lista.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _bllPostor.Desuscribir(postor.Id.Value, sub.Id);
                CargarSuscriptores(sub.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Tab 2: Abrir Subasta ─────────────────────────────────────────────────

        private void CargarUnidadesDisponibles()
        {
            cmbUnidadAbrir.Items.Clear();
            lblPrecioBaseAbrir.Text = "";
            try
            {
                List<BE.UnidadDeVenta>    todas  = _bllCatalogo.ObtenerTodos();
                List<BE.Subasta>          activas = _bllSubasta.ObtenerActivas();
                System.Collections.Generic.HashSet<int> idsEnSubasta =
                    new System.Collections.Generic.HashSet<int>();
                foreach (BE.Subasta s in activas) idsEnSubasta.Add(s.IdUnidad);

                foreach (BE.UnidadDeVenta u in todas)
                {
                    if (!idsEnSubasta.Contains(u.Id))
                    {
                        ItemCombo ic = new ItemCombo();
                        ic.Id    = u.Id;
                        ic.Texto = u.Nombre;
                        cmbUnidadAbrir.Items.Add(ic);
                    }
                }
            }
            catch { }
            if (cmbUnidadAbrir.Items.Count > 0) cmbUnidadAbrir.SelectedIndex = 0;
        }

        private void cmbUnidadAbrir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cmbUnidadAbrir.SelectedItem is ItemCombo item) || !item.Id.HasValue)
            {
                lblPrecioBaseAbrir.Text = "";
                return;
            }
            try
            {
                decimal precioBase = _bllCatalogo.ObtenerPrecioBase(item.Id.Value);
                lblPrecioBaseAbrir.Text = $"Precio base: $ {precioBase:N2}";
            }
            catch { lblPrecioBaseAbrir.Text = ""; }
        }

        private void btnAbrirSubasta_Click(object sender, EventArgs e)
        {
            if (!(cmbUnidadAbrir.SelectedItem is ItemCombo item) || !item.Id.HasValue)
            {
                MessageBox.Show("Seleccione una unidad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idMartillero = Seguridad.SessionManager.GetInstance.Usuario.Id;
                int idSubasta    = _bllSubasta.AbrirSubasta(item.Id.Value, idMartillero);
                MessageBox.Show($"Subasta #{idSubasta} abierta correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefrescarTab1();
                CargarUnidadesDisponibles();
                CargarSubastasParaCerrar();
                tabControl.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Tab 3: Cerrar Subasta ────────────────────────────────────────────────

        private void CargarSubastasParaCerrar()
        {
            cmbSubastaCerrar.Items.Clear();
            try
            {
                List<BE.Subasta> activas = _bllSubasta.ObtenerActivas();
                foreach (BE.Subasta s in activas)
                {
                    ItemComboSubasta ic = new ItemComboSubasta();
                    ic.Subasta = s;
                    ic.Texto   = $"#{s.Id} — {s.NombreUnidad}";
                    cmbSubastaCerrar.Items.Add(ic);
                }
            }
            catch { }
            if (cmbSubastaCerrar.Items.Count > 0)
                cmbSubastaCerrar.SelectedIndex = 0;
            else
                LimpiarResumenCierre();
        }

        private void cmbSubastaCerrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cmbSubastaCerrar.SelectedItem is ItemComboSubasta item)) { LimpiarResumenCierre(); return; }
            BE.Subasta s = item.Subasta;
            try
            {
                List<BE.Suscripcion> suscs = _bllPostor.ObtenerSuscripcionesActivas(s.Id);
                List<BE.Puja>        pujas = _bllSubasta.ObtenerHistorialPujas(s.Id);
                BE.Puja mejorPuja = pujas.FindLast(p => p.Estado == BE.EstadoPuja.Aceptada);

                lblResumenProducto.Text          = s.NombreUnidad;
                lblResumenPrecioVigente.Text      = $"$ {s.PrecioVigente:N2}";
                lblResumenGanador.Text            = mejorPuja != null ? mejorPuja.NombrePostor : "Sin pujas";
                lblResumenTotalPujas.Text         = pujas.Count.ToString();
                lblResumenTotalSuscriptores.Text  = suscs.Count.ToString();
            }
            catch { LimpiarResumenCierre(); }
        }

        private void LimpiarResumenCierre()
        {
            lblResumenProducto.Text         = "—";
            lblResumenPrecioVigente.Text     = "—";
            lblResumenGanador.Text           = "—";
            lblResumenTotalPujas.Text        = "—";
            lblResumenTotalSuscriptores.Text = "—";
        }

        private void btnCerrarSubasta_Click(object sender, EventArgs e)
        {
            if (!(cmbSubastaCerrar.SelectedItem is ItemComboSubasta item))
            {
                MessageBox.Show("Seleccione una subasta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BE.Subasta s = item.Subasta;
            try
            {
                List<BE.Suscripcion> suscs = _bllPostor.ObtenerSuscripcionesActivas(s.Id);
                string confirmMsg = $"¿Cerrar la subasta de «{s.NombreUnidad}»?\n" +
                                    $"Se notificará a {suscs.Count} suscriptor(es).";

                if (MessageBox.Show(confirmMsg, "Confirmar cierre",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                int idMartillero = Seguridad.SessionManager.GetInstance.Usuario.Id;
                _bllSubasta.CerrarSubasta(s.Id, idMartillero, txtObservaciones.Text.Trim());

                MessageBox.Show("Subasta cerrada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefrescarTab1();
                CargarUnidadesDisponibles();
                CargarSubastasParaCerrar();
                tabControl.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── DTOs internos ────────────────────────────────────────────────────────

        private class ItemCombo
        {
            public int?   Id    { get; set; }
            public string Texto { get; set; }
            public override string ToString() => Texto;
        }

        private class ItemComboSubasta
        {
            public BE.Subasta Subasta { get; set; }
            public string     Texto   { get; set; }
            public override string ToString() => Texto;
        }
    }
}
