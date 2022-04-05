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
        SAPbouiCOM.Button btnUpdate;
        List<DTO.DTOSociosNegocio> lbeDTOSociosNegocio;

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
            btnUpdate = (SAPbouiCOM.Button)oForm.Items.Item("btnUpdate").Specific;

            btnBuscar.ClickAfter += BtnBuscar_ClickAfter;
            btnUpdate.ClickAfter += BtnUpdate_ClickAfter;
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

        private void BtnUpdate_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            var oCompany = (SAPbobsCOM.Company)Globals.SBO_Application.Company.GetDICompany();
            SAPbobsCOM.BusinessPartners odoc = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

            WSMC.SRVB1SMCSoapClient Servicio = new WSMC.SRVB1SMCSoapClient();

            if (grdLista.DataTable.Rows.Count > 0)
            {
                int valor = Globals.SBO_Application.MessageBox("¿Desea Actualizar la lista de Socios?", 1,"OK" ,"Cancel" );

                if (valor == 1)
                {
                    //DAO.DAOSociosNegocio SocioListado = new DAO.DAOSociosNegocio();
                    //var lbeDTOSociosNegocio = SocioListado.Listar(cboEstado.Selected.Description, cboCondi.Selected.Description);
                    var contador = 0;


                   


                    for (int i = 0; i < lbeDTOSociosNegocio.Count; i++)
                    {

                        WSMC.SocioDeNegocioDTO result = Servicio.GetSocioDeNegocioSunat(lbeDTOSociosNegocio[i].Codigo);

                        odoc.GetByKey(lbeDTOSociosNegocio[i].CardCode);

                        //if (lbeDTOSociosNegocio[i].Codigo == result.Dni)
                        //{
                        //    odoc.UserFields.Fields.Item("U_SMC_Condicion").Value = (result.Condicion == null) ? "" : result.Condicion;
                        //    odoc.UserFields.Fields.Item("U_SMC_Estado").Value = (result.Estado == null) ? "" : result.Estado;
                        //}
                        if (lbeDTOSociosNegocio[i].Codigo == result.Ruc)
                        {
                            odoc.UserFields.Fields.Item("U_SMC_Condicion").Value = (result.Condicion == null) ? "" : result.Condicion;
                            odoc.UserFields.Fields.Item("U_SMC_Estado").Value = (result.Estado == null) ? "" : result.Estado;
                            //odoc.UserFields.Fields.Item("U_SMC_Ubigeo").Value = (result.Ubigeo == null) ? "" : result.Ubigeo;
                        }

                        int res = odoc.Update();

                        contador++;

                    }



                    if (contador == lbeDTOSociosNegocio.Count)
                    {
                        Globals.SBO_Application.MessageBox("Carga Completada");
                        return;
                    }



                }
                else
                {
                    return;
                }
                
                
            }
            else
            {
                Globals.SBO_Application.MessageBox("No se encontraron datos");
                return;
            }


        }
        
        private void BtnBuscar_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
          
            DAO.DAOSociosNegocio SocioListado = new DAO.DAOSociosNegocio();
            lbeDTOSociosNegocio = SocioListado.Listar(cboEstado.Selected.Description, cboCondi.Selected.Description);

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
