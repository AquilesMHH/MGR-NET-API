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
    [RoutePrefix("rest/compendioGeneral")]
    public class CompendioGeneralController : ApiController
    {
        CompendioGeneralServiceImpl _compendioGeneralServiceImpl;
        public CompendioGeneralController()
        {
            _compendioGeneralServiceImpl = new CompendioGeneralServiceImpl();
        }
        [Route("claseMedidas", Name = "compendio Clase Medida")]
        [HttpGet]
        public List<CompendioDetalleDto> listarClaseMedidas([FromUri] int id_sujeto_riesgo)
        {
            return _compendioGeneralServiceImpl.listarClaseMedidas(id_sujeto_riesgo);
        }

    }
}
