//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

using System.Windows.Forms;
using System;
using Proyecto;
using System.Drawing;
public partial class VentanaErrores : Form
{
    public VentanaErrores(int cantidadErrores, string[] stringErrores)
    {
        InitializeComponent();

        //cargo el campo con el detalle de los errores
        string errores = "";
        for (int i = 0; i < cantidadErrores; i++)
            errores = errores + stringErrores[i] + "{0}";

        this.lblErrores.Text = string.Format(errores, Environment.NewLine);

        //defino el tamano de la ventana
        int altoTextoErrores = int.Parse(this.lblErrores.Size.Height.ToString());
        int anchoTextoErrores = int.Parse(this.lblErrores.Size.Width.ToString());

        if (altoTextoErrores < 30)
            altoTextoErrores = 30;
            
        int altoVentana = 33 + altoTextoErrores + 33 + 61;
        int anchoVentana = 132 + anchoTextoErrores;

        Size s = new Size(anchoVentana, altoVentana);
        this.Size = s;
    }

    private void btnAceptar_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}
