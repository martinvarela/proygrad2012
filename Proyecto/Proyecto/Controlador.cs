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
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;


class Controlador
{
    private static Controlador instancia;
    private IWorkspace wsSSA;
    private Controlador() 
    {
        IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
        //esta ruta la indica el usuario
        IWorkspace workspace = workspaceFactory.OpenFromFile("C:\\temp\\Sample", 0);
        this.wsSSA = workspace;
        this.ssa = new SSA(this.wsSSA);
        
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
    //private List<PuntoMuestreo> puntosMuestreo;
    private Muestreo muestreo { get; set; }
    private Blackmore blackmore;


    public SSA getSSA() { return this.ssa; }
    public void setSSA(SSA s) { this.ssa = s; }


    private double mediaCapas { get; set; }
    private void setearMedias() 
    {
        this.mediaCapas = 0;

        if (this.capas != null)
        {
            double temp = 0;
            int cant = 0;
            foreach (Entrada entrada in this.capas)
            {
                temp += entrada.setearMedias();
                cant++;
            }

            if (cant > 0)
                this.mediaCapas = temp / cant;
        }
    }
    //private void crearPuntosMuestreo() { }

    //se crea la instancia Muestreo con su respectiva lista de posibles puntos de Muestreos.
    //se devuelve en el arcmap la capa "CR-hhMMss" que se cambiara por el nombre pasado como parametro que contiene los posibles puntos de muestreo(todos) 
    //para realizar de forma manual el semivariograma de forma de encontrar cual es el RANGO.
    public Muestreo crearPuntosMuestreo(bool conRed, String rutaEntrada, bool filasColumnas, int vertical, int horizontal, List<int> variablesMarcadas, ProgressBar pBar, Label lblProgressBar)
    {
        IMap map = ArcMap.Document.FocusMap;
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

        this.nombreCapaPuntosZonificacion = "PZ_" + ahora;
        lblProgressBar.Text = "Creando layer con los puntos de zonificación...";
        this.capaPuntosZonificacion = this.crearCapaPuntosZonificacion(map, nombreCapaPuntosZonificacion, zonificacion.PuntosZonificacion, pBar);
        lblProgressBar.Text = "";

        this.nombreCapaPoligonos = "CR_" + ahora;
        this.nombreCapaPuntosMuestreo = "CR_" + ahora + "_label";

        if (conRed)
        {
            //se crea la capa de red con las filas y columnas pasadas como parametro
            //se carga en el controlar la capaPoligonos y capaPuntosMuestreo
            this.crearRed(map, this.nombreCapaPoligonos, zonificacion.PuntoOrigen, zonificacion.PuntoOpuesto, filasColumnas, vertical, horizontal, true, this.nombreCapaPuntosZonificacion);

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

            pBar.Visible = false;
            lblProgressBar.Text = "";
     
        }
        else
        {
            //esto en el caso de que se quiera trabajar con todos los puntos de zonificacion.
            this.capaPuntosMuestreo = this.capaPuntosZonificacion as FeatureLayer;
        }
        return muestreo;
    }

    //capaPuntosMuestreo es la capa seleccionada por el usuario
    //metodoInterpolacion puede ser IDW o Kriging
    //rango ??? o cantmuestras
    //error maximo aceptado en % ej: 5
    public void optimizarMuestreo(IFeatureClass capaPuntosMuestreo, String metodoInterpolacion, int rango, double error)
    {
       IFeatureClass resultado = this.ssa.SimulatedAnnealing(capaPuntosMuestreo, metodoInterpolacion, rango, error);    
    } 

    public void crearBlackmore(bool filaColumna, int vertical, int horizontal)
    {
        this.blackmore = new Blackmore(filaColumna, vertical, horizontal);
        List<Celda> celdas = this.blackmore.getCeldas();
        foreach (Celda c in celdas)
        {
            foreach (Entrada e in this.capas)
            {
                DTDatosDM datosCelda = e.calcularDsYMedia(c);
                this.blackmore.setDatos(c, datosCelda);
            }
        }
    }

    private IFeatureClass crearCapaPuntosZonificacion(IMap map, string nombreFeatureClass, List<PuntoZonificacion> listaPuntos, ProgressBar pBar)
    {
        IWorkspace ws = ((IDataset)map.Layer[0]).Workspace;
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

        IFeatureLayer featureLayer = new FeatureLayerClass();
        featureLayer.FeatureClass = nuevaFeatureClass;

        ILayer layer = (ILayer)featureLayer;
        layer.Name = featureLayer.FeatureClass.AliasName;
        // Add the Layer to the focus map
        map.AddLayer(layer);

        ESRI.ArcGIS.Carto.IActiveView activeView = (ESRI.ArcGIS.Carto.IActiveView)map;
        activeView.Refresh();

        pBar.Visible = false;

        return nuevaFeatureClass;
    }

    private IFeatureClass crearCapaPuntosMuestreo(IMap map, string nombreFeatureClass, List<PuntoMuestreo> listaPuntos)
    {

        IWorkspace ws = ((IDataset)map.Layer[0]).Workspace;
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
            featureClass = featureWorkspace.CreateFeatureClass(featureClassName,
                                                                                validatedFields, null, ocDescription.ClassExtensionCLSID,
                                                                                esriFeatureType.esriFTSimple, "Shape", "");
        }
        catch (System.Runtime.InteropServices.COMException e)
        {
            MessageBox.Show(e.Message.ToString());
        }

        return featureClass;
    }

