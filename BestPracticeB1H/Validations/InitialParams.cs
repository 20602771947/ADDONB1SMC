using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ADDONB1SMC.Validations
{
    class InitialParams
    {
        //public static bool Hana = Globals.IsHana();
        public static string Country;
        public static string RUC;
      

        public static void Get()
        {   
            Globals.Query = "SELECT \"Country\", \"TaxIdNum\" FROM OADM";
            Globals.RunQuery(Globals.Query);
            Country = Globals.oRec.Fields.Item(0).Value;
            RUC = Globals.oRec.Fields.Item(1).Value ?? "";
            Globals.Release(Globals.oRec);
        }

       

        public static DataTable ConvertRecordSet(SAPbobsCOM.Recordset SAPRecordset)
        {
            DataTable functionReturnValue = null;

            //\ This function will take an SAP recordset from the SAPbobsCOM library and convert it to a more
            //\ easily used ADO.NET datatable which can be used for data binding much easier.

            DataTable dtTable = new DataTable();
            DataColumn NewCol = null;
            DataRow NewRow = null;
            int ColCount = 0;

            try
            {
                for (ColCount = 0; ColCount <= SAPRecordset.Fields.Count - 1; ColCount++)
                {
                    NewCol = new DataColumn(SAPRecordset.Fields.Item(ColCount).Name);
                    dtTable.Columns.Add(NewCol);
                }
                while (!(SAPRecordset.EoF))
                {
                    NewRow = dtTable.NewRow();
                    //populate each column in the row we're creating

                    for (ColCount = 0; ColCount <= SAPRecordset.Fields.Count - 1; ColCount++)
                    {
                        NewRow[SAPRecordset.Fields.Item(ColCount).Name] = SAPRecordset.Fields.Item(ColCount).Value;

                    }

                    //Add the row to the datatable
                    dtTable.Rows.Add(NewRow);
                    SAPRecordset.MoveNext();
                }
                return dtTable;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.SetStatusBarMessage(ex.Message);
                return functionReturnValue;
            }
        }
    }
}
