using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Proyecto
{
    public partial class ventanaMuestreo : Form
    {
        public ventanaMuestreo()
        {
            InitializeComponent();

            this.cboTipoRed.SelectedIndex = 0;
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
            //control previo de datos para la ejecucion de la funcion
            String[] stringErrores = new String[5];
            for (int i = 0; i < stringErrores.Length; i++) 
                stringErrores[i] = "";
            int cantidadErrores = 0;
            if (ptoVerdeZF.Visible)
            {
                cantidadErrores++;
                stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblOpenFileZF.Text + ": Ruta inválida para el archivo de zonificación(*.ZF).";
            }
            if (ptoVerdeVariables.Visible)
            {
                cantidadErrores++;
                stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblVariables.Text + ": Se debe marcar alguna variable.";
            }
            if (ptoVerdeVertical.Visible)
            {
                cantidadErrores++;
                stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblVertical.Text + ": El valor debe ser entero y mayor que cero.";
            }
            if (ptoVerdeHorizontal.Visible)
            {
                cantidadErrores++;
                stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblHorizontal.Text + ": El valor debe ser entero y mayor que cero.";
            }
            if (ptoVerdeDestino.Visible)
            {
                cantidadErrores++;
                stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblMuestreo.Text + ": Ruta inválida donde se guardará la capa de posibles puntos de muestreo.";
            }
            

            //si esta no hay errores, ejecuto la funcion de crear puntos de muestreo
            if (cantidadErrores == 0)
            {
                Controlador controlador = Controlador.getInstancia;
                List<int> variables = new List<int>();
                for (int i = 0; i < chkLstVariables.Items.Count; i++)
                {
                    if (chkLstVariables.GetItemChecked(i))
                        variables.Add(i);
                }

                //Obtengo el nombre y ruta de la capa de salida ingresado por el usuario
                String rutaCapa = Path.GetDirectoryName(this.txtMuestreo.Text.ToString());
                String nombreCapa = Path.GetFileNameWithoutExtension(this.txtMuestreo.Text.ToString());

                //desabilito el boton Aceptar
                this.btnAceptar.Enabled = false;

                if (chkSinRed.Checked)
                {
                    controlador.crearPuntosMuestreo(false, txtArchivoZF.Text, true, 0, 0, variables, pBar, rutaCapa, nombreCapa, this.lblProgressBar);
                }
                else if (this.cboTipoRed.SelectedIndex == 0)
                {
                    controlador.crearPuntosMuestreo(true, txtArchivoZF.Text, true, int.Parse(this.txtVertical.Text), int.Parse(this.txtHorizontal.Text), variables, pBar, rutaCapa, nombreCapa, this.lblProgressBar);
                }
                else if (this.cboTipoRed.SelectedIndex == 1)
                {
                    controlador.crearPuntosMuestreo(true, txtArchivoZF.Text, false, int.Parse(this.txtVertical.Text), int.Parse(this.txtHorizontal.Text), variables, pBar, rutaCapa, nombreCapa, this.lblProgressBar);
                }
                
                this.Close();
            }
            else
            {
                VentanaErrores ventanaErrores = new VentanaErrores(cantidadErrores, stringErrores);
                ventanaErrores.ShowDialog();
            }
        }

        private void txtArchivoZF_TextChanged(object sender, EventArgs e)
        {
            if (txtArchivoZF.Text.EndsWith(".ZF"))
            {
                ptoVerdeZF.Visible = false;
                Controlador controlador = Controlador.getInstancia;
                List<string> listaVariables = controlador.cargarVariables(txtArchivoZF.Text);
                foreach (string nombreVariable in listaVariables)
                {
                    this.chkLstVariables.Items.Add(nombreVariable);
                }

                //this.cargarVariables(txtArchivoZF.Text);
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
            destino.OverwritePrompt = false;
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
                this.Size = new Size(950, 506);
                btnAyuda.Text = "Ocultar ayuda <<";
            }
            else
            {
                this.Size = new Size(482, 506);
                panelAyuda.Visible = true;
                btnAyuda.Text = "Mostrar ayuda >>";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkLstVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkLstVariables.CheckedItems.Count == 0)
            {
                ptoVerdeVariables.Visible = true;
            }
            else
            {
                ptoVerdeVariables.Visible = false;
            }
        }

        private void chkLstVariables_GotFocus(object sender, EventArgs e)
        {
            panelAyuda.Controls.Clear();
            panelAyuda.Controls.Add(lblTituloVariables);
            panelAyuda.Controls.Add(lblDescripcionVariables);
        }

        private void cboTipoRed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboTipoRed.SelectedIndex == 0)
            {
                //cantidad de filas y columnas
                this.lblHorizontal.Text = "Columnas";
                this.lblVertical.Text = "Filas";
            }
            else if (this.cboTipoRed.SelectedIndex == 1)
            {
                //definir tamano de celda
                this.lblHorizontal.Text = "Ancho";
                this.lblVertical.Text = "Alto";
            }
        }

        private void cboTipoRed_GotFucus(object sender, EventArgs e)
        {
            panelAyuda.Controls.Clear();
            panelAyuda.Controls.Add(lblTituloRed);
            panelAyuda.Controls.Add(lblDescripcionRed1);
            panelAyuda.Controls.Add(lblDescripcionVariables);
            panelAyuda.Controls.Add(lblDescripcionRed3);
            panelAyuda.Controls.Add(lblDescripcionRed4);
            panelAyuda.Controls.Add(lblDescripcionRed5);
            panelAyuda.Controls.Add(lblDescripcionRed6);
            panelAyuda.Controls.Add(lblDescripcionRed7);
            panelAyuda.Controls.Add(lblDescripcionRed8);
            panelAyuda.Controls.Add(lblDescripcionRed9);
        }

        private void txtVertical_GotFucus(object sender, EventArgs e)
        {
            cboTipoRed_GotFucus(sender, e);
        }

        private void txtVertical_LostFocus(object sender, EventArgs e)
        {
            int i = 0;
            if (int.TryParse(this.txtVertical.Text.ToString(),out i) && i>0)
            {
                ptoVerdeVertical.Visible = false;
            }
            else
            {
                this.txtVertical.Text = "";
                ptoVerdeVertical.Visible = true;
            }
        }

        private void txtHorizontal_GotFucus(object sender, EventArgs e)
        {
            cboTipoRed_GotFucus(sender, e);
        }

        private void txtHorizontal_LostFocus(object sender, EventArgs e)
        {
            int i = 0;
            if (int.TryParse(this.txtHorizontal.Text.ToString(), out i) && i > 0)
            {
                ptoVerdeHorizontal.Visible = false;
            }
            else
            {
                this.txtHorizontal.Text = "";
                ptoVerdeHorizontal.Visible = true;
            }
        }

        private void chkSinRed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSinRed.Checked)
            {
                cboTipoRed.Enabled = false;
                txtHorizontal.Enabled = false;
                txtVertical.Enabled = false;
                ptoVerdeHorizontal.Visible = false;
                ptoVerdeVertical.Visible = false;
            }
            else
            {
                cboTipoRed.Enabled = true;
                txtHorizontal.Enabled = true;
                txtVertical.Enabled = true;
                ptoVerdeHorizontal.Visible = true;
                ptoVerdeVertical.Visible = true;
            }
        }

        private void chkSinRed_GotFocus(object sender, EventArgs e)
        {
            cboTipoRed_GotFucus(sender, e);
        }

    }
}
