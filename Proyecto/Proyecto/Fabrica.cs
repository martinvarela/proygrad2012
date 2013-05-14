using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Fabrica
{
    private static Fabrica instancia;
    private IBlackmore moduloBlackmore;
    private Fabrica()
    {
        this.moduloBlackmore = new BlackmoreControlador();
    }

    public static Fabrica getInstancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new Fabrica();
            }
            return instancia;
        }
    }

    public IBlackmore getIBlackmore()
    {
        return this.moduloBlackmore;
    }
}
