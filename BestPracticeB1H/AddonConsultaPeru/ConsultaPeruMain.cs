using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ADDONB1SMC.AddonConsultaPeru
{
    public class ConsultaPeruMain
    {
       static VIEW.frmSMCSociosN vSocios = null;

        public static void CrearObjetosForm(SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.Item oItem;
            oItem = oForm.Items.Add("btnCP", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            oItem.Top = oForm.Items.Item(40).Top;
            oItem.Left = oForm.Items.Item(41).Left + 160;
            oItem.Width = 70;
            (oItem.Specific as SAPbouiCOM.Button).Caption = "Buscar Sunat";
            (oItem.Specific as SAPbouiCOM.Button).ClickAfter += ConsultaPeruMain_ClickAfter;


            SAPbouiCOM.Item oItem1;
            oItem1 = oForm.Items.Add("btnCP01", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            //oItem1.Top = oForm.Items.Item(40).Top;
            oItem1.Left = oForm.Items.Item(41).Left + 160;
            oItem1.Width = 70;
            (oItem1.Specific as SAPbouiCOM.Button).Caption = "Buscar Masivo";
            (oItem1.Specific as SAPbouiCOM.Button).ClickAfter += ConsultaPeruMain_ClickAfter01;
            
        }




        private static void ConsultaPeruMain_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            SAPbouiCOM.Form oForm = Globals.SBO_Application.Forms.Item(pVal.FormUID);

            if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_FIND_MODE || oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
            {
                Globals.SBO_Application.MessageBox("No esta permitido el uso del boton en este modo");
                return;
            }

            ADDONB1SMC.WSMC.SRVB1SMCSoapClient Servicio = new WSMC.SRVB1SMCSoapClient();
            WSMC.SocioDeNegocioDTO result = Servicio.GetSocioDeNegocioSunat(oForm.Items.Item("41").Specific.Value.ToString());


            if (String.IsNullOrEmpty(result.RazonSocial)){
                oForm.Items.Item("7").Specific.Value = result.ApellidoPaterno + " " + result.ApellidoMaterno + " " + result.Nombres;
            }else{
                oForm.Items.Item("7").Specific.Value = result.RazonSocial;
           
            
            ((SAPbouiCOM.EditText)Globals.SBO_Application.Forms.GetForm("-134", pVal.FormTypeCount).Items.Item("U_SMC_Estado").Specific).Value = result.Estado;
            ((SAPbouiCOM.EditText)Globals.SBO_Application.Forms.GetForm("-134", pVal.FormTypeCount).Items.Item("U_SMC_Ubigeo").Specific).Value = result.Ubigeo;
            ((SAPbouiCOM.EditText)Globals.SBO_Application.Forms.GetForm("-134", pVal.FormTypeCount).Items.Item("U_SMC_Condicion").Specific).Value = result.Condicion;



            oForm.Freeze(true);

            var prevPane = oForm.PaneLevel;
            oForm.PaneLevel = 7;
            oForm.Items.Item("15").Click();
            var folderAddress = (SAPbouiCOM.Folder)oForm.Items.Item("15").Specific;
            folderAddress.Item.Click();
            folderAddress.Select();

            SAPbouiCOM.Matrix b1Matrix1 = (SAPbouiCOM.Matrix)oForm.Items.Item("178").Specific;
            var editAddressName = (SAPbouiCOM.EditText)b1Matrix1.Columns.Item("1").Cells.Item(1).Specific;
            var editAdress = (SAPbouiCOM.EditText)b1Matrix1.Columns.Item("2").Cells.Item(1).Specific;
            var departamento = (SAPbouiCOM.ComboBox)b1Matrix1.Columns.Item("7").Cells.Item(1).Specific;
            var provincia = (SAPbouiCOM.EditText)b1Matrix1.Columns.Item("6").Cells.Item(1).Specific;
            var distrito = (SAPbouiCOM.EditText)b1Matrix1.Columns.Item("5").Cells.Item(1).Specific;
            editAddressName.String = "FISCAL";
            editAdress.String = result.Direccion;
            var ubigeo = String.IsNullOrEmpty(result.Ubigeo.Substring(0, 2))?"": result.Ubigeo.Substring(0, 2);
            departamento.Select(ubigeo, SAPbouiCOM.BoSearchKey.psk_ByValue);
            provincia.String = String.IsNullOrEmpty(result.Provincia)?"" : result.Provincia;
            distrito.String = String.IsNullOrEmpty(result.Distrito)?"": result.Distrito;

            oForm.Freeze(false);

            }

            //SAPbouiCOM.Matrix b1Matrix = (SAPbouiCOM.Matrix)oForm.Items.Item("69").Specific;

            //b1Matrix.SelectionMode = new SAPbouiCOM.BoMatrixSelect;


            //b1Matrix.Columns.Item("20").Editable = true;

            //var itemAddressBillCell = (SAPbouiCOM.Cell)b1Matrix.Columns.Item("20").Cells.Item(2);
            //var itemAddressShipCell = (SAPbouiCOM.Cell)b1Matrix.Columns.Item("20").Cells.Item(4);
            //itemAddressBillCell.Click();

            //FillInNameInAddressID(EditText0.String)
            //funcion


            // b1Matrix1.Columns.Item("20").Editable = true;

            //var itemAddressBill = (SAPbouiCOM.Cell)b1Matrix1.Columns.Item("20").Cells.Item(2);
            //var itemAddressShip = (SAPbouiCOM.Cell)b1Matrix1.Columns.Item("20").Cells.Item(4);

            //end funcion

            //var itemAddressShipCell1 = (SAPbouiCOM.Cell)b1Matrix1.Columns.Item("20").Cells.Item(5);

            // itemAddressShipCell1.Click();







            //2.Inicialice un EditText en Matrix:

            //SAPbouiCOM.EditText oEditItemCode = (SAPbouiCOM.EditText)oMatrix.Columns.Item("1").Cells.Item(1).Specific;


            //((SAPbouiCOM.EditText)oMatrix.Columns.Item("1").Cells.Item(1).Specific).String = "Hola";
            //3.Establecer el valor en estos EditText:

            // oEditCode.Value = "Cualquier valor";

            // oEditItemCode.Value = "FISCAL";


            //var oMatrix = oForm.Items.Item("178").Specific;
            //((SAPbouiCOM.EditTextColumn)oMatrix.Columns.Item("2").Cells.Item(1).Specific).SetText(1,"hola"); // Col1 Column



            //Mtrx -- matrix id
            //((SAPbouiCOM.EditText)oMatrix.Columns.Item("2").Cells.Item(1).Specific).Value="o"; // Col1 Column



            //SAPbobsCOM.BusinessPartners businessObject = (SAPbobsCOM.BusinessPartners)Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
            //businessObject.UserFields.Fields.Item("U_SMC_Estado").Value = "dd";

            //oForm.Items.Item("U_SMC_Estado").Specific.Value = 1;



            //SAPbobsCOM.BusinessPartners businessObject = (SAPbobsCOM.BusinessPartners)ConexionSBO.BP.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);



            //var oCompany = (SAPbobsCOM.Company)Globals.SBO_Application.Company.GetDICompany();
            //SAPbobsCOM.BusinessPartners odoc = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
            //odoc.GetByKey(cardCode);
            //if (myInfo.Habido.Contains("NO HABIDO"))
            //odoc.UserFields.Fields.Item("U_SMC_Estado").Value = "hola";
            //if (myInfo.EstadoSunat.Contains("BAJA DEFINITIVA"))
            // odoc.UserFields.Fields.Item("U_SMC_BAJADEF").Value = "01";
            //int res = odoc.;






            //SAPbouiCOM.EditText txt = (SAPbouiCOM.EditText)oForm.Items.Item("U_SMC_Estado").Specific.Value;

            //txt.Value = "holaa";
            //SAPbouiCOM.Form oForma = Application.SBO_Application.Forms.Item(_x);




            //oForm.DataSources.DBDataSources.Item("U_SMC_Estado").SetValue(0,0, result.Estado);
            //oForm.DataSources.UserDataSources.Item("U_SMC_Estado").Value = result.Estado;
            //oForm.DataSources.UserDataSources.Item

            //oForm.Items.Item("U_SMC_Estado").Specific.value = result.Estado;
            //oForm.Items.Item("U_SMC_Ubigeo").Specific.Value = result.Ubigeo;
            //oForm.Items.Item("U_SMC_Condicion").Specific.Value = result.Condicion;
            //oForm.Items.Item("178").Specific.Value = result.Direccion;



        }




        private static void ConsultaPeruMain_ClickAfter01(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            vSocios = new VIEW.frmSMCSociosN();

            
        

            //SAPbouiCOM.Form oForm;
            //SAPbouiCOM.FormCreationParams creationPackage;
            //creationPackage = (SAPbouiCOM.FormCreationParams)Globals.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
            //creationPackage.UniqueID = "SMC_ListaSociosN";
            //creationPackage.FormType = "SMC_ListaSociosN";
            //creationPackage.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Fixed;
            //oForm = Globals.SBO_Application.Forms.AddEx(creationPackage);
            //oForm.Title = "Lista Socios de Negocio";
            //oForm.Left = 400;
            //oForm.Top = 100;
            //oForm.ClientWidth = 270;
            //oForm.ClientHeight = 154;
            //oForm.Visible = true;



        }


      


    }
}
