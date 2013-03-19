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
            //txtArchivoZF.Text = "C:\\Users\\Gonzalo\\Desktop\\New folder\\20110608_153342\\Sur_todvar.ZF";
            txtMuestreo.Text = "C:\\Users\\Gonzalo\\Desktop\\asdas";
            if (!ptoVerdeZF.Visible)
            {

                Controlador controlador = new Controlador();
                List<int> variables = new List<int>();
                for (int i = 0; i < chkLstVariables.Items.Count; i++)
                {
                    if (chkLstVariables.GetItemChecked(i))
                        variables.Add(i);
                }
                if (this.cboTipoRed.SelectedIndex == 0)
                {
                    controlador.muestreoOptimoFilasColumnas(txtArchivoZF.Text, int.Parse(this.txtVertical.Text), int.Parse(this.txtHorizontal.Text), variables);
                }
                else if (this.cboTipoRed.SelectedIndex == 1)
                {
                    //controlador.muestreoOptimoAltoAncho(txtArchivoZF.Text, int.Parse(this.txtVertical.Text), int.Parse(this.txtHorizontal.Text), variables);
                }
                
                // Display the ProgressBar control.
                pBar.Visible = true;
                // Set Minimum to 1 to represent the first file being copied.
                pBar.Minimum = 1;
                // Set Maximum to the total number of files to copy.
                pBar.Maximum = 50;
                // Set the initial value of the ProgressBar.
                pBar.Value = 1;
                // Set the Step property to a value of 1 to represent each file being copied.
                pBar.Step = 1;

                // Loop through all files to copy.
                for (int x = 1; x <= 50; x++)
                {
                    System.Threading.Thread.Sleep(500);                    
                    // Perform the increment on the ProgressBar.
                    pBar.PerformStep();
                }

                pBar.Visible = false;
                //this.Close();
            }
        }

        private void txtArchivoZF_TextChanged(object sender, EventArgs e)
        {
            if (txtArchivoZF.Text.EndsWith(".ZF"))
            {
                ptoVerdeZF.Visible = false;
                this.cargarVariables(txtArchivoZF.Text);
            }
            else
            {
                ptoVerdeZF.Visible = true;
            }
        }

        //cargar las variables del archivo .zf
        private void cargarVariables(string rutaZF)
        {
            //Obtengo el archivo
            StreamReader objReader = new StreamReader(rutaZF);
            //Incicializo la variable donde voy a guardar cada linea que leo y la variable donde voy a guardar en memoria el contenido del archivo
            string sLine = "";
            int Rows = 0;
            int Cols = 0;
            double xinicial = 0;
            double yinicial = 0;
            int cellSize = 0;
            int cant_variables = 0;
            string string_cant_variables = "VarQty:";

            string comienzo_datos = "[Cells]";

            //Leo la linea actual del archivo
            sLine = objReader.ReadLine();

            //leo hasta la etiqueta [Cells] y saco los valores de rows, cols y cant_variables 
            while (sLine != null)
            {
                if (((sLine != "") && (sLine.Length >= string_cant_variables.Length) && sLine.Substring(0, string_cant_variables.Length) == string_cant_variables))
                {
                    cant_variables = Int32.Parse(sLine.Substring(string_cant_variables.Length, sLine.Length - string_cant_variables.Length));
                    int i = 1;
                    sLine = objReader.ReadLine();
                    String nombreVariable = "";
                    while (i <= cant_variables && sLine != "")
                    {
                        String aux = "Var" + i + ": ";
                        if ((sLine.Substring(0, aux.Length) == aux))
                        {
                            nombreVariable = sLine.Substring(aux.Length, sLine.Length - aux.Length);
                            this.chkLstVariables.Items.Add(nombreVariable);
                        }
                        i++;
                        sLine = objReader.ReadLine();
                    }
                }

                //Llegue a la etiqueta [Cells] entonces se que a continuacion empiezan los valores de los puntos muestreados
                if (((sLine != "") && (sLine.Substring(0, comienzo_datos.Length) == comienzo_datos)))
                    break;

                sLine = objReader.ReadLine();
            }  //fin while de datos generales
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
                this.Size = new Size(954, 556);
                btnAyuda.Text = "Ocultar ayuda <<";
            }
            else
            {
                this.Size = new Size(484, 556);
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
            panelAyuda.Controls.Add(lblDescripcionRed2);
            panelAyuda.Controls.Add(lblDescripcionRed3);
            panelAyuda.Controls.Add(lblDescripcionRed4);
            panelAyuda.Controls.Add(lblDescripcionRed5);
            panelAyuda.Controls.Add(lblDescripcionRed6);
            panelAyuda.Controls.Add(lblDescripcionRed7);
        }

        private void txtVertical_GotFucus(object sender, EventArgs e)
        {
            panelAyuda.Controls.Clear();
            panelAyuda.Controls.Add(lblTituloRed);
            panelAyuda.Controls.Add(lblDescripcionRed1);
            panelAyuda.Controls.Add(lblDescripcionRed2);
            panelAyuda.Controls.Add(lblDescripcionRed3);
            panelAyuda.Controls.Add(lblDescripcionRed4);
            panelAyuda.Controls.Add(lblDescripcionRed5);
            panelAyuda.Controls.Add(lblDescripcionRed6);
            panelAyuda.Controls.Add(lblDescripcionRed7);
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
            panelAyuda.Controls.Clear();
            panelAyuda.Controls.Add(lblTituloRed);
            panelAyuda.Controls.Add(lblDescripcionRed1);
            panelAyuda.Controls.Add(lblDescripcionRed2);
            panelAyuda.Controls.Add(lblDescripcionRed3);
            panelAyuda.Controls.Add(lblDescripcionRed4);
            panelAyuda.Controls.Add(lblDescripcionRed5);
            panelAyuda.Controls.Add(lblDescripcionRed6);
            panelAyuda.Controls.Add(lblDescripcionRed7);
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
    }
}
