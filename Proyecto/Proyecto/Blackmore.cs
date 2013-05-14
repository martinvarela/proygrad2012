using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Carto;


class Blackmore : Capa
{
    private int ancho;
    private int alto;
    private int filas;
    private int columnas;
    private double parametroDST;
    private IFeatureLayer layerPoligonos;
    private IFeatureClass featureBlackmore;
    private List<Celda> celdas;

    public List<Celda> getCeldas()
    {
        return this.celdas;
    }
    public void setDatos(Celda celda, DTDatosDM datosCelda)
    {
        celda.setDatos(datosCelda);
    }

    //public void agregarCelda(Celda celda)
    //{
    //    if (this.celdas == null)
    //        this.celdas = new List<Celda>();

    //    this.celdas.Add(celda);
    //}

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
    }

    public double getParametroDST()
    {
        return this.parametroDST;
    }

    public IFeatureLayer getPoligonosBlackmore()
    {
        return this.layerPoligonos;
    }

    public Blackmore(bool filasColumnas, int vertical, int horizontal, double dst, ILayer layerTemplate, string nombreCapaBlackmore, IWorkspace wsCapaBlackmore)
    {
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

        
        gp.AddOutputsToMap = true;
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

}
