using MGR_Persistence.com.pe.mgr.dao.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Common.com.pe.mgr.common.constants;

namespace MGR_Business.com.pe.mgr.service.impl
{
    public class ComboServiceImpl : ComboService
    {
        private ComboDaoImpl _comboDaoImpl;
        public ComboServiceImpl()
        {
            _comboDaoImpl = new ComboDaoImpl();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<ComboBoxDto> listarCategoriaVocabularioNegocioCombo(int sujeto_riesgo)
        {
            return _comboDaoImpl.listarCategoriaVocabularioNegocioCombo(sujeto_riesgo);
        }

        public List<ComboBoxDto> listarCompendioDetalleCatalogoCombo(int id_compendio)
        {
            return _comboDaoImpl.listarCompendioDetalleCatalogoCombo(id_compendio);
        }

        public List<ComboBoxDto> listarCompendioDetalleCatalogoComboReferencia(int id_compendio)
        {
            return _comboDaoImpl.ListarCompendioDetalleCatalogoComboReferencia(id_compendio);
        }
        public List<ComboBoxDto> listarCategoriaSimbolosCombo(int id_compendio)
        {
            return _comboDaoImpl.listarCategoriaSimbolosCombo(id_compendio);
        }

        public List<ComboBoxDto> listarFiltroCombo(int id_compendio)
        {
            return _comboDaoImpl.listarFiltroCombo(id_compendio);
        }
        public List<ComboBoxDto> listarSujetoRiesgoCombo()
        {
          return _comboDaoImpl.listarSujetoRiesgoCombo();
        }

        public List<ComboBoxDto> listarValoresVigentesCompendioCombo()
        {
            List<ComboBoxDto> lstComboBoxDto = new List<ComboBoxDto>();
            ComboBoxDto comboBoxDto = new ComboBoxDto();
            comboBoxDto.CODIGO = Compendio.COMPENDIO_VALORES_VIGENTES_TODOS_COD);
            comboBoxDto.DESCRIPCION = Compendio.COMPENDIO_VALORES_VIGENTES_TODOS_DESC);

            lstComboBoxDto.Add(comboBoxDto);

            comboBoxDto = new ComboBoxDto();

            comboBoxDto.CODIGO =Compendio.COMPENDIO_VALORES_VIGENTES_SI_COD;
            comboBoxDto.DESCRIPCION =Compendio.COMPENDIO_VALORES_VIGENTES_SI_DESC;

            lstComboBoxDto.Add(comboBoxDto);

            comboBoxDto = new ComboBoxDto();

            comboBoxDto.CODIGO =Compendio.COMPENDIO_VALORES_VIGENTES_NO_COD;
            comboBoxDto.DESCRIPCION =Compendio.COMPENDIO_VALORES_VIGENTES_NO_DESC;

            lstComboBoxDto.Add(comboBoxDto);

            return lstComboBoxDto;
        }
        public List<ComboBoxDto> listarVariableCatalogoCombo()
        {
            return _comboDaoImpl.listarVariableCatalogoCombo();
        }
    }
}
