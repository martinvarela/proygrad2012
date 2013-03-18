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
            txtArchivoZF.Text = "C:\\Users\\Gonzalo\\Desktop\\New folder\\20110608_153342\\Sur_todvar.ZF";
            txtMuestreo.Text = "C:\\Users\\Gonzalo\\Desktop\\asdas";
            if (!ptoVerdeZF.Visible)
            {

                Controlador controlador = new Controlador();
                controlador.muestreoOptimo(txtArchivoZF.Text,10,10);
                this.Close();
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
            string string_rows = "Rows: ";
            string string_cols = "Cols: ";
            string string_Xinicial = "CoordX: ";
            string string_Yinicial = "CoordY: ";
            string string_CellSize = "CellSize: ";
            string string_cant_variables = "VarQty:";
            string NAN = "NaN";

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
