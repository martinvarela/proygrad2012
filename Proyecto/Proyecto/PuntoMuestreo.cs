using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PuntoMuestreo : Punto
{

    public void calcularValor(List<PuntoZonificacion> pts)
    {
        //paso 1
        //intersectar hace una "seleccion" entre los puntos de 'pts' y se queda con aquellos q esten dentro del punto de muestreo.
        List<PuntoZonificacion> psz = this.intersectar(pts);

        //paso 2
        float temp = 0;
        int cant = 0;
        for (int i = 0; i < psz.Count; i++)
        {
            //paso 3y4
            temp += psz[i].Variabilidad;
            cant++;
        }
        //paso 5
        this.Valor = temp / cant;

    }

    private List<PuntoZonificacion> intersectar(List<PuntoZonificacion> pts)
    {

        throw new NotImplementedException();
    }
}
