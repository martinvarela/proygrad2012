using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

class DTPSimulatedAnnealing
{
    private IFeatureClass capaPuntosMuestreo;
    private string metodoInterpolacion;
    private double expIDW;
    private double error;
    private string pathArchivo;

    public DTPSimulatedAnnealing(IFeatureClass capaPuntosMuestreo, string metodoInterpolacion, double expIDW, double error, string pathArchivo)
    { 
        this.capaPuntosMuestreo = capaPuntosMuestreo;
        this.metodoInterpolacion = metodoInterpolacion;
        this.expIDW = expIDW;
        this.error = error;
        this.pathArchivo = pathArchivo;
    }

    public IFeatureClass getCapaPuntosMuestreo()
    {
        return this.capaPuntosMuestreo;
    }
    public void setCapaPuntosMuestreo(IFeatureClass f)
    {
        this.capaPuntosMuestreo = f;
    }

    public string getMetodoInterpolacion()
    {
        return this.metodoInterpolacion;
    }
    public void setMetodoInterpolacion(string s)
    {
        this.metodoInterpolacion = s;
    }

    public double getExpIDW()
    {
        return this.expIDW;
    }
    public void setExpIDW(double e)
    {
        this.expIDW = e;
    }

    public double getError()
    {
        return this.error;
    }
    public void setError(double e)
    {
        this.error = e;
    }

    public string getPathArchivo()
    {
        return this.pathArchivo;
    }
    public void setPathArchivo(string p)
    {
        this.pathArchivo = p;
    }

}
