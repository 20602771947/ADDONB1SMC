using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Xml;
using System.Security.Cryptography;

using Ionic.Zip;


namespace ADDONB1SMC
{
    public static class Globals
    {
        public static int cuantosrepetidos = 0;
        public static int camposentotal = 0;

        public static SAPbouiCOM.ProgressBar progressBar;
        public static SAPbobsCOM.Recordset oRec = default(SAPbobsCOM.Recordset);
        public static SAPbobsCOM.Recordset oRec2 = default(SAPbobsCOM.Recordset);
        public static SAPbobsCOM.Recordset oRec3 = default(SAPbobsCOM.Recordset);
        public static SAPbobsCOM.Recordset oRec4 = default(SAPbobsCOM.Recordset);
        public static SAPbobsCOM.Recordset oRec5 = default(SAPbobsCOM.Recordset);
        public static SAPbouiCOM.Application SBO_Application;
        public static SAPbobsCOM.CompanyService oCmpSrv;
        public static SAPbouiCOM.EventFilters oFilters;
        public static SAPbouiCOM.EventFilter oFilter;
        public static SAPbobsCOM.Company oCompany;
        public static int SAPVersion;
        public static bool licensed = false;        
        public static SAPbouiCOM.Form DialogForm;
        public static string filename;
        private static string pathItem;

        public static string User = null;
        public static bool SuperUser = false;
        public static string Addon = null;
        public static string version = null;
        public static bool IGVMixed = false;
        public static bool IGVReprocesado = false;
        public static string oldversion = "";
        public static bool Actual = false;
        public static string Query = null;
        public static string Query2 = null;
        public static string Query3 = null;
        public static string Query4 = null;
        public static string Query5 = null;
        public static Dictionary<string, string> CancelDictionary = new Dictionary<string, string> { };

        public static string TXTPath = null;
        public static string SentToDataFormEventAction = "";
        public static string[] AuxForFormDataEvent = new string[10] { "", "", "", "", "", "", "", "", "", "" };
        public static bool RetButtonExists = false;


        //Validation Variables
        public static string Action = null;
        public static string Error = null;
        public static string ServerError = null;
        public static int continuar = -1;

        public static string LogFile = null;

        public static string FE = null;
        public static string CFG = null;
        public static string BAN = null;
        public static string DET = null;
        public static string RET = null;
        public static string PRD = null;
        public static string REP = null;
        public static string FTR = null;
        public static string FNG = null;
        public static string CDR = null; //Consignador
        public static string GRC = null; //Grandes compradores
        public static string CBM = null; //Cobros masivos
        public static string AlmDestino = "";
        public static string USDCurrCode = "?", EURCurrCode = "?", GBPCurrCode = "?", CADCurrCode = "?";

        #region RL globals and encryption
        #region Boolean & encryption vars
        public static byte[] Key = Encoding.ASCII.GetBytes("12EstaClave34es56dificil489ssswf");
        public static byte[] IV = Encoding.ASCII.GetBytes("Devjoker7.37hAES");
        //Impresos
        public static bool bRL_01_01 = false;
        public static bool bRL_01_02 = false;
        public static bool bRL_03_01 = false;
        public static bool bRL_03_02 = false;
        public static bool bRL_03_03 = false;
        public static bool bRL_03_0A = false;
        public static bool bRL_03_04 = false;
        public static bool bRL_03_05 = false;
        public static bool bRL_03_0B = false;
        public static bool bRL_03_0C = false;
        public static bool bRL_03_06 = false;
        public static bool bRL_03_07 = false;
        public static bool bRL_03_08 = false;
        public static bool bRL_03_09 = false;
        public static bool bRL_03_0D = false;
        public static bool bRL_03_10 = false;
        public static bool bRL_03_11 = false;
        public static bool bRL_03_12 = false;
        public static bool bRL_03_0E = false;
        public static bool bRL_03_0F = false;
        public static bool bRL_03_0G = false;
        public static bool bRL_03_13 = false;
        public static bool bRL_03_14 = false;
        public static bool bRL_03_0H = false;
        public static bool bRL_03_15 = false;
        public static bool bRL_03_16 = false;
        public static bool bRL_03_17 = false;
        public static bool bRL_03_18 = false;
        public static bool bRL_03_19 = false;
        public static bool bRL_03_20 = false;
        public static bool bRL_03_0N = false;
        public static bool bRL_04_01 = false;
        public static bool bRL_05_01 = false;
        public static bool bRL_06_01 = false;
        public static bool bRL_07_01 = false;
        public static bool bRL_07_02 = false;
        public static bool bRL_07_03 = false;
        public static bool bRL_07_04 = false;
        public static bool bRL_08_01 = false;
        public static bool bRL_09_01 = false;
        public static bool bRL_09_02 = false;
        public static bool bRL_10_01 = false;
        public static bool bRL_10_02 = false;
        public static bool bRL_10_03 = false;
        public static bool bRL_12_01 = false;
        public static bool bRL_13_01 = false;
        public static bool bRL_14_01 = false;
        public static bool bRL_16_01 = false;
        public static bool bRL_17_01 = false;
        //PDTs
        public static bool bDT_06_26 = false;
        public static bool bDT_DA_IN = false;
        public static bool bDT_DA_CS = false;
        public static bool bDT_DA_CP = false;
        public static bool bDT_DA_CC = false;
        public static bool bDT_DA_BC = false;
        public static bool bDT_06_97 = false;
        public static bool bDT_PL_PS = false;
        public static bool bDT_PL_DC = false;
        //PDBs
        public static bool bDB_DA_CO = false;
        public static bool bDB_DA_VE = false;
        public static bool bDB_DA_TC = false;
        public static bool bDB_DA_FP = false;
        //Libros Electronicos
        public static bool bLE_05_01 = false;
        public static bool bLE_05_02 = false;
        public static bool bLE_06_01 = false;
        public static bool bLE_08_01 = false;
        public static bool bLE_13_01 = false;
        public static bool bLE_14_01 = false;

