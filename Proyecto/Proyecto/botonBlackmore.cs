using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using System.Data;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
//using ESRI.ArcGIS.esriSystem;
//using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;


namespace Proyecto
{
    public class botonBlackmore : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public botonBlackmore()
        {
        }

        protected override void OnClick()
        {
            //se crea la ventana principal de Blackmore
            ventanaBlackmore ventana = new ventanaBlackmore();

            //se obtienen las capas abiertas
            IMap map = ArcMap.Document.FocusMap;
            IEnumLayer enumlayers = map.get_Layers();

            enumlayers.Reset();
            ILayer layer = enumlayers.Next();
            int cantFilas = 0;

            while (layer != null)
            {
                String s = layer.Name.ToString();

                //checkbox
                DataGridViewCheckBoxCell ckeck = new DataGridViewCheckBoxCell();
                if (layer.Visible)
                {
                    ckeck.Value = true;
                }
                else
                {
                    ckeck.Value = false;
                }

                //textbox
                DataGridViewTextBoxCell text = new DataGridViewTextBoxCell();
                text.Value = s.ToString();

                //combobox
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                int cantidadCampos = featureLayer.FeatureClass.Fields.FieldCount;

                DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();

                for (int i = 0; i < cantidadCampos; i++)
                {
                    combo.Items.Add(featureLayer.FeatureClass.Fields.get_Field(i).Name.ToString());
                }

                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(ckeck);
                row.Cells.Add(text);
                row.Cells.Add(combo);

                ventana.dataGridView1.Rows.Add(row);
                cantFilas++;

                layer = enumlayers.Next();
            }

            //se hace visible la ventana
            ventana.botonRed.Visible = true;
            ventana.Visible = true;
        }

        //protected override void onupdate()
        //{
        //    enabled = arcmap.application != null;
        //}
    }

}
