using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ADDONB1SMC.NewItems
{
    public static class ERPMenu
    {
        public static void AddMenuItems()
        {
            SAPbouiCOM.Menus oMenus = default(SAPbouiCOM.Menus);
            SAPbouiCOM.MenuItem oMenuItem = default(SAPbouiCOM.MenuItem);
            oMenus = Globals.SBO_Application.Menus;
            SAPbouiCOM.MenuCreationParams oCreationPackage = default(SAPbouiCOM.MenuCreationParams);
            oCreationPackage = Globals.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
            

            try
            {
                if (Globals.SuperUser)
                {

                }

                oMenuItem = Globals.SBO_Application.Menus.Item("43520");
                oMenus = oMenuItem.SubMenus;
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
                oCreationPackage.UniqueID = "AddonSMC";
                oCreationPackage.String = "Addons SmartCode";
                oCreationPackage.Position = oMenuItem.SubMenus.Count + 1;
                string sPath = System.Windows.Forms.Application.StartupPath;
                if (Globals.IsHana() == false)
                    oCreationPackage.Image = sPath + "\\SMC.png";
                else
                    oCreationPackage.Image = sPath + "\\SMC.png";
                oMenus.AddEx(oCreationPackage);





                #region//SubMenus (Aquí se agregan los submenus)

                

                if (Globals.SuperUser)
                {
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
                    oCreationPackage.UniqueID = "SM_ERP_ADMIN";
                    oCreationPackage.String = "Gestion";
                    oMenus.AddEx(oCreationPackage);
                }

                if (Globals.FE == "Y")
                {
                    oMenuItem = Globals.SBO_Application.Menus.Item("AddonSMC");
                    oMenus = oMenuItem.SubMenus;

                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
                    oCreationPackage.UniqueID = "SM_ERP_FE";
                    oCreationPackage.String = "CPE Facturación Electrónica";
                    oMenus.AddEx(oCreationPackage);

                }

                #endregion


                #region Menu FE
                if (Globals.FE == "Y")
                {
                    oMenuItem = Globals.SBO_Application.Menus.Item("SM_ERP_FE");
                    oMenus = oMenuItem.SubMenus;

                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "SM_ERP_FE00";
                    oCreationPackage.String = "Estado CPE";
                    oMenus.AddEx(oCreationPackage);

                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "SM_ERP_FE01";
                    oCreationPackage.String = "Envio Masivo de CPE";
                    oMenus.AddEx(oCreationPackage);
                }
                #endregion



                if (Globals.ConsultaPeru == "Y")
                {
                    oMenuItem = Globals.SBO_Application.Menus.Item("AddonSMC");
                    oMenus = oMenuItem.SubMenus;

                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
                    oCreationPackage.UniqueID = "SM_ERP_CP";
                    oCreationPackage.String = "Consulta Peru";
                    oMenus.AddEx(oCreationPackage);

                }




                #region Menu ConsultaPeru
                if (Globals.ConsultaPeru == "Y")
                {
                  
                    oMenuItem = Globals.SBO_Application.Menus.Item("SM_ERP_CP");
                    oMenus = oMenuItem.SubMenus;

                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "SM_ERP_CP00";
                    oCreationPackage.String = "Consulta Masiva";
                    oMenus.AddEx(oCreationPackage);



                }
                #endregion

            }



            catch (Exception ex)
            {
                Globals.SBO_Application.SetStatusBarMessage(ex.Message.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, false);
            }
        }

        private static void BtnMasivo_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            Globals.SBO_Application.MessageBox("hola");

        }
    }
}
