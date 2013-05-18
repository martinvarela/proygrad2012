using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.DataManagementTools;
using System.IO;

namespace Proyecto
{
    public partial class ventanaBlackmore : Form
    {
        private List<DTCapasBlackmore> listaCapas;

        public ventanaBlackmore()
        {
            try
            {
                InitializeComponent();

                Fabrica fabrica = Fabrica.getInstancia;
                IBlackmore iBlackmore = fabrica.getIBlackmore();
                this.listaCapas = iBlackmore.cargarCapasBlackmore();

                foreach (DTCapasBlackmore capa in this.listaCapas)
                {
                    //agrego la nueva fila
                    int i;
                    i = dgvVentana.Rows.Add(new DataGridViewRow());

                    DataGridViewCell check = new DataGridViewCheckBoxCell();
                    check.Value = false;
                    dgvVentana.Rows[i].Cells[0] = check;

                    DataGridViewCell nombreCapa = new DataGridViewTextBoxCell();
                    nombreCapa.Value = capa.getNombreCapa();
                    dgvVentana.Rows[i].Cells[1] = nombreCapa;

                    //cargo el combo con los atributos
                    DataGridViewComboBoxCell comboAtributos = new DataGridViewComboBoxCell();
                    List<string> listaAtributos = capa.getListaAtributos();
                    foreach (string atributo in listaAtributos)
                    {
                        comboAtributos.Items.Add(atributo);
                    }
                    //se inicializa el combo de atributos
                    comboAtributos.Value = listaAtributos[0];

                    dgvVentana.Rows[i].Cells[2] = comboAtributos;
                }

                this.cboTipoRed.SelectedIndex = 0;
                this.Visible = true;
            }
            catch (ProyectoException p)
            {
                String[] strErrores = new String[1];
                strErrores[0] = p.Message;
                VentanaErrores v = new VentanaErrores(1, strErrores);
                v.ShowDialog();
            }
        }

