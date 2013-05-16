using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.IO;

namespace Proyecto
{
    public partial class ventanaSSA : Form
    {
        public ventanaSSA()
        {
            try
            {
                InitializeComponent();

                Fabrica fabrica = Fabrica.getInstancia;
                IOptimizar ioptimizar = fabrica.getIOptimizar();
                List<string> listaCapas = ioptimizar.cargarCapasMuestreo();
                foreach (string nombreCapa in listaCapas)
                {
                    this.cboCapaMuestreo.Items.Add(nombreCapa);
                }

                this.cboCapaMuestreo.SelectedItem = 0;
                if (this.cboCapaMuestreo.Text == "")
                    this.ptoVerdeCapa.Visible = true;
                else
                    this.ptoVerdeCapa.Visible = false;

                //cargo los metodos de interpolacion posibles
                cboMetodoEstimacion.Items.Add("IDW");
                //cboMetodoEstimacion.Items.Add("Kriging");
                cboMetodoEstimacion.SelectedIndex = 0;

                //textbox error
                this.ptoVerdeError.Visible = true;

                //textExpIDW
                double d = 2.0;
                this.txtExpIDW.Text = d.ToString();
                this.ptoVerdeExp.Visible = false;

                //textCantMuestras
                this.ptoVerdeMuestras.Visible = true;

                this.Visible = true;
            }
            catch (ProyectoException p)
            {
                String[] strErrores = new String[1];
                strErrores[0] = p.Message;
                VentanaErrores v = new VentanaErrores(1, strErrores);
                v.ShowDialog();
            }
            catch
            {
                String[] strErrores = new String[1];
                strErrores[0] = "Error inesperado al cargar la ventana de optimización.";
                VentanaErrores v = new VentanaErrores(1, strErrores);
                v.ShowDialog();
            }
        }

