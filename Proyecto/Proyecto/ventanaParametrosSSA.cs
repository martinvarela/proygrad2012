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
        private string valorAux;
        
        public ventanaParametrosSSA()
        {
            InitializeComponent();

            Controlador controlador = Controlador.getInstancia;
            //cargo el valor de la temperatura inicial
            txtTemperatura.Text = controlador.getSSA().getTemperaturaInicial().ToString();
            //cargo el valor del factor de reduccion
            txtFactor.Text = controlador.getSSA().getFactorReduccion().ToString();
            //cargo el valor de epsilon
            txtEpsilon.Text = controlador.getSSA().getEpsilon().ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Controlador controlador = Controlador.getInstancia;
            SSA ssa = controlador.getSSA();
            ssa.setTemperaturaInicial(Double.Parse(txtTemperatura.Text.ToString()));
            ssa.setFactorReduccion(Double.Parse(txtFactor.Text.ToString()));
            ssa.setEpsilon(Double.Parse(txtEpsilon.Text.ToString()));

            controlador.setSSA(ssa);

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
        
        private void txtEpsilon_GotFocus(object sender, EventArgs e)
        {
            this.valorAux = txtEpsilon.Text.ToString();
        }
        private void txtEpsilon_LostFocus(object sender, EventArgs e)
        {
            if (txtEpsilon.Text.ToString() != valorAux)
            {
                double i = 0;
                if (double.TryParse(this.txtEpsilon.Text.ToString(), out i) && i > 0)
                {
                    valorAux = this.txtEpsilon.Text.ToString();
                }
                else
                {
                    this.txtEpsilon.Text = valorAux;
                }
            }
        }
    
    }
}
