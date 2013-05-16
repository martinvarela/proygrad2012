using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Carto;
using Proyecto;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Geoprocessor;
using System.Globalization;



class Zonificacion
{
    //public SSA ssa;
 
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

    public int getTamanoCelda()
    {
        return tamanoCelda;
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

    private ESRI.ArcGIS.Geometry.IPoint puntoOrigen;
    public ESRI.ArcGIS.Geometry.IPoint PuntoOrigen
    {
        get { return puntoOrigen; }
        set { puntoOrigen = value; }
    }
    
    private ESRI.ArcGIS.Geometry.IPoint puntoOpuesto;
    public ESRI.ArcGIS.Geometry.IPoint PuntoOpuesto
    {
        get { return puntoOpuesto; }
        set { puntoOpuesto = value; }
    }

    public Zonificacion(String rutaEntrada, List<int> variables_seleccion, System.Windows.Forms.ProgressBar pBar)
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


        //se setea el separador decimal ',' para una correcta lectura del archivo ZF
        if (!CultureInfo.CurrentCulture.IsReadOnly)
            CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";

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
            else if (((sLine != "") && (sLine.Length >= string_Yinicial.Length) && sLine.Substring(0, string_Yinicial.Length) == string_Yinicial))
            {
                yinicial = Double.Parse(sLine.Substring(string_Yinicial.Length, sLine.Length - string_Yinicial.Length));
                this.coordenadaInicial.Y = yinicial;
            }
            else if (((sLine != "") && (sLine.Length >= string_CellSize.Length) && sLine.Substring(0, string_CellSize.Length) == string_CellSize))
            {
                cellSize = Int32.Parse(sLine.Substring(string_CellSize.Length, sLine.Length - string_CellSize.Length));
                this.tamanoCelda = cellSize;
            }
            else if (((sLine != "") && (sLine.Length >= string_cant_variables.Length) && sLine.Substring(0, string_cant_variables.Length) == string_cant_variables))
            {
                cant_variables = Int32.Parse(sLine.Substring(string_cant_variables.Length, sLine.Length - string_cant_variables.Length));
                int i = 1;
                sLine = objReader.ReadLine();
                String nombreVariable = "";

                int p = 0;
                while (i <= cant_variables && sLine != "" && p < variables_seleccion.Count)
                {
                    if ((i - 1) == variables_seleccion[p]) //es i-1 porque en el archivo ZF el i comienza en 1 y la seleccion de variable comienza en 0
                    {
                        String aux = "Var" + i + ": ";
                        if ((sLine.Substring(0, aux.Length) == aux))
                        {
                            nombreVariable = sLine.Substring(aux.Length, sLine.Length - aux.Length);
                            Variable variable = new Variable(nombreVariable);
                            this.agregarVariable(variable);
                        }
                        p++;
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

        //se setea el puntoOrigen
        this.puntoOrigen = new ESRI.ArcGIS.Geometry.PointClass();
        this.puntoOrigen.X = xinicial;
        this.puntoOrigen.Y = yinicial - Rows * cellSize;

        //se setea el puntoOpuesto
        this.puntoOpuesto = new ESRI.ArcGIS.Geometry.PointClass();
        this.puntoOpuesto.X = xinicial + Cols * cellSize;
        this.puntoOpuesto.Y = yinicial;

        //seteo el progressBar
        pBar.Minimum = 1;
        pBar.Maximum = Rows * Cols;
        pBar.Value = 1;
        pBar.Step = 1;
        pBar.Visible = true;

        //comienza el proceso de los puntos de la zonificacion
        for (int iy = 1; iy <= this.Filas; iy++)
        {
            for (int ix = 1; ix <= this.Columnas; ix++)
            {
                sLine = objReader.ReadLine();

                PuntoZonificacion ptoZonificacion = new PuntoZonificacion();
                ptoZonificacion.Coordenada = calcularCoordenada(this.coordenadaInicial, this.tamanoCelda, ix - 1, iy - 1);
                ptoZonificacion.Variables = this.Variables;

                char[] coma = { ';' };
                string[] datos = null;
                datos = sLine.Split(coma);
                bool puntoUtil = true;

                for (int i = 0; i < variables_seleccion.Count; i++)
                {
                    if (datos[variables_seleccion[i]] == NAN || datos[variables_seleccion[i]] == "0" || datos[variables_seleccion[i]] == "-1")
                    {
                        puntoUtil = false;
                        break;
                    }
                    else
                        ptoZonificacion.agregarDato(ptoZonificacion.Variables[i].Nombre, float.Parse(datos[variables_seleccion[i]]));
                }
                //agrego el punto solo si tiene datos
                if (puntoUtil)
                    this.agregarPuntoZonificacion(ptoZonificacion);

                //actualizo el progressBar.
                pBar.PerformStep();
            }
        }

        //cierro el archivo
        objReader.Close();

        //cierro el progressBar
        pBar.Visible = false;

    }

    public void calcularVariabilidad(System.Windows.Forms.ProgressBar pBar)
    {
        //inicializo el progressBar
        pBar.Minimum = 1;
        pBar.Maximum = this.variables.Count * this.puntosZonificacion.Count;
        pBar.Step = 1;
        pBar.Value = 1;
        pBar.Visible = true;
        for (int i = 0; i < this.Variables.Count; i++)
        {
            this.Variables[i].calcularMedia(this.puntosZonificacion);
            pBar.PerformStep();
        }
        for (int i = 0; i < this.puntosZonificacion.Count; i++)
        {
            this.puntosZonificacion[i].calcularVariabilidad();
            pBar.PerformStep();
        }
        pBar.Visible = false;

        //double cuadX = (this.puntosZonificacion[10].Coordenada.X - this.puntosZonificacion[2800].Coordenada.X) * (this.puntosZonificacion[10].Coordenada.X - this.puntosZonificacion[2800].Coordenada.X);
        //double cuadY = (this.puntosZonificacion[10].Coordenada.Y - this.puntosZonificacion[2800].Coordenada.Y) * (this.puntosZonificacion[10].Coordenada.Y - this.puntosZonificacion[2800].Coordenada.Y);
        //double distancia = Math.Sqrt(cuadX + cuadY);
        //MessageBox.Show("distancia" + distancia.ToString());
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

    //Dada la coordenada inicial y el tamamio de la celda, calculamos la siguiente coordenada en base al x e y actual (desplazamiento en celdas
    //desde la posicion (x,y)=(0,0) hasta la posicion actual x,y pasada por parametro)
    private Coordenada calcularCoordenada(Coordenada coordenadaInicial, int TamanoCelda, int x, int y)
    {
        Coordenada coordenada = new Coordenada();
        coordenada.X = coordenadaInicial.X + TamanoCelda * x;
        coordenada.Y = coordenadaInicial.Y - TamanoCelda * y;
        return coordenada;

    }

    //Calcula la distancia entre dos puntos y devuelve la distancia en metros luego de redondear el resultado
    private int calcularDistancia(int i, int j)
    {
        double cuadX = (this.puntosZonificacion[i].Coordenada.X - this.puntosZonificacion[j].Coordenada.X) * (this.puntosZonificacion[i].Coordenada.X - this.puntosZonificacion[j].Coordenada.X);
        double cuadY = (this.puntosZonificacion[i].Coordenada.Y - this.puntosZonificacion[j].Coordenada.Y) * (this.puntosZonificacion[i].Coordenada.Y - this.puntosZonificacion[j].Coordenada.Y);
        double distancia = Math.Sqrt(cuadX + cuadY);
        return (int)Math.Round(distancia);
    }

}
