using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class ventanaMuestreo : Form
    {
        public ventanaMuestreo()
        {
            InitializeComponent();
        }

        private void botonAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            
            open.Filter = "ZF Files|*.ZF|All Files|*.*";
            open.FilterIndex = 1;

            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = open.FileName;
            }
        }
        
        private void Zonificacion_Click(object sender, EventArgs e)
        {
            Zonificacion zonificacion = new Zonificacion(textBox1.Text);         
        }
    }
}
