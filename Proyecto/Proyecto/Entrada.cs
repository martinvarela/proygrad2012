using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//esto es una linea para probar el gittt
//linea que pongo yo (Gonzalo)
//esta linea la meto yo (martin)
class Entrada : Capa
{
    private String nombreAtributo { get; set; }
    private double media { get; set; }
    private List<PuntoCapa> puntosCapa;

    public double setearMedias()
    {
        this.media = 0;
        if (this.puntosCapa != null)
        {
            double temp = 0;
            int cant = 0;

            foreach (PuntoCapa puntoCapa in this.puntosCapa)
            {
                temp += puntoCapa.Valor;
                cant++;
            }

            if (cant > 0)
                this.media = temp / cant;
        }

        return this.media;
    }

    public DTDatosDM calcularDsYMedia(Celda celda)
    {
        List<PuntoCapa> puntos = this.interseccion(celda);
        double temp = 0;
        int cant = 0;
        foreach (PuntoCapa p in puntos)
        {
            temp += Math.Pow((p.Valor - this.media),2);
            cant++;
        }
        DTDatosDM datosCelda = new DTDatosDM((double)(temp / cant), this.media);
        return datosCelda;

    }
    private List<PuntoCapa> interseccion(Celda celda)
    {
        //:HACER!!

        return new List<PuntoCapa>();
    }
    public void agregarPuntoCapa(PuntoCapa puntoCapa)
    {
        if (this.puntosCapa == null)
            this.puntosCapa = new List<PuntoCapa>();

        this.puntosCapa.Add(puntoCapa);
    }


}
