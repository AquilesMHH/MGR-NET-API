using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Persistence.com.pe.mgr.dao
{
    
   public interface SqlDataTableDao
   {
       DataTable GetDataTable(string sqlQuery, OracleParameter[] parameters);

    }
}
