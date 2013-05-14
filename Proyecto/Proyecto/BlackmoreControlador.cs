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


class BlackmoreControlador : IBlackmore
{
    private IWorkspace wsBlackmore;
    private List<Capa> capas;
    private Blackmore blackmore;
    private Entrada entradaBase;

    public BlackmoreControlador() { }

    //GONZALO: verificar si no hay alto acoplamiento
    //Excepciones: OK
    //ProyectoException
    public void crearBlackmore(bool filasColumnas, int vertical, int horizontal, List<DTCapasBlackmore> capasDT, double dst, string nombreCapaBlackmore, string rutaCapaBlackmore)
    {
        try
        {
            this.capas = new List<Capa>();

            //se setea el workspace de donde se guardaran los datos generados.
            //asi como tambien los datos creados temporalmente que luego se eliminaran. 
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
            IWorkspace workspaceBlackmore = workspaceFactory.OpenFromFile(rutaCapaBlackmore, 0);
            this.wsBlackmore = workspaceBlackmore;

            ILayer layerCapaBase = null;
            this.entradaBase = new Entrada();

            //se crean las capas de entrada(instancias)
            int indice = 0;
            foreach (DTCapasBlackmore dtCapa in capasDT)
            {
                //creo una nueva entrada
                Entrada capaEntrada = new Entrada();
                capaEntrada.setNombreAtributo(dtCapa.getListaAtributos()[0].ToString());
                capaEntrada.setNombre(dtCapa.getNombreCapa());
                capaEntrada.setLayerCapa(dtCapa.getCapa());
                capaEntrada.setIndice(indice);
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

                //agrego la capa a la lista de capas del controlador
                this.capas.Add(capaEntrada);
                indice++;
            }

            string ahora = System.DateTime.Now.ToString("HHmmss");
            //paso 
            //creo la instancia de Blackmore, se crea la capa de red
            this.blackmore = new Blackmore(filasColumnas, vertical, horizontal, dst,
                                           layerCapaBase, ahora + "_celdasAux",
                                           this.wsBlackmore);

            //paso 2
            IFeatureLayer poligonosBlackmore = this.blackmore.getPoligonosBlackmore();

            //paso 3
            String rutaCapaUnion = rutaCapaBlackmore + @"\" + nombreCapaBlackmore + ".shp";
            IFeatureClass unionCapaBase = this.unionEspacial(poligonosBlackmore.FeatureClass, layerCapaBase, rutaCapaUnion, false, entradaBase.getNombreAtributo(), "merge_" + entradaBase.getIndice().ToString());

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
                    capaEntrada.setCapaUnion(this.unionEspacial(unionCapaBase, capaEntrada.getLayerCapa(), rutaCapaUnion, true, capaEntrada.getNombreAtributo(), "merge_" + capaEntrada.getIndice().ToString()));
                    auxMediaCapas += capaEntrada.getMedia();
                    cantCapas++;

                }
            }

            //paso 
            IFeatureClass featureUnion = entradaBase.getCapaUnion();
            //falta modificar el nombre de la capa para ser devuelta y setearla en la instancia de 'blackmore'

            //paso  
            int indiceDst = this.crearField(featureUnion, "std_dev", esriFieldType.esriFieldTypeDouble);
            //paso 
            int indiceMean = this.crearField(featureUnion, "mean", esriFieldType.esriFieldTypeDouble);
            //paso
            int indiceClasificacion = this.crearField(featureUnion, "clase", esriFieldType.esriFieldTypeInteger);


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
                c.clasificar(dst, auxMediaCapas / cantCapas);


                this.setValoresFeatureUnion(featureUnion, fid, indiceDst, c.getDesviacion(), indiceMean, c.getMedia(), indiceClasificacion, c.getClasificacion());

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
                    for (int i = fields.FieldCount - 1; i >= 0; i--)
                    {
                        f = fields.get_Field(i);
                        if (!camposGuardar.Contains(f.Name))
                            e.getCapaUnion().DeleteField(f);
                    }
                }
            }

            //borro capa de poligonos inicial
            if (((IDataset)(this.blackmore.getPoligonosBlackmore().FeatureClass)).CanDelete())
                ((IDataset)(this.blackmore.getPoligonosBlackmore().FeatureClass)).Delete();
        }
        catch (ProyectoException e)
        {
            throw new ProyectoException(e.Message);
        }
        catch
        {
            throw new ProyectoException("No se pudo completar la operación, verifique los datos.");
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
    private IFeatureClass unionEspacial(IFeatureClass entidadDestino, ILayer entidadUnion, string entidadSalida, bool mantenerEntidades, string nombreAtributo, string atributoTablaUnion)
    {
        Geoprocessor gpt = new Geoprocessor();
        try
        {
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


    //GONZALO: me parece q esta funcion va para Blackmore
    //GONZALO: ademas de lo que hace deberia 'limpiar' la tabla, o sea, eliminar los atributos estan de mas
    //Excepciones: OK
    //ProyectoException
    private void setValoresFeatureUnion(IFeatureClass featureUnion, int fid, int indiceDst, double dsv, int indiceMean, double mean, int indiceClasificacion, int clase)
    {
        try
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
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch
        {
            throw new ProyectoException("Ocurrio un error al escribir la tabla de valores de la nueva capa.");
        }
    }

    //crea un nuevo field con el nombre nombreField en el featureClass pasado como parametro
    //devuelve el indice del field creado
    //Exception OK
    //ProyectoException
    private int crearField(IFeatureClass featureClass, String nombreField, esriFieldType tipoField)
    {
        try
        {
            IField field = new FieldClass();
            IFieldEdit fieldEdit = (IFieldEdit)field;
            fieldEdit.Name_2 = nombreField;
            fieldEdit.Type_2 = tipoField;

            ISchemaLock schemaLock = (ISchemaLock)featureClass;
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
            featureClass.AddField(field);
            return featureClass.FindField(nombreField);
        }
        catch 
        {
            throw new ProyectoException("No se pudo agregar el campo " + nombreField + " a la tabla de salida.");
        }
    }

}