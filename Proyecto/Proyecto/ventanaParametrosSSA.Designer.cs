﻿namespace Proyecto
{
    partial class ventanaParametrosSSA
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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblTemperatura = new System.Windows.Forms.Label();
            this.txtTemperatura = new System.Windows.Forms.TextBox();
            this.lblFactor = new System.Windows.Forms.Label();
            this.txtFactor = new System.Windows.Forms.TextBox();
            this.lblEpsilon = new System.Windows.Forms.Label();
            this.txtEpsilon = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(47, 193);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(110, 28);
            this.btnCerrar.TabIndex = 10;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblTemperatura
            // 
            this.lblTemperatura.AutoSize = true;
            this.lblTemperatura.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemperatura.Location = new System.Drawing.Point(12, 9);
            this.lblTemperatura.Name = "lblTemperatura";
            this.lblTemperatura.Size = new System.Drawing.Size(112, 15);
            this.lblTemperatura.TabIndex = 26;
            this.lblTemperatura.Text = "Temperatura inicial";
            // 
            // txtTemperatura
            // 
            this.txtTemperatura.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemperatura.Location = new System.Drawing.Point(15, 28);
            this.txtTemperatura.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTemperatura.Name = "txtTemperatura";
            this.txtTemperatura.Size = new System.Drawing.Size(174, 21);
            this.txtTemperatura.TabIndex = 25;
            this.txtTemperatura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTemperatura.GotFocus += new System.EventHandler(this.txtTemperatura_GotFocus);
            this.txtTemperatura.LostFocus += new System.EventHandler(this.txtTemperatura_LostFocus);
            // 
            // lblFactor
            // 
            this.lblFactor.AutoSize = true;
            this.lblFactor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactor.Location = new System.Drawing.Point(9, 66);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(115, 15);
            this.lblFactor.TabIndex = 28;
            this.lblFactor.Text = "Factor de reducción";
            // 
            // txtFactor
            // 
            this.txtFactor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFactor.Location = new System.Drawing.Point(15, 86);
            this.txtFactor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFactor.Name = "txtFactor";
            this.txtFactor.Size = new System.Drawing.Size(174, 21);
            this.txtFactor.TabIndex = 27;
            this.txtFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFactor.GotFocus += new System.EventHandler(this.txtFactor_GotFocus);
            this.txtFactor.LostFocus += new System.EventHandler(this.txtFactor_LostFocus);
            // 
            // lblEpsilon
            // 
            this.lblEpsilon.AutoSize = true;
            this.lblEpsilon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEpsilon.Location = new System.Drawing.Point(12, 125);
            this.lblEpsilon.Name = "lblEpsilon";
            this.lblEpsilon.Size = new System.Drawing.Size(49, 15);
            this.lblEpsilon.TabIndex = 30;
            this.lblEpsilon.Text = "Epsilon";
            // 
            // txtEpsilon
            // 
            this.txtEpsilon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEpsilon.Location = new System.Drawing.Point(15, 144);
            this.txtEpsilon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEpsilon.Name = "txtEpsilon";
            this.txtEpsilon.Size = new System.Drawing.Size(174, 21);
            this.txtEpsilon.TabIndex = 29;
            this.txtEpsilon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEpsilon.GotFocus += new System.EventHandler(this.txtEpsilon_GotFocus);
            this.txtEpsilon.LostFocus += new System.EventHandler(this.txtEpsilon_LostFocus);
            // 
            // ventanaParametrosSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 236);
            this.Controls.Add(this.lblEpsilon);
            this.Controls.Add(this.txtEpsilon);
            this.Controls.Add(this.lblFactor);
            this.Controls.Add(this.txtFactor);
            this.Controls.Add(this.lblTemperatura);
            this.Controls.Add(this.txtTemperatura);
            this.Controls.Add(this.btnCerrar);
            this.Name = "ventanaParametrosSSA";
            this.Text = "Parametros SSA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblTemperatura;
        private System.Windows.Forms.TextBox txtTemperatura;
        private System.Windows.Forms.Label lblFactor;
        private System.Windows.Forms.TextBox txtFactor;
        private System.Windows.Forms.Label lblEpsilon;
        private System.Windows.Forms.TextBox txtEpsilon;
    }
}