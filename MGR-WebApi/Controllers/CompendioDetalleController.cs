using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MGR_WebApi.Controllers
{
    [RoutePrefix("rest/compendioDetalle")]
    public class CompendioDetalleController : ApiController
    {
       // ComboServiceImpl _comboServiceImpl;
        public CompendioDetalleController()
        {
          //  _comboServiceImpl = new ComboServiceImpl();
        }

        [Route("resultados", Name = "Detall Result")]
        [HttpGet]
        public List<CompendioDetalleDto> Detalle([FromUri]  int id_compendio,  int id_detalle)
        {
            return null;
        }

    }
}
