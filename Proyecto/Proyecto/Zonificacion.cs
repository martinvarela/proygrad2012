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
                sLine = objReader.ReadLine(); 
                
                PuntoZonificacion ptoZonificacion = new PuntoZonificacion();
                ptoZonificacion.Coordenada = calcularCoordenada(this.coordenadaInicial, this.TamanoCelda, ix-1, iy-1);
                ptoZonificacion.Variables = this.Variables;

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
                this.agregarPuntoZonificacion(ptoZonificacion);
            }
        }

        //cierro el archivo
        objReader.Close();
        

        /*PRUEBAS */        
        IMap map = ArcMap.Document.FocusMap;
        MessageBox.Show(map.LayerCount.ToString());
        MessageBox.Show(map.Name);
        MessageBox.Show(map.Layers.Next().Name);
         
        IWorkspace ws = ((IDataset)map.Layer[0]).Workspace;
        
       

        IFeatureClass nueva = CreateFeatureClass((IWorkspace2)ws, null, "nuevaClase", null, null, null, "nada");
        //IFeatureClass otraClase = CreateStandaloneFeatureClass(null, "clase nueva 2", null, "ss");
        
        
        
        ESRI.ArcGIS.esriSystem.IExtension extension = ArcMap.Application.FindExtensionByName("ESRI Object Editor");
        ESRI.ArcGIS.Editor.IEditor2 editor2 = extension as ESRI.ArcGIS.Editor.IEditor2; // Dynamic Cast

        if (editor2.EditState != esriEditState.esriStateEditing)
        {
            editor2.StartEditing(ws);
        }
        
        /*HAGO LOS CAMBIOS*/
        IFeatureCursor cursor = nueva.Insert(true);
        //cursor.InsertFeature
        IFeatureBuffer featureBuffer = nueva.CreateFeatureBuffer();

        int indice = nueva.FindField("Valor");
        featureBuffer.set_Value(indice, 1233);
        cursor.InsertFeature(featureBuffer);

        // Calling flush allows you to handle any errors at a known time rather then on the cursor destruction.
        cursor.Flush();

        // Explicitly release the cursor.
        Marshal.ReleaseComObject(cursor);

        //Stop editing and save edits
        editor2.StopEditing(true);

        //map.AddLayer((ILayer)nueva);

        IFeatureLayer featureLayer = new FeatureLayerClass();
        featureLayer.FeatureClass = nueva;
        ILayer layer = (ILayer)featureLayer;
        layer.Name = featureLayer.FeatureClass.AliasName;
        // Add the Layer to the focus map
        map.AddLayer(layer);

        ESRI.ArcGIS.Carto.IActiveView activeView = (ESRI.ArcGIS.Carto.IActiveView)map;
        activeView.Refresh();
        /*FIN PRUEBAS*/

        
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
        
        Point puntoo = new Point();
        puntoo.X = punto.Coordenada.X;
        puntoo.Y = punto.Coordenada.Y;

        RgbColor color = new ESRI.ArcGIS.Display.RgbColor();
        color.Blue = 222;
        color.Green = 0;
        color.Red = 0;
        MarkerElement puntooooo = new MarkerElement();
        puntooooo.Geometry = puntoo;
        //esto hay que cambiarlo
        //AddGraphicToMap(ArcMap.Document.FocusMap, puntoo, color, color);
        
        
    }

    #region "Create FeatureClass"

    ///<summary>Simple helper to create a featureclass in a geodatabase.</summary>
    /// 
    ///<param name="workspace">An IWorkspace2 interface</param>
    ///<param name="featureDataset">An IFeatureDataset interface or Nothing</param>
    ///<param name="featureClassName">A System.String that contains the name of the feature class to open or create. Example: "states"</param>
    ///<param name="fields">An IFields interface</param>
    ///<param name="CLSID">A UID value or Nothing. Example "esriGeoDatabase.Feature" or Nothing</param>
    ///<param name="CLSEXT">A UID value or Nothing (this is the class extension if you want to reference a class extension when creating the feature class).</param>
    ///<param name="strConfigKeyword">An empty System.String or RDBMS table string for ArcSDE. Example: "myTable" or ""</param>
    ///  
    ///<returns>An IFeatureClass interface or a Nothing</returns>
    ///  
    ///<remarks>
    ///  (1) If a 'featureClassName' already exists in the workspace a reference to that feature class 
    ///      object will be returned.
    ///  (2) If an IFeatureDataset is passed in for the 'featureDataset' argument the feature class
    ///      will be created in the dataset. If a Nothing is passed in for the 'featureDataset'
    ///      argument the feature class will be created in the workspace.
    ///  (3) When creating a feature class in a dataset the spatial reference is inherited 
    ///      from the dataset object.
    ///  (4) If an IFields interface is supplied for the 'fields' collection it will be used to create the
    ///      table. If a Nothing value is supplied for the 'fields' collection, a table will be created using 
    ///      default values in the method.
    ///  (5) The 'strConfigurationKeyword' parameter allows the application to control the physical layout 
    ///      for this table in the underlying RDBMS—for example, in the case of an Oracle database, the 
    ///      configuration keyword controls the tablespace in which the table is created, the initial and 
    ///     next extents, and other properties. The 'strConfigurationKeywords' for an ArcSDE instance are 
    ///      set up by the ArcSDE data administrator, the list of available keywords supported by a workspace 
    ///      may be obtained using the IWorkspaceConfiguration interface. For more information on configuration 
    ///      keywords, refer to the ArcSDE documentation. When not using an ArcSDE table use an empty 
    ///      string (ex: "").
    ///</remarks>
    public IFeatureClass CreateFeatureClass(IWorkspace2 workspace, IFeatureDataset featureDataset, String featureClassName, IFields fields, UID CLSID, UID CLSEXT, String strConfigKeyword)
    {
        if (featureClassName == "") return null; // name was not passed in 

        IFeatureClass featureClass;
        IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace; // Explicit Cast

        if (workspace.get_NameExists(esriDatasetType.esriDTFeatureClass, featureClassName)) //feature class with that name already exists 
        {
            featureClass = featureWorkspace.OpenFeatureClass(featureClassName);
            return featureClass;
        }

        // assign the class id value if not assigned
        if (CLSID == null)
        {
            CLSID = new UIDClass();
            CLSID.Value = "esriGeoDatabase.Feature";
        }

        IObjectClassDescription objectClassDescription = new FeatureClassDescriptionClass();

        // if a fields collection is not passed in then supply our own
        if (fields == null)
        {
            // create the fields using the required fields method
            fields = objectClassDescription.RequiredFields;
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields; // Explicit Cast
            IField field = new FieldClass();

            // create a user defined text field
            IFieldEdit fieldEdit = (IFieldEdit)field; // Explicit Cast

            // setup field properties
            fieldEdit.Name_2 = "Valor";
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            fieldEdit.IsNullable_2 = true;
            fieldEdit.AliasName_2 = "Valor";
            fieldEdit.DefaultValue_2 = 0;
            fieldEdit.Editable_2 = true;
            
            // add field to field collection
            fieldsEdit.AddField(field);
            //fields = (IFields)fieldsEdit; // Explicit Cast


            IField puntoField = new FieldClass();

            
            // create a user defined text field
            IFieldEdit puntoFieldEdit = (IFieldEdit)puntoField; // Explicit Cast

            // setup field properties
            puntoFieldEdit.Name_2 = "Punto";
            puntoFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            puntoFieldEdit.IsNullable_2 = true;
            puntoFieldEdit.AliasName_2 = "Punto";
            puntoFieldEdit.DefaultValue_2 = 0;
            puntoFieldEdit.Editable_2 = true;

            // Modify the GeometryDef object before using the fields collection to create a 
            // feature class.
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            
            // Alter the feature class geometry type to lines (default is polygons).
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            puntoFieldEdit.GeometryDef_2 = geometryDefEdit;
            // add field to field collection
            fieldsEdit.AddField(puntoField);
            fields = (IFields)fieldsEdit; // Explicit Cast
        }

        String strShapeField = "";

        // locate the shape field
        for (int j = 0; j < fields.FieldCount; j++)
        {
            if (fields.get_Field(j).Type == esriFieldType.esriFieldTypeGeometry)
            {
                strShapeField = fields.get_Field(j).Name;
            }
        }

        // Use IFieldChecker to create a validated fields collection.
        IFieldChecker fieldChecker = new FieldCheckerClass();
        IEnumFieldError enumFieldError = null;
        IFields validatedFields = null;
        fieldChecker.ValidateWorkspace = (IWorkspace)workspace;
        fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

        // The enumFieldError enumerator can be inspected at this point to determine 
        // which fields were modified during validation.


        // finally create and return the feature class
        if (featureDataset == null)// if no feature dataset passed in, create at the workspace level
        {
            featureClass = featureWorkspace.CreateFeatureClass(featureClassName, validatedFields, CLSID, CLSEXT, esriFeatureType.esriFTSimple, /*strShapeField*/"", strConfigKeyword);
        }
        else
        {
            featureClass = featureDataset.CreateFeatureClass(featureClassName, validatedFields, CLSID, CLSEXT, esriFeatureType.esriFTSimple, strShapeField, strConfigKeyword);
        }
        return featureClass;
    }
    #endregion

    public IFeatureClass CreateStandaloneFeatureClass(IWorkspace workspace, String featureClassName, IFields fieldsCollection, String shapeFieldName)
    {
        IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
        IFeatureClassDescription fcDesc = new FeatureClassDescriptionClass();
        IObjectClassDescription ocDesc = (IObjectClassDescription)fcDesc;

        // Use IFieldChecker to create a validated fields collection.
        IFieldChecker fieldChecker = new FieldCheckerClass();
        IEnumFieldError enumFieldError = null;
        IFields validatedFields = null;
        fieldChecker.ValidateWorkspace = workspace;
        fieldChecker.Validate(fieldsCollection, out enumFieldError, out
    validatedFields);

        // The enumFieldError enumerator can be inspected at this point to determine 
        // which fields were modified during validation.
        IFeatureClass featureClass = featureWorkspace.CreateFeatureClass
          (featureClassName, validatedFields, ocDesc.InstanceCLSID,
          ocDesc.ClassExtensionCLSID, esriFeatureType.esriFTSimple, shapeFieldName,
          "");
        return featureClass;
    }


    ///<remarks>You could also use the: application.FindExtensionByName("ESRI Object Editor") to get the extension object.</remarks>
    public ESRI.ArcGIS.Editor.IEditor2 GetEditorFromArcMap(ESRI.ArcGIS.ArcMapUI.IMxApplication mxApplication)
    {
        if (mxApplication == null)
        {
            return null;
        }
        ESRI.ArcGIS.Framework.IApplication application = mxApplication as ESRI.ArcGIS.Framework.IApplication; // Dynamic Cast
        ESRI.ArcGIS.esriSystem.IExtension extension = ArcMap.Application.FindExtensionByName("ESRI Object Editor");
        ESRI.ArcGIS.Editor.IEditor2 editor2 = extension as ESRI.ArcGIS.Editor.IEditor2; // Dynamic Cast

        return editor2;
    }

    ///<summary>Get MxApplication from ArcMap</summary>
///<param name="application">An IApplication interface that is the ArcMap application.</param>
///<returns>An IMxApplication interface.</returns>
///<remarks>The IMxApplication interface allows access the AppDisplay object, the selection environment, and the default printer page settings.</remarks>
public ESRI.ArcGIS.Carto.IMap GetMxApplicationFromArcMap(ESRI.ArcGIS.Framework.IApplication application)
{

  if (application == null)
  {
	return null;
  }

  if (! (application is ESRI.ArcGIS.ArcMapUI.IMxApplication))
  {
	return null;
  }

  ESRI.ArcGIS.ArcMapUI.IMxApplication mxApplication = (ESRI.ArcGIS.ArcMapUI.IMxApplication)application; // Explicit Cast

  return (ESRI.ArcGIS.Carto.IMap)mxApplication;
}



}
