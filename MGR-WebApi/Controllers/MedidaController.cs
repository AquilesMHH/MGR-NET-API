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
    [RoutePrefix("rest/seguridad")]
    public class MedidaController : ApiController
    {
        private MedidasServiceImpl _medidasServiceImpl;
        public MedidaController()
        {
            _medidasServiceImpl = new MedidasServiceImpl();
        }
        [Route("implementarmedidamultipledao", Name = "Vario Medidas")]
        [HttpPost]
        public String implementarMedidaMultipleDao(List<MedidaRevImpRq> lstMedidaRevImpRq) {

            return _medidasServiceImpl.mplementarMedidaMultipleDao("", 0, lstMedidaRevImpRq);
        }

       

    }
}
