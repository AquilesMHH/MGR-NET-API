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

namespace MGR_Persistence.com.pe.mgr.dao.impl
{ 
    public class MedidaDaoImpl : MedidaDao,  IDisposable
    {
        private ModelMGRContext context;
        public string conn = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public MedidaDaoImpl()
        {
            context = new ModelMGRContext();
        }

        public string implementarMedidaMultipleDao(String metodo, int idSession,  List<MedidaRevImpRq> lstMedidaRevImpRq)
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
                if (ConstanteEnum.OPERADORES.Equals(elemento)){
                    sql = MgrEnumConsultaGeneral.MgrOperadorMatematicoComboFC(elemento);
                }
                if (ConstanteEnum.FUNCIONES.Equals(elemento)){
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

                        foreach (OperadorMatematico objEntity in objListar) {
                            if (tipo_valor.Equals(-1))
                            {
                                indicador = objEntity.INDICADOR_SIN_VALOR==1?true:false;
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
                throw new MgrServiceException("","error");
            }
        }

        public List<ComboBoxDto> obtenerTipoRespValores(int sujeto, int tipoRespuesta)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                String sql = "";
                try
                {

                 if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_SELECTIVIDAD_REVISION)) {
                   sql = MgrEnumConsultaGeneral.MgrTipoRespCanalSelectividadCombo(sujeto);
                  }
            
                if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_MENSAJE_VALIDACION)) {
                   sql = MgrEnumConsultaGeneral.MgrTipoRespCodigoValidacionCombo(sujeto); 
                  }
                if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_VALOR)) { //VALOR
                    sql = MgrEnumConsultaGeneral.MgrTipoRespValorCombo(0); 
                }
                if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_LISTA_VALORES)) { //LISTA VALOR
                    sql = MgrEnumConsultaGeneral.MgrCompendioDetalleCombo(104); 
                      
                }
                if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_MEDICION)) { //MEDICION
                   sql = MgrEnumConsultaGeneral.MgrCompendioSujetoRiesgoCombo(105, sujeto);
                }
            
                if (tipoRespuesta.Equals(Constantes.MEDIDA_TIPO_RESPUESTA_REGLA_NEGOCIO)) { //REGLA NEGOCIO
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
    }    
}
