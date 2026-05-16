using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmRegistrarPuja : Form
    {
        private readonly BLL.SubastaBLL _bllSubasta = new BLL.SubastaBLL();
        private readonly BLL.PostorBLL  _bllPostor  = new BLL.PostorBLL();

        public FrmRegistrarPuja()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                var activas = _bllSubasta.ObtenerActivas();
                cboSubasta.DataSource    = activas;
                cboSubasta.DisplayMember = "NombreUnidad";
                cboSubasta.ValueMember   = "Id";
                cboSubasta.SelectedIndex = activas.Count > 0 ? 0 : -1;

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
            }
            else
            {
                lblPrecioVigente.Text   = "Precio vigente: —";
                dgvHistorial.DataSource = null;
            }
        }

        private void CargarHistorial(int idSubasta)
        {
            try
            {
                var historial = _bllSubasta.ObtenerHistorialPujas(idSubasta);
                dgvHistorial.AutoGenerateColumns = false;
                if (dgvHistorial.Columns.Count == 0)
                {
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombrePostor", HeaderText = "Postor",  Width = 160 });
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Monto",        HeaderText = "Monto",   Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado",       HeaderText = "Estado",  Width = 100 });
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaHora",    HeaderText = "Fecha/Hora", Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" } });
                    dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MotivoRechazo", HeaderText = "Motivo rechazo", Width = 200 });
                    dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                {
                    MessageBox.Show($"Puja ACEPTADA: $ {puja.Monto:N2}\nNuevo precio vigente de la subasta.",
                        "Puja registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Puja RECHAZADA.\n{puja.MotivoRechazo}",
                        "Puja rechazada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                CargarDatos();
                if (cboSubasta.SelectedItem is BE.Subasta s2)
                    CargarHistorial(s2.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e) => CargarDatos();
    }
}
