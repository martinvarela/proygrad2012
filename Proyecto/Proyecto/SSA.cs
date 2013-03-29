using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Proyecto;

class SSA
{
    private double temperaturaInicial;
    private double factorReduccion;
    private double epsilon;

    public static int cantMuestras = 40;
    public List<int> muestreados;
    public List<int> todos;

    public SSA()
    {
        this.temperaturaInicial = 100;
        this.factorReduccion = 0.01;
        this.epsilon = 0.000001;
    }

    public double getTemperaturaInicial() { return this.temperaturaInicial; }
    public void setTemperaturaInicial(double t) { this.temperaturaInicial = t; }

    public double getFactorReduccion() { return this.factorReduccion; }
    public void setFactorReduccion(double f) { this.factorReduccion = f; }

    public double getEpsilon() { return this.epsilon; }
    public void setEpsilon(double e) { this.epsilon = e; }


    
    public List<PuntoMuestreo> SimulatedAnnealing2(List<PuntoMuestreo> puntos)
    {
        List<PuntoMuestreo> resultado = new List<PuntoMuestreo>();
        PuntoMuestreo aux;
        Coordenada coordAux;
        
        //a partir de los puntos, selecciona als muestras iniciales
        //List<PuntoZonificacion> zonif =  Inicializar2(resultado);
        //List<PuntoZonificacion> zonif =  ClonarZonif(resultado);
        Inicializar3(puntos);
        //Imprimir2(todos);
        //Imprimir2(muestreados);
        //List<PuntoZonificacion> zonifAux;
        List<int> auxMuestreados;
        List<int> auxTodos;
        double auxFitness;
        double fitness;
        
        //Random rnd = new Random();
        int iteration = -1;
        ////the probability
        double proba;
        double alpha = 0.999;
        double temperature = 400.0;
        double epsilon = 0.0000001;
        double delta;
        fitness = CalcularFitness2(puntos, muestreados);

        System.Diagnostics.Debug.WriteLine(" fitness: " + fitness);
        while (iteration < 100/*temperature > epsilon*/ )
        {
            iteration++;

            //zonifAux = ClonarZonif(zonif);
            auxMuestreados = ClonarLista(muestreados);
            auxTodos = ClonarLista(todos);
            MoverMuestra2(auxMuestreados, auxTodos);
            auxFitness = CalcularFitness2(puntos, auxMuestreados);
            System.Diagnostics.Debug.WriteLine(" auxFitnens: " + auxFitness);
            delta = auxFitness - fitness;
            //if the new distance is better accept it and assign it
            if (delta < 0)
            {
                muestreados = auxMuestreados;
                todos = auxTodos;
                fitness = auxFitness;
            }
            /*else
            {
                proba = rnd.NextDouble();
                //if the new distance is worse accept 
                //it but with a probability level
                //if the probability is less than 
                //E to the power -delta/temperature.
                //otherwise the old value is kept
                if (proba < Math.Exp(-delta / temperature))
                {
                    Console.WriteLine("acepto una peor");
                    muestras = aux_muestras;
                    resultado = aux_resultado;
                    fitnnes = aux_fitnnes;
                }
            }*/
            //cooling process on every iteration
            temperature *= alpha;
            //print every 400 iterations
            if (iteration % 10 == 0)
            {
                System.Diagnostics.Debug.WriteLine(fitness);
                Imprimir2(todos);
                Imprimir2(muestreados);
                System.Diagnostics.Debug.WriteLine("temp: " + temperature + " delta: " + delta + " fitnnes: " + fitness + " aux_fitnnes: " + auxFitness);
                System.Diagnostics.Debug.WriteLine("iter: " + iteration);
            }
        }

        //creo la lista de PuntosMuestreo con los puntos a muestrear 
        for (int i = 0; i < muestreados.Count; i++)
        {
            aux = new PuntoMuestreo();
            coordAux = new Coordenada();
            coordAux.X = puntos[muestreados[i]].getCoordenada().X;
            coordAux.Y = puntos[muestreados[i]].getCoordenada().Y;
            aux.setCoordenada(coordAux);
            aux.setValor(puntos[muestreados[i]].getValor());
            resultado.Add(aux);
        }

        return resultado;


    }

    
    //el fitness es el error cuadratico medio entre los valores en los puntos reales 
    //y los valores en los puntos interpolados con las muestras
    private double CalcularFitness(List<PuntoMuestreo> zonif, List<int> muestras)
    {
        IMap map = ArcMap.Document.FocusMap; 
        String capaPuntosMuestreo = "m" + System.DateTime.Now.ToString("ddHHmmss");

        //agregar como parametro 
        IFeatureClass vecinosClass = this.crearCapaPuntosMuestreo(map, capaPuntosMuestreo, zonif);
        String capaIDW = "v" + System.DateTime.Now.ToString("ddHHmmss");

        Geoprocessor gp = new Geoprocessor();
        ESRI.ArcGIS.GeostatisticalAnalystTools.IDW vecinosIDW = new ESRI.ArcGIS.GeostatisticalAnalystTools.IDW();
        vecinosIDW.in_features = vecinosClass;
        vecinosIDW.out_ga_layer = capaIDW;
        vecinosIDW.z_field = vecinosClass.FindField("Valor");

        gp.AddOutputsToMap = true;
        gp.Execute(vecinosIDW, null);

        String capaEstimacion = "vOut" + System.DateTime.Now.ToString("ddHHmmss");

        Geoprocessor gp2 = new Geoprocessor();
        ESRI.ArcGIS.GeostatisticalAnalystTools.GALayerToPoints estimacion = new ESRI.ArcGIS.GeostatisticalAnalystTools.GALayerToPoints();
        //esta tiene que ser la capa con todos los puntos!!!!!!!!!!!!!!!!!
        estimacion.in_locations = capaPuntosMuestreo;
        estimacion.out_feature_class = capaEstimacion;
        estimacion.z_field = "Valor";
        estimacion.in_geostat_layer = vecinosIDW.out_ga_layer;

        try
        {
            System.Diagnostics.Debug.WriteLine("Executing the try statement.");
            gp2.Execute(estimacion, null);
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


        //recorrer la capa capaEstimacion, hacer el cuadrado de cada error y sumarlos

        //return suma errores
        return 1000;

    }

    //el fitness es el error cuadratico medio entre los valores en los puntos reales 
    //y los valores en los puntos interpolados con las muestras
    public double CalcularFitness2(List<PuntoMuestreo> zonif, List<int> muestras)
    {   
        double valor = 0;
        int pos;
        for (int i = 0; i < muestras.Count; i++)
        {
            pos = muestras[i];
            valor += zonif[pos].getValor();
        }
        return valor;
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
    private void Inicializar3(List<PuntoMuestreo> listaMuestreo)
    {
        this.todos = new List<int>();
        this.muestreados = new List<int>();
        for (int ind = 0; ind < listaMuestreo.Count; ind++)
        {
            if (((ind + 1) % (listaMuestreo.Count / cantMuestras)) == 0)
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


}
