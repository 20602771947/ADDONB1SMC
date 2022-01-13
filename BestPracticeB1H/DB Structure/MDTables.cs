using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Linq;

namespace ADDONB1SMC
{
    public class MDTables
    {
        public MDTables()
        { }
        public void CreateTableMD(string sTableID, string TableName, SAPbobsCOM.BoUTBTableType TableType)
        {
            SAPbobsCOM.UserTablesMD oUserTablesMD = default(SAPbobsCOM.UserTablesMD);
            try
            {
                int iVer = 0;
                oUserTablesMD = (SAPbobsCOM.UserTablesMD)Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);

                if (!oUserTablesMD.GetByKey(sTableID))
                {
                    Globals.Release(oUserTablesMD);
                    SAPbobsCOM.UserTablesMD tableCreate = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                    tableCreate.TableName = Strings.Format(sTableID);
                    tableCreate.TableDescription = Strings.Format(TableName);
                    tableCreate.TableType = TableType;

                    int iret = 0;
                    string ErrMsg = "";
                    iret = tableCreate.Add();
                    if (!(iret == 0))
                    {
                        iVer = iVer + 1;
                        Globals.oCompany.GetLastError(out iret, out ErrMsg);
                        //Globals.SBO_Application.MessageBox(ErrMsg);
                        Globals.WriteLogTxt("CREATE TABLE\t" + sTableID + "\t" + TableName + "\t" + ErrMsg, Globals.LogFile);
                    }
                    else
                    {
                        Globals.SBO_Application.StatusBar.SetText("Tabla " + sTableID + " creada con éxito", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                    Globals.Release(tableCreate);
                }
                else
                {
                    #region Es igual?
                    string[] Incoming = new string[] { sTableID, TableName };
                    string[] InDatabase = new string[] { oUserTablesMD.TableName, oUserTablesMD.TableDescription };
                    bool isEqual = Enumerable.SequenceEqual(Incoming, InDatabase);
                    #endregion
                    Globals.Release(oUserTablesMD);
                    if (isEqual) return;
                    SAPbobsCOM.UserTablesMD tableUpdate = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                    tableUpdate.GetByKey(sTableID);
                    tableUpdate.TableName = Strings.Format(sTableID);
                    tableUpdate.TableDescription = Strings.Format(TableName);
                    int iret = 0;
                    string ErrMsg = "";
                    iret = tableUpdate.Update();
                    if (!(iret == 0))
                    {
                        iVer = iVer + 1;
                        Globals.oCompany.GetLastError(out iret, out ErrMsg);
                        //Globals.SBO_Application.MessageBox(ErrMsg);
                        Globals.WriteLogTxt("UPDATE TABLE\t" + sTableID + "\t" + TableName + "\t" + ErrMsg, Globals.LogFile);
                    }
                    else
                    {
                        Globals.SBO_Application.StatusBar.SetText("Tabla " + sTableID + " actualizada con éxito", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                    Globals.Release(oUserTablesMD);
                }
                Globals.Release(oUserTablesMD);
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
            }
        }

        public void DeleteTableMD(string sTableID)
        {
            try
            {
                int iret = 0;
                string ErrMsg = "";
                SAPbobsCOM.UserTablesMD oUserTablesMD = default(SAPbobsCOM.UserTablesMD);
                oUserTablesMD = (SAPbobsCOM.UserTablesMD)Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                if (oUserTablesMD.GetByKey(sTableID))
                {
                    iret = oUserTablesMD.Remove();
                    if (iret != 0)
                    {
                        Globals.oCompany.GetLastError(out iret, out ErrMsg);
                        //Globals.SBO_Application.MessageBox(ErrMsg);
                        Globals.WriteLogTxt("DELETE TABLE\t" + sTableID + "\t" + ErrMsg, Globals.LogFile);
                    }
                    else
                    {
                        Globals.SBO_Application.StatusBar.SetText("Tabla " + sTableID + " Eliminada con éxito", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                }
                Globals.Release(oUserTablesMD);

            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
    }
}
