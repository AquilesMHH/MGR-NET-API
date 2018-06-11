using MGR_Business.com.pe.mgr.service.impl;
using MGR_Common.com.pe.mgr.common.constants;
using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MGR_WebApi.Controllers
{
    [RoutePrefix("rest/variaciones")]
    public class VariacionesController : ApiController
    {
        VariacionesServiceImpl _variacionesServiceImpl;
        public VariacionesController()
        {
            _variacionesServiceImpl = new VariacionesServiceImpl();
        }
        [Route("variacioncompendio", Name = "compendio variacioncompendio")]
        [HttpGet]
        public List<VariacionDto> obtenerVariacionCompendio([FromUri] int idCompendio)
        {
            return _variacionesServiceImpl.listar(Constantes.GRTA_COMPENDIO_GENERAL, idCompendio.ToString());
        }
    
    }
}
