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
            this.SuspendLayout();
            // 
            // lblCapaMuestreo
            // 
            this.lblCapaMuestreo.AutoSize = true;
            this.lblCapaMuestreo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCapaMuestreo.Location = new System.Drawing.Point(29, 20);
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
            this.cboCapaMuestreo.Location = new System.Drawing.Point(32, 39);
            this.cboCapaMuestreo.Name = "cboCapaMuestreo";
            this.cboCapaMuestreo.Size = new System.Drawing.Size(371, 23);
            this.cboCapaMuestreo.TabIndex = 23;
            // 
            // lblMetodoEstimacion
            // 
            this.lblMetodoEstimacion.AutoSize = true;
            this.lblMetodoEstimacion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetodoEstimacion.Location = new System.Drawing.Point(31, 82);
            this.lblMetodoEstimacion.Name = "lblMetodoEstimacion";
            this.lblMetodoEstimacion.Size = new System.Drawing.Size(128, 15);
            this.lblMetodoEstimacion.TabIndex = 26;
            this.lblMetodoEstimacion.Text = "Metodo de estimación";
            // 
            // cboMetodoEstimacion
            // 
            this.cboMetodoEstimacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMetodoEstimacion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMetodoEstimacion.FormattingEnabled = true;
            this.cboMetodoEstimacion.Location = new System.Drawing.Point(32, 101);
            this.cboMetodoEstimacion.Name = "cboMetodoEstimacion";
            this.cboMetodoEstimacion.Size = new System.Drawing.Size(370, 23);
            this.cboMetodoEstimacion.TabIndex = 25;
            // 
            // lblRango
            // 
            this.lblRango.AutoSize = true;
            this.lblRango.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRango.Location = new System.Drawing.Point(29, 145);
            this.lblRango.Name = "lblRango";
            this.lblRango.Size = new System.Drawing.Size(44, 15);
            this.lblRango.TabIndex = 28;
            this.lblRango.Text = "Rango";
            // 
            // txtRango
            // 
            this.txtRango.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRango.Location = new System.Drawing.Point(32, 164);
            this.txtRango.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRango.Name = "txtRango";
            this.txtRango.Size = new System.Drawing.Size(174, 21);
            this.txtRango.TabIndex = 27;
            this.txtRango.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.Location = new System.Drawing.Point(29, 201);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(115, 15);
            this.lblError.TabIndex = 30;
            this.lblError.Text = "Error en estimación";
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtError.Location = new System.Drawing.Point(32, 220);
            this.txtError.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(174, 21);
            this.txtError.TabIndex = 29;
            this.txtError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(32, 248);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(420, 23);
            this.pBar.TabIndex = 34;
            this.pBar.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(134, 292);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 28);
            this.btnCancelar.TabIndex = 32;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAyuda
            // 
            this.btnAyuda.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAyuda.Location = new System.Drawing.Point(339, 292);
            this.btnAyuda.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAyuda.Name = "btnAyuda";
            this.btnAyuda.Size = new System.Drawing.Size(112, 28);
            this.btnAyuda.TabIndex = 33;
            this.btnAyuda.Text = "Mostrar ayuda >>";
            this.btnAyuda.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(32, 292);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(98, 28);
            this.btnAceptar.TabIndex = 31;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnParametros
            // 
            this.btnParametros.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParametros.Location = new System.Drawing.Point(236, 292);
            this.btnParametros.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnParametros.Name = "btnParametros";
            this.btnParametros.Size = new System.Drawing.Size(98, 28);
            this.btnParametros.TabIndex = 35;
            this.btnParametros.Text = "Parámetros";
            this.btnParametros.UseVisualStyleBackColor = true;
            this.btnParametros.Click += new System.EventHandler(this.btnParametros_Click);
            // 
            // ventanaSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 333);
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
            this.Name = "ventanaSSA";
            this.Text = "Optimización de muestreo";
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
    }
}