using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace Proyecto
{
    class classBlackmore
    {
        public void crearRed(IMap targetMap, string rutaLayer, string nombreLayer, bool selectable)
        {
            try
            {
                // Create a FeatureLayer and assign a shapefile to it.
                IFeatureLayer featureLayer = new FeatureLayerClass();
                IEnumLayer enumlayers = targetMap.get_Layers();

                enumlayers.Reset();
                ILayer capa = enumlayers.Next();
                
                Point origen = new Point();
                origen.X = -58.027281;
                origen.Y = -31.996907;

                Point opuesto = new Point();
                opuesto.X = -58.007481;
                opuesto.Y = -31.980507;


                Double anchoCelda = 0.01;
                Double altoCelda = 0.01;
                int nroFilas = 0;
                int nroColumnas = 0;

                //ESRI.ArcGIS.esriSystem.IAoInitialize ArcObjects = new ESRI.ArcGIS.esriSystem.AoInitializeClass();
                //ArcObjects.Initialize(esriLicenseProductCode.esriLicenseProductCodeArcInfo);
                //ArcObjects.Initialize(esriLicenseProductCode.esriLicenseProductCodeArcEditor);
                //ArcObjects.Initialize(esriLicenseProductCode.esriLicenseProductCodeArcServer);  

                ESRI.ArcGIS.esriSystem.AoInitialize ao = new AoInitialize();
                esriLicenseStatus status = ao.Initialize(ESRI.ArcGIS.esriSystem.esriLicenseProductCode.esriLicenseProductCodeEngine);
                if (status == esriLicenseStatus.esriLicenseUnavailable ||
                    status == esriLicenseStatus.esriLicenseFailure ||
                    status == esriLicenseStatus.esriLicenseNotLicensed || true)
                {

                    Geoprocessor gp = new Geoprocessor();


                    //ESRI.ArcGIS.DataManagementTools.CreateFishnet fishNet = new ESRI.ArcGIS.DataManagementTools.CreateFishnet();
                    ESRI.ArcGIS.DataManagementTools.CreateFishnet fishNet = new ESRI.ArcGIS.DataManagementTools.CreateFishnet();
                    fishNet.out_feature_class = capa;
                    fishNet.origin_coord = origen;
                    fishNet.y_axis_coord = origen;
                    fishNet.cell_width = anchoCelda;
                    fishNet.cell_height = altoCelda;
                    fishNet.number_rows = nroFilas;
                    fishNet.number_columns = nroColumnas;

                    fishNet.out_label = "salidaasdasd";

                    //fishNet.template = capa;
                    //fishNet.geometry_type = "POLYGON";
                    gp.AddOutputsToMap = true;
                    gp.OverwriteOutput = true;
                    //ESRI.ArcGIS.esriSystem.IVariantArray iva = new ESRI.ArcGIS.esriSystem.VarArray();

                    //gp.AddToolbox(fishNet.ToolboxDirectory+"\\"+fishNet.ToolName);
                    gp.Execute(fishNet, null);//.Execute(fishNet,null);


                    //ILayer layerFishNet = (ILayer)featureFishNet.FeatureClass;
                    ///pFLayer.Name = "Sample XY Event layer";
                    //featureLayer.Name = "nombre del feature";

                    //layer.Name = rutaLayer + nombreLayer;

                    //layer.Visible = true;
                    //featureLayer.Selectable = selectable;

                    // Add the layer to the focus map.
                    //                targetMap.AddLayer(featureFishNet);
                    //targetMap.AddLayer(layerFishNet);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }

        }

    }
}
