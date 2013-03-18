using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

class PuntoZonificacion
{
    private Coordenada coordenada;
    internal Coordenada Coordenada
    {
        get { return coordenada; }
        set { coordenada = value; }
    }
    private float variabilidad;
    public float Variabilidad
    {
        get { return variabilidad; }
        set { variabilidad = value; }
    }
    private Hashtable hashDato;
    private List<Variable> variables;
    internal List<Variable> Variables
    {
        get { return variables; }
        set { variables = value; }
    }
    private bool util;
    public bool Util
    {
        get { return util; }
        set { util = value; }
    }
    public PuntoZonificacion() { }
    
    public void calcularVariabilidad()
    {
        DTVariable datosVariable;
        float medicion =0;
        float temp = 0;
        int cant = 0;
       // for (int i = 0; i < this.Variables.Count; i++)
        for (int i = 3; i < 4 ; i++)
        {
            datosVariable = this.Variables[i].getDatos();
            medicion = (float)this.hashDato[datosVariable.Nombre.ToString()];
            temp =+ (float)Math.Pow((double)((medicion - datosVariable.Media) / datosVariable.Media),2);
            cant++;
        }
        this.variabilidad = temp / cant;
    }
    
    public float getMedicion(String nombre)  
    {
        return (float)this.hashDato[nombre.ToString()];
    }

    public void agregarDato(String nombreVariable, float dato)
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
