using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


class Zonificacion
{
    private int columnas;
    public int Columnas
    {
        get { return columnas; }
        set { columnas = value; }
    }

    private int filas;
    public int Filas
    {
        get { return filas; }
        set { filas = value; }
    }

    public Coordenada coordenadaInicial;

    private int tamanoCelda;
    public int TamanoCelda
    {
        get { return tamanoCelda; }
        set { tamanoCelda = value; }
    }
    private List<Variable> variables;
    internal List<Variable> Variables
    {
        get { return variables; }
        set { variables = value; }
    }
    private List<PuntoZonificacion> puntosZonificacion;
    internal List<PuntoZonificacion> PuntosZonificacion
    {
        get { return puntosZonificacion; }
        set { puntosZonificacion = value; }
    }

    //Dada la coordenada inicial y el tamamio de la celda, calculamos la siguiente coordenada en base al x e y actual (desplazamiento en celdas
    //desde la posicion (x,y)=(0,0) hasta la posicion actual x,y pasada por parametro)
    public Coordenada calcularCoordenada(Coordenada coordenadaInicial, int TamanoCelda, int x, int y)
    {
        Coordenada coordenada = new Coordenada();
        coordenada.X = coordenadaInicial.X + TamanoCelda * x;
        coordenada.Y = coordenadaInicial.Y + TamanoCelda * y;
        return coordenada;

    }
    
    public Zonificacion(String rutaEntrada)
    {
        //Obtengo el archivo
        StreamReader objReader = new StreamReader(rutaEntrada);
        //Incicializo la variable donde voy a guardar cada linea que leo y la variable donde voy a guardar en memoria el contenido del archivo
        string sLine = "";
        int Rows = 0;
        int Cols = 0;
        double xinicial = 0;
        double yinicial = 0;
        int cellSize = 0;
        int cant_variables = 0;
        string string_rows = "Rows: ";
        string string_cols = "Cols: ";
        string string_Xinicial = "CoordX: ";
        string string_Yinicial = "CoordY: ";
        string string_CellSize = "CellSize: ";
        string string_cant_variables = "VarQty:";
        coordenadaInicial = new Coordenada();
        string NAN = "NaN";

        string comienzo_datos = "[Cells]";

        //Leo la linea actual del archivo
        sLine = objReader.ReadLine();

        //leo hasta la etiqueta [Cells] y saco los valores de rows, cols y cant_variables 
        while (sLine != null)
        {
            if (((sLine != "") && sLine.Substring(0, string_rows.Length) == string_rows))
            {
                Rows = Int32.Parse(sLine.Substring(string_rows.Length, sLine.Length - string_rows.Length));
                this.Filas = Rows;
            } 
            else if (((sLine != "") && sLine.Substring(0, string_cols.Length) == string_cols))
            {
                Cols = Int32.Parse(sLine.Substring(string_cols.Length, sLine.Length - string_cols.Length));
                this.Columnas = Cols;
            }
            else if (((sLine != "") && (sLine.Length >= string_Xinicial.Length) && sLine.Substring(0, string_Xinicial.Length) == string_Xinicial))
            {
                xinicial = Double.Parse(sLine.Substring(string_Xinicial.Length, sLine.Length - string_Xinicial.Length));
                this.coordenadaInicial.X = xinicial; 
            }
            else if (((sLine != "") && (sLine.Length >= string_Yinicial.Length)  && sLine.Substring(0, string_Yinicial.Length) == string_Yinicial))
            {
                yinicial = Double.Parse(sLine.Substring(string_Yinicial.Length, sLine.Length - string_Yinicial.Length));
                this.coordenadaInicial.Y = yinicial;
            }
            else if (((sLine != "") && (sLine.Length >= string_CellSize.Length)  && sLine.Substring(0, string_CellSize.Length) == string_CellSize))
            {
                cellSize = Int32.Parse(sLine.Substring(string_CellSize.Length, sLine.Length - string_CellSize.Length));
                this.TamanoCelda = cellSize;
            }
            else if (((sLine != "") && (sLine.Length >= string_cant_variables.Length) && sLine.Substring(0, string_cant_variables.Length) == string_cant_variables))
            {
                cant_variables = Int32.Parse(sLine.Substring(string_cant_variables.Length, sLine.Length - string_cant_variables.Length));
                int i = 1;
                sLine = objReader.ReadLine();
                String nombreVariable = "";
                while (i <= cant_variables && sLine != "")
                {
                    String aux = "Var" + i + ": ";
                    if ((sLine.Substring(0, aux.Length) == aux))
                    {
                        nombreVariable = sLine.Substring(aux.Length, sLine.Length - aux.Length);
                        Variable variable = new Variable(nombreVariable);
                        this.agregarVariable(variable);
                    }
                    i++;
                    sLine = objReader.ReadLine();
                }
            }

            //Llegue a la etiqueta [Cells] entonces se que a continuacion empiezan los valores de los puntos muestreados
            if (((sLine != "") && (sLine.Substring(0, comienzo_datos.Length) == comienzo_datos)))
                break;

            sLine = objReader.ReadLine();
        }  //fin while de datos generales

        for (int iy = 1; iy <= this.Filas; iy++)
        {
            for (int ix = 1; ix <= this.Columnas; ix++)
            {
                PuntoZonificacion ptoZonificacion = new PuntoZonificacion();
                ptoZonificacion.Coordenada = calcularCoordenada(this.coordenadaInicial, this.TamanoCelda, ix-1, iy-1);
                ptoZonificacion.Variables = this.Variables;

                sLine = objReader.ReadLine();
                char[] coma = { ';' };
                string[] datos = null;
                datos = sLine.Split(coma);
                bool puntoUtil = false;
                for (int i = 0; i < cant_variables; i++)
                {
                    float dato;
                    if (datos[i] == NAN)
                    {
                        dato = 0;
                    }
                    else
                    {
                        puntoUtil = true;
                        dato = float.Parse(datos[i]);
                    }
                    ptoZonificacion.agregarDato(ptoZonificacion.Variables[i].Nombre, dato);
                    ptoZonificacion.Util = puntoUtil;

                }
                sLine = objReader.ReadLine();
            }
        }

        //cierro el archivo
        objReader.Close();        
    }

    public void calcularVariabilidad()
    {
        for (int i=0; i < this.Variables.Count; i++) { this.Variables[i].calcularMedia(); }
        for (int i = 0; i < this.puntosZonificacion.Count; i++) { this.puntosZonificacion[i].calcularVariabilidad(); }
    }

    public void agregarVariable(Variable variable)
    {
        if (this.variables == null)
            this.variables = new List<Variable>();

        this.variables.Add(variable);
    }

    public void agregarPuntoZonificacion(PuntoZonificacion punto)
    {
        if (this.puntosZonificacion == null)
            this.puntosZonificacion = new List<PuntoZonificacion>();

        this.puntosZonificacion.Add(punto);
    }

}
