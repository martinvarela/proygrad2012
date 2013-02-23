using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


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
            //no se puede minimizar
            ventana.MinimizeBox = false;
            //no se puede maximizar
            ventana.MaximizeBox = false;
            //no se puede cambiar el tamano
            ventana.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            ventana.Visible = true;
        }

        protected override void OnUpdate()
        {
        }
    }
}
