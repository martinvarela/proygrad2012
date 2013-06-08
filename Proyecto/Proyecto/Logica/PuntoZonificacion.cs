//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Collections;

using System.Collections;
using System.Collections.Generic;
class PuntoZonificacion
{
    private Coordenada coordenada;
    private double variabilidad;
    private Hashtable hashDato;
    private List<Variable> variables;

    public Coordenada getCoordenada()
    {
       return this.coordenada;
    }
    public void setCoordenada(Coordenada c)
    {
        this.coordenada = c;
    }

    public double getVariabilidad()
    {
        return this.variabilidad;
    }
    public void setVariabilidad(double d)
    {
        this.variabilidad = d;
    }    
    
    public List<Variable> getVariables()
    {
        return this.variables;
    }
    public void setVariables(List<Variable> l)
    {
        this.variables = l;
    }    

    public PuntoZonificacion()
    { }

    public void calcularVariabilidad()
    {
        DTVariable datosVariable;
        double medicion =0;
        double temp = 0;
        int cant = 0;

        //mas de 1 variable, se normalizan los valores
        if (this.getVariables().Count > 1)
        {
            for (int i = 0; i < this.getVariables().Count; i++)
            {
                datosVariable = this.getVariables()[i].getDatos();
                medicion = (double)this.hashDato[datosVariable.Nombre.ToString()];
                temp += (double)(medicion / datosVariable.Media);
                cant++;
            }
            this.variabilidad = (double)(temp / cant);
        }
        else
        {   //una sola variable, no se normaliza
            datosVariable = this.getVariables()[0].getDatos();
            medicion = (double)this.hashDato[datosVariable.Nombre.ToString()];
            this.variabilidad = medicion; 
        }

    }
    
    public double getMedicion(string nombre)  
    {
        return (double)this.hashDato[nombre.ToString()];
    }

    public void agregarDato(string nombreVariable, double dato)
    {
        if (this.hashDato == null)
            this.hashDato = new Hashtable();

        this.hashDato.Add(nombreVariable, dato);
    }

    public void agregarVariable(Variable variable)
    {
        if (this.variables == null)
            this.variables = new List<Variable>();

        this.variables.Add(variable);
    }
}
