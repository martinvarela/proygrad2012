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
                txtArchivoZF.Text = open.FileName;
            }
        }
        
        private void Zonificacion_Click(object sender, EventArgs e)
        {
            txtArchivoZF.Text = "C:\\Users\\Nico\\Desktop\\Datos Proyecto\\20110608_153342\\Sur_todvar.ZF";                     
            txtMuestreo.Text = "C:\\Users\\Nico\\Desktop\\asdas";
            if (!ptoVerdeZF.Visible)
            {

                Controlador controlador = new Controlador();
                controlador.muestreoOptimo(txtArchivoZF.Text,10,10);
                this.Close();
            }
        }

        private void txtArchivoZF_TextChanged(object sender, EventArgs e)
        {
            if (txtArchivoZF.Text != "")
            {
                ptoVerdeZF.Visible = false;
            }
            else
            {
                ptoVerdeZF.Visible = true;
            }
        }

        private void botonAbrirMuestreo_Click(object sender, EventArgs e)
        {
            SaveFileDialog destino = new SaveFileDialog();
            //destino.CheckPathExists = true;
            if (destino.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtMuestreo.Text = destino.FileName.ToString();
            }

        }

        private void txtMuestreo_TextChanged(object sender, EventArgs e)
        {
            if (txtMuestreo.Text != "")
            {
                ptoVerdeDestino.Visible = false;
            }
            else
            {
                ptoVerdeDestino.Visible = true;
            }

        }

        private void txtArchivoZF_GotFocus(object sender, EventArgs e)
        {
            panelAyuda.Controls.Clear();

            panelAyuda.Controls.Add(labelTituloZonificacion);
            panelAyuda.Controls.Add(labelDescripcionZonificacion);
        }

        private void txtMuestreo_GotFocus(object sender, EventArgs e)
        {
            panelAyuda.Controls.Clear();

            panelAyuda.Controls.Add(labelTituloMuestreo);
            panelAyuda.Controls.Add(labelDescripcionMuestreo);
        }

        private void botonAyuda_Click(object sender, EventArgs e)
        {
            if (btnAyuda.Text.ToString() == "Mostrar ayuda >>")
            {
                panelAyuda.Visible = true;
                this.Size = new Size(922, 485);
                btnAyuda.Text = "Ocultar ayuda <<";
            }
            else
            {
                this.Size = new Size(484, 485);
                panelAyuda.Visible = true;
                btnAyuda.Text = "Mostrar ayuda >>";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
