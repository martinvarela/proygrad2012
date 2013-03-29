using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Punto
{
    private Coordenada coordenada;
    private double valor;

    public void setCoordenada(Coordenada c)
    {
        this.coordenada = c;
    }
    public Coordenada getCoordenada()
    {
        return this.coordenada;
    }

    public void setValor(double v)
    {
        this.valor = v;
    }
    public double getValor()
    {
        return this.valor;
    }
}
