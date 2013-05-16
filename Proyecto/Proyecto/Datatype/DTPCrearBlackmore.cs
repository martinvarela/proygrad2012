using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class DTPCrearBlackmore
{
    private bool filasColumnas;
    private int vertical;
    private int horizontal;
    private List<DTCapasBlackmore> capasDT;
    private double dst;
    private string nombreCapaBlackmore;
    private string rutaCapaBlackmore;

    public DTPCrearBlackmore(bool filasColumnas, int vertical, int horizontal, List<DTCapasBlackmore> capasDT, double dst, string nombreCapaBlackmore, string rutaCapaBlackmore)
    {
        this.filasColumnas = filasColumnas;
        this.vertical = vertical;
        this.horizontal = horizontal;
        this.capasDT = capasDT;
        this.dst = dst;
        this.nombreCapaBlackmore = nombreCapaBlackmore;
        this.rutaCapaBlackmore = rutaCapaBlackmore;
    }

    public bool getFilasColumnas()
    {
        return this.filasColumnas;
    }

    public int getVertical()
    {
        return this.vertical;
    }
    public int getHorizontal()
    {
        return this.horizontal;
    }
    public List<DTCapasBlackmore> getCapasDT()
    {
        return this.capasDT;
    }
    public double getDst()
    {
        return this.dst;
    }
    public string getNombreCapaBlackmore()
    {
        return this.nombreCapaBlackmore;
    }
    public string getRutaCapaBlackmore()
    {
        return this.rutaCapaBlackmore;
    }
    
}
