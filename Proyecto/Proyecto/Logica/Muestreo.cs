//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;


using System.Collections.Generic;
class Muestreo
{
    private List<PuntoMuestreo> puntosMuestreo;

    public List<PuntoMuestreo> getPuntosMuestreo()
    {
        return this.puntosMuestreo;
    }
    public void setPuntosMuestreo(List<PuntoMuestreo> l)
    {
        this.puntosMuestreo = l;
    }

    public void agregarPuntoMuestreo(PuntoMuestreo puntoMuestreo)
    {
        if (this.puntosMuestreo == null)
            this.puntosMuestreo = new List<PuntoMuestreo>();

        this.puntosMuestreo.Add(puntoMuestreo);
    }
}

