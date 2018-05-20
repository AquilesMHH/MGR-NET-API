using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Model;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using MGR_Common.com.pe.mgr.common.util;
using MGR_Common.com.pe.mgr.common.constants;
using MGR_Persistence.com.pe.mgr.dao.SqlString;
using System.Collections;

namespace MGR_Persistence.com.pe.mgr.dao.impl
{
    public class MedidaDaoImpl : MedidaDao, IDisposable
    {
        private ModelMGRContext context;
        private OperacionesMedidasDaoImpl adoOperacionesMed;
        public string conn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public MedidaDaoImpl()
        {
            adoOperacionesMed = new OperacionesMedidasDaoImpl();
            context = new ModelMGRContext();
        }

        public string implementarMedidaMultipleDao(String metodo, int idSession, List<MedidaRevImpRq> lstMedidaRevImpRq)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                }
                catch (Exception ext)
                {
                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }
                return null;
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string revisarImplementarMedidaMultipleDao(string metodo, int idSession, List<MedidaRevImpRq> lstMedidaRevRq)
        {
            return null;
        }

        public int[] batchUpdateRevImp(bool revisar, List<MedidaRevImpRq> lstMedidaRevImpRq)
        {


            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (MedidaRevImpRq obj in lstMedidaRevImpRq)
                    {

                    }
                }
                catch (Exception ext)
                {
                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }
                return null;
            }
        }

        public int updateRevImp(bool revisar, MedidaRevImpRq medidaRevImpRq)
        {
            String sql = "";
            int cantidad = 0;
            if (revisar)
            {
                sql = "UPDATE GRTA_MEDIDAS SET " +
                          "ESTADO_MEDIDA  " + "" + medidaRevImpRq.ESTADO + "" + ", " +
                          "FECHA_REGISTRO = SYSDATE," +
                          "SESSION_REGISTRO " + "" + medidaRevImpRq.IDSESSION + "" + " " +
                          "WHERE ID_MEDIDA " + "" + medidaRevImpRq.ID_MEDIDA + "" + " " +
                          "AND VERSION_MEDIDA " + "" + medidaRevImpRq.VERSION_MEDIDA + "" + " ";
            }
            else
            {
                sql = "UPDATE GRTA_MEDIDAS SET " +
                          "ESTADO_MEDIDA =  " + "" + medidaRevImpRq.ESTADO + "" + ", " +
                          "FECHA_REGISTRO = SYSDATE," +
                          "FECHA_INICIO_VIGENCIA " + "" + medidaRevImpRq.FECHAINICIOVIGENCIA + "" + " ," +
                          "FECHA_FIN_VIGENCIA " + "" + medidaRevImpRq.FECHAFINVIGENCIA + "" + " ," +
                          "SESSION_REGISTRO " + "" + medidaRevImpRq.IDSESSION + "" + " " +
                          "WHERE ID_MEDIDA = ? " +
                          "AND VERSION_MEDIDA = ?";
            }
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    cantidad = MGR_Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, null);
                }
                catch (Exception ext)
                {
                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }

            }

            return cantidad;
        }

        public List<Row> listarFormulaItems(int elemento, int sujetoRiesgo, string tipoMedida, int linea, string funcionalidad)
        {
            try
            {
                String sql = "";
                if (elemento.Equals(ConstanteEnum.FUENTE_DATOS))
                {
                    if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_EVALUACION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrFuenteDatosProcesoEvaluacionCombo(sujetoRiesgo);
                    }
                    else if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_FISCALIZACION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrFuenteDatosProgramaFizcalizacionCombo(sujetoRiesgo);
                    }
                    else if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_BENFORD))
                    {
                        sql = MgrEnumConsultaGeneral.MgrFuenteDatosrBenfordCombo(sujetoRiesgo);
                    }
                    else
                    {
                        //Si el tipo de medida es (Modelo Probabilistico (PR) o Red Neuronal) y Filtro es el General de la Medida (linea condiciones es igual 0 (cero).
                        if ((tipoMedida.Equals(Constantes.TIPO_MEDIDA_MODELO_PROBABILISTICO) || tipoMedida.Equals(Constantes.TIPO_MEDIDA_RED_NEURONAL)) && linea == 0)
                        {
                            sql = MgrEnumConsultaGeneral.MgrFuenteDatos1821767Combo(sujetoRiesgo);
                        } //Si el tipo de medida es criterio experto o método de excepción y se trata del filtro general:
                        else if ((tipoMedida.Equals(Constantes.TIPO_MEDIDA_CRITERIO_EXPERTO) || tipoMedida.Equals(Constantes.TIPO_MEDIDA_METODO_EXCEPCION)) && linea == 0)
                        {
                            sql = MgrEnumConsultaGeneral.MgrFuenteDatos1719Combo(sujetoRiesgo);
                        }
                        else
                        {
                            sql = MgrEnumConsultaGeneral.MgrFuenteDatosLineaDiferenteCeroCombo(sujetoRiesgo);
                        }
                    }


                }
                else if (elemento.Equals(ConstanteEnum.FUENTE_DATOS))
                {
                    sql = MgrEnumConsultaGeneral.MgrTipoOperadorCombo;
                }
                else if (elemento.Equals(ConstanteEnum.FUENTE_DATOS))
                {
                    sql = MgrEnumConsultaGeneral.MgrGrupoFuncionCombo(0);
                }
                Row objRow = new Row();
                DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                objRow.Add("valor", dataSet);
                List<Row> obj = new List<Row>();
                obj.Add(objRow);
                return obj;
            }
            catch (Exception e)
            {
                return null;
                // throw new MgrServiceException(e.getErrorCode(), "Error al consultar los elementos");
            }
        }
        public List<Row> listarFormulaItemsConCategoria(int elemento, int sujetoRiesgo, string tipoMedida, int linea, string funcionalidad)
        {

            try
            {
                String sql = "";
                // DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrCompendioDetalleComboReferencia, System.Data.CommandType.Text, parameters);
                if (ConstanteEnum.FUENTE_DATOS.Equals(elemento))
                {
                    if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_EVALUACION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrFuenteDatosProcesoEvaluacionCombo(sujetoRiesgo);
                    }
                    else if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_FISCALIZACION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrFuenteDatosProgramaFizcalizacionCombo(sujetoRiesgo);
                    }
                    else if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_BENFORD))
                    {
                        sql = MgrEnumConsultaGeneral.MgrFuenteDatosrBenfordCombo(sujetoRiesgo);
                    }
                    else
                    {
                        //Si el tipo de medida es (Modelo Probabilistico (PR) o Red Neuronal) y Filtro es el General de la Medida (linea condiciones es igual 0 (cero).
                        if ((tipoMedida.Equals(Constantes.TIPO_MEDIDA_MODELO_PROBABILISTICO) || tipoMedida.Equals(Constantes.TIPO_MEDIDA_RED_NEURONAL)) && linea == 0)
                        {
                            sql = MgrEnumConsultaGeneral.MgrFuenteDatos1821767ConCategoriaCombo(sujetoRiesgo);
                        } //Si el tipo de medida es criterio experto o método de excepción y se trata del filtro general:
                        else if ((tipoMedida.Equals(Constantes.TIPO_MEDIDA_CRITERIO_EXPERTO) || tipoMedida.Equals(Constantes.TIPO_MEDIDA_METODO_EXCEPCION)) && linea == 0)
                        {
                            sql = MgrEnumConsultaGeneral.MgrFuenteDatos1719ConCategoriaCombo(sujetoRiesgo);
                        }
                        else
                        {
                            sql = MgrEnumConsultaGeneral.MgrFuenteDatosLineaDiferenteCeroConCategoriaCombo(sujetoRiesgo);
                        }
                    }
                }
                if (ConstanteEnum.OPERADORES.Equals(elemento))
                {
                    sql = MgrEnumConsultaGeneral.MgrOperadorMatematicoComboFC(elemento);
                }
                if (ConstanteEnum.FUNCIONES.Equals(elemento))
                {
                    sql = MgrEnumConsultaGeneral.MgrFuncionCombo(elemento);
                }
                Row objRow = new Row();
                DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                objRow.Add("valor", dataSet);
                List<Row> obj = new List<Row>();
                obj.Add(objRow);
                return obj;
            }
            catch (Exception e)
            {
                // throw new MgrServiceException(e.getErrorCode(), "Error al consultar los elementos");
                return null;
            }

        }

        public List<Row> listarFormulaOpciones(int elemento, int itemElemento, int sujetoRiesgo, string tipoMedida, int linea, string funcionalidad)
        {
            try
            {
                String sql = "";

                if (ConstanteEnum.FUENTE_DATOS.Equals(elemento))
                {
                    if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_EVALUACION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoExtraccionCombo(sujetoRiesgo, itemElemento);
                    }
                    else if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_FISCALIZACION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoExtraccionCombo2(sujetoRiesgo, itemElemento);
                    }
                    else if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_BENFORD))
                    {
                        sql = MgrEnumConsultaGeneral.MgrVariablesSujRiesgoConsolidacion(sujetoRiesgo, itemElemento);
                    }
                    else
                    {
                        //Si el tipo de medida es (Modelo Probabilistico (PR) o Red Neuronal) y Filtro es el General de la Medida (linea condiciones es igual 0 (cero).
                        if ((tipoMedida.Equals(Constantes.TIPO_MEDIDA_MODELO_PROBABILISTICO) || tipoMedida.Equals(Constantes.TIPO_MEDIDA_RED_NEURONAL)) && linea == 0)
                        {
                            sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoCombo2(sujetoRiesgo, itemElemento);
                        } //Si el tipo de medida es criterio experto o método de excepción y se trata del filtro general:
                        else if ((tipoMedida.Equals(Constantes.TIPO_MEDIDA_CRITERIO_EXPERTO) || tipoMedida.Equals(Constantes.TIPO_MEDIDA_METODO_EXCEPCION)) && linea == 0)
                        {
                            sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoCombo3(sujetoRiesgo, itemElemento);
                        }
                        else
                        {
                            sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoCombo(sujetoRiesgo, itemElemento);
                        }
                    }
                }

                if (ConstanteEnum.OPERADORES.Equals(elemento))
                {
                    sql = MgrEnumConsultaGeneral.MgrOperadorMatematicoComboFC(elemento);
                }
                if (ConstanteEnum.FUNCIONES.Equals(elemento))
                {
                    sql = MgrEnumConsultaGeneral.MgrFuncionCombo(elemento);
                }
                Row objRow = new Row();
                DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                objRow.Add("valor", dataSet);
                List<Row> obj = new List<Row>();
                obj.Add(objRow);
                return obj;
            }
            catch (Exception e)
            {
                // throw new MgrServiceException(e.getErrorCode(), "Error al consultar los elementos");
                return null;
            }
        }

        public List<Row> listarFormulaOpcionesConCategoria(int categoria, int elemento, int itemElemento, int sujetoRiesgo, string tipoMedida, int linea, string funcionalidad, string cadena)
        {
            try
            {
                String sql = "";

                if (ConstanteEnum.FUENTE_DATOS.Equals(elemento))
                {
                    if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_EVALUACION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoExtraccionCombo(sujetoRiesgo, itemElemento);
                    }
                    else if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_FISCALIZACION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoExtraccionCombo2(sujetoRiesgo, itemElemento);
                    }
                    else if (funcionalidad.Equals(Constantes.MEDIDA_FUNCIONALIDAD_BENFORD))
                    {
                        sql = MgrEnumConsultaGeneral.MgrVariablesSujRiesgoConsolidacion(sujetoRiesgo, itemElemento);
                    }
                    else
                    {
                        //Si el tipo de medida es (Modelo Probabilistico (PR) o Red Neuronal) y Filtro es el General de la Medida (linea condiciones es igual 0 (cero).
                        if ((tipoMedida.Equals(Constantes.TIPO_MEDIDA_MODELO_PROBABILISTICO) || tipoMedida.Equals(Constantes.TIPO_MEDIDA_RED_NEURONAL)) && linea == 0)
                        {
                            sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoCombo2(sujetoRiesgo, itemElemento);
                        } //Si el tipo de medida es criterio experto o método de excepción y se trata del filtro general:
                        else if ((tipoMedida.Equals(Constantes.TIPO_MEDIDA_CRITERIO_EXPERTO) || tipoMedida.Equals(Constantes.TIPO_MEDIDA_METODO_EXCEPCION)) && linea == 0)
                        {
                            sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoCombo3(sujetoRiesgo, itemElemento);
                        }
                        else
                        {
                            sql = MgrEnumConsultaGeneral.MgrVariablesSujetoRiesgoCombo(sujetoRiesgo, itemElemento);
                        }
                    }
                }

                if (ConstanteEnum.OPERADORES.Equals(elemento))
                {
                    sql = MgrEnumConsultaGeneral.MgrOperadorMatematicoComboFC(elemento);
                }
                if (ConstanteEnum.FUNCIONES.Equals(elemento))
                {
                    sql = MgrEnumConsultaGeneral.MgrFuncionCombo(elemento);
                }
                Row objRow = new Row();
                DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                objRow.Add("valor", dataSet);
                List<Row> obj = new List<Row>();
                obj.Add(objRow);
                return obj;
            }
            catch (Exception e)
            {
                // throw new MgrServiceException(e.getErrorCode(), "Error al consultar los elementos");
                return null;
            }
        }

        public List<Row> listarValorOperador(int categoria, int operador, int tipo_valor, int sujeto_riesgo, int variable)
        {
            try
            {
                if (operador != null)
                {
                    if (tipo_valor.Equals(-1) || tipo_valor.Equals(45) || tipo_valor.Equals(46) || tipo_valor.Equals(47))
                    {
                        bool indicador = false;
                        Row objRow = new Row();
                        DataSet dsFuncionalidad = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrIndicadoresOperadorMatematico(operador), System.Data.CommandType.Text, null);
                        List<OperadorMatematico> objListar = new List<OperadorMatematico>();
                        if (dsFuncionalidad != null)
                        {
                            objListar = dsFuncionalidad.Tables[0].DataTableToList<OperadorMatematico>();
                        }

                        foreach (OperadorMatematico objEntity in objListar)
                        {
                            if (tipo_valor.Equals(-1))
                            {
                                indicador = objEntity.INDICADOR_SIN_VALOR == 1 ? true : false;
                            }
                            else if (tipo_valor.Equals(45))
                            {
                                indicador = objEntity.INDICADOR_SUMINISTRADO == 1 ? true : false;
                            }
                            else if (tipo_valor.Equals(46))
                            {
                                indicador = objEntity.INDICADOR_LISTA == 1 ? true : false;
                            }
                            else if (tipo_valor.Equals(47))
                            {
                                indicador = objEntity.INDICADOR_PARAMETRO == 1 ? true : false;
                            }
                        }


                        if (indicador)
                        {
                            if (tipo_valor.Equals(-1))
                            {
                                return new List<Row>(); //sin valor
                            }
                            else if (tipo_valor.Equals(45))
                            {
                                return new List<Row>();  //valor suministrado
                            }
                            else if (tipo_valor.Equals(46))
                            { //valor lista
                                if (variable > 0)
                                {
                                    DataSet dsIndicador = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrTipoValorListaConCategoria(sujeto_riesgo, variable), System.Data.CommandType.Text, null);
                                    objRow.Add("valor-1", dsIndicador);
                                    List<Row> obj = new List<Row>();
                                    obj.Add(objRow);
                                    return obj;
                                }
                                else

                                {
                                    throw new MgrServiceException(ErrorCodeConstant.ESQ_00000, "Variable requerida");
                                }
                            }
                            else if (tipo_valor.Equals(47))
                            { //valor parametro
                                DataSet dsIndicador = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrTipoValorParametroConCategoria(categoria, sujeto_riesgo), System.Data.CommandType.Text, null);
                                objRow.Add("valor-2", dsIndicador);
                                List<Row> obj = new List<Row>();
                                obj.Add(objRow);
                                return obj;
                            }

                            return new List<Row>();
                        }
                        else
                        {
                            if (objListar.Count() != 0)
                            {
                                throw new MgrServiceException(ErrorCodeConstant.ESQ_00000, "Tipo de valor no permitido para el operador");
                            }
                            else
                            {
                                throw new MgrServiceException(ErrorCodeConstant.ESQ_00000, "Operador no existe");
                            }
                        }
                    }
                    else
                    {
                        throw new MgrServiceException(ErrorCodeConstant.ESQ_00000, "El tipo de valor no definido");
                    }
                }
                else
                {
                    throw new MgrServiceException(ErrorCodeConstant.ESQ_00000, "Valor Operador necesario");
                }
            }
            catch (Exception e)
            {
                throw new MgrServiceException("", "error");
            }
        }

        public List<ComboBoxDto> obtenerTipoRespValores(int sujeto, int tipoRespuesta)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                String sql = "";
                try
                {

                    if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_SELECTIVIDAD_REVISION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrTipoRespCanalSelectividadCombo(sujeto);
                    }

                    if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_MENSAJE_VALIDACION))
                    {
                        sql = MgrEnumConsultaGeneral.MgrTipoRespCodigoValidacionCombo(sujeto);
                    }
                    if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_VALOR))
                    { //VALOR
                        sql = MgrEnumConsultaGeneral.MgrTipoRespValorCombo(0);
                    }
                    if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_LISTA_VALORES))
                    { //LISTA VALOR
                        sql = MgrEnumConsultaGeneral.MgrCompendioDetalleCombo(104);

                    }
                    if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_MEDICION))
                    { //MEDICION
                        sql = MgrEnumConsultaGeneral.MgrCompendioSujetoRiesgoCombo(105, sujeto);
                    }

                    if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_REGLA_NEGOCIO))
                    { //REGLA NEGOCIO
                        sql = MgrEnumConsultaGeneral.MgrCompendioDetalleComboReferencia(7);
                    }

                    List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                    if (dataSet != null)
                    {
                        objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
                        return objLista;
                    }
                }
                catch (Exception ext)
                {

                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }
                return null;
            }
        }

        public List<ComboBoxDto> obtenerVariablePorOrigen(int sujetoRiesgo, int origen)
        {
            try
            {
                String sql = "";
                if (EnumOrigenVariableEnum.COMPENDIO_TABLAS.Equals(origen))
                {
                    sql = MgrEnumConsultaGeneral.MgrCompendioxTipoTablaCombo(1);
                }
                else if (EnumOrigenVariableEnum.VARIABLES.Equals(origen))
                {
                    sql = MgrEnumConsultaGeneral.MgrVariableExternaCombo(sujetoRiesgo);
                }
                List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                if (dataSet != null)
                {
                    objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
                    return objLista;
                }
            }
            catch (Exception ext)
            {

                string valor = ext.ToString();

            }
            return null;
        }

        public List<ComboBoxDto> obtenerValorVariable(int origen, int id_detalle)
        {
            try
            {
                String sql = "";
                if (EnumOrigenVariableEnum.COMPENDIO_TABLAS.Equals(origen))
                {
                    sql = MgrEnumConsultaGeneral.MgrCompendioDetalleCombo(id_detalle);
                }
                else if (EnumOrigenVariableEnum.VARIABLES.Equals(origen))
                {
                    sql = MgrEnumConsultaGeneral.MgrVariableExternaValorCombo(id_detalle);
                }
                List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                if (dataSet != null)
                {
                    objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
                    return objLista;
                }
            }
            catch (Exception ext)
            {

                string valor = ext.ToString();

            }
            return null;
        }

        public List<ComboBoxDto> obtenerUnidadMedicion(int id_grupo)
        {
            try
            {
                String sql = MgrEnumConsultaGeneral.MgrUnidadMedicionCombo(id_grupo);
                List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                if (dataSet != null)
                {
                    objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
                    return objLista;
                }
            }
            catch (Exception ext)
            {
                string valor = ext.ToString();
            }
            return null;
        }

        public List<ComboBoxDto> obtenerMedidaPorTipo(int tipo_medida, int sujeto_riesgo)
        {
            try
            {
                String sql = MgrEnumConsultaGeneral.MgrMedidaPorTipo(tipo_medida, sujeto_riesgo);
                List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                if (dataSet != null)
                {
                    objLista = dataSet.Tables[0].DataTableToList<ComboBoxDto>();
                    return objLista;
                }
            }
            catch (Exception ext)
            {
                string valor = ext.ToString();
            }
            return null;
        }

        public string consultarDescripcionMedPrec(int sujetoRiesgo, int tipoMedida, int idMedida, int versionMedida)
        {
            try
            {
                String pIdMedidaVersion = idMedida + "-" + versionMedida;
                String sql = MgrEnumConsultaGeneral.MgrConsultarDescripcionMedidaPrecedente(pIdMedidaVersion, sujetoRiesgo, tipoMedida);

                List<ComboBoxDto> objLista = new List<ComboBoxDto>();
                DataSet dataSet = MGR_Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
                if (dataSet != null)
                {
                    return dataSet.Tables[0].Rows[0]["NOMBRE_MEDIDA"].ToString();

                }
            }
            catch (Exception ext)
            {
                string valor = ext.ToString();
            }
            return null;

        }

        public List<ComboBoxDto> consultarAutoComplete(string metodo, string termino, int sujetoRiesgo, int tipoMedida)
        {
            try
            {

                return null;
            }
            catch (Exception ext)
            {
                string valor = ext.ToString();
                throw new MgrServiceException(ErrorCodeConstant.SERV_PARAMETROS_INCORRECTOS, "Parametro invalido para el metodo");
              
            }
            
        }

        public string guardarRetenerMedidaDao(string metodo, int idSession, MedidaRq medidaRq)
        {
            GRTA_FILTROS_MEDIDAS objfiltro = new GRTA_FILTROS_MEDIDAS();
            GRTA_MEDIDAS objMedida = new GRTA_MEDIDAS();
            objMedida.VERSION_MEDIDA =(byte) medidaRq.VERSION_MEDIDA;
            objMedida.FUNCION_ACTIVACION = medidaRq.FUNCIONACTIVACION;
            objMedida.ID_POLITICA =(short) medidaRq.POLITICA;
            // objMedida.SESSION_REGISTRO = medidaRq.SESSION_REGISTRO;
            // objMedida.SUJETO_RIESGO = (byte) medidaRq.SUJETORIESGO;
            // objMedida.JERARQUIA_MEDIDA = medidaRq.JERARQUIA;
            // objMedida.VERSION_PRECEDENTE = (byte) medidaRq.MEDIDAPRECEDENTEVERSION;
            // objMedida.FECHA_INICIO_VIGENCIA = DateTime.Parse(medidaRq.FECHAVIGENCIAINI);
            //objMedida.FLAG_REPLICACION = medidaRq.repli
            // objMedida.CLASE_MEDIDA = medidaRq.CLASEMEDIDA;
            ////objMedida.FECHA_FIN_VIGENCIA = DateTime.Parse(medidaRq.FECHAVIGENCIAFIN);
            //objMedida.TERMINO_MOMENTO = Decimal.Parse(medidaRq.TERMINOMOMENTO.Trim());
            // objMedida.TIPO_MEDIDA = medidaRq.TIPOMEDIDA;
            if (metodo.Equals("guardar")) {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int maxAge = context.GRTA_MEDIDAS.Select(p => p.ID_MEDIDA).DefaultIfEmpty(0).Max();
                        objMedida.ID_MEDIDA = maxAge + 1;
                        context.GRTA_MEDIDAS.Add(objMedida);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }
                }
            }
            if (metodo.Equals("retener"))
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int maxAge = context.GRTA_FILTROS_MEDIDAS.Select(p => p.FILTRO_MEDIDAS).DefaultIfEmpty(0).Max();
                        objfiltro.FILTRO_MEDIDAS = maxAge + 1;
                        context.GRTA_FILTROS_MEDIDAS.Add(objfiltro);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }
                }
            }
                return null;
        }

        public List<MedidaConsultaDto> getPorParametros(Hashtable mapValues)
        {
            //Hashtable mapValues = new Hashtable();
            //mapValues.Add("", "asdf");

            Dictionary<string, string> parans = new Dictionary<string, string>();
            StringBuilder sbSelect = new StringBuilder();
            StringBuilder sbFrom = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();

            String query = "";

            String[] arrayEstado = new String[0];

            int cantidadParametros = 0;

            sbSelect.Append("SELECT DISTINCT MEDIDAS.ID_MEDIDA ID_MEDIDA, MEDIDAS.VERSION_MEDIDA VERSION, MEDIDAS.TIPO_MEDIDA COD_TIPO_MEDIDA,\n");
            sbSelect.Append("COALESCE((SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE=MEDIDAS.TIPO_MEDIDA), 'Indefinido') TIPO_MEDIDA,\n");
            sbSelect.Append("MEDIDAS.NOMBRE_MEDIDA NOMBRE_MEDIDA, TO_CHAR(MEDIDAS.FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY HH24:MI') || ' - ' || COALESCE(TO_CHAR(MEDIDAS.FECHA_FIN_VIGENCIA,'DD/MM/YYYY HH24:MI'), 'Indefinido') Periodo,\n");
            sbSelect.Append("MEDIDAS.SUJETO_RIESGO COD_SUJETO_RIESGO, (SELECT UPPER(DESCRIPCION_BREVE) FROM GRTA_SUJETO_RIESGO WHERE SUJETO_RIESGO=MEDIDAS.SUJETO_RIESGO) SUJETO_RIESGO,\n");
            sbSelect.Append("MEDIDAS.ESTADO_MEDIDA COD_ESTADO_MEDIDA, COALESCE((SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE=MEDIDAS.ESTADO_MEDIDA), 'Indefinido') ESTADO_MEDIDA,\n");
            sbSelect.Append("(SELECT COUNT(0) FROM GRTA_CONDICION_MEDIDAS WHERE ID_MEDIDA=MEDIDAS.ID_MEDIDA AND VERSION_MEDIDA=MEDIDAS.VERSION_MEDIDA) CANTIDAD_CONDICIONES\n");
            sbFrom.Append("FROM GRTA_MEDIDAS MEDIDAS\n");
            sbWhere.Append("WHERE 1=1\n");

            
            foreach (DictionaryEntry entry in mapValues)
            {
                //Console.WriteLine("{0}, {1}", entry.Key, entry.Value);
                if ((entry.Key.Equals("tipo_medida")) && (!entry.Value.IsNullOrEmpty())) {
                    sbWhere.Append("AND MEDIDAS.TIPO_MEDIDA= " + entry.Value.ToString().Trim());
                }
                if ((entry.Key.Equals("id_medida")) && (!entry.Value.IsNullOrEmpty()))
                {
                    sbWhere.Append("AND MEDIDAS.ID_MEDIDA= " + entry.Value.ToString().Trim());
                }
                if ((entry.Key.Equals("version_medida")) && (!entry.Value.IsNullOrEmpty()))
                {
                    sbWhere.Append("AND MEDIDAS.VERSION_MEDIDA= " + entry.Value.ToString().Trim());
                }
                if ((entry.Key.Equals("estado_medida")) && (!entry.Value.IsNullOrEmpty()))
                {
                   // arrayEstado = param.get("estado_medida").toString().split(",");
                  //  String strParam = String.Join(",", Collections.nCopies(arrayEstado.Length, "?"));
                  //  sbWhere.Append("AND MEDIDAS.ESTADO_MEDIDA IN (").Append(strParam).Append(")\n");
                }
                if ((entry.Key.Equals("sujeto_riesgo")) && (!entry.Value.IsNullOrEmpty()))
                {
                    sbWhere.Append("AND MEDIDAS.SUJETO_RIESGO = " + entry.Value.ToString().Trim());
                }
                if ((entry.Key.Equals("politica")) && (!entry.Value.IsNullOrEmpty()))
                {
                    sbWhere.Append("AND MEDIDAS.ID_POLITICA = " + entry.Value.ToString().Trim());
                }
                if ((entry.Key.Equals("descripcion")) && (!entry.Value.IsNullOrEmpty()))
                {
                    sbWhere.Append("AND (MEDIDAS.NOMBRE_MEDIDA LIKE '%'||UPPER('"+ entry.Value.ToString().Trim() + "')||'%' OR MEDIDAS.DESCRIPCION LIKE '%'||UPPER('" + entry.Value.ToString().Trim() + "')||'%')\n");
                }
                if ((entry.Key.Equals("fecha_inicio_vigencia")) && (!entry.Value.IsNullOrEmpty()))
                {
                    sbWhere.Append("AND MEDIDAS.ESTADO_MEDIDA=42\n");
                    sbWhere.Append("AND (MEDIDAS.FECHA_FIN_VIGENCIA IS NULL OR TO_DATE(TO_CHAR(MEDIDAS.FECHA_FIN_VIGENCIA,'DD/MM/YYYY')) >= " + "'" + entry.Value.ToString().Trim() + "'");
                    sbWhere.Append("AND TO_DATE(TO_CHAR(MEDIDAS.FECHA_INICIO_VIGENCIA,'DD/MM/YYYY')) <='" + entry.Value.ToString().Trim() + "')");
                }
                if ((entry.Key.Equals("vigente")) && (entry.Value.Equals("1")))
                {
                    sbWhere.Append("AND SYSDATE BETWEEN MEDIDAS.FECHA_INICIO_VIGENCIA AND COALESCE(MEDIDAS.FECHA_FIN_VIGENCIA, SYSDATE)\n");
                    sbWhere.Append("AND MEDIDAS.ESTADO_MEDIDA=42\n");
                }
                else {
                    sbWhere.Append("AND MEDIDAS.ESTADO_MEDIDA=42 AND MEDIDAS.FECHA_FIN_VIGENCIA IS NOT NULL\n");
                    sbWhere.Append("AND SYSDATE > COALESCE(MEDIDAS.FECHA_FIN_VIGENCIA, SYSDATE)\n");
                }
                if ((entry.Key.Equals("variable")) && (!entry.Value.IsNullOrEmpty()))
                {
                    sbFrom.Append(", GRTA_FILTROS_MEDIDAS FILTROS_MEDIDAS, GRTA_FILTROS FILTROS\n");
                    sbWhere.Append("AND FILTROS_MEDIDAS.ID_MEDIDA=MEDIDAS.ID_MEDIDA\n");
                    sbWhere.Append("AND FILTROS_MEDIDAS.VERSION_MEDIDA=MEDIDAS.VERSION_MEDIDA\n");
                    sbWhere.Append("AND FILTROS.ID_FILTROS=FILTROS_MEDIDAS.ID_FILTROS\n");
                    sbWhere.Append("AND FILTROS.EXPRESION_FILTRO LIKE '%'||'" + entry.Value.ToString().Trim() + "'||'%'\n");
                    if ((entry.Key.Equals("valor_variable")) && (!entry.Value.IsNullOrEmpty()))
                    {
                        sbWhere.Append("AND FILTROS.EXPRESION_FILTRO LIKE '%'||'" + entry.Value.ToString().Trim() + "'||'%'\n");
                    }
                }
                sbOrder.Append("ORDER BY MEDIDAS.ID_MEDIDA DESC, MEDIDAS.VERSION_MEDIDA DESC\n");
               //query = sbSelect.ToString().Concat(sbFrom.ToString()).Concat(sbWhere.ToString()).Concat(sbOrder.ToString());
                query = sbSelect.ToString() + " " + sbFrom.ToString() + " " + sbWhere.ToString() + " " + sbOrder.ToString();

               
            }
            List<MedidaConsultaDto> objLista = null;
            DataSet dataSet = MGR_Common.OracleHelper.Query(conn, query, System.Data.CommandType.Text, null);
            if (dataSet != null)
            {
                objLista = dataSet.Tables[0].DataTableToList<MedidaConsultaDto>();
                return objLista;
            }
            return null;
        }

        public string revisarImplementarMedidaDao(string metodo, int idSession, MedidaRevImpRq medidaRevImpRq)
        {
            bool revisar = false;
            int tipoOperacion =0;
            StringBuilder sbRespuesta = new StringBuilder();
            
            if (metodo.Equals("revisar")){
                revisar = true;
                tipoOperacion = (int) EnumTipoOperacion.REVISION;
                if (medidaRevImpRq.CONFORME)
                {
                    sbRespuesta.Append("La regla ha sido Revisada Conforme");
                    medidaRevImpRq.ESTADO =40; // Revision Conforme
                }
                else
                {
                    sbRespuesta.Append("La regla ha sido Revisada No Conforme");
                    medidaRevImpRq.ESTADO=41; // Revision No Conforme
                }
            }
            else if (metodo.Equals("implementar"))
            {
                revisar = false;
                tipoOperacion = (int) EnumTipoOperacion.APROBACION;
                if (medidaRevImpRq.CONFORME)
                {
                    sbRespuesta.Append("La regla ha sido Aprobada");
                    medidaRevImpRq.ESTADO =42; // Aprobada
                }
                else
                {
                    sbRespuesta.Append("La regla ha sido Rechazada");
                    medidaRevImpRq.ESTADO =43; // Rechazada
                }
            }
            medidaRevImpRq.IDSESSION =idSession;

            if (medidaRevImpRq.FECHAINICIOVIGENCIA.IsNullOrEmpty())
            {
                DateTime fechaInicioVigencia = DateTime.Parse(medidaRevImpRq.FECHAINICIOVIGENCIA);
                DateTime fechaActual = DateTime.Now;

                if (fechaInicioVigencia.CompareTo(fechaActual) == 0)
                {
                    fechaInicioVigencia = DateTime.Now;
                }

                medidaRevImpRq.FECHAINICIOVIGENCIA = fechaInicioVigencia.ToString();
            }

            medidaRevImpRq.DATFECHAFINVIGENCIA = DateTime.Parse(medidaRevImpRq.FECHAFINVIGENCIA);
            string sql = "";
            if (revisar)
            {
                sql = "UPDATE GRTA_MEDIDAS SET " +
                          "ESTADO_MEDIDA = " + medidaRevImpRq.ESTADO + "," +
                          "FECHA_REGISTRO = SYSDATE," +
                          "SESSION_REGISTRO = " + idSession + " " +
                          "WHERE ID_MEDIDA =  " + medidaRevImpRq.ID_MEDIDA + " " +
                          "AND VERSION_MEDIDA = " + medidaRevImpRq.VERSION_MEDIDA + "";
            }
            else
            {
                sql = "UPDATE GRTA_MEDIDAS SET " +
                           "ESTADO_MEDIDA = " + medidaRevImpRq.ESTADO + "," +
                          "FECHA_REGISTRO = SYSDATE," +
                          "FECHA_INICIO_VIGENCIA = " + medidaRevImpRq.DATFECHAINICIOVIGENCIA.ToString() + "," +
                          "FECHA_FIN_VIGENCIA = " + medidaRevImpRq.DATFECHAFINVIGENCIA.ToString() + "," +
                          "SESSION_REGISTRO = " + idSession + " " +
                          "WHERE ID_MEDIDA =  " + medidaRevImpRq.ID_MEDIDA + " " +
                          "AND VERSION_MEDIDA = " + medidaRevImpRq.VERSION_MEDIDA + "";
            }
            String sqlOpe = "";
            OperacionesMedidas objOperaMedida = new OperacionesMedidas();
            objOperaMedida.ID_MEDIDA = medidaRevImpRq.ID_MEDIDA;
            objOperaMedida.VERSION_MEDIDA = medidaRevImpRq.VERSION_MEDIDA;
            objOperaMedida.SESSION_OPERACION = idSession;
            objOperaMedida.TIPO_OPERACION = tipoOperacion;
            objOperaMedida.COMENTARIO = medidaRevImpRq.COMENTARIOS;
            objOperaMedida.FECHA_OPERACION = DateTime.Now;
            adoOperacionesMed.saveOrUpdate(objOperaMedida, true);
            return sbRespuesta.ToString();
        }
        public int updateEstado(int id_medida, int version_medida, int estado, int idSession)
        {
          string  sql = "UPDATE GRTA_MEDIDAS SET " +
                           "ESTADO_MEDIDA = " + estado + "," +
                           "FECHA_REGISTRO = SYSDATE," +
                           "SESSION_REGISTRO = " + idSession + " " +
                           "WHERE ID_MEDIDA =  " + id_medida + " " +
                           "AND VERSION_MEDIDA = " + version_medida + "";
            int cantidad = 0;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    cantidad = MGR_Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, null);
                }
                catch (Exception ext)
                {
                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }

            }
            return cantidad;
	}
    public string devolverMedidaDao(int id_medida, int version_medida, int estado, string comentarios, int idSession)
        {

            int nuevoEstado = 0;

            if (estado == 39)
            {// Estado Creada
                nuevoEstado = 44;
            }
            else if (estado == 40)
            {// Estado Rev. Conforme
                nuevoEstado = 39;
            }

            if (nuevoEstado == 0)
            {
                throw new MgrServiceException(ErrorCodeConstant.ESQ_00000, "Estado de medida para devolver no definido");
            }
             updateEstado(id_medida, version_medida, nuevoEstado, idSession);
            OperacionesMedidas objOperaMedida = new OperacionesMedidas();
            objOperaMedida.ID_MEDIDA = id_medida;
            objOperaMedida.VERSION_MEDIDA = version_medida;
            objOperaMedida.SESSION_OPERACION = idSession;
            objOperaMedida.TIPO_OPERACION = estado;
            objOperaMedida.COMENTARIO = comentarios;
            objOperaMedida.FECHA_OPERACION = DateTime.Now;
            adoOperacionesMed.saveOrUpdate(objOperaMedida, true);
            return "La regla ha sido devuelta";
          
        }
    }
}
