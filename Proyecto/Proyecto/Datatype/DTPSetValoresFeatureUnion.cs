using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

class DTPSetValoresFeatureUnion
{
    private IFeatureClass featureUnion;
    private int fid;
    private int indiceDst;
    private double dsv;
    private int indiceMean;
    private double mean;
    private int indiceClasificacion;
    private int clase;
    
    public DTPSetValoresFeatureUnion(IFeatureClass featureUnion, int fid, int indiceDst, double dsv, int indiceMean, double mean, int indiceClasificacion, int clase)
    {
        this.featureUnion = featureUnion;
        this.fid = fid;
        this.indiceDst = indiceDst;
        this.dsv = dsv;
        this.indiceMean = indiceMean;
        this.mean = mean;
        this.indiceClasificacion = indiceClasificacion;
        this.clase = clase;
    }

    public IFeatureClass getFeatureUnion()
    {
        return this.featureUnion;
    }

    public int getFid()
    {
        return this.fid;
    }

    public int getIndiceDst()
    {
        return this.indiceDst;
    }

    public double getDsv()
    {
        return this.dsv;
    }

    public int getIndiceMean()
    {
        return this.indiceMean;
    }

    public double getMean()
    {
        return this.mean;
    }

    public int getIndiceClasificacion()
    {
        return this.indiceClasificacion;
    }

    public int getClase()
    {
        return this.clase;
    }
}
