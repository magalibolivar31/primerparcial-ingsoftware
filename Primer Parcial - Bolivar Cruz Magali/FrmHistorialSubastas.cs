using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmHistorialSubastas : Form
    {
        private readonly BLL.SubastaBLL _bll = new BLL.SubastaBLL();

        public FrmHistorialSubastas()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
            Buscar();
        }

        private void btnBuscar_Click(object sender, EventArgs e) => Buscar();

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpDesde.Value             = DateTime.Today.AddMonths(-1);
            dtpHasta.Value             = DateTime.Today;
            txtFiltroUnidad.Clear();
            txtFiltroGanador.Clear();
            cboResultado.SelectedIndex = 0;
            Buscar();
        }

        private void Buscar()
        {
            try
            {
                string resultado;
                switch (cboResultado.SelectedIndex)
                {
                    case 1:  resultado = "ADJUDICADA"; break;
                    case 2:  resultado = "DESIERTA";   break;
                    default: resultado = null;         break;
                }

                List<BE.Subasta> lista = _bll.ObtenerHistorial(
                    dtpDesde.Value.Date,
                    dtpHasta.Value.Date,
                    txtFiltroUnidad.Text.Trim(),
                    txtFiltroGanador.Text.Trim(),
                    resultado);

                dgvSubastas.AutoGenerateColumns = false;
                if (dgvSubastas.Columns.Count == 0)
                {
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id",                HeaderText = "ID",         Width = 45  });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreUnidad",      HeaderText = "Unidad",     Width = 220 });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioInicial",     HeaderText = "P. Base",    Width = 90,  DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioFinal",       HeaderText = "P. Final",   Width = 90,  DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreGanador",     HeaderText = "Ganador",    Width = 160 });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreMartillero",  HeaderText = "Martillero", Width = 140 });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaApertura",     HeaderText = "Apertura",   Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
                    dgvSubastas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaCierre",       HeaderText = "Cierre",     Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
                    dgvSubastas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvSubastas.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                }

                dgvSubastas.DataSource = null;
                dgvSubastas.DataSource = lista;
                lblConteoSubastas.Text = $"{lista.Count} subasta(s) encontrada(s).";
                LimpiarPujas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSubastas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSubastas.CurrentRow?.DataBoundItem is BE.Subasta s)
                CargarPujas(s);
            else
                LimpiarPujas();
        }

        private void CargarPujas(BE.Subasta subasta)
        {
            try
            {
                List<BE.Puja> pujas = _bll.ObtenerHistorialPujas(subasta.Id);

                dgvPujas.AutoGenerateColumns = false;
                if (dgvPujas.Columns.Count == 0)
                {
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombrePostor",  HeaderText = "Postor",         Width = 180 });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Monto",         HeaderText = "Monto",          Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado",        HeaderText = "Estado",         Width = 100 });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaHora",     HeaderText = "Fecha / Hora",   Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" } });
                    dgvPujas.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MotivoRechazo", HeaderText = "Motivo Rechazo", Width = 250 });
                    dgvPujas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvPujas.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                }

                dgvPujas.DataSource = null;
                dgvPujas.DataSource = pujas;

                var postoresUnicos = new HashSet<int>();
                int aceptadas = 0;
                foreach (BE.Puja p in pujas)
                {
                    postoresUnicos.Add(p.IdPostor);
                    if (p.Estado == BE.EstadoPuja.Aceptada) aceptadas++;
                }

                string ganador = subasta.NombreGanador != null
                    ? $"Ganador: {subasta.NombreGanador} — $ {subasta.PrecioFinal:N2}"
                    : "Desierta (sin ganador)";

                lblSumarioPujas.Text = $"{pujas.Count} puja(s)  ·  {aceptadas} aceptada(s)  ·  {postoresUnicos.Count} postor(es) participante(s)  ·  {ganador}";
                grpPujas.Text        = $"Pujas — #{subasta.Id}: {subasta.NombreUnidad}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarPujas()
        {
            dgvPujas.DataSource  = null;
            lblSumarioPujas.Text = "Seleccione una subasta para ver el detalle de pujas.";
            grpPujas.Text        = "Detalle de Pujas";
        }
    }
}
