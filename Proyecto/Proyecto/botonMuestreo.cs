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
            ventana.Visible = true;
        }

        protected override void OnUpdate()
        {
        }
    }
}
