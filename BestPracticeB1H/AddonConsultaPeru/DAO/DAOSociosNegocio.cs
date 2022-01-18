using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ADDONB1SMC.AddonConsultaPeru.DTO;

namespace ADDONB1SMC.AddonConsultaPeru.DAO
{
    public class DAOSociosNegocio
    {

        public List<DTOSociosNegocio> Listar(string estado,string condicion)
        {
            List<DTOSociosNegocio> lstDTOSociosNegocio = new List<DTOSociosNegocio>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();

                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSocios", cn);
                    da.SelectCommand.Parameters.AddWithValue("@estado", estado);
                    da.SelectCommand.Parameters.AddWithValue("@condicion", condicion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();

                    while (dr.Read())
                    {
                        DTOSociosNegocio oDTOSociosNegocio = new DTOSociosNegocio();
                        oDTOSociosNegocio.Codigo = dr["LicTradNum"].ToString();
                        oDTOSociosNegocio.RazonSocial = dr["CardName"].ToString();
                        oDTOSociosNegocio.Tipo = dr["U_EXX_TIPODOCU"].ToString();
                        oDTOSociosNegocio.Condicion = dr["U_SMC_Condicion"].ToString();
                        oDTOSociosNegocio.Estado = dr["U_SMC_Estado"].ToString();
                        oDTOSociosNegocio.Ubigeo = dr["U_SMC_Ubigeo"].ToString();
                        lstDTOSociosNegocio.Add(oDTOSociosNegocio);
                    }
                }
                catch (Exception ex)
                {
                }

                return lstDTOSociosNegocio;
            }

        }

    }
}
