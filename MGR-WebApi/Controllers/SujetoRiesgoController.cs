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
  
    [RoutePrefix("rest/admin/sujetoriesgo")]
    public class SujetoRiesgoController : ApiController
    {
        private SujetoRiesgoServiceImpl _sujetoRiesgoService;
        public SujetoRiesgoController()
        {
            _sujetoRiesgoService = new SujetoRiesgoServiceImpl();
        }
        [Route("porSession", Name = "Admin porSession")]
        [HttpGet]
        public List<ComboBoxDto> listaPorSession()
        {
            int sessionId = 1670;
            return _sujetoRiesgoService.listarPorSession(sessionId);
        }
          
    }
}
