using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

interface IOptimizar
{
    void optimizarMuestreo(DTPOptimizarMuestreo dtp);
    
    List<string> cargarCapasMuestreo();

    int calcularArea(string nombreCapa);

    int setearRango(int r);

    DTParametrosSSA getParametrosSSA();

    void setParametrosSSA(DTParametrosSSA dt);
}
