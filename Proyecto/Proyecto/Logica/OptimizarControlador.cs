using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;
using Proyecto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;

class OptimizarControlador : IOptimizar
{
    private static OptimizarControlador instancia;
    private IWorkspace wsSSA;
    private SSA ssa;
    private double area;
    private int rango;

    public static OptimizarControlador getInstancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new OptimizarControlador();
            }
            return instancia;
        }
    }

    public OptimizarControlador()
    {
        this.ssa = new SSA();
    }

    //capaPuntosMuestreo es la capa seleccionada por el usuario
    //metodoInterpolacion puede ser IDW o Kriging
    //rango ??? o cantmuestras
    //error maximo aceptado en % ej: 5
    //Excepciones: OK
    //ProyectoException
    public void optimizarMuestreo(DTPOptimizarMuestreo dtp)
    {
        try
        {
            IFeatureClass capaPuntosMuestreo = dtp.getCapaPuntosMuestreo();
            string metodoInterpolacion = dtp.getMetodoInterpolacion();
            double expIDW = dtp.getExpIDW();
            int nroMuestras = dtp.getNroMuestras();
            double error = dtp.getError();
            string rutaCapa = dtp.getRutaCapa();

            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();

            //esta ruta la indica el usuario
            string fechaActual = System.DateTime.Now.ToString("ddMMyyyy_HHmm");
            string nombreDirectorio = fechaActual + "_Capas";
            string nombreArchivo = fechaActual + "_Resumen.txt";
            string pathCombinado = System.IO.Path.Combine(rutaCapa, nombreDirectorio);
            string pathArchivo = System.IO.Path.Combine(rutaCapa, nombreArchivo);

            System.IO.Directory.CreateDirectory(pathCombinado);
            IWorkspace workspace = workspaceFactory.OpenFromFile(pathCombinado, 0);
            this.wsSSA = workspace;
            this.ssa.setWorkspace(this.wsSSA);
            this.ssa.cantMuestras = nroMuestras;
            IFeatureClass resultado = this.ssa.simulatedAnnealing(new DTPSimulatedAnnealing(capaPuntosMuestreo, metodoInterpolacion, expIDW, error, pathArchivo));
        }
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch
        {
            throw new ProyectoException("Ha ocurrido un error al ejecutar la optimización del diseño de muestreo.");
        }
    }

    //Devuelve los nombres de las capas abiertas en ArcMap que tienen: un fields de nombre "Valor" y geometria de tipo punto.
    //Excepciones: OK
    //ProyectoException
    public List<string> cargarCapasMuestreo()
    {
        try
        {
            List<string> listaCapas = new List<string>();

            IMap targetMap = ArcMap.Document.FocusMap;
            //cargo el combo de capas abiertas
            IEnumLayer enumLayers = targetMap.get_Layers();
            enumLayers.Reset();
            ILayer layer = enumLayers.Next();

            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            while (layer != null)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer != null)
                {
                    IFeatureClass fc = featureLayer.FeatureClass;
                    if (fc.FindField("Valor") != -1 && fc.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        listaCapas.Add(layer.Name.ToString());
                    }
                }
                layer = enumLayers.Next();
            }
            return listaCapas;
        }
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);            
        }
        catch
        {
            throw new ProyectoException("Error inesperado al cargar las capas de posibles muestreos.");
        }
    }

    //Devuelve el area en metros de la capa pasada como parametro.
    //Excepciones: OK
    //ProyectoException
    public int calcularArea(string nombreCapa)
    {
        try
        {
            Geoprocessor gp = new Geoprocessor();
            
            ESRI.ArcGIS.DataManagementTools.MinimumBoundingGeometry poligono = new ESRI.ArcGIS.DataManagementTools.MinimumBoundingGeometry();
            poligono.geometry_type = "CONVEX_HULL";
            poligono.group_option = "ALL";
            poligono.in_features = nombreCapa;
            gp.TemporaryMapLayers = true;
            gp.AddOutputsToMap = false;

            IFeatureClass fc;
            IQueryFilter qf;
            
            IGPUtilities gpUtils = new GPUtilitiesClass();
            IGeoProcessorResult result = (IGeoProcessorResult)gp.Execute(poligono, null);
            gpUtils.DecodeFeatureLayer(result.GetOutput(0), out fc, out qf);
            
            IFeatureCursor cursorPoligono = fc.Search(null, false);
            int indice = fc.FindField("Shape_Area");
            IFeature datosPoligono = cursorPoligono.NextFeature();
            while (datosPoligono != null)
            {
                this.area = (double)datosPoligono.get_Value(indice);
                datosPoligono = cursorPoligono.NextFeature();
            }

            /*se elimina la feature class del poligono creado*/
            if (((IDataset)fc).CanDelete())
                ((IDataset)fc).Delete();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursorPoligono);

            if (this.rango == -1)
                return -1;
            else
                return this.calcularNroMuestras();
        }
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch 
        {
            throw new ProyectoException("Ha ocurrido un error al calcular el area de la capa '" + nombreCapa.ToString() + "'");
        }
    }

    //Setea el valor del rango que elige el usuario.
    //Excepciones: OK
    //ProyectoException
    public int setearRango(int r)
    {
        try
        {
            this.rango = r;
            if (this.area == -1)
                return -1;
            else
                return calcularNroMuestras();
        }
        catch (ProyectoException p)
        {
            throw new ProyectoException(p.Message);
        }
        catch
        {
            throw new ProyectoException("Ha ocurrido un error al setear el rango.");
        }
    }

    //Devuelve los parametros de SSA.
    //Excepciones: OK
    //ProyectoException
    public DTParametrosSSA getParametrosSSA()
    {
        try
        {
            DTParametrosSSA dt = new DTParametrosSSA(this.ssa.getTemperaturaInicial(), this.ssa.getFactorReduccion(), this.ssa.getIteraciones());
            return dt;
        }
        catch
        {
            throw new ProyectoException("Ha ocurrido un error al obtener los parámetros de SSA.");
        }
    }

    //Setea los parametros de SSA.
    //Excepciones: OK
    //ProyectoException
    public void setParametrosSSA(DTParametrosSSA dt)
    {
        try
        {
            this.ssa.setTemperaturaInicial(dt.getTemperaturaInicial());
            this.ssa.setFactorReduccion(dt.getFactorReduccion());
            this.ssa.setIteraciones(dt.getIteraciones());
        }
        catch
        {
            throw new ProyectoException("Ha ocurrido un error al guardar los parámetros de SSA.");
        }
    }

    //Devuelve la cantidad de numero de muestras
    //Excepciones: OK
    //ProyectoException
    private int calcularNroMuestras()
    {
        try
        {
            int muestras = (int)(Math.Round(this.area * 4 / Math.Pow(this.rango, 2)));
            return muestras;
        }
        catch
        {
            throw new ProyectoException("Ha ocurrido un error al calcular el número de muestras.");
        }
    }

}
