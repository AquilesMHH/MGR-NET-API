using MGR_Business.com.pe.mgr.service.impl;
using MGR_Common.com.pe.mgr.common.util;
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
        private MedidaServiceImpl _medidasServiceImpl;
        public MedidaController()
        {
            _medidasServiceImpl = new MedidaServiceImpl();
        }

        [Route("implementarmedidamultipledao", Name = "Vario Medidas")]
        [HttpPost]
        public String implementarMedidaMultipleDao(List<MedidaRevImpRq> lstMedidaRevImpRq) {

            return _medidasServiceImpl.mplementarMedidaMultipleDao("", 0, lstMedidaRevImpRq);
        }
        [Route("revisarmedidamultipledao", Name = "Revisar Medidas")]
        [HttpPost]
        public String revisarImplementarMedidaMultipleDao(List<MedidaRevImpRq> lstMedidaRevRq)
        {
            int idSession = 19;// controllerUtil.getSessionId();
            String resultado = _medidasServiceImpl.revisarImplementarMedidaMultipleDao("revisar", idSession, lstMedidaRevRq);
            return resultado;
        }
        [Route("formulaItems", Name = "Vario formulaItems")]
        [HttpGet]
        public List<Row> listarFormulaItems(int elemento, int sujetoRiesgo, string tipoMedida, int linea, string funcionalidad)
        {
            return _medidasServiceImpl.listarFormulaItems(elemento, sujetoRiesgo, tipoMedida, linea, funcionalidad);
        }

        [Route("formulaOpciones", Name = "Vario formulaOpciones")]
        [HttpGet]
        public List<Row> listarFormulaOpciones(int elemento, int itemElemento, int sujetoRiesgo, String tipoMedida, int linea, String funcionalidad)
        {
            return _medidasServiceImpl.listarFormulaOpciones(elemento, itemElemento, sujetoRiesgo, tipoMedida, linea, funcionalidad);
        }     

        [Route("formulaopcionesconcategoria", Name = "Vario formulaopcionesconcategoria")]
        [HttpGet]
        public List<Row> listarFormulaOpcionesConCategoria(int categoria, int elemento, int itemElemento, int sujetoRiesgo, String tipoMedida, int linea, String funcionalidad, String cadena)
        {
            return _medidasServiceImpl.listarFormulaOpcionesConCategoria(categoria, elemento, itemElemento, sujetoRiesgo, tipoMedida, linea, funcionalidad, cadena);
        }

        [Route("valoroperador", Name = "Vario valoroperador")]
        [HttpGet]
        public List<Row> listarValorOperador(int categoria, int operador, int tipo_valor, int sujeto_riesgo,   int variable )
        {
            return   _medidasServiceImpl.listarValorOperador(categoria, operador, tipo_valor, sujeto_riesgo, variable);
        }

        [Route("obtenerTipoRespValores", Name = "Vario obtenerTipoRespValores")]
        [HttpGet]
        public List<ComboBoxDto> obtenerTipoRespValores(int sujetoRiesgo, int tipoRespuesta, int consecutivo, int linea, int salida)
        {
            return _medidasServiceImpl.obtenerTipoRespValores(sujetoRiesgo, tipoRespuesta) ;
        }

        [Route("variablePorOrigen", Name = "Vario variablePorOrigen")]
        [HttpGet]
        public List<ComboBoxDto> obtenerVariablePorOrigen(int sujetoRiesgo, int origen)
        { 
            return  _medidasServiceImpl.obtenerVariablePorOrigen(sujetoRiesgo, origen);
        }
        [Route("unidadmedicion", Name = "Vario unidadmedicion")]
        [HttpGet]
        public List<ComboBoxDto> obtenerUnidadMedicion(int tipo_medicion )
        {
            return   _medidasServiceImpl.obtenerUnidadMedicion(tipo_medicion);
        }

        [Route("consultarDescripcionMedPrec", Name = "Vario consultarDescripcionMedPrec")]
        [HttpGet]
        public String consultarDescripcionMedPrec(int sujetoRiesgo, int tipoMedida, int idMedida, int versionMedida)
        {
            return _medidasServiceImpl.consultarDescripcionMedPrec(sujetoRiesgo, tipoMedida, idMedida, versionMedida);
        }
        [Route("autoComplete/destinatario", Name = "Vario destinatario")]
        [HttpGet]
        public List<ComboBoxDto> consultarAutoCompleteDatos(String termino)
        {   return _medidasServiceImpl.consultarAutoComplete("destinatario", termino, 0, 0);
        }
        [Route("guardarmedidadao", Name = "Vario guardarmedidadao")]
        [HttpPost]
        public String guardarMedidaDao(MedidaRq medidaRq)
        {
            int idSession = 0;
            return _medidasServiceImpl.guardarRetenerMedidaDao("guardar", idSession, medidaRq);
        }
        [Route("implementarmedidadao", Name = "Vario implementarmedidadao")]
        [HttpPost]
        public String implementarMedidaDao(MedidaRq medidaRq)
        {
            int idSession = 0;
            return _medidasServiceImpl.guardarRetenerMedidaDao("implementar", idSession, medidaRq);
        }

        [Route("devolvermedidadao", Name = "Vario devolvermedidadao")]
        [HttpPost]
        public String devolvermedidadao(int id_medida, int version_medida, int estado, string comentarios)
        {
            int idSession = 0;
            return _medidasServiceImpl.devolverMedidaDao( id_medida,  version_medida,  estado,  comentarios,idSession);
        }
    }
}
