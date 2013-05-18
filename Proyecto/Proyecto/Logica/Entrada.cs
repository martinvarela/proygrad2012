using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

class Entrada : Capa
{
    private string nombreAtributo; //1
    private IFeatureClass capaUnion; //2
    private ILayer layerCapa; //3
    private bool esCapaBase; //4
    private double media; //5
    private int indice; //6

    //1
    public void setNombreAtributo(String s)
    {
        this.nombreAtributo = s;
    }
    public string getNombreAtributo()
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
        int indJoinCount = this.capaUnion.FindField("Join_Count");
        int cantEnCelda;
        IFeature datosCeldas = cursorCeldas.NextFeature();
        while (datosCeldas != null)
        {
            cantEnCelda = (int)datosCeldas.get_Value(indJoinCount);
            if (cantEnCelda > 0)
            {
                mediaAux += (double)datosCeldas.get_Value(indice);
                cant++;                
            }
            datosCeldas = cursorCeldas.NextFeature();
        }
        if (cant > 0)
            this.media = mediaAux / cant;
        else
            this.media = -1;
        System.Runtime.InteropServices.Marshal.ReleaseComObject(cursorCeldas);

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
        double resultado = -1;
        IQueryFilter queryFilter = new QueryFilterClass();
        queryFilter.WhereClause = "FID = " + fid.ToString();
        IFeatureCursor featureCursor = this.capaUnion.Search(queryFilter, false);
        IFeature selPuntosFeature = featureCursor.NextFeature();
        if (selPuntosFeature != null)
        {
            resultado = (double)selPuntosFeature.get_Value(selPuntosFeature.Fields.FindField("merge_" + this.indice.ToString()));
        }

        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
        return resultado;
    }

}
