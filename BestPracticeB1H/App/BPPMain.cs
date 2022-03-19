using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using ADDONB1SMC.NewItems;
using ADDONB1SMC.DB_Structure;


using ADDONB1SMC.AddonFacturaElectronica;
using ADDONB1SMC.AddonConsultaPeru;
using ADDONB1SMC.Validations;

namespace ADDONB1SMC
{
    public class BPPMain
    {
        #region variables
        private static int[] MandatoryUDFWindow = 
            new int[] { -134, -150, -63, -62, -804, -140, -180, -65300, -133, -60090, -65302, -65303, -65304, -65305, -65307, -179, -60091,
                        -1470000200, -540000988, -142, -143, -182, -65301, -141, -65306, -181, -60092, -426, -170, -721, -720, -940 };
        private static int[] MandatoryUDFMainWindow =
            new int[] { 134, 150, 63, 62, 804, 140, 180, 65300, 133, 60090, 65302, 65303, 65304, 65305, 65307, 179, 60091,
                        1470000200, 540000988, 142, 143, 182, 65301, 141, 65306, 181, 60092, 426, 170, 721, 720, 940, 70001 };
        private static string[] FormIdForDownpayment = new string[] { "141", "133" };
        private static string[] SalesDocs = new string[] { "140", "180", "65300", "133", "60090", "65302", "65303", "65304", "65305", "65307", "179", "60091" };
        private static string[] PurchsDocs = new string[] { "143", "182", "65301", "141", "65306", "181", "60092" };
        private static string[] InvDocs = new string[] { "720", "721", "940" };
        private static bool CheckUDFWindow = true;
        private static string FileName;
        #endregion

