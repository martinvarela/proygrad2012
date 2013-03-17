using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Punto
{
    private Coordenada coordenada;

    internal Coordenada Coordenada
    {
        get { return coordenada; }
        set { coordenada = value; }
    }
    private double valor;

    public double Valor
    {
        get { return valor; }
        set { valor = value; }
    }


}
