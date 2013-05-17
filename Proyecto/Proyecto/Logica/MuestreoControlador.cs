using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using Proyecto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using System.IO;

class MuestreoControlador : IMuestreo
{
    private IWorkspace wsZonif;
    private string nombreCapaPuntosZonificacion;
    private string nombreCapaPoligonos;
    private string nombreCapaPuntosMuestreo;
    private IFeatureClass capaPuntosZonificacion;
    private IFeatureLayer capaPuntosMuestreo;
    private IFeatureLayer capaPoligonos;

    public MuestreoControlador() 
    { }

    //se crea la instancia Muestreo con su respectiva lista de posibles puntos de Muestreos.
    //se devuelve en el arcmap la capa "CR-hhMMss" que se cambiara por el nombre pasado como parametro que contiene los posibles puntos de muestreo(todos) 
    //para realizar de forma manual el semivariograma de forma de encontrar cual es el RANGO.
    //Excepciones: OK
    //ProyectoException
    public Muestreo crearPuntosMuestreo(DTPCrearPuntosMuestreo dtp)
    {
        try
        {
            bool conRed = dtp.getConRed();
            string rutaEntrada = dtp.getRutaEntrada();
            bool filasColumnas = dtp.getFilasColumnas();
            int vertical = dtp.getVertical();
            int horizontal = dtp.getHorizontal();
            List<int> variablesMarcadas = dtp.getVariablesMarcadas();
            ProgressBar pBar = dtp.getPBar();
            string rutaCapa = dtp.getRutaCapa();
            string nombreCapa = dtp.getNombreCapa();
            Label lblProgressBar = dtp.getLblProgressBar();

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
                this.crearRed(new DTPCrearRed(map, this.nombreCapaPoligonos, nombreCapa, zonificacion.PuntoOrigen, zonificacion.PuntoOpuesto, filasColumnas, vertical, horizontal, true, this.nombreCapaPuntosZonificacion));

                //paso 5
                lblProgressBar.Text = "Creando layer con los puntos de red...";

                pBar.Minimum = 1;
                pBar.Maximum = vertical * horizontal;
                pBar.Step = 1;
                pBar.Value = 1;
                pBar.Visible = true;

                //calcula los valores de los puntos de muestreo haciendo promedio en los puntos que "caen" dentro de la celda de la capa de poligonos
                //agrega cada punto de muestreo a la lista de la instancia de muestreo.
                this.cargarValoresPuntosMuestreo(new DTPCargarValoresPuntosMuestreo(map, muestreo, this.nombreCapaPuntosZonificacion, this.nombreCapaPoligonos, this.nombreCapaPuntosMuestreo, 2, 2, pBar));

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
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch
        {
            throw new ProyectoException("Se ha producido un error al ejecutar la operacion 'Crear muestreo'.");
        }
    }

    //Devuelve las variables contenidas en el archivo .ZF
    //Excepciones: OK 
    //ProyectoException
    public List<string> cargarVariables(string rutaZF)
    {
        try
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
        catch
        {
            throw new ProyectoException("Ha ocurrido un error al leer el archivo .ZF, por favor verifique su contenido.");
        }
    }

    //crea la capa con los puntos de zonificacion
    //Excepciones: OK
    //ProyectoException
    private IFeatureClass crearCapaPuntosZonificacion(IMap map, string nombreFeatureClass, List<PuntoZonificacion> listaPuntos, ProgressBar pBar)
    {
        try
        {
            IWorkspace ws = this.wsZonif;
            IWorkspace2 ws2 = (IWorkspace2)ws;
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)ws2;

            IFeatureClass nuevaFeatureClass = this.crearFeatureClassConFields(nombreFeatureClass, featureWorkspace);

            IFeatureBuffer featureBuffer = nuevaFeatureClass.CreateFeatureBuffer();
            IFeatureCursor FeatureCursor = nuevaFeatureClass.Insert(true);
            IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)ws;

            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();

            object featureOID;
            ESRI.ArcGIS.Geometry.IPoint point;

            pBar.Minimum = 1;
            pBar.Maximum = listaPuntos.Count;
            pBar.Step = 1;
            pBar.Value = 1;
            pBar.Visible = true;

            foreach (PuntoZonificacion aux in listaPuntos)
            {
                point = new ESRI.ArcGIS.Geometry.PointClass();
                point.X = aux.Coordenada.X;
                point.Y = aux.Coordenada.Y;

                featureBuffer.Shape = point;
                featureBuffer.set_Value(featureBuffer.Fields.FindField("Valor"), aux.Variabilidad);
                featureOID = FeatureCursor.InsertFeature(featureBuffer);

                pBar.PerformStep();
            }
            FeatureCursor.Flush();

            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(FeatureCursor);

            pBar.Visible = false;

            return nuevaFeatureClass;
        }
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch
        {
            throw new ProyectoException("Error al crear la capa de puntos de zonificación.");
        }
    }

    //crea un featureClass con los los fields: "OID", "Shape", "Valor"
    //Excepciones: OK
    //ProyectoException
    private IFeatureClass crearFeatureClassConFields(String featureClassName, IFeatureWorkspace featureWorkspace)
    {
        try
        {
            IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
            IObjectClassDescription ocDescription = (IObjectClassDescription)fcDescription;

            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

            IField oidField = new FieldClass();
            IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
            oidFieldEdit.Name_2 = "OID";
            oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(oidField);

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

            IField geometryField = new FieldClass();
            IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
            geometryFieldEdit.Name_2 = "Shape";
            geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            geometryFieldEdit.GeometryDef_2 = geometryDef;
            fieldsEdit.AddField(geometryField);

            IField valorField = new FieldClass();
            IFieldEdit valorFieldEdit = (IFieldEdit)valorField;
            valorFieldEdit.Name_2 = "Valor";
            valorFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            fieldsEdit.AddField(valorField);

            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (IWorkspace)featureWorkspace;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

            IFeatureClass featureClass = null;
            featureClass = featureWorkspace.CreateFeatureClass(featureClassName, validatedFields, null, ocDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple, "Shape", "");

            return featureClass;
        }
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch
        {
            throw new ProyectoException("Error al crear la capa con los fields OID, Shape, Valor.");
        }
    }

    //devuelve el feature que extiende a la capa 'nombreCapa'
    //Excepciones: OK
    //ProyectoException
    private IFeatureClass crearPoligonoExtension(string nombreCapa, string nomSalida, int distAgregacion)
    {
        try
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
            IGPUtilities gpUtils = new GPUtilitiesClass();
            IGeoProcessorResult result = (IGeoProcessorResult)gp.Execute(poligonoExt, null);
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc, out qf);
            return fc;
        }
        catch 
        {
            throw new ProyectoException("Ha ocurrido un error al crear la extensión '" + nombreCapa.ToString() + "'.");
        }
    }

    //devuelve el IFeatureClass correspondiente a la capa de puntos de muestreo (sin optimizar).
    //Excepciones: OK
    //ProyectoException
    private void crearRed(DTPCrearRed dtp)
    {
        try
        {
            IMap targetMap = dtp.getTargetMap();
            string nombreCapaPoligonos = dtp.getNombreCapaPoligonos();
            string nombreCapa = dtp.getNombreCapa();
            IPoint puntoOrigen = dtp.getPuntoOrigen();
            IPoint puntoOpuesto = dtp.getPuntoOpuesto();
            bool filasColumnas = dtp.getFilasColumnas();
            int vertical = dtp.getVertical();
            int horizontal = dtp.getHorizontal();
            bool selectable = dtp.getSelectable();
            string capaZonificacion = dtp.getCapaZonificacion();

            Geoprocessor gp = new Geoprocessor();
            ESRI.ArcGIS.DataManagementTools.CreateFishnet fishNet = new ESRI.ArcGIS.DataManagementTools.CreateFishnet();

            fishNet.out_feature_class = this.wsZonif.PathName + "\\" + nombreCapaPoligonos;
            fishNet.origin_coord = puntoOrigen.X.ToString() + " " + puntoOrigen.Y.ToString();
            double medio = (puntoOpuesto.Y + puntoOrigen.Y) / 2;
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
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch
        {
            throw new ProyectoException("Ha ocurrido un error al crear la capa de red.");
        }
    }

    //Carga en la capa de puntos de muestreo los valores de los puntos usando los poligonos de la capa de poligonos pasada como parametro
    //Excepciones: OK
    //ProyectoException
    private void cargarValoresPuntosMuestreo(DTPCargarValoresPuntosMuestreo dtp)
    {
        try
        {
            IMap map = dtp.getMap();
            Muestreo muestreo = dtp.getMuestreo();
            string nombreCapaPuntosZonificacion = dtp.getNombreCapaPuntosZonificacion();
            string nombreCapaPoligonos = dtp.getNombreCapaPoligonos();
            string nombreCapaPuntosMuestreos = dtp.getNombreCapaPuntosMuestreos();
            int indiceAtributoEnTablaPoligonos = dtp.getIndiceAtributoEnTablaPoligonos();
            int indiceAtributoEnTablaPuntos = dtp.getIndiceAtributoEnTablaPuntos();
            ProgressBar pBar = dtp.getPBar();

            //se crea el campo Promedio en la capa de Puntos de Muestreos
            int indicePromedioFieldPuntosMuestreo = this.crearFieldAFeatureClass(this.capaPuntosMuestreo.FeatureClass, "Valor", esriFieldType.esriFieldTypeDouble);

            //Creo el updateCursorPoligono para iterar en las filas de los poligonos (poligono por poligono).
            IFeatureCursor updateCursorPoligono = this.capaPoligonos.FeatureClass.Update(null, false);

            //Creo el updateCursorPuntosMuestreo para iterar en las filas de los puntos de muestreo (punto por punto).
            IFeatureCursor updateCursorPuntosMuestreo = this.capaPuntosMuestreo.FeatureClass.Update(null, false);

            IFeature poligonoIFeature = null;
            IFeature puntosMuestreoIFeature = null;
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
                spatialFilter.Geometry = searchGeometry; //poligono
                System.String nameOfShapeField = this.capaPuntosZonificacion.ShapeFieldName;
                spatialFilter.GeometryField = nameOfShapeField;
                spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter = (IQueryFilter)spatialFilter;
                IFeatureCursor featureCursor = this.capaPuntosZonificacion.Search(queryFilter, false);

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
                    puntoMuestreo.setCoordenadaXY(ipoint.X, ipoint.Y);

                    //se agrega el punto de muestreo al muestreo
                    muestreo.agregarPuntoMuestreo(puntoMuestreo);
                }
                else
                {
                    //se borra el punto de muestreo que no tenga promedio
                    puntosMuestreoIFeature.Delete();
                }
            }
            Marshal.ReleaseComObject(updateCursorPuntosMuestreo);
        }
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch
        {
            throw new ProyectoException("Ha ocurrido un error al cargar los valores de los posibles puntos de muestreo.");
        }
    }

    //crea un nuevo field con el nombre 'nombreField' en el featureClass pasado como parametro
    //devuelve el indice del field creado
    //Excepciones: OK
    //ProyectoException
    private int crearFieldAFeatureClass(IFeatureClass featureClass, String nombreField, esriFieldType tipoField)
    {
        IField field = new FieldClass();
        IFieldEdit fieldEdit = (IFieldEdit)field;
        fieldEdit.Name_2 = nombreField;
        fieldEdit.Type_2 = tipoField;

        ISchemaLock schemaLock = (ISchemaLock)featureClass;
        try
        {
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
            featureClass.AddField(field);
            return featureClass.FindField(nombreField);
        }
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch
        {
            throw new ProyectoException("Ha ocurrido un error al agregar el field '" + nombreField +"' a la capa de salida de muestreo.");
        }
        finally
        {
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
        }
    }

    //quita los puntos de posible muestreo que estan fuera de los limites del campo
    //Excepciones: OK
    //ProyectoException
    private void quitarPuntosExternos(string nomCapaMuestreo, IFeatureClass extensionCampo)
    {
        IGeoProcessor2 gp = new GeoProcessorClass();
        IVariantArray parameters = new VarArrayClass();
        try
        {
            parameters.Add(nomCapaMuestreo);
            parameters.Add(extensionCampo);
            parameters.Add("OUTSIDE");

            gp.Execute("ErasePoint", parameters, null);

            if (((IDataset)extensionCampo).CanDelete())
                ((IDataset)extensionCampo).Delete();

        }
        catch 
        {
            throw new ProyectoException("Ha ocurrido un error al intentar eliminar los puntos que están fuera del campo.");
        }
    }

}
