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
using ESRI.ArcGIS.Catalog;



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

    //Dada la coordenada inicial y el tamamio de la celda, calculamos la siguiente coordenada en base al x e y actual (desplazamiento en celdas
    //desde la posicion (x,y)=(0,0) hasta la posicion actual x,y pasada por parametro)
    public Coordenada calcularCoordenada(Coordenada coordenadaInicial, int TamanoCelda, int x, int y)
    {
        Coordenada coordenada = new Coordenada();
        coordenada.X = coordenadaInicial.X + TamanoCelda * x;
        coordenada.Y = coordenadaInicial.Y - TamanoCelda * y;
        return coordenada;

    }



    public Zonificacion(String rutaEntrada, int[] variables_seleccion)
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
                 // for (int i = 0; i < variables_seleccion.Length; i++){
                   // if (datos[variables_seleccion[i]] == NAN)
                int p = 0;
                while (i <= cant_variables && sLine != "" && p < variables_seleccion.Length )
                {
                    if ((i-1) == variables_seleccion[p]) //es i-1 porque en el archivo ZF el i comienza en 1 y la seleccion de variable comienza en 0
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

        //comienza el proceso de los puntos de la zonificacion
        for (int iy = 1; iy <= this.Filas; iy++)
        {
            for (int ix = 1; ix <= this.Columnas; ix++)
            {
                sLine = objReader.ReadLine(); 
                
                PuntoZonificacion ptoZonificacion = new PuntoZonificacion();
                ptoZonificacion.Coordenada = calcularCoordenada(this.coordenadaInicial, this.TamanoCelda, ix-1, iy-1);
                ptoZonificacion.Variables = this.Variables;

                char[] coma = { ';' };
                string[] datos = null;
                datos = sLine.Split(coma);
                bool puntoUtil = true;

                /*for (int i = 0; i < cant_variables; i++)
                {
                    if (datos[i] == NAN)
                    {
                        puntoUtil = false;
                        break;
                    }
                    else
                        ptoZonificacion.agregarDato(ptoZonificacion.Variables[i].Nombre, float.Parse(datos[i]));
                }*/
                for (int i = 0; i < variables_seleccion.Length; i++){
                    if (datos[variables_seleccion[i]] == NAN)
                    {
                        puntoUtil = false;
                        break;
                    }
                    else
                        ptoZonificacion.agregarDato(ptoZonificacion.Variables[i].Nombre, float.Parse(datos[variables_seleccion[i]]));                    
                }
                ptoZonificacion.Util = puntoUtil;
                //agrego el punto solo si tiene datos
                if (ptoZonificacion.Util)
                    this.agregarPuntoZonificacion(ptoZonificacion);
            }
        }

        //cierro el archivo
        objReader.Close();

       // this.calcularSemivariograma();

        
    }

    //ACA TENGO QUE PONER MI FUNCION
    /****************************************************************************************************************************************/

    //Calcula la distancia entre dos puntos y devuelve la distancia en metros luego de redondear el resultado
    public int calcularDistancia(int i, int j)
    {
        double cuadX = (this.puntosZonificacion[i].Coordenada.X - this.puntosZonificacion[j].Coordenada.X) * (this.puntosZonificacion[i].Coordenada.X - this.puntosZonificacion[j].Coordenada.X);
        double cuadY = (this.puntosZonificacion[i].Coordenada.Y - this.puntosZonificacion[j].Coordenada.Y) * (this.puntosZonificacion[i].Coordenada.Y - this.puntosZonificacion[j].Coordenada.Y);
        double distancia = Math.Sqrt(cuadX + cuadY);
        return (int)Math.Round(distancia);

    }

    /*
    public void calcularSemivariograma(){

        int distanciaActual = 0;
        int longitudMenor = (int)((Math.Min(this.Columnas, this.Filas)/2) * 0.8); //0.8 es un coeficiente de ajuste
        //es la maxima distancia a la que se calcula el semivariograma (coordenada x en la grafica del semivariograma)
        int hmax = (longitudMenor *  this.TamanoCelda);
        int h = 0;  //rango del semivariograma
        double max = 0; //maximo valor del semivariograma
        double[] VariablididadPuntosDistancia = new double[hmax+1]; // por la formula de mas abajo (subindice = (distanciaActual-1) / this.TamanoCelda;) no precisa "+1"
        double[] cantPuntosDistancia = new double[hmax+1];

        for (int i = 0; i < this.puntosZonificacion.Count; i++) {
            for (int j = i + 1; j < this.puntosZonificacion.Count; j++) {
                distanciaActual = calcularDistancia(i, j);
                if (distanciaActual <= hmax) {
                    VariablididadPuntosDistancia[distanciaActual] += Math.Pow(this.puntosZonificacion[i].Variabilidad - this.puntosZonificacion[j].Variabilidad, 2); 
                    cantPuntosDistancia[distanciaActual] += 1;
                }
            }
        }

        //Tengo que "normalizar" los sumandos
        for (int i = 0; i <= hmax; i++){
            if (cantPuntosDistancia[i] != 0)
                VariablididadPuntosDistancia[i] = VariablididadPuntosDistancia[i] / (2 * cantPuntosDistancia[i]);
        }       
        for (int i = 0; i < hmax; i++){
            if (VariablididadPuntosDistancia[i] > max){
                max = VariablididadPuntosDistancia[i];
            }
        }
        double limite = max * 0.92;
        while (VariablididadPuntosDistancia[h] < limite){
            h++;
        }

    }
    */
    /*
    public void calcularSemivariograma()
    {

        int distanciaActual = 0;
        int longitudMenor = (int)((Math.Min(this.Columnas, this.Filas) / 2) * 0.8); //0.8 es un coeficiente de ajuste
        //es la maxima distancia a la que se calcula el semivariograma (coordenada x en la grafica del semivariograma)
        int hmax = (longitudMenor * this.TamanoCelda);

        //Cada posicion del arreglo tendra los valores para las distancias dentro de cada rango, de la siguiente manera
        // {[0, h], (h, 2h], (2h, 3h], siendo h el tamaño de las celdas
        double[] VariablididadPuntosDistancia = new double[longitudMenor]; // por la formula de mas abajo (subindice = (distanciaActual-1) / this.TamanoCelda;) no precisa "+1"
        double[] cantPuntosDistancia = new double[longitudMenor];
        int subindice;

        double[] VariablididadPuntosDistancia1 = new double[hmax + 1]; // por la formula de mas abajo (subindice = (distanciaActual-1) / this.TamanoCelda;) no precisa "+1"
        double[] cantPuntosDistancia1 = new double[hmax + 1];

        double result = 0;

        //for (int i = 0; i < 1; i++) {
        for (int i = 0; i < this.puntosZonificacion.Count; i++)
        {
            for (int j = i + 1; j < this.puntosZonificacion.Count; j++)
            {
                distanciaActual = calcularDistancia(i, j);
                if (distanciaActual <= hmax) 
                    //if ((distanciaActual <= hmax) && (distanciaActual % this.tamanoCelda == 0))
                {
                    subindice = (distanciaActual - 1) / (this.TamanoCelda*this.TamanoCelda); //Se le resta 1 para que se ubique en el lugar correspondiente del arreglo!
                    result = Math.Pow(this.puntosZonificacion[i].Variabilidad - this.puntosZonificacion[j].Variabilidad, 2);

                    VariablididadPuntosDistancia[subindice] += result;
                    cantPuntosDistancia[subindice] += 1;

                    //VIEJO METODO
                    VariablididadPuntosDistancia1[distanciaActual] += result;
                    cantPuntosDistancia1[distanciaActual] += 1;

                }
            }
        }

        //ahora se actualiza "VariablididadPuntosDistancia" donde el j-esimo lugar indicara el valor para el semivariograma en el dominio
        //(j * tamañoceldas , (j+1)*tamañoceldas )
        for (int i = 0; i < longitudMenor; i++)
        {
            if (cantPuntosDistancia[i] != 0)
                VariablididadPuntosDistancia[i] = VariablididadPuntosDistancia[i] / (2 * cantPuntosDistancia[i]);
        }

        //Tengo que "normalizar" los sumandos
        for (int i = 0; i <= hmax; i++)
        {
            if (cantPuntosDistancia1[i] != 0)
                VariablididadPuntosDistancia1[i] = VariablididadPuntosDistancia1[i] / (2 * cantPuntosDistancia1[i]);
        }
        int h = 0;
        double max = 0;
        for (int i = 0; i < hmax; i++)
        {
            if (VariablididadPuntosDistancia1[i] > max)
            {
                //h = i;
                max = VariablididadPuntosDistancia1[i];
            }
        }

        double limite = max * 0.92;
        while (VariablididadPuntosDistancia1[h] < limite)
        {
            h++;
        }

    }
    */
    /****************************************************************************************************************************************/


    public void calcularVariabilidad()
    {        
        for (int i = 0; i < this.Variables.Count; i++) { this.Variables[i].calcularMedia(this.puntosZonificacion); }                       
        for (int i = 0; i < this.puntosZonificacion.Count; i++) { this.puntosZonificacion[i].calcularVariabilidad(); }
        //Lo hace para todas las variables seleccionadas
      /*  for (int i = 0; i < variables_seleccion.Length; i++) { this.Variables[variables_seleccion[i]].calcularMedia(this.puntosZonificacion); }

        for (int i = 0; i < this.puntosZonificacion.Count; i++) { this.puntosZonificacion[i].calcularVariabilidad(variables_seleccion); }*/
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

    //#region "Create FeatureClass"
    /////<summary>Simple helper to create a featureclass in a geodatabase.</summary>
    ///// 
    /////<param name="workspace">An IWorkspace2 interface</param>
    /////<param name="featureDataset">An IFeatureDataset interface or Nothing</param>
    /////<param name="featureClassName">A System.String that contains the name of the feature class to open or create. Example: "states"</param>
    /////<param name="fields">An IFields interface</param>
    /////<param name="CLSID">A UID value or Nothing. Example "esriGeoDatabase.Feature" or Nothing</param>
    /////<param name="CLSEXT">A UID value or Nothing (this is the class extension if you want to reference a class extension when creating the feature class).</param>
    /////<param name="strConfigKeyword">An empty System.String or RDBMS table string for ArcSDE. Example: "myTable" or ""</param>
    /////  
    /////<returns>An IFeatureClass interface or a Nothing</returns>
    /////  
    /////<remarks>
    /////  (1) If a 'featureClassName' already exists in the workspace a reference to that feature class 
    /////      object will be returned.
    /////  (2) If an IFeatureDataset is passed in for the 'featureDataset' argument the feature class
    /////      will be created in the dataset. If a Nothing is passed in for the 'featureDataset'
    /////      argument the feature class will be created in the workspace.
    /////  (3) When creating a feature class in a dataset the spatial reference is inherited 
    /////      from the dataset object.
    /////  (4) If an IFields interface is supplied for the 'fields' collection it will be used to create the
    /////      table. If a Nothing value is supplied for the 'fields' collection, a table will be created using 
    /////      default values in the method.
    /////  (5) The 'strConfigurationKeyword' parameter allows the application to control the physical layout 
    /////      for this table in the underlying RDBMS—for example, in the case of an Oracle database, the 
    /////      configuration keyword controls the tablespace in which the table is created, the initial and 
    /////     next extents, and other properties. The 'strConfigurationKeywords' for an ArcSDE instance are 
    /////      set up by the ArcSDE data administrator, the list of available keywords supported by a workspace 
    /////      may be obtained using the IWorkspaceConfiguration interface. For more information on configuration 
    /////      keywords, refer to the ArcSDE documentation. When not using an ArcSDE table use an empty 
    /////      string (ex: "").
    /////</remarks>
    //public IFeatureClass CreateFeatureClass(IWorkspace2 workspace, IFeatureDataset featureDataset, String featureClassName, IFields fields, UID CLSID, UID CLSEXT, String strConfigKeyword)
    //{
    //    if (featureClassName == "") return null; // name was not passed in 

    //    IFeatureClass featureClass;
    //    IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace; // Explicit Cast

    //    if (workspace.get_NameExists(esriDatasetType.esriDTFeatureClass, featureClassName)) //feature class with that name already exists 
    //    {
    //        featureClass = featureWorkspace.OpenFeatureClass(featureClassName);
    //        return featureClass;
    //    }

    //    // assign the class id value if not assigned
    //    if (CLSID == null)
    //    {
    //        CLSID = new UIDClass();
    //        CLSID.Value = "esriGeoDatabase.Feature";
    //    }

    //    IObjectClassDescription objectClassDescription = new FeatureClassDescriptionClass();

    //    // if a fields collection is not passed in then supply our own
    //    if (fields == null)
    //    {
    //        // create the fields using the required fields method
    //        fields = objectClassDescription.RequiredFields;
    //        IFieldsEdit fieldsEdit = (IFieldsEdit)fields; // Explicit Cast
    //        IField field = new FieldClass();

    //        // create a user defined text field
    //        IFieldEdit fieldEdit = (IFieldEdit)field; // Explicit Cast

    //        // setup field properties
    //        fieldEdit.Name_2 = "Valor";
    //        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
    //        fieldEdit.IsNullable_2 = true;
    //        fieldEdit.AliasName_2 = "Valor";
    //        fieldEdit.DefaultValue_2 = 0;
    //        fieldEdit.Editable_2 = true;
            
    //        // add field to field collection
    //        fieldsEdit.AddField(field);
    //        //fields = (IFields)fieldsEdit; // Explicit Cast


    //        IField puntoField = new FieldClass();

            
    //        // create a user defined text field
    //        IFieldEdit puntoFieldEdit = (IFieldEdit)puntoField; // Explicit Cast

    //        // setup field properties
    //        puntoFieldEdit.Name_2 = "Punto";
    //        puntoFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
    //        puntoFieldEdit.IsNullable_2 = true;
    //        puntoFieldEdit.AliasName_2 = "Punto";
    //        puntoFieldEdit.DefaultValue_2 = 0;
    //        puntoFieldEdit.Editable_2 = true;

    //        // Modify the GeometryDef object before using the fields collection to create a 
    //        // feature class.
    //        IGeometryDef geometryDef = new GeometryDefClass();
    //        IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            
    //        // Alter the feature class geometry type to lines (default is polygons).
    //        geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
    //        puntoFieldEdit.GeometryDef_2 = geometryDefEdit;
    //        // add field to field collection
    //        fieldsEdit.AddField(puntoField);
    //        fields = (IFields)fieldsEdit; // Explicit Cast
    //    }

    //    String strShapeField = "";

    //    // locate the shape field
    //    for (int j = 0; j < fields.FieldCount; j++)
    //    {
    //        if (fields.get_Field(j).Type == esriFieldType.esriFieldTypeGeometry)
    //        {
    //            strShapeField = fields.get_Field(j).Name;
    //        }
    //    }

    //    // Use IFieldChecker to create a validated fields collection.
    //    IFieldChecker fieldChecker = new FieldCheckerClass();
    //    IEnumFieldError enumFieldError = null;
    //    IFields validatedFields = null;
    //    fieldChecker.ValidateWorkspace = (IWorkspace)workspace;
    //    fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

    //    // The enumFieldError enumerator can be inspected at this point to determine 
    //    // which fields were modified during validation.


    //    // finally create and return the feature class
    //    if (featureDataset == null)// if no feature dataset passed in, create at the workspace level
    //    {
    //        featureClass = featureWorkspace.CreateFeatureClass(featureClassName, validatedFields, CLSID, CLSEXT, esriFeatureType.esriFTSimple, /*strShapeField*/"", strConfigKeyword);
    //    }
    //    else
    //    {
    //        featureClass = featureDataset.CreateFeatureClass(featureClassName, validatedFields, CLSID, CLSEXT, esriFeatureType.esriFTSimple, strShapeField, strConfigKeyword);
    //    }
    //    return featureClass;
    //}
    //#endregion





}
