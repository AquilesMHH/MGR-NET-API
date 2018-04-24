using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Business.com.pe.mgr.service
{
    public interface MedidasService
    {
        string mplementarMedidaMultipleDao(String metodo, int idSession, List<MedidaRevImpRq> lstMedidaRevImpRq);
    }
}
