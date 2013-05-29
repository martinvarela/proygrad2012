using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class ventanaParametrosSSA : Form
    {
        private IOptimizar ioptimizar;
        private string valorAux;
        
        public ventanaParametrosSSA()
        {
            InitializeComponent();

            Fabrica fabrica = Fabrica.getInstancia;
            this.ioptimizar = fabrica.getIOptimizar();

            DTParametrosSSA dtp = ioptimizar.getParametrosSSA();
            //cargo el valor de la temperatura inicial
            txtTemperatura.Text = dtp.getTemperaturaInicial().ToString();
            //cargo el valor del factor de reduccion
            txtFactor.Text = dtp.getFactorReduccion().ToString();
            //cargo el valor de iteraciones
            txtIteraciones.Text = dtp.getIteraciones().ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DTParametrosSSA dtp = new DTParametrosSSA(Double.Parse(txtTemperatura.Text.ToString()), Double.Parse(txtFactor.Text.ToString()), int.Parse(txtIteraciones.Text.ToString()));
            ioptimizar.setParametrosSSA(dtp);

            this.Close();
        }

        private void txtTemperatura_GotFocus(object sender, EventArgs e)
        {
            this.valorAux = txtTemperatura.Text.ToString();
        }
        private void txtTemperatura_LostFocus(object sender, EventArgs e)
        {
            if (txtTemperatura.Text.ToString() != valorAux)
            {
                double i = 0;
                if (double.TryParse(this.txtTemperatura.Text.ToString(), out i) && i > 0)
                {
                    valorAux = this.txtTemperatura.Text.ToString();
                }
                else
                {
                    this.txtTemperatura.Text = valorAux;
                }
            }
        }

        private void txtFactor_GotFocus(object sender, EventArgs e)
        {
            this.valorAux = txtFactor.Text.ToString();
        }
        private void txtFactor_LostFocus(object sender, EventArgs e)
        {
            if (txtFactor.Text.ToString() != valorAux)
            {
                double i = 0;
                if (double.TryParse(this.txtFactor.Text.ToString(), out i) && i > 0)
                {
                    valorAux = this.txtFactor.Text.ToString();
                }
                else
                {
                    this.txtFactor.Text = valorAux;
                }
            }
        }
        
        private void txtIteraciones_GotFocus(object sender, EventArgs e)
        {
            this.valorAux = txtIteraciones.Text.ToString();
        }
        private void txtIteraciones_LostFocus(object sender, EventArgs e)
        {
            if (txtIteraciones.Text.ToString() != valorAux)
            {
                int i = 0;
                if (int.TryParse(this.txtIteraciones.Text.ToString(), out i) && i > 0)
                {
                    valorAux = this.txtIteraciones.Text.ToString();
                }
                else
                {
                    this.txtIteraciones.Text = valorAux;
                }
            }
        }
    }
}
