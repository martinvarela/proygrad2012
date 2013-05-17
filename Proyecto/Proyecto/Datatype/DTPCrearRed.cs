using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

class DTPCrearRed
{
    private IMap targetMap;
    private string nombreCapaPoligonos;
    private string nombreCapa;
    private IPoint puntoOrigen;
    private IPoint puntoOpuesto;
    private bool filasColumnas;
    private int vertical;
    private int horizontal;
    private bool selectable;
    private string capaZonificacion;

    public DTPCrearRed(IMap targetMap, string nombreCapaPoligonos, string nombreCapa, IPoint puntoOrigen, IPoint puntoOpuesto, bool filasColumnas, int vertical, int horizontal, bool selectable, string capaZonificacion)
    {
        this.targetMap = targetMap;
        this.nombreCapaPoligonos = nombreCapaPoligonos;
        this.nombreCapa = nombreCapa;
        this.puntoOrigen = puntoOrigen;
        this.puntoOpuesto = puntoOpuesto;
        this.filasColumnas = filasColumnas;
        this.vertical = vertical;
        this.horizontal = horizontal;
        this.selectable = selectable;
        this.capaZonificacion = capaZonificacion;
    }

    public IMap getTargetMap()
    {
        return this.targetMap;
    }

    public string getNombreCapaPoligonos()
    {
        return this.nombreCapaPoligonos;
    }

    public string getNombreCapa()
    {
        return this.nombreCapa;
    }

    public IPoint getPuntoOrigen()
    {
        return this.puntoOrigen;
    }

    public IPoint getPuntoOpuesto()
    {
        return this.puntoOpuesto;
    }

    public bool getFilasColumnas()
    {
        return this.filasColumnas;
    }

    public int getVertical()
    {
        return this.vertical;
    }

    public int getHorizontal()
    {
        return this.horizontal;
    }

    public bool getSelectable()
    {
        return this.selectable;
    }

    public string getCapaZonificacion()
    {
        return this.capaZonificacion;
    }
}
