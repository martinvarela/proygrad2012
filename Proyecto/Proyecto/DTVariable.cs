using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class DTVariable
{

    private String nombre;
    public String Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }
    private float media;

    public float Media
    {
        get { return media; }
        set { media = value; }
    }

    public DTVariable() { }
        
    public DTVariable(String nombre, float media)
    {
        this.nombre = nombre;
        this.media = media;
    }

        
}
