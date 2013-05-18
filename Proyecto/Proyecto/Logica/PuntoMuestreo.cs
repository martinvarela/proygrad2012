using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PuntoMuestreo : Punto
{
    public PuntoMuestreo()
    {
        this.setCoordenada(new Coordenada());
    }

    public void setCoordenadaXY(double x, double y)
    {
        Coordenada c = this.getCoordenada();
        c.setX(x);
        c.setY(y);
    }
}
