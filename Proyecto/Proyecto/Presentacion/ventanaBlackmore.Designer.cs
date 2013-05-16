using System.Windows.Forms;
namespace Proyecto
{
    partial class ventanaBlackmore
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ventanaBlackmore));
            this.dgvVentana = new System.Windows.Forms.DataGridView();
            this.dgvCheckBoxSeleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvTextBoxCapas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvComboBoxAtributo = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ptoVerdeRuta = new System.Windows.Forms.PictureBox();
            this.lblRuta = new System.Windows.Forms.Label();
            this.btnRuta = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAyuda = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.panelAyuda = new System.Windows.Forms.Panel();
            this.ptoVerdeHorizontal = new System.Windows.Forms.PictureBox();
            this.ptoVerdeVertical = new System.Windows.Forms.PictureBox();
            this.lblVertical = new System.Windows.Forms.Label();
            this.lblTipoRed = new System.Windows.Forms.Label();
            this.cboTipoRed = new System.Windows.Forms.ComboBox();
            this.txtVertical = new System.Windows.Forms.TextBox();
            this.txtHorizontal = new System.Windows.Forms.TextBox();
            this.lblHorizontal = new System.Windows.Forms.Label();
            this.lblEstabilidad = new System.Windows.Forms.Label();
            this.ptoVerdeEstabilidad = new System.Windows.Forms.PictureBox();
            this.txtEstabilidad = new System.Windows.Forms.TextBox();
            this.lblDGV = new System.Windows.Forms.Label();
            this.lblBase = new System.Windows.Forms.Label();
            this.cboBase = new System.Windows.Forms.ComboBox();
            this.ptoVerdeBase = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeRuta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeHorizontal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeEstabilidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeBase)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvVentana
            // 
            this.dgvVentana.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.dgvVentana.AllowDrop = true;
            this.dgvVentana.AllowUserToAddRows = false;
            this.dgvVentana.AllowUserToDeleteRows = false;
            this.dgvVentana.AllowUserToResizeColumns = false;
            this.dgvVentana.AllowUserToResizeRows = false;
            this.dgvVentana.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVentana.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVentana.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVentana.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCheckBoxSeleccionar,
            this.dgvTextBoxCapas,
            this.dgvComboBoxAtributo});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVentana.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVentana.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvVentana.Location = new System.Drawing.Point(32, 32);
            this.dgvVentana.Name = "dgvVentana";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVentana.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVentana.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVentana.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVentana.Size = new System.Drawing.Size(396, 170);
            this.dgvVentana.TabIndex = 0;
            this.dgvVentana.CellValueChanged +=new DataGridViewCellEventHandler(dgvVentana_CellValueChanged);
            //
            // dgvCheckBoxSeleccionar
            // 
            this.dgvCheckBoxSeleccionar.HeaderText = "Usar";
            this.dgvCheckBoxSeleccionar.Name = "dgvCheckBoxSeleccionar";
            this.dgvCheckBoxSeleccionar.Width = 45;
            // 
            // dgvTextBoxCapas
            // 
            this.dgvTextBoxCapas.HeaderText = "Capas";
            this.dgvTextBoxCapas.Name = "dgvTextBoxCapas";
            this.dgvTextBoxCapas.ReadOnly = true;
            this.dgvTextBoxCapas.Width = 213;
            // 
            // dgvComboBoxAtributo
            // 
            this.dgvComboBoxAtributo.HeaderText = "Atributo";
            this.dgvComboBoxAtributo.Name = "dgvComboBoxAtributo";
            this.dgvComboBoxAtributo.Width = 135;
            // 
            // ptoVerdeRuta
            // 
            this.ptoVerdeRuta.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeRuta.Image")));
            this.ptoVerdeRuta.Location = new System.Drawing.Point(7, 381);
            this.ptoVerdeRuta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeRuta.Name = "ptoVerdeRuta";
            this.ptoVerdeRuta.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeRuta.TabIndex = 12;
            this.ptoVerdeRuta.TabStop = false;
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuta.Location = new System.Drawing.Point(29, 359);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(124, 15);
            this.lblRuta.TabIndex = 11;
            this.lblRuta.Text = "Ruta destino de capa";
            // 
            // btnRuta
            // 
            this.btnRuta.Image = ((System.Drawing.Image)(resources.GetObject("btnRuta.Image")));
            this.btnRuta.Location = new System.Drawing.Point(397, 378);
            this.btnRuta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.Size = new System.Drawing.Size(31, 28);
            this.btnRuta.TabIndex = 10;
            this.btnRuta.UseVisualStyleBackColor = true;
            this.btnRuta.Click += new System.EventHandler(this.btnRuta_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuta.Location = new System.Drawing.Point(32, 380);
            this.txtRuta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(359, 21);
            this.txtRuta.TabIndex = 9;
            this.txtRuta.TextChanged += new System.EventHandler(this.txtRuta_TextChanged);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(32, 407);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(396, 23);
            this.pBar.TabIndex = 19;
            this.pBar.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(159, 437);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(121, 28);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAyuda
            // 
            this.btnAyuda.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAyuda.Location = new System.Drawing.Point(284, 437);
            this.btnAyuda.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAyuda.Name = "btnAyuda";
            this.btnAyuda.Size = new System.Drawing.Size(144, 28);
            this.btnAyuda.TabIndex = 18;
            this.btnAyuda.Text = "Mostrar ayuda >>";
            this.btnAyuda.UseVisualStyleBackColor = true;
            this.btnAyuda.Click += new System.EventHandler(this.btnAyuda_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(32, 437);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(121, 28);
            this.btnAceptar.TabIndex = 16;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // panelAyuda
            // 
            this.panelAyuda.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelAyuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAyuda.Location = new System.Drawing.Point(474, 13);
            this.panelAyuda.Name = "panelAyuda";
            this.panelAyuda.Size = new System.Drawing.Size(399, 407);
            this.panelAyuda.TabIndex = 20;
            // 
            // ptoVerdeHorizontal
            // 
            this.ptoVerdeHorizontal.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeHorizontal.Image")));
            this.ptoVerdeHorizontal.Location = new System.Drawing.Point(232, 331);
            this.ptoVerdeHorizontal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeHorizontal.Name = "ptoVerdeHorizontal";
            this.ptoVerdeHorizontal.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeHorizontal.TabIndex = 33;
            this.ptoVerdeHorizontal.TabStop = false;
            // 
            // ptoVerdeVertical
            // 
            this.ptoVerdeVertical.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeVertical.Image")));
            this.ptoVerdeVertical.Location = new System.Drawing.Point(7, 332);
            this.ptoVerdeVertical.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeVertical.Name = "ptoVerdeVertical";
            this.ptoVerdeVertical.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeVertical.TabIndex = 32;
            this.ptoVerdeVertical.TabStop = false;
            // 
            // lblVertical
            // 
            this.lblVertical.AutoSize = true;
            this.lblVertical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVertical.Location = new System.Drawing.Point(29, 310);
            this.lblVertical.Name = "lblVertical";
            this.lblVertical.Size = new System.Drawing.Size(34, 15);
            this.lblVertical.TabIndex = 31;
            this.lblVertical.Text = "Filas";
            // 
            // lblTipoRed
            // 
            this.lblTipoRed.AutoSize = true;
            this.lblTipoRed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoRed.Location = new System.Drawing.Point(29, 260);
            this.lblTipoRed.Name = "lblTipoRed";
            this.lblTipoRed.Size = new System.Drawing.Size(69, 15);
            this.lblTipoRed.TabIndex = 30;
            this.lblTipoRed.Text = "Tipo de red";
            // 
            // cboTipoRed
            // 
            this.cboTipoRed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoRed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoRed.FormattingEnabled = true;
            this.cboTipoRed.Items.AddRange(new object[] {
            "Cantidad de celdas",
            "Tamaño de celdas"});
            this.cboTipoRed.Location = new System.Drawing.Point(32, 279);
            this.cboTipoRed.Name = "cboTipoRed";
            this.cboTipoRed.Size = new System.Drawing.Size(171, 23);
            this.cboTipoRed.TabIndex = 27;
            this.cboTipoRed.SelectedIndexChanged += new System.EventHandler(this.cboTipoRed_SelectedIndexChanged);
            // 
            // txtVertical
            // 
            this.txtVertical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVertical.Location = new System.Drawing.Point(32, 331);
            this.txtVertical.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVertical.Name = "txtVertical";
            this.txtVertical.Size = new System.Drawing.Size(171, 21);
            this.txtVertical.TabIndex = 28;
            this.txtVertical.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVertical.LostFocus += new System.EventHandler(this.txtVertical_LostFocus);
            // 
            // txtHorizontal
            // 
            this.txtHorizontal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHorizontal.Location = new System.Drawing.Point(257, 331);
            this.txtHorizontal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHorizontal.Name = "txtHorizontal";
            this.txtHorizontal.Size = new System.Drawing.Size(171, 21);
            this.txtHorizontal.TabIndex = 29;
            this.txtHorizontal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHorizontal.LostFocus += new System.EventHandler(this.txtHorizontal_LostFocus);
            // 
            // lblHorizontal
            // 
            this.lblHorizontal.AutoSize = true;
            this.lblHorizontal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHorizontal.Location = new System.Drawing.Point(254, 312);
            this.lblHorizontal.Name = "lblHorizontal";
            this.lblHorizontal.Size = new System.Drawing.Size(65, 15);
            this.lblHorizontal.TabIndex = 34;
            this.lblHorizontal.Text = "Columnas";
            // 
            // lblEstabilidad
            // 
            this.lblEstabilidad.AutoSize = true;
            this.lblEstabilidad.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstabilidad.Location = new System.Drawing.Point(281, 208);
            this.lblEstabilidad.Name = "lblEstabilidad";
            this.lblEstabilidad.Size = new System.Drawing.Size(69, 15);
            this.lblEstabilidad.TabIndex = 37;
            this.lblEstabilidad.Text = "Estabilidad";
            // 
            // ptoVerdeEstabilidad
            // 
            this.ptoVerdeEstabilidad.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeEstabilidad.Image")));
            this.ptoVerdeEstabilidad.Location = new System.Drawing.Point(259, 229);
            this.ptoVerdeEstabilidad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeEstabilidad.Name = "ptoVerdeEstabilidad";
            this.ptoVerdeEstabilidad.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeEstabilidad.TabIndex = 36;
            this.ptoVerdeEstabilidad.TabStop = false;
            this.ptoVerdeEstabilidad.Visible = false;
            // 
            // txtEstabilidad
            // 
            this.txtEstabilidad.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstabilidad.Location = new System.Drawing.Point(284, 229);
            this.txtEstabilidad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEstabilidad.Name = "txtEstabilidad";
            this.txtEstabilidad.Size = new System.Drawing.Size(144, 21);
            this.txtEstabilidad.TabIndex = 35;
            this.txtEstabilidad.Text = "1.0";
            this.txtEstabilidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEstabilidad.LostFocus += new System.EventHandler(this.txtEstabilidad_LostFocus);
            // 
            // lblDGV
            // 
            this.lblDGV.AutoSize = true;
            this.lblDGV.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDGV.Location = new System.Drawing.Point(29, 9);
            this.lblDGV.Name = "lblDGV";
            this.lblDGV.Size = new System.Drawing.Size(92, 15);
            this.lblDGV.TabIndex = 39;
            this.lblDGV.Text = "Capas a utilizar";
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBase.Location = new System.Drawing.Point(29, 208);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(68, 15);
            this.lblBase.TabIndex = 41;
            this.lblBase.Text = "Capa base";
            // 
            // cboBase
            // 
            this.cboBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBase.FormattingEnabled = true;
            this.cboBase.Location = new System.Drawing.Point(32, 227);
            this.cboBase.Name = "cboBase";
            this.cboBase.Size = new System.Drawing.Size(219, 23);
            this.cboBase.TabIndex = 40;
            this.cboBase.SelectedIndexChanged += new System.EventHandler(cboBase_SelectedIndexChanged);
            // 
            // ptoVerdeBase
            // 
            this.ptoVerdeBase.Image = ((System.Drawing.Image)(resources.GetObject("ptoVerdeBase.Image")));
            this.ptoVerdeBase.Location = new System.Drawing.Point(7, 227);
            this.ptoVerdeBase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ptoVerdeBase.Name = "ptoVerdeBase";
            this.ptoVerdeBase.Size = new System.Drawing.Size(19, 20);
            this.ptoVerdeBase.TabIndex = 42;
            this.ptoVerdeBase.TabStop = false;
            // 
            // ventanaBlackmore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 472);
            this.Controls.Add(this.ptoVerdeBase);
            this.Controls.Add(this.lblBase);
            this.Controls.Add(this.cboBase);
            this.Controls.Add(this.lblDGV);
            this.Controls.Add(this.lblEstabilidad);
            this.Controls.Add(this.ptoVerdeEstabilidad);
            this.Controls.Add(this.txtEstabilidad);
            this.Controls.Add(this.lblHorizontal);
            this.Controls.Add(this.ptoVerdeHorizontal);
            this.Controls.Add(this.ptoVerdeVertical);
            this.Controls.Add(this.lblVertical);
            this.Controls.Add(this.lblTipoRed);
            this.Controls.Add(this.cboTipoRed);
            this.Controls.Add(this.txtVertical);
            this.Controls.Add(this.txtHorizontal);
            this.Controls.Add(this.panelAyuda);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAyuda);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.ptoVerdeRuta);
            this.Controls.Add(this.lblRuta);
            this.Controls.Add(this.btnRuta);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.dgvVentana);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ventanaBlackmore";
            this.RightToLeftLayout = true;
            this.Text = "Blackmore - Variabilidad temporal";
            //this.Load += new System.EventHandler(this.ventanaBlackmore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeRuta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeHorizontal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeEstabilidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptoVerdeBase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvVentana;
        private System.Windows.Forms.PictureBox ptoVerdeRuta;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.Button btnRuta;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAyuda;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel panelAyuda;
        private System.Windows.Forms.PictureBox ptoVerdeHorizontal;
        private System.Windows.Forms.PictureBox ptoVerdeVertical;
        private System.Windows.Forms.Label lblVertical;
        private System.Windows.Forms.Label lblTipoRed;
        private System.Windows.Forms.ComboBox cboTipoRed;
        private System.Windows.Forms.TextBox txtVertical;
        private System.Windows.Forms.TextBox txtHorizontal;
        private System.Windows.Forms.Label lblHorizontal;
        private System.Windows.Forms.Label lblEstabilidad;
        private System.Windows.Forms.PictureBox ptoVerdeEstabilidad;
        private System.Windows.Forms.TextBox txtEstabilidad;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvCheckBoxSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxCapas;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvComboBoxAtributo;
        private System.Windows.Forms.Label lblDGV;
        private System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.ComboBox cboBase;
        private System.Windows.Forms.PictureBox ptoVerdeBase;
    }
}