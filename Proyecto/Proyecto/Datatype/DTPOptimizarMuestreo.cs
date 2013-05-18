using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

class DTPOptimizarMuestreo
{
    private IFeatureClass capaPuntosMuestreo;
    private string metodoInterpolacion;
    private double expIDW;
    private int nroMuestras;
    private double error;
    private string rutaCapa;

    public DTPOptimizarMuestreo(IFeatureClass capaPuntosMuestreo, string metodoInterpolacion, double expIDW, int nroMuestras, double error, string rutaCapa)
    { 
        this.capaPuntosMuestreo = capaPuntosMuestreo;
        this.metodoInterpolacion = metodoInterpolacion;
        this.expIDW = expIDW;
        this.nroMuestras = nroMuestras;
        this.error = error;
        this.rutaCapa = rutaCapa;
    }

    public IFeatureClass getCapaPuntosMuestreo()
    {
        return this.capaPuntosMuestreo;
    }

    public string getMetodoInterpolacion()
    {
        return this.metodoInterpolacion;
    }

    public double getExpIDW()
    {
        return this.expIDW;
    }

    public int getNroMuestras()
    {
        return this.nroMuestras;
    }

    public double getError()
    {
        return this.error;
    }

    public string getRutaCapa()
    {
        return this.rutaCapa;
    }

}
