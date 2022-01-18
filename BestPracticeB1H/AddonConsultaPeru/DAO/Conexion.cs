using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ADDONB1SMC.AddonConsultaPeru.DAO
{
    public class Conexion
    {
        public SqlConnection conectar()
        {
            return new SqlConnection("Server=192.168.0.140 ;Database=SBO_SMC_DEMO;User ID=sa;Password=tdnsap;Trusted_Connection=False");
        }
    }
}
