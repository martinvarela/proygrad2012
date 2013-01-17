using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Celda
{
    //private Coordenada ubicacion;
    private float desviacion;
    private float media;
    private int clasificacion;
    //nico
    public Celda() { }

    public void setDatos(DTDatosDM datos)
    {
        this.desviacion = datos.Desviacion;
        this.media = datos.Media;
    }

}
