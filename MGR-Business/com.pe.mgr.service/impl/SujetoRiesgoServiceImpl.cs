using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Persistence.com.pe.mgr.dao.impl;

namespace MGR_Business.com.pe.mgr.service.impl
{
    public class SujetoRiesgoServiceImpl : SujetoRiesgoService
    {
        private SujetoRiesgoDaoImpl _SujetoRiesgoDaoImpl;
        public SujetoRiesgoServiceImpl()
        {
            _SujetoRiesgoDaoImpl = new SujetoRiesgoDaoImpl();
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

        public List<ComboBoxDto> listarPorSession(int id_session)
        {
            return _SujetoRiesgoDaoImpl.listarPorSession(id_session);
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
