//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.IO;
//using ESRI.ArcGIS.Carto;

namespace Proyecto
{
    public class botonOptimizar : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public botonOptimizar()
        {
        }

        protected override void OnClick()
        {
            //se crea la ventana principal del SSA
            ventanaSSA ventana = new ventanaSSA();
            //no se puede minimizar
            ventana.MinimizeBox = true;
            //no se puede maximizar
            ventana.MaximizeBox = false;
            //no se puede cambiar el tamano
            ventana.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        protected override void OnUpdate()
        {
        }
    }
}