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
    }
}
