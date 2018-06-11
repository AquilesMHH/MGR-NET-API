using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Persistence.com.pe.mgr.dao.impl;

namespace MGR_Business.com.pe.mgr.service.impl
{
    public class VariacionesServiceImpl : VariacionesService
    {
        private VariacionesDaoImpl _variacionesDaoImpl;
        public VariacionesServiceImpl()
        {
            _variacionesDaoImpl = new VariacionesDaoImpl();
        }
        public List<VariacionDto> listar(string nombreTabla, string claveRegistro)
        {
            return _variacionesDaoImpl.listar(nombreTabla, claveRegistro);
        }
    }
}
