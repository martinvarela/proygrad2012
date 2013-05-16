using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using Proyecto;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;


class BlackmoreControlador : IBlackmore
{
    private IWorkspace wsBlackmore;
    private List<Capa> capas;
    private Blackmore blackmore;
    private Entrada entradaBase;

    public BlackmoreControlador() { }

    //Excepciones: OK
    //ProyectoException
    public void crearBlackmore(DTPCrearBlackmore dtp)
    {
        try
        {
            bool filasColumnas = dtp.getFilasColumnas();
            int vertical = dtp.getVertical();
            int horizontal = dtp.getHorizontal();
            List<DTCapasBlackmore> capasDT = dtp.getCapasDT();
            double dst = dtp.getDst();
            string nombreCapaBlackmore = dtp.getNombreCapaBlackmore();
            string rutaCapaBlackmore = dtp.getRutaCapaBlackmore();

            this.capas = new List<Capa>();

            //paso 1
            //se setea el workspace de donde se guardaran los datos generados.
            //asi como tambien los datos creados temporalmente que luego se eliminaran. 
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
            IWorkspace workspaceBlackmore = workspaceFactory.OpenFromFile(rutaCapaBlackmore, 0);
            this.wsBlackmore = workspaceBlackmore;

            ILayer layerCapaBase = null;
            this.entradaBase = new Entrada();

            //paso 2
            //se crean las capas de entrada(instancias)
            int indice = 0;
            foreach (DTCapasBlackmore dtCapa in capasDT)
            {
                //creo una nueva entrada
                //paso 3
                Entrada capaEntrada = new Entrada();
                capaEntrada.setNombreAtributo(dtCapa.getListaAtributos()[0].ToString());
                capaEntrada.setNombre(dtCapa.getNombreCapa());
                capaEntrada.setLayerCapa(dtCapa.getCapa());
                capaEntrada.setIndice(indice);
                //paso 4
                if (dtCapa.esCapaBase())
                {
                    capaEntrada.setEsCapaBase(true);
                    layerCapaBase = dtCapa.getCapa();
                    this.entradaBase = capaEntrada;
                }
                else
                {
                    capaEntrada.setEsCapaBase(false);
                }

                //paso 5
                //agrego la capa a la lista de capas del controlador
                this.capas.Add(capaEntrada);
                indice++;
            }


            string ahora = System.DateTime.Now.ToString("HHmmss");
            //paso 6 
            //creo la instancia de Blackmore, se crea la capa de red
            DTPBlackmore dtpblackmore = new DTPBlackmore(filasColumnas, vertical, horizontal, dst, layerCapaBase, ahora + "_celdasAux", this.wsBlackmore);
            this.blackmore = new Blackmore(dtpblackmore);

            //paso 7
            IFeatureLayer poligonosBlackmore = this.blackmore.getPoligonosBlackmore();
            String rutaCapaUnion = rutaCapaBlackmore + @"\" + nombreCapaBlackmore + ".shp";
            DTPUnionEspacial dtpu = new DTPUnionEspacial(poligonosBlackmore.FeatureClass, layerCapaBase, rutaCapaUnion, false, entradaBase.getNombreAtributo(), "merge_" + entradaBase.getIndice().ToString());
            IFeatureClass unionCapaBase = this.unionEspacial(dtpu);

            //paso 8
            entradaBase.setCapaUnion(unionCapaBase);

            //paso 9
            double auxMediaCapas = entradaBase.getMedia();
            int cantCapas = 1;

            //paso 10
            this.blackmore.crearCeldas(unionCapaBase);

            //paso 11*
            foreach (Entrada capaEntrada in this.capas)
            {
                //paso 12*, 13*
                if (!capaEntrada.getEsCapaBase())
                {
                    rutaCapaUnion = rutaCapaBlackmore + @"\" + ahora + "_UNION" + capaEntrada.getNombre() + ".shp";
                    DTPUnionEspacial dtpu1 = new DTPUnionEspacial(unionCapaBase, capaEntrada.getLayerCapa(), rutaCapaUnion, true, capaEntrada.getNombreAtributo(), "merge_" + capaEntrada.getIndice().ToString());
                    capaEntrada.setCapaUnion(this.unionEspacial(dtpu1));
                    auxMediaCapas += capaEntrada.getMedia();
                    cantCapas++;

                }
            }

            //paso 14
            IFeatureClass featureUnion = entradaBase.getCapaUnion();

            //paso 15
            this.blackmore.completarFeature(featureUnion, this.capas, auxMediaCapas / cantCapas);


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
                    for (int i = fields.FieldCount - 1; i >= 0; i--)
                    {
                        f = fields.get_Field(i);
                        if (!camposGuardar.Contains(f.Name))
                            this.deleteField(e.getCapaUnion(), f.Name.ToString());
                    }
                }
            }

