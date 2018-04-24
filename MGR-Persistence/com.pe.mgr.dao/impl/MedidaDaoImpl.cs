using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Model;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MGR_Persistence.com.pe.mgr.dao.impl
{ 
    public class MedidaDaoImpl : MedidasDao,  IDisposable
    {
        private ModelMGRContext context;
        public string conn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public MedidaDaoImpl()
        {
            context = new ModelMGRContext();
        }

        public string mplementarMedidaMultipleDao(String metodo, int idSession,  List<MedidaRevImpRq> lstMedidaRevImpRq)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                  
                }
                catch (Exception ext)
                {
                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }
                return null;
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
