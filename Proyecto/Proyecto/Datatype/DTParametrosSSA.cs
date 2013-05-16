using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class DTParametrosSSA
{
    private double temperaturaInicial;
    private double factorReduccion;
    private int iteraciones;

    public DTParametrosSSA(double temperaturaInicial, double factorReduccion, int iteraciones)
    {
        this.temperaturaInicial = temperaturaInicial;
        this.factorReduccion = factorReduccion;
        this.iteraciones = iteraciones;
    }

    public double getTemperaturaInicial()
    {
        return this.temperaturaInicial;
    }
    public void SetTemperaturaInicial(double temperaturaInicial)
    {
        this.temperaturaInicial = temperaturaInicial;
    }

    public double getFactorReduccion()
    {
        return this.factorReduccion;
    }
    public void setFactorReduccion(double factorReduccion)
    {
        this.factorReduccion = factorReduccion;
    }
    
    public int getIteraciones()
    {
        return this.iteraciones;
    }
    public void setIteraciones(int iteraciones)
    {
        this.iteraciones = iteraciones;
    }
}
