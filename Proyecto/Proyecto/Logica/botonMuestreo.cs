using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace Proyecto
{
    public class botonMuestreo : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public botonMuestreo()
        {
        }

        protected override void OnClick()
        {
            //se crea la ventana principal del Muestreo
            ventanaMuestreo ventana = new ventanaMuestreo();
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
