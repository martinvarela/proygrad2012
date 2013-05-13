using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;


class DTCapasBlackmore
{

    private ILayer capa;
    private string nombreCapa;
    private bool capaBase;
    private List<string> listaAtributos; 

    public DTCapasBlackmore() { }

    public ILayer getCapa()
    {
        return this.capa;
    }
    public void setCapa(ILayer c)
    {
        this.capa = c;
    }

    public bool esCapaBase()
    {
        return this.capaBase;
    }
    public void setCapaBase(bool b)
    {
        this.capaBase = b;
    }

    public string getNombreCapa()
    {
        return this.nombreCapa;
    }
    public void setNombreCapa(string n)
    {
        this.nombreCapa = n;
    }

    public List<string> getListaAtributos()
    {
        return this.listaAtributos;
    }
    public void agregarAtributo(string atributo)
    {
        if (this.listaAtributos == null)
            this.listaAtributos = new List<string>();

        this.listaAtributos.Add(atributo);
    }
    
}
