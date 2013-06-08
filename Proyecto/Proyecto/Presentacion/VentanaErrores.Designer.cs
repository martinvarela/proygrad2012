using System;
partial class VentanaErrores
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaErrores));
        this.btnAceptar = new System.Windows.Forms.Button();
        this.panel = new System.Windows.Forms.Panel();
        this.lblErrores = new System.Windows.Forms.Label();
        this.lblImagen = new System.Windows.Forms.Label();
        this.panel.SuspendLayout();
        this.SuspendLayout();
        // 
        // btnAceptar
        // 
        resources.ApplyResources(this.btnAceptar, "btnAceptar");
        this.btnAceptar.Name = "btnAceptar";
        this.btnAceptar.UseVisualStyleBackColor = true;
        this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
        // 
        // panel
        // 
        resources.ApplyResources(this.panel, "panel");
        this.panel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
        this.panel.Controls.Add(this.lblErrores);
        this.panel.Controls.Add(this.lblImagen);
        this.panel.Name = "panel";
        // 
        // lblErrores
        // 
        resources.ApplyResources(this.lblErrores, "lblErrores");
        this.lblErrores.Name = "lblErrores";
        // 
        // lblImagen
        // 
        this.lblImagen.Image = global::Proyecto.Properties.Resources.advertencia;
        resources.ApplyResources(this.lblImagen, "lblImagen");
        this.lblImagen.Name = "lblImagen";
        // 
        // VentanaErrores
        // 
        resources.ApplyResources(this, "$this");
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.Controls.Add(this.panel);
        this.Controls.Add(this.btnAceptar);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "VentanaErrores";
        this.ShowIcon = false;
        this.panel.ResumeLayout(false);
        this.panel.PerformLayout();
        this.ResumeLayout(false);

        }

    #endregion

    private System.Windows.Forms.Button btnAceptar;
    private System.Windows.Forms.Panel panel;
    private System.Windows.Forms.Label lblErrores;
    private System.Windows.Forms.Label lblImagen;

}