        //dgvVentana
        private void dgvVentana_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.cboBase.Items.Clear();
                for (int i = 0; i < this.dgvVentana.RowCount; i++)
                {
                    if ((bool)this.dgvVentana.Rows[i].Cells[0].Value)
                    {
                        this.cboBase.Items.Add(this.dgvVentana.Rows[i].Cells[1].Value.ToString());
                    }
                }
                this.cboBase.SelectedItem = 0;
                this.ptoVerdeBase.Visible = true;
            }
        }

        private void dgvVentana_GotFocus(object sender, EventArgs e) 
        {
            panelAyuda.Controls.Clear();

            panelAyuda.Controls.Add(labelTituloCapas);
            panelAyuda.Controls.Add(labelDescripcionCapas);
        }

        //btnRuta
        private void btnRuta_Click(object sender, EventArgs e)
        {
            SaveFileDialog destino = new SaveFileDialog();
            destino.OverwritePrompt = false;
            if (destino.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtRuta.Text = destino.FileName.ToString();
            }
        }

        //cboBase
        private void cboBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboBase.SelectedItem.ToString() == "")
                this.ptoVerdeBase.Visible = true;
            else
                this.ptoVerdeBase.Visible = false;
        }
        private void cboBase_GotFocus(object sender, EventArgs e)
        {
            panelAyuda.Controls.Clear();

            panelAyuda.Controls.Add(labelTituloCapaBase);
            panelAyuda.Controls.Add(labelDescripcionCapaBase);
        }

        //txtEstabilidad
        private void txtEstabilidad_LostFocus(object sender, EventArgs e)
        {
            double i;
            if (double.TryParse(this.txtEstabilidad.Text, out i) && i >= 0)
            {
                this.ptoVerdeEstabilidad.Visible = false;
                this.txtEstabilidad.Text = i.ToString();
            }
            else
            {
                this.txtEstabilidad.Text = "";
                this.ptoVerdeEstabilidad.Visible = true;
            }
        }
        private void txtEstabilidad_GotFocus(object sender, EventArgs e)
        {
            panelAyuda.Controls.Clear();

            panelAyuda.Controls.Add(labelTituloEstabilidad);
            panelAyuda.Controls.Add(labelDescripcionEstabilidad);
        }

        //cboTipoRed
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
        private void cboTipoRed_GotFocus(object sender, EventArgs e)
        {
            panelAyuda.Controls.Clear();

            panelAyuda.Controls.Add(labelTituloTipoRed);
            panelAyuda.Controls.Add(labelDescripcionTipo);
        }        

        //txtVertical
        private void txtVertical_LostFocus(object sender, EventArgs e)
        {
            int i = 0;
            if (int.TryParse(this.txtVertical.Text.ToString(), out i) && i > 0)
            {
                ptoVerdeVertical.Visible = false;
            }
            else
            {
                this.txtVertical.Text = "";
                ptoVerdeVertical.Visible = true;
            }
        }
        private void txtVertical_GotFocus(object sender, EventArgs e)
        {
            this.cboTipoRed_GotFocus(sender, e);
        }
        
        //txtHorizontal
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
        private void txtHorizontal_GotFocus(object sender, EventArgs e)
        {
            this.cboTipoRed_GotFocus(sender, e);
        }


        //btnCancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //txtRuta
        private void txtRuta_TextChanged(object sender, EventArgs e)
        {
            if (txtRuta.Text != "")
            {
                ptoVerdeRuta.Visible = false;
            }
            else
            {
                ptoVerdeRuta.Visible = true;
            }
        }
        private void txtRuta_GotFocus(object sender, EventArgs e)
        {
            panelAyuda.Controls.Clear();

            panelAyuda.Controls.Add(labelTituloRuta);
            panelAyuda.Controls.Add(labelDescripcionRuta);
        }

        //btnAyuda
        private void btnAyuda_Click(object sender, EventArgs e)
        {
            if (btnAyuda.Text.ToString() == "Mostrar ayuda >>")
            {
                panelAyuda.Visible = true;
                this.Size = new Size(925, 512);
                btnAyuda.Text = "Ocultar ayuda <<";
            }
            else
            {
                panelAyuda.Visible = true;
                this.Size = new Size(477, 512);
                btnAyuda.Text = "Mostrar ayuda >>";
            }
        }
        

        //btnAceptar
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //control previo de datos para la ejecucion de la funcion
                /////////////////////////////////////////////////////////

                String[] stringErrores = new String[6];
                for (int i = 0; i < stringErrores.Length; i++)
                    stringErrores[i] = "";
                int cantidadErrores = 0;

                if (ptoVerdeBase.Visible)
                {
                    cantidadErrores++;
                    stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblBase.Text + ": Se debe seleccionar una capa base.";
                }

                Fabrica fabrica = Fabrica.getInstancia;
                IBlackmore iBlackmore = fabrica.getIBlackmore();

                List<DTCapasBlackmore> capasMarcadas = new List<DTCapasBlackmore>();
                for (int i = 0; i < this.dgvVentana.RowCount; i++)
                {
                    if ((bool)this.dgvVentana.Rows[i].Cells[0].FormattedValue)
                    {
                        DTCapasBlackmore dt = new DTCapasBlackmore();
                        dt.setNombreCapa(this.dgvVentana.Rows[i].Cells[1].Value.ToString());
                        dt.agregarAtributo(this.dgvVentana.Rows[i].Cells[2].Value.ToString());
                        dt.setCapa(this.listaCapas[i].getCapa());
                        if (dt.getNombreCapa() == this.cboBase.SelectedItem.ToString())
                            dt.setCapaBase(true);
                        else
                            dt.setCapaBase(false);
                        capasMarcadas.Add(dt);
                    }
                }

                if (capasMarcadas.Count < 2)
                {
                    cantidadErrores++;
                    stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblDGV.Text + ": Se deben de marcar al menos dos capas";
                }
                if (ptoVerdeEstabilidad.Visible)
                {
                    cantidadErrores++;
                    stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblEstabilidad.Text + ": Se debe ingresar al menos un valor.";
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
                if (ptoVerdeRuta.Visible)
                {
                    cantidadErrores++;
                    stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblRuta.Text + ": Ruta inválida donde se guardará la capa de Blackmore.";
                }

                //si esta no hay errores, ejecuto la funcion de crear capa de Blackmore
                ///////////////////////////////////////////////////////////////////////
                if (cantidadErrores == 0)
                {
                    //Obtengo el nombre y ruta de la capa de salida ingresado por el usuario
                    string rutaCapa = Path.GetDirectoryName(this.txtRuta.Text.ToString());
                    string nombreCapa = Path.GetFileNameWithoutExtension(this.txtRuta.Text.ToString());

                    //desabilito el boton Aceptar
                    this.btnAceptar.Enabled = false;

                    bool filasColumnas;
                    if (this.cboTipoRed.SelectedIndex == 0)
                        filasColumnas = true;
                    else
                        filasColumnas = false;

                    //llamada a crear Blackmore
                    DTPCrearBlackmore dtp = new DTPCrearBlackmore(filasColumnas, int.Parse(this.txtVertical.Text), int.Parse(this.txtHorizontal.Text), capasMarcadas, double.Parse(this.txtEstabilidad.Text), nombreCapa, rutaCapa);
                    iBlackmore.crearBlackmore(dtp);
                    this.Close();
                }
                else
                {
                    VentanaErrores ventanaErrores = new VentanaErrores(cantidadErrores, stringErrores);
                    ventanaErrores.ShowDialog();
                }
            }
            catch (ProyectoException p)
            {
                String[] strErrores = new String[1];
                strErrores[0] = p.Message;
                VentanaErrores v = new VentanaErrores(1, strErrores);
                v.ShowDialog();
            }
        }
    }
}
