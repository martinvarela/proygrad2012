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
            this.Zonificacion = new System.Windows.Forms.Button();
            this.lblOpenFileZF = new System.Windows.Forms.Label();
            this.ptoVerdeZF = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeZF)).BeginInit();
            this.SuspendLayout();
            // 
            // txtArchivoZF
            // 
            this.txtArchivoZF.Location = new System.Drawing.Point(27, 39);
            this.txtArchivoZF.Name = "txtArchivoZF";
            this.txtArchivoZF.Size = new System.Drawing.Size(227, 20);
            this.txtArchivoZF.TabIndex = 0;
            this.txtArchivoZF.TextChanged += new System.EventHandler(this.txtArchivoZF_TextChanged);
            // 
            // botonAbrir
            // 
            this.botonAbrir.Image = ((System.Drawing.Image)(resources.GetObject("botonAbrir.Image")));
            this.botonAbrir.Location = new System.Drawing.Point(260, 37);
            this.botonAbrir.Name = "botonAbrir";
            this.botonAbrir.Size = new System.Drawing.Size(27, 23);
            this.botonAbrir.TabIndex = 1;
            this.botonAbrir.UseVisualStyleBackColor = true;
            this.botonAbrir.Click += new System.EventHandler(this.botonAbrir_Click);
            // 
            // Zonificacion
            // 
            this.Zonificacion.Location = new System.Drawing.Point(66, 227);
            this.Zonificacion.Name = "Zonificacion";
            this.Zonificacion.Size = new System.Drawing.Size(75, 23);
            this.Zonificacion.TabIndex = 2;
            this.Zonificacion.Text = "Zonificacion";
            this.Zonificacion.UseVisualStyleBackColor = true;
            this.Zonificacion.Click += new System.EventHandler(this.Zonificacion_Click);
            // 
            // lblOpenFileZF
            // 
            this.lblOpenFileZF.AutoSize = true;
            this.lblOpenFileZF.Location = new System.Drawing.Point(24, 22);
            this.lblOpenFileZF.Name = "lblOpenFileZF";
            this.lblOpenFileZF.Size = new System.Drawing.Size(117, 13);
            this.lblOpenFileZF.TabIndex = 3;
            this.lblOpenFileZF.Text = "Archivo de zonificación";
            // 
            // ptoVerdeZF
            // 
            this.ptoVerdeZF.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeZF.Image")));
            this.ptoVerdeZF.Location = new System.Drawing.Point(11, 39);
            this.ptoVerdeZF.Name = "ptoVerdeZF";
            this.ptoVerdeZF.Size = new System.Drawing.Size(16, 16);
            this.ptoVerdeZF.TabIndex = 4;
            this.ptoVerdeZF.TabStop = false;
            // 
            // ventanaMuestreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 262);
            this.Controls.Add(this.ptoVerdeZF);
            this.Controls.Add(this.lblOpenFileZF);
            this.Controls.Add(this.Zonificacion);
            this.Controls.Add(this.botonAbrir);
            this.Controls.Add(this.txtArchivoZF);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ventanaMuestreo";
            this.ShowIcon = false;
            this.Text = "Optimización de muestreo";
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeZF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtArchivoZF;
        private System.Windows.Forms.Button botonAbrir;
        private System.Windows.Forms.Button Zonificacion;
        private System.Windows.Forms.Label lblOpenFileZF;
        private System.Windows.Forms.PictureBox ptoVerdeZF;

    }
}