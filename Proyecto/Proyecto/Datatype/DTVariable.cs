using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class DTVariable
{

    private string nombre;
    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }
    private double media;

    public double Media
    {
        get { return media; }
        set { media = value; }
    }

    public DTVariable() { }
        
    public DTVariable(String nombre, double media)
    {
        this.nombre = nombre;
        this.media = media;
    }

        
}
