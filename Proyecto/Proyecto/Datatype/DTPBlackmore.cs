using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

class DTPBlackmore
{
    private bool filasColumnas;
    private int vertical;
    private int horizontal;
    private double dst;
    private ILayer layerTemplate;
    private string nombreCapaBlackmore;
    private IWorkspace wsCapaBlackmore;

    public DTPBlackmore(bool filasColumnas, int vertical, int horizontal, double dst, ILayer layerTemplate, string nombreCapaBlackmore, IWorkspace wsCapaBlackmore)
    {
        this.filasColumnas = filasColumnas;
        this.vertical = vertical;
        this.horizontal = horizontal;
        this.dst = dst;
        this.layerTemplate = layerTemplate;
        this.nombreCapaBlackmore = nombreCapaBlackmore;
        this.wsCapaBlackmore = wsCapaBlackmore;
    }

    public bool getFilasColumnas()
    {
        return this.filasColumnas;
    }

    public int getVertical()
    {
        return this.vertical;
    }
    
    public int getHorizontal()
    {
        return this.horizontal;
    }
    
    public double getDdst()
    {
        return this.dst;
    }
    
    public ILayer getLayerTemplate()
    {
        return this.layerTemplate;
    }
    
    public string getNombreCapaBlackmore()
    {
        return this.nombreCapaBlackmore;
    }
    
    public IWorkspace getWsCapaBlackmore()
    {
        return this.wsCapaBlackmore;
    }



}
