using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Entrada : Capa
{
    private String nombreAtributo;
    private double media;
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
                temp += puntoCapa.getValor();
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
            temp += Math.Pow((p.getValor() - this.media),2);
            cant++;
        }
        DTDatosDM datosCelda = new DTDatosDM((double)(temp / cant), this.media);
        return datosCelda;

    }
    public List<PuntoCapa> interseccion(Celda celda)
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

    public void setNombreAtributo(String s)
    {
        this.nombreAtributo = s;
    }
    public String getNombreAtributo()
    {
        return this.nombreAtributo;
    }

}
