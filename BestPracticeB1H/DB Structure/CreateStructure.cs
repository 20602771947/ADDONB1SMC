using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ADDONB1SMC.DB_Structure;


namespace ADDONB1SMC.DB_Structure
{
    class CreateStructure
    {
        public static void CreateStruct()
        {
            
          
                bool isHana = Globals.IsHana();
                Globals.LogFile = "SMC_Log_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                
                MDTables oMDTables = new MDTables();
                MDFields oMDFields = new MDFields();

                #region Facturacion Electronica

                oMDFields.CreateRegularField("OINV", "SMC_EstadoFE", "FE Estado CPE SUNAT", SAPbobsCOM.BoFieldTypes.db_Alpha,
                  SAPbobsCOM.BoFldSubTypes.st_None, 20, SAPbobsCOM.BoYesNoEnum.tNO, null, null, null, null, null);

                oMDFields.CreateRegularField("OINV", "SMC_SUNAT_FIRMA", "FE FIRMA CPE SUNAT", SAPbobsCOM.BoFieldTypes.db_Memo,
                    SAPbobsCOM.BoFldSubTypes.st_None, 0, SAPbobsCOM.BoYesNoEnum.tNO, null, null, null, null, null);

                oMDFields.CreateRegularField("OINV", "SMC_SUNAT_HASH", "FE HASH CPE SUNAT", SAPbobsCOM.BoFieldTypes.db_Memo,
                    SAPbobsCOM.BoFldSubTypes.st_None, 0, SAPbobsCOM.BoYesNoEnum.tNO, null, null, null, null, null);

                oMDFields.CreateRegularField("OINV", "SMC_TipoOper21", "FE TIPO OPERACION CPE SUNAT", SAPbobsCOM.BoFieldTypes.db_Alpha,
                 SAPbobsCOM.BoFldSubTypes.st_None, 2, SAPbobsCOM.BoYesNoEnum.tNO, null, null, new string[] { "01", "02","03","04","05" },
                 new string[] { "Venta Interna", "Exportacion", "No Domiciliado", "Anticipo - Venta Interna", "Venta Itinerante" }, "01");

                #endregion                
            
                               
               
          
            
            
            
        }
    }
}