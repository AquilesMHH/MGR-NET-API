using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using System.Configuration;
using MGR_Model;
using MGR_Persistence.com.pe.mgr.dao.SqlString;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MGR_Persistence.com.pe.mgr.dao.impl
{

    public class CompendioGeneralDaoImpl : CompendioGeneralDao, IDisposable
    {
        private ModelMGRContext context;
       
        public string conn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public CompendioGeneralDaoImpl()
        {
            context = new ModelMGRContext();
        }

        public CompendioGeneral guardarCompendioGeneral(CompendioGeneral obj)
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral obtenerCompendioGeneral(int id_compendio)
        {
            throw new NotImplementedException();
        }

        public List<CompendioGeneralBean> consultarPorParametro(CompendioGeneral obj)
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral actualizarCompendioGeneral(CompendioGeneral obj)
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral eliminarCompendioGeneral(int id_compendio)
        {
            throw new NotImplementedException();
        }

        public List<CompendioDetalleDto> listarClaseMedidas(int id_sujeto_riesgo)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<CompendioDetalleDto> objLista = new List<CompendioDetalleDto>();
                    OracleParameter[] parameters = new OracleParameter[] {
                                 new OracleParameter("@id_sujeto_riesgo", id_sujeto_riesgo) };
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrClaseMedidaCombo(id_sujeto_riesgo), System.Data.CommandType.Text, parameters);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<CompendioDetalleDto>();
                        return objLista;
                    }
                }
                catch (Exception ext)
                {
                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }
                return null;
            }
        }


        public List<CompendioGeneralBean> listarCompendioDetalles(int id_compendio)
        {
            throw new NotImplementedException();
        }

        public List<CompendioGeneralBean> listarTipoTabla(int id_sujeto_riesgo)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral get(int id_compendio)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<CompendioGeneral> objLista = new List<CompendioGeneral>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.obtenerCompendioGeneral(id_compendio), System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<CompendioGeneral>();
                        return objLista[0];
                    }
                }
                catch (Exception ext)
                {
                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }
                return null;
            }
        }
    }
}
