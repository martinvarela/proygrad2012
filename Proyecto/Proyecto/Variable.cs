using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Variable
{
    private String nombre;
    public String Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }
    private float media { get; set; }
    private List<PuntoZonificacion> puntosZonificacion;
    private string nombreVariable; 

    public Variable() { }

    public Variable(string nombreVariable)
    {
        this.nombreVariable = nombreVariable;
    }

    public void calcularMedia()
    {
        float temp = 0;
        int cant = 0;
        float medicion;
        for (int i = 0; i < this.puntosZonificacion.Count; i++)
        {
            PuntoZonificacion p = this.puntosZonificacion[i];
            if (p.Util)
            {
                medicion = p.getMedicion(this.Nombre);
                temp =+ medicion;
                cant++;
            }
        }
        this.media = temp / cant;


    }
    private void actualizar() { }

    public DTVariable getDatos()
    {
        return new DTVariable(this.nombre,this.media);
    }

}
