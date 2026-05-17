using System;
using System.Windows.Forms;

namespace GUI
{
    // RF-05 / RF-06 / RF-07 / RF-08 — PATRON OBSERVER (Concrete Observer)
    //
    // Este formulario implementa IObserverPostor y se registra como suscriptor
    // en el Subject (GestorNotificaciones) cada vez que el martillero selecciona
    // una subasta activa. Cuando otra puja es aceptada (RF-06) o la subasta se
    // cierra (RF-07), el Subject invoca Actualizar() de forma automática — sin
    // que este formulario tenga que "preguntar" nada (push, no pull).
    // Al cambiar de subasta o cerrar el formulario se desuscribe de inmediato (RF-08).
    public partial class FrmRegistrarPuja : Form, Servicios.IObserverPostor
    {
        private readonly BLL.SubastaBLL _bllSubasta = new BLL.SubastaBLL();
        private readonly BLL.PostorBLL  _bllPostor  = new BLL.PostorBLL();

        // Id de la subasta a la que estamos suscriptos actualmente; -1 = ninguna.
        private int _subastaObservadaId = -1;

        public FrmRegistrarPuja()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            pnlAlerta.Visible = false;
            lblObserverStatus.Text      = "Sin suscripción activa";
            lblObserverStatus.ForeColor = System.Drawing.Color.Gray;
            CargarDatos();
        }

        // RF-08: al cerrar el formulario, desuscribirse de inmediato.
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DesuscribirActual();
            base.OnFormClosing(e);
        }

        // ── IObserverPostor ────────────────────────────────────────────────────

        // RF-06 / RF-07: el Subject (GestorNotificaciones) invoca este método
        // automáticamente cuando el precio cambia o la subasta se cierra.
        // InvokeRequired cubre el caso de llamadas desde otro hilo.
        public void Actualizar(BE.Subasta subasta)
        {
            if (InvokeRequired) { Invoke(new Action(() => Actualizar(subasta))); return; }

            if (subasta.EstaCerrada)
            {
                string resultado = subasta.EsDesierta
                    ? "Subasta CERRADA — desierta (sin ganador)"
                    : $"Subasta CERRADA — Ganador: {subasta.NombreGanador}  |  Precio final: $ {subasta.PrecioFinal:N2}";
                lblPrecioVigente.Text = resultado;
                MostrarAlerta(resultado, System.Drawing.Color.FromArgb(180, 50, 50));
                DesuscribirActual(); // RF-08: la subasta cerró, nada más que seguir
            }
            else
            {
                lblPrecioVigente.Text = $"Precio vigente: $ {subasta.PrecioVigente:N2}";
                nudMonto.Minimum      = subasta.PrecioVigente + 0.01m;
                if (nudMonto.Value < nudMonto.Minimum) nudMonto.Value = nudMonto.Minimum;
                MostrarAlerta($"Nuevo precio vigente: $ {subasta.PrecioVigente:N2}",
                              System.Drawing.Color.FromArgb(25, 130, 55));
            }

            CargarHistorial(subasta.Id);
        }

        // ── Suscripción Observer ──────────────────────────────────────────────

        // RF-05: registrarse en el Subject de la subasta seleccionada.
        private void SuscribirA(int idSubasta)
        {
            DesuscribirActual();
            _bllSubasta.SuscribirObserver(idSubasta, this);
            _subastaObservadaId       = idSubasta;
            lblObserverStatus.Text      = $"Suscripto a subasta #{idSubasta} — recibirás alertas automáticas";
            lblObserverStatus.ForeColor = System.Drawing.Color.DarkGreen;
        }

        // RF-08: darse de baja del Subject activo.
        private void DesuscribirActual()
        {
            if (_subastaObservadaId < 0) return;
            _bllSubasta.DesuscribirObserver(_subastaObservadaId, this);
            _subastaObservadaId         = -1;
            lblObserverStatus.Text      = "Sin suscripción activa";
            lblObserverStatus.ForeColor = System.Drawing.Color.Gray;
        }

        private void MostrarAlerta(string mensaje, System.Drawing.Color color)
        {
            lblAlerta.Text      = mensaje;
            pnlAlerta.BackColor = color;
            pnlAlerta.Visible   = true;
        }

        // ── Carga de datos y handlers ─────────────────────────────────────────

        private void CargarDatos()
        {
            try
            {
                int idAnterior = _subastaObservadaId;

                var activas = _bllSubasta.ObtenerActivas();
                cboSubasta.DataSource    = activas;
                cboSubasta.DisplayMember = "NombreUnidad";
                cboSubasta.ValueMember   = "Id";
                cboSubasta.SelectedIndex = activas.Count > 0 ? 0 : -1;

                // Restaurar suscripción si la subasta anterior sigue activa
                if (idAnterior > 0 && cboSubasta.SelectedIndex >= 0)
                {
                    for (int i = 0; i < activas.Count; i++)
                    {
                        if (activas[i].Id == idAnterior)
                        { cboSubasta.SelectedIndex = i; break; }
                    }
                }

                var postores = _bllPostor.ObtenerTodos();
                cboPostor.DataSource    = postores;
                cboPostor.DisplayMember = "Nombre";
                cboPostor.ValueMember   = "Id";
                cboPostor.SelectedIndex = postores.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboSubasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSubasta.SelectedItem is BE.Subasta s)
            {
                lblPrecioVigente.Text = $"Precio vigente: $ {s.PrecioVigente:N2}";
                nudMonto.Minimum      = s.PrecioVigente + 0.01m;
                nudMonto.Value        = nudMonto.Minimum;
                CargarHistorial(s.Id);
                SuscribirA(s.Id);  // RF-05
            }
            else
            {
                lblPrecioVigente.Text   = "Precio vigente: —";
                dgvHistorial.DataSource = null;
                DesuscribirActual();   // RF-08
            }
            pnlAlerta.Visible = false;
        }

        private void CargarHistorial(int idSubasta)
        {
            try
            {
                var historial = _bllSubasta.ObtenerHistorialPujas(idSubasta);
                dgvHistorial.AutoGenerateColumns = false;
                if (dgvHistorial.Columns.Count == 0)
                {
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombrePostor",  HeaderText = "Postor",        Width = 160 });
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Monto",         HeaderText = "Monto",         Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado",        HeaderText = "Estado",        Width = 100 });
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaHora",     HeaderText = "Fecha/Hora",    Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" } });
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MotivoRechazo", HeaderText = "Motivo rechazo",Width = 200 });
                    dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvHistorial.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                }
                dgvHistorial.DataSource = null;
                dgvHistorial.DataSource = historial;
            }
            catch { /* historial no crítico */ }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!(cboSubasta.SelectedItem is BE.Subasta subasta))
            {
                MessageBox.Show("Seleccione una subasta activa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!(cboPostor.SelectedItem is BE.Postor postor))
            {
                MessageBox.Show("Seleccione un postor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BE.Puja puja = _bllSubasta.RegistrarPuja(subasta.Id, postor.Id, nudMonto.Value);

                if (puja.Estado == BE.EstadoPuja.Aceptada)
                    MessageBox.Show($"Puja ACEPTADA: $ {puja.Monto:N2}\nNuevo precio vigente de la subasta.",
                        "Puja registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show($"Puja RECHAZADA.\n{puja.MotivoRechazo}",
                        "Puja rechazada", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                CargarDatos();
                if (cboSubasta.SelectedItem is BE.Subasta s2) CargarHistorial(s2.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e) => CargarDatos();
    }
}
