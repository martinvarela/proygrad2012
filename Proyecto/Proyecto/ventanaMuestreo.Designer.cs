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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.botonAbrir = new System.Windows.Forms.Button();
            this.Zonificacion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // botonAbrir
            // 
            this.botonAbrir.Location = new System.Drawing.Point(120, 13);
            this.botonAbrir.Name = "botonAbrir";
            this.botonAbrir.Size = new System.Drawing.Size(75, 23);
            this.botonAbrir.TabIndex = 1;
            this.botonAbrir.Text = "abrir";
            this.botonAbrir.UseVisualStyleBackColor = true;
            this.botonAbrir.Click += new System.EventHandler(this.botonAbrir_Click);
            // 
            // Zonificacion
            // 
            this.Zonificacion.Location = new System.Drawing.Point(120, 128);
            this.Zonificacion.Name = "Zonificacion";
            this.Zonificacion.Size = new System.Drawing.Size(75, 23);
            this.Zonificacion.TabIndex = 2;
            this.Zonificacion.Text = "Zonificacion";
            this.Zonificacion.UseVisualStyleBackColor = true;
            this.Zonificacion.Click += new System.EventHandler(this.Zonificacion_Click);
            // 
            // ventanaMuestreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.Zonificacion);
            this.Controls.Add(this.botonAbrir);
            this.Controls.Add(this.textBox1);
            this.Name = "ventanaMuestreo";
            this.Text = "ventanaMuestreo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button botonAbrir;
        private System.Windows.Forms.Button Zonificacion;

    }
}