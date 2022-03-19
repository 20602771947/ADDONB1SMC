using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADDONB1SMC.Admin.VIEW
{
    public class frmControlAdmin
    {
        SAPbouiCOM.Form oForm = null;
        SAPbouiCOM.CheckBox chkFE;
        SAPbouiCOM.CheckBox chKConsultaPeru;
        SAPbouiCOM.CheckBox chkOpt3;
        SAPbouiCOM.Button btnCancelar;
        SAPbouiCOM.Button btnAceptar;

        public frmControlAdmin()
        {
            init();
        }


        public void init()
        {
            try
            {
                Globals.SBO_Application.Forms.Item("frmSMCGestionAdmin").Select();
            }
            catch (Exception)
            {

                LoadForm();
                InitForm();



            }
        }

        private void LoadForm()
        {
            string formType = "";
            var folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormControlGestion.srf");
            var xml = System.IO.File.ReadAllText(folder);

            SAPbouiCOM.FormCreationParams oParams = null;

            oParams = (SAPbouiCOM.FormCreationParams)Globals.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
            oParams.UniqueID = "frmSMCGestionAdmin";

            if (formType.Equals(""))
            {
                oParams.XmlData = xml;
            }
            else
            {
                oParams.FormType = formType;
            }


            oForm = Globals.SBO_Application.Forms.AddEx(oParams);
            oForm.Left = (Globals.SBO_Application.Desktop.Width - oForm.Width) / 2;
            oForm.Top = (Globals.SBO_Application.Desktop.Height - oForm.Height) / 2;
            oForm.Visible = true;
           // oForm.m
        }


        private void InitForm()
        {
            chkFE = (SAPbouiCOM.CheckBox)oForm.Items.Item("chkItem1").Specific;
            chKConsultaPeru = (SAPbouiCOM.CheckBox)oForm.Items.Item("chkItem2").Specific;
            chkOpt3 = (SAPbouiCOM.CheckBox)oForm.Items.Item("chkItem3").Specific;

            chkFE.ValOn = "Y";
            chkFE.ValOff = "N";


            chKConsultaPeru.ValOn = "Y";
            chKConsultaPeru.ValOff = "N";

            btnAceptar = (SAPbouiCOM.Button)oForm.Items.Item("btnOk").Specific;
            btnCancelar = (SAPbouiCOM.Button)oForm.Items.Item("btnCancel").Specific;

            btnAceptar.ClickAfter += BtnAceptar_ClickAfter;
            

        }

        private void BtnAceptar_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            
            if (Globals.SBO_Application.MessageBox("Desea guardar los cambios?") == 1)
            {
                
            }

        }
    }

}
