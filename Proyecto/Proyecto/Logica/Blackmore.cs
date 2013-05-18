using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;


class Blackmore : Capa
{
    private int ancho;
    private int alto;
    private int filas;
    private int columnas;
    private double parametroDST;
    private IFeatureLayer layerPoligonos;
    private List<Celda> celdas;

    public Blackmore(DTPBlackmore dtp)
    {
        bool filasColumnas = dtp.getFilasColumnas();
        int vertical = dtp.getVertical();
        int horizontal = dtp.getHorizontal();
        double dst = dtp.getDdst();
        ILayer layerTemplate = dtp.getLayerTemplate();
        string nombreCapaBlackmore = dtp.getNombreCapaBlackmore();
        IWorkspace wsCapaBlackmore = dtp.getWsCapaBlackmore();

        if (filasColumnas)
        {
            this.filas = vertical;
            this.columnas = horizontal;
            this.ancho = 0;
            this.alto = 0;
        }
        else
        {
            this.ancho = horizontal;
            this.alto = vertical;
            this.filas = 0;
            this.columnas = 0;
            //se setea la proyeccion de la capa base si es que no la tiene
            //esto es para que el tamaño de las celdas de la grilla sea en metros
            IGeoDataset gds = layerTemplate as IGeoDataset;
            IProjectedCoordinateSystem projCoordSys = gds.SpatialReference as IProjectedCoordinateSystem;
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference spatialReference = spatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_WGS1984UTM_21S);
            layerTemplate.SpatialReference = spatialReference;
        }

        this.parametroDST = dst;

        //se crea la featureClass de salida (red)
        Geoprocessor gp = new Geoprocessor();
        ESRI.ArcGIS.DataManagementTools.CreateFishnet fishNet = new ESRI.ArcGIS.DataManagementTools.CreateFishnet();
        fishNet.out_feature_class = wsCapaBlackmore.PathName + "\\" + nombreCapaBlackmore;

        fishNet.origin_coord = layerTemplate.AreaOfInterest.XMin.ToString() + " " + layerTemplate.AreaOfInterest.YMin.ToString();
        double medio = (layerTemplate.AreaOfInterest.YMin + layerTemplate.AreaOfInterest.YMax) / 2;
        fishNet.y_axis_coord = layerTemplate.AreaOfInterest.XMin.ToString() + " " + medio.ToString();
        fishNet.corner_coord = layerTemplate.AreaOfInterest.XMax.ToString() + " " + layerTemplate.AreaOfInterest.YMax.ToString();

        fishNet.number_rows = this.filas;
        fishNet.number_columns = this.columnas;
        fishNet.cell_height = this.alto;
        fishNet.cell_width = this.ancho;
        
        fishNet.template = layerTemplate;
        fishNet.geometry_type = "POLYGON";
        fishNet.labels = "NO_LABELS";
        
        gp.AddOutputsToMap = false;
        gp.OverwriteOutput = true;

        IFeatureClass fc0;
        IQueryFilter qf;
        try
        {
            IGPUtilities gpUtils = new GPUtilitiesClass();

            IGeoProcessorResult result = (IGeoProcessorResult)gp.Execute(fishNet, null);
            //fc0 viene la capa de poligono de Blackmore.
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc0, out qf);

            //se busca la capa de poligonos de Blackmore para setear en la instancia de Blackmore
            IFeatureLayer fLayer = new FeatureLayerClass();
            fLayer.FeatureClass = fc0;
            IDataset dataSet = (IDataset)layerTemplate;

            if (dataSet is ESRI.ArcGIS.Geodatabase.IGeoDataset)
            {
                //then grab the spatial reference information and return it.
                ESRI.ArcGIS.Geodatabase.IGeoDataset geoDataset = (ESRI.ArcGIS.Geodatabase.IGeoDataset)dataSet;
                fLayer.SpatialReference = geoDataset.SpatialReference;
            }
            this.layerPoligonos = fLayer;
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


