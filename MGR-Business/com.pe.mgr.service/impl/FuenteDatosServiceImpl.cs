using MGR_Entidades.Entidad;
using MGR_Persistence.com.pe.mgr.dao;
using MGR_Persistence.com.pe.mgr.dao.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Business.com.pe.mgr.service.impl
{
    public class FuenteDatosServiceImpl : FuenteDatosService, IDisposable
    {
        private SujetoRiesgoDaoImpl _sujetoRiesgoDAO;
        public FuenteDatosServiceImpl()
        {
            _sujetoRiesgoDAO = new SujetoRiesgoDaoImpl();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public GRTA_SUJETO_RIESGO get(GRTA_SUJETO_RIESGO obj)
        {
            throw new NotImplementedException();
        }

        public List<VW_COMPENDIO_SUJETO> listar(VW_COMPENDIO_SUJETO obj)
        {
           return  null;
        }

        public int registrar(FuenteDatosTablaAnalisisRq obj)
        {
            throw new NotImplementedException();
        }

        public int update(FuenteDatosTablaAnalisisRq obj)
        {
            throw new NotImplementedException();
        }




    }

}
