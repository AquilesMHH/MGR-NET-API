using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Model;
using MGR_Persistence.com.pe.mgr.dao.SqlString;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MGR_Persistence.com.pe.mgr.dao.impl
{
    public class ComboDaoImpl : ComboDao, IDisposable
    {
        private ModelMGRContext context;
        OracleConect oracleConex;
        public string conn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public ComboDaoImpl()
        {
            context = new ModelMGRContext();
            oracleConex = new OracleConect();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<ComboBoxDto> ListarCompendioDetalleCatalogoComboReferencia(int id_compendio)
        {
           using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    OracleParameter[] parameters = new OracleParameter[] {
                                 new OracleParameter("@id_compendio", id_compendio) };
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrCompendioDetalleComboReferencia(id_compendio), System.Data.CommandType.Text, parameters);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
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

        public List<ComboBoxDto> listarCompendioDetalleCatalogoCombo(int id_compendio)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    OracleParameter[] parameters = new OracleParameter[] {
                                 new OracleParameter("@id_compendio", id_compendio) };
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrCompendioDetalleCombo(id_compendio), System.Data.CommandType.Text, parameters);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
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

        public List<ComboBoxDto> listarCategoriaVocabularioNegocioCombo(int sujeto_riesgo)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrCategoriaCategoriaVocabularioNegocioCombo(sujeto_riesgo), System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
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

        public List<ComboBoxDto> listarCategoriaParametrosCombo(int sujeto_riesgo)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrCategoriaParametrosCombo(sujeto_riesgo), System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
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
        public List<ComboBoxDto> listarCategoriaSimbolosCombo(int sujeto_riesgo)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrCategoriaSimbolosCombo(sujeto_riesgo), System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
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

        public List<ComboBoxDto> listarFiltroCombo(int sujeto_riesgo)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrVariableFiltroCombo(sujeto_riesgo), System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
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

        public List<ComboBoxDto> listarSujetoRiesgoCombo()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrSujetoRiesgoCombo(), System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
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

        public List<ComboBoxDto> listarValoresVigentesCompendioCombo()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrSujetoRiesgoCombo(), System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
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

        public List<ComboBoxDto> listarVariableCatalogoCombo()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrVariableCatalogo(), System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
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
    }
}
