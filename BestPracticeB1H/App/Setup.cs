using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.CSharp;

namespace ADDONB1SMC
{
    public class Setup
    {
        public Setup() { }
        public bool validarVersion(string addOnName, String addOnVersion)
        {
            bool retorno = false;
            try
            {
                //1. Si LA TABLA no existe la creo
                if (!checkCampoBD("@SMC_SETUP", "U_SYP_VERS"))
                {
                    creaTablaMD("SMC_SETUP", "Setup_de_AddOns_SMARTCODE", SAPbobsCOM.BoUTBTableType.bott_NoObject);
                    creaCampoMD("SMC_SETUP", "SMC_ADDN", "Nombre del AddOn", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100);
                    creaCampoMD("SMC_SETUP", "SMC_VERS", "Version del AddOn", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100);
                    creaCampoMD("SMC_SETUP", "SMC_RUTA", "Ruta auxiliar para AddOn", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 254);
                    //confirmarVersion(addOnName, addOnVersion);
                }
                else
                {
                    //2. Valido que los datos de add-on y versión coincidan
                    if (Globals.IsHana() == false)
                        Globals.Query = "SELECT ISNULL(\"U_SYP_VERS\",0)U_SYP_VERS FROM \"@SMC_SETUP\" WHERE U_SMC_ADDN = '" + addOnName + "' ORDER BY U_SMC_VERS DESC";
                    if (Globals.IsHana() == true)
                        Globals.Query = "SELECT IFNULL(U_SYP_VERS,'0') AS U_SYP_VERS FROM \"@SMC_SETUP\" WHERE U_SMC_ADDN = '" + addOnName + "' ORDER BY U_SMC_VERS DESC";
                    Globals.RunQuery(Globals.Query);
                    if (Globals.oRec.EoF)
                    {
                        addOnName = "SMARTCODE";
                        Globals.SBO_Application.MessageBox("Se creará la estructura de datos para el Add-On " + addOnName);
                        //confirmarVersion(addOnName, addOnVersion);
                    }
                    else
                    {
                        string valorversion1 = Globals.oRec.Fields.Item("U_SMC_VERS").Value.ToString();
                        Globals.oldversion = valorversion1;
                        string valorversion2 = addOnVersion.ToString();
                        int version = CompareVersion(valorversion1, valorversion2);

                        if (version == 1)
                        {
                            Globals.SBO_Application.MessageBox("Se actualizará la estructura de datos para el Add-On " + addOnName + " de versión " + Globals.oRec.Fields.Item("U_SYP_VERS").Value.ToString() + " a " + addOnVersion);
                        }
                        else if (version == 2)
                        {
                            Globals.SBO_Application.MessageBox("Se detectó una versión del Add-On " + addOnName + " más avanzada (" + Globals.oRec.Fields.Item("U_SYP_VERS").Value.ToString() + ") instalada previamente. No se recomienda el uso de la versión que está intentando ejecutar (" + addOnVersion + ")");
                            retorno = true;
                        }
                        else if (version == 0)
                        {
                            retorno = true;
                        }
                    }
                    Globals.Release(Globals.oRec);
                }
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
            }
            return retorno;
        }
        
       
        public void cerrarInstancias(System.Diagnostics.Process procesoActual, bool autoKill = false)
        {
            try
            {
                System.Diagnostics.Process[] procesos = Process.GetProcessesByName(procesoActual.ProcessName);
                for (int Z001 = 0; Z001 <= procesos.Length - 1; Z001++)
                {
                    if (procesos[Z001].Id != procesoActual.Id && procesos[Z001].SessionId == Process.GetProcessById(procesoActual.Id).SessionId)
                    {
                        //Globals.SBO_Application.MessageBox("Matando " + procesoActual.ProcessName.ToString() + " " + procesoActual.MachineName.ToString());
                        Globals.SBO_Application.StatusBar.SetText("Proceso duplicado " + procesoActual.ProcessName.ToString() + " " + procesoActual.MachineName.ToString() + ". Cerrando proceso", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                        procesos[Z001].Kill();
                    }
                }
                if (autoKill)
                    procesoActual.Kill();
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
            }
        }
        public int validarInstancias(System.Diagnostics.Process procesoActual)
        {
            try
            {
                System.Diagnostics.Process[] procesos = Process.GetProcessesByName(procesoActual.ProcessName);
                return procesos.Length;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
                return 0;
            }
        }
        public bool checkCampoBD(string Tabla, string Campo)
        {
            bool retorno = false;
            try
            {
                string strSQLBD = null;
                SAPbobsCOM.Recordset oLocalBD = default(SAPbobsCOM.Recordset);
                oLocalBD = (SAPbobsCOM.Recordset)Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (Globals.IsHana() == true)
                {
                    strSQLBD = "SELECT COLUMN_NAME FROM SYS.M_CS_COLUMNS WHERE COLUMN_NAME = '" + Campo + "' AND TABLE_NAME = '" + Tabla + "' AND SCHEMA_NAME = '" + Globals.oCompany.CompanyDB.ToString() + "'";
                    oLocalBD.DoQuery(strSQLBD);
                }
                else
                {
                    strSQLBD = "SELECT column_name ";
                    strSQLBD += "FROM [" + Globals.oCompany.CompanyDB + "].INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = '" + Campo + "' AND Table_Name ='" + Tabla + "'";
                    oLocalBD.DoQuery(strSQLBD);
                }
                if (oLocalBD.EoF == false)
                {
                    retorno = true;
                }
                Globals.Release(oLocalBD);
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
            }
            return retorno;
        }
        public void creaTablaMD(string NbTabla, string DescTabla, SAPbobsCOM.BoUTBTableType TablaTipo)
        {
            SAPbobsCOM.UserTablesMD oUserTablesMD = default(SAPbobsCOM.UserTablesMD);
            try
            {
                int iVer = 0;
                oUserTablesMD = (SAPbobsCOM.UserTablesMD)Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);

                if (!oUserTablesMD.GetByKey(NbTabla))
                {
                    SAPbobsCOM.UserTablesMD tablaACrear = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                    tablaACrear.TableName = Strings.Format(NbTabla);
                    tablaACrear.TableDescription = Strings.Format(DescTabla);
                    tablaACrear.TableType = TablaTipo;

                    int retX = 0;
                    string strSQLx = "";
                    retX = tablaACrear.Add();
                    if (!(retX == 0))
                    {
                        iVer = iVer + 1;
                        Globals.oCompany.GetLastError(out retX, out strSQLx);
                    }
                    else
                    {
                        Globals.SBO_Application.StatusBar.SetText("Tabla " + NbTabla + " creada con éxito", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                    Globals.Release(tablaACrear);
                }
                Globals.Release(oUserTablesMD);
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
            }
        }
        public void creaCampoMD(string NbTabla, string NbCampo, string DescCampo, SAPbobsCOM.BoFieldTypes TipoDato, SAPbobsCOM.BoFldSubTypes subtipo = SAPbobsCOM.BoFldSubTypes.st_None, int Tamaño = 10, SAPbobsCOM.BoYesNoEnum Obligatorio = SAPbobsCOM.BoYesNoEnum.tNO, string[] validValues = null, string[] validDescription = null, string valorPorDef = "", string tablaVinculada = "")
        {
            SAPbobsCOM.UserFieldsMD oUserFieldsMD = default(SAPbobsCOM.UserFieldsMD);
            try
            {
                oUserFieldsMD = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

                oUserFieldsMD.TableName = NbTabla;
                oUserFieldsMD.Name = NbCampo;
                oUserFieldsMD.Description = DescCampo;
                oUserFieldsMD.Type = TipoDato;
                if (TipoDato != SAPbobsCOM.BoFieldTypes.db_Date)
                    oUserFieldsMD.EditSize = Tamaño;
                if (TipoDato == SAPbobsCOM.BoFieldTypes.db_Float)
                    oUserFieldsMD.SubType = subtipo;

                if (!string.IsNullOrEmpty(tablaVinculada))
                {
                    oUserFieldsMD.LinkedTable = tablaVinculada;
                }
                else
                {
                    if ((validValues != null))
                    {
                        for (int i = 0; i <= validValues.Length - 1; i++)
                        {
                            if (validDescription == null)
                            {
                                oUserFieldsMD.ValidValues.Description = validValues[i];
                            }
                            else
                            {
                                oUserFieldsMD.ValidValues.Description = validDescription[i];
                            }
                            oUserFieldsMD.ValidValues.Value = validValues[i];
                            oUserFieldsMD.ValidValues.Add();
                        }
                    }

                    if (!string.IsNullOrEmpty(valorPorDef))
                    {
                        oUserFieldsMD.DefaultValue = valorPorDef;
                        oUserFieldsMD.Mandatory = Obligatorio;
                    }
                }

                int retX = 0;
                string strSQLx = "";
                retX = oUserFieldsMD.Add();

                if (retX != 0)
                {
                    Globals.oCompany.GetLastError(out retX, out strSQLx);
                }
                else
                {
                    Globals.SBO_Application.StatusBar.SetText("Campo " + NbCampo + " " + DescCampo + ": Creado con éxito", SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldsMD);
                oUserFieldsMD = null;
                GC.Collect();
                return;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
            }
        }

       

        private string directcast(string CampoMax)
        {
            throw new NotImplementedException();
        }
        private int CompareVersion(string a, string b)
        {
            int a1, b1;
            string aa = a.Replace(".", "");
            string bb = b.Replace(".", "");
            if (aa.Length > bb.Length)
            {
                bb = bb.PadRight(bb.Length + (aa.Length - bb.Length), '0');
            }
            else if (aa.Length < bb.Length)
            {
                aa = aa.PadRight(aa.Length + (bb.Length - aa.Length), '0');
            }
            a1 = Convert.ToInt32(aa);
            b1 = Convert.ToInt32(bb);
            if (a1 == b1) return 0;
            else if (a1 < b1) return 1;
            else return 2;
        }
    }
}