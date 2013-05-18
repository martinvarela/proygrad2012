using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class DTDatosDM
{
    private double desviacion;
    private double media;
    
    public double getDesviacion()
    {
        return this.desviacion;
    }
    public void setDesviacion(double d)
    {
        this.desviacion = d;
    }

    public double getMedia()
    {
        return this.media;
    }
    public void setMedia(double m)
    {
        this.media = m;
    }

    public DTDatosDM() { }

    public DTDatosDM(double desviacion, double media)
    {
        this.desviacion = desviacion;
        this.media = media;
    }
}
