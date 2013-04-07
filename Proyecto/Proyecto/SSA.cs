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
using ESRI.ArcGIS.DataSourcesGDB;

class SSA
{
    private double temperaturaInicial;
    private double factorReduccion;
    private double epsilon;
    public int cantMuestras = 15;
    public List<int> muestreados;
    public List<int> todos;
    private IWorkspace ws;

    public SSA(IWorkspace wsSSA)
    {
        this.temperaturaInicial = 100;
        this.factorReduccion = 0.01;
        this.epsilon = 0.000001;
        this.ws = wsSSA;
    }

    public double getTemperaturaInicial() { return this.temperaturaInicial; }
    public void setTemperaturaInicial(double t) { this.temperaturaInicial = t; }

    public double getFactorReduccion() { return this.factorReduccion; }
    public void setFactorReduccion(double f) { this.factorReduccion = f; }

    public double getEpsilon() { return this.epsilon; }
    public void setEpsilon(double e) { this.epsilon = e; }



    public IFeatureClass SimulatedAnnealing(IFeatureClass capaPuntosMuestreo, String metodoInterpolacion, int rango, double error)
    {

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


        while (this.cantMuestras > 10)
        {
            //a partir de los puntos, selecciona las muestras iniciales
            Inicializar(listaPuntosMuestreo);

            List<int> auxMuestreados;
            List<int> auxTodos;
            double auxFitness;
            double fitness;

            Random rnd = new Random();
            int iteration = -1;
            ////the probability
            double proba;
            double alpha = 0.999;
            double temperature = 400.0;
            double epsilon = 0.0000001;
            double delta;

            //calculo fitness del muestreo inicial
            if (metodoInterpolacion == "Kriging")
            {
                fitness = CalcularFitnessKriging(listaPuntosMuestreo, muestreados, capaPuntosMuestreo);
            }
            else
            {
                fitness = CalcularFitnessIDW(listaPuntosMuestreo, muestreados, capaPuntosMuestreo);
            }


            System.Diagnostics.Debug.WriteLine(" fitness: " + fitness);

            //LOOP principal
            while (iteration < 10 && temperature > epsilon)
            {
                iteration++;

                auxMuestreados = ClonarLista(muestreados);
                auxTodos = ClonarLista(todos);
                MoverMuestra2(auxMuestreados, auxTodos);
                if (metodoInterpolacion == "Kriging")
                {
                    auxFitness = CalcularFitnessKriging(listaPuntosMuestreo, auxMuestreados, capaPuntosMuestreo);
                }
                else
                {
                    auxFitness = CalcularFitnessIDW(listaPuntosMuestreo, auxMuestreados, capaPuntosMuestreo);
                }
                System.Diagnostics.Debug.WriteLine(" auxFitnens: " + auxFitness);
                delta = auxFitness - fitness;
                //if the new distance is better accept it and assign it
                if (delta < 0)
                {
                    muestreados = auxMuestreados;
                    todos = auxTodos;
                    fitness = auxFitness;
                }
                //else
                //{
                //    proba = rnd.NextDouble();
                //    //if the new distance is worse accept 
                //    //it but with a probability level
                //    //if the probability is less than 
                //    //E to the power -delta/temperature.
                //    //otherwise the old value is kept

                //    //el delta es muy bajo para que funcione bien, el exp siempre da muuy cerca de 1, calibrar!!!!
                //    if (proba < Math.Exp(-delta / temperature))
                //    {
                //        Console.WriteLine("acepto una peor");
                //        muestreados = auxMuestreados;
                //        todos = auxTodos;
                //        fitness = auxFitness;
                //    }
                //}
                //cooling process on every iteration
                temperature *= alpha;
                //print every 100 iterations
                if (iteration % 100 == 0)
                {
                    System.Diagnostics.Debug.WriteLine(fitness);
                    Imprimir2(todos);
                    Imprimir2(muestreados);
                    System.Diagnostics.Debug.WriteLine("temp: " + temperature + " delta: " + delta + " fitnnes: " + fitness + " aux_fitnnes: " + auxFitness);
                    System.Diagnostics.Debug.WriteLine("iter: " + iteration);
                }
            }

            IMap map = ArcMap.Document.FocusMap;

            //creo y agrego al mapa la capa de muestreo optimo
            String nombreCapaPuntosMuestreoOptimo = "MO" +muestreados.Count.ToString() + "_"  + System.DateTime.Now.ToString("ddMMyyyy_hhmmss");
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
                string path = this.ws.PathName + @"\PruebaArchivo.txt";
                if (!File.Exists(path))
                {
                    StreamWriter sw = File.CreateText(path);
                    sw.WriteLine("Fecha de creacion del archivo " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
                    sw.WriteLine("Estadisticas del Muestreo");
                    sw.WriteLine("");
                    sw.WriteLine("Nombre Capa: " + nombreCapaPuntosMuestreoOptimo);
                    sw.WriteLine("  Cantidad de Muestras: " + cantMuestras.ToString());
                    sw.WriteLine("  Error: " + fitness.ToString());
                    sw.WriteLine("  Iteraciones: " + iteration.ToString());
                    sw.Close();
                }
                else if (File.Exists(path))
                {
                    TextWriter tw = new StreamWriter(path, true);
                    tw.WriteLine("");
                    tw.WriteLine("Nombre Capa: " + nombreCapaPuntosMuestreoOptimo);
                    tw.WriteLine("  Cantidad de Muestras: " + cantMuestras.ToString());
                    tw.WriteLine("  Error: " + fitness.ToString());
                    tw.WriteLine("  Iteraciones: " + iteration.ToString());
                    tw.Close();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            this.cantMuestras--;
        }
        return null;
        
    }

    
    //el fitness es el error cuadratico medio entre los valores en los puntos reales 
    //y los valores en los puntos interpolados con las muestras
    private double CalcularFitnessIDW(List<PuntoMuestreo> listaPuntosMuestreo, List<int> listaIndicesMuestras, IFeatureClass capaPuntosMuestreo)
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
        gp.TemporaryMapLayers = true;
        gp.OverwriteOutput = true;
        gp.AddOutputsToMap = false;
        gp.Execute(capaIDW, null);

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
        
        //try
        //{
            System.Diagnostics.Debug.WriteLine("Executing the try statement.");
            IGeoProcessorResult result = (IGeoProcessorResult)gp2.Execute(capaEstimacion, null);
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc, out qf);
            
        //}
        //catch (NullReferenceException e)
        //{
        //    System.Diagnostics.Debug.WriteLine("{0} Caught exception #1." + e);
        //}
        //catch
        //{
        //    for (int i = 0; i < gp2.MessageCount; i++)
        //        System.Diagnostics.Debug.WriteLine(gp2.GetMessage(i));
        //    System.Diagnostics.Debug.WriteLine("Caught exception #2.");
        //}

       
        //recorrer la capa capaEstimacion, hacer el cuadrado de cada error y sumarlos
        IEnumLayer enumlayers = map.get_Layers();
        //ILayer layerPuntosEstimados = enumlayers.Next();
        //while ((layerPuntosEstimados != null) && (layerPuntosEstimados.Name != nombreCapaEstimacion))
        //{
        //    layerPuntosEstimados = enumlayers.Next();
        //}
        //IFeatureLayer ifeaturelayerPuntosEstimados = layerPuntosEstimados as FeatureLayer;


        //IFeatureCursor cursorEstimacion = ifeaturelayerPuntosEstimados.FeatureClass.Search(null, false);
        //int indiceError = ifeaturelayerPuntosEstimados.FeatureClass.FindField("Error");

        IFeatureCursor cursorEstimacion = fc.Search(null, false);
        int indiceError = fc.FindField("Error");
        IFeature featureEstimacion = cursorEstimacion.NextFeature();
        //gets the column ID where we can find the report ID
        double errorTotal = 0;
        while (featureEstimacion != null)
        {
            double errorPunto = (double) featureEstimacion.get_Value(indiceError);
            errorTotal += Math.Pow(errorPunto, 2);

            featureEstimacion = cursorEstimacion.NextFeature();
        }

        //borro la capa auxiliar creada
        if (((IDataset)capaPuntosMuestreoOptimo).CanDelete())
        {
            ((IDataset)capaPuntosMuestreoOptimo).Delete();
        }

        //return suma errores
        return errorTotal;

    }

