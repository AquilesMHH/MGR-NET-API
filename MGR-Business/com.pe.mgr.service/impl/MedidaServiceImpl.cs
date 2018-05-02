using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Persistence.com.pe.mgr.dao.impl;
using MGR_Common.com.pe.mgr.common.constants;
using MGR_Common.com.pe.mgr.common.util;

namespace MGR_Business.com.pe.mgr.service.impl
{
    public class MedidaServiceImpl : MedidaService
    {
        private MedidaDaoImpl _medidaDaoImpl;
        private OperacionesMedidasDaoImpl _operacionesMedidasDaoImpl;
        public MedidaServiceImpl()
        {
            _medidaDaoImpl = new MedidaDaoImpl();
            _operacionesMedidasDaoImpl = new OperacionesMedidasDaoImpl();
        }

        public List<Row> listarFormulaItems(int elemento, int sujetoRiesgo, string tipoMedida, int linea, string funcionalidad)
        {
            return _medidaDaoImpl.listarFormulaItems(elemento, sujetoRiesgo, tipoMedida, linea, funcionalidad);
        }

        public string mplementarMedidaMultipleDao(string  metodo, int idSession, List<MedidaRevImpRq> lstMedidaRevImpRq)
        {
            try
            {
                bool revisar = false;

                EnumTipoOperacion tipoOperacion = 0;

                StringBuilder sbRespuesta = new StringBuilder();

                int i = 0;

                foreach (MedidaRevImpRq medidaRevImpRq in lstMedidaRevImpRq)
                {
                    if (i == 0)
                    {
                        if (metodo.Equals("revisar"))
                        {
                            if (medidaRevImpRq.CONFORME)
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Revisada Conforme");
                            }
                            else
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Revisada No Conforme");
                            }
                        }
                        else if (metodo.Equals("implementar"))
                        {
                            if (medidaRevImpRq.APROBADA)
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Aprobada(s)");
                            }
                            else
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Rechazada(s)");
                            }
                        }
                    }

                    if (metodo.Equals("revisar"))
                    {
                        revisar = true;

                        tipoOperacion = EnumTipoOperacion.REVISION;

                        if (medidaRevImpRq.CONFORME)
                        {
                            medidaRevImpRq.ESTADO=40; // Revision Conforme
                        }
                        else
                        {
                            medidaRevImpRq.ESTADO = 41 ; // Revision No Conforme
                        }
                    }
                    else if (metodo.Equals("implementar"))
                    {
                        revisar = false;

                        tipoOperacion = EnumTipoOperacion.APROBACION;

                        if (medidaRevImpRq.APROBADA)
                        {
                            medidaRevImpRq.ESTADO=42; // Aprobada
                        }
                        else
                        {
                            medidaRevImpRq.ESTADO=43; // Rechazada
                        }
                    }

                    medidaRevImpRq.IDSESSION=idSession;

                    if ((medidaRevImpRq.FECHAINICIOVIGENCIA  != null) && (!string.IsNullOrEmpty(medidaRevImpRq.FECHAINICIOVIGENCIA)))
                    {
                       /* Date fechaInicioVigencia = this.serviceUtil.parseDateWithInit(medidaRevImpRq.getFechaInicioVigencia());
                        Date fechaActual = this.serviceUtil.dateNowWithoutTime();

                        if (fechaInicioVigencia.compareTo(fechaActual) == 0)
                        {
                            fechaInicioVigencia = this.serviceUtil.dateNow();
                        }

                        medidaRevImpRq.setDatFechaInicioVigencia(fechaInicioVigencia);*/
                    }

                   // medidaRevImpRq.setDatFechaFinVigencia(this.serviceUtil.parseDateWithFinish(medidaRevImpRq.getFechaFinVigencia()));

                   // this.medidasDAO.updateRevImp(revisar, medidaRevImpRq);

                   // this.insertarOperacionMedida(medidaRevImpRq.getId_medida(), medidaRevImpRq.getVersion_medida(),
                     //       medidaRevImpRq.getComentarios(), idSession, tipoOperacion.getCodigo());

