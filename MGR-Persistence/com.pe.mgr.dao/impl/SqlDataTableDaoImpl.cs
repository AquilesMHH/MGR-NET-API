using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using MGR_Model;

namespace MGR_Persistence.com.pe.mgr.dao.impl
{
    public class SqlDataTableDaoImpl : SqlDataTableDao
    {
        private ModelMGRContext _context;
        public SqlDataTableDaoImpl()
        {
            _context = new ModelMGRContext();
        }
        public DataTable GetDataTable(string sqlQuery, OracleParameter[] parameters)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory(_context.Database.Connection);

                    using (var cmd = factory.CreateCommand())
                    {
                        cmd.CommandText = sqlQuery;
                        cmd.Parameters.AddRange(parameters);
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = _context.Database.Connection;
                        using (var adapter = factory.CreateDataAdapter())
                        {
                            adapter.SelectCommand = cmd;

                            var tb = new DataTable();
                            adapter.Fill(tb);
                            return tb;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return null;
        }
    }
}