    //el fitness es el error cuadratico medio entre los valores en los puntos reales 
    //y los valores en los puntos interpolados con las muestras
    private double CalcularFitnessKriging(List<PuntoMuestreo> listaPuntosMuestreo, List<int> listaIndicesMuestras, IFeatureClass capaPuntosMuestreo)
    {
        IMap map = ArcMap.Document.FocusMap;


        String nombreCapaPuntosMuestreoOptimo = "MO" + System.DateTime.Now.ToString("ddHHmmss");

        //agregar como parametro 
        IFeatureClass capaPuntosMuestreoOptimo = this.crearCapaPuntosMuestreo(map, nombreCapaPuntosMuestreoOptimo, listaPuntosMuestreo, listaIndicesMuestras);
        String nombreCapaKR = "KR" + System.DateTime.Now.ToString("ddHHmmss");

        Geoprocessor gp = new Geoprocessor();
        ESRI.ArcGIS.SpatialAnalystTools.Kriging capaKR = new ESRI.ArcGIS.SpatialAnalystTools.Kriging();
        capaKR.in_point_features = capaPuntosMuestreoOptimo;
        capaKR.z_field = "Valor";
        capaKR.out_surface_raster = nombreCapaKR;
        capaKR.semiVariogram_props = "Spherical";
        
        
        gp.AddOutputsToMap = true;
        
        try
        {
            System.Diagnostics.Debug.WriteLine("Executing the try statement.");
            gp.Execute(capaKR, null);
        }
        catch (NullReferenceException e)
        {
            System.Diagnostics.Debug.WriteLine("{0} Caught exception #1." + e);
        }
        catch
        {
            for (int i = 0; i < gp.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp.GetMessage(i));
            System.Diagnostics.Debug.WriteLine("Caught exception #1.");
        }
        finally
        {
            for (int i = 0; i < gp.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp.GetMessage(i));
            System.Diagnostics.Debug.WriteLine("Caught exception #1.");
        }


        String nombreCapaEstimacion = "Estimacion" + System.DateTime.Now.ToString("ddHHmmss");

        Geoprocessor gp2 = new Geoprocessor();
        ESRI.ArcGIS.SpatialAnalystTools.ExtractValuesToPoints capaEstimacion = new ESRI.ArcGIS.SpatialAnalystTools.ExtractValuesToPoints();
        //esta tiene que ser la capa con todos los puntos!!!!!!!!!!!!!!!!!
        capaEstimacion.in_point_features = capaPuntosMuestreo;
        capaEstimacion.out_point_features = nombreCapaEstimacion;
        capaEstimacion.in_raster = nombreCapaKR;

        try
        {
            System.Diagnostics.Debug.WriteLine("Executing the try statement.");
            gp2.Execute(capaEstimacion, null);
        }
        catch (NullReferenceException e)
        {
            System.Diagnostics.Debug.WriteLine("{0} Caught exception #1." + e);
        }
        catch
        {
            for (int i = 0; i < gp2.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp2.GetMessage(i));
            System.Diagnostics.Debug.WriteLine("Caught exception #2.");
        }
        finally
        {
            for (int i = 0; i < gp2.MessageCount; i++)
                System.Diagnostics.Debug.WriteLine(gp2.GetMessage(i));
            System.Diagnostics.Debug.WriteLine("Caught exception #2.");
        }


        //recorrer la capa capaEstimacion, hacer el cuadrado de cada error y sumarlos
        IEnumLayer enumlayers = map.get_Layers();
        ILayer layerPuntosEstimados = enumlayers.Next();
        while ((layerPuntosEstimados != null) && (layerPuntosEstimados.Name != nombreCapaEstimacion))
        {
            layerPuntosEstimados = enumlayers.Next();
        }
        IFeatureLayer ifeaturelayerPuntosEstimados = layerPuntosEstimados as FeatureLayer;


        IFeatureCursor cursorEstimacion = ifeaturelayerPuntosEstimados.FeatureClass.Search(null, false);
        int indiceReal = ifeaturelayerPuntosEstimados.FeatureClass.FindField("Valor");
        int indiceEstimado = ifeaturelayerPuntosEstimados.FeatureClass.FindField("RASTERVALU");

        IFeature featureEstimacion = cursorEstimacion.NextFeature();
        //gets the column ID where we can find the report ID
        double errorTotal = 0;
        while (featureEstimacion != null)
        {
            double errorPunto = (double)featureEstimacion.get_Value(indiceReal) - (double)featureEstimacion.get_Value(indiceEstimado);
            errorTotal += Math.Pow(errorPunto, 2);

            featureEstimacion = cursorEstimacion.NextFeature();
        }
        //return suma errores
        return errorTotal;

    }

    
    
