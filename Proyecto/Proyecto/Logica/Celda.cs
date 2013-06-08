//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;


class Celda
{
    private int fid;
    private double desviacion;
    private double media;
    private int clasificacion;

    public Celda() { }

    //1
    public void setFID(int f)
    {
        this.fid = f;
    }
    public int getFID()
    {
        return this.fid;
    }
    
    //2
    public void setDesviacion(double d)
    {
        this.desviacion = d;
    }
    public double getDesviacion()
    {
        return this.desviacion;
    }
    
    //3
    public void setMedia(double m)
    {
        this.media = m;
    }
    public double getMedia()
    {
        return this.media;
    }

    public int getClasificacion()
    {
        return this.clasificacion;
    }

    //4
    // 1-Bajo rendimiento y Estable  
    // 2-Bajo rendimiento y Inestable  
    // 3-Alto rendimiento y Inestable 
    // 4-Alto rendimiento y Estable 
    public void clasificar(double parametroDST, double mediaGeneral)
    {
        double cv = this.desviacion / mediaGeneral;
        if (cv <= parametroDST)
        {
            //es estable
            if (this.media <= mediaGeneral)
            {
                //es estable y bajo rendimiento
                this.clasificacion = 1;
            }
            else 
            {
                //es estable y alto rendimiento
                this.clasificacion = 4;
            }
        }
        else
        {
            //es inestable
            if (this.media <= mediaGeneral)
            {
                //es inestable y bajo rendimiento
                this.clasificacion = 2;
            }
            else 
            {
                //es inestable y alto rendimiento
                this.clasificacion = 3;
            }
        }
    }

    public void setDatos(DTDatosDM datos)
    {
        this.desviacion = datos.getDesviacion();
        this.media = datos.getMedia();
    }

}
