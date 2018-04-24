using MGR_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using System.Data.Entity;
using MGR_Persistence.com.pe.mgr.dao.SqlString;
using System.Configuration;
using System.Data;

namespace MGR_Persistence.com.pe.mgr.dao.impl
{

    public class SujetoRiesgoDaoImpl : SujetoRiesgoDao, IDisposable
    {
        private ModelMGRContext context;
        OracleConect oracleConex;
        public string conn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public SujetoRiesgoDaoImpl()
        {
            context = new ModelMGRContext();
            oracleConex = new OracleConect();
        }
 
        public List<ComboBoxDto> listarPorSession(int id_session)
        {
            MgrLoginBean mgrLoginBean = new MgrLoginBean();
            int pIdUsuario = 0;
            string sqlQuery = "";
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // var vGRTA_USUARIO = context.Database.SqlQuery<GRTA_USUARIO>(MgrEnumConsultaGeneral.MgrVerificaUsuario(obj),pIdUsuario).FirstOrDefault();
                    List<ComboBoxDto> objLista =new  List<ComboBoxDto>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrSujetoRiesgoUsuario(id_session), System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
                        return objLista;
                    }
                    //GRTA_USUARIO varUsuario = (from t in context.GRTA_USUARIO where t.ID_USUARIO == pIdUsuario orderby t.ID_USUARIO descending select t).SingleOrDefault();

                }
                catch (Exception ext)
                {
                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }
            }
            return null;
        }
 
    public SujetoRiesgoRs actualizar(int sujeto_riesgo, SujetoRiesgoRq sujetoRiesgoRq)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public SujetoRiesgoRs eliminar(int sujeto_riesgo)
        {
            throw new NotImplementedException();
        }

        public List<SujetoRiesgoDto> listar(SujetoRiesgoRq sujetoRiesgoRq)
        {
            throw new NotImplementedException();
        }

        public List<ComboBoxDto> listarCombo()
        {
            throw new NotImplementedException();
        }

        public List<ComboBoxDto> listarComboSujeto(int id_tipo_seleccion)
        {
            throw new NotImplementedException();
        }

        public List<ComboBoxDto> listarComboVFiltro(int id_sujeto_riesgo)
        {
            throw new NotImplementedException();
        }

     

        public SujetoRiesgoRs obtenerAmbitoRestriccion(int sujeto_riesgo)
        {
            throw new NotImplementedException();
        }

        public SujetoRiesgoDto obtenerPorId(int sujeto_riesgo)
        {
            throw new NotImplementedException();
        }

        public SujetoRiesgoRs registrar(SujetoRiesgoRq sujetoRiesgoRq)
        {
            throw new NotImplementedException();
        }
    }
}


