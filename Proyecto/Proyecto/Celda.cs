using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Celda
{
    //private Coordenada ubicacion;
    private double desviacion;
    private double media;
    private int clasificacion;
    //nico
    //nico2
    public Celda() { }

    public void setDatos(DTDatosDM datos)
    {
        this.desviacion = datos.Desviacion;
        this.media = datos.Media;
    }

}
