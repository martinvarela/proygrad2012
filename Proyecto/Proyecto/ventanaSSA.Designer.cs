using System.Windows.Forms;
namespace Proyecto
{
    partial class ventanaSSA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ventanaSSA));
            this.lblCapaMuestreo = new System.Windows.Forms.Label();
            this.cboCapaMuestreo = new System.Windows.Forms.ComboBox();
            this.lblMetodoEstimacion = new System.Windows.Forms.Label();
            this.cboMetodoEstimacion = new System.Windows.Forms.ComboBox();
            this.lblRango = new System.Windows.Forms.Label();
            this.txtRango = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.TextBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAyuda = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnParametros = new System.Windows.Forms.Button();
            this.ptoVerdeCapa = new System.Windows.Forms.PictureBox();
            this.ptoVerdeError = new System.Windows.Forms.PictureBox();
            this.panelAyuda = new System.Windows.Forms.Panel();
            this.lblDescripcionCarpeta = new System.Windows.Forms.Label();
            this.lblTituloCarpeta = new System.Windows.Forms.Label();
            this.lblDescripcionError = new System.Windows.Forms.Label();
            this.lblTituloError = new System.Windows.Forms.Label();
            this.lblDescripcionRango = new System.Windows.Forms.Label();
            this.lblTituloRango = new System.Windows.Forms.Label();
            this.lblDescripcionMetodo = new System.Windows.Forms.Label();
            this.lblTituloMetodo = new System.Windows.Forms.Label();
            this.lblDescripcionCapa = new System.Windows.Forms.Label();
            this.lblTituloCapa = new System.Windows.Forms.Label();
            this.txtCarpeta = new System.Windows.Forms.TextBox();
            this.lblRutaDestino = new System.Windows.Forms.Label();
            this.btnCarpeta = new System.Windows.Forms.Button();
            this.ptoVerdeCarpeta = new System.Windows.Forms.PictureBox();
            this.txtExpIDW = new System.Windows.Forms.TextBox();
            this.lblExpIDW = new System.Windows.Forms.Label();
            this.ptoVerdeExp = new System.Windows.Forms.PictureBox();
            this.ptoVerdeMuestras = new System.Windows.Forms.PictureBox();
            this.lblMuestras = new System.Windows.Forms.Label();
            this.txtCantMuestras = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeCapa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeError)).BeginInit();
            this.panelAyuda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeCarpeta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeExp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeMuestras)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCapaMuestreo
            // 
            this.lblCapaMuestreo.AutoSize = true;
            this.lblCapaMuestreo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCapaMuestreo.Location = new System.Drawing.Point(29, 12);
            this.lblCapaMuestreo.Name = "lblCapaMuestreo";
            this.lblCapaMuestreo.Size = new System.Drawing.Size(110, 15);
            this.lblCapaMuestreo.TabIndex = 24;
            this.lblCapaMuestreo.Text = "Capa de muestreo";
            // 
            // cboCapaMuestreo
            // 
            this.cboCapaMuestreo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCapaMuestreo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCapaMuestreo.FormattingEnabled = true;
            this.cboCapaMuestreo.Location = new System.Drawing.Point(32, 31);
            this.cboCapaMuestreo.Name = "cboCapaMuestreo";
            this.cboCapaMuestreo.Size = new System.Drawing.Size(371, 23);
            this.cboCapaMuestreo.TabIndex = 1;
            this.cboCapaMuestreo.SelectedIndexChanged += new System.EventHandler(this.cboCapaMuestreo_SelectedIndexChanged);
            this.cboCapaMuestreo.GotFocus += new System.EventHandler(this.cboCapaMuestreo_GotFocus);
            // 
            // lblMetodoEstimacion
            // 
            this.lblMetodoEstimacion.AutoSize = true;
            this.lblMetodoEstimacion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetodoEstimacion.Location = new System.Drawing.Point(31, 69);
            this.lblMetodoEstimacion.Name = "lblMetodoEstimacion";
            this.lblMetodoEstimacion.Size = new System.Drawing.Size(128, 15);
            this.lblMetodoEstimacion.TabIndex = 26;
            this.lblMetodoEstimacion.Text = "Método de estimación";
            // 
            // cboMetodoEstimacion
            // 
            this.cboMetodoEstimacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMetodoEstimacion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMetodoEstimacion.FormattingEnabled = true;
            this.cboMetodoEstimacion.Location = new System.Drawing.Point(32, 88);
            this.cboMetodoEstimacion.Name = "cboMetodoEstimacion";
            this.cboMetodoEstimacion.Size = new System.Drawing.Size(160, 23);
            this.cboMetodoEstimacion.TabIndex = 2;
            this.cboMetodoEstimacion.GotFocus += new System.EventHandler(this.cboMetodoEstimacion_GotFocus);
            // 
            // lblRango
            // 
            this.lblRango.AutoSize = true;
            this.lblRango.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRango.Location = new System.Drawing.Point(29, 190);
            this.lblRango.Name = "lblRango";
            this.lblRango.Size = new System.Drawing.Size(66, 15);
            this.lblRango.TabIndex = 28;
            this.lblRango.Text = "Rango (m)";
            // 
            // txtRango
            // 
            this.txtRango.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRango.Location = new System.Drawing.Point(32, 209);
            this.txtRango.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRango.Name = "txtRango";
            this.txtRango.Size = new System.Drawing.Size(94, 21);
            this.txtRango.TabIndex = 5;
            this.txtRango.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRango.GotFocus += new System.EventHandler(this.txtRango_GotFocus);
            this.txtRango.LostFocus += new System.EventHandler(this.txtRango_LostFocus);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.Location = new System.Drawing.Point(291, 190);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(139, 15);
            this.lblError.TabIndex = 30;
            this.lblError.Text = "Root Mean Square Error";
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtError.Location = new System.Drawing.Point(294, 209);
            this.txtError.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(82, 21);
            this.txtError.TabIndex = 6;
            this.txtError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtError.GotFocus += new System.EventHandler(this.txtError_GotFocus);
            this.txtError.LostFocus += new System.EventHandler(this.txtError_LostFocus);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(32, 258);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(420, 23);
            this.pBar.TabIndex = 34;
            this.pBar.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(134, 287);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 28);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAyuda
            // 
            this.btnAyuda.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAyuda.Location = new System.Drawing.Point(339, 287);
            this.btnAyuda.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAyuda.Name = "btnAyuda";
            this.btnAyuda.Size = new System.Drawing.Size(112, 28);
            this.btnAyuda.TabIndex = 10;
            this.btnAyuda.Text = "Mostrar ayuda >>";
            this.btnAyuda.UseVisualStyleBackColor = true;
            this.btnAyuda.Click += new System.EventHandler(this.btnAyuda_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(32, 287);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(98, 28);
            this.btnAceptar.TabIndex = 7;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnParametros
            // 
            this.btnParametros.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParametros.Location = new System.Drawing.Point(236, 287);
            this.btnParametros.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnParametros.Name = "btnParametros";
            this.btnParametros.Size = new System.Drawing.Size(98, 28);
            this.btnParametros.TabIndex = 9;
            this.btnParametros.Text = "Parámetros";
            this.btnParametros.UseVisualStyleBackColor = true;
            this.btnParametros.Click += new System.EventHandler(this.btnParametros_Click);
            // 
            // ptoVerdeCapa
            // 
            this.ptoVerdeCapa.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeCapa.Image")));
            this.ptoVerdeCapa.Location = new System.Drawing.Point(7, 31);
            this.ptoVerdeCapa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeCapa.Name = "ptoVerdeCapa";
            this.ptoVerdeCapa.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeCapa.TabIndex = 36;
            this.ptoVerdeCapa.TabStop = false;
            // 
            // ptoVerdeError
            // 
            this.ptoVerdeError.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeError.Image")));
            this.ptoVerdeError.Location = new System.Drawing.Point(269, 212);
            this.ptoVerdeError.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeError.Name = "ptoVerdeError";
            this.ptoVerdeError.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeError.TabIndex = 39;
            this.ptoVerdeError.TabStop = false;
            // 
            // panelAyuda
            // 
            this.panelAyuda.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelAyuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAyuda.Controls.Add(this.lblDescripcionCarpeta);
            this.panelAyuda.Controls.Add(this.lblTituloCarpeta);
            this.panelAyuda.Controls.Add(this.lblDescripcionError);
            this.panelAyuda.Controls.Add(this.lblTituloError);
            this.panelAyuda.Controls.Add(this.lblDescripcionRango);
            this.panelAyuda.Controls.Add(this.lblTituloRango);
            this.panelAyuda.Controls.Add(this.lblDescripcionMetodo);
            this.panelAyuda.Controls.Add(this.lblTituloMetodo);
            this.panelAyuda.Controls.Add(this.lblDescripcionCapa);
            this.panelAyuda.Controls.Add(this.lblTituloCapa);
            this.panelAyuda.Location = new System.Drawing.Point(476, 12);
            this.panelAyuda.Name = "panelAyuda";
            this.panelAyuda.Size = new System.Drawing.Size(509, 303);
            this.panelAyuda.TabIndex = 40;
            // 
            // lblDescripcionCarpeta
            // 
            this.lblDescripcionCarpeta.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionCarpeta.Location = new System.Drawing.Point(23, 57);
            this.lblDescripcionCarpeta.Name = "lblDescripcionCarpeta";
            this.lblDescripcionCarpeta.Size = new System.Drawing.Size(482, 33);
            this.lblDescripcionCarpeta.TabIndex = 11;
            this.lblDescripcionCarpeta.Text = "Ingrese la ruta destino donde se guadará la salida.";
            // 
            // lblTituloCarpeta
            // 
            this.lblTituloCarpeta.AutoSize = true;
            this.lblTituloCarpeta.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloCarpeta.Location = new System.Drawing.Point(22, 8);
            this.lblTituloCarpeta.Name = "lblTituloCarpeta";
            this.lblTituloCarpeta.Size = new System.Drawing.Size(153, 19);
            this.lblTituloCarpeta.TabIndex = 10;
            this.lblTituloCarpeta.Text = "Carpeta de destino";
            // 
            // lblDescripcionError
            // 
            this.lblDescripcionError.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionError.Location = new System.Drawing.Point(23, 57);
            this.lblDescripcionError.Name = "lblDescripcionError";
            this.lblDescripcionError.Size = new System.Drawing.Size(482, 33);
            this.lblDescripcionError.TabIndex = 9;
            this.lblDescripcionError.Text = "Ingrese el porcentaje de error en la estimación de los puntos no muestreados.";
            // 
            // lblTituloError
            // 
            this.lblTituloError.AutoSize = true;
            this.lblTituloError.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloError.Location = new System.Drawing.Point(22, 8);
            this.lblTituloError.Name = "lblTituloError";
            this.lblTituloError.Size = new System.Drawing.Size(158, 19);
            this.lblTituloError.TabIndex = 8;
            this.lblTituloError.Text = "Error de estimación";
            // 
            // lblDescripcionRango
            // 
            this.lblDescripcionRango.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionRango.Location = new System.Drawing.Point(23, 57);
            this.lblDescripcionRango.Name = "lblDescripcionRango";
            this.lblDescripcionRango.Size = new System.Drawing.Size(482, 33);
            this.lblDescripcionRango.TabIndex = 7;
            this.lblDescripcionRango.Text = "Ingrese el rango de correlación entre los puntos. Unidad es metros (m).";
            // 
            // lblTituloRango
            // 
            this.lblTituloRango.AutoSize = true;
            this.lblTituloRango.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloRango.Location = new System.Drawing.Point(22, 8);
            this.lblTituloRango.Name = "lblTituloRango";
            this.lblTituloRango.Size = new System.Drawing.Size(173, 19);
            this.lblTituloRango.TabIndex = 6;
            this.lblTituloRango.Text = "Rango de correlación";
            // 
            // lblDescripcionMetodo
            // 
            this.lblDescripcionMetodo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionMetodo.Location = new System.Drawing.Point(23, 57);
            this.lblDescripcionMetodo.Name = "lblDescripcionMetodo";
            this.lblDescripcionMetodo.Size = new System.Drawing.Size(482, 33);
            this.lblDescripcionMetodo.TabIndex = 5;
            this.lblDescripcionMetodo.Text = "Seleccione el método con el cual se realizará la estimación de los puntos no mues" +
                "treados.";
            // 
            // lblTituloMetodo
            // 
            this.lblTituloMetodo.AutoSize = true;
            this.lblTituloMetodo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloMetodo.Location = new System.Drawing.Point(22, 8);
            this.lblTituloMetodo.Name = "lblTituloMetodo";
            this.lblTituloMetodo.Size = new System.Drawing.Size(176, 19);
            this.lblTituloMetodo.TabIndex = 4;
            this.lblTituloMetodo.Text = "Método de estimación";
            // 
            // lblDescripcionCapa
            // 
            this.lblDescripcionCapa.AutoSize = true;
            this.lblDescripcionCapa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionCapa.Location = new System.Drawing.Point(23, 57);
            this.lblDescripcionCapa.Name = "lblDescripcionCapa";
            this.lblDescripcionCapa.Size = new System.Drawing.Size(402, 16);
            this.lblDescripcionCapa.TabIndex = 3;
            this.lblDescripcionCapa.Text = "Ingrese la capa con los posibles puntos de muestreo para optimizar.";
            // 
            // lblTituloCapa
            // 
            this.lblTituloCapa.AutoSize = true;
            this.lblTituloCapa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloCapa.Location = new System.Drawing.Point(22, 8);
            this.lblTituloCapa.Name = "lblTituloCapa";
            this.lblTituloCapa.Size = new System.Drawing.Size(148, 19);
            this.lblTituloCapa.TabIndex = 2;
            this.lblTituloCapa.Text = "Capa de muestreo";
            // 
            // txtCarpeta
            // 
            this.txtCarpeta.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCarpeta.Location = new System.Drawing.Point(34, 153);
            this.txtCarpeta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCarpeta.Name = "txtCarpeta";
            this.txtCarpeta.Size = new System.Drawing.Size(368, 21);
            this.txtCarpeta.TabIndex = 3;
            this.txtCarpeta.TextChanged += new System.EventHandler(this.txtCarpeta_TextChanged);
            this.txtCarpeta.GotFocus += new System.EventHandler(this.txtCarpeta_GotFocus);
            this.txtCarpeta.LostFocus += new System.EventHandler(this.txtCarpeta_LostFocus);
            // 
            // lblRutaDestino
            // 
            this.lblRutaDestino.AutoSize = true;
            this.lblRutaDestino.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRutaDestino.Location = new System.Drawing.Point(31, 134);
            this.lblRutaDestino.Name = "lblRutaDestino";
            this.lblRutaDestino.Size = new System.Drawing.Size(95, 15);
            this.lblRutaDestino.TabIndex = 42;
            this.lblRutaDestino.Text = "Carpeta destino";
            // 
            // btnCarpeta
            // 
            this.btnCarpeta.Image = ((System.Drawing.Image)(resources.GetObject("btnCarpeta.Image")));
            this.btnCarpeta.Location = new System.Drawing.Point(408, 153);
            this.btnCarpeta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCarpeta.Name = "btnCarpeta";
            this.btnCarpeta.Size = new System.Drawing.Size(31, 28);
            this.btnCarpeta.TabIndex = 4;
            this.btnCarpeta.UseVisualStyleBackColor = true;
            this.btnCarpeta.Click += new System.EventHandler(this.btnCarpeta_Click);
            // 
            // ptoVerdeCarpeta
            // 
            this.ptoVerdeCarpeta.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeCarpeta.Image")));
            this.ptoVerdeCarpeta.Location = new System.Drawing.Point(7, 153);
            this.ptoVerdeCarpeta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeCarpeta.Name = "ptoVerdeCarpeta";
            this.ptoVerdeCarpeta.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeCarpeta.TabIndex = 44;
            this.ptoVerdeCarpeta.TabStop = false;
            // 
            // txtExpIDW
            // 
            this.txtExpIDW.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpIDW.Location = new System.Drawing.Point(247, 90);
            this.txtExpIDW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtExpIDW.Name = "txtExpIDW";
            this.txtExpIDW.Size = new System.Drawing.Size(82, 21);
            this.txtExpIDW.TabIndex = 45;
            this.txtExpIDW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExpIDW.LostFocus += new System.EventHandler(this.txtExpIDW_LostFocus);
            // 
            // lblExpIDW
            // 
            this.lblExpIDW.AutoSize = true;
            this.lblExpIDW.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpIDW.Location = new System.Drawing.Point(244, 71);
            this.lblExpIDW.Name = "lblExpIDW";
            this.lblExpIDW.Size = new System.Drawing.Size(91, 15);
            this.lblExpIDW.TabIndex = 46;
            this.lblExpIDW.Text = "Exponente IDW";
            // 
            // ptoVerdeExp
            // 
            this.ptoVerdeExp.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeExp.Image")));
            this.ptoVerdeExp.Location = new System.Drawing.Point(222, 91);
            this.ptoVerdeExp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeExp.Name = "ptoVerdeExp";
            this.ptoVerdeExp.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeExp.TabIndex = 47;
            this.ptoVerdeExp.TabStop = false;
            // 
            // ptoVerdeMuestras
            // 
            this.ptoVerdeMuestras.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeMuestras.Image")));
            this.ptoVerdeMuestras.Location = new System.Drawing.Point(137, 212);
            this.ptoVerdeMuestras.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeMuestras.Name = "ptoVerdeMuestras";
            this.ptoVerdeMuestras.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeMuestras.TabIndex = 50;
            this.ptoVerdeMuestras.TabStop = false;
            // 
            // lblMuestras
            // 
            this.lblMuestras.AutoSize = true;
            this.lblMuestras.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMuestras.Location = new System.Drawing.Point(159, 190);
            this.lblMuestras.Name = "lblMuestras";
            this.lblMuestras.Size = new System.Drawing.Size(100, 15);
            this.lblMuestras.TabIndex = 49;
            this.lblMuestras.Text = "Nro de muestras";
            // 
            // txtCantMuestras
            // 
            this.txtCantMuestras.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantMuestras.Location = new System.Drawing.Point(162, 209);
            this.txtCantMuestras.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCantMuestras.Name = "txtCantMuestras";
            this.txtCantMuestras.Size = new System.Drawing.Size(94, 21);
            this.txtCantMuestras.TabIndex = 48;
            this.txtCantMuestras.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantMuestras.LostFocus += new System.EventHandler(this.txtCantMuestras_LostFocus);
            // 
            // ventanaSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 326);
            this.Controls.Add(this.ptoVerdeMuestras);
            this.Controls.Add(this.lblMuestras);
            this.Controls.Add(this.txtCantMuestras);
            this.Controls.Add(this.ptoVerdeExp);
            this.Controls.Add(this.lblExpIDW);
            this.Controls.Add(this.txtExpIDW);
            this.Controls.Add(this.ptoVerdeCarpeta);
            this.Controls.Add(this.btnCarpeta);
            this.Controls.Add(this.lblRutaDestino);
            this.Controls.Add(this.txtCarpeta);
            this.Controls.Add(this.panelAyuda);
            this.Controls.Add(this.ptoVerdeError);
            this.Controls.Add(this.ptoVerdeCapa);
            this.Controls.Add(this.btnParametros);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAyuda);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.lblRango);
            this.Controls.Add(this.txtRango);
            this.Controls.Add(this.lblMetodoEstimacion);
            this.Controls.Add(this.cboMetodoEstimacion);
            this.Controls.Add(this.lblCapaMuestreo);
            this.Controls.Add(this.cboCapaMuestreo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ventanaSSA";
            this.Text = "Optimización de muestreo";
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeCapa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeError)).EndInit();
            this.panelAyuda.ResumeLayout(false);
            this.panelAyuda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeCarpeta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeExp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeMuestras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCapaMuestreo;
        private System.Windows.Forms.ComboBox cboCapaMuestreo;
        private System.Windows.Forms.Label lblMetodoEstimacion;
        private System.Windows.Forms.ComboBox cboMetodoEstimacion;
        private System.Windows.Forms.Label lblRango;
        private System.Windows.Forms.TextBox txtRango;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAyuda;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnParametros;
        private System.Windows.Forms.PictureBox ptoVerdeCapa;
        private System.Windows.Forms.PictureBox ptoVerdeError;
        private System.Windows.Forms.Panel panelAyuda;
        private System.Windows.Forms.Label lblDescripcionCapa;
        private System.Windows.Forms.Label lblTituloCapa;
        private System.Windows.Forms.Label lblDescripcionMetodo;
        private System.Windows.Forms.Label lblTituloMetodo;
        private System.Windows.Forms.Label lblDescripcionRango;
        private System.Windows.Forms.Label lblTituloRango;
        private System.Windows.Forms.Label lblDescripcionError;
        private System.Windows.Forms.Label lblTituloError;
        private TextBox txtCarpeta;
        private Label lblRutaDestino;
        private Button btnCarpeta;
        private PictureBox ptoVerdeCarpeta;
        private Label lblDescripcionCarpeta;
        private Label lblTituloCarpeta;
        private TextBox txtExpIDW;
        private Label lblExpIDW;
        private PictureBox ptoVerdeExp;
        private PictureBox ptoVerdeMuestras;
        private Label lblMuestras;
        private TextBox txtCantMuestras;
    }
}