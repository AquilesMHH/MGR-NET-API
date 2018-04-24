using MGR_Business.com.pe.mgr.service;
using MGR_Business.com.pe.mgr.service.impl;
using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MGR_WebApi.Controllers
{
    //[RoutePrefix("/rest/admin/fuentedatos")]
    [RoutePrefix("rest/admin/FuenteDatos")]
    public class FuenteDatosController : ApiController
    {
        private FuenteDatosServiceImpl _fuenteDatosService;
        // GET api/<controller>
        public FuenteDatosController()
        {
            _fuenteDatosService = new FuenteDatosServiceImpl();
        }

        [Route("listar", Name = "Listado de Fuente de Datos")]
        [HttpGet]
        public IEnumerable<VW_COMPENDIO_SUJETO> listar([FromUri] FuenteDatosRq model)
        {
            VW_COMPENDIO_SUJETO objEnt = new VW_COMPENDIO_SUJETO();
            return _fuenteDatosService.listar(objEnt);

        }
        [Route("registrarFuente", Name = "Registro de Fuente de Datos")]
        [HttpPost]
        //[ResponseType(typeof(actualizarFuente))]
        public int registrarFuente(FuenteDatosRq model)
        {
            FuenteDatosTablaAnalisisRq objEnt = new FuenteDatosTablaAnalisisRq();
            return _fuenteDatosService.registrar(objEnt);

        }
        [Route("actualizarFuente", Name = "Actualizar Fuente de Datos")]
        [HttpPost]
        //[ResponseType(typeof(actualizarFuente))]
        public int actualizarFuente(FuenteDatosRq model)
        {
            FuenteDatosTablaAnalisisRq objEnt = new FuenteDatosTablaAnalisisRq();
            return _fuenteDatosService.registrar(objEnt);

        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }


      
    }
}

/*
 *  
 *    [Route("listar/{sujeto_riesgo}/{id_parametro}/{tipo_tabla}/{origen_fuente}/{nombre_tabla}/{nombre_campo}", Name = "listar")]
        [HttpGet]
        public IEnumerable<VW_COMPENDIO_SUJETO> listar(int sujeto_riesgo, int id_parametro, int tipo_tabla, int origen_fuente, string nombre_tabla, string nombre_campo)
        {
            VW_COMPENDIO_SUJETO objEnt = new VW_COMPENDIO_SUJETO();
            return _fuenteDatosService.listar(objEnt);
            
        } 
 * */
