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

            ventana.Visible = true;

            //Controlador c1 = Controlador.getInstancia;
            //c1.numero = 3;
            //MessageBox.Show("en muestreo se creo el controlador c1 y tiene numero " + c1.numero);
        }

        protected override void OnUpdate()
        {
        }
    }
}
