using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADDONB1SMC.AddonConsultaPeru
{
    public class ConsultaPeruMain
    {
        public static void CrearObjetosForm(SAPbouiCOM.Form oForm)
        {

            SAPbouiCOM.Item oItem;
            oItem = oForm.Items.Add("btnCP", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            oItem.Top = oForm.Items.Item(5).Top;
            oItem.Left = oForm.Items.Item(41).Left + 160;
            oItem.Width = 70;
            (oItem.Specific as SAPbouiCOM.Button).Caption = "Buscar";
            (oItem.Specific as SAPbouiCOM.Button).ClickAfter += ConsultaPeruMain_ClickAfter;

        }

        private static void ConsultaPeruMain_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oForm = Globals.SBO_Application.Forms.Item(pVal.FormUID);
            //Globals.SBO_Application.MessageBox("CLIC CLIC CLIC");

            //oForm.Items.Item("16").Specific.Value = "ESCRIBIENDO TEXTO SDK";

            ADDONB1SMC.WSMC.SRVB1SMCSoapClient Servicio = new WSMC.SRVB1SMCSoapClient();

            WSMC.SocioDeNegocioDTO result = Servicio.GetSocioDeNegocioSunat(oForm.Items.Item("41").Specific.Value.ToString());
           

            oForm.Items.Item("7").Specific.Value = result.RazonSocial;
           
            //Globals.SBO_Application.MessageBox();


            //;

        }

        //public static void hola()
        //{
        //    Globals.SBO_Application.MessageBox("CLIC");
        //}

    }
}
