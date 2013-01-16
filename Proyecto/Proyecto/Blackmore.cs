using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Blackmore : Capa
{
    private float ancho { get; set; }
    private float alto { get; set; }
    private int filas { get; set; }
    private int columnas { get; set; }
    private float parametroDST { get; set; }
    private List<Celda> celdas;

    internal List<Celda> Celdas
    {
        get { return celdas; }
        set { celdas = value; }
    }

    public void setDatos(Celda celda, DTDatosDM datosCelda)
    {
        celda.setDatos(datosCelda);
    }
    public void agregarCelda(Celda celda)
    {
        if (this.celdas == null)
            this.celdas = new List<Celda>();

        this.celdas.Add(celda);
    }


    public Blackmore(int filas, int columnas)
    {
        this.filas = filas;
        this.columnas = columnas;
    }

    public Blackmore(float ancho, float alto)
    {
        this.ancho = ancho;
        this.alto  = alto;
    }
}
