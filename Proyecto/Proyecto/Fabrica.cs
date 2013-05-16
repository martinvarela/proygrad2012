using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Fabrica
{
    private static Fabrica instancia;
    private Fabrica()
    { }

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
        return new BlackmoreControlador();
    }
    public IMuestreo getIMuestreo()
    {
        return new MuestreoControlador();
    }
    public IOptimizar getIOptimizar()
    {
        return new OptimizarControlador();
    }
}
