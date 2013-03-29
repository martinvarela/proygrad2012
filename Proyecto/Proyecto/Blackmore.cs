using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Blackmore : Capa
{
    private int ancho;
    private int alto;
    private int filas;
    private int columnas;
    private float parametroDST { get; set; }
    private List<Celda> celdas;

    public List<Celda> getCeldas()
    {
        return this.celdas;
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


    public Blackmore(bool filasColumnas, int vertical, int horizontal)
    {
        if (filasColumnas)
        {
            this.filas = vertical;
            this.columnas = horizontal;
        }
        else
        {
            this.ancho = horizontal;
            this.alto = vertical;
        }
    }

}
