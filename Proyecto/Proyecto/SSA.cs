using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Proyecto;
using ESRI.ArcGIS.Geoprocessing;
using System.IO;
using ESRI.ArcGIS.esriSystem;


class SSA
{
    private double temperaturaInicial;
    private double factorReduccion;
    private int iteraciones;
    public int cantMuestras;
    public List<int> muestreados;
    public List<int> todos;
    private IWorkspace ws;

    public SSA()
    {
        this.temperaturaInicial = 14.427;
        this.factorReduccion = 0.994;
        this.iteraciones = 1000;
        //this.cantMuestras = 30;
    }

    public double getTemperaturaInicial() { return this.temperaturaInicial; }
    public void setTemperaturaInicial(double t) { this.temperaturaInicial = t; }

    public double getFactorReduccion() { return this.factorReduccion; }
    public void setFactorReduccion(double f) { this.factorReduccion = f; }

    public int getIteraciones() { return this.iteraciones; }
    public void setIteraciones(int i) { this.iteraciones = i; }

    public void setWorkspace(IWorkspace workspace)
    {
        this.ws = workspace;
    }

    public IFeatureClass SimulatedAnnealing(IFeatureClass capaPuntosMuestreo, String metodoInterpolacion, double expIDW, double rango, double error, string pathArchivo)
    {
        double factorReduccionAux = this.factorReduccion;
        int iteracionesAux = this.iteraciones;
        int cantMuestrasAux = this.cantMuestras;
        //creo la lista de PuntosMuestreo a partir de la IFeatureClass
        List<PuntoMuestreo> listaPuntosMuestreo = new List<PuntoMuestreo>();
        IFeatureCursor cursorPuntosMuestreo = capaPuntosMuestreo.Search(null, false);
        int indice = capaPuntosMuestreo.FindField("Valor");
        IFeature featurePunto = cursorPuntosMuestreo.NextFeature();
        while (featurePunto != null)
        {
            IGeometry g = featurePunto.Shape;
            IPoint p = g as IPoint;
            Coordenada c = new Coordenada();
            c.X = p.X;
            c.Y = p.Y;
            PuntoMuestreo puntoMuestreo = new PuntoMuestreo();
            puntoMuestreo.setCoordenada(c);
            puntoMuestreo.setValor((double) featurePunto.get_Value(indice)); 
            listaPuntosMuestreo.Add(puntoMuestreo);

            featurePunto = cursorPuntosMuestreo.NextFeature();
        }

        bool llegueAErrorEsperado = true;

        while (cantMuestrasAux > 1 && llegueAErrorEsperado)
        {
            double temperaturaInicialAux = this.temperaturaInicial;
            //a partir de los puntos, selecciona las muestras iniciales
            Inicializar(listaPuntosMuestreo, cantMuestrasAux);

            List<int> auxMuestreados;
            List<int> auxTodos;
            double auxFitness;
            double fitness;

            Random rnd = new Random();
            int iteration = 0;
            ////the probability
            double proba;
            double delta;

            //calculo fitness del muestreo inicial
            fitness = CalcularFitnessIDW(listaPuntosMuestreo, muestreados, capaPuntosMuestreo, expIDW);
       
            System.Diagnostics.Debug.WriteLine(" fitness: " + fitness);

            //LOOP principal
            while (iteration < iteracionesAux && (fitness*100 > error) )
            {
                iteration++;

                auxMuestreados = ClonarLista(muestreados);
                auxTodos = ClonarLista(todos);
                MoverMuestra(auxMuestreados, auxTodos);
                auxFitness = CalcularFitnessIDW(listaPuntosMuestreo, auxMuestreados, capaPuntosMuestreo, expIDW);
                System.Diagnostics.Debug.WriteLine(" auxFitnens: " + auxFitness);
                delta = ((auxFitness - fitness)/fitness)*100;
                //if the new distance is better accept it and assign it
                if (delta < 0)
                {
                    muestreados = auxMuestreados;
                    todos = auxTodos;
                    fitness = auxFitness;
                }
                else
                {
                    proba = rnd.NextDouble();
                    System.Diagnostics.Debug.WriteLine("delta: " + delta +", peor, division: " + Math.Exp(-delta / temperaturaInicialAux).ToString());
                    //el delta es muy bajo para que funcione bien, el exp siempre da muuy cerca de 1, calibrar!!!!
                    if (proba < Math.Exp(-delta / temperaturaInicialAux))
                    {
                        System.Diagnostics.Debug.WriteLine("acepto una peor, proba: " + proba.ToString() );
                        muestreados = auxMuestreados;
                        todos = auxTodos;
                        fitness = auxFitness;
                    }
                }
                //cooling process on every iteration
                temperaturaInicialAux *= factorReduccionAux;
                //print every 100 iterations
                if (iteration % 100 == 0)
                {
                    System.Diagnostics.Debug.WriteLine(fitness);
                    System.Diagnostics.Debug.WriteLine("temp: " + temperaturaInicialAux + " delta: " + delta + " fitnnes: " + fitness + " aux_fitnnes: " + auxFitness);
                    System.Diagnostics.Debug.WriteLine("iter: " + iteration);
                }
            }

            IMap map = ArcMap.Document.FocusMap;

            //creo y agrego al mapa la capa de muestreo optimo
            String nombreCapaPuntosMuestreoOptimo = "MO" +muestreados.Count.ToString() + "_"  + System.DateTime.Now.ToString("ddMMyyyy_HHmmss");
            IFeatureClass result = this.crearCapaPuntosMuestreo(map, nombreCapaPuntosMuestreoOptimo, listaPuntosMuestreo, muestreados);
            IFeatureLayer featureLayerResult = new FeatureLayerClass();
            featureLayerResult.FeatureClass = result;
            ILayer layerResult = (ILayer)featureLayerResult;
            layerResult.Name = featureLayerResult.FeatureClass.AliasName;
            map.AddLayer(layerResult);
            ESRI.ArcGIS.Carto.IActiveView activeView = (ESRI.ArcGIS.Carto.IActiveView)map;
            activeView.Refresh();

            try
            {
                //string path = @"C:\PruebaArchivo.txt";
                //string path = capaPuntosMuestreo.FeatureDataset.Workspace.PathName;
                //string path = this.ws.PathName + @"\PruebaArchivo.txt";
                string path = pathArchivo;
                if (!File.Exists(path))
                {
                    StreamWriter sw = File.CreateText(path);
                    sw.WriteLine("Fecha de creacion del archivo " + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    sw.WriteLine("Estadisticas del Muestreo");
                    sw.WriteLine("");
                    sw.WriteLine("Nombre Capa: " + nombreCapaPuntosMuestreoOptimo);
                    sw.WriteLine("  Cantidad de Muestras: " + cantMuestrasAux.ToString());
                    sw.WriteLine("  Error: " + fitness.ToString());
                    sw.WriteLine("  Iteraciones: " + iteration.ToString());
                    sw.Close();
                }
                else if (File.Exists(path))
                {
                    TextWriter tw = new StreamWriter(path, true);
                    tw.WriteLine("");
                    tw.WriteLine("Nombre Capa: " + nombreCapaPuntosMuestreoOptimo);
                    tw.WriteLine("  Cantidad de Muestras: " + cantMuestrasAux.ToString());
                    tw.WriteLine("  Error: " + fitness.ToString());
                    tw.WriteLine("  Iteraciones: " + iteration.ToString());
                    tw.Close();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            if (fitness * 100 > error)
                llegueAErrorEsperado = false;
            cantMuestrasAux--;
        }
        return null;
        
    }

    
    //el fitness es el error cuadratico medio entre los valores en los puntos reales 
    //y los valores en los puntos interpolados con las muestras
    private double CalcularFitnessIDW(List<PuntoMuestreo> listaPuntosMuestreo, List<int> listaIndicesMuestras, IFeatureClass capaPuntosMuestreo, double expIDW)
    {
        IMap map = ArcMap.Document.FocusMap; 
        
        
        String nombreCapaPuntosMuestreoOptimo = "MO" + System.DateTime.Now.ToString("ddHHmmssfff");

        //agregar como parametro 
        IFeatureClass capaPuntosMuestreoOptimo = this.crearCapaPuntosMuestreo(map, nombreCapaPuntosMuestreoOptimo, listaPuntosMuestreo, listaIndicesMuestras );
        String nombreCapaIDW = "IDW";// +System.DateTime.Now.ToString("ddHHmmss");

        Geoprocessor gp = new Geoprocessor();
        ESRI.ArcGIS.GeostatisticalAnalystTools.IDW capaIDW = new ESRI.ArcGIS.GeostatisticalAnalystTools.IDW();
        capaIDW.in_features = capaPuntosMuestreoOptimo;
        capaIDW.out_ga_layer = nombreCapaIDW;
        capaIDW.z_field = capaPuntosMuestreoOptimo.FindField("Valor");
        capaIDW.power = expIDW; 
        gp.TemporaryMapLayers = true;
        gp.OverwriteOutput = true;
        gp.AddOutputsToMap = false;

        try
        {
            gp.Execute(capaIDW, null);
        }
        catch
        { 
            for (int i = 0; i < gp.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp.GetMessage(i));
            System.Diagnostics.Debug.WriteLine("Caught exception #2.");
        }

        String nombreCapaEstimacion = "Estimacion";// +System.DateTime.Now.ToString("ddHHmmss");

        Geoprocessor gp2 = new Geoprocessor();
        ESRI.ArcGIS.GeostatisticalAnalystTools.GALayerToPoints capaEstimacion = new ESRI.ArcGIS.GeostatisticalAnalystTools.GALayerToPoints();
        //esta tiene que ser la capa con todos los puntos!!!!!!!!!!!!!!!!!
        capaEstimacion.in_locations = capaPuntosMuestreo;
        capaEstimacion.out_feature_class = nombreCapaEstimacion;
        capaEstimacion.z_field = "Valor";
        capaEstimacion.in_geostat_layer = nombreCapaIDW;
        gp2.AddOutputsToMap = false;
        gp2.OverwriteOutput = true;
        gp2.TemporaryMapLayers = true;
        IGPUtilities gpUtils = new GPUtilitiesClass();
        IFeatureClass fc;
        IQueryFilter qf;
        double errorTotal = 0;
        try
        {
            //System.Diagnostics.Debug.WriteLine("Executing the try statement.");
            IGeoProcessorResult result = (IGeoProcessorResult)gp2.Execute(capaEstimacion, null);
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc, out qf);
            //recorrer la capa capaEstimacion, hacer el cuadrado de cada error y sumarlos
            IEnumLayer enumlayers = map.get_Layers();
            IFeatureCursor cursorEstimacion = fc.Search(null, false);
            int indiceError = fc.FindField("Error");
            int indiceValor = fc.FindField("Valor");
            IFeature featureEstimacion = cursorEstimacion.NextFeature();
            //gets the column ID where we can find the report ID
            while (featureEstimacion != null)
            {
                double errorPunto = Math.Abs((double)featureEstimacion.get_Value(indiceError));
                double valorReal = (double)featureEstimacion.get_Value(indiceValor);

                //errorTotal += errorPunto/valorReal;
                errorTotal += Math.Pow(errorPunto, 2);

                featureEstimacion = cursorEstimacion.NextFeature();
            }

            //errorTotal = errorTotal / listaPuntosMuestreo.Count;
            errorTotal = Math.Sqrt(errorTotal / listaPuntosMuestreo.Count);
        }
        catch
        {
            for (int i = 0; i < gp2.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp2.GetMessage(i));
            System.Diagnostics.Debug.WriteLine("Caught exception #2.");
            errorTotal = 9999999999999999;
        }

       
        

        //borro la capa auxiliar creada
        if (((IDataset)capaPuntosMuestreoOptimo).CanDelete())
        {
            ((IDataset)capaPuntosMuestreoOptimo).Delete();
        }

        //return suma errores
        return errorTotal;

    }

    
    //aca voy a tener una lista con los indices de los puntos de muestreo y voy a modificar uno al azar
    public void MoverMuestra(List<int> auxMuestreados, List<int> auxTodos)
    {
        Random rnd = new Random();
        int mover = rnd.Next(auxMuestreados.Count); // creates a number between 0 and auxMuestreados.Count
        int posMover = auxMuestreados[mover]; //pos en la lista de auxTodos a mover
        //encontrar un punto aleatorio que no este marcado para muestrear
        bool encontre = false;
        while (!encontre)
        {
            int pos = rnd.Next(auxTodos.Count);
            //el punto no estaba muestreado
            if (auxTodos[pos] == 0)
            {
                auxTodos[pos] = 1;
                auxTodos[posMover] = 0;
                auxMuestreados[mover] = pos;
                encontre = true;
            }
        }
    }

    private void Inicializar(List<PuntoMuestreo> listaMuestreo, int cantMuestrasAux)
    {
        this.todos = new List<int>();
        this.muestreados = new List<int>();
        for (int ind = 0; ind < listaMuestreo.Count; ind++)
        {
            this.todos.Add(0);
        }

        Random rnd = new Random();
        for (int ind = 0; ind < cantMuestrasAux; ind++)
        {
            bool encontre = false;
            while (!encontre)
            {
                int pos = rnd.Next(this.todos.Count);
                //el punto no estaba muestreado
                if (this.todos[pos] == 0)
                {
                    this.todos[pos] = 1;
                    this.muestreados.Add(pos);
                    encontre = true;
                }
            }
        }
    }

    public List<PuntoZonificacion> ClonarZonif(List<PuntoZonificacion> zonif) {
        List<PuntoZonificacion> resultado = new List<PuntoZonificacion>();
        for (int i = 0; i < zonif.Count; i++)
        {
            PuntoZonificacion puntoRes = new PuntoZonificacion();
            Coordenada coordenada = new Coordenada();
            coordenada.X = zonif[i].Coordenada.X;
            coordenada.Y = zonif[i].Coordenada.Y;
            puntoRes.Coordenada = coordenada;
            puntoRes.Variabilidad = zonif[i].Variabilidad;
            resultado.Add(puntoRes);
        }
        return resultado;
    }

    public List<int> ClonarLista( List<int> lista ){
        List<int> res = new List<int>();
        for (int i=0; i < lista.Count; i++)
        {
            res.Add(lista[i]);
        }
        return res;
        
    }

    public IFeatureClass crearCapaPuntosMuestreo(IMap map, string nombreFeatureClass, List<PuntoMuestreo> listaPuntosMuestreo, List<int> listaIndicesMuestreo)
    {
        
        IWorkspace2 ws2 = (IWorkspace2)this.ws;
        IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)ws2; // Explicit Cast

        IFeatureClass nuevaFeatureClass = this.crearFeatureClassConFields(nombreFeatureClass, featureWorkspace);

        IFeatureBuffer featureBuffer = nuevaFeatureClass.CreateFeatureBuffer();
        IFeatureCursor FeatureCursor = nuevaFeatureClass.Insert(true);

        IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)this.ws;

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
        int indiceValor = nuevaFeatureClass.FindField("Valor");
        //foreach (PuntoMuestreo aux in listaPuntosMuestreo)
        foreach (int i in listaIndicesMuestreo)
        {
            point = new ESRI.ArcGIS.Geometry.PointClass();
            point.X = listaPuntosMuestreo[i].getCoordenada().X;
            point.Y = listaPuntosMuestreo[i].getCoordenada().Y;

            featureBuffer.Shape = point;
            featureBuffer.set_Value(indiceValor, listaPuntosMuestreo[i].getValor());

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
        //map.AddLayer(layer);

        //ESRI.ArcGIS.Carto.IActiveView activeView = (ESRI.ArcGIS.Carto.IActiveView)map;
        //activeView.Refresh();
        
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
        try
        {
            //System.Diagnostics.Debug.WriteLine("Executing the try statement.");
            IFeatureClass featureClass = featureWorkspace.CreateFeatureClass(featureClassName,
                                                                            validatedFields, null, ocDescription.ClassExtensionCLSID,
                                                                            esriFeatureType.esriFTSimple, "Shape", "");
            return featureClass;
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine("{0} Caught exception #1." + e);
            return null;
        }
        
    }


}
