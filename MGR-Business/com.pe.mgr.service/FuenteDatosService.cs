using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Business.com.pe.mgr.service
{
   
    public interface FuenteDatosService : IDisposable
    {
        int registrar(FuenteDatosTablaAnalisisRq obj);
        int update(FuenteDatosTablaAnalisisRq obj);
        GRTA_SUJETO_RIESGO get(GRTA_SUJETO_RIESGO obj);
        List<VW_COMPENDIO_SUJETO> listar(VW_COMPENDIO_SUJETO obj);

    }
}
