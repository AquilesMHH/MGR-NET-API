using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MGR_WebApi.Controllers
{
    [RoutePrefix("rest/operacioncomun")]
    public class OperacionComunController : ApiController
    {
        [Route("consultardescripcioncatalogo", Name = "Detall consultardescripcioncatalogo")]
        [HttpGet]
        public String consutlarDescripcionCatalogo([FromUri]  int variable, string codigo)
        {
            return null;
        }

        [Route("autocompletarparacompendio", Name = "Detall autocompletarparacompendio")]
        [HttpGet]
        public String autocompletarParaCompendio([FromUri]  int variable, string codigo)
        {
            return null;
        }


        [Route("consultardescripcionfiltro", Name = "Detall consultardescripcionfiltro")]
        [HttpGet]
        public String consultarDescripcionFiltro([FromUri]  int variable, string codigo)
        {
            return null;
        }
    }
}
