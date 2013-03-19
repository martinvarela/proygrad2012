using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class DTDatosDM
{

    private double desviacion;
    public double Desviacion
    {
        get { return desviacion; }
        set { desviacion = value; }
    }
    private double media;
    public double Media
    {
        get { return media; }
        set { media = value; }
    }

    public DTDatosDM() { }

    public DTDatosDM(double desviacion, double media)
    {
        this.desviacion = desviacion;
        this.media = media;
    }

        
}
