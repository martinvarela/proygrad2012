using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Fabrica
{
    private static Fabrica instancia;
    private static OptimizarControlador instanciaOptimizarControlador;

    private Fabrica()
    { }
    
    public static Fabrica getInstancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new Fabrica();
                instanciaOptimizarControlador = null;
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
        if (instanciaOptimizarControlador == null)
        {
            instanciaOptimizarControlador = new OptimizarControlador();
        }
        return instanciaOptimizarControlador;
    }
}
