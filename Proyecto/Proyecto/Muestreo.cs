using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Muestreo
{
    private String nombre { get; set; }
    private String ruta { get; set; }
    //private SistemaCoordenada sistemaCoordinada { get; set; }
    private List<PuntoMuestreo> puntosMuestreo;

    internal List<PuntoMuestreo> PuntosMuestreo
    {
        get { return puntosMuestreo; }
        set { puntosMuestreo = value; }
    }

    public void agregarPuntoMuestreo(PuntoMuestreo puntoMuestreo)
    {
        if (this.puntosMuestreo == null)
            this.puntosMuestreo = new List<PuntoMuestreo>();

        this.puntosMuestreo.Add(puntoMuestreo);
    }

    public void sacarPuntoMuestreo(PuntoMuestreo puntoMuestreo)
    {
        if (this.puntosMuestreo != null)
            this.puntosMuestreo.Remove(puntoMuestreo);
    }


}

