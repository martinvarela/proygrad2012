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
            this.labelDescripcionMuestreo = new System.Windows.Forms.Label();
            this.labelTituloMuestreo = new System.Windows.Forms.Label();
            this.labelDescripcionZonificacion = new System.Windows.Forms.Label();
            this.labelTituloZonificacion = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeZF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeDestino)).BeginInit();
            this.panelAyuda.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtArchivoZF
            // 
            this.txtArchivoZF.Location = new System.Drawing.Point(31, 48);
            this.txtArchivoZF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtArchivoZF.Name = "txtArchivoZF";
            this.txtArchivoZF.Size = new System.Drawing.Size(377, 22);
            this.txtArchivoZF.TabIndex = 0;
            this.txtArchivoZF.TextChanged += new System.EventHandler(this.txtArchivoZF_TextChanged);
            this.txtArchivoZF.GotFocus += new System.EventHandler(this.txtArchivoZF_GotFocus);
            // 
            // botonAbrir
            // 
            this.botonAbrir.Image = ((System.Drawing.Image)(resources.GetObject("botonAbrir.Image")));
            this.botonAbrir.Location = new System.Drawing.Point(420, 46);
            this.botonAbrir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.botonAbrir.Name = "botonAbrir";
            this.botonAbrir.Size = new System.Drawing.Size(31, 28);
            this.botonAbrir.TabIndex = 1;
            this.botonAbrir.UseVisualStyleBackColor = true;
            this.botonAbrir.Click += new System.EventHandler(this.botonAbrir_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(31, 396);
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
            this.lblOpenFileZF.AutoSize = true;
            this.lblOpenFileZF.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenFileZF.Location = new System.Drawing.Point(28, 27);
            this.lblOpenFileZF.Name = "lblOpenFileZF";
            this.lblOpenFileZF.Size = new System.Drawing.Size(140, 16);
            this.lblOpenFileZF.TabIndex = 3;
            this.lblOpenFileZF.Text = "Archivo de zonificación";
            // 
            // ptoVerdeZF
            // 
            this.ptoVerdeZF.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeZF.Image")));
            this.ptoVerdeZF.Location = new System.Drawing.Point(13, 48);
            this.ptoVerdeZF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeZF.Name = "ptoVerdeZF";
            this.ptoVerdeZF.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeZF.TabIndex = 4;
            this.ptoVerdeZF.TabStop = false;
            // 
            // ptoVerdeDestino
            // 
            this.ptoVerdeDestino.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeDestino.Image")));
            this.ptoVerdeDestino.Location = new System.Drawing.Point(13, 116);
            this.ptoVerdeDestino.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeDestino.Name = "ptoVerdeDestino";
            this.ptoVerdeDestino.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeDestino.TabIndex = 8;
            this.ptoVerdeDestino.TabStop = false;
            // 
            // lblMuestreo
            // 
            this.lblMuestreo.AutoSize = true;
            this.lblMuestreo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMuestreo.Location = new System.Drawing.Point(28, 95);
            this.lblMuestreo.Name = "lblMuestreo";
            this.lblMuestreo.Size = new System.Drawing.Size(114, 16);
            this.lblMuestreo.TabIndex = 7;
            this.lblMuestreo.Text = "Capa de muestreo";
            // 
            // botonAbrirMuestreo
            // 
            this.botonAbrirMuestreo.Image = ((System.Drawing.Image)(resources.GetObject("botonAbrirMuestreo.Image")));
            this.botonAbrirMuestreo.Location = new System.Drawing.Point(420, 112);
            this.botonAbrirMuestreo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.botonAbrirMuestreo.Name = "botonAbrirMuestreo";
            this.botonAbrirMuestreo.Size = new System.Drawing.Size(31, 28);
            this.botonAbrirMuestreo.TabIndex = 6;
            this.botonAbrirMuestreo.UseVisualStyleBackColor = true;
            this.botonAbrirMuestreo.Click += new System.EventHandler(this.botonAbrirMuestreo_Click);
            // 
            // txtMuestreo
            // 
            this.txtMuestreo.Location = new System.Drawing.Point(31, 116);
            this.txtMuestreo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMuestreo.Name = "txtMuestreo";
            this.txtMuestreo.Size = new System.Drawing.Size(377, 22);
            this.txtMuestreo.TabIndex = 5;
            this.txtMuestreo.TextChanged += new System.EventHandler(this.txtMuestreo_TextChanged);
            this.txtMuestreo.GotFocus += new System.EventHandler(this.txtMuestreo_GotFocus);
            // 
            // btnAyuda
            // 
            this.btnAyuda.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAyuda.Location = new System.Drawing.Point(294, 396);
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
            this.panelAyuda.Controls.Add(this.labelDescripcionMuestreo);
            this.panelAyuda.Controls.Add(this.labelTituloMuestreo);
            this.panelAyuda.Controls.Add(this.labelDescripcionZonificacion);
            this.panelAyuda.Controls.Add(this.labelTituloZonificacion);
            this.panelAyuda.Location = new System.Drawing.Point(475, 27);
            this.panelAyuda.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelAyuda.Name = "panelAyuda";
            this.panelAyuda.Size = new System.Drawing.Size(408, 397);
            this.panelAyuda.TabIndex = 10;
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
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(162, 396);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 28);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // ventanaMuestreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 447);
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
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeZF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeDestino)).EndInit();
            this.panelAyuda.ResumeLayout(false);
            this.panelAyuda.PerformLayout();
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


    }
}