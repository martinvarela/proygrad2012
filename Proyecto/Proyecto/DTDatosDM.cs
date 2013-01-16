using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class DTDatosDM
{

    private float desviacion;
    public float Desviacion
    {
        get { return desviacion; }
        set { desviacion = value; }
    }
    private float media;
    public float Media
    {
        get { return media; }
        set { media = value; }
    }

    public DTDatosDM() { }

    public DTDatosDM(float desviacion, float media)
    {
        this.desviacion = desviacion;
        this.media = media;
    }

        
}
