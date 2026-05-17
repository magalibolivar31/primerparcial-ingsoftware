namespace GUI
{
    partial class FrmGestionSubastas
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSubastasActivas = new System.Windows.Forms.DataGridView();
            this.grpPuja = new System.Windows.Forms.GroupBox();
            this.lblLblPrecioVigente = new System.Windows.Forms.Label();
            this.lblPrecioVigenteActual = new System.Windows.Forms.Label();
            this.lblLblPostorPuja = new System.Windows.Forms.Label();
            this.cmbPostorPuja = new System.Windows.Forms.ComboBox();
            this.lblLblMonto = new System.Windows.Forms.Label();
            this.nudMonto = new System.Windows.Forms.NumericUpDown();
            this.btnRegistrarPuja = new System.Windows.Forms.Button();
            this.lblValidacionMonto = new System.Windows.Forms.Label();
            this.grpSuscriptores = new System.Windows.Forms.GroupBox();
            this.lstSuscriptores = new System.Windows.Forms.ListBox();
            this.cmbPostorSuscribir = new System.Windows.Forms.ComboBox();
            this.btnSuscribir = new System.Windows.Forms.Button();
            this.btnDesuscribir = new System.Windows.Forms.Button();
            this.grpHistorial = new System.Windows.Forms.GroupBox();
            this.dgvHistorialPujas = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblUnidadAbrir = new System.Windows.Forms.Label();
            this.cmbUnidadAbrir = new System.Windows.Forms.ComboBox();
            this.lblPrecioBaseAbrir = new System.Windows.Forms.Label();
            this.btnAbrirSubasta = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblSubastaCerrar = new System.Windows.Forms.Label();
            this.cmbSubastaCerrar = new System.Windows.Forms.ComboBox();
            this.grpResumen = new System.Windows.Forms.GroupBox();
            this.lblRProd = new System.Windows.Forms.Label();
            this.lblResumenProducto = new System.Windows.Forms.Label();
            this.lblRPrecio = new System.Windows.Forms.Label();
            this.lblResumenPrecioVigente = new System.Windows.Forms.Label();
            this.lblRGan = new System.Windows.Forms.Label();
            this.lblResumenGanador = new System.Windows.Forms.Label();
            this.lblRPujas = new System.Windows.Forms.Label();
            this.lblResumenTotalPujas = new System.Windows.Forms.Label();
            this.lblRSusc = new System.Windows.Forms.Label();
            this.lblResumenTotalSuscriptores = new System.Windows.Forms.Label();
            this.lblObsCerrar = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.btnCerrarSubasta = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubastasActivas)).BeginInit();
            this.grpPuja.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).BeginInit();
            this.grpSuscriptores.SuspendLayout();
            this.grpHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialPujas)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.grpResumen.SuspendLayout();
            this.SuspendLayout();

            //
            // tabControl
            //
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1100, 670);
            this.tabControl.TabIndex = 0;
            //
            // tabPage1
            //
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1092, 637);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "🔨 Subastas Activas";
            this.tabPage1.UseVisualStyleBackColor = true;
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.dgvSubastasActivas);
            this.splitContainer1.Panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.grpHistorial,
                this.grpSuscriptores,
                this.grpPuja
            });
            this.splitContainer1.Size = new System.Drawing.Size(1086, 631);
            this.splitContainer1.SplitterDistance = 630;
            this.splitContainer1.TabIndex = 0;
            //
            // dgvSubastasActivas
            //
            this.dgvSubastasActivas.AllowUserToAddRows = false;
            this.dgvSubastasActivas.AllowUserToDeleteRows = false;
            this.dgvSubastasActivas.AutoGenerateColumns = false;
            this.dgvSubastasActivas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubastasActivas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubastasActivas.Location = new System.Drawing.Point(0, 0);
            this.dgvSubastasActivas.MultiSelect = false;
            this.dgvSubastasActivas.Name = "dgvSubastasActivas";
            this.dgvSubastasActivas.ReadOnly = true;
            this.dgvSubastasActivas.RowHeadersVisible = false;
            this.dgvSubastasActivas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubastasActivas.Size = new System.Drawing.Size(630, 631);
            this.dgvSubastasActivas.TabIndex = 0;
            this.dgvSubastasActivas.SelectionChanged += new System.EventHandler(this.dgvSubastasActivas_SelectionChanged);
            //
            // grpPuja  (Dock=Top — aparece arriba)
            //
            this.grpPuja.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblValidacionMonto,
                this.btnRegistrarPuja,
                this.nudMonto,
                this.lblLblMonto,
                this.cmbPostorPuja,
                this.lblLblPostorPuja,
                this.lblPrecioVigenteActual,
                this.lblLblPrecioVigente
            });
            this.grpPuja.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpPuja.Enabled = false;
            this.grpPuja.Location = new System.Drawing.Point(0, 0);
            this.grpPuja.Name = "grpPuja";
            this.grpPuja.Size = new System.Drawing.Size(452, 200);
            this.grpPuja.TabIndex = 0;
            this.grpPuja.TabStop = false;
            this.grpPuja.Text = "Registrar Puja";
            //
            // lblLblPrecioVigente
            //
            this.lblLblPrecioVigente.AutoSize = true;
            this.lblLblPrecioVigente.Location = new System.Drawing.Point(8, 24);
            this.lblLblPrecioVigente.Name = "lblLblPrecioVigente";
            this.lblLblPrecioVigente.Size = new System.Drawing.Size(95, 15);
            this.lblLblPrecioVigente.TabIndex = 0;
            this.lblLblPrecioVigente.Text = "Precio Vigente:";
            //
            // lblPrecioVigenteActual
            //
            this.lblPrecioVigenteActual.AutoSize = false;
            this.lblPrecioVigenteActual.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPrecioVigenteActual.ForeColor = System.Drawing.Color.Green;
            this.lblPrecioVigenteActual.Location = new System.Drawing.Point(115, 16);
            this.lblPrecioVigenteActual.Name = "lblPrecioVigenteActual";
            this.lblPrecioVigenteActual.Size = new System.Drawing.Size(210, 32);
            this.lblPrecioVigenteActual.TabIndex = 1;
            this.lblPrecioVigenteActual.Text = "—";
            //
            // lblLblPostorPuja
            //
            this.lblLblPostorPuja.AutoSize = true;
            this.lblLblPostorPuja.Location = new System.Drawing.Point(8, 62);
            this.lblLblPostorPuja.Name = "lblLblPostorPuja";
            this.lblLblPostorPuja.Size = new System.Drawing.Size(44, 15);
            this.lblLblPostorPuja.TabIndex = 2;
            this.lblLblPostorPuja.Text = "Postor:";
            //
            // cmbPostorPuja
            //
            this.cmbPostorPuja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPostorPuja.FormattingEnabled = true;
            this.cmbPostorPuja.Location = new System.Drawing.Point(70, 59);
            this.cmbPostorPuja.Name = "cmbPostorPuja";
            this.cmbPostorPuja.Size = new System.Drawing.Size(370, 23);
            this.cmbPostorPuja.TabIndex = 3;
            //
            // lblLblMonto
            //
            this.lblLblMonto.AutoSize = true;
            this.lblLblMonto.Location = new System.Drawing.Point(8, 95);
            this.lblLblMonto.Name = "lblLblMonto";
            this.lblLblMonto.Size = new System.Drawing.Size(56, 15);
            this.lblLblMonto.TabIndex = 4;
            this.lblLblMonto.Text = "Monto ($):";
            //
            // nudMonto
            //
            this.nudMonto.DecimalPlaces = 2;
            this.nudMonto.Location = new System.Drawing.Point(70, 92);
            this.nudMonto.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            this.nudMonto.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudMonto.Name = "nudMonto";
            this.nudMonto.Size = new System.Drawing.Size(180, 23);
            this.nudMonto.TabIndex = 5;
            this.nudMonto.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnRegistrarPuja
            //
            this.btnRegistrarPuja.Location = new System.Drawing.Point(262, 89);
            this.btnRegistrarPuja.Name = "btnRegistrarPuja";
            this.btnRegistrarPuja.Size = new System.Drawing.Size(178, 29);
            this.btnRegistrarPuja.TabIndex = 6;
            this.btnRegistrarPuja.Text = "Registrar Puja";
            this.btnRegistrarPuja.UseVisualStyleBackColor = true;
            this.btnRegistrarPuja.Click += new System.EventHandler(this.btnRegistrarPuja_Click);
            //
            // lblValidacionMonto
            //
            this.lblValidacionMonto.AutoSize = false;
            this.lblValidacionMonto.ForeColor = System.Drawing.Color.Red;
            this.lblValidacionMonto.Location = new System.Drawing.Point(8, 130);
            this.lblValidacionMonto.Name = "lblValidacionMonto";
            this.lblValidacionMonto.Size = new System.Drawing.Size(432, 55);
            this.lblValidacionMonto.TabIndex = 7;
            this.lblValidacionMonto.Text = "";
            //
            // grpSuscriptores  (Dock=Top — aparece debajo de grpPuja)
            //
            this.grpSuscriptores.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnDesuscribir,
                this.btnSuscribir,
                this.cmbPostorSuscribir,
                this.lstSuscriptores
            });
            this.grpSuscriptores.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSuscriptores.Enabled = false;
            this.grpSuscriptores.Location = new System.Drawing.Point(0, 200);
            this.grpSuscriptores.Name = "grpSuscriptores";
            this.grpSuscriptores.Size = new System.Drawing.Size(452, 185);
            this.grpSuscriptores.TabIndex = 1;
            this.grpSuscriptores.TabStop = false;
            this.grpSuscriptores.Text = "Suscriptores";
            //
            // lstSuscriptores
            //
            this.lstSuscriptores.FormattingEnabled = true;
            this.lstSuscriptores.ItemHeight = 15;
            this.lstSuscriptores.Location = new System.Drawing.Point(8, 22);
            this.lstSuscriptores.Name = "lstSuscriptores";
            this.lstSuscriptores.Size = new System.Drawing.Size(190, 154);
            this.lstSuscriptores.TabIndex = 0;
            //
            // cmbPostorSuscribir
            //
            this.cmbPostorSuscribir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPostorSuscribir.FormattingEnabled = true;
            this.cmbPostorSuscribir.Location = new System.Drawing.Point(210, 22);
            this.cmbPostorSuscribir.Name = "cmbPostorSuscribir";
            this.cmbPostorSuscribir.Size = new System.Drawing.Size(230, 23);
            this.cmbPostorSuscribir.TabIndex = 1;
            //
            // btnSuscribir
            //
            this.btnSuscribir.Location = new System.Drawing.Point(210, 55);
            this.btnSuscribir.Name = "btnSuscribir";
            this.btnSuscribir.Size = new System.Drawing.Size(230, 29);
            this.btnSuscribir.TabIndex = 2;
            this.btnSuscribir.Text = "Suscribir";
            this.btnSuscribir.UseVisualStyleBackColor = true;
            this.btnSuscribir.Click += new System.EventHandler(this.btnSuscribir_Click);
            //
            // btnDesuscribir
            //
            this.btnDesuscribir.Location = new System.Drawing.Point(210, 92);
            this.btnDesuscribir.Name = "btnDesuscribir";
            this.btnDesuscribir.Size = new System.Drawing.Size(230, 29);
            this.btnDesuscribir.TabIndex = 3;
            this.btnDesuscribir.Text = "Desuscribir seleccionado";
            this.btnDesuscribir.UseVisualStyleBackColor = true;
            this.btnDesuscribir.Click += new System.EventHandler(this.btnDesuscribir_Click);
            //
            // grpHistorial  (Dock=Fill — ocupa el resto del panel)
            //
            this.grpHistorial.Controls.Add(this.dgvHistorialPujas);
            this.grpHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistorial.Location = new System.Drawing.Point(0, 385);
            this.grpHistorial.Name = "grpHistorial";
            this.grpHistorial.Size = new System.Drawing.Size(452, 246);
            this.grpHistorial.TabIndex = 2;
            this.grpHistorial.TabStop = false;
            this.grpHistorial.Text = "Historial de Pujas";
            //
            // dgvHistorialPujas
            //
            this.dgvHistorialPujas.AllowUserToAddRows = false;
            this.dgvHistorialPujas.AllowUserToDeleteRows = false;
            this.dgvHistorialPujas.AutoGenerateColumns = false;
            this.dgvHistorialPujas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorialPujas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorialPujas.Location = new System.Drawing.Point(3, 19);
            this.dgvHistorialPujas.MultiSelect = false;
            this.dgvHistorialPujas.Name = "dgvHistorialPujas";
            this.dgvHistorialPujas.ReadOnly = true;
            this.dgvHistorialPujas.RowHeadersVisible = false;
            this.dgvHistorialPujas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorialPujas.Size = new System.Drawing.Size(446, 224);
            this.dgvHistorialPujas.TabIndex = 0;
            //
            // tabPage2
            //
            this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnAbrirSubasta,
                this.lblPrecioBaseAbrir,
                this.cmbUnidadAbrir,
                this.lblUnidadAbrir
            });
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1092, 637);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "▶ Abrir Subasta";
            this.tabPage2.UseVisualStyleBackColor = true;
            //
            // lblUnidadAbrir
            //
            this.lblUnidadAbrir.AutoSize = true;
            this.lblUnidadAbrir.Location = new System.Drawing.Point(20, 25);
            this.lblUnidadAbrir.Name = "lblUnidadAbrir";
            this.lblUnidadAbrir.Size = new System.Drawing.Size(110, 15);
            this.lblUnidadAbrir.TabIndex = 0;
            this.lblUnidadAbrir.Text = "Unidad disponible:";
            //
            // cmbUnidadAbrir
            //
            this.cmbUnidadAbrir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnidadAbrir.FormattingEnabled = true;
            this.cmbUnidadAbrir.Location = new System.Drawing.Point(20, 45);
            this.cmbUnidadAbrir.Name = "cmbUnidadAbrir";
            this.cmbUnidadAbrir.Size = new System.Drawing.Size(400, 23);
            this.cmbUnidadAbrir.TabIndex = 1;
            this.cmbUnidadAbrir.SelectedIndexChanged += new System.EventHandler(this.cmbUnidadAbrir_SelectedIndexChanged);
            //
            // lblPrecioBaseAbrir
            //
            this.lblPrecioBaseAbrir.AutoSize = true;
            this.lblPrecioBaseAbrir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPrecioBaseAbrir.Location = new System.Drawing.Point(20, 80);
            this.lblPrecioBaseAbrir.Name = "lblPrecioBaseAbrir";
            this.lblPrecioBaseAbrir.Size = new System.Drawing.Size(0, 19);
            this.lblPrecioBaseAbrir.TabIndex = 2;
            this.lblPrecioBaseAbrir.Text = "";
            //
            // btnAbrirSubasta
            //
            this.btnAbrirSubasta.Location = new System.Drawing.Point(20, 115);
            this.btnAbrirSubasta.Name = "btnAbrirSubasta";
            this.btnAbrirSubasta.Size = new System.Drawing.Size(200, 35);
            this.btnAbrirSubasta.TabIndex = 3;
            this.btnAbrirSubasta.Text = "Abrir Subasta";
            this.btnAbrirSubasta.UseVisualStyleBackColor = true;
            this.btnAbrirSubasta.Click += new System.EventHandler(this.btnAbrirSubasta_Click);
            //
            // tabPage3
            //
            this.tabPage3.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnCerrarSubasta,
                this.txtObservaciones,
                this.lblObsCerrar,
                this.grpResumen,
                this.cmbSubastaCerrar,
                this.lblSubastaCerrar
            });
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1092, 637);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "⏹ Cerrar Subasta";
            this.tabPage3.UseVisualStyleBackColor = true;
            //
            // lblSubastaCerrar
            //
            this.lblSubastaCerrar.AutoSize = true;
            this.lblSubastaCerrar.Location = new System.Drawing.Point(20, 25);
            this.lblSubastaCerrar.Name = "lblSubastaCerrar";
            this.lblSubastaCerrar.Size = new System.Drawing.Size(55, 15);
            this.lblSubastaCerrar.TabIndex = 0;
            this.lblSubastaCerrar.Text = "Subasta:";
            //
            // cmbSubastaCerrar
            //
            this.cmbSubastaCerrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubastaCerrar.FormattingEnabled = true;
            this.cmbSubastaCerrar.Location = new System.Drawing.Point(20, 45);
            this.cmbSubastaCerrar.Name = "cmbSubastaCerrar";
            this.cmbSubastaCerrar.Size = new System.Drawing.Size(400, 23);
            this.cmbSubastaCerrar.TabIndex = 1;
            this.cmbSubastaCerrar.SelectedIndexChanged += new System.EventHandler(this.cmbSubastaCerrar_SelectedIndexChanged);
            //
            // grpResumen
            //
            this.grpResumen.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblResumenTotalSuscriptores,
                this.lblRSusc,
                this.lblResumenTotalPujas,
                this.lblRPujas,
                this.lblResumenGanador,
                this.lblRGan,
                this.lblResumenPrecioVigente,
                this.lblRPrecio,
                this.lblResumenProducto,
                this.lblRProd
            });
            this.grpResumen.Location = new System.Drawing.Point(20, 80);
            this.grpResumen.Name = "grpResumen";
            this.grpResumen.Size = new System.Drawing.Size(500, 180);
            this.grpResumen.TabIndex = 2;
            this.grpResumen.TabStop = false;
            this.grpResumen.Text = "Resumen";
            //
            // lblRProd
            //
            this.lblRProd.AutoSize = true;
            this.lblRProd.Location = new System.Drawing.Point(10, 25);
            this.lblRProd.Name = "lblRProd";
            this.lblRProd.Size = new System.Drawing.Size(59, 15);
            this.lblRProd.TabIndex = 0;
            this.lblRProd.Text = "Producto:";
            //
            // lblResumenProducto
            //
            this.lblResumenProducto.AutoSize = true;
            this.lblResumenProducto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblResumenProducto.Location = new System.Drawing.Point(170, 25);
            this.lblResumenProducto.Name = "lblResumenProducto";
            this.lblResumenProducto.Size = new System.Drawing.Size(17, 15);
            this.lblResumenProducto.TabIndex = 1;
            this.lblResumenProducto.Text = "—";
            //
            // lblRPrecio
            //
            this.lblRPrecio.AutoSize = true;
            this.lblRPrecio.Location = new System.Drawing.Point(10, 55);
            this.lblRPrecio.Name = "lblRPrecio";
            this.lblRPrecio.Size = new System.Drawing.Size(85, 15);
            this.lblRPrecio.TabIndex = 2;
            this.lblRPrecio.Text = "Precio Vigente:";
            //
            // lblResumenPrecioVigente
            //
            this.lblResumenPrecioVigente.AutoSize = true;
            this.lblResumenPrecioVigente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblResumenPrecioVigente.Location = new System.Drawing.Point(170, 55);
            this.lblResumenPrecioVigente.Name = "lblResumenPrecioVigente";
            this.lblResumenPrecioVigente.Size = new System.Drawing.Size(17, 15);
            this.lblResumenPrecioVigente.TabIndex = 3;
            this.lblResumenPrecioVigente.Text = "—";
            //
            // lblRGan
            //
            this.lblRGan.AutoSize = true;
            this.lblRGan.Location = new System.Drawing.Point(10, 85);
            this.lblRGan.Name = "lblRGan";
            this.lblRGan.Size = new System.Drawing.Size(57, 15);
            this.lblRGan.TabIndex = 4;
            this.lblRGan.Text = "Ganador:";
            //
            // lblResumenGanador
            //
            this.lblResumenGanador.AutoSize = true;
            this.lblResumenGanador.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblResumenGanador.Location = new System.Drawing.Point(170, 85);
            this.lblResumenGanador.Name = "lblResumenGanador";
            this.lblResumenGanador.Size = new System.Drawing.Size(17, 15);
            this.lblResumenGanador.TabIndex = 5;
            this.lblResumenGanador.Text = "—";
            //
            // lblRPujas
            //
            this.lblRPujas.AutoSize = true;
            this.lblRPujas.Location = new System.Drawing.Point(10, 115);
            this.lblRPujas.Name = "lblRPujas";
            this.lblRPujas.Size = new System.Drawing.Size(72, 15);
            this.lblRPujas.TabIndex = 6;
            this.lblRPujas.Text = "Total Pujas:";
            //
            // lblResumenTotalPujas
            //
            this.lblResumenTotalPujas.AutoSize = true;
            this.lblResumenTotalPujas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblResumenTotalPujas.Location = new System.Drawing.Point(170, 115);
            this.lblResumenTotalPujas.Name = "lblResumenTotalPujas";
            this.lblResumenTotalPujas.Size = new System.Drawing.Size(17, 15);
            this.lblResumenTotalPujas.TabIndex = 7;
            this.lblResumenTotalPujas.Text = "—";
            //
            // lblRSusc
            //
            this.lblRSusc.AutoSize = true;
            this.lblRSusc.Location = new System.Drawing.Point(10, 145);
            this.lblRSusc.Name = "lblRSusc";
            this.lblRSusc.Size = new System.Drawing.Size(120, 15);
            this.lblRSusc.TabIndex = 8;
            this.lblRSusc.Text = "Total Suscriptores:";
            //
            // lblResumenTotalSuscriptores
            //
            this.lblResumenTotalSuscriptores.AutoSize = true;
            this.lblResumenTotalSuscriptores.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblResumenTotalSuscriptores.Location = new System.Drawing.Point(170, 145);
            this.lblResumenTotalSuscriptores.Name = "lblResumenTotalSuscriptores";
            this.lblResumenTotalSuscriptores.Size = new System.Drawing.Size(17, 15);
            this.lblResumenTotalSuscriptores.TabIndex = 9;
            this.lblResumenTotalSuscriptores.Text = "—";
            //
            // lblObsCerrar
            //
            this.lblObsCerrar.AutoSize = true;
            this.lblObsCerrar.Location = new System.Drawing.Point(20, 275);
            this.lblObsCerrar.Name = "lblObsCerrar";
            this.lblObsCerrar.Size = new System.Drawing.Size(155, 15);
            this.lblObsCerrar.TabIndex = 3;
            this.lblObsCerrar.Text = "Observaciones (opcional):";
            //
            // txtObservaciones
            //
            this.txtObservaciones.Location = new System.Drawing.Point(20, 295);
            this.txtObservaciones.MaxLength = 500;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(500, 70);
            this.txtObservaciones.TabIndex = 4;
            //
            // btnCerrarSubasta
            //
            this.btnCerrarSubasta.Location = new System.Drawing.Point(20, 380);
            this.btnCerrarSubasta.Name = "btnCerrarSubasta";
            this.btnCerrarSubasta.Size = new System.Drawing.Size(200, 35);
            this.btnCerrarSubasta.TabIndex = 5;
            this.btnCerrarSubasta.Text = "Cerrar Subasta";
            this.btnCerrarSubasta.UseVisualStyleBackColor = true;
            this.btnCerrarSubasta.Click += new System.EventHandler(this.btnCerrarSubasta_Click);
            //
            // FrmGestionSubastas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 670);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(960, 620);
            this.Name = "FrmGestionSubastas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de Subastas";

            this.grpResumen.ResumeLayout(false);
            this.grpResumen.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialPujas)).EndInit();
            this.grpHistorial.ResumeLayout(false);
            this.grpSuscriptores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).EndInit();
            this.grpPuja.ResumeLayout(false);
            this.grpPuja.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubastasActivas)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Controles ─────────────────────────────────────────────────────────────
        private System.Windows.Forms.TabControl             tabControl;
        private System.Windows.Forms.TabPage                tabPage1;
        private System.Windows.Forms.TabPage                tabPage2;
        private System.Windows.Forms.TabPage                tabPage3;
        private System.Windows.Forms.SplitContainer         splitContainer1;
        private System.Windows.Forms.DataGridView           dgvSubastasActivas;
        private System.Windows.Forms.GroupBox               grpPuja;
        private System.Windows.Forms.Label                  lblLblPrecioVigente;
        private System.Windows.Forms.Label                  lblPrecioVigenteActual;
        private System.Windows.Forms.Label                  lblLblPostorPuja;
        private System.Windows.Forms.ComboBox               cmbPostorPuja;
        private System.Windows.Forms.Label                  lblLblMonto;
        private System.Windows.Forms.NumericUpDown          nudMonto;
        private System.Windows.Forms.Button                 btnRegistrarPuja;
        private System.Windows.Forms.Label                  lblValidacionMonto;
        private System.Windows.Forms.GroupBox               grpSuscriptores;
        private System.Windows.Forms.ListBox                lstSuscriptores;
        private System.Windows.Forms.ComboBox               cmbPostorSuscribir;
        private System.Windows.Forms.Button                 btnSuscribir;
        private System.Windows.Forms.Button                 btnDesuscribir;
        private System.Windows.Forms.GroupBox               grpHistorial;
        private System.Windows.Forms.DataGridView           dgvHistorialPujas;
        private System.Windows.Forms.Label                  lblUnidadAbrir;
        private System.Windows.Forms.ComboBox               cmbUnidadAbrir;
        private System.Windows.Forms.Label                  lblPrecioBaseAbrir;
        private System.Windows.Forms.Button                 btnAbrirSubasta;
        private System.Windows.Forms.Label                  lblSubastaCerrar;
        private System.Windows.Forms.ComboBox               cmbSubastaCerrar;
        private System.Windows.Forms.GroupBox               grpResumen;
        private System.Windows.Forms.Label                  lblRProd;
        private System.Windows.Forms.Label                  lblResumenProducto;
        private System.Windows.Forms.Label                  lblRPrecio;
        private System.Windows.Forms.Label                  lblResumenPrecioVigente;
        private System.Windows.Forms.Label                  lblRGan;
        private System.Windows.Forms.Label                  lblResumenGanador;
        private System.Windows.Forms.Label                  lblRPujas;
        private System.Windows.Forms.Label                  lblResumenTotalPujas;
        private System.Windows.Forms.Label                  lblRSusc;
        private System.Windows.Forms.Label                  lblResumenTotalSuscriptores;
        private System.Windows.Forms.Label                  lblObsCerrar;
        private System.Windows.Forms.TextBox                txtObservaciones;
        private System.Windows.Forms.Button                 btnCerrarSubasta;
    }
}