        //ALEJANDRO 20160401
        //49
        public static bool bLE_05_01_01 = false;
        public static bool bLE_05_01_02 = false;
        public static bool bLE_05_03_01 = false;
        public static bool bLE_05_03_02 = false;
        public static bool bLE_05_03_03 = false;
        public static bool bLE_05_03_04 = false;
        public static bool bLE_05_03_05 = false;
        public static bool bLE_05_03_06 = false;
        public static bool bLE_05_03_07 = false; //Cta 20
        public static bool bLE_05_03_09 = false;//Cta 34
        public static bool bLE_05_03_11 = false;//Cta 41
        public static bool bLE_05_03_12 = false;//Cta 42
        public static bool bLE_05_03_13 = false;//Cta 46
        public static bool bLE_05_03_14 = false;//Cta 47
        public static bool bLE_05_03_15 = false;//Cta 49
        public static bool bLE_05_03_16 = false;//Cta 50
        public static bool bLE_05_03_17 = false;//Balance Compro
        public static bool bLE_05_03_18 = false;//Flujo de efectivo
        public static bool bLE_05_03_19 = false;//patrimonio neto
        public static bool bLE_05_03_20 = false;//Funcion
        public static bool bLE_05_03_23 = false;  //03.23  NOTAS A LOS ESTADOS FINANCIEROS (3)
        public static bool bLE_05_03_24 = false; //03.24  ESTADO DE RESULTADOS INTEGRALES
        public static bool bLE_05_03_25 = false; //03.25   ESTADO DE FLUJOS DE EFECTIVO - MÉTODO INDIRECTO
        public static bool bLE_05_04_01 = false;//retenciones 4.1
        public static bool bLE_05_05_01 = false;//Diario
        public static bool bLE_05_05_03 = false;//Detalle plan cuentas
        public static bool bLE_05_06_01 = false;//Mayor
        public static bool bLE_05_07_01 = false;//Detalle Activos Fijos

        public static bool bLE_05_07_03 = false;//Activos Diferencia de cambio
        public static bool bLE_05_07_04 = false;//Activos Leasing
        public static bool bLE_05_08_01 = false;//Compras
        public static bool bLE_05_08_02 = false;//Compras no domiciliado
        public static bool bLE_05_09_01 = false;//Consignador
        public static bool bLE_05_09_02 = false;//Consignatario
        public static bool bLE_05_10_01 = false;//Costos 1
        public static bool bLE_05_10_02 = false;//Costos 2
        public static bool bLE_05_10_03 = false;//Costos 3
        public static bool bLE_05_10_04 = false; //10.4 Centro de Costo
        public static bool bLE_05_12_01 = false;//Unidades fisicas
        public static bool bLE_05_13_01 = false;//Inventario valorizado
        public static bool bLE_05_14_01 = false;//Ventas  
        public static bool bLE_05_17_01 = false;//retencion
        public static bool bLE_05_16_01 = false;//percepcion

