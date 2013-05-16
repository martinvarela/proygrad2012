using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

interface IMuestreo
{
    Muestreo crearPuntosMuestreo(DTPCrearPuntosMuestreo dtp);

    List<string> cargarVariables(string rutaZF);
}