        public BPPMain()
        {

            


            Connect.SetApplication();
            Connect.ConnectToCompany();
            Globals.SAPVersion = Globals.oCompany.Version;
            InitialParams.Get();
            Globals.FE = "Y";
            Globals.ConsultaPeru = "Y";

            
            
            
            Globals.licensed = true;
            Globals.SBO_Application.SetStatusBarMessage("Validando estructura de la Base de Datos", SAPbouiCOM.BoMessageTime.bmt_Short, false);
            if (Globals.licensed)
            {                
                if (Globals.IsHana() == true) Globals.RunQuery(ADDONB1SMC.Properties.Resources.HanaSari.ToString());
                else Globals.RunQuery(ADDONB1SMC.Properties.Resources.SQLSari.ToString());
                Globals.Addon = Globals.oRec.Fields.Item(0).Value.ToString();
                Globals.version = Globals.oRec.Fields.Item(1).Value.ToString();
                Globals.Release(Globals.oRec);
                Globals.User = Globals.oCompany.UserName;
                Globals.Query = "select SUPERUSER from OUSR where \"USER_CODE\" = '" + Globals.User + "'";
                Globals.RunQuery(Globals.Query);
                string res = Globals.oRec.Fields.Item(0).Value;
                Globals.Release(Globals.oRec);
                if (res == "Y") Globals.SuperUser = true; else Globals.SuperUser = false;
                #region Revisa Versión Cloud
                if (Globals.Addon == "")
                {
                    Globals.Addon = Assembly.GetEntryAssembly().GetName().Name;
                    Version version = Assembly.GetEntryAssembly().GetName().Version;
                    Globals.version = version.ToString().Replace(".0.0", "");
                }
                #endregion
                #region Estructura
                //Crear Tablas y Campos de Usuario
                Setup oSetup = new Setup();
                Globals.Actual = oSetup.validarVersion(Globals.Addon, Globals.version);
                if (Globals.Actual == false)
                {
                    Globals.Query = "SELECT \"Name\" FROM OCRY WHERE \"Code\" = '" + InitialParams.Country + "'";
                    Globals.RunQuery(Globals.Query);
                    Globals.oRec.MoveFirst();
                    string oSelCountry = Globals.oRec.Fields.Item(0).Value;
                    Globals.Release(Globals.oRec);
                    if (InitialParams.Country != "PE")
                    {
                        Globals.SBO_Application.MessageBox("Seleccione un país válido, salga de SAP e intente nuevamente.");
                        return;
                    }
                    else
                    {
                        if (Globals.SBO_Application.MessageBox("Se va a crear la estructura para el país: " + oSelCountry + "\nSi desea continuar presione Sí;\nCaso contrario presione No, cambie el país y reinicie la aplicación.\n El ADDONB1SMC no funcionará correctamente si no se actualiza la Base de Datos.\n", 1, "Si", "No") == 1)
                        {
                            CreateStructure.CreateStruct();
                            
                         
                            Globals.continuar = 0;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                    Globals.continuar = 0;
                #endregion
             
                
                ERPMenu.AddMenuItems();
                //ShowSplash.ShowSplashScreen();
                Globals.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
                Globals.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
                Globals.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                Globals.SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SBO_Application_MenuEvent);
                Globals.SBO_Application.MessageBox("ADDONB1SMC Conectado");
            }
          
          
            Globals.SBO_Application.StatusBar.SetText("ADDONB1SMC está conectado.", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
        }

        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (pVal.FormTypeEx != "0")
            {
                try
                {
                    

                    #region //FormDraw





                    if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DRAW && CheckUDFWindow)
                    {
                        if (MandatoryUDFMainWindow.Contains(pVal.FormType) & pVal.Action_Success == true)
                        {
                            if (!Globals.SBO_Application.Menus.Item("6913").Checked)
                            {
                                Globals.SBO_Application.Menus.Item("6913").Activate();
                            }
                        }
                    }
                    #endregion

                    #region //FormLoad
                    if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD)
                    {

                        //FACTURA DEUDORES
                        if (pVal.FormType==133 && pVal.BeforeAction == true & Globals.FE == "Y")
                        {
                            SAPbouiCOM.Form oForm = Globals.SBO_Application.Forms.Item(pVal.FormUID);
                            FeMain.CrearObjetosForm(oForm);
                        }

                        //SOCIOS DE NEGOCIO
                        if (pVal.FormType == 134 && pVal.BeforeAction == true & Globals.ConsultaPeru == "Y")
                        {
                            SAPbouiCOM.Form oForm = Globals.SBO_Application.Forms.Item(pVal.FormUID);
                            ConsultaPeruMain.CrearObjetosForm(oForm);
                        }

                    }
                    #endregion


                    #region //FormEdit
                    //if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_VALIDATE || pVal.EventType == SAPbouiCOM.BoEventTypes.et_KEY_DOWN || pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD)
                    //{
                    //    if (pVal.FormType == 134 && pVal.BeforeAction == true & Globals.ConsultaPeru == "Y")
                    //    {
                    //        SAPbouiCOM.Form oForm = Globals.SBO_Application.Forms.ActiveForm;
                    //        if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_FIND_MODE)
                    //        {
                    //            ((SAPbouiCOM.Button)Globals.SBO_Application.Forms.GetForm("134", pVal.FormTypeCount).Items.Item("btnCP").Specific).Item.Enabled = false;
                    //        }
                    //    }

                    //}
                    #endregion


                    #region //Combo Select
                    if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT & pVal.Action_Success == true)
                    {
                        //ComboSelectActions.ComboSelect(oForm, pVal.FormType, pVal.ItemUID);
                    }
                    #endregion

                    #region//Validaciones Item_Pressed
                    #region *ActionSuccess False

                    if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED & pVal.Action_Success == false) 
                    {
                        //No Permite cerrar los campos de usuarion mientras se crea un documento
                        if (MandatoryUDFWindow.Contains(pVal.FormType) && pVal.ItemUID == "4")
                        {
                            Globals.Error = "No se puede cerrar la ventana de campos de usuario";
                            throw new Exception(Globals.Error);
                        }


                        //ClickActions.Click(oForm, pVal.FormType, pVal.FormTypeEx, pVal.ItemUID, pVal.Action_Success);


                        if (pVal.FormType == 133 && pVal.BeforeAction == true)
                        {

                            FeMain.hola();

                        }


                    


                    }

                   


                        #endregion
                        #endregion

                    }
                catch (Exception ex)
                {
                    BubbleEvent = false;
                    if (ex.Message.IndexOf("Form - Not found  [66000-9]") != -1)
                    {
                        Globals.Error = "SMC: Activar campos de usuario al crear un documento";
                        Globals.SBO_Application.SetStatusBarMessage(Globals.Error, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                    }
                    else
                    {
                        Globals.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                    }
                }
            }
        }

        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                SAPbouiCOM.Form oForm = Globals.SBO_Application.Forms.Item(BusinessObjectInfo.FormUID);
                //Vuelve a revisar número Sunat antes de agregar documento
                if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD & BusinessObjectInfo.BeforeAction == true)
                {
                    if (SalesDocs.Contains(BusinessObjectInfo.FormTypeEx) || PurchsDocs.Contains(BusinessObjectInfo.FormTypeEx))
                    {
                        //CheckSunatAgain.CheckSunatNumber(BusinessObjectInfo.FormTypeEx, BusinessObjectInfo.ObjectKey, oForm);
                    }
              
                }

               
                if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE & BusinessObjectInfo.BeforeAction == true)
                {
                   
                }

                if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD & BusinessObjectInfo.ActionSuccess == true)
                {

                }
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.SetStatusBarMessage(ex.Message);
                BubbleEvent = false;
                return;
            }
        }

        private void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            if (EventType == SAPbouiCOM.BoAppEventTypes.aet_ShutDown)
            {
                System.Windows.Forms.Application.Exit();
            }
            if (EventType == SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged)
            {
                System.Windows.Forms.Application.Exit();
            }
            if (EventType == SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged)
            {
                System.Windows.Forms.Application.Exit();
            }
            if (EventType == SAPbouiCOM.BoAppEventTypes.aet_FontChanged)
            {
                System.Windows.Forms.Application.Exit();
            }
            if (EventType == SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.MenuItem menu = Globals.SBO_Application.Menus.Item("47616");
            if (pVal.MenuUID == "SM_ERP_CP00" && pVal.BeforeAction)
            {
                var vSocios = new AddonConsultaPeru.VIEW.frmSMCSociosN();
            }

            if (pVal.MenuUID == "SM_ERP_ADMIN01" && pVal.BeforeAction)
            {
                new Admin.VIEW.frmControlAdmin();
            }


        }



    }
}
