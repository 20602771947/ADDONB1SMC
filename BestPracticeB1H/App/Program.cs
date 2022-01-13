using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ADDONB1SMC.DB_Structure;
using ADDONB1SMC.NewItems;


namespace ADDONB1SMC
{
    class Program
    {
        [STAThread]
        public static void Main()
        {            
            try
            {                
                BPPMain oBPPRun = new BPPMain();
                if (Globals.continuar == 0)
                    System.Windows.Forms.Application.Run();
            }
            catch (Exception e)
            {
                Globals.SBO_Application.MessageBox(e.Message.ToString());
            }
        }
    }
}
