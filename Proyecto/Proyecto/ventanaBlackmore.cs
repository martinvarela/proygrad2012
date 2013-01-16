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

namespace Proyecto
{
    public partial class ventanaBlackmore : Form
    {
        public ventanaBlackmore()
        {
            InitializeComponent();
        }

        private void botonRed_Click(object sender, EventArgs e)
        {
            IMap map = ArcMap.Document.FocusMap;
            classBlackmore cb = new classBlackmore();
            String ruta = "C:\\Usuarios\\Gonzalo\\Escritorio\\";
            String nombreLayer = "capaRed";
            cb.crearRed(map,ruta,nombreLayer,true);

            this.Close();
            
            //ESRI.ArcGIS.DataManagementTools.CreateFishnet(redLayer,);

        }

    }


}