                    i++;
                }

               return sbRespuesta.ToString() ;
            }
            catch (Exception e)
            {
               // this.logger.error(e.getMessage(), e);

               // throw new MgrServiceException(e.getErrorCode(), "Error al ".concat(metodo).concat(" la(s) regla(s)"));
            }
            return null;
             
        }
        public List<Row> listarFormulaItemsConCategoria(int elemento, int sujetoRiesgo, string tipoMedida, int linea, string funcionalidad)
        {
            return _medidaDaoImpl.listarFormulaItemsConCategoria(elemento, sujetoRiesgo, tipoMedida, linea, funcionalidad);
        }
     
        public List<Row> listarFormulaOpciones(int elemento, int itemElemento, int sujetoRiesgo, String tipoMedida, int linea, String funcionalidad)
        {
            return _medidaDaoImpl.listarFormulaOpciones(elemento, itemElemento, sujetoRiesgo, tipoMedida, linea, funcionalidad);
        }

        public List<Row> listarFormulaOpcionesConCategoria(int categoria, int elemento, int itemElemento, int sujetoRiesgo, string tipoMedida, int linea, string funcionalidad, string cadena)
        {
            return _medidaDaoImpl.listarFormulaOpcionesConCategoria(categoria,elemento, itemElemento, sujetoRiesgo, tipoMedida, linea, funcionalidad, cadena);
        }
        public List<Row> listarValorOperador(int categoria, int operador, int tipo_valor, int sujeto_riesgo, int variable)
        {
            return _medidaDaoImpl.listarValorOperador(categoria, operador, tipo_valor, sujeto_riesgo, variable);
        }

        public List<ComboBoxDto> obtenerTipoRespValores(int sujeto, int tipoRespuesta)
        {
            return _medidaDaoImpl.obtenerTipoRespValores(sujeto, tipoRespuesta);
        }

        public List<ComboBoxDto> obtenerVariablePorOrigen(int sujetoRiesgo, int origen)
        {
            return _medidaDaoImpl.obtenerVariablePorOrigen(sujetoRiesgo, origen);
        }

        public List<ComboBoxDto> obtenerValorVariable(int origen, int id_detalle)
        {
            return _medidaDaoImpl.obtenerValorVariable(origen, id_detalle);
        }

        public List<ComboBoxDto> obtenerUnidadMedicion(int id_grupo)
        {
            return _medidaDaoImpl.obtenerUnidadMedicion(id_grupo);
        }
        public string revisarImplementarMedidaMultipleDao(string metodo, int idSession, List<MedidaRevImpRq> lstMedidaRevRq)
        {
            try
            {
                bool revisar = false;

                EnumTipoOperacion tipoOperacion = 0;

                StringBuilder sbRespuesta = new StringBuilder();

                int i = 0;

                foreach (MedidaRevImpRq medidaRevImpRq in lstMedidaRevRq)
                {
                    if (i == 0)
                    {
                        if (metodo.Equals("revisar"))
                        {
                            if (medidaRevImpRq.CONFORME)
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Revisada Conforme");
                            }
                            else
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Revisada No Conforme");
                            }
                        }
                        else if (metodo.Equals("implementar"))
                        {
                            if (medidaRevImpRq.APROBADA)
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Aprobada(s)");
                            }
                            else
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Rechazada(s)");
                            }
                        }
                    }

                    if (metodo.Equals("revisar"))
                    {
                        revisar = true;

                        tipoOperacion = EnumTipoOperacion.REVISION;

                        if (medidaRevImpRq.CONFORME)
                        {
                            medidaRevImpRq.ESTADO = 40; // Revision Conforme
                        }
                        else
                        {
                            medidaRevImpRq.ESTADO = 41; // Revision No Conforme
                        }
                    }
                    else if (metodo.Equals("implementar"))
                    {
                        revisar = false;

                        tipoOperacion = EnumTipoOperacion.APROBACION;

                        if (medidaRevImpRq.APROBADA)
                        {
                            medidaRevImpRq.ESTADO = 42;  // Aprobada
                        }
                        else
                        {
                            medidaRevImpRq.ESTADO = 43;// Rechazada
                        }
                    }

                    medidaRevImpRq.IDSESSION = idSession;

                    if (medidaRevImpRq.FECHAINICIOVIGENCIA != null && !medidaRevImpRq.FECHAINICIOVIGENCIA.Equals(""))
                    {
                        // Date fechaInicioVigencia = this.serviceUtil.parseDateWithInit(medidaRevImpRq.getFechaInicioVigencia());
                        DateTime fechaInicioVigencia = DateTime.Parse(medidaRevImpRq.FECHAINICIOVIGENCIA);
                        DateTime fechaActual = DateTime.Now.Date;

                        if (fechaInicioVigencia.CompareTo(fechaActual) == 0)
                        {
                            fechaInicioVigencia = DateTime.Now.Date;
                        }

                        medidaRevImpRq.FECHAINICIOVIGENCIA = fechaInicioVigencia.ToShortDateString();
                    }

                    // medidaRevImpRq.setDatFechaFinVigencia(this.serviceUtil.parseDateWithFinish(medidaRevImpRq.getFechaFinVigencia()));
                    //medidaRevImpRq.FECHAFINVIGENCIA = medidaRevImpRq.FECHAFINVIGENCIA;
                    _medidaDaoImpl.updateRevImp(revisar, medidaRevImpRq);
                    insertarOperacionMedida(medidaRevImpRq, tipoOperacion);


                    i++;
                }

                return sbRespuesta.ToString();
            }
            catch (Exception e)
            {
                //this.logger.error(e.getMessage(), e);

                // throw new MgrServiceException(e.getErrorCode(), "Error al ".concat(metodo).concat(" la(s) regla(s)"));
            }
            return null;
        }
        private void insertarOperacionMedida(MedidaRevImpRq obj, EnumTipoOperacion tipoOperacion)
        {
            OperacionesMedidas operacioneMedida = new OperacionesMedidas();
            operacioneMedida.TIPO_OPERACION = (int)tipoOperacion;
            operacioneMedida.ID_MEDIDA = obj.ID_MEDIDA;
            operacioneMedida.VERSION_MEDIDA = obj.VERSION_MEDIDA;
            operacioneMedida.TIPO_OPERACION = 0;
            operacioneMedida.COMENTARIO = obj.COMENTARIOS.ToUpper();
            operacioneMedida.FECHA_OPERACION = DateTime.Now;
            operacioneMedida.SESSION_OPERACION = obj.IDSESSION;
            _operacionesMedidasDaoImpl.saveOrUpdate(operacioneMedida, true);
        }

        public List<ComboBoxDto> obtenerMedidaPorTipo(int tipo_medida, int sujeto_riesgo)
        {
            return _medidaDaoImpl.obtenerMedidaPorTipo(tipo_medida, sujeto_riesgo);
        }

        public string consultarDescripcionMedPrec(int sujetoRiesgo, int tipoMedida, int idMedida, int versionMedida)
        {
            return _medidaDaoImpl.consultarDescripcionMedPrec(sujetoRiesgo, tipoMedida, idMedida, versionMedida);
        }

        public List<ComboBoxDto> consultarAutoComplete(string metodo, string termino, int sujetoRiesgo, int tipoMedida)
        {
            return _medidaDaoImpl.consultarAutoComplete(metodo, termino, sujetoRiesgo, tipoMedida);
        }
    }
}
