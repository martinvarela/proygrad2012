using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IBlackmore
{
    void crearBlackmore(bool filasColumnas, int vertical, int horizontal, List<DTCapasBlackmore> capasDT, double dst, string nombreCapaBlackmore, string rutaCapaBlackmore);
    List<DTCapasBlackmore> cargarCapasBlackmore();
}
