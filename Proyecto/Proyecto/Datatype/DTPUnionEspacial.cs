using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

class DTPUnionEspacial
{
    private IFeatureClass entidadDestino;
    private ILayer entidadUnion;
    private string entidadSalida;
    private bool mantenerEntidades;
    private string nombreAtributo;
    private string atributoTablaUnion;

    public DTPUnionEspacial(IFeatureClass entidadDestino, ILayer entidadUnion, string entidadSalida, bool mantenerEntidades, string nombreAtributo, string atributoTablaUnion) 
    {
        this.entidadDestino = entidadDestino;
        this.entidadUnion = entidadUnion;
        this.entidadSalida = entidadSalida;
        this.mantenerEntidades = mantenerEntidades;
        this.nombreAtributo = nombreAtributo;
        this.atributoTablaUnion = atributoTablaUnion;
    }

    public IFeatureClass getEntidadDestino()
    {
        return this.entidadDestino;
    }

    public ILayer getEntidadUnion()
    {
        return this.entidadUnion;
    }

    public string getEntidadSalida()
    {
        return this.entidadSalida;
    }

    public bool getMantenerEntidades()
    {
        return this.mantenerEntidades;
    }

    public string getNombreAtributo()
    {
        return this.nombreAtributo;
    }

    public string getAtributoTablaUnion()
    {
        return this.atributoTablaUnion;
    }
}
