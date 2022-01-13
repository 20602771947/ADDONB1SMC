using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using ADDONB1SMC.DB_Structure;

namespace ADDONB1SMC.DB_Structure
{
    class Functions
    {
        public static void DropCreateFunction(string FunctionName, bool isHana, string ResourceName)
        {
            try
            {
                
                
            }
            catch (Exception ex)
            {
                Globals.SBO_Application.SetStatusBarMessage(ex.Message);
            }
        }
    }
}
