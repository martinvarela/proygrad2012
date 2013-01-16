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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridVIewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.botonRed = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn,
            this.dataGridViewTextBoxColumn,
            this.DataGridVIewComboBoxColumn});
            this.dataGridView1.Location = new System.Drawing.Point(21, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(727, 197);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridViewCheckBoxColumn
            // 
            this.dataGridViewCheckBoxColumn.HeaderText = "Seleccion";
            this.dataGridViewCheckBoxColumn.Name = "dataGridViewCheckBoxColumn";
            this.dataGridViewCheckBoxColumn.Width = 60;
            // 
            // dataGridViewTextBoxColumn
            // 
            this.dataGridViewTextBoxColumn.HeaderText = "Layer";
            this.dataGridViewTextBoxColumn.Name = "dataGridViewTextBoxColumn";
            this.dataGridViewTextBoxColumn.Width = 300;
            // 
            // DataGridVIewComboBoxColumn
            // 
            this.DataGridVIewComboBoxColumn.HeaderText = "Atributos";
            this.DataGridVIewComboBoxColumn.Name = "DataGridVIewComboBoxColumn";
            this.DataGridVIewComboBoxColumn.Width = 300;
            // 
            // botonRed
            // 
            this.botonRed.Location = new System.Drawing.Point(480, 274);
            this.botonRed.Name = "botonRed";
            this.botonRed.Size = new System.Drawing.Size(75, 23);
            this.botonRed.TabIndex = 1;
            this.botonRed.Text = "Crear Red";
            this.botonRed.UseVisualStyleBackColor = true;
            this.botonRed.Click += new System.EventHandler(this.botonRed_Click);
            this.Visible = true;
            // 
            // ventanaBlackmore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 352);
            this.Controls.Add(this.botonRed);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ventanaBlackmore";
            this.Text = "ventanaBlackmore";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn;
        public System.Windows.Forms.DataGridViewComboBoxColumn DataGridVIewComboBoxColumn;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Button botonRed;
    }
}