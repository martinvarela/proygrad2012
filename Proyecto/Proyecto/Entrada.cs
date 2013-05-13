using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

class Entrada : Capa
{
    private String nombreAtributo; //1
    private IFeatureClass capaUnion; //2
    private ILayer layerCapa; //3
    private bool esCapaBase; //4
    private double media; //5
    private int indice; //6
    //private List<PuntoCapa> puntosCapa;


    public DTDatosDM calcularDsYMedia(Celda celda)
    {
        List<PuntoCapa> puntos = this.interseccion(celda);
        double temp = 0;
        int cant = 0;
        foreach (PuntoCapa p in puntos)
        {
            temp += Math.Pow((p.getValor() - this.media),2);
            cant++;
        }
        DTDatosDM datosCelda = new DTDatosDM((double)(temp / cant), this.media);
        return datosCelda;

    }
    public List<PuntoCapa> interseccion(Celda celda)
    {
        //:HACER!!

        return new List<PuntoCapa>();
    }
    //public void agregarPuntoCapa(PuntoCapa puntoCapa)

    //{
    //    if (this.puntosCapa == null)
    //        this.puntosCapa = new List<PuntoCapa>();

    //    this.puntosCapa.Add(puntoCapa);
    //}

    //1
    public void setNombreAtributo(String s)
    {
        this.nombreAtributo = s;
    }
    public String getNombreAtributo()
    {
        return this.nombreAtributo;
    }

    //2
    public void setCapaUnion(IFeatureClass l)
    {
        this.capaUnion = l;
        //calcula la media de la capa
        this.calcularMedia();
    }
    public IFeatureClass getCapaUnion()
    {
        return this.capaUnion;
    }

    //3
    public void setLayerCapa(ILayer l)
    {
        this.layerCapa = l;
    }
    public ILayer getLayerCapa()
    {
        return this.layerCapa;
    }

    //4
    public void setEsCapaBase(bool b)
    {
        this.esCapaBase = b;
    }
    public bool getEsCapaBase()
    {
        return this.esCapaBase;
    }

    //5
    public void calcularMedia()
    {
        double mediaAux = 0;
        int cant = 0;
        IFeatureCursor cursorCeldas = this.capaUnion.Search(null, false);
        int indice = this.capaUnion.FindField("merge_" + this.indice.ToString());
        IFeature datosCeldas = cursorCeldas.NextFeature();
        while (datosCeldas != null)
        {
            mediaAux += (double)datosCeldas.get_Value(indice);
            cant++;
            datosCeldas = cursorCeldas.NextFeature();
        }
        if (cant > 0)
            this.media = mediaAux / cant;
        else
            this.media = -1;

    }

    public double getMedia()
    {
        return this.media;
    }

    //6
    public void setIndice(int i)
    {
        this.indice = i;
    }
    public int getIndice()
    {
        return this.indice;
    }



    //retorno el valor de la celda 'fid'.
    //busco en la tabla de union el registro cuyo FID sea 'fid' y devuelvo el valor del campo 'Entrada.nombreAtributo'
    public double getValorCelda(int fid)
    {
        IQueryFilter queryFilter = new QueryFilterClass();
        queryFilter.WhereClause = "FID = " + fid.ToString();
        IFeatureCursor featureCursor = this.capaUnion.Search(queryFilter, false);
        IFeature selPuntosFeature = featureCursor.NextFeature();
        if (selPuntosFeature != null)
        {
            return (double)selPuntosFeature.get_Value(selPuntosFeature.Fields.FindField("merge_"+this.indice.ToString()));
        }
        else 
        {
            return -1;
        }
    }
}
