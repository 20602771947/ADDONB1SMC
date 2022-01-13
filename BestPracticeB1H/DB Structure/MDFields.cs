using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Linq;


namespace ADDONB1SMC
{
    public class MDFields
    {
        public static SAPbobsCOM.UserFieldsMD oUserFieldsMD;
        bool exists = false;
        public MDFields() { }

        public void CreateRegularField(string sTableID, string sAliasID, string Descr, SAPbobsCOM.BoFieldTypes Type,
            SAPbobsCOM.BoFldSubTypes SubType, int Size,
            SAPbobsCOM.BoYesNoEnum Mandatory, string LinkedTo, string LinkedStuff,
            string[] validValues, string[] validDescription, string DefaultValue)
        {
            try
            {
                Globals.camposentotal++;

                int iret = -1;
                string ErrMsg = null;
                exists = FieldExists(sTableID, sAliasID);
                if (exists == false)
                {
                    #region Create Field
                    oUserFieldsMD = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                    oUserFieldsMD.TableName = sTableID;
                    oUserFieldsMD.Name = sAliasID;
                    oUserFieldsMD.Description = Descr;
                    oUserFieldsMD.Type = Type;
                    oUserFieldsMD.Mandatory = Mandatory;
                    if (SubType != SAPbobsCOM.BoFldSubTypes.st_None) oUserFieldsMD.SubType = SubType;
                    if (Size != 0) oUserFieldsMD.EditSize = Size;
                    if (LinkedTo == "Table") oUserFieldsMD.LinkedTable = LinkedStuff;
                    if (LinkedTo == "UDO") oUserFieldsMD.LinkedUDO = LinkedStuff;
                    #region valid Values
                    if ((validValues != null))
                    {
                        for (int i = 0; i <= validValues.Length - 1; i++)
                        {
                            if (validDescription == null)
                                oUserFieldsMD.ValidValues.Description = validValues[i];
                            else
                                oUserFieldsMD.ValidValues.Description = validDescription[i];
                            oUserFieldsMD.ValidValues.Value = validValues[i];
                            oUserFieldsMD.ValidValues.Add();
                        }
                    }
                    #endregion
                    if (DefaultValue != null) oUserFieldsMD.DefaultValue = DefaultValue;
                    iret = oUserFieldsMD.Add();
                    if (iret != 0)
                    {
                        Globals.oCompany.GetLastError(out iret, out ErrMsg);
                        //Globals.SBO_Application.MessageBox(sTableID + "." + sAliasID+ " "+ErrMsg);
                        Globals.WriteLogTxt("CREATE FIELD\t" + sAliasID + "\t" + Descr + "\t" + ErrMsg, Globals.LogFile);
                    }
                    else
                    {
                        Globals.SBO_Application.StatusBar.SetText("Campo " + sAliasID + " " + Descr + ": Creado con éxito", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                    Globals.Release(oUserFieldsMD);
                    #endregion
                }
                else
                {
                    #region Update Field
                    #region es igual?
                    int NewSize = Size;
                    bool isEqual = true;
                    bool isEqualValids = true;
                    string NewLinkedStuff = LinkedStuff;
                    string NewDefaultValue = DefaultValue;
                    if (LinkedStuff == null) NewLinkedStuff = "";
                    if (DefaultValue == null) NewDefaultValue = "";

                    if (Type != SAPbobsCOM.BoFieldTypes.db_Alpha)
                    {
                        Globals.Query = "select	\"TableID\",\"AliasID\",\"Descr\",\"EditSize\",case when \"NotNull\" = 'Y' then 'tYES' else 'tNO'end as  NotNull, " +
                                    "case	when coalesce(\"RTable\",'NO') =  'NO' and coalesce(\"RelUDO\",'NO') =  'NO' then null " +
                                    "		when coalesce(\"RTable\",'NO') <> 'NO' and coalesce(\"RelUDO\",'NO') =  'NO' then \"RTable\" " +
                                    "		when coalesce(\"RTable\",'NO') =  'NO' and coalesce(\"RelUDO\",'NO') <> 'NO' then \"RelUDO\" " +
                                    "		end as relStuff,\"Dflt\" " +
                                    "from	\"CUFD\" " +
                                    "where	\"TableID\" = '" + sTableID + "' and \"AliasID\" = '" + sAliasID + "'";
                        Globals.RunQuery(Globals.Query);
                        NewSize = Globals.oRec.Fields.Item(3).Value;
                        Globals.Release(Globals.oRec);
                    }
                    if (LinkedTo != null)
                    {
                        Globals.Query = "select	\"TableID\",\"AliasID\",\"Descr\",\"EditSize\",case when \"NotNull\" = 'Y' then 'tYES' else 'tNO'end as  NotNull, " +
                                    "case	when coalesce(\"RTable\",'NO') =  'NO' and coalesce(\"RelUDO\",'NO') =  'NO' then null " +
                                    "		when coalesce(\"RTable\",'NO') <> 'NO' and coalesce(\"RelUDO\",'NO') =  'NO' then \"RTable\" " +
                                    "		when coalesce(\"RTable\",'NO') =  'NO' and coalesce(\"RelUDO\",'NO') <> 'NO' then \"RelUDO\" " +
                                    "		end as relStuff,\"Dflt\" " +
                                    "from	\"CUFD\" " +
                                    "where	\"TableID\" = '" + sTableID + "' and \"AliasID\" = '" + sAliasID + "'";
                        Globals.RunQuery(Globals.Query);
                        NewSize = Globals.oRec.Fields.Item(3).Value;
                        Globals.Release(Globals.oRec);
                    }
                    string[] Incoming = new string[] { sTableID, sAliasID, Descr, NewSize.ToString(), Mandatory.ToString(), NewLinkedStuff, NewDefaultValue };
                    Globals.Query = "select	\"TableID\",\"AliasID\",\"Descr\",\"EditSize\",case when \"NotNull\" = 'Y' then 'tYES' else 'tNO'end as  NotNull, " +
                                    "case	when coalesce(\"RTable\",'NO') =  'NO' and coalesce(\"RelUDO\",'NO') =  'NO' then null " +
                                    "		when coalesce(\"RTable\",'NO') <> 'NO' and coalesce(\"RelUDO\",'NO') =  'NO' then \"RTable\" " +
                                    "		when coalesce(\"RTable\",'NO') =  'NO' and coalesce(\"RelUDO\",'NO') <> 'NO' then \"RelUDO\" " +
                                    "		end as relStuff,\"Dflt\" " +
                                    "from	\"CUFD\" " +
                                    "where	\"TableID\" = '" + sTableID + "' and \"AliasID\" = '" + sAliasID + "'";
                    Globals.RunQuery(Globals.Query);
                    string[] InDatabase = new string[] { Globals.oRec.Fields.Item(0).Value.ToString(), Globals.oRec.Fields.Item(1).Value.ToString(), Globals.oRec.Fields.Item(2).Value.ToString(), Globals.oRec.Fields.Item(3).Value.ToString(), Globals.oRec.Fields.Item(4).Value.ToString(), Globals.oRec.Fields.Item(5).Value.ToString(), Globals.oRec.Fields.Item(6).Value.ToString() };
                    Globals.Release(Globals.oRec);
                    isEqual = Enumerable.SequenceEqual(Incoming, InDatabase);
                    if (validValues != null)
                    {
                        #region valid values
                        if (!Globals.IsHana())
                            #region valid values SQL
                            Globals.Query = "Select distinct A.TableID,C.AliasID,C.Descr, " +
                                            "substring(" +
                                            "(" +
                                            "Select ','+B.FldValue+'-'+B.Descr  AS [text()] " +
                                            "From dbo.UFD1 B " +
                                            "Where B.FieldID = A.FieldID and B.TableID = A.TableID " +
                                            "ORDER BY B.FldValue asc " +
                                            "For XML PATH ('') " +
                                            "), 2, 10000) [codes] " +
                                            "From	dbo.UFD1 A " +
                                            "inner join CUFD C on A.TableID = C.TableID and C.FieldID = A.FieldID " +
                                            "where	A.TableID not like '@A%' and A.TableID not like 'A%' and C.AliasID = '" + sAliasID + "' and A.TableID = '" + sTableID + "' " +
                                            "order by 1,2,3 asc ";
                            #endregion
                        else
                            #region valid values HANA
                            Globals.Query = "SELECT A.\"TableID\",B.\"AliasID\",B.\"Descr\", STRING_AGG(A.\"FldValue\"||'-'||A.\"Descr\", ',' ORDER BY A.\"FldValue\" asc)  AS \"Codes\" " +
                                            "FROM UFD1 A inner join CUFD B on A.\"TableID\" = B.\"TableID\" and A.\"FieldID\" = B.\"FieldID\" " +
                                            "WHERE A.\"TableID\" not like '@A%' and A.\"TableID\" not like 'A%' and B.\"AliasID\" = '" + sAliasID + "' and A.\"TableID\" = '" + sTableID + "' " +
                                            "GROUP BY A.\"TableID\",B.\"AliasID\",B.\"Descr\"";
                            #endregion
                        //string[] InDB = new string[]
                        Globals.RunQuery(Globals.Query);
                        string[] InDatabaseValids = Globals.oRec.Fields.Item(3).Value.Split(',');
                        Globals.Release(Globals.oRec);
                        string[] IncomingValids = new string[validValues.Count()];
                        for (int i = 0; i < validValues.Count(); i++)
                        {
                            string Concat = validValues[i].ToString() + "-" + validDescription[i].ToString();
                            IncomingValids[i] = Concat;
                        }
                        var sorted = IncomingValids.OrderBy(item => item).ToArray();
                        Array.Sort(IncomingValids);
                        isEqualValids = Enumerable.SequenceEqual(IncomingValids, InDatabaseValids);
                        //if (isEqual)
                        //{
                        //    Globals.cuantosrepetidos++;
                        //    return;
                        //}
                        //else
                        //{
                        //    Globals.SBO_Application.MessageBox(sTableID + "." + sAliasID + "\n" + string.Join(",", IncomingValids.OrderBy(item => item).ToArray()) + "\n" + string.Join(",", InDatabaseValids.OrderBy(item => item).ToArray()));
                        //}
                        #endregion
                    }
                    if (isEqual & isEqualValids)
                    {
                        Globals.SBO_Application.StatusBar.SetText("Campo " + sTableID + "." + sAliasID + " " + Descr + ": Sin cambios", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                        Globals.cuantosrepetidos++;
                        return;
                    }
                    #endregion

                    oUserFieldsMD = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                    int FieldId = GetFieldID(sTableID, sAliasID);
                    oUserFieldsMD.GetByKey(sTableID, FieldId);

                    int NumValidVal = oUserFieldsMD.ValidValues.Count;
                    if (NumValidVal > 0 & LinkedTo != null)
                    {
                        int i = NumValidVal - 1;
                        while (NumValidVal > 0)
                        {
                            oUserFieldsMD.ValidValues.SetCurrentLine(i);
                            oUserFieldsMD.ValidValues.Delete();
                            NumValidVal--;
                            i--;
                        }
                    }

                    oUserFieldsMD.Description = Descr;
                    oUserFieldsMD.Mandatory = Mandatory;
                    //Globals.SBO_Application.MessageBox(Size.ToString());
                    if (Size != 0) oUserFieldsMD.EditSize = Size;
                    if (LinkedTo == "Table") oUserFieldsMD.LinkedTable = LinkedStuff;
                    if (LinkedTo == "UDO") oUserFieldsMD.LinkedUDO = LinkedStuff;
                    #region valid Values
                    if ((validValues != null))
                    {
                        var FieldValues = new Dictionary<string, string>();
                        for (int i = 0; i < validValues.Count(); i++)
                        {
                            FieldValues.Add(validValues[i], validDescription[i]);
                        }

                        for (int i = 0; i < NumValidVal; i++)
                        {
                            oUserFieldsMD.ValidValues.SetCurrentLine(i);
                            if (!FieldValues.ContainsKey(oUserFieldsMD.ValidValues.Value))
                            {
                                FieldValues.Add(oUserFieldsMD.ValidValues.Value, oUserFieldsMD.ValidValues.Description);
                            }
                        }

                        int j = NumValidVal - 1;
                        while (NumValidVal > 0)
                        {
                            oUserFieldsMD.ValidValues.SetCurrentLine(j);
                            oUserFieldsMD.ValidValues.Delete();
                            NumValidVal--;
                            j--;
                        }

                        var list = FieldValues.Keys.ToList();
                        list.Sort();

                        foreach (var key in list)
                        {
                            oUserFieldsMD.ValidValues.Value = key;
                            oUserFieldsMD.ValidValues.Description = FieldValues[key];
                            oUserFieldsMD.ValidValues.Add();
                        }
                    }
                    #endregion
                    oUserFieldsMD.DefaultValue = DefaultValue;
                    iret = oUserFieldsMD.Update();
                    if (iret != 0)
                    {
                        if (iret != -1029)
                        {
                            Globals.oCompany.GetLastError(out iret, out ErrMsg);
                            //Globals.SBO_Application.MessageBox(sTableID + "." + sAliasID + ErrMsg);
                            Globals.WriteLogTxt("UPDATE FIELD\t" + sAliasID + "\t" + Descr + "\t" + ErrMsg, Globals.LogFile);
                        }
                        else
                            Globals.SBO_Application.StatusBar.SetText("Campo " + sTableID + "." + sAliasID + " " + Descr + ErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                    else
                    {
                        Globals.SBO_Application.StatusBar.SetText("Campo " + sTableID + "." + sAliasID + " " + Descr + ": Actualizado con éxito", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                    Globals.Release(oUserFieldsMD);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
            }
        }

        public void FixField(string sTableID, string sAliasID, string Descr, SAPbobsCOM.BoFieldTypes Type,
            SAPbobsCOM.BoFldSubTypes SubType, int Size,
            SAPbobsCOM.BoYesNoEnum Mandatory, string LinkedTo, string LinkedStuff,
            string[] validValues, string[] validDescription, string DefaultValue, string sAliasIDTemp)
        {
            try
            {
                #region Create temp
                int iret = -1;
                string ErrMsg = null;
                oUserFieldsMD = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                oUserFieldsMD.TableName = sTableID;
                oUserFieldsMD.Name = sAliasIDTemp;
                oUserFieldsMD.Description = Descr;
                oUserFieldsMD.Type = Type;
                oUserFieldsMD.Mandatory = Mandatory;
                if (SubType != SAPbobsCOM.BoFldSubTypes.st_None) oUserFieldsMD.SubType = SubType;
                if (Size != 0) oUserFieldsMD.EditSize = Size;
                iret = oUserFieldsMD.Add();
                if (iret != 0)
                {
                    Globals.oCompany.GetLastError(out iret, out ErrMsg);
                    Globals.WriteLogTxt("CREATE FIELD\t" + sAliasID + "\t" + Descr + "\t" + ErrMsg, Globals.LogFile);
                }
                else
                {
                    Globals.SBO_Application.StatusBar.SetText("Campo " + sAliasID + " " + Descr + ": Creado con éxito", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                }
                Globals.Release(oUserFieldsMD);
                #endregion
                #region Update from old to temp
                Globals.Query = "";
                #endregion
                #region Delete old
                #endregion
                #region create new
                #endregion
                #region update from temp to new
                #endregion
                #region delete temp
                #endregion
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
            }
        }

        public static bool FieldExists(string sTableID, string sAliasID)
        {
            try
            {
                int FieldId = GetFieldID(sTableID, sAliasID);
                oUserFieldsMD = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                if (oUserFieldsMD.GetByKey(sTableID, FieldId) == true)
                {
                    Globals.Release(oUserFieldsMD);
                    return true;
                }
                else
                {
                    Globals.Release(oUserFieldsMD);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
                return false;
            }
        }
        private static int GetFieldID(string sTableID, string sAliasID)
        {
            int iRetVal = -1;
            try
            {
                Globals.Query = ("select \"FieldID\" from CUFD where \"TableID\" = '" + sTableID + "' and \"AliasID\" = '" + sAliasID + "'");
                Globals.RunQuery(Globals.Query);
                if (!Globals.oRec.EoF) iRetVal = Convert.ToInt32(Globals.oRec.Fields.Item("FieldID").Value.ToString());
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
            }
            finally
            {
                Globals.Release(Globals.oRec);
            }
            return iRetVal;
        }
        public void DeleteUserField(string table, string aliasID)
        {
            oUserFieldsMD = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
            int iFieldID = GetFieldID(table, aliasID);
            try
            {
                if (oUserFieldsMD.GetByKey(table, iFieldID))
                {
                    int wasRemoved = oUserFieldsMD.Remove();
                    if (wasRemoved != 0)
                    {
                        Globals.oCompany.GetLastError(out wasRemoved, out Globals.Error);
                        //Globals.SBO_Application.MessageBox(Globals.Error);
                        Globals.WriteLogTxt("DELETE FIELD\t" + aliasID + Globals.Error, Globals.LogFile);
                    }
                }
                else
                {
                    Globals.SBO_Application.SetStatusBarMessage("No existe campo '" + aliasID + "' en tabla '" + table + "'. No se pudo eliminar.", SAPbouiCOM.BoMessageTime.bmt_Short, false);
                }
                Globals.Release(oUserFieldsMD);
            }
            catch (Exception ex)
            {
                ex.ToString();
                Globals.SBO_Application.SetStatusBarMessage("Error al eliminar campo", SAPbouiCOM.BoMessageTime.bmt_Short, false);
            }
        }
    }
}