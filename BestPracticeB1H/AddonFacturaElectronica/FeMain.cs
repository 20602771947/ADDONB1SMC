using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADDONB1SMC.AddonFacturaElectronica
{
    public class FeMain
    {


   
        public static void CrearObjetosForm(SAPbouiCOM.Form oForm)
        {
           
           

            SAPbouiCOM.Item oItem;
            oItem = oForm.Items.Add("btnFE", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            oItem.Top = oForm.Items.Item(1).Top;
            oItem.Left = oForm.Items.Item(2).Left + 160;
            oItem.Width = 70;
            (oItem.Specific as SAPbouiCOM.Button).Caption = "Registrar CPE";
            (oItem.Specific as SAPbouiCOM.Button).ClickAfter += FeMain_ClickAfter;

        }

        private static void FeMain_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oForm = Globals.SBO_Application.Forms.Item(pVal.FormUID);
            Globals.SBO_Application.MessageBox("CLIC CLIC CLIC");

            oForm.Items.Item("16").Specific.Value = "ESCRIBIENDO TEXTO SDK";

            Globals.SBO_Application.MessageBox(oForm.Items.Item("16").Specific.Value.ToString() + " CAPTURADO");
            
            
        }

        public static void hola()
        {
            Globals.SBO_Application.MessageBox("CLIC");
        }
    }
}
