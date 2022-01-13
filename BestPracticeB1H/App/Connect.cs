using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace ADDONB1SMC
{
    public class Connect
    {
        public static void SetApplication()
        {
            try
            {
                SAPbouiCOM.SboGuiApi SboGuiApi = default(SAPbouiCOM.SboGuiApi);
                string sConnectionString = null;
                SboGuiApi = new SAPbouiCOM.SboGuiApi();
                if (Environment.GetCommandLineArgs().Length > 1)
                {
                    sConnectionString = System.Convert.ToString(Environment.GetCommandLineArgs().GetValue(1));
                }
                else
                {
                    sConnectionString = System.Convert.ToString(Environment.GetCommandLineArgs().GetValue(0));
                }
                SboGuiApi.Connect(sConnectionString);
                Globals.SBO_Application = SboGuiApi.GetApplication();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool ConnectToCompany()
        {
            try
            {
                Globals.oCompany = Globals.SBO_Application.Company.GetDICompany();
                return true;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
                return false;
            }
        }

        public static void SetFilters()
        {
            Globals.oFilters = new SAPbouiCOM.EventFilters();
            #region ITEM_PRESSED
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);

            //Formularios de Ventas
            Globals.oFilter.AddEx("139");//Orden de Venta
            Globals.oFilter.AddEx("140");//Entrega de Mercancía Cliente
            Globals.oFilter.AddEx("180");//Devolución de Mercancía Cliente
            Globals.oFilter.AddEx("65308");//Solicitud Anticipo Cliente
            Globals.oFilter.AddEx("65300");//Factura de Anticipo Cliente
            Globals.oFilter.AddEx("133");//Factura de Cliente
            Globals.oFilter.AddEx("60090");//Factura + Pago
            Globals.oFilter.AddEx("65302");//Factura Exenta cliente
            Globals.oFilter.AddEx("65303");//Nota de Débito de Cliente
            Globals.oFilter.AddEx("65304");//Boleta Cliente
            Globals.oFilter.AddEx("65305");//Boleta Exenta Cliente
            Globals.oFilter.AddEx("65307");//Factura de Exportación Cliente
            Globals.oFilter.AddEx("179");//Nota de Crédito de Cliente
            Globals.oFilter.AddEx("60091");//Factura de Reserva de Cliente
            #endregion

            
            #region FORM_LOAD
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD);
            Globals.oFilter.AddEx("65308");//Solicitud de anticipo de cliente
            Globals.oFilter.AddEx("65300");//Factura de anticipo de cliente
            Globals.oFilter.AddEx("133");//Factura de cliente
            Globals.oFilter.AddEx("60090");//Factura de cliente + pago
            Globals.oFilter.AddEx("65302");//Factura exenta de deudores
            Globals.oFilter.AddEx("65303");//Nota de débito de clientes
            Globals.oFilter.AddEx("65304");//Boleta
            Globals.oFilter.AddEx("65305");//Boleta exenta
            Globals.oFilter.AddEx("65307");//Factura de exportación
            Globals.oFilter.AddEx("179");//Nota de crédito de cliente
            Globals.oFilter.AddEx("60091");//Factura de reserva clientes
            
            //---Campos de Usuario
            
            Globals.oFilter.AddEx("-65300");//Anticipo de Cliente
            Globals.oFilter.AddEx("-133");//Factura de Cliente
            Globals.oFilter.AddEx("-60090");//Factura + Pago
            Globals.oFilter.AddEx("-65303");//Nota de Débito de Cliente
            Globals.oFilter.AddEx("-179");//Nota de Crédito de Cliente
            Globals.oFilter.AddEx("-60091");//Factura de Reserva
            #endregion

            #region FORM_DATA_ADD
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD);
            Globals.oFilter.AddEx("65300");//Factura de Anticipo Clientes
            Globals.oFilter.AddEx("133");//Factura Clientes
            Globals.oFilter.AddEx("60090");//Factura + Pago
            Globals.oFilter.AddEx("65302");//Factura Exenta Clientes
            Globals.oFilter.AddEx("65303");//Nota de Débito de Cliente
            Globals.oFilter.AddEx("65304");//Boleta Clientes
            Globals.oFilter.AddEx("65305");//Boleta Exenta Clientes
            Globals.oFilter.AddEx("65307");//Factura de Exportación Clientes
            Globals.oFilter.AddEx("179");//Nota de Crédito de Cliente
            Globals.oFilter.AddEx("60091");//Factura de Reserva de Cliente

            #endregion
            #region FORM_DATA_UPDATE
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE);
            
            #endregion
            #region FORM_DATA_LOAD
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD);

            
            #endregion

            #region CHOOSE_FROM_LIST
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST);

            
            #endregion
            #region DOUBLE_CLICK
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_DOUBLE_CLICK);

            #endregion
            #region LOST_FOCUS

            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_LOST_FOCUS);
            
            #endregion
            #region FORM_DRAW
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DRAW);

            
            Globals.oFilter.AddEx("65300");//Factura de Anticipo Cliente
            Globals.oFilter.AddEx("133");//Factura Cliente
            Globals.oFilter.AddEx("60090");//Factura + Pago
            Globals.oFilter.AddEx("65302");//Factura Exenta Cliente
            Globals.oFilter.AddEx("65303");//Nota de Débito Cliente
            Globals.oFilter.AddEx("65304");//Boleta Cliente
            Globals.oFilter.AddEx("65305");//Boleta Exenta Cliente
            Globals.oFilter.AddEx("65307");//Factura Exportación Cliente
            Globals.oFilter.AddEx("179");//Nota de Crédito Cliente
            Globals.oFilter.AddEx("60091");//Factura de Reserva Cliente
            
            #endregion
            #region FORM_CLOSE
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_CLOSE);
            
            #endregion
            #region FORM_ACTIVATE
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE);
            
            #endregion
            #region GOT_FOCUS
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_GOT_FOCUS);
            
            #endregion
            #region CLICK
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_CLICK);
            
            #endregion
            #region VALIDATE
            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_VALIDATE);
            
            #endregion

            Globals.oFilter = Globals.oFilters.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK);
            Globals.SBO_Application.SetFilter(Globals.oFilters);

        }

       
    }
}
