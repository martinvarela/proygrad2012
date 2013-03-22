namespace Proyecto
{
    partial class ventanaMuestreo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ventanaMuestreo));
            this.txtArchivoZF = new System.Windows.Forms.TextBox();
            this.botonAbrir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblOpenFileZF = new System.Windows.Forms.Label();
            this.ptoVerdeZF = new System.Windows.Forms.PictureBox();
            this.ptoVerdeDestino = new System.Windows.Forms.PictureBox();
            this.lblMuestreo = new System.Windows.Forms.Label();
            this.botonAbrirMuestreo = new System.Windows.Forms.Button();
            this.txtMuestreo = new System.Windows.Forms.TextBox();
            this.btnAyuda = new System.Windows.Forms.Button();
            this.panelAyuda = new System.Windows.Forms.Panel();
            this.lblDescripcionRed7 = new System.Windows.Forms.Label();
            this.lblDescripcionRed6 = new System.Windows.Forms.Label();
            this.lblDescripcionRed4 = new System.Windows.Forms.Label();
            this.lblDescripcionRed3 = new System.Windows.Forms.Label();
            this.lblDescripcionRed5 = new System.Windows.Forms.Label();
            this.lblDescripcionRed2 = new System.Windows.Forms.Label();
            this.lblDescripcionRed1 = new System.Windows.Forms.Label();
            this.lblTituloRed = new System.Windows.Forms.Label();
            this.labelDescripcionMuestreo = new System.Windows.Forms.Label();
            this.labelTituloMuestreo = new System.Windows.Forms.Label();
            this.labelDescripcionZonificacion = new System.Windows.Forms.Label();
            this.labelTituloZonificacion = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.chkLstVariables = new System.Windows.Forms.CheckedListBox();
            this.lblVariables = new System.Windows.Forms.Label();
            this.ptoVerdeVariables = new System.Windows.Forms.PictureBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.txtHorizontal = new System.Windows.Forms.TextBox();
            this.txtVertical = new System.Windows.Forms.TextBox();
            this.cboTipoRed = new System.Windows.Forms.ComboBox();
            this.lblTipoRed = new System.Windows.Forms.Label();
            this.lblHorizontal = new System.Windows.Forms.Label();
            this.lblVertical = new System.Windows.Forms.Label();
            this.ptoVerdeVertical = new System.Windows.Forms.PictureBox();
            this.ptoVerdeHorizontal = new System.Windows.Forms.PictureBox();
            this.lblProgressBar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeZF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeDestino)).BeginInit();
            this.panelAyuda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeHorizontal)).BeginInit();
            this.SuspendLayout();
            // 
            // txtArchivoZF
            // 
            this.txtArchivoZF.Enabled = false;
            this.txtArchivoZF.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArchivoZF.Location = new System.Drawing.Point(31, 34);
            this.txtArchivoZF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtArchivoZF.Name = "txtArchivoZF";
            this.txtArchivoZF.Size = new System.Drawing.Size(377, 21);
            this.txtArchivoZF.TabIndex = 0;
            this.txtArchivoZF.TextChanged += new System.EventHandler(this.txtArchivoZF_TextChanged);
            this.txtArchivoZF.GotFocus += new System.EventHandler(this.txtArchivoZF_GotFocus);
            // 
            // botonAbrir
            // 
            this.botonAbrir.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonAbrir.Image = ((System.Drawing.Image)(resources.GetObject("botonAbrir.Image")));
            this.botonAbrir.Location = new System.Drawing.Point(420, 32);
            this.botonAbrir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.botonAbrir.Name = "botonAbrir";
            this.botonAbrir.Size = new System.Drawing.Size(31, 28);
            this.botonAbrir.TabIndex = 1;
            this.botonAbrir.UseVisualStyleBackColor = true;
            this.botonAbrir.Click += new System.EventHandler(this.botonAbrir_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(31, 479);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(110, 28);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.Zonificacion_Click);
            // 
            // lblOpenFileZF
            // 
            this.lblOpenFileZF.AutoEllipsis = true;
            this.lblOpenFileZF.AutoSize = true;
            this.lblOpenFileZF.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenFileZF.Location = new System.Drawing.Point(28, 13);
            this.lblOpenFileZF.Name = "lblOpenFileZF";
            this.lblOpenFileZF.Size = new System.Drawing.Size(130, 15);
            this.lblOpenFileZF.TabIndex = 3;
            this.lblOpenFileZF.Text = "Archivo de zonificación";
            // 
            // ptoVerdeZF
            // 
            this.ptoVerdeZF.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeZF.Image")));
            this.ptoVerdeZF.Location = new System.Drawing.Point(13, 34);
            this.ptoVerdeZF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeZF.Name = "ptoVerdeZF";
            this.ptoVerdeZF.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeZF.TabIndex = 4;
            this.ptoVerdeZF.TabStop = false;
            // 
            // ptoVerdeDestino
            // 
            this.ptoVerdeDestino.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeDestino.Image")));
            this.ptoVerdeDestino.Location = new System.Drawing.Point(13, 319);
            this.ptoVerdeDestino.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeDestino.Name = "ptoVerdeDestino";
            this.ptoVerdeDestino.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeDestino.TabIndex = 8;
            this.ptoVerdeDestino.TabStop = false;
            // 
            // lblMuestreo
            // 
            this.lblMuestreo.AutoSize = true;
            this.lblMuestreo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMuestreo.Location = new System.Drawing.Point(28, 298);
            this.lblMuestreo.Name = "lblMuestreo";
            this.lblMuestreo.Size = new System.Drawing.Size(110, 15);
            this.lblMuestreo.TabIndex = 7;
            this.lblMuestreo.Text = "Capa de muestreo";
            // 
            // botonAbrirMuestreo
            // 
            this.botonAbrirMuestreo.Image = ((System.Drawing.Image)(resources.GetObject("botonAbrirMuestreo.Image")));
            this.botonAbrirMuestreo.Location = new System.Drawing.Point(420, 315);
            this.botonAbrirMuestreo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.botonAbrirMuestreo.Name = "botonAbrirMuestreo";
            this.botonAbrirMuestreo.Size = new System.Drawing.Size(31, 28);
            this.botonAbrirMuestreo.TabIndex = 6;
            this.botonAbrirMuestreo.UseVisualStyleBackColor = true;
            this.botonAbrirMuestreo.Click += new System.EventHandler(this.botonAbrirMuestreo_Click);
            // 
            // txtMuestreo
            // 
            this.txtMuestreo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMuestreo.Location = new System.Drawing.Point(31, 319);
            this.txtMuestreo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMuestreo.Name = "txtMuestreo";
            this.txtMuestreo.Size = new System.Drawing.Size(377, 21);
            this.txtMuestreo.TabIndex = 5;
            this.txtMuestreo.TextChanged += new System.EventHandler(this.txtMuestreo_TextChanged);
            this.txtMuestreo.GotFocus += new System.EventHandler(this.txtMuestreo_GotFocus);
            // 
            // btnAyuda
            // 
            this.btnAyuda.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAyuda.Location = new System.Drawing.Point(294, 479);
            this.btnAyuda.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAyuda.Name = "btnAyuda";
            this.btnAyuda.Size = new System.Drawing.Size(157, 28);
            this.btnAyuda.TabIndex = 9;
            this.btnAyuda.Text = "Mostrar ayuda >>";
            this.btnAyuda.UseVisualStyleBackColor = true;
            this.btnAyuda.Click += new System.EventHandler(this.botonAyuda_Click);
            // 
            // panelAyuda
            // 
            this.panelAyuda.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelAyuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAyuda.Controls.Add(this.lblDescripcionRed7);
            this.panelAyuda.Controls.Add(this.lblDescripcionRed6);
            this.panelAyuda.Controls.Add(this.lblDescripcionRed4);
            this.panelAyuda.Controls.Add(this.lblDescripcionRed3);
            this.panelAyuda.Controls.Add(this.lblDescripcionRed5);
            this.panelAyuda.Controls.Add(this.lblDescripcionRed2);
            this.panelAyuda.Controls.Add(this.lblDescripcionRed1);
            this.panelAyuda.Controls.Add(this.lblTituloRed);
            this.panelAyuda.Controls.Add(this.labelDescripcionMuestreo);
            this.panelAyuda.Controls.Add(this.labelTituloMuestreo);
            this.panelAyuda.Controls.Add(this.labelDescripcionZonificacion);
            this.panelAyuda.Controls.Add(this.labelTituloZonificacion);
            this.panelAyuda.Location = new System.Drawing.Point(475, 27);
            this.panelAyuda.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelAyuda.Name = "panelAyuda";
            this.panelAyuda.Size = new System.Drawing.Size(442, 478);
            this.panelAyuda.TabIndex = 10;
            // 
            // lblDescripcionRed7
            // 
            this.lblDescripcionRed7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionRed7.Location = new System.Drawing.Point(36, 276);
            this.lblDescripcionRed7.Name = "lblDescripcionRed7";
            this.lblDescripcionRed7.Size = new System.Drawing.Size(384, 22);
            this.lblDescripcionRed7.TabIndex = 11;
            this.lblDescripcionRed7.Text = "Ancho - Ancho de las celdas con las que se construira la red.";
            // 
            // lblDescripcionRed6
            // 
            this.lblDescripcionRed6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionRed6.Location = new System.Drawing.Point(36, 258);
            this.lblDescripcionRed6.Name = "lblDescripcionRed6";
            this.lblDescripcionRed6.Size = new System.Drawing.Size(384, 16);
            this.lblDescripcionRed6.TabIndex = 10;
            this.lblDescripcionRed6.Text = "Alto - Alto de las celdas con las que se construira la red.";
            // 
            // lblDescripcionRed4
            // 
            this.lblDescripcionRed4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionRed4.Location = new System.Drawing.Point(36, 157);
            this.lblDescripcionRed4.Name = "lblDescripcionRed4";
            this.lblDescripcionRed4.Size = new System.Drawing.Size(409, 18);
            this.lblDescripcionRed4.TabIndex = 9;
            this.lblDescripcionRed4.Text = "Columnas - Cantidad de columnas con las que se construira la red.";
            // 
            // lblDescripcionRed3
            // 
            this.lblDescripcionRed3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionRed3.Location = new System.Drawing.Point(36, 139);
            this.lblDescripcionRed3.Name = "lblDescripcionRed3";
            this.lblDescripcionRed3.Size = new System.Drawing.Size(384, 16);
            this.lblDescripcionRed3.TabIndex = 8;
            this.lblDescripcionRed3.Text = "Filas - Cantidad de filas con las que se construira la red.";
            // 
            // lblDescripcionRed5
            // 
            this.lblDescripcionRed5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionRed5.Location = new System.Drawing.Point(19, 196);
            this.lblDescripcionRed5.Name = "lblDescripcionRed5";
            this.lblDescripcionRed5.Size = new System.Drawing.Size(384, 56);
            this.lblDescripcionRed5.TabIndex = 7;
            this.lblDescripcionRed5.Text = "Tamaño de celdas - Determina que se hara la generalización de puntos mediante una" +
                " red construida en base al ancho y alto de cada celda.";
            // 
            // lblDescripcionRed2
            // 
            this.lblDescripcionRed2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionRed2.Location = new System.Drawing.Point(19, 80);
            this.lblDescripcionRed2.Name = "lblDescripcionRed2";
            this.lblDescripcionRed2.Size = new System.Drawing.Size(384, 56);
            this.lblDescripcionRed2.TabIndex = 6;
            this.lblDescripcionRed2.Text = "Cantidad de celdas - Determina que se hara la generalización de puntos mediante u" +
                "na red construida en base a cantidad de filas y columnas.";
            // 
            // lblDescripcionRed1
            // 
            this.lblDescripcionRed1.AutoSize = true;
            this.lblDescripcionRed1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionRed1.Location = new System.Drawing.Point(19, 57);
            this.lblDescripcionRed1.Name = "lblDescripcionRed1";
            this.lblDescripcionRed1.Size = new System.Drawing.Size(81, 16);
            this.lblDescripcionRed1.TabIndex = 5;
            this.lblDescripcionRed1.Text = "Tipo de red";
            // 
            // lblTituloRed
            // 
            this.lblTituloRed.AutoSize = true;
            this.lblTituloRed.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloRed.Location = new System.Drawing.Point(18, 20);
            this.lblTituloRed.Name = "lblTituloRed";
            this.lblTituloRed.Size = new System.Drawing.Size(204, 19);
            this.lblTituloRed.TabIndex = 4;
            this.lblTituloRed.Text = "Generalización de puntos";
            // 
            // labelDescripcionMuestreo
            // 
            this.labelDescripcionMuestreo.AutoSize = true;
            this.labelDescripcionMuestreo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescripcionMuestreo.Location = new System.Drawing.Point(19, 86);
            this.labelDescripcionMuestreo.Name = "labelDescripcionMuestreo";
            this.labelDescripcionMuestreo.Size = new System.Drawing.Size(239, 16);
            this.labelDescripcionMuestreo.TabIndex = 3;
            this.labelDescripcionMuestreo.Text = "Ruta de destino de la capa de muestreo";
            // 
            // labelTituloMuestreo
            // 
            this.labelTituloMuestreo.AutoSize = true;
            this.labelTituloMuestreo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTituloMuestreo.Location = new System.Drawing.Point(18, 20);
            this.labelTituloMuestreo.Name = "labelTituloMuestreo";
            this.labelTituloMuestreo.Size = new System.Drawing.Size(148, 19);
            this.labelTituloMuestreo.TabIndex = 2;
            this.labelTituloMuestreo.Text = "Capa de muestreo";
            // 
            // labelDescripcionZonificacion
            // 
            this.labelDescripcionZonificacion.AutoSize = true;
            this.labelDescripcionZonificacion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescripcionZonificacion.Location = new System.Drawing.Point(19, 86);
            this.labelDescripcionZonificacion.Name = "labelDescripcionZonificacion";
            this.labelDescripcionZonificacion.Size = new System.Drawing.Size(301, 16);
            this.labelDescripcionZonificacion.TabIndex = 1;
            this.labelDescripcionZonificacion.Text = "Ingrese el archivo correspondiente a la zonificación";
            // 
            // labelTituloZonificacion
            // 
            this.labelTituloZonificacion.AutoSize = true;
            this.labelTituloZonificacion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTituloZonificacion.Location = new System.Drawing.Point(14, 20);
            this.labelTituloZonificacion.Name = "labelTituloZonificacion";
            this.labelTituloZonificacion.Size = new System.Drawing.Size(188, 19);
            this.labelTituloZonificacion.TabIndex = 0;
            this.labelTituloZonificacion.Text = "Archivo de zonificación";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(162, 479);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 28);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // chkLstVariables
            // 
            this.chkLstVariables.CheckOnClick = true;
            this.chkLstVariables.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLstVariables.FormattingEnabled = true;
            this.chkLstVariables.Location = new System.Drawing.Point(31, 85);
            this.chkLstVariables.Name = "chkLstVariables";
            this.chkLstVariables.Size = new System.Drawing.Size(377, 84);
            this.chkLstVariables.TabIndex = 12;
            this.chkLstVariables.SelectedIndexChanged += new System.EventHandler(this.chkLstVariables_SelectedIndexChanged);
            // 
            // lblVariables
            // 
            this.lblVariables.AutoSize = true;
            this.lblVariables.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVariables.Location = new System.Drawing.Point(28, 65);
            this.lblVariables.Name = "lblVariables";
            this.lblVariables.Size = new System.Drawing.Size(58, 15);
            this.lblVariables.TabIndex = 13;
            this.lblVariables.Text = "Variables";
            // 
            // ptoVerdeVariables
            // 
            this.ptoVerdeVariables.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeVariables.Image")));
            this.ptoVerdeVariables.Location = new System.Drawing.Point(12, 85);
            this.ptoVerdeVariables.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeVariables.Name = "ptoVerdeVariables";
            this.ptoVerdeVariables.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeVariables.TabIndex = 14;
            this.ptoVerdeVariables.TabStop = false;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(31, 431);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(420, 23);
            this.pBar.TabIndex = 15;
            this.pBar.Visible = false;
            // 
            // txtHorizontal
            // 
            this.txtHorizontal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHorizontal.Location = new System.Drawing.Point(234, 247);
            this.txtHorizontal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHorizontal.Name = "txtHorizontal";
            this.txtHorizontal.Size = new System.Drawing.Size(174, 21);
            this.txtHorizontal.TabIndex = 17;
            this.txtHorizontal.GotFocus += new System.EventHandler(this.txtHorizontal_GotFucus);
            this.txtHorizontal.LostFocus += new System.EventHandler(this.txtHorizontal_LostFocus);
            // 
            // txtVertical
            // 
            this.txtVertical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVertical.Location = new System.Drawing.Point(31, 247);
            this.txtVertical.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVertical.Name = "txtVertical";
            this.txtVertical.Size = new System.Drawing.Size(174, 21);
            this.txtVertical.TabIndex = 19;
            this.txtVertical.GotFocus += new System.EventHandler(this.txtVertical_GotFucus);
            this.txtVertical.LostFocus += new System.EventHandler(this.txtVertical_LostFocus);
            // 
            // cboTipoRed
            // 
            this.cboTipoRed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoRed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoRed.FormattingEnabled = true;
            this.cboTipoRed.Items.AddRange(new object[] {
            "Cantidad de celdas",
            "Tamaño de celdas"});
            this.cboTipoRed.Location = new System.Drawing.Point(31, 196);
            this.cboTipoRed.Name = "cboTipoRed";
            this.cboTipoRed.Size = new System.Drawing.Size(174, 23);
            this.cboTipoRed.TabIndex = 21;
            this.cboTipoRed.SelectedIndexChanged += new System.EventHandler(this.cboTipoRed_SelectedIndexChanged);
            this.cboTipoRed.GotFocus += new System.EventHandler(this.cboTipoRed_GotFucus);
            // 
            // lblTipoRed
            // 
            this.lblTipoRed.AutoSize = true;
            this.lblTipoRed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoRed.Location = new System.Drawing.Point(28, 176);
            this.lblTipoRed.Name = "lblTipoRed";
            this.lblTipoRed.Size = new System.Drawing.Size(69, 15);
            this.lblTipoRed.TabIndex = 22;
            this.lblTipoRed.Text = "Tipo de red";
            // 
            // lblHorizontal
            // 
            this.lblHorizontal.AutoSize = true;
            this.lblHorizontal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHorizontal.Location = new System.Drawing.Point(231, 227);
            this.lblHorizontal.Name = "lblHorizontal";
            this.lblHorizontal.Size = new System.Drawing.Size(65, 15);
            this.lblHorizontal.TabIndex = 23;
            this.lblHorizontal.Text = "Columnas";
            // 
            // lblVertical
            // 
            this.lblVertical.AutoSize = true;
            this.lblVertical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVertical.Location = new System.Drawing.Point(28, 227);
            this.lblVertical.Name = "lblVertical";
            this.lblVertical.Size = new System.Drawing.Size(34, 15);
            this.lblVertical.TabIndex = 24;
            this.lblVertical.Text = "Filas";
            // 
            // ptoVerdeVertical
            // 
            this.ptoVerdeVertical.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeVertical.Image")));
            this.ptoVerdeVertical.Location = new System.Drawing.Point(13, 247);
            this.ptoVerdeVertical.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeVertical.Name = "ptoVerdeVertical";
            this.ptoVerdeVertical.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeVertical.TabIndex = 25;
            this.ptoVerdeVertical.TabStop = false;
            // 
            // ptoVerdeHorizontal
            // 
            this.ptoVerdeHorizontal.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeHorizontal.Image")));
            this.ptoVerdeHorizontal.Location = new System.Drawing.Point(213, 249);
            this.ptoVerdeHorizontal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeHorizontal.Name = "ptoVerdeHorizontal";
            this.ptoVerdeHorizontal.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeHorizontal.TabIndex = 26;
            this.ptoVerdeHorizontal.TabStop = false;
            // 
            // lblProgressBar
            // 
            this.lblProgressBar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressBar.Location = new System.Drawing.Point(31, 408);
            this.lblProgressBar.Name = "lblProgressBar";
            this.lblProgressBar.Size = new System.Drawing.Size(420, 20);
            this.lblProgressBar.TabIndex = 27;
            // 
            // ventanaMuestreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 518);
            this.Controls.Add(this.lblProgressBar);
            this.Controls.Add(this.ptoVerdeHorizontal);
            this.Controls.Add(this.ptoVerdeVertical);
            this.Controls.Add(this.lblVertical);
            this.Controls.Add(this.lblHorizontal);
            this.Controls.Add(this.lblTipoRed);
            this.Controls.Add(this.cboTipoRed);
            this.Controls.Add(this.txtVertical);
            this.Controls.Add(this.txtHorizontal);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.ptoVerdeVariables);
            this.Controls.Add(this.lblVariables);
            this.Controls.Add(this.chkLstVariables);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.panelAyuda);
            this.Controls.Add(this.btnAyuda);
            this.Controls.Add(this.ptoVerdeDestino);
            this.Controls.Add(this.lblMuestreo);
            this.Controls.Add(this.botonAbrirMuestreo);
            this.Controls.Add(this.txtMuestreo);
            this.Controls.Add(this.ptoVerdeZF);
            this.Controls.Add(this.lblOpenFileZF);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.botonAbrir);
            this.Controls.Add(this.txtArchivoZF);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ventanaMuestreo";
            this.Text = "Optimización de muestreo";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeZF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeDestino)).EndInit();
            this.panelAyuda.ResumeLayout(false);
            this.panelAyuda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeVariables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeHorizontal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtArchivoZF;
        private System.Windows.Forms.Button botonAbrir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblOpenFileZF;
        private System.Windows.Forms.PictureBox ptoVerdeZF;
        private System.Windows.Forms.PictureBox ptoVerdeDestino;
        private System.Windows.Forms.Label lblMuestreo;
        private System.Windows.Forms.Button botonAbrirMuestreo;
        private System.Windows.Forms.TextBox txtMuestreo;
        private System.Windows.Forms.Button btnAyuda;
        private System.Windows.Forms.Panel panelAyuda;
        private System.Windows.Forms.Label labelDescripcionZonificacion;
        private System.Windows.Forms.Label labelTituloZonificacion;
        private System.Windows.Forms.Label labelDescripcionMuestreo;
        private System.Windows.Forms.Label labelTituloMuestreo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.CheckedListBox chkLstVariables;
        private System.Windows.Forms.Label lblVariables;
        private System.Windows.Forms.PictureBox ptoVerdeVariables;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.TextBox txtHorizontal;
        private System.Windows.Forms.TextBox txtVertical;
        private System.Windows.Forms.ComboBox cboTipoRed;
        private System.Windows.Forms.Label lblTipoRed;
        private System.Windows.Forms.Label lblHorizontal;
        private System.Windows.Forms.Label lblVertical;
        private System.Windows.Forms.PictureBox ptoVerdeVertical;
        private System.Windows.Forms.PictureBox ptoVerdeHorizontal;
        public System.Windows.Forms.Label lblDescripcionRed7;
        public System.Windows.Forms.Label lblDescripcionRed6;
        public System.Windows.Forms.Label lblDescripcionRed4;
        public System.Windows.Forms.Label lblDescripcionRed3;
        public System.Windows.Forms.Label lblDescripcionRed5;
        public System.Windows.Forms.Label lblDescripcionRed2;
        private System.Windows.Forms.Label lblDescripcionRed1;
        private System.Windows.Forms.Label lblTituloRed;
        private System.Windows.Forms.Label lblProgressBar;

    }
}