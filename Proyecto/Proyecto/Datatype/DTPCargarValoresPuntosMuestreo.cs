using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

class DTPCargarValoresPuntosMuestreo
{
    private IMap map;
    private Muestreo muestreo;
    private string nombreCapaPuntosZonificacion;
    private string nombreCapaPoligonos;
    private string nombreCapaPuntosMuestreos;
    private int indiceAtributoEnTablaPoligonos;
    private int indiceAtributoEnTablaPuntos;
    private ProgressBar pBar;


    public DTPCargarValoresPuntosMuestreo(IMap map, Muestreo muestreo, string nombreCapaPuntosZonificacion, string nombreCapaPoligonos, string nombreCapaPuntosMuestreos, int indiceAtributoEnTablaPoligonos, int indiceAtributoEnTablaPuntos, ProgressBar pBar)
    {
        this.map = map;
        this.muestreo = muestreo;
        this.nombreCapaPuntosZonificacion = nombreCapaPuntosZonificacion;
        this.nombreCapaPoligonos = nombreCapaPoligonos;
        this.nombreCapaPuntosMuestreos = nombreCapaPuntosMuestreos;
        this.indiceAtributoEnTablaPoligonos = indiceAtributoEnTablaPoligonos;
        this.indiceAtributoEnTablaPuntos = indiceAtributoEnTablaPuntos;
        this.pBar = pBar;
    }

    public IMap getMap()
    {
        return this.map;
    }

    public Muestreo getMuestreo()
    {
        return this.muestreo;
    }

    public string getNombreCapaPuntosZonificacion()
    {
        return this.nombreCapaPuntosZonificacion;
    }

    public string getNombreCapaPoligonos()
    {
        return this.nombreCapaPoligonos;
    }

    public string getNombreCapaPuntosMuestreos()
    {
        return this.nombreCapaPuntosMuestreos;
    }

    public int getIndiceAtributoEnTablaPoligonos()
    {
        return this.indiceAtributoEnTablaPoligonos;
    }

    public int getIndiceAtributoEnTablaPuntos()
    {
        return this.indiceAtributoEnTablaPuntos;
    }

    public ProgressBar getPBar()
    {
        return this.pBar;
    }


        
        
        
}
