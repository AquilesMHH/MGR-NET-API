using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using System.Data;
using MGR_Persistence.com.pe.mgr.dao.SqlString;
using System.Configuration;

namespace MGR_Persistence.com.pe.mgr.dao.impl
{
    public class VariacionesDaoImpl : VariacionesDao
    {
        public string conn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<VariacionDto> listar(string nombreTabla, string claveRegistro)
        {
            string query = MgrEnumConsultaGeneral.MgrVariaciones(nombreTabla, claveRegistro)
            List<VariacionDto> objLista = null;
            DataSet dataSet = MGR_Common.OracleHelper.Query(conn, query, System.Data.CommandType.Text, null);
            if (dataSet != null)
            {
                objLista = dataSet.Tables[0].DataTableToList<VariacionDto>();
                return objLista;
            }
            return null;
            throw new NotImplementedException();
        }
    }
}
