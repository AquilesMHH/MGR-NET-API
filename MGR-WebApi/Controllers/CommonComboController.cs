using MGR_Business.com.pe.mgr.service.impl;
using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MGR_WebApi.Controllers
{
    [RoutePrefix("rest/common")]
    public class CommonComboController : ApiController
    {
        ComboServiceImpl _comboServiceImpl;
        public CommonComboController()
        {
            _comboServiceImpl = new ComboServiceImpl();
        }
        [Route("compendioDetalleReferencia", Name = "Common Detalle Ref")]
        [HttpGet]
        public List<ComboBoxDto> listarCompendioDetalleCatalogoComboReferencia([FromUri] int id_compendio)
        {
            return _comboServiceImpl.listarCompendioDetalleCatalogoComboReferencia(id_compendio);
        }
       
        [Route("compendioDetalle", Name = "Common Detalle ")]
        [HttpGet]
        public List<ComboBoxDto> listarCompendioDetalleCatalogoCombo([FromUri] int id_compendio)
        {
            return _comboServiceImpl.listarCompendioDetalleCatalogoCombo(id_compendio);
        }

        [Route("categoriavocabularionegocio", Name = "Common categoriavocabularionegocio ")]
        [HttpGet]
        public List<ComboBoxDto> listarCategoriaVocabularioNegocioCombo([FromUri] int sujeto_riesgo)
        {
            return  _comboServiceImpl.listarCategoriaVocabularioNegocioCombo(sujeto_riesgo);
        }

        [Route("categoriasimbolos", Name = "Common categoriasimbolos ")]
        [HttpGet]
        public List<ComboBoxDto> listarCategoriaSimbolosCombo([FromUri] int sujeto_riesgo)
        {
            return _comboServiceImpl.listarCategoriaSimbolosCombo(sujeto_riesgo);
        }

        [Route("variableFiltro", Name = "Common variableFiltro ")]
        [HttpGet]
        public List<ComboBoxDto> listarVariableFiltro([FromUri] int sujeto_riesgo)
        {
            return _comboServiceImpl.listarFiltroCombo(sujeto_riesgo);
        }

        [Route("sujetoriesgo", Name = "Common sujetoriesgo ")]
        [HttpGet]
        public List<ComboBoxDto> listarSujetoRiesgoCombo()
        {
            return _comboServiceImpl.listarSujetoRiesgoCombo();
        }

        [Route("valoresvigentescompendiodetalle", Name = "Common valoresvigentescompendiodetalle ")]
        [HttpGet]
        public List<ComboBoxDto> listarValoresVigentesCompendioDetalleCombo()
        {
            return _comboServiceImpl.listarValoresVigentesCompendioCombo();
        }
        [Route("variablecatalogo", Name = "Common variablecatalogo ")]
        [HttpGet]
        public List<ComboBoxDto> listarVariableCatalogoCombo()
        {
            return _comboServiceImpl.listarVariableCatalogoCombo();
        }
         
        }

    }
}