    //aca voy a tener una lista con los indices de los puntos de muestreo y voy a modificar uno al azar
    public void MoverMuestra2(List<int> auxMuestreados, List<int> auxTodos)
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

    

    public void Imprimir2(List<int> aux_todos)
    {
        foreach ( int aux in aux_todos ) 
        {
                System.Diagnostics.Debug.WriteLine(aux + ' ');

        } System.Diagnostics.Debug.WriteLine("");
    }

  
    
    //funcion para inicializar los puntos a muestrear, esto depende de la cantidad de muestras a seleccionar
    //armar un estilo de grilla con puntos equiespaciados ó seleccionar los puntos al azar
    private List<PuntoZonificacion> Inicializar2(List<PuntoZonificacion> listaZonificacion)
    {
        List<PuntoZonificacion> res = new List<PuntoZonificacion>(); 
        PuntoZonificacion aux;
        Coordenada coordAux;
        this.todos = new List<int>();
        this.muestreados = new List<int>();
        for (int ind=0 ; ind < listaZonificacion.Count; ind++){
            if (((ind + 1) % (listaZonificacion.Count /cantMuestras)) == 0)
            {
                this.todos.Add(1);
                this.muestreados.Add(ind);
                aux = new PuntoZonificacion();
                coordAux = new Coordenada();
                coordAux.X = listaZonificacion[ind].Coordenada.X;
                coordAux.Y = listaZonificacion[ind].Coordenada.Y;
                aux.Coordenada = coordAux;
                aux.Variabilidad = listaZonificacion[ind].Variabilidad;
                res.Add(aux);
            }
            else
            {
                this.todos.Add(0);
            }
        }
        return res;
    }

    //funcion para inicializar los puntos a muestrear, esto depende de la cantidad de muestras a seleccionar
    //armar un estilo de grilla con puntos equiespaciados ó seleccionar los puntos al azar
    private void Inicializar(List<PuntoMuestreo> listaMuestreo)
    {
        this.todos = new List<int>();
        this.muestreados = new List<int>();
        for (int ind = 0; ind < listaMuestreo.Count; ind++)
        {
            if (((ind + 1) % (listaMuestreo.Count / this.cantMuestras)) == 0)
            {
                this.todos.Add(1);
                this.muestreados.Add(ind);
            }
            else
            {
                this.todos.Add(0);
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

    //calculo el Root Mean Square Error para los puntos estimados a partir de las muestras
    //esta va a ser la funcion de fitness, cuando menor sea el error, mejor es la solucion
    public double RMSE(List<PuntoZonificacion> reales, List<PuntoZonificacion> estimados) {
        double error= 0;
        for (int i = 0; i < reales.Count; i++)
        {
            error += Math.Pow(reales[i].Variabilidad - estimados[i].Variabilidad, 2);
        }
        error = Math.Sqrt( error / reales.Count);
        return error;
    
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
            System.Diagnostics.Debug.WriteLine("Executing the try statement.");
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
