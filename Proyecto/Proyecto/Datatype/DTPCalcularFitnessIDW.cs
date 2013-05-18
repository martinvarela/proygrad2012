using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

class DTPCalcularFitnessIDW
{
    private double expIDW;
    private IFeatureClass capaPuntosMuestreo;
    private List<PuntoMuestreo> listaPuntosMuestreo;
    private List<int> listaIndicesMuestras;

    public DTPCalcularFitnessIDW(List<PuntoMuestreo> listaPuntosMuestreo, List<int> listaIndicesMuestras, IFeatureClass capaPuntosMuestreo, double expIDW) 
    {
        this.listaPuntosMuestreo = listaPuntosMuestreo;
        this.listaIndicesMuestras = listaIndicesMuestras;
        this.capaPuntosMuestreo = capaPuntosMuestreo;
        this.expIDW = expIDW; 
    }

    public double getExpIDW()
    {
        return this.expIDW;
    }
    public void setExpIDW(double e)
    {
        this.expIDW =e;
    }

    public IFeatureClass getCapaPuntosMuestreo()
    {
        return this.capaPuntosMuestreo;
    }
    public void setCapaPuntosMuestreo(IFeatureClass f)
    {
        this.capaPuntosMuestreo = f;
    }

    public List<PuntoMuestreo> getListaPuntosMuestreo()
    {
        return this.listaPuntosMuestreo;
    }
    public void setListaPuntosMuestreo(List<PuntoMuestreo> l)
    {
        this.listaPuntosMuestreo = l;
    }

    public List<int> getListaIndicesMuestras()
    {
        return this.listaIndicesMuestras;
    }
    public void setListaIndicesMuestras(List<int> l)
    {
        this.listaIndicesMuestras = l;
    }


}
