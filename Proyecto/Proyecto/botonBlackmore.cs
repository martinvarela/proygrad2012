using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using System.Data;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
//using ESRI.ArcGIS.esriSystem;
//using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;


namespace Proyecto
{
    public class botonBlackmore : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public botonBlackmore()
        {
        }

        protected override void OnClick()
        {
            //se crea la ventana principal de Blackmore
            ventanaBlackmore ventana = new ventanaBlackmore();
        }

    }

}