    //crea una instancia de Celda para cada registro de featureCapa.
    //Setea el atributo FID de cada registro en la Celda
    public void crearCeldas(IFeatureClass featureCapa)
    {
        IFeatureCursor cursorPoligono = featureCapa.Search(null, false);
        int indice = featureCapa.FindField("FID");
        IFeature poligono = cursorPoligono.NextFeature();
        
        this.celdas = new List<Celda>();
        while (poligono != null)
        {
            Celda c = new Celda();
            c.setFID((int)poligono.get_Value(indice));

            this.celdas.Add(c);
            poligono = cursorPoligono.NextFeature();
        }
        System.Runtime.InteropServices.Marshal.ReleaseComObject(cursorPoligono);

    }

    public IFeatureLayer getPoligonosBlackmore()
    {
        return this.layerPoligonos;
    }

    public List<Celda> getCeldas()
    {
        return this.celdas;
    }

    public void setDatos(Celda celda, DTDatosDM datosCelda)
    {
        celda.setDatos(datosCelda);
    }

    public void completarFeature(IFeatureClass featureUnion, List<Capa> entradas, double mediaGeneral)
    {
        //falta modificar el nombre de la capa para ser devuelta y setearla en la instancia de 'blackmore'

        //paso  
        int indiceDst = this.crearField(featureUnion, "std_dev", esriFieldType.esriFieldTypeDouble);
        //paso 
        int indiceMean = this.crearField(featureUnion, "mean", esriFieldType.esriFieldTypeDouble);
        //paso
        int indiceClasificacion = this.crearField(featureUnion, "clase", esriFieldType.esriFieldTypeInteger);

        //paso 
        foreach (Celda c in this.celdas)
        {
            int fid = c.getFID();
            double auxValor = 0;
            double dstCelda = 0;
            double valorCelda = 0;
            int n = 0;
            foreach (Entrada e in entradas)
            {
                auxValor = e.getValorCelda(fid);
                if (auxValor != 0)
                {
                    dstCelda += Math.Pow(auxValor - e.getMedia(), 2);
                    valorCelda += auxValor;
                    n++;
                }
                auxValor = 0;
            }
            c.setDesviacion(Math.Sqrt(dstCelda / n));
            c.setMedia(valorCelda / n);
            c.clasificar(this.parametroDST, mediaGeneral);

            this.setValoresFeatureUnion(new DTPSetValoresFeatureUnion(featureUnion, fid, indiceDst, c.getDesviacion(), indiceMean, c.getMedia(), indiceClasificacion, c.getClasificacion()));
        }
    }

    //crea un nuevo field con el nombre nombreField en el featureClass pasado como parametro
    //devuelve el indice del field creado
    //Exception OK
    //ProyectoException
    private int crearField(IFeatureClass featureClass, string nombreField, esriFieldType tipoField)
    {
        ISchemaLock schemaLock = (ISchemaLock)featureClass;
        try
        {
            IField field = new FieldClass();
            IFieldEdit fieldEdit = (IFieldEdit)field;
            fieldEdit.Name_2 = nombreField;
            fieldEdit.Type_2 = tipoField;

            schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
            featureClass.AddField(field);
            return featureClass.FindField(nombreField);
        }
        catch
        {
            throw new ProyectoException("No se pudo agregar el campo " + nombreField + " a la tabla de salida.");
        }
        finally
        {
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
        }
    }


    //Excepciones: OK
    //ProyectoException
    private void setValoresFeatureUnion(DTPSetValoresFeatureUnion dtp)
    {
        try
        {
            IFeatureClass featureUnion = dtp.getFeatureUnion();
            int fid = dtp.getFid();
            int indiceDst = dtp.getIndiceDst() ;
            double dsv = dtp.getDsv();
            int indiceMean = dtp.getIndiceMean();
            double mean = dtp.getMean();
            int indiceClasificacion = dtp.getIndiceClasificacion();
            int clase = dtp.getClase();

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
            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
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

}