        #endregion
        public static string DecryptString(string CryptedText)
        {
            if (CryptedText == null)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (CryptedText.Length == 0)
            {
                return "";
            }
            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException("Key");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("Key");
            }
            byte[] cipherText = Convert.FromBase64String(CryptedText);
            string plaintext = null;
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt, System.Text.Encoding.UTF8))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
       
      
        #endregion

        public static void StartProgress(int Steps, string StartLabel)
        {
            progressBar = Globals.SBO_Application.StatusBar.CreateProgressBar(StartLabel, Steps, true);
            progressBar.Text = StartLabel;
            progressBar.Value = 0;
            for (int i = 1; i <= Steps; i++)
            {
                progressBar.Value = i;
            }
            progressBar.Stop();
            Globals.Release(progressBar);
        }

        public static void EndProgress()
        {
            progressBar.Stop();
            Globals.Release(progressBar);
        }

        public static object Release(object objeto)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objeto);
            Query = null;
            GC.Collect();
            return null;
        }
        public static SAPbobsCOM.Recordset RunQuery(string Query)
        {
            try
            {
                oRec = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRec.DoQuery(Query);
                return oRec;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
                return null;
            }
        }
        public static SAPbobsCOM.Recordset RunQuery2(string Query2)
        {
            try
            {
                oRec2 = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRec2.DoQuery(Query2);
                return oRec2;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
                return null;
            }
        }
        public static SAPbobsCOM.Recordset RunQuery3(string Query3)
        {
            try
            {
                oRec3 = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRec3.DoQuery(Query3);
                return oRec3;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
                return null;
            }
        }
        public static SAPbobsCOM.Recordset RunQuery4(string Query4)
        {
            try
            {
                oRec4 = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRec4.DoQuery(Query4);
                return oRec4;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
                return null;
            }
        }
        public static SAPbobsCOM.Recordset RunQuery5(string Query5)
        {
            try
            {
                oRec5 = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRec5.DoQuery(Query5);
                return oRec5;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
                return null;
            }
        }
        public static bool IsHana()
        {
            try
            {
                if (Globals.oCompany.DbServerType == (SAPbobsCOM.BoDataServerTypes)9)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.MessageBox(ex.Message);
                return false;
            }
        }
        public static string GetUDFValue(string Field, string Type, int FormType, int FormTypeCount)
        {
            FormType = -1 * FormType;
            SAPbouiCOM.Form oUserForm = Globals.SBO_Application.Forms.GetFormByTypeAndCount(FormType, FormTypeCount);
            if (Type == "ComboBox")
            {
                SAPbouiCOM.ComboBox oUDF = oUserForm.Items.Item(Field).Specific;
                return (oUDF.Value.Replace(" ", "") == "" | oUDF.Value == null) ? null : oUDF.Value;
            }
            else if (Type == "EditText")
            {
                SAPbouiCOM.EditText oUDF = oUserForm.Items.Item(Field).Specific;
                return (oUDF.Value.Replace(" ", "") == "" | oUDF.Value == null) ? null : oUDF.Value;
            }
            else
            {
                return null;
            }
        }
        public static void SetUDFValue(string Field, string Type, int FormType, int FormTypeCount, string NewValue)
        {
            FormType = -1 * FormType;
            SAPbouiCOM.Form oUserForm = Globals.SBO_Application.Forms.GetFormByTypeAndCount(FormType, FormTypeCount);
            if (Type == "EditText")
            {
                SAPbouiCOM.EditText oUDF = oUserForm.Items.Item(Field).Specific;
                oUDF.Value = NewValue;
            }
            if (Type == "ComboBox")
            {
                SAPbouiCOM.ComboBox oUDF = oUserForm.Items.Item(Field).Specific;
                oUDF.Select(NewValue);
            }
        }
        public static void ClearUDF(string Field, SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.EditText oUDF = oForm.Items.Item(Field).Specific;
            oUDF.Value = "";
        }
        public static string GetResourceValue(string name, string ResourceName)
        {
            //ResourceManager rm = new ResourceManager("ADDONB1SMC.Properties.Resources", Assembly.GetExecutingAssembly());
            ResourceManager rm = new ResourceManager(ResourceName, Assembly.GetExecutingAssembly());
            string value = rm.GetString(name);
            return value;
        }
        public static string LoadFromXML(ref string FileName)
        {
            System.Xml.XmlDocument oXmlDoc = null;
            string sPath = null;
            oXmlDoc = new System.Xml.XmlDocument();
            sPath = System.Windows.Forms.Application.StartupPath;
            oXmlDoc.Load(sPath + FileName);
            return (oXmlDoc.InnerXml);
        }
        
        public static void DeleteTxt(string filename, string destPath) {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + destPath;
            string FILE_NAME = path + "\\" + filename + ".txt";
            if (System.IO.File.Exists(FILE_NAME) == true)
            {
                System.IO.File.Delete(FILE_NAME);
            }
        }
        public static void DeleteFile2(string Formato, string filename, string destPath)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + destPath;
            string FILE_NAME = path + "\\" + filename + "." + Formato;
            if (System.IO.File.Exists(FILE_NAME) == true)
            {
                System.IO.File.Delete(FILE_NAME);
            }
        }
        public static void WriteTxt(string x, string filename, string destPath)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + destPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //else {
            //    //Directory.Delete(path);
            //    Directory.CreateDirectory(path);
            //}
            string FILE_NAME = path + "\\" + filename + ".txt";
            if (System.IO.File.Exists(FILE_NAME) == false)
            {
                System.IO.File.Create(FILE_NAME).Dispose();
            }
            System.IO.StreamWriter objWriter = new System.IO.StreamWriter(FILE_NAME, true, Encoding.Default);
            objWriter.WriteLine(x);
            objWriter.Close();
        }
        #region Grandes Compradores
        public static void GenerarZip(string filename, string destPath) {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + destPath;
            string FILE_NAME = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + filename;
            ZipFile zip = new ZipFile();
            zip.AddFile(FILE_NAME, "");
            zip.Save(path);
        }
        public static bool ValidarExistenciaArchivo(string file,string formato, string directorio) {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + directorio;
            if (!Directory.Exists(path))
            {
                return false;
            }
            string FILE_NAME = path + "\\" + file + formato;
            if (System.IO.File.Exists(FILE_NAME) == false)
            {
                return false;
            }
            return true;
        }
        #endregion
        //public static string CalculateMD5Hash(string input)
        //{
        //    // step 1, calculate MD5 hash from input
        //    MD5 md5 = System.Security.Cryptography.MD5.Create();
        //    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        //    byte[] hash = md5.ComputeHash(inputBytes);

        //    // step 2, convert byte array to hex string
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < hash.Length; i++)
        //    {
        //        sb.Append(hash[i].ToString("X2"));
        //    }
        //    return sb.ToString();
        //}
        //public static string CheckLicense()
        //{
        //    Globals.Query = "SELECT \"TaxIdNum\" FROM OADM";
        //    Globals.RunQuery(Globals.Query);
        //    Globals.oRec.MoveFirst();
        //    string RUC = Globals.oRec.Fields.Item(0).Value ?? "";
        //    Globals.Release(Globals.oRec);
        //    if (RUC != "")
        //    {
        //        bool LicField = MDFields.FieldExists("ADM1", "SYP_LICHASH");
        //        if (!LicField) { return "Licencia BPS Inválida"; }
        //        //Globals.Query = (Globals.IsHana()) ? "SELECT HARDWARE_KEY from M_LICENSE" : "SELECT net_address FROM sysprocesses WHERE spid = @@SPID";
        //        //Globals.Query = (Globals.IsHana()) ? "SELECT HARDWARE_KEY from M_LICENSE" : "Declare @t table (i uniqueidentifier default newsequentialid(),m as cast(i as char(36))) insert into @t default values; select substring(m,25,2) + substring(m,27,2) + substring(m,29,2) + substring(m,31,2) + substring(m,33,2) + substring(m,35,2) AS MacAddress FROM @t";
        //        Globals.Query = (Globals.IsHana()) ? "SELECT HARDWARE_KEY from M_LICENSE" : "SELECT CAST(SERVERPROPERTY('CollationID') as nvarchar(max)) + CAST(SERVERPROPERTY('ServerName') as nvarchar(max))";
        //        Globals.RunQuery(Globals.Query);
        //        Globals.oRec.MoveFirst();
        //        string MAC = Globals.oRec.Fields.Item(0).Value;
        //        Globals.Release(Globals.oRec);
        //        string firstHash = CalculateMD5Hash(RUC + MAC + "lollipop");
        //        string secondHash = CalculateMD5Hash(firstHash + "popsicle");
        //        Globals.Query = "SELECT U_SYP_LICHASH FROM ADM1";
        //        Globals.RunQuery(Globals.Query);
        //        Globals.oRec.MoveFirst();
        //        string SavedHash = Globals.oRec.Fields.Item(0).Value;
        //        Globals.Release(Globals.oRec);
        //        if (SavedHash == secondHash) { return ""; }
        //        else { return "Licencia BPS Inválida"; }
        //        //select create_date, DATEADD(dd, 15, create_date) from SYS.databases WHERE name = 'GREENBEAN'
        //    }
        //    else
        //    {
        //        return "Licencia BPS Inválida";
        //    }
        //}
        //public static void LoadLicenseForm()
        //{
        //    bool valid = false;
        //    SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);
        //    SAPbouiCOM.FormCreationParams fcp = default(SAPbouiCOM.FormCreationParams);
        //    fcp = Globals.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
        //    fcp.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Sizable;
        //    fcp.FormType = "Lic";
        //    fcp.UniqueID = "Lic_1";
        //    string FormName = "\\frmLicense.srf";
        //    fcp.XmlData = Globals.LoadFromXML(ref FormName);
        //    oForm = Globals.SBO_Application.Forms.AddEx(fcp);
        //    string hk = "";
        //    Globals.Query = "SELECT \"TaxIdNum\" FROM OADM";
        //    Globals.RunQuery(Globals.Query);
        //    Globals.oRec.MoveFirst();
        //    string RUC = Globals.oRec.Fields.Item(0).Value ?? "";
        //    Globals.Release(Globals.oRec);
        //    if (RUC != "")
        //    {
        //        bool LicField = MDFields.FieldExists("ADM1", "SYP_LICHASH");
        //        if (!LicField) { hk = "Actualizar estructura BPS"; }
        //        else
        //        {
        //            Globals.Query = (Globals.IsHana()) ? "SELECT HARDWARE_KEY from M_LICENSE" : "SELECT CAST(SERVERPROPERTY('CollationID') as nvarchar(max)) + CAST(SERVERPROPERTY('ServerName') as nvarchar(max))";
        //            Globals.RunQuery(Globals.Query);
        //            Globals.oRec.MoveFirst();
        //            string MAC = Globals.oRec.Fields.Item(0).Value;
        //            Globals.Release(Globals.oRec);
        //            hk = CalculateMD5Hash(RUC + MAC + "lollipop");
        //            valid = true;
        //        }
        //    }
        //    else { hk = "Definir Ruc en detalles de sociedad"; }
        //    oForm.Items.Item("hk").Specific.caption = hk;
        //    //if (!valid) { oForm.Items.Item("btnBrowse").Specific.Enabled = false; }

        //    oForm.Top = 150;
        //    oForm.Left = 350;
        //    oForm.Visible = true;
        //}
        public static void SentToFormDataEvent()
        {
            #region CCNew
            if (Globals.AuxForFormDataEvent[0] == "CCNew")
            {
                string newCorr = Globals.AuxForFormDataEvent[1];
                string oSerCCER = Globals.AuxForFormDataEvent[2];
                Globals.AuxForFormDataEvent[0] = "";
                Globals.AuxForFormDataEvent[1] = "";
                Globals.AuxForFormDataEvent[2] = "";
                Globals.Query = "UPDATE \"@SYP_NUMDOC\" SET \"U_SYP_NDCD\" = '" + newCorr + "' WHERE \"Code\" = 'CC' and \"U_SYP_NDSD\" = '" + oSerCCER + "'";
                Globals.RunQuery(Globals.Query);
                Globals.Release(Globals.oRec);
            }
            #endregion
            #region ERNew
            if (Globals.AuxForFormDataEvent[0] == "ERNew")
            {
                string newCorr = Globals.AuxForFormDataEvent[1];
                string oSerCCER = Globals.AuxForFormDataEvent[2];
                Globals.AuxForFormDataEvent[0] = "";
                Globals.AuxForFormDataEvent[1] = "";
                Globals.AuxForFormDataEvent[2] = "";
                Globals.Query = "UPDATE \"@SYP_NUMDOC\" SET \"U_SYP_NDCD\" = '" + newCorr + "' WHERE \"Code\" = 'ER' and \"U_SYP_NDSD\" = '" + oSerCCER + "'";
                Globals.RunQuery(Globals.Query);
                Globals.Release(Globals.oRec);
            }
            #endregion
            #region CCClose
            if (Globals.AuxForFormDataEvent[0] == "CCClose")
            {
                bool asociada = (Globals.AuxForFormDataEvent[2] == "Asociada") ? true : false;
                string NroCaja = AuxForFormDataEvent[3];
                string CuentaAsoc = AuxForFormDataEvent[4];
                string Message = (asociada) ? "Se creó el asiento " + Globals.AuxForFormDataEvent[1] + ". Se procederá a abrir la ventana de reconciliaciones" : "Se creó el asiento preliminar de cierre: " + Globals.AuxForFormDataEvent[1] + ". Revisarlo, crearlo y reconciliar la cuenta";
                Globals.SBO_Application.MessageBox(Message);
                
                Globals.AuxForFormDataEvent[0] = "";
                Globals.AuxForFormDataEvent[1] = "";
                Globals.AuxForFormDataEvent[2] = "";
                Globals.AuxForFormDataEvent[3] = "";
                Globals.AuxForFormDataEvent[4] = "";
                if (!asociada) Globals.SBO_Application.ActivateMenuItem("1541");
                else 
                {
                    List<string> BPs = new List<string>();
                    Globals.Query = "SELECT DISTINCT A.\"ShortName\" FROM JDT1 A INNER JOIN OACT B ON A.\"Account\" = B.\"AcctCode\" WHERE \"Ref1\" = '" + NroCaja + "' and B.\"LocManTran\" = 'Y'";
                    Globals.RunQuery(Globals.Query);
                    if (Globals.oRec.RecordCount == 0)
                    {
                        Globals.Release(Globals.oRec);
                        Globals.SBO_Application.MessageBox("No se encontró ninguna provisión para esta Entrega a Rendir");
                    }
                    else
                    {
                        for (int i = 0; i < Globals.oRec.RecordCount; i++)
                        {
                            BPs.Add(Globals.oRec.Fields.Item(0).Value);
                            Globals.oRec.MoveNext();
                        }
                        Globals.Release(Globals.oRec);
                        if (Globals.SBO_Application.MessageBox("Se abrirá la ventana de reconciliaciones. \n¿Desea Continuar?", 1, "Si", "No") == 1)
                        {
                            Globals.SBO_Application.ActivateMenuItem("9459");
                            SAPbouiCOM.Form oReconForm = SBO_Application.Forms.ActiveForm;
                            oReconForm.Items.Item("10000084").Specific.Checked = true;
                            SAPbouiCOM.Matrix oReconMatrix = oReconForm.Items.Item("10000085").Specific;
                            for (int i = 0; i < BPs.Count; i++)
                            {
                                oReconMatrix.Columns.Item("10000003").Cells.Item(i + 1).Specific.Value = BPs[i];
                            }
                            oReconForm.Items.Item("120000001").Click();
                            SAPbouiCOM.Form oReconForm2 = SBO_Application.Forms.ActiveForm;
                            oReconForm2.Items.Item("Column").Specific.Select("Ref.1 (fila SN)");
                            oReconForm2.Items.Item("Value").Specific.Value = NroCaja;
                            oReconForm2.Items.Item("Pick").Click();
                        }
                    }
                }
            }
            #endregion
            #region ERClose
            if (Globals.AuxForFormDataEvent[0] == "ERClose")
            {
                List<string> BPs = new List<string>();
                Globals.Query = "SELECT DISTINCT \"CardCode\" FROM OPCH WHERE \"U_SYP_CODERCC\" = '" + Globals.AuxForFormDataEvent[2] + "'";
                string oEmployee = Globals.AuxForFormDataEvent[1];
                string oERNum = Globals.AuxForFormDataEvent[2];
                Globals.AuxForFormDataEvent[0] = "";
                Globals.AuxForFormDataEvent[1] = "";
                Globals.AuxForFormDataEvent[2] = "";
                Globals.RunQuery(Globals.Query);
                Globals.oRec.MoveFirst();
                if (Globals.oRec.RecordCount == 0)
                {
                    Globals.Release(Globals.oRec);
                    Globals.SBO_Application.MessageBox("No se encontró ninguna provisión para esta Entrega a Rendir");
                }
                else
                {
                    for (int i = 0; i < Globals.oRec.RecordCount; i++)
                    {
                        if (Globals.oRec.Fields.Item(0).Value != oEmployee) { BPs.Add(Globals.oRec.Fields.Item(0).Value); }
                        Globals.oRec.MoveNext();
                    }
                    Globals.Release(Globals.oRec);
                    if (Globals.SBO_Application.MessageBox("Se abrirá la ventana de reconciliaciones. \n¿Desea Continuar?", 1, "Si", "No") == 1)
                    {
                        Globals.SBO_Application.ActivateMenuItem("9459");
                        SAPbouiCOM.Form oReconForm = SBO_Application.Forms.ActiveForm;
                        oReconForm.Items.Item("10000084").Specific.Checked = true;
                        SAPbouiCOM.Matrix oReconMatrix = oReconForm.Items.Item("10000085").Specific;
                        oReconMatrix.Columns.Item("10000003").Cells.Item(1).Specific.Value = oEmployee;
                        for (int i = 0; i < BPs.Count; i++)
                        {
                            oReconMatrix.Columns.Item("10000003").Cells.Item(i + 2).Specific.Value = BPs[i];
                        }
                        oReconForm.Items.Item("120000001").Click();
                        SAPbouiCOM.Form oReconForm2 = SBO_Application.Forms.ActiveForm;
                        oReconForm2.Items.Item("Column").Specific.Select("Ref.1 (fila SN)");
                        oReconForm2.Items.Item("Value").Specific.Value = oERNum;
                        oReconForm2.Items.Item("Pick").Click();
                    }
                }
            }
            #endregion
            #region CancelSalesDownpayment
            if (Globals.AuxForFormDataEvent[0] == "CancelSalesDownpayment")
            {
                string oOriginalDocEntry = Globals.AuxForFormDataEvent[1];
                string oTipoOriginal = Globals.AuxForFormDataEvent[2];
                string oSerie = Globals.AuxForFormDataEvent[3];
                string oCorrelativo = Globals.AuxForFormDataEvent[4];
                string oStatus = Globals.AuxForFormDataEvent[5];
                Globals.AuxForFormDataEvent[0] = "";
                Globals.AuxForFormDataEvent[1] = "";
                Globals.AuxForFormDataEvent[2] = "";
                Globals.AuxForFormDataEvent[3] = "";
                Globals.AuxForFormDataEvent[4] = "";
                Globals.AuxForFormDataEvent[5] = "";
                SAPbobsCOM.Documents oOriginalDoc = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDownPayments);
                oOriginalDoc.GetByKey(Convert.ToInt32(oOriginalDocEntry));
                oOriginalDoc.NumAtCard = oTipoOriginal + "-" + oSerie + "-" + oCorrelativo;
                oOriginalDoc.JournalMemo = oTipoOriginal + "-" + oSerie + "-" + oCorrelativo + " (Cancelado)";
                oOriginalDoc.UserFields.Fields.Item("U_SYP_MDTD").Value = oTipoOriginal;
                oOriginalDoc.UserFields.Fields.Item("U_SYP_STATUS").Value = oStatus.Replace(" ", "");
                oOriginalDoc.Update();
            }
            #endregion
            #region CancelPurchaseDownpayment
            if (Globals.AuxForFormDataEvent[0] == "CancelPurchaseDownpayment")
            {
                string oOriginalDocEntry = Globals.AuxForFormDataEvent[1];
                string oTipoOriginal = Globals.AuxForFormDataEvent[2];
                string oSerie = Globals.AuxForFormDataEvent[3];
                string oCorrelativo = Globals.AuxForFormDataEvent[4];
                string oStatus = Globals.AuxForFormDataEvent[5];
                Globals.AuxForFormDataEvent[0] = "";
                Globals.AuxForFormDataEvent[1] = "";
                Globals.AuxForFormDataEvent[2] = "";
                Globals.AuxForFormDataEvent[3] = "";
                Globals.AuxForFormDataEvent[4] = "";
                Globals.AuxForFormDataEvent[5] = "";
                SAPbobsCOM.Documents oOriginalDoc = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDownPayments);
                oOriginalDoc.GetByKey(Convert.ToInt32(oOriginalDocEntry));
                oOriginalDoc.NumAtCard = oTipoOriginal + "-" + oSerie + "-" + oCorrelativo;
                oOriginalDoc.JournalMemo = oTipoOriginal + "-" + oSerie + "-" + oCorrelativo + " (Cancelado)";
                oOriginalDoc.UserFields.Fields.Item("U_SYP_MDTD").Value = oTipoOriginal;
                oOriginalDoc.UserFields.Fields.Item("U_SYP_STATUS").Value = oStatus.Replace(" ", "");
                oOriginalDoc.Update();
            }
            #endregion
            #region Cancel Stock Transfer
            if (Globals.AuxForFormDataEvent[0] == "CancelStockTransfer")
            {
                string Series = AuxForFormDataEvent[1].Replace(" ", "");
                string DocNum = AuxForFormDataEvent[2];
                AuxForFormDataEvent[0] = "";
                AuxForFormDataEvent[1] = "";
                AuxForFormDataEvent[2] = "";
                System.Threading.Thread.Sleep(500);
                Query = "SELECT \"CANCELED\", \"U_SYP_STATUS\", \"U_SYP_MDTD\", \"U_SYP_MDSD\", \"U_SYP_MDCD\" FROM OWTR WHERE \"Series\" = '" + Series + "' and \"DocNum\" = '" + DocNum + "'";
                RunQuery(Query);
                oRec.MoveFirst();
                if (oRec.Fields.Item(0).Value == "Y")
                {
                    string tipo = oRec.Fields.Item(2).Value;
                    string seri = oRec.Fields.Item(3).Value;
                    string corr = oRec.Fields.Item(4).Value;
                    string stat = oRec.Fields.Item(1).Value;
                    string tipo2 = (stat == "I") ? "AI" : tipo;
                    Release(oRec);

                    Query = "UPDATE OWTR SET \"U_SYP_STATUS\" = '" + stat + "', \"U_SYP_MDTD\" = '" + tipo2 + "' WHERE \"U_SYP_MDTD\" = '" + tipo + "' and \"U_SYP_MDSD\" = '" + seri + "' and \"U_SYP_MDCD\" = '" + corr + "'";
                    RunQuery(Query);
                    Release(oRec);
                }
            }
            #endregion
        }
        public static void WriteLogTxt(string x, string filename)
        {
            //System.Windows.Forms.Application.StartupPath;
            string path = System.Windows.Forms.Application.StartupPath + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string FILE_NAME = path + "\\" + filename + ".txt";
            if (System.IO.File.Exists(FILE_NAME) == false)
            {
                System.IO.File.Create(FILE_NAME).Dispose();
            }
            System.IO.StreamWriter objWriter = new System.IO.StreamWriter(FILE_NAME, true, Encoding.Default);
            objWriter.WriteLine(x);
            objWriter.Close();
        }
        public static DateTime ConvertDate(string date)
        {
            if (date.Length == 8)
            {
                date = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                return Convert.ToDateTime(date);
            }
            else
            {
                Globals.Error = "(SYP)BPS : Invalid Date Format";
                throw new Exception(Globals.Error);
            }
        }

        public static void TipoCambio()
        {
            try
            {
                DateTime CurrDate = DateTime.Today;
                DateTime QueryDate;
                int dayInt = Convert.ToInt32(CurrDate.DayOfWeek);
                if (dayInt == 6) { QueryDate = CurrDate.AddDays(-1); }
                else if (dayInt == 0) { QueryDate = CurrDate.AddDays(-2); }
                else if (dayInt == 1) { QueryDate = CurrDate.AddDays(-3); }
                else { QueryDate = CurrDate.AddDays(-1); }
                bool USD = false, EUR = false, GBP = false, CAD = false;
                bool USD_a = true, USD_b = true, USD_c = true, EUR_a = true, EUR_b = true, EUR_c = true, GBP_a = true, GBP_b = true, GBP_c = true, CAD_a = true, CAD_b = true, CAD_c = true;
                Globals.USDCurrCode = "?";
                Globals.EURCurrCode = "?";
                Globals.GBPCurrCode = "?";
                Globals.CADCurrCode = "?";
                int unassigned = 0;

                Globals.Query = "SELECT \"ISOCurrCod\", \"CurrCode\" FROM OCRN WHERE \"ISOCurrCod\" in ('USD', 'EUR', 'GBP', 'CAD')";
                Globals.RunQuery(Globals.Query);
                if (Globals.oRec.RecordCount > 0)
                {
                    Globals.oRec.MoveFirst();
                    while (!Globals.oRec.EoF)
                    {
                        if (Globals.oRec.Fields.Item(0).Value == "USD")
                        {
                            if (!USD)
                            {
                                USD = true;
                                Globals.USDCurrCode = Globals.oRec.Fields.Item(1).Value;

                                Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + CurrDate.ToString("yyyyMMdd") + "' and \"Currency\" = '" + USDCurrCode + "'";
                                Globals.RunQuery2(Globals.Query2);
                                if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { USD_a = false; unassigned++; }
                                Globals.Release(Globals.oRec2);

                                if (dayInt == 1)
                                {
                                    Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + (CurrDate.AddDays(-1)).ToString("yyyyMMdd") + "' and \"Currency\" = '" + USDCurrCode + "'";
                                    Globals.RunQuery2(Globals.Query2);
                                    if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { USD_b = false; unassigned++; }
                                    Globals.Release(Globals.oRec2);

                                    Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + (CurrDate.AddDays(-2)).ToString("yyyyMMdd") + "' and \"Currency\" = '" + USDCurrCode + "'";
                                    Globals.RunQuery2(Globals.Query2);
                                    if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { USD_c = false; unassigned++; }
                                    Globals.Release(Globals.oRec2);
                                }
                            }
                        }
                        else if (Globals.oRec.Fields.Item(0).Value == "EUR")
                        {
                            if (!EUR)
                            {
                                EUR = true;
                                Globals.EURCurrCode = Globals.oRec.Fields.Item(1).Value;

                                Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + CurrDate.ToString("yyyyMMdd") + "' and \"Currency\" = '" + EURCurrCode + "'";
                                Globals.RunQuery2(Globals.Query2);
                                if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { EUR_a = false; unassigned++; }
                                Globals.Release(Globals.oRec2);

                                if (dayInt == 1)
                                {
                                    Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + (CurrDate.AddDays(-1)).ToString("yyyyMMdd") + "' and \"Currency\" = '" + EURCurrCode + "'";
                                    Globals.RunQuery2(Globals.Query2);
                                    if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { EUR_b = false; unassigned++; }
                                    Globals.Release(Globals.oRec2);

                                    Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + (CurrDate.AddDays(-2)).ToString("yyyyMMdd") + "' and \"Currency\" = '" + EURCurrCode + "'";
                                    Globals.RunQuery2(Globals.Query2);
                                    if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { EUR_c = false; unassigned++; }
                                    Globals.Release(Globals.oRec2);
                                }
                            }
                        }
                        else if (Globals.oRec.Fields.Item(0).Value == "GBP")
                        {
                            if (!GBP)
                            {
                                GBP = true;
                                Globals.GBPCurrCode = Globals.oRec.Fields.Item(1).Value;

                                Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + CurrDate.ToString("yyyyMMdd") + "' and \"Currency\" = '" + GBPCurrCode + "'";
                                Globals.RunQuery2(Globals.Query2);
                                if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { GBP_a = false; unassigned++; }
                                Globals.Release(Globals.oRec2);

                                if (dayInt == 1)
                                {
                                    Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + (CurrDate.AddDays(-1)).ToString("yyyyMMdd") + "' and \"Currency\" = '" + GBPCurrCode + "'";
                                    Globals.RunQuery2(Globals.Query2);
                                    if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { GBP_b = false; unassigned++; }
                                    Globals.Release(Globals.oRec2);

                                    Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + (CurrDate.AddDays(-2)).ToString("yyyyMMdd") + "' and \"Currency\" = '" + GBPCurrCode + "'";
                                    Globals.RunQuery2(Globals.Query2);
                                    if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { GBP_c = false; unassigned++; }
                                    Globals.Release(Globals.oRec2);
                                }
                            }
                        }
                        else
                        {
                            if (!CAD)
                            {
                                CAD = true;
                                Globals.CADCurrCode = Globals.oRec.Fields.Item(1).Value;

                                Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + CurrDate.ToString("yyyyMMdd") + "' and \"Currency\" = '" + CADCurrCode + "'";
                                Globals.RunQuery2(Globals.Query2);
                                if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { CAD_a = false; unassigned++; }
                                Globals.Release(Globals.oRec2);

                                if (dayInt == 1)
                                {
                                    Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + (CurrDate.AddDays(-1)).ToString("yyyyMMdd") + "' and \"Currency\" = '" + CADCurrCode + "'";
                                    Globals.RunQuery2(Globals.Query2);
                                    if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { CAD_b = false; unassigned++; }
                                    Globals.Release(Globals.oRec2);

                                    Globals.Query2 = "SELECT COUNT(\"Rate\") FROM ORTT WHERE \"RateDate\" = '" + (CurrDate.AddDays(-2)).ToString("yyyyMMdd") + "' and \"Currency\" = '" + CADCurrCode + "'";
                                    Globals.RunQuery2(Globals.Query2);
                                    if (!(Globals.oRec2.Fields.Item(0).Value == 1)) { CAD_c = false; unassigned++; }
                                    Globals.Release(Globals.oRec2);
                                }
                            }
                        }
                        Globals.oRec.MoveNext();
                    }
                    Globals.Release(Globals.oRec);

                    if (unassigned > 0)
                    {
                        string year = QueryDate.Year.ToString().PadLeft(2, '0');
                        string month = QueryDate.Month.ToString().PadLeft(2, '0');
                        string day = QueryDate.Day.ToString().PadLeft(2, '0');
                        string url = "https://www.sbs.gob.pe/app/stats/tc-cv.asp?FECHA_CONSULTA=" + day + "/" + month + "/" + year;
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.ContentType = "text/html";
                        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        int status = Convert.ToInt32(response.StatusCode);
                        if (status < 200 || status >= 300) { Globals.SBO_Application.MessageBox("Error " + status + " al buscar el tipo de cambio, reiniciar SAP"); return; }
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string respuesta = reader.ReadToEnd();
                        respuesta = respuesta.Replace("\r", "");
                        respuesta = respuesta.Replace("\n", "");
                        respuesta = respuesta.Replace("\t", "");
                        respuesta = respuesta.Replace("&nbsp", "");

                        //'Murica
                        int index = respuesta.IndexOf("Dólar de N.A.<br />");
                        respuesta = respuesta.Substring(index);
                        index = respuesta.IndexOf("<td class=\"APLI_fila2\">");
                        respuesta = respuesta.Substring(index + 25);
                        index = respuesta.IndexOf("<td class=\"APLI_fila2\">");
                        respuesta = respuesta.Substring(index + 25);
                        int largo = respuesta.IndexOf("</td>");
                        string dolar = respuesta.Substring(0, largo);
                        double dolarTC = Convert.ToDouble(dolar);

                        //Ohh Canada!!
                        index = respuesta.IndexOf("Dólar canadiense<br />");
                        respuesta = respuesta.Substring(index);
                        index = respuesta.IndexOf("<td class=\"APLI_fila2\">");
                        respuesta = respuesta.Substring(index + 25);
                        index = respuesta.IndexOf("<td class=\"APLI_fila2\">");
                        respuesta = respuesta.Substring(index + 25);
                        largo = respuesta.IndexOf("</td>");
                        string looney = respuesta.Substring(0, largo);
                        double looneyTC = Convert.ToDouble(looney);

                        //Brexits
                        index = respuesta.IndexOf("Libra Esterlina<br />");
                        respuesta = respuesta.Substring(index);
                        index = respuesta.IndexOf("<td class=\"APLI_fila2\">");
                        respuesta = respuesta.Substring(index + 25);
                        index = respuesta.IndexOf("<td class=\"APLI_fila2\">");
                        respuesta = respuesta.Substring(index + 25);
                        largo = respuesta.IndexOf("</td>");
                        string libra = respuesta.Substring(0, largo);
                        double libraTC = Convert.ToDouble(libra);

                        //Old Continent
                        index = respuesta.IndexOf("Euro<br />");
                        respuesta = respuesta.Substring(index);
                        index = respuesta.IndexOf("<td class=\"APLI_fila2\">");
                        respuesta = respuesta.Substring(index + 25);
                        index = respuesta.IndexOf("<td class=\"APLI_fila2\">");
                        respuesta = respuesta.Substring(index + 25);
                        largo = respuesta.IndexOf("</td>");
                        string euro = respuesta.Substring(0, largo);
                        double euroTC = Convert.ToDouble(euro);

                        SAPbobsCOM.SBObob SBObob = Globals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);
                        if (USD && !USD_a) { SBObob.SetCurrencyRate(USDCurrCode, CurrDate, dolarTC, false); }
                        if (USD && !USD_b) { SBObob.SetCurrencyRate(USDCurrCode, CurrDate.AddDays(-1), dolarTC, false); }
                        if (USD && !USD_c) { SBObob.SetCurrencyRate(USDCurrCode, CurrDate.AddDays(-2), dolarTC, false); }
                        if (EUR && !EUR_a) { SBObob.SetCurrencyRate(EURCurrCode, CurrDate, euroTC, false); }
                        if (EUR && !EUR_b) { SBObob.SetCurrencyRate(EURCurrCode, CurrDate.AddDays(-1), euroTC, false); }
                        if (EUR && !EUR_c) { SBObob.SetCurrencyRate(EURCurrCode, CurrDate.AddDays(-2), euroTC, false); }
                        if (CAD && !CAD_a) { SBObob.SetCurrencyRate(CADCurrCode, CurrDate, looneyTC, false); }
                        if (CAD && !CAD_b) { SBObob.SetCurrencyRate(CADCurrCode, CurrDate.AddDays(-1), looneyTC, false); }
                        if (CAD && !CAD_c) { SBObob.SetCurrencyRate(CADCurrCode, CurrDate.AddDays(-2), looneyTC, false); }
                        if (GBP && !GBP_a) { SBObob.SetCurrencyRate(GBPCurrCode, CurrDate, libraTC, false); }
                        if (GBP && !GBP_b) { SBObob.SetCurrencyRate(GBPCurrCode, CurrDate.AddDays(-1), libraTC, false); }
                        if (GBP && !GBP_c) { SBObob.SetCurrencyRate(GBPCurrCode, CurrDate.AddDays(-2), libraTC, false); }
                    }
                }
                else
                {
                    Globals.Release(Globals.oRec);
                }


            }
            catch (Exception ex)
            {
                Globals.SBO_Application.SetStatusBarMessage("Error al conseguir tipo de cambio.\n" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, false);
            }
        }
        //In order for this to work alway use the item id "Path" for the editText where the path will be saved.
        public static void OpenFile(SAPbouiCOM.Form oForm, string path)
        {
            try
            {
                pathItem = path;
                DialogForm = oForm;
                System.Threading.Thread ShowFolderBrowserThread;
                ShowFolderBrowserThread = new System.Threading.Thread(ShowFolderBrowser);
                if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    ShowFolderBrowserThread.SetApartmentState(ApartmentState.STA);
                    ShowFolderBrowserThread.Start();
                }
                else if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    ShowFolderBrowserThread.Start();
                    ShowFolderBrowserThread.Join();
                }
                //ShowFolderBrowserThread.Abort();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public static void OpenFileExt(SAPbouiCOM.Form oForm, string path)
        {
            try
            {
                pathItem = path;
                DialogForm = oForm;
                System.Threading.Thread ShowFolderBrowserThread;
                ShowFolderBrowserThread = new System.Threading.Thread(ShowFolderBrowserExt);
                if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    ShowFolderBrowserThread.SetApartmentState(ApartmentState.STA);
                    ShowFolderBrowserThread.Start();
                }
                else if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    ShowFolderBrowserThread.Start();
                    ShowFolderBrowserThread.Join();
                }
                //ShowFolderBrowserThread.Abort();
            }
            catch (Exception ex)
            {
            }
        }
        public static void UploadFile(SAPbouiCOM.Form oForm, string Path)
        {
            Dictionary<string, string> allTheThings = new Dictionary<string, string>();
 	          Dictionary<string, string> allTheThings2 = new Dictionary<string, string>();

            using (StreamReader file = new StreamReader(Path))
            {
                string nrocons = "";
                string nrocomp = "";
                string nrodoc = "";
                string oProveedor = "";
                string oDocNum = "";

                SAPbouiCOM.Matrix oMatrix = oForm.Items.Item("MyGrid").Specific;
                string oLote = oForm.Items.Item("Combo1").Specific.Value;

                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string[] bananaSplits = line.Split('\t');

                    if (bananaSplits[0] == "Numero de operacion            ")
                        allTheThings.Add(bananaSplits[0].Trim(), bananaSplits[1].Trim());
                    else if (bananaSplits[0] == "Lote                           ")
                    {
                        string val1 = bananaSplits[0].Trim();
                        string val2 = bananaSplits[1].Trim();
                        allTheThings.Add(val1, val2);
                    }
                    else if (bananaSplits[0] == "Numero de constancia                  ")
                        nrocons = bananaSplits[1].Trim();
                    else if (bananaSplits[0] == "N�mero Documento del Proveedor      ")
                        nrodoc = bananaSplits[1].Trim();
                    else if (bananaSplits[0] == "N�mero de Comprobante                 ")
                    {
                        nrocomp = bananaSplits[1].Trim();
                        allTheThings.Add(nrocomp + nrodoc, nrocons);
                    }
                    continue;
                }

                if (oLote.StartsWith("P"))
                    oLote = oLote.Substring(1);

                Globals.Query = "SELECT \"DocNum\",D.\"LicTradNum\" from OPCH H inner join OCRD D on H.\"CardCode\" = D.\"CardCode\"  WHERE U_SYP_DLOTE = 'P" + oLote + "' ";
                Globals.RunQuery(Globals.Query);
                Globals.oRec.MoveFirst();

                while (!Globals.oRec.EoF)
                {
                    oDocNum = Globals.oRec.Fields.Item(0).Value.ToString();
                    oProveedor = Globals.oRec.Fields.Item(1).Value;

                    allTheThings2.Add(oDocNum, oProveedor);
                    Globals.oRec.MoveNext();
                }

                if (oLote == allTheThings["Lote"])
                {
                    for (int i = 1; i <= oMatrix.RowCount; i++)
                    {
                        string oDoc3 = oMatrix.Columns.Item("Col_10").Cells.Item(i).Specific.Value;
                        oDoc3 = oDoc3.Substring(3);
                        string[] bananaSplits2 = oDoc3.Split('-');
                        string oDoc13 = bananaSplits2[0];
                        string oDoc23 = bananaSplits2[1];
                        oDoc13 = oDoc13.PadLeft(4, '0');
                        oDoc23 = oDoc23.PadLeft(8, '0');
                        oDoc3 = oDoc13 + " " + oDoc23;

                        string oDocNum2 = oMatrix.Columns.Item("Col_3").Cells.Item(i).Specific.Value;
                        //oMatrix.Columns.Item("Col_17").Cells.Item(i).Specific.Value = allTheThings["Numero de operacion"];
                        oMatrix.Columns.Item("Col_16").Cells.Item(i).Specific.Value = allTheThings[oDoc3 + allTheThings2[oDocNum2]];
                    }
                }
            }
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(allTheThings);
            //GC.Collect();
        }
        public static void OpenFolder(SAPbouiCOM.Form oForm, string path)
        {
            try
            {
                pathItem = path;
                DialogForm = oForm;
                System.Threading.Thread ShowFolderBrowserThread;
                ShowFolderBrowserThread = new System.Threading.Thread(ShowFolderBrowseDialog);
                if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    ShowFolderBrowserThread.SetApartmentState(ApartmentState.STA);
                    ShowFolderBrowserThread.Start();
                }
                else if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    ShowFolderBrowserThread.Start();
                    ShowFolderBrowserThread.Join();
                }
                //ShowFolderBrowserThread.Abort();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public static void ShowFolderBrowser()
        {
            try
            {
                NativeWindow nws = new NativeWindow();
                OpenFileDialog MyTest = new OpenFileDialog();
                MyTest.Multiselect = false;
                MyTest.Filter = "Text Files (.txt)|*.txt";
                Process[] MyProcs = null;
                //string filename = null;
                MyProcs = Process.GetProcessesByName("SAP Business One");
                nws.AssignHandle(System.Diagnostics.Process.GetProcessesByName("SAP Business One")[0].MainWindowHandle);
                if (MyTest.ShowDialog(nws) == System.Windows.Forms.DialogResult.OK)
                {
                    filename = MyTest.FileName;
                    DialogForm.Items.Item(pathItem).Specific.Value = filename;
                    DialogForm = null;
                    pathItem = null;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public static void ShowFolderBrowserExt()
        {
            try
            {
                NativeWindow nws = new NativeWindow();
                OpenFileDialog MyTest = new OpenFileDialog();
                MyTest.Multiselect = false;
                MyTest.Filter = "Libro de Excel 97-2003 (.xls)|*.xls|Libro de Excel (*.xlsx)|*.xlsx";
                Process[] MyProcs = null;
                //string filename = null;
                MyProcs = Process.GetProcessesByName("SAP Business One");
                nws.AssignHandle(System.Diagnostics.Process.GetProcessesByName("SAP Business One")[0].MainWindowHandle);
                if (MyTest.ShowDialog(nws) == System.Windows.Forms.DialogResult.OK)
                {
                    filename = MyTest.FileName;
                    DialogForm.Items.Item(pathItem).Specific.Value = filename;
                    DialogForm = null;
                    pathItem = null;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static void ShowFolderBrowseDialog()
        {
            try
            {
                NativeWindow nws = new NativeWindow();
                FolderBrowserDialog MyTest = new FolderBrowserDialog();
                Process[] MyProcs = null;
                //string filename = null;
                MyProcs = Process.GetProcessesByName("SAP Business One");
                nws.AssignHandle(System.Diagnostics.Process.GetProcessesByName("SAP Business One")[0].MainWindowHandle);
                if (MyTest.ShowDialog(nws) == System.Windows.Forms.DialogResult.OK)
                {
                    DialogForm.Items.Item(pathItem).Specific.Value = MyTest.SelectedPath;
                    DialogForm = null;
                    pathItem = null;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public static string VerificarRUC(string RUC)
        {
            try
            {
                if (RUC.Length == 11)
                {
                    int d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, sumDigits, residuo, resta, CheckDigit;
                    d1 = Convert.ToInt32(RUC.Substring(0, 1)) * 5;
                    d2 = Convert.ToInt32(RUC.Substring(1, 1)) * 4;
                    d3 = Convert.ToInt32(RUC.Substring(2, 1)) * 3;
                    d4 = Convert.ToInt32(RUC.Substring(3, 1)) * 2;
                    d5 = Convert.ToInt32(RUC.Substring(4, 1)) * 7;
                    d6 = Convert.ToInt32(RUC.Substring(5, 1)) * 6;
                    d7 = Convert.ToInt32(RUC.Substring(6, 1)) * 5;
                    d8 = Convert.ToInt32(RUC.Substring(7, 1)) * 4;
                    d9 = Convert.ToInt32(RUC.Substring(8, 1)) * 3;
                    d10 = Convert.ToInt32(RUC.Substring(9, 1)) * 2;
                    d11 = Convert.ToInt32(RUC.Substring(10, 1));

                    sumDigits = d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8 + d9 + d10;
                    residuo = sumDigits % 11;
                    resta = 11 - residuo;

                    if (resta == 10) { CheckDigit = 0; }
                    else if (resta == 11) { CheckDigit = 1; }
                    else { CheckDigit = resta; }

                    if (d11 != CheckDigit)
                    {
                        return "(SYP)BPS : El RUC ingresado es incorrecto";
                    }
                    else
                    {
                        return "ok";
                    }
                }
                else
                {
                    return "(SYP)BPS : El RUC debe tener 11 dígitos";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
     
    }
}