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
    public class SeguridadController : ApiController
    {
        private LoginServiceImpl _loginService;
        public SeguridadController()
        {
            _loginService = new LoginServiceImpl();
        }
        [Route("login", Name = "Seguidad login")]
        [HttpPost]
        public MgrLoginBean login([FromUri] string usuario, string clave)
        {
            GRTA_USUARIO objUser = new GRTA_USUARIO();
            objUser.USUARIO = usuario;
            objUser.CLAVE = clave;
            return _loginService.login(objUser, "dd", "adsf");
        }


    }
}
