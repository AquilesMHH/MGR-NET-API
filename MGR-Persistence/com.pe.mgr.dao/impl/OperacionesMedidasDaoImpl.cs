using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Model;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace MGR_Persistence.com.pe.mgr.dao.impl
{
    public class OperacionesMedidasDaoImpl : OperacionesMedidasDao
    {
        private ModelMGRContext context;
        public string conn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public OperacionesMedidasDaoImpl()
        {
            context = new ModelMGRContext();
        }
        public int saveOrUpdate(OperacionesMedidas operaciones_medidas, bool grabar)
        {
            String sql = "";
            String sqlSub = "";
            int ouputValue = 0;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (!grabar)
                    {
                        sql = "update grta_operaciones_medidas "
                               + "set id_medida  = " + "" + operaciones_medidas.ID_MEDIDA + "" + ", "
                               + "version_medida = " + "" + operaciones_medidas.VERSION_MEDIDA + "" + ", "
                               + "session_operacion = " + "" + operaciones_medidas.SESSION_OPERACION + "" + ", "
                               + "comentario  = " + "'" + operaciones_medidas.COMENTARIO + "'" + ", "
                               + "fecha_operacion = " + "'" + operaciones_medidas.FECHA_OPERACION + "'"
                               + "where  operaciones_medidas = " + "'" + operaciones_medidas.OPERACIONES_MEDIDAS + "'";
                    }
                    else
                    {
                        OracleParameter param1 = new OracleParameter("P_ID", OracleDbType.Int16);
                        param1.Direction = System.Data.ParameterDirection.Output;
                        sqlSub = sql = "select max(OPERACIONES_MEDIDAS)+1 from grta_operaciones_medidas";
                        var varRol = context.Database.SqlQuery<int>(sqlSub, param1).Single();
                        ouputValue = varRol;
                        sql = "insert into grta_operaciones_medidas ("
                               + "operaciones_medidas, "
                               + "id_medida, "
                               + "version_medida, "
                               + "session_operacion, "
                               + "tipo_operacion, "
                               + "comentario, "
                               + "fecha_operacion) "
                               + "values (" + "" + param1.Value + "" + ","
                               + "" + operaciones_medidas.ID_MEDIDA + "" + ","
                               + "" + operaciones_medidas.VERSION_MEDIDA + "" + ","
                               + "" + operaciones_medidas.SESSION_OPERACION + "" + ","
                               + "" + operaciones_medidas.TIPO_OPERACION + "" + ","
                               + "" + operaciones_medidas.COMENTARIO + "" + ","
                               + "" + operaciones_medidas.FECHA_OPERACION + "" + ")";
                    }
                    MGR_Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, null);
                }
                catch (Exception ext)
                {
                  string valor = ext.ToString();
                  dbContextTransaction.Rollback();
                }
            }
            return ouputValue;
        }
       
    }
}
