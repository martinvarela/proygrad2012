using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Carto;
using Proyecto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;



class Controlador
{
    private static Controlador instancia;
    private IWorkspace wsSSA;
    private IWorkspace wsZonif;
    private IWorkspace wsBlackmore;
    private double rango = -1;
    private double area = -1;

    private Controlador() 
    {
        this.ssa = new SSA();
        
    }
    public static Controlador getInstancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new Controlador();    
            }
            return instancia;
        }
    }
    
    private String nombreCapaPuntosZonificacion;
    private String nombreCapaPoligonos;
    private String nombreCapaPuntosMuestreo;

    private IFeatureClass capaPuntosZonificacion;
    private IFeatureLayer capaPuntosMuestreo;
    private IFeatureLayer capaPoligonos;
    private SSA ssa;

    //atributos
    private Zonificacion zonificacion { get; set; }
    private List<Capa> capas;
    private Muestreo muestreo { get; set; }
    private Blackmore blackmore;


    public SSA getSSA() { return this.ssa; }
    public void setSSA(SSA s) { this.ssa = s; }

    //se crea la instancia Muestreo con su respectiva lista de posibles puntos de Muestreos.
    //se devuelve en el arcmap la capa "CR-hhMMss" que se cambiara por el nombre pasado como parametro que contiene los posibles puntos de muestreo(todos) 
    //para realizar de forma manual el semivariograma de forma de encontrar cual es el RANGO.
    public Muestreo crearPuntosMuestreo(bool conRed, String rutaEntrada, bool filasColumnas, int vertical, int horizontal, List<int> variablesMarcadas, ProgressBar pBar, String rutaCapa, String nombreCapa, Label lblProgressBar)
    {
        IMap map = ArcMap.Document.FocusMap;
        
        IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
        IWorkspace workspaceZonif = workspaceFactory.OpenFromFile(rutaCapa, 0);
        this.wsZonif = workspaceZonif;
        
        //paso 1
        lblProgressBar.Text = "Cargando puntos de zonificación...";
        lblProgressBar.Visible = true;

        Zonificacion zonificacion = new Zonificacion(rutaEntrada, variablesMarcadas, pBar);
        //lblProgressBar.Text = "";

        //paso 2
        lblProgressBar.Text = "Calculando variabilidad de los puntos de zonificación...";
        zonificacion.calcularVariabilidad(pBar);
        lblProgressBar.Text = "";

        //paso 3 - crear Puntos de Muestreo
        Muestreo muestreo = new Muestreo();

        //paso 4
        //se crea una layer temporal con los puntos de zonificacion sacados del .ZF
        //IMap map = ArcMap.Document.FocusMap;
        String ahora = System.DateTime.Now.ToString("HHmmss");
        if (conRed)
        {
            this.nombreCapaPuntosZonificacion = "PZ_" + ahora;
            this.nombreCapaPoligonos = "CR_" + ahora;
            this.nombreCapaPuntosMuestreo = "CR_" + ahora + "_label";
        }
        else
        {
            this.nombreCapaPuntosZonificacion = nombreCapa;
        }

        lblProgressBar.Text = "Creando layer con los puntos de zonificación...";
        this.capaPuntosZonificacion = this.crearCapaPuntosZonificacion(map, nombreCapaPuntosZonificacion, zonificacion.PuntosZonificacion, pBar);
        lblProgressBar.Text = "";

        
        if (conRed)
        {
            int tamCelda = zonificacion.getTamanoCelda();
            //hacer poligono de todo el campo
            string nomPolExt = "polExt" + System.DateTime.Now.ToString("HHmmss");
            IFeatureClass extensionPoligono = this.crearPoligonoExtension(nombreCapaPuntosZonificacion, nomPolExt, tamCelda);

            //se crea la capa de red con las filas y columnas pasadas como parametro
            //se carga en el controlar la capaPoligonos y capaPuntosMuestreo
            this.crearRed(map, this.nombreCapaPoligonos, nombreCapa, zonificacion.PuntoOrigen, zonificacion.PuntoOpuesto, filasColumnas, vertical, horizontal, true, this.nombreCapaPuntosZonificacion);

            //paso 5
            lblProgressBar.Text = "Creando layer con los puntos de red...";

            pBar.Minimum = 1;
            pBar.Maximum = vertical * horizontal;
            pBar.Step = 1;
            pBar.Value = 1;
            pBar.Visible = true;

            //calcula los valores de los puntos de muestreo haciendo promedio en los puntos que "caen" dentro de la celda de la capa de poligonos
            //agrega cada punto de muestreo a la lista de la instancia de muestreo.
            this.cargarValoresPuntosMuestreo(map, muestreo, this.nombreCapaPuntosZonificacion, this.nombreCapaPoligonos, this.nombreCapaPuntosMuestreo, 2, 2, pBar);


            //se quitan los puntos externos al campo
            this.quitarPuntosExternos(this.nombreCapaPuntosMuestreo, extensionPoligono);
            

            pBar.Visible = false;
            lblProgressBar.Text = "";

            //borro la capa auxiliar de puntosZonificacion creada
            if (((IDataset)this.capaPuntosZonificacion).CanDelete())
            {
                ((IDataset)this.capaPuntosZonificacion).Delete();
            }

            //borro la capa auxiliar de poligonos creada
            if (((IDataset)this.capaPoligonos.FeatureClass).CanDelete())
            {
                ((IDataset)this.capaPoligonos.FeatureClass).Delete();
            }
     
        }
        else
        {
            //esto en el caso de que se quiera trabajar con todos los puntos de zonificacion.
            this.capaPuntosMuestreo = new FeatureLayer();
            this.capaPuntosMuestreo.FeatureClass = this.capaPuntosZonificacion;
            //se agrega al map la capa de puntos de muestreo (la de puntos, no la de poligonos)
            ILayer layer = (ILayer)this.capaPuntosMuestreo;
            layer.Name = this.capaPuntosMuestreo.FeatureClass.AliasName;
            map.AddLayer(layer);

            ESRI.ArcGIS.Carto.IActiveView activeView = (ESRI.ArcGIS.Carto.IActiveView)map;
            activeView.Refresh();
            
        }
        
        return muestreo;
    }

    //capaPuntosMuestreo es la capa seleccionada por el usuario
    //metodoInterpolacion puede ser IDW o Kriging
    //rango ??? o cantmuestras
    //error maximo aceptado en % ej: 5
    public void optimizarMuestreo(IFeatureClass capaPuntosMuestreo, String metodoInterpolacion, double expIDW, int nroMuestras, double error, string rutaCapa)
    {
        IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
        
        //esta ruta la indica el usuario
        string fechaActual = System.DateTime.Now.ToString("ddMMyyyy_HHmm");
        string nombreDirectorio = fechaActual + "_Capas";
        string nombreArchivo = fechaActual + "_Resumen.txt";
        string pathCombinado = System.IO.Path.Combine(rutaCapa, nombreDirectorio);
        string pathArchivo = System.IO.Path.Combine(rutaCapa, nombreArchivo);
        
        System.IO.Directory.CreateDirectory(pathCombinado);
        IWorkspace workspace = workspaceFactory.OpenFromFile(pathCombinado, 0);
        this.wsSSA = workspace;
        this.ssa.setWorkspace(this.wsSSA);
        this.ssa.cantMuestras = nroMuestras;
        IFeatureClass resultado = this.ssa.SimulatedAnnealing(capaPuntosMuestreo, metodoInterpolacion, expIDW, error, pathArchivo);    
    } 

    public void crearBlackmore(bool filasColumnas, int vertical, int horizontal, List<DTCapasBlackmore> capasDT, double dst, string nombreCapaBlackmore, string rutaCapaBlackmore)
    {

        this.capas = new List<Capa>();

        IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
        IWorkspace workspaceBlackmore = workspaceFactory.OpenFromFile(rutaCapaBlackmore, 0);
        this.wsBlackmore = workspaceBlackmore;

        ILayer layerCapaBase = null;
        Entrada entradaBase = null;
        //IFeatureLayer fLayerCapaBase;
        //IFeatureClass fClassTemplate = null;
        
        //se crean las capas de entrada(instancias)
        int indice = 0;
        foreach (DTCapasBlackmore dtCapa in capasDT)
        {
            //creo una nueva entrada
            Entrada capaEntrada = new Entrada();
            capaEntrada.setNombreAtributo(dtCapa.getListaAtributos()[0].ToString());
            capaEntrada.setNombre(dtCapa.getNombreCapa());
            //capa de union no tiene por el momento
            capaEntrada.setLayerCapa(dtCapa.getCapa());
            capaEntrada.setIndice(indice);
            if (dtCapa.esCapaBase())
            {
                capaEntrada.setEsCapaBase(true);
                layerCapaBase = dtCapa.getCapa();
                entradaBase = capaEntrada;
                //fLayerCapaBase = dtCapa.getCapa() as FeatureLayer;
                //fClassTemplate = fLayerCapaBase.FeatureClass;
            }
            else
            {
                capaEntrada.setEsCapaBase(false);
            }
 
            //agrego la capa a la lista de capas del controlador
            this.capas.Add(capaEntrada);
            indice++;
        }

        string ahora = System.DateTime.Now.ToString("HHmmss");
        //paso 1
        //creo la instancia de Blackmore, se crea la capa de red (futura salida del modulo)
        this.blackmore = new Blackmore(filasColumnas, vertical, horizontal, dst,
                                       layerCapaBase, ahora + "_celdasAux", 
                                       this.wsBlackmore);

        //paso 2
        IFeatureLayer poligonosBlackmore = this.blackmore.getPoligonosBlackmore();

        //paso 3
        String rutaCapaUnion = rutaCapaBlackmore + @"\" + nombreCapaBlackmore + ".shp";
        IFeatureClass unionCapaBase = this.unionEspacial(poligonosBlackmore.FeatureClass, layerCapaBase, rutaCapaUnion, false, entradaBase.getNombreAtributo(),"merge_"+entradaBase.getIndice().ToString());


        //paso 4
        entradaBase.setCapaUnion(unionCapaBase);
        double auxMediaCapas = entradaBase.getMedia();
        int cantCapas = 1;

        //paso 5
        this.blackmore.crearCeldas(unionCapaBase);

        //paso 6 y 7
        foreach (Entrada capaEntrada in this.capas)
        {
            if (!capaEntrada.getEsCapaBase())
            {
                rutaCapaUnion = rutaCapaBlackmore + @"\" + ahora + "_UNION" + capaEntrada.getNombre() + ".shp";
                capaEntrada.setCapaUnion(this.unionEspacial(unionCapaBase, capaEntrada.getLayerCapa(), rutaCapaUnion, true, capaEntrada.getNombreAtributo(),"merge_" + capaEntrada.getIndice().ToString()));    
                auxMediaCapas += capaEntrada.getMedia();
                cantCapas++;

            }
        }

        //paso 
        IFeatureClass featureUnion = entradaBase.getCapaUnion();
        //falta modificar el nombre de la capa para ser devuelta y setearla en la instancia de 'blackmore'

        //paso  
        int indiceDst = this.crearFieldAFeatureClass(featureUnion, "std_dev", esriFieldType.esriFieldTypeDouble);
        //paso 
        int indiceMean = this.crearFieldAFeatureClass(featureUnion, "mean", esriFieldType.esriFieldTypeDouble);
        //paso
        int indiceClasificacion = this.crearFieldAFeatureClass(featureUnion, "clase", esriFieldType.esriFieldTypeInteger);


        //paso 
        foreach (Celda c in blackmore.getCeldas())
        {
            int fid = c.getFID();
            double auxValor = 0;
            double dstCelda = 0;
            double valorCelda = 0;
            int n = 0;
            foreach (Entrada e in this.capas)
            {
                auxValor = e.getValorCelda(fid);
                dstCelda += Math.Pow(auxValor - e.getMedia(), 2);
                valorCelda += auxValor;
                n++;
                auxValor = 0;
            }
            c.setDesviacion(Math.Sqrt(dstCelda / n));
            c.setMedia(valorCelda / n);
            c.clasificar(dst, auxMediaCapas/cantCapas);


            this.setValoresFeatureUnion(featureUnion,fid,indiceDst,c.getDesviacion(),indiceMean,c.getMedia(),indiceClasificacion,c.getClasificacion());

        }

        ////se borran las capas auxiliares ya que no se usan mas, solo me quedo con la capa base
        foreach (Entrada e in this.capas)
        {
            if (!e.getEsCapaBase())
            {
                if (((IDataset)(e.getCapaUnion())).CanDelete())
                    ((IDataset)(e.getCapaUnion())).Delete();
            }
            //se borran los atributos de la capa base que no se necesitan
            else
            {
                List<string> camposGuardar = new List<string>();
                camposGuardar.Add("FID");
                camposGuardar.Add("Shape");
                camposGuardar.Add("std_dev");
                camposGuardar.Add("mean");
                camposGuardar.Add("clase");
                IFields fields = e.getCapaUnion().Fields;
                IField f;
                for (int i = fields.FieldCount-1 ; i >= 0 ; i--)
                {
                    f = fields.get_Field(i);
                    if (!camposGuardar.Contains(f.Name))
                    {
                        e.getCapaUnion().DeleteField(f);
                    }
                }
            }
        }

        //borro capa de poligonos inicial
        if (((IDataset)(this.blackmore.getPoligonosBlackmore().FeatureClass)).CanDelete())
            ((IDataset)(this.blackmore.getPoligonosBlackmore().FeatureClass)).Delete();
            


    }

    private IFeatureClass crearCapaPuntosZonificacion(IMap map, string nombreFeatureClass, List<PuntoZonificacion> listaPuntos, ProgressBar pBar)
    {
        IWorkspace ws = this.wsZonif;
        IWorkspace2 ws2 = (IWorkspace2)ws;
        IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)ws2; // Explicit Cast

        IFeatureClass nuevaFeatureClass = this.crearFeatureClassConFields(nombreFeatureClass, featureWorkspace);

        IFeatureBuffer featureBuffer = nuevaFeatureClass.CreateFeatureBuffer();
        IFeatureCursor FeatureCursor = nuevaFeatureClass.Insert(true);

        IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)ws;

        //Start an edit session and operation
        workspaceEdit.StartEditing(true);
        workspaceEdit.StartEditOperation();

        object featureOID;

        ESRI.ArcGIS.Geometry.IPoint point;

        pBar.Minimum = 1;
        pBar.Maximum = listaPuntos.Count;
        pBar.Step = 1;
        pBar.Value = 1;
        pBar.Visible = true;
        //int indiceValor = featureBuffer.Fields.FindField("Valor");

        try
        {
            foreach (PuntoZonificacion aux in listaPuntos)
            {
                point = new ESRI.ArcGIS.Geometry.PointClass();
                point.X = aux.Coordenada.X;
                point.Y = aux.Coordenada.Y;

                featureBuffer.Shape = point;
                featureBuffer.set_Value(featureBuffer.Fields.FindField("Valor"), aux.Variabilidad);

                //Insert the feature into the feature cursor
                featureOID = FeatureCursor.InsertFeature(featureBuffer);

                pBar.PerformStep();
            }
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message.ToString());
        }
        //Flush the feature cursor to the database
        //Calling flush allows you to handle any errors at a known time rather then on the cursor destruction.
        FeatureCursor.Flush();

        //Stop editing
        workspaceEdit.StopEditOperation();
        workspaceEdit.StopEditing(true);

        //Release the Cursor
        System.Runtime.InteropServices.Marshal.ReleaseComObject(FeatureCursor);

        //IFeatureLayer featureLayer = new FeatureLayerClass();
        //featureLayer.FeatureClass = nuevaFeatureClass;

        //ILayer layer = (ILayer)featureLayer;
        //layer.Name = featureLayer.FeatureClass.AliasName;
        //// Add the Layer to the focus map
        //map.AddLayer(layer);

        //ESRI.ArcGIS.Carto.IActiveView activeView = (ESRI.ArcGIS.Carto.IActiveView)map;
        //activeView.Refresh();

        pBar.Visible = false;

        return nuevaFeatureClass;
    }

    private IFeatureClass crearCapaPuntosMuestreo(IMap map, string nombreFeatureClass, List<PuntoMuestreo> listaPuntos)
    {

        IWorkspace ws = this.wsZonif;
        IWorkspace2 ws2 = (IWorkspace2)ws;
        IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)ws2; // Explicit Cast

        IFeatureClass nuevaFeatureClass = this.crearFeatureClassConFields(nombreFeatureClass, featureWorkspace);

        IFeatureBuffer featureBuffer = nuevaFeatureClass.CreateFeatureBuffer();
        IFeatureCursor FeatureCursor = nuevaFeatureClass.Insert(true);

        IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)ws;

        //Start an edit session and operation
        workspaceEdit.StartEditing(true);
        workspaceEdit.StartEditOperation();

        object featureOID;

        //With a feature buffer you have the ability to set the attribute for a specific field to be
        //the same for all features added to the buffer.
        //featureBuffer.set_Value(featureBuffer.Fields.FindField("Valor"), 0);

        //Here you can set the featurebuffers's shape by setting the featureBuffer.Shape
        //to a geomerty that matched the featureclasses.
        //Create 100 features using FeatureBuffer and insert into a feature cursor
        ESRI.ArcGIS.Geometry.IPoint point;// = new ESRI.ArcGIS.Geometry.PointClass();

        foreach (PuntoMuestreo aux in listaPuntos)
        {
            point = new ESRI.ArcGIS.Geometry.PointClass();
            point.X = aux.getCoordenada().X;
            point.Y = aux.getCoordenada().Y;

            featureBuffer.Shape = point;
            featureBuffer.set_Value(featureBuffer.Fields.FindField("Valor"), aux.getValor());

            //Insert the feature into the feature cursor
            featureOID = FeatureCursor.InsertFeature(featureBuffer);

        }
        //Flush the feature cursor to the database
        //Calling flush allows you to handle any errors at a known time rather then on the cursor destruction.
        FeatureCursor.Flush();

        //Stop editing
        workspaceEdit.StopEditOperation();
        workspaceEdit.StopEditing(true);

        //Release the Cursor
        System.Runtime.InteropServices.Marshal.ReleaseComObject(FeatureCursor);

        IFeatureLayer featureLayer = new FeatureLayerClass();

        featureLayer.FeatureClass = nuevaFeatureClass;

        ILayer layer = (ILayer)featureLayer;
        layer.Name = featureLayer.FeatureClass.AliasName;
        // Add the Layer to the focus map
        map.AddLayer(layer);

        ESRI.ArcGIS.Carto.IActiveView activeView = (ESRI.ArcGIS.Carto.IActiveView)map;
        activeView.Refresh();

        return nuevaFeatureClass;
    }

    private IFeatureClass crearFeatureClassConFields(String featureClassName, IFeatureWorkspace featureWorkspace)
    {
        IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
        IObjectClassDescription ocDescription = (IObjectClassDescription)fcDescription;

        // Create a fields collection for the feature class.
        IFields fields = new FieldsClass();
        IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

        // Add an Object ID field to the fields collection. This is mandatory for feature classes.
        IField oidField = new FieldClass();
        IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
        oidFieldEdit.Name_2 = "OID";
        oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
        fieldsEdit.AddField(oidField);

        // Create a geometry definition (and spatial reference) for the feature class.
        IGeometryDef geometryDef = new GeometryDefClass();
        IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
        geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
        ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
        ISpatialReference spatialReference = spatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_WGS1984UTM_21S);
        ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
        spatialReferenceResolution.ConstructFromHorizon();
        spatialReferenceResolution.SetDefaultXYResolution();
        ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
        spatialReferenceTolerance.SetDefaultXYTolerance();
        geometryDefEdit.SpatialReference_2 = spatialReference;

        // Add a geometry field to the fields collection. This is where the geometry definition is applied.
        IField geometryField = new FieldClass();
        IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
        geometryFieldEdit.Name_2 = "Shape";
        geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
        geometryFieldEdit.GeometryDef_2 = geometryDef;
        fieldsEdit.AddField(geometryField);

        // Create a Name text field for the fields collection.
        IField valorField = new FieldClass();
        IFieldEdit valorFieldEdit = (IFieldEdit)valorField;
        valorFieldEdit.Name_2 = "Valor";
        valorFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
        fieldsEdit.AddField(valorField);

        // Use IFieldChecker to create a validated fields collection.
        IFieldChecker fieldChecker = new FieldCheckerClass();
        IEnumFieldError enumFieldError = null;
        IFields validatedFields = null;
        fieldChecker.ValidateWorkspace = (IWorkspace)featureWorkspace;
        fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

        // The enumFieldError enumerator can be inspected at this point to determine 
        // which fields were modified during validation.

        // Create the feature class. Note that the CLSID parameter is null—this indicates to use the
        // default CLSID, esriGeodatabase.Feature (acceptable in most cases for feature classes).
        IFeatureClass featureClass = null;
        try
        {
            featureClass = featureWorkspace.CreateFeatureClass(featureClassName, validatedFields, null, ocDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple, "Shape", "");
        }
        catch (System.Runtime.InteropServices.COMException e)
        {
            MessageBox.Show(e.Message.ToString());
        }

        return featureClass;
    }

    private void cargarValoresPuntosMuestreo(IMap map, Muestreo muestreo, String nombreCapaPuntosZonificacion, String nombreCapaPoligonos, String nombreCapaPuntosMuestreos,
                                             int indiceAtributoEnTablaPoligonos, int indiceAtributoEnTablaPuntos, ProgressBar pBar)
    {

        //se crea el campo Promedio en la capa de Puntos de Muestreos
        int indicePromedioFieldPuntosMuestreo = this.crearFieldAFeatureClass(this.capaPuntosMuestreo.FeatureClass, "Valor", esriFieldType.esriFieldTypeDouble);
        //int indicePromedioFieldPuntosMuestreo = this.crearFieldAFeatureClass(featureclassPuntosMuestreo, "Valor", esriFieldType.esriFieldTypeDouble);
        
        //Creo el updateCursorPoligono para iterar en las filas de los poligonos (poligono por poligono).
        IFeatureCursor updateCursorPoligono = this.capaPoligonos.FeatureClass.Update(null, false);
        //IFeatureCursor updateCursorPoligono = ifeaturelayerPoligono.FeatureClass.Update(null, false);

        //Creo el updateCursorPuntosMuestreo para iterar en las filas de los puntos de muestreo (punto por punto).
        IFeatureCursor updateCursorPuntosMuestreo = this.capaPuntosMuestreo.FeatureClass.Update(null, false);
        //IFeatureCursor updateCursorPuntosMuestreo = ifeaturelayerPuntosMuestreo.FeatureClass.Update(null, false);

        IFeature poligonoIFeature = null;
        IFeature puntosMuestreoIFeature = null;
        try
        {
            //itero en cada poligono de la capa de red
            //si la interseccion devuelve vacia, se elimina el poligono
            //en caso contrario se calcula y se setea el valor del promedio.
            while ((poligonoIFeature = updateCursorPoligono.NextFeature()) != null)
                {
                //se actura el cursos de puntos de muestreo
                puntosMuestreoIFeature = updateCursorPuntosMuestreo.NextFeature();

                //se prepara la interseccion
                IGeometry searchGeometry = poligonoIFeature.Shape;

                // se crea el spatial query filter
                ISpatialFilter spatialFilter = new SpatialFilterClass();

                // specify the geometry to query with
                spatialFilter.Geometry = searchGeometry; //poligono
                // specify what the geometry field is called on the Feature Class that we will be querying against
                System.String nameOfShapeField = this.capaPuntosZonificacion.ShapeFieldName;
                //System.String nameOfShapeField = featureclassPuntos.ShapeFieldName;
                spatialFilter.GeometryField = nameOfShapeField;
                // specify the type of spatial operation to use
                spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                // perform the query and use a cursor to hold the results
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter = (IQueryFilter)spatialFilter;
                IFeatureCursor featureCursor = this.capaPuntosZonificacion.Search(queryFilter, false);
                //IFeatureCursor featureCursor = featureclassPuntos.Search(queryFilter, false);

                IFeature selPuntosFeature = null;
                float resultado = 0;
                int cantidadPuntos = 0;

                selPuntosFeature = featureCursor.NextFeature();
                while (selPuntosFeature != null)
                {
                    resultado += (float)Convert.ToDecimal(selPuntosFeature.get_Value(2));//2 es el indiceAtributoEnPuntos
                    cantidadPuntos++;
                    selPuntosFeature = featureCursor.NextFeature();
                }

                pBar.PerformStep();
                if (cantidadPuntos > 0)
                {
                    //se calcula el promedio
                    double promedio = resultado / cantidadPuntos;

                    //se setea el promedio en la capa de puntos de muestreo
                    puntosMuestreoIFeature.set_Value(indicePromedioFieldPuntosMuestreo, (Double)promedio);
                    updateCursorPuntosMuestreo.UpdateFeature(puntosMuestreoIFeature);

                    //se crea un punto de Muestreo
                    PuntoMuestreo puntoMuestreo = new PuntoMuestreo();
                    puntoMuestreo.setValor(promedio);
                    IGeometry geometry = puntosMuestreoIFeature.Shape;
                    IPoint ipoint = geometry as IPoint;

                    puntoMuestreo.setCoordenadaXY(ipoint.X,ipoint.Y);

                    //se agrega el punto de muestreo al muestreo
                    muestreo.agregarPuntoMuestreo(puntoMuestreo);
                }
                else
                {
                    //se borra el punto de muestreo que no tenga promedio
                    puntosMuestreoIFeature.Delete();
                }
            }
            ////se borra la layer de poligonos
            //map.DeleteLayer(layerPoligono);

            ////se borra la layer de Puntos
            //map.DeleteLayer(layerPuntos);
        }
        catch //(COMException comExc)
        {
            // Handle any errors that might occur on NextFeature().
        }

        // If the cursor is no longer needed, release it.
        Marshal.ReleaseComObject(updateCursorPuntosMuestreo);
    }


    //crea un nuevo field con el nombre nombreField en el featureClass pasado como parametro
    //devuelve el indice del field creado
    private int crearFieldAFeatureClass(IFeatureClass featureClass, String nombreField, esriFieldType tipoField)
    {
        IField field = new FieldClass();
        IFieldEdit fieldEdit = (IFieldEdit)field;
        fieldEdit.Name_2 = nombreField;
        fieldEdit.Type_2 = tipoField;

        ISchemaLock schemaLock = (ISchemaLock)featureClass;
        try
        {
            // A try block is necessary, as an exclusive lock may not be available.
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
            // Add the field.
            featureClass.AddField(field);
        }
        catch (Exception exc)
        {
            // Handle this in a way appropriate to your application.
            Console.WriteLine(exc.Message);
        }
        finally
        {
            // Set the lock to shared, whether or not an error occurred.
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
        }
        return featureClass.FindField(nombreField);
    }
    
    //devuelve el IFeatureClass correspondiente a la capa de puntos de muestreo (sin optimizar).
    private void crearRed(IMap targetMap, string nombreCapaPoligonos, string nombreCapa, IPoint puntoOrigen, IPoint puntoOpuesto, bool filasColumnas, int vertical, int horizontal, bool selectable, string capaZonificacion)
    {
        Geoprocessor gp = new Geoprocessor();

        ESRI.ArcGIS.DataManagementTools.CreateFishnet fishNet = new ESRI.ArcGIS.DataManagementTools.CreateFishnet();
        fishNet.out_feature_class = this.wsZonif.PathName + "\\" + nombreCapaPoligonos;

        fishNet.origin_coord = puntoOrigen.X.ToString() + " " + puntoOrigen.Y.ToString();


        double medio = (puntoOpuesto.Y + puntoOrigen.Y)/2;
        fishNet.y_axis_coord = puntoOrigen.X.ToString() + " " + medio.ToString();
        fishNet.corner_coord = puntoOpuesto.X.ToString() + " " + puntoOpuesto.Y.ToString();
        if (filasColumnas)
        {
            fishNet.number_rows = vertical;
            fishNet.number_columns = horizontal;
        
            fishNet.cell_height = 0;
            fishNet.cell_width = 0;
        }
        else
        {
            fishNet.cell_height = vertical;
            fishNet.cell_width = horizontal;
            fishNet.number_rows = 0;
            fishNet.number_columns = 0;
        }
        fishNet.out_label = nombreCapaPoligonos;
        fishNet.geometry_type = "POLYGON";
        fishNet.template = this.capaPuntosZonificacion;
        gp.AddOutputsToMap = false;
        gp.OverwriteOutput = true;

        IFeatureClass fc0;
        IFeatureClass fc1;
        IQueryFilter qf;
        try
        {
            IGPUtilities gpUtils = new GPUtilitiesClass();

            IGeoProcessorResult result = (IGeoProcessorResult)gp.Execute(fishNet, null);
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc0, out qf);
            gpUtils.DecodeFeatureLayer(result.GetOutput(1), out fc1, out qf);

            //cambio el nombre de la capa resultado
            ESRI.ArcGIS.DataManagementTools.Rename rename = new ESRI.ArcGIS.DataManagementTools.Rename();
            rename.in_data = this.wsZonif.PathName + "\\" + this.nombreCapaPuntosMuestreo + ".shp";
            rename.out_data = this.wsZonif.PathName + "\\" + nombreCapa + ".shp";
            Geoprocessor g = new Geoprocessor();
            g.Execute(rename, null);

            //se busca la capa de poligonos para setear en controlador
            IFeatureLayer fLayer0 = new FeatureLayerClass();
            fLayer0.FeatureClass = fc0;
            this.capaPoligonos = fLayer0;

            //se busca la capa de puntos de muestreo para retornar
            IFeatureLayer fLayer1 = new FeatureLayerClass();
            fLayer1.FeatureClass = fc1;
            this.capaPuntosMuestreo = fLayer1;

            //se agrega al map la capa de puntos de muestreo (la de puntos, no la de poligonos)
            ILayer layer = (ILayer)fLayer1;
            layer.Name = fLayer1.FeatureClass.AliasName;
            targetMap.AddLayer(layer);

            ESRI.ArcGIS.Carto.IActiveView activeView = (ESRI.ArcGIS.Carto.IActiveView)targetMap;
            activeView.Refresh();
            
            //se setea en el controlador el nombre de la capa de puntos de muestreo elegida por el usuario.
            this.nombreCapaPuntosMuestreo = nombreCapa;
        }
        catch (NullReferenceException e)
        {
            System.Diagnostics.Debug.WriteLine("{0} Caught exception #1." + e);
        }
        catch
        {
            for (int i = 0; i < gp.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp.GetMessage(i));
        }
    }

    //cargar las variables del archivo .zf
    public List<string> cargarVariables(string rutaZF)
    {
        List<string> listaVariables = new List<string>();
        //Obtengo el archivo
        StreamReader objReader = new StreamReader(rutaZF);
        //Incicializo la variable donde voy a guardar cada linea que leo y la variable donde voy a guardar en memoria el contenido del archivo
        string sLine = "";
        int cant_variables = 0;
        string string_cant_variables = "VarQty:";

        string comienzo_datos = "[Cells]";

        //Leo la linea actual del archivo
        sLine = objReader.ReadLine();

        //leo hasta la etiqueta [Cells] y saco los valores de rows, cols y cant_variables 
        while (sLine != null)
        {
            if (((sLine != "") && (sLine.Length >= string_cant_variables.Length) && sLine.Substring(0, string_cant_variables.Length) == string_cant_variables))
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
                        listaVariables.Add(nombreVariable);
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

        return listaVariables;
    }

    public List<string> cargarCapasMuestreo() 
    {
        List<string> listaCapas = new List<string>();

        IMap targetMap = ArcMap.Document.FocusMap;

        //cargo el combo de capas abiertas
        IEnumLayer enumLayers = targetMap.get_Layers();
        enumLayers.Reset();
        ILayer layer = enumLayers.Next();

        IGeometryDef geometryDef = new GeometryDefClass();
        IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
        geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
        while (layer != null)
        {
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            if (featureLayer != null)
            {
                IFeatureClass fc = featureLayer.FeatureClass;
                if (fc.FindField("Valor") != -1 && fc.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    listaCapas.Add(layer.Name.ToString());
                }
            }
            layer = enumLayers.Next();
        }
        return listaCapas;
    }

    public int calcularArea(string nombreCapa) 
    {
        
        Geoprocessor gp = new Geoprocessor();
        ESRI.ArcGIS.DataManagementTools.MinimumBoundingGeometry poligono = new ESRI.ArcGIS.DataManagementTools.MinimumBoundingGeometry();
        poligono.geometry_type = "CONVEX_HULL";
        poligono.group_option = "ALL";
        poligono.in_features = nombreCapa;
        gp.TemporaryMapLayers = true;
        gp.AddOutputsToMap = false;
        //poligono.out_feature_class;
        IFeatureClass fc;
        IQueryFilter qf;
        try
        {
            IGPUtilities gpUtils = new GPUtilitiesClass();
            IGeoProcessorResult result = (IGeoProcessorResult)gp.Execute(poligono, null);
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc, out qf);
            IFeatureCursor cursorPoligono = fc.Search(null, false);
            int indice = fc.FindField("Shape_Area");
            IFeature datosPoligono = cursorPoligono.NextFeature();
            while (datosPoligono != null)
            {
                this.area = (double)datosPoligono.get_Value(indice);
                datosPoligono = cursorPoligono.NextFeature();
            }

            /*se elimina la feature class del poligono creado*/
            if (((IDataset)fc).CanDelete())
            {
                ((IDataset)fc).Delete();
            }

            if (this.rango == -1)
            {
                return -1;
            }
            else
            {
                return calcularNroMuestras();
            }
            
        }
        catch 
        {
            for (int i = 0; i < gp.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp.GetMessage(i));
            return -1;
        }

    }

    private IFeatureClass crearPoligonoExtension(string nombreCapa, string nomSalida, int distAgregacion)
    {
        Geoprocessor gp = new Geoprocessor();
        ESRI.ArcGIS.CartographyTools.AggregatePoints poligonoExt = new ESRI.ArcGIS.CartographyTools.AggregatePoints();
        poligonoExt.in_features = this.capaPuntosZonificacion;
        poligonoExt.out_feature_class = this.wsZonif.PathName + "\\" + nomSalida + ".shp";
        poligonoExt.aggregation_distance = distAgregacion * 2;
        gp.TemporaryMapLayers = true;
        gp.AddOutputsToMap = false;
        
        IFeatureClass fc;
        IQueryFilter qf;
        try
        {
            IGPUtilities gpUtils = new GPUtilitiesClass();
            IGeoProcessorResult result = (IGeoProcessorResult)gp.Execute(poligonoExt, null);
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc, out qf);
            return fc;
        }   
        catch
        {
            for (int i = 0; i < gp.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp.GetMessage(i));
            return null;
        }

    }

    private void quitarPuntosExternos(string nomCapaMuestreo, IFeatureClass extensionCampo)
    {
        IGeoProcessor2 gp = new GeoProcessorClass();
        // Create an IVariantArray to hold the parameter values.
        IVariantArray parameters = new VarArrayClass();
        try
        {
            // Create the geoprocessor.
            // Populate the variant array with parameter values.
            parameters.Add(nomCapaMuestreo);
            parameters.Add(extensionCampo);
            parameters.Add("OUTSIDE");
            // Execute the tool.
            gp.Execute("ErasePoint", parameters, null);

            //borro la capa de extension, ya que no se usa mas
            if (((IDataset)extensionCampo).CanDelete())
            {
                ((IDataset)extensionCampo).Delete();
            }

        }
        catch
        {
            for (int i = 0; i < gp.MessageCount; i++)
            {
                System.Diagnostics.Debug.WriteLine(gp.GetMessage(i));
            }
        }

    }

    public int setearRango(int r) {
        this.rango = r;
        if (this.area == -1)
            return -1;
        else
            return calcularNroMuestras();
    }

    public int calcularNroMuestras()
    {
        int muestras = (int)(Math.Round(this.area * 4 / Math.Pow(this.rango, 2)));
        //this.ssa.cantMuestras = muestras;
        return muestras;
    }

    //GONZALO: ya la pase a BlackmoreControlador
    public List<DTCapasBlackmore> cargarCapasBlackmore()
    {
        List<DTCapasBlackmore> listaCapas = new List<DTCapasBlackmore>();

        IMap targetMap = ArcMap.Document.FocusMap;

        //cargo el combo de capas abiertas
        IEnumLayer enumLayers = targetMap.get_Layers();
        enumLayers.Reset();
        ILayer layer = enumLayers.Next();

        while (layer != null)
        {
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            if (featureLayer != null)
            {
                IFeatureClass fc = featureLayer.FeatureClass;
                if (fc.Fields.FieldCount > 0)
                {
                    DTCapasBlackmore dtCapa = new DTCapasBlackmore();
                    dtCapa.setCapa(layer);
                    dtCapa.setNombreCapa(layer.Name.ToString());
                    dtCapa.setCapaBase(false);
                    bool tieneAtributos = false;

                    for (int i = 0; i < fc.Fields.FieldCount; i++)
                    {
                        IField field = fc.Fields.get_Field(i);
                        if (field.Type == esriFieldType.esriFieldTypeDouble || field.Type == esriFieldType.esriFieldTypeInteger ||
                            field.Type == esriFieldType.esriFieldTypeSmallInteger || field.Type == esriFieldType.esriFieldTypeSingle)
                        {
                            dtCapa.agregarAtributo(field.Name.ToString());
                            tieneAtributos = true; 
                        }
                    }

                    if (tieneAtributos)
                        listaCapas.Add(dtCapa);
                }
            }
            layer = enumLayers.Next();
        }
        return listaCapas;
    }

    //GONZALO: ya la pase a BlackmoreControlador
    public IFeatureClass unionEspacial(IFeatureClass entidadDestino, ILayer entidadUnion, string entidadSalida, bool mantenerEntidades, string nombreAtributo, string atributoTablaUnion)
    {
        Geoprocessor gpt = new Geoprocessor();
        try
        {
            IFeatureLayer featureLayerUnion = entidadUnion as IFeatureLayer;
            IFeatureClass featureClassUnion = featureLayerUnion.FeatureClass;
            IDataset dsUnion = (IDataset)entidadUnion;
            string pathUnion = dsUnion.Workspace.PathName + @"\" + dsUnion.Name + ".shp";

            IGPUtilities gputilities = new GPUtilitiesClass();
            // Initialize the GPFieldMapping
            IGPFieldMapping fieldmapping = new GPFieldMappingClass();
            // Create a new output field
            IDETable inputTableA = (IDETable)gputilities.MakeDataElement(pathUnion, null, null);

            // Create a new FieldMap
            IGPFieldMap campoMap = new GPFieldMapClass();
            campoMap.MergeRule = ESRI.ArcGIS.Geoprocessing.esriGPFieldMapMergeRule.esriGPFieldMapMergeRuleMean;
            
            
            campoMap.AddInputField(inputTableA, featureClassUnion.Fields.get_Field(featureClassUnion.FindField(nombreAtributo)), -1, -1); // this.capaPuntosMuestreo.FeatureClass.Fields.get_Field(this.capaPuntosMuestreo.FeatureClass.FindField("Valor")), -1, -1);

            IFieldEdit fieldEdit = new FieldClass();
            int indice = campoMap.FindInputField(inputTableA, featureClassUnion.Fields.get_Field(featureClassUnion.FindField(nombreAtributo)).Name);
            fieldEdit.Name_2 = atributoTablaUnion;
            fieldEdit.Type_2 = campoMap.Fields.get_Field(indice).Type;

            campoMap.OutputField = fieldEdit;

            fieldmapping.AddFieldMap(campoMap);

            ESRI.ArcGIS.AnalysisTools.SpatialJoin sJoin = new ESRI.ArcGIS.AnalysisTools.SpatialJoin();
            sJoin.target_features = entidadDestino;
            sJoin.join_features = featureClassUnion; //@"C:\Users\Martin\Desktop\Nueva carpeta\saleotraaa.shp";//this.nombreCapaPuntosMuestreo;
            sJoin.field_mapping = fieldmapping; //"Valor \"Valor\" true true false 19 Double 0 0 ,Mean,#," + @"C:\Users\\Martin\Desktop\Proyecto Grado\New folder\20110608_153342\salida50.shp" + ",Valor,-1,-1";
            sJoin.join_operation = "JOIN_ONE_TO_ONE";
            if (!mantenerEntidades)
            {
                sJoin.join_type = "KEEP_COMMON";
            }
            else
            {
                sJoin.join_type = "KEEP_ALL";
            }
            sJoin.match_option = "INTERSECT";
            //String hora = System.DateTime.Now.ToString("HHmmss");
            sJoin.out_feature_class = entidadSalida;//wsZonif.PathName + "\\" + hora + "join.shp";


            IFeatureClass fc;
            IQueryFilter qf;
            IGPUtilities gpUtils = new GPUtilitiesClass();

            IGeoProcessorResult result = (IGeoProcessorResult)gpt.Execute(sJoin, null);
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc, out qf);

            for (int i = 0; i < gpt.MessageCount; i++)
            {
                System.Diagnostics.Debug.WriteLine(gpt.GetMessage(i));
            }


            return fc;
        }
        catch
        {
            for (int i = 0; i < gpt.MessageCount; i++)
            {
                System.Diagnostics.Debug.WriteLine(gpt.GetMessage(i));
            }

            return null;
        }

    }

    //GONZALO: ya la pase a BlackmoreControlador, aunque me parece q tendria q estar en Blackmore
    public void setValoresFeatureUnion(IFeatureClass featureUnion, int fid, int indiceDst, double dsv, int indiceMean, double mean, int indiceClasificacion, int clase)
    {
        IQueryFilter queryFilter = new QueryFilterClass();
        queryFilter.WhereClause = "FID = " + fid.ToString();
        IFeatureCursor featureCursor = featureUnion.Update(queryFilter, false);
        IFeature celdaFeature = featureCursor.NextFeature();
        if (celdaFeature != null)
        {
            celdaFeature.set_Value(indiceDst, dsv);
            celdaFeature.set_Value(indiceMean, mean);
            celdaFeature.set_Value(indiceClasificacion, clase);

        }
        featureCursor.UpdateFeature(celdaFeature);

    }
}
