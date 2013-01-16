﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//esto es una linea para probar el gittt
//esta linea la meto yo (martin)
class Entrada : Capa
{
    private String nombreAtributo { get; set; }
    private float media { get; set; }
    private List<PuntoCapa> puntosCapa;

    public float setearMedias()
    {
        this.media = 0;
        if (this.puntosCapa != null)
        {
            float temp = 0;
            int cant = 0;

            foreach (PuntoCapa puntoCapa in this.puntosCapa)
            {
                temp = +puntoCapa.Valor;
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
            temp =+ Math.Pow((p.Valor - this.media),2);
            cant++;
        }
        DTDatosDM datosCelda = new DTDatosDM((float)(temp / cant), this.media);
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
