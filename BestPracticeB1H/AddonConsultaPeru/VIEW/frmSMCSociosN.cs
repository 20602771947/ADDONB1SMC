using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ADDONB1SMC.AddonConsultaPeru.VIEW
{
    public class frmSMCSociosN
    {
        SAPbouiCOM.Form oForm = null;
        SAPbouiCOM.ComboBox cboEstado;
        SAPbouiCOM.ComboBox cboCondi;
        SAPbouiCOM.Grid grdLista;
        SAPbouiCOM.Button btnBuscar;
        public frmSMCSociosN()
        {
            init();
        }

        public void init()
        {
            try
            {
                Globals.SBO_Application.Forms.Item("frmSMCSociosN").Select();
            }
            catch (Exception)
            {
                LoadFormListarSocio();
                InitForm();
                grdLista.AutoResizeColumns();
                
               
            }
        }

        private void LoadFormListarSocio()
        {
            string formType = "";
            var folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormMasivo.srf");
            var xml = System.IO.File.ReadAllText(folder);
            
            SAPbouiCOM.FormCreationParams oParams = null;

            oParams = (SAPbouiCOM.FormCreationParams)Globals.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
            oParams.UniqueID = "frmSMCSociosN";

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
        }

        private void InitForm()
        {
         

            cboEstado = (SAPbouiCOM.ComboBox)oForm.Items.Item("cboEstado").Specific;
            cboCondi = (SAPbouiCOM.ComboBox)oForm.Items.Item("cboCondi").Specific;
            grdLista = (SAPbouiCOM.Grid)oForm.Items.Item("grdLista").Specific;
            btnBuscar = (SAPbouiCOM.Button)oForm.Items.Item("btnBuscar").Specific;
            
            btnBuscar.ClickAfter += BtnBuscar_ClickAfter; 

            //grdLista.Columns.Item("Col_Codigo").DataBind.Bind("DT_0", "Col_Codigo");
            //grdLista.Columns.Item("Col_RazonSocial").DataBind.Bind("DT_0", "Col_RazonSocial");
            //grdLista.Columns.Item("Col_Tipo").DataBind.Bind("DT_0", "Col_Tipo");
            //grdLista.Columns.Item("Col_Estado").DataBind.Bind("DT_0", "Col_Estado");
            //grdLista.Columns.Item("Col_Condicion").DataBind.Bind("DT_0", "Col_Condicion");


            cboEstado.ValidValues.Add("0", "TODOS");
            cboEstado.ValidValues.Add("1","ACTIVO");
            cboEstado.ValidValues.Add("2", "NO ACTIVO");

            cboEstado.ExpandType = SAPbouiCOM.BoExpandType.et_DescriptionOnly;
            cboEstado.Item.DisplayDesc = true;
            cboEstado.Select("0", SAPbouiCOM.BoSearchKey.psk_ByValue);

            cboCondi.ValidValues.Add("0", "TODOS");
            cboCondi.ValidValues.Add("1", "HABIDO");
            cboCondi.ValidValues.Add("2", "NO HABIDO");

            cboCondi.ExpandType = SAPbouiCOM.BoExpandType.et_DescriptionOnly;
            cboCondi.Item.DisplayDesc = true;
            cboCondi.Select("0", SAPbouiCOM.BoSearchKey.psk_ByValue);

        }

        private void BtnBuscar_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
          

            DAO.DAOSociosNegocio SocioListado = new DAO.DAOSociosNegocio();
            var lbeDTOSociosNegocio = SocioListado.Listar(cboEstado.Selected.Description, cboCondi.Selected.Description);

            SAPbouiCOM.DataTable dataTable;
            dataTable = oForm.DataSources.DataTables.Item("DT_0");

           

            dataTable.Rows.Clear();

            oForm.Freeze(true);

            for (int i = 0; i < lbeDTOSociosNegocio.Count; i++)
            {
                dataTable.Rows.Add();
                dataTable.SetValue("Col_Codigo", i, lbeDTOSociosNegocio[i].Codigo);
                dataTable.SetValue("Col_RazonSocial", i, lbeDTOSociosNegocio[i].RazonSocial);
                dataTable.SetValue("Col_Tipo", i, lbeDTOSociosNegocio[i].Tipo);
                dataTable.SetValue("Col_Estado", i, lbeDTOSociosNegocio[i].Estado);
                dataTable.SetValue("Col_Condicion", i, lbeDTOSociosNegocio[i].Condicion);
            }

            oForm.Freeze(false);

            grdLista.AutoResizeColumns();
           

        }
    }

   
}