    private void cargarValoresPuntosMuestreo(IMap map,
                                             Muestreo muestreo,
                                             String nombreCapaPuntosZonificacion,
                                             String nombreCapaPoligonos,
                                             String nombreCapaPuntosMuestreos,
                                             int indiceAtributoEnTablaPoligonos,
                                             int indiceAtributoEnTablaPuntos,
                                             ProgressBar pBar)
    {
        IEnumLayer enumlayers = map.get_Layers();

        //se busca la capa de puntos
        enumlayers.Reset();
        ILayer layerPuntos = enumlayers.Next();
        while ((layerPuntos != null) && (layerPuntos.Name != nombreCapaPuntosZonificacion))
        {

            layerPuntos = enumlayers.Next();
        }
        IFeatureLayer ifeaturelayerPuntos = layerPuntos as FeatureLayer;
        IFeatureClass featureclassPuntos = ifeaturelayerPuntos.FeatureClass;

        //se busca la capa de poligonos
        enumlayers.Reset();
        ILayer layerPoligono = enumlayers.Next();
        while ((layerPoligono != null) && (layerPoligono.Name != nombreCapaPoligonos))
        {
            layerPoligono = enumlayers.Next();
        }
        IFeatureLayer ifeaturelayerPoligono = layerPoligono as FeatureLayer;
        IFeatureClass featureclassPoligono = ifeaturelayerPoligono.FeatureClass;

        //se busca la capa de etiquetas de poligonos, seran los futuros puntos de muestreo
        enumlayers.Reset();
        ILayer layerPuntosMuestreo = enumlayers.Next();
        while ((layerPuntosMuestreo != null) && (layerPuntosMuestreo.Name != nombreCapaPuntosMuestreos))
        {
            layerPuntosMuestreo = enumlayers.Next();
        }
        IFeatureLayer ifeaturelayerPuntosMuestreo = layerPuntosMuestreo as FeatureLayer;
        IFeatureClass featureclassPuntosMuestreo = ifeaturelayerPuntosMuestreo.FeatureClass;

        //se crea el campo Promedio en la capa de Puntos de Muestreos
        int indicePromedioFieldPuntosMuestreo = this.crearFieldAFeatureClass(featureclassPuntosMuestreo, "Valor", esriFieldType.esriFieldTypeDouble);

        //Creo el updateCursorPoligono para iterar en las filas de los poligonos (poligono por poligono).
        IFeatureCursor updateCursorPoligono = ifeaturelayerPoligono.FeatureClass.Update(null, false);


        //Creo el updateCursorPuntosMuestreo para iterar en las filas de los puntos de muestreo (punto por punto).
        IFeatureCursor updateCursorPuntosMuestreo = ifeaturelayerPuntosMuestreo.FeatureClass.Update(null, false);

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
                System.String nameOfShapeField = featureclassPuntos.ShapeFieldName;
                spatialFilter.GeometryField = nameOfShapeField;
                // specify the type of spatial operation to use
                spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                // perform the query and use a cursor to hold the results
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter = (IQueryFilter)spatialFilter;
                IFeatureCursor featureCursor = featureclassPuntos.Search(queryFilter, false);

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
            //se borra la layer de poligonos
            map.DeleteLayer(layerPoligono);

            //se borra la layer de Puntos
            map.DeleteLayer(layerPuntos);
        }
        catch (COMException comExc)
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
    private void crearRed(IMap targetMap,
                          string nombreCapaPoligonos,
                          IPoint puntoOrigen,
                          IPoint puntoOpuesto,
                          bool filasColumnas,
                          int vertical,
                          int horizontal,
                          bool selectable,
                          string capaZonificacion)
    {
        Geoprocessor gp = new Geoprocessor();

        ESRI.ArcGIS.DataManagementTools.CreateFishnet fishNet = new ESRI.ArcGIS.DataManagementTools.CreateFishnet();
        fishNet.out_feature_class = nombreCapaPoligonos;

        fishNet.origin_coord = puntoOrigen.X.ToString() + " " + puntoOrigen.Y.ToString();

        double medio = (puntoOpuesto.Y + puntoOrigen.Y)/2;
        fishNet.y_axis_coord = puntoOrigen.X.ToString() + " " + medio.ToString();
        fishNet.corner_coord = puntoOpuesto.X.ToString() + " " + puntoOpuesto.Y.ToString();
        fishNet.cell_width = 0;
        fishNet.cell_height = 0;
        if (filasColumnas)
        {
            fishNet.number_rows = vertical;
            fishNet.number_columns = horizontal;
        }
        else
        {
            fishNet.cell_height = vertical;
            fishNet.cell_width = horizontal;
        }
        fishNet.out_label = nombreCapaPoligonos;
        fishNet.geometry_type = "POLYGON";
        fishNet.template = capaZonificacion;
        gp.AddOutputsToMap = true;
        gp.OverwriteOutput = true;

        try
        {
            gp.Execute(fishNet, null);
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

        //se busca la capa de poligonos para setear en controlador
        IEnumLayer enumlayers = targetMap.get_Layers();
        enumlayers.Reset();
        ILayer layerPoligonos = enumlayers.Next();
        while ((layerPoligonos != null) && (layerPoligonos.Name != nombreCapaPoligonos))
        {
            layerPoligonos = enumlayers.Next();
        }
        IFeatureLayer ifeaturelayerPoligonos = layerPoligonos as FeatureLayer;
        this.capaPoligonos = ifeaturelayerPoligonos;

        //se busca la capa de puntos de muestreo para retornar
        enumlayers.Reset();
        ILayer layerPuntos = enumlayers.Next();
        String nombreCapaPuntosMuestreo = nombreCapaPoligonos + "_label";
        while ((layerPuntos != null) && (layerPuntos.Name != nombreCapaPuntosMuestreo))
        {
            layerPuntos = enumlayers.Next();
        }
        IFeatureLayer ifeaturelayerPuntos = layerPuntos as FeatureLayer;
        this.capaPuntosMuestreo = ifeaturelayerPuntos;
    }

}
