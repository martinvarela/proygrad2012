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

namespace Proyecto
{
    public partial class ventanaSSA : Form
    {
        public ventanaSSA()
        {
            InitializeComponent();
            IMap targetMap = ArcMap.Document.FocusMap;
            
            //cargo el combo de capas abiertas
            IEnumLayer enumLayers = targetMap.get_Layers();
            enumLayers.Reset();
            ILayer layer = enumLayers.Next();
            while (layer != null)
            {
                this.cboCapaMuestreo.Items.Add(layer.Name.ToString()); 
                layer = enumLayers.Next();
                
            }

            //cargo los metodos de interpolacion posibles
            cboMetodoEstimacion.Items.Add("Kriging");
            cboMetodoEstimacion.Items.Add("IDW");
            cboMetodoEstimacion.SelectedIndex = 0;


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

            ventanaParametrosSSA.Visible = true;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
