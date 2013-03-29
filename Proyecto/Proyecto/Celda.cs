using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Celda
{
    private double desviacion;
    private double media;
    private int clasificacion;

    public Celda() { }

    public void setDatos(DTDatosDM datos)
    {
        this.desviacion = datos.Desviacion;
        this.media = datos.Media;
    }
    public void setClasificacion(int c)
    {
        this.clasificacion = c;
    }
    public int getClasificacion()
    {
        return this.clasificacion;
    }
}
