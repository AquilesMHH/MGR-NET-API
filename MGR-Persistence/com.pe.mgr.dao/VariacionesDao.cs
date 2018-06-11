using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Persistence.com.pe.mgr.dao
{
    public interface VariacionesDao : IDisposable
    {
        List<VariacionDto> listar(String nombreTabla, String claveRegistro);
    }
}
