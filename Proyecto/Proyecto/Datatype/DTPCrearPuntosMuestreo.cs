using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

class DTPCrearPuntosMuestreo
{
    private bool conRed;
    private string rutaEntrada;
    private bool filasColumnas;
    private int vertical;
    private int horizontal;
    private List<int> variablesMarcadas;
    private ProgressBar pBar;
    private string rutaCapa;
    private string nombreCapa;
    private Label lblProgressBar;

    public DTPCrearPuntosMuestreo(bool conRed, string rutaEntrada, bool filasColumnas, int vertical, int horizontal, List<int> variablesMarcadas, ProgressBar pBar, string rutaCapa, string nombreCapa, Label lblProgressBar)
    {
        this.conRed = conRed;
        this.rutaEntrada = rutaEntrada;
        this.filasColumnas = filasColumnas;
        this.vertical = vertical;
        this.horizontal = horizontal;
        this.variablesMarcadas = variablesMarcadas; 
        this.pBar = pBar;
        this.rutaCapa = rutaCapa;
        this.nombreCapa = nombreCapa;
        this.lblProgressBar = lblProgressBar;
    }

    public bool getConRed()
    {
        return this.conRed;
    }
    public string getRutaEntrada()
    {
        return this.rutaEntrada;
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
    public List<int> getVariablesMarcadas()
    {
        return this.variablesMarcadas;
    }
    public ProgressBar getPBar()
    {
        return this.pBar;
    }
    public string getRutaCapa()
    {
        return this.rutaCapa;
    }
    public string getNombreCapa()
    {
        return this.nombreCapa;
    }
    public Label getLblProgressBar()
    {
        return this.lblProgressBar;
    }


}

