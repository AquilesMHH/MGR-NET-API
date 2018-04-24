using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Persistence.com.pe.mgr.dao.impl;

namespace MGR_Business.com.pe.mgr.service.impl
{

    public class CompendioGeneralServiceImpl : CompendioGeneralService
    {
        private CompendioGeneralDaoImpl _compendioGeneralDaoImpl;
        public CompendioGeneralServiceImpl()
        {
            _compendioGeneralDaoImpl = new CompendioGeneralDaoImpl();
        }

        public CompendioGeneral actualizarCompendioGeneral(CompendioGeneral obj)
        {
            throw new NotImplementedException();
        }

        public List<CompendioGeneralBean> consultarPorParametro(CompendioGeneral obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral eliminarCompendioGeneral(int id_compendio)
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral guardarCompendioGeneral(CompendioGeneral obj)
        {
            throw new NotImplementedException();
        }

        public List<CompendioDetalleDto> listarClaseMedidas(int id_sujeto_riesgo)
        {
            return _compendioGeneralDaoImpl.listarClaseMedidas(id_sujeto_riesgo);
        }

        public List<CompendioGeneralBean> listarCompendioDetalles(int id_compendio)
        {
            throw new NotImplementedException();
        }

        public List<CompendioGeneralBean> listarTipoTabla(int id_sujeto_riesgo)
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral obtenerCompendioGeneral(int id_compendio)
        {
            throw new NotImplementedException();
        }
    }
}
