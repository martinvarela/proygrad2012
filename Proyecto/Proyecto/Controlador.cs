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

class Controlador
{
    //atributos
    private Zonificacion zonificacion { get; set; }
    private List<Capa> capas;
    //private List<PuntoMuestreo> puntosMuestreo;
    private Muestreo muestreo { get; set; }
    private Blackmore blackmore;

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

    private void crearPuntosMuestreo()
    {

    }

    public Muestreo muestreoOptimoFilasColumnas(String rutaEntrada, int filas, int columnas, List<int> variablesMarcadas)
    {
      
        //paso 1
        Zonificacion zonificacion = new Zonificacion(rutaEntrada, variablesMarcadas);

        //paso 2
        zonificacion.calcularVariabilidad();

        //paso 3 - crear Puntos de Muestreo
        Muestreo muestreo = new Muestreo();

        //paso 4
        //se crea una layer temporal con los puntos de zonificacion sacados del .ZF
        IMap map = ArcMap.Document.FocusMap;
        String ahora = System.DateTime.Now.ToString("HHmmss");
        String nombreCapaPuntosZonificacion = "PZ-" + ahora;
        IFeatureClass capaPuntosZonificacion = this.crearCapaPuntosZonificacion(map, nombreCapaPuntosZonificacion, zonificacion.PuntosZonificacion);

        //se crea la capa de red con las filas y columnas pasadas como parametro
        //hacer!!
        String nombreCapaPoligonos = "CR" + ahora;
        String nombreCapaPuntosPosibles = "CR" + ahora + "_label";
        this.crearRed(map, nombreCapaPoligonos, zonificacion.PuntoOrigen, zonificacion.PuntoOpuesto, filas, columnas, true, nombreCapaPuntosZonificacion);
        
        IFeatureClass capaPuntosPosibles;
        IEnumLayer enumlayers = map.get_Layers();

        //se busca la capa de puntos de muestreo
        enumlayers.Reset();
        ILayer layerPuntos = enumlayers.Next();
        while ((layerPuntos != null) && (layerPuntos.Name != nombreCapaPuntosPosibles))
        {
            layerPuntos = enumlayers.Next();
        }
        IFeatureLayer ifeaturelayerPuntos = layerPuntos as FeatureLayer;
        capaPuntosPosibles = ifeaturelayerPuntos.FeatureClass;
        

        //paso 5
        //calcula los valores de los puntos de muestreo haciendo promedio en los puntos que "caen" dentro de la celda de la capa de poligonos
        //agrega cada punto de muestreo a la lista de la instancia de muestreo.
        this.cargarValoresPuntosMuestreo(map, muestreo, nombreCapaPuntosZonificacion, nombreCapaPoligonos, nombreCapaPuntosPosibles, 2, 2);


        //paso6
        //SSA
        SSA ssa = new SSA();
        List<PuntoMuestreo> puntosMuestrear = ssa.SimulatedAnnealing2(this.muestreo.PuntosMuestreo);
        
        //CREAR LA CAPA CON LOS PUNTOS A MUESTREAR Y MOSTRARLA EN EL MAPA

        return new Muestreo();
    
    }
    public Muestreo muestreoOptimoAltoAncho(String rutaEntrada, int alto, int ancho, List<int> variablesMarcadas)
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

    public IFeatureClass crearCapaPuntosZonificacion(IMap map, string nombreFeatureClass, List<PuntoZonificacion> listaPuntos)
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
        foreach (PuntoZonificacion aux in listaPuntos)
        {
            point = new ESRI.ArcGIS.Geometry.PointClass();
            point.X = aux.Coordenada.X;
            point.Y = aux.Coordenada.Y;

            featureBuffer.Shape = point;
            featureBuffer.set_Value(featureBuffer.Fields.FindField("Valor"), aux.Variabilidad);

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

    public IFeatureClass crearCapaPuntosMuestreo(IMap map, string nombreFeatureClass, List<PuntoMuestreo> listaPuntos)
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
            point.X = aux.Coordenada.X;
            point.Y = aux.Coordenada.Y;

            featureBuffer.Shape = point;
            featureBuffer.set_Value(featureBuffer.Fields.FindField("Valor"), aux.Valor);

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

    public IFeatureClass crearFeatureClassConFields(String featureClassName, IFeatureWorkspace featureWorkspace)
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
        IFeatureClass featureClass = featureWorkspace.CreateFeatureClass(featureClassName,
                                                                            validatedFields, null, ocDescription.ClassExtensionCLSID,
                                                                            esriFeatureType.esriFTSimple, "Shape", "");

        return featureClass;
    }

    public void cargarValoresPuntosMuestreo(IMap map, 
                                            Muestreo muestreo,
                                            String nombreCapaPuntosZonificacion, 
                                            String nombreCapaPoligonos, 
                                            String nombreCapaPuntosMuestreos, 
                                            int indiceAtributoEnTablaPoligonos, 
                                            int indiceAtributoEnTablaPuntos)
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
        int indicePromedioFieldPuntosMuestreo = this.crearFieldAFeatureClass(featureclassPuntosMuestreo, "Promedio", esriFieldType.esriFieldTypeDouble);

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

                if (cantidadPuntos > 0)
                {
                    //se calcula el promedio
                    double promedio = resultado / cantidadPuntos;

                    //se setea el promedio en la capa de puntos de muestreo
                    puntosMuestreoIFeature.set_Value(indicePromedioFieldPuntosMuestreo, (Double)promedio);
                    updateCursorPuntosMuestreo.UpdateFeature(puntosMuestreoIFeature);

                    //se crea un punto de Muestreo
                    PuntoMuestreo puntoMuestreo = new PuntoMuestreo();
                    puntoMuestreo.Valor = promedio;
                    IGeometry geometry = puntosMuestreoIFeature.Shape;
                    IPoint ipoint = geometry as IPoint;
                    puntoMuestreo.Coordenada.X = ipoint.X;
                    puntoMuestreo.Coordenada.Y = ipoint.Y;

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
    public int crearFieldAFeatureClass(IFeatureClass featureClass, String nombreField, esriFieldType tipoField)
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
    public void crearRed(IMap targetMap, string nombreLayer, IPoint puntoOrigen, IPoint puntoOpuesto, int nroFilas, int nroColumnas, bool selectable, string capaZonificacion)
    {

        Geoprocessor gp = new Geoprocessor();

        ESRI.ArcGIS.DataManagementTools.CreateFishnet fishNet = new ESRI.ArcGIS.DataManagementTools.CreateFishnet();
        fishNet.out_feature_class = nombreLayer;

        fishNet.origin_coord = puntoOrigen.X.ToString() + " " + puntoOrigen.Y.ToString();

        double medio = (puntoOpuesto.Y + puntoOrigen.Y)/2;
        fishNet.y_axis_coord = puntoOrigen.X.ToString() + " " + medio.ToString();
        
        fishNet.corner_coord = puntoOpuesto.X.ToString() + " " + puntoOpuesto.Y.ToString();

        fishNet.cell_width = 0;
        fishNet.cell_height = 0;
        fishNet.number_rows = nroFilas;
        fishNet.number_columns = nroColumnas;
        fishNet.out_label = nombreLayer;
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
        finally
        {
            for (int i = 0; i < gp.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp.GetMessage(i));
        }
    }

}
