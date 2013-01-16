using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

class Controlador
{
    //atributos
    private Zonificacion zonificacion { get; set; }
    private List<Capa> capas;
    private List<PuntoMuestreo> puntosMuestreo;
    private Muestreo muestreo { get; set; }
    private Blackmore blackmore;

    private float mediaCapas { get; set; }

    private void setearMedias() 
    {
        this.mediaCapas = 0;

        if (this.capas != null)
        {
            float temp = 0;
            int cant = 0;
            foreach (Entrada entrada in this.capas)
            {
                temp =+ entrada.setearMedias();
                cant++;
            }

            if (cant > 0)
                this.mediaCapas = temp / cant;
        }
    }
    private void crearPuntosMuestreo() { }

    public Muestreo muestreoOptimo(String rutaEntrada, int filas, int columnas)
    {
      
        Zonificacion zonificacion = new Zonificacion(rutaEntrada);

        ///paso 3
        ///

        //paso 4
        List<PuntoZonificacion> pts = zonificacion.PuntosZonificacion;
        
        //paso 5
        for (int i = 0; i < this.puntosMuestreo.Count; i++)
        {
            PuntoMuestreo pm = this.puntosMuestreo[i];
            pm.calcularValor(pts);
        }

            return new Muestreo();
    }
    public Muestreo muestreoOptimo(File entrada, float alto, float ancho)
    {
        //HACER!!!
        return new Muestreo();
    }
    public void crearBlackmore(int filas, int columnas)
    {
        this.blackmore = new Blackmore(filas, columnas);
        List<Celda> celdas = this.blackmore.Celdas;
        foreach (Celda c in celdas)
        {
            foreach (Entrada e in this.capas)
            {
                DTDatosDM datosCelda = e.calcularDsYMedia(c);
                this.blackmore.setDatos(c, datosCelda);
            }
        }
    }
    public void crearBlackmore(float alto, float ancho)
    {
        this.blackmore = new Blackmore(alto, ancho);
        List<Celda> celdas = this.blackmore.Celdas;
        foreach (Celda c in celdas)
        {
            foreach (Entrada e in this.capas)
            {
                DTDatosDM datosCelda = e.calcularDsYMedia(c);
                this.blackmore.setDatos(c, datosCelda);
            }
        }
    }


}
