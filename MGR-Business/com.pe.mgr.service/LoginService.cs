using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Business.com.pe.mgr.service
{
    public interface LoginService : IDisposable
    {
        MgrLoginBean login(GRTA_USUARIO usuario, String ip, String token);
        FuncionalidadDto obtenerFuncionalidadDerecho(String usuario, String codigo_funcionalidad);
    }
}
