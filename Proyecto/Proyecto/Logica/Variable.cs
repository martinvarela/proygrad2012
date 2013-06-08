//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using System.Collections.Generic;
class Variable
{
    private string nombre;
    private double media; //{ get; set; }
    private List<PuntoZonificacion> puntosZonificacion;

    public string getNombre()
    {
        return this.nombre;
    }
    public void setNombre(string s)
    {
        this.nombre = s;
    }
    
    public Variable()
    { }

    public Variable(string nombreVariable)
    {
        this.nombre = nombreVariable;
    }

    public void calcularMedia(List<PuntoZonificacion> listaPuntos)
    {
        double temp = 0;
        int cant = 0;
        double medicion;
        this.puntosZonificacion = listaPuntos;
        for (int i = 0; i < this.puntosZonificacion.Count; i++)
        {
            PuntoZonificacion p = this.puntosZonificacion[i];
            medicion = p.getMedicion(this.nombre);
            temp += medicion;
            cant++;
        }
        this.media = temp / cant;
    }

    public DTVariable getDatos()
    {
        return new DTVariable(this.nombre,this.media);
    }
}