        private void btnParametros_Click(object sender, EventArgs e)
        {
            //se crea la ventana principal del Muestreo
            ventanaParametrosSSA ventanaParametrosSSA = new ventanaParametrosSSA();
            //no se puede minimizar
            ventanaParametrosSSA.MinimizeBox = false;
            //no se puede maximizar
            ventanaParametrosSSA.MaximizeBox = false;
            //no se puede cambiar el tamano
            ventanaParametrosSSA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            ventanaParametrosSSA.ShowDialog();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //control previo de datos para la ejecucion de la funcion
                String[] stringErrores = new String[5];
                for (int i = 0; i < stringErrores.Length; i++)
                    stringErrores[i] = "";
                int cantidadErrores = 0;
                if (ptoVerdeCapa.Visible)
                {
                    cantidadErrores++;
                    stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblCapaMuestreo.Text + ": Debe seleccionar una capa de muestreo.";
                }
                if (ptoVerdeCarpeta.Visible)
                {
                    cantidadErrores++;
                    stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblRutaDestino.Text + ": Ruta inválida donde se guardarán las capas de optimización de muestreo.";
                }
                if (ptoVerdeMuestras.Visible)
                {
                    cantidadErrores++;
                    stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblMuestras.Text + ": Debe indicar un valor entero para el numero de muestras.";
                }
                if (ptoVerdeError.Visible)
                {
                    cantidadErrores++;
                    stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblError.Text + ": Debe indicar un valor entero para el error.";
                }
                if (ptoVerdeExp.Visible)
                {
                    cantidadErrores++;
                    stringErrores[cantidadErrores - 1] = "ERROR: " + this.lblExpIDW.Text + ": Debe indicar un valor real positivo para el exponente del metodo IDW.";
                }

                //si esta no hay errores, ejecuto la funcion de crear puntos de muestreo
                if (cantidadErrores == 0)
                {
                    //se deshabilita el boton Aceptar
                    this.btnAceptar.Enabled = false;

                    IMap targetMap = ArcMap.Document.FocusMap;

                    IEnumLayer enumLayers = targetMap.get_Layers();
                    enumLayers.Reset();
                    ILayer layer = enumLayers.Next();
                    while (layer != null && layer.Name != cboCapaMuestreo.SelectedItem.ToString())
                    {
                        layer = enumLayers.Next();
                    }

                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    IFeatureClass featureClass = featureLayer.FeatureClass;

                    //Obtengo el nombre y ruta de la capa de salida ingresado por el usuario
                    String rutaCapa = System.IO.Path.GetFullPath(this.txtCarpeta.Text.ToString());
                    Fabrica fabrica = Fabrica.getInstancia;
                    IOptimizar ioptimizar = fabrica.getIOptimizar();
                    ioptimizar.optimizarMuestreo(featureClass, cboMetodoEstimacion.SelectedItem.ToString(), double.Parse(txtExpIDW.Text.ToString()), int.Parse(txtCantMuestras.Text.ToString()), double.Parse(txtError.Text.ToString()), rutaCapa);

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
            catch
            {
                String[] strErrores = new String[1];
                strErrores[0] = "Error inesperado al ejecutar la optimización.";
                VentanaErrores v = new VentanaErrores(1, strErrores);
                v.ShowDialog();
            }
        }

        private void cboCapaMuestreo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cboCapaMuestreo.Text == "")
                {
                    this.ptoVerdeCapa.Visible = true;
                }
                else
                {
                    Fabrica fabrica = Fabrica.getInstancia;
                    IOptimizar ioptimizar = fabrica.getIOptimizar();
                    this.ptoVerdeCapa.Visible = false;
                    int muestras = ioptimizar.calcularArea(this.cboCapaMuestreo.Text);
                    if (muestras != -1)
                    {
                        this.txtCantMuestras.Text = muestras.ToString();
                        this.ptoVerdeMuestras.Visible = false;
                    }
                }
            }
            catch (ProyectoException p)
            {
                String[] strErrores = new String[1];
                strErrores[0] = p.Message;
                VentanaErrores v = new VentanaErrores(1, strErrores);
                v.ShowDialog();
            }
            catch
            {
                String[] strErrores = new String[1];
                strErrores[0] = "Error inesperado al ejecutar el cambio de capa.";
                VentanaErrores v = new VentanaErrores(1, strErrores);
                v.ShowDialog();
            }
        }

        private void cboCapaMuestreo_GotFocus(object sender, EventArgs e)
        {
            this.panelAyuda.Controls.Clear();
            this.panelAyuda.Controls.Add(this.lblTituloCapa);
            this.panelAyuda.Controls.Add(this.lblDescripcionCapa);
        }

        private void txtRango_LostFocus(object sender, EventArgs e)
        {
            try
            {
                int i;
                int cantMuestras;
                if (int.TryParse(this.txtRango.Text, out i))
                {
                    Fabrica fabrica = Fabrica.getInstancia;
                    IOptimizar ioptimizar = fabrica.getIOptimizar();

                    cantMuestras = ioptimizar.setearRango(i);
                    if (cantMuestras != -1)
                    {
                        this.txtCantMuestras.Text = cantMuestras.ToString();
                        this.ptoVerdeMuestras.Visible = false;
                    }
                }
                else
                {
                    this.txtRango.Text = "";
                }
            }
            catch (ProyectoException p)
            {
                String[] strErrores = new String[1];
                strErrores[0] = p.Message;
                VentanaErrores v = new VentanaErrores(1, strErrores);
                v.ShowDialog();
            }
            catch
            {
                String[] strErrores = new String[1];
                strErrores[0] = "Error inesperado al setear el rango de capa.";
                VentanaErrores v = new VentanaErrores(1, strErrores);
                v.ShowDialog();
            }
        }

        private void txtError_LostFocus(object sender, EventArgs e)
        {
            double i;
            if (double.TryParse(this.txtError.Text, out i))
            {
                this.ptoVerdeError.Visible = false;
                this.txtError.Text = i.ToString();
            }
            else
            {
                this.txtError.Text = "";
                this.ptoVerdeError.Visible = true;
            }
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            if (this.btnAyuda.Text == "Mostrar ayuda >>")
            {
                panelAyuda.Visible = true;
                this.Size = new Size(1014, 364);
                btnAyuda.Text = "Ocultar ayuda <<";
            }
            else
            {
                this.Size = new Size(484, 364);
                panelAyuda.Visible = true;
                btnAyuda.Text = "Mostrar ayuda >>";
            }
        }

        private void cboMetodoEstimacion_GotFocus(object sender, EventArgs e)
        {
            this.panelAyuda.Controls.Clear();
            this.panelAyuda.Controls.Add(this.lblTituloMetodo);
            this.panelAyuda.Controls.Add(this.lblDescripcionMetodo);
        }

        private void txtRango_GotFocus(object sender, EventArgs e)
        {
            this.panelAyuda.Controls.Clear();
            this.panelAyuda.Controls.Add(this.lblTituloRango);
            this.panelAyuda.Controls.Add(this.lblDescripcionRango);
        }

        private void txtError_GotFocus(object sender, EventArgs e)
        {
            this.panelAyuda.Controls.Clear();
            this.panelAyuda.Controls.Add(this.lblTituloError);
            this.panelAyuda.Controls.Add(this.lblDescripcionError);
        }

        private void btnCarpeta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog carpeta = new FolderBrowserDialog();
            if (carpeta.ShowDialog() == DialogResult.OK)
            {
                txtCarpeta.Text = carpeta.SelectedPath;
            }
        }

        private void txtCarpeta_LostFocus(object sender, EventArgs e)
        {
            if (txtCarpeta.Text != "" )
                this.ptoVerdeCarpeta.Visible = false;
            else
                this.ptoVerdeCarpeta.Visible = true;
        }

        private void txtCarpeta_TextChanged(object sender, EventArgs e)
        {
            if (txtCarpeta.Text != "")
            {
                this.ptoVerdeCarpeta.Visible = false;
            }
            else
            {
                this.ptoVerdeCarpeta.Visible = true;
            }
        }

        private void txtCarpeta_GotFocus(object sender, EventArgs e)
        {
            this.panelAyuda.Controls.Clear();
            this.panelAyuda.Controls.Add(this.lblTituloCarpeta);
            this.panelAyuda.Controls.Add(this.lblDescripcionCarpeta);
        }

        private void txtExpIDW_LostFocus(object sender, EventArgs e)
        {
            double i;
            if (double.TryParse(this.txtExpIDW.Text, out i))
            {
                this.ptoVerdeExp.Visible = false;
                this.txtExpIDW.Text = i.ToString();
            }
            else
            {
                this.txtExpIDW.Text = "";
                this.ptoVerdeExp.Visible = true;
            }
        }
        
        private void txtCantMuestras_LostFocus(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(this.txtCantMuestras.Text, out i))
            {
                this.ptoVerdeMuestras.Visible = false;
                this.txtCantMuestras.Text = i.ToString();
            }
            else
            {
                this.txtCantMuestras.Text = "";
                this.ptoVerdeMuestras.Visible = true;
            }
        }


    }
}
