using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Business.com.pe.mgr.service
{
    public interface VariacionesService
    {
        List<VariacionDto> listar(String nombreTabla, String claveRegistro);
    }
}