            //borro capa de poligonos inicial
            if (((IDataset)(this.blackmore.getPoligonosBlackmore().FeatureClass)).CanDelete())
                ((IDataset)(this.blackmore.getPoligonosBlackmore().FeatureClass)).Delete();

            
            //agrega la salida al mapa
            IFeatureLayer fl = new FeatureLayer();
            fl.FeatureClass = entradaBase.getCapaUnion();
            IGeoFeatureLayer pGeoFeatureLayer = fl as IGeoFeatureLayer;
            this.definirColoresClasesCapa(pGeoFeatureLayer, "clase");
            ILayer layer = (ILayer)fl;
            layer.Name = fl.FeatureClass.AliasName;
            IMap targetMap = ArcMap.Document.FocusMap;
            targetMap.AddLayer(layer);

            ESRI.ArcGIS.Carto.IActiveView activeView = (ESRI.ArcGIS.Carto.IActiveView)targetMap;
            activeView.Refresh();

        }
        catch
        {
            throw new ProyectoException("Ocurrio un error al ejecutar la herramienta 'CrearBlackmore'.");
        }
    }

    //retorna una lista de datatypes con los datos de las capas cargadas en el ArcMap.
    //filtros de capa: capas con featureClass y que tienen al menos 1 atributo double o entero corto o entero largo o float(single)
    //Excepciones: OK
    //ProyectoException
    public List<DTCapasBlackmore> cargarCapasBlackmore()
    {
        List<DTCapasBlackmore> listaCapas = new List<DTCapasBlackmore>();

        IMap targetMap = ArcMap.Document.FocusMap;

        //obtengo las capas abiertas
        IEnumLayer enumLayers = targetMap.get_Layers();
        enumLayers.Reset();
        ILayer layer = enumLayers.Next();

        if (layer == null)
        {
            //si no hay capas abiertas en ArcMap
            throw new ProyectoException("No hay capas abiertas en ArcMap.");
        }
        else
        {
            //itero en todas las capas
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
            if (listaCapas.Count > 0)
            {
                return listaCapas;
            }
            else
            {
                throw new ProyectoException("No existen capas abiertas con el tipo requerido.");
            }
        }
    }


    //devuelve un FeatureClass con la union de las entidades pasadas por parametro
    //Excepciones: OK
    //ProyectoException
    private IFeatureClass unionEspacial(DTPUnionEspacial dtp)
    {
        Geoprocessor gpt = new Geoprocessor();
        try
        {
            IFeatureClass entidadDestino = dtp.getEntidadDestino();
            ILayer entidadUnion = dtp.getEntidadUnion();
            string entidadSalida = dtp.getEntidadSalida();
            bool mantenerEntidades = dtp.getMantenerEntidades();
            string nombreAtributo = dtp.getNombreAtributo();
            string atributoTablaUnion = dtp.getAtributoTablaUnion();

            IFeatureLayer featureLayerUnion = entidadUnion as IFeatureLayer;
            IFeatureClass featureClassUnion = featureLayerUnion.FeatureClass;
            IDataset dsUnion = (IDataset)entidadUnion;
            string pathUnion = dsUnion.Workspace.PathName + @"\" + dsUnion.Name + ".shp";

            IGPUtilities gputilities = new GPUtilitiesClass();
            IGPFieldMapping fieldmapping = new GPFieldMappingClass();
            IDETable inputTableA = (IDETable)gputilities.MakeDataElement(pathUnion, null, null);

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
            sJoin.out_feature_class = entidadSalida;

            IFeatureClass fc;
            IQueryFilter qf;
            IGPUtilities gpUtils = new GPUtilitiesClass();

            IGeoProcessorResult result = (IGeoProcessorResult)gpt.Execute(sJoin, null);
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc, out qf);

            return fc;
        }
        catch
        {
            throw new ProyectoException("Ocurrio un error al intentar realizar la Union Espacial de las capas, por favor verifique los tipos de Shapefile.");
        }
    }

    //Elimina el field 'nombreField' de la FeatureClass 'featureClass'
    //Excepciones: OK
    //ProyectoException
    public void deleteField(IObjectClass featureClass, String nombreField)
    {
        int indiceField = featureClass.FindField(nombreField);
        IField field = featureClass.Fields.get_Field(indiceField);

        ISchemaLock schemaLock = (ISchemaLock)featureClass;
        try
        {
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
            featureClass.DeleteField(field);
        }
        catch
        {
            throw new ProyectoException("Error al intentar los campos de la tabla de salida.");
        }
        finally
        {
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
        }
    }

    //Funcion que setea las clases con los diferentes colores y leyendas para la capa blackmore
    //agrega 4 Clases: "1","2","3","4"
    private void definirColoresClasesCapa(IGeoFeatureLayer pGeoFeatureLayer, string nombreCampo)
    {

        IRandomColorRamp pRandomColorRamp = new RandomColorRampClass();
        //Se crea la ColorRamp
        pRandomColorRamp.MinSaturation = 20;
        pRandomColorRamp.MaxSaturation = 40;
        pRandomColorRamp.MinValue = 85;
        pRandomColorRamp.MaxValue = 100;
        pRandomColorRamp.StartHue = 76;
        pRandomColorRamp.EndHue = 188;
        pRandomColorRamp.UseSeed = true;
        pRandomColorRamp.Seed = 43;
        
        //Se crea el Renderer .
        IUniqueValueRenderer pUniqueValueRenderer = new UniqueValueRendererClass();

        ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
        pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
        pSimpleFillSymbol.Outline.Width = 0.4;

        //Seteo de propiedades
        pUniqueValueRenderer.FieldCount = 1;
        pUniqueValueRenderer.set_Field(0, nombreCampo);
        pUniqueValueRenderer.DefaultSymbol = pSimpleFillSymbol as ISymbol;
        pUniqueValueRenderer.UseDefaultSymbol = false;
        
        //Se agregan las clases
        string nombreEtiqueta = "Clasificacion";
        ISimpleFillSymbol pClassSymbol = new SimpleFillSymbolClass();
        pClassSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
        pClassSymbol.Outline.Width = 0.4;
        pUniqueValueRenderer.AddValue("1", nombreEtiqueta, pClassSymbol as
                    ISymbol);
        pUniqueValueRenderer.set_Label("1", "Estable - Bajo Rendimiento");
        pUniqueValueRenderer.set_Symbol("1", pClassSymbol as ISymbol);

        ISimpleFillSymbol pClassSymbol2 = new SimpleFillSymbolClass();
        pClassSymbol2.Style = esriSimpleFillStyle.esriSFSSolid;
        pClassSymbol2.Outline.Width = 0.4;
        pUniqueValueRenderer.AddValue("2", nombreEtiqueta, pClassSymbol2 as
                    ISymbol);
        pUniqueValueRenderer.set_Label("2", "Inestable - Bajo Rendimiento");
        pUniqueValueRenderer.set_Symbol("2", pClassSymbol2 as ISymbol);

        ISimpleFillSymbol pClassSymbol3 = new SimpleFillSymbolClass();
        pClassSymbol3.Style = esriSimpleFillStyle.esriSFSSolid;
        pClassSymbol3.Outline.Width = 0.4;
        pUniqueValueRenderer.AddValue("3", nombreEtiqueta, pClassSymbol3 as
                    ISymbol);
        pUniqueValueRenderer.set_Label("3", "Inestable - Alto Rendimiento");
        pUniqueValueRenderer.set_Symbol("3", pClassSymbol3 as ISymbol);

        ISimpleFillSymbol pClassSymbol4 = new SimpleFillSymbolClass();
        pClassSymbol4.Style = esriSimpleFillStyle.esriSFSSolid;
        pClassSymbol4.Outline.Width = 0.4;
        pUniqueValueRenderer.AddValue("4", nombreEtiqueta, pClassSymbol4 as
                    ISymbol);
        pUniqueValueRenderer.set_Label("4", "Estable - Alto Rendimiento");
        pUniqueValueRenderer.set_Symbol("4", pClassSymbol4 as ISymbol);

        //Seteo del valor de la ColorRamp
        pRandomColorRamp.Size = pUniqueValueRenderer.ValueCount;
        bool bOK;
        pRandomColorRamp.CreateRamp(out bOK);

        IEnumColors pEnumColors = pRandomColorRamp.Colors;
        pEnumColors.Reset();

        //Se cargan los simbolos con los colores al renderer
        for (int j = 0; j <= pUniqueValueRenderer.ValueCount - 1; j++)
        {
            string xv;
            xv = pUniqueValueRenderer.get_Value(j);
            if (xv != "")
            {
                ISimpleFillSymbol pSimpleFillColor = pUniqueValueRenderer.get_Symbol(xv)
                    as ISimpleFillSymbol;
                pSimpleFillColor.Color = pEnumColors.Next();
                pUniqueValueRenderer.set_Symbol(xv, pSimpleFillColor as ISymbol);

            }
        }

        //Se asigna el renderer a la capa
        pGeoFeatureLayer.Renderer = pUniqueValueRenderer as IFeatureRenderer;

        IUID pUID = new UIDClass();
        pUID.Value = "{683C994E-A17B-11D1-8816-080009EC732A}";
        pGeoFeatureLayer.RendererPropertyPageClassID = pUID as UIDClass;

    }

}