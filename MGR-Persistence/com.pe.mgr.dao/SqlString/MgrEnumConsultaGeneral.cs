using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Persistence.com.pe.mgr.dao.SqlString
{
    public static class MgrEnumConsultaGeneral
    {

        public static String MgrVariableCatalogo()
        {
            return @"SELECT CODIGO_VARIABLE codigo, DESCRIPCION_BREVE descripcion FROM GRTA_VARIABLES\n" +
                    " WHERE TIPO_VARIABLE=116 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)";
        }
        public static String MgrSujetoRiesgoCombo()
        {
            return @"SELECT SUJETO_RIESGO codigo, DESCRIPCION_BREVE descripcion FROM GRTA_SUJETO_RIESGO\n" +
                        " WHERE SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE) ORDER BY SUJETO_RIESGO ASC";
        }
        public static String MgrVerificaUsuario(GRTA_USUARIO obj) {
            return @"SELECT DISTINCT USUARIO.ID_USUARIO "
               + "FROM GRTA_USUARIO USUARIO LEFT JOIN GRTA_USUARIO_ROL USUARIO_ROL ON USUARIO.ID_USUARIO = USUARIO_ROL.ID_USUARIO AND USUARIO_ROL.ESTADO = 1 "
               + "INNER JOIN GRTA_ROL ROL ON ROL.ID_ROL = USUARIO_ROL.ID_ROL AND ROL.ESTADO = 1 "
               + "LEFT JOIN GRTA_USUARIO_FUNCIONALIDAD USUARIO_FUNCIONALIDAD ON USUARIO_FUNCIONALIDAD.ID_USUARIO = USUARIO.ID_USUARIO AND USUARIO_FUNCIONALIDAD.ESTADO = 1 "
               + "WHERE NVL2(USUARIO.VIGENCIA_CLAVE, USUARIO.VIGENCIA_CLAVE, SYSDATE) >= SYSDATE AND USUARIO.USUARIO = " + "'" + obj.USUARIO + "'"
               + "AND USUARIO.CLAVE = GRPK_OPERACIONES_COMUNES.GRFN_ENCRIPTAR(" + "'" + obj.USUARIO + "'" + ", " + "'" + obj.CLAVE + "'" + ") AND USUARIO.ESTADO = 1 ";
        }
         
        public static String MgrCategoriaParametrosCombo(int sujeto_riesgo)
        {
            return @"SELECT PARAMETROS.CLASE_PARAMETRO CODIGO, COMPENDIO.NOMBRE DESCRIPCION \n"
            + "FROM GRTA_PARAMETROS PARAMETROS, GRTA_COMPENDIO_DETALLE COMPENDIO \n"
            + "WHERE PARAMETROS.CLASE_PARAMETRO=COMPENDIO.ID_DETALLE \n"
            + "AND (PARAMETROS.SUJETO_RIESGO= " + sujeto_riesgo + " OR PARAMETROS.SUJETO_RIESGO IS NULL) \n"
            + "AND SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND COALESCE(COMPENDIO.FECHA_FIN_VIGENCIA,SYSDATE) \n"
            + "GROUP BY PARAMETROS.CLASE_PARAMETRO,COMPENDIO.NOMBRE";
        }
        public static String MgrMedidaPorTipo(int tipo_medida, int sujeto_riesgo)
        {
            return @"SELECT ID_MEDIDA||'-'||VERSION_MEDIDA CODIGO, ID_MEDIDA||'-'||VERSION_MEDIDA||': '||NOMBRE_MEDIDA DESCRIPCION \n" +
            "FROM GRTA_MEDIDAS \n" +
            "WHERE TIPO_MEDIDA = TO_NUMBER(" + tipo_medida + ") AND SUJETO_RIESGO = TO_NUMBER(" + sujeto_riesgo + ") \n" +
            "AND SYSDATE BETWEEN COALESCE(FECHA_INICIO_VIGENCIA, SYSDATE) AND COALESCE(FECHA_FIN_VIGENCIA, SYSDATE) \n" +
            "ORDER BY ID_MEDIDA, VERSION_MEDIDA";
        }
        public static String MgrTipoRespCanalSelectividadCombo(int sujeto_riesgo)
        {
            return @"SELECT ID_DETALLE CODIGO, NOMBRE DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=3 AND SUJETO_RIESGO=TO_NUMBER( " + sujeto_riesgo + ") AND CODIGO_ALTERNO IS NOT NULL AND" +
                 " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";
        }
        public static String MgrConsultarMedidasPrecedentesAutoComplete(int pSujetoRiesgo, int pTipoMedida, string pNombreMedida)
        {
            return @"SELECT ID_MEDIDA||'-'||VERSION_MEDIDA CODIGO, NOMBRE_MEDIDA DESCRIPCION FROM GRTA_MEDIDAS" +
                                                " WHERE UPPER(NOMBRE_MEDIDA) LIKE UPPER('%' ||'" + pNombreMedida + "' || '%')" +
                                                " AND SUJETO_RIESGO=TO_NUMBER(" + pSujetoRiesgo + ") AND TIPO_MEDIDA=TO_NUMBER(" + pTipoMedida + ") AND ESTADO_MEDIDA=42";
        }
        public static String MgrConsultarDestinatarioAutoComplete(int pSujetoRiesgo, int pTipoMedida, string pParametro)
        {
            return @"SELECT ID_DETALLE CODIGO, DESCRIPCION||' ('||NOMBRE||')' DESCRIPCION FROM GRTA_COMPENDIO_DETALLE" +
                                            " WHERE ( UPPER(DESCRIPCION) LIKE UPPER('%'||'" + pParametro + "'||'%') OR UPPER(NOMBRE) LIKE UPPER('%'||'" + pParametro + "'||'%') ) AND" +
                                            " ID_COMPENDIO=63 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";
        }

        public static String MgrConsultarDescripcionMedidaPrecedente(string pIdMedidaVersion,int pSujetoRiesgo, int pTipoMedida)
        {
            return @"SELECT NOMBRE_MEDIDA nombre_medida FROM GRTA_MEDIDAS WHERE  ID_MEDIDA||'-'||VERSION_MEDIDA = '" + pIdMedidaVersion + "' " +
                                            " AND SUJETO_RIESGO=TO_NUMBER( " + pSujetoRiesgo + ") AND TIPO_MEDIDA=TO_NUMBER( " + pTipoMedida + ") AND ESTADO_MEDIDA=42";
        }
        
        public static String MgrVariableExternaValorCombo(int variable)
        {
            return @"SELECT GRPK_OPERACIONES_COMUNES.GRFN_ELEMENTOS_CATALOGO (CODIGO_VARIABLE) valor \n" +
                "FROM GRTA_VARIABLES \n" +
                "WHERE CODIGO_VARIABLE=TO_NUMBER(" + variable + ")\n ";
        }
        public static String MgrVariableFiltroCombo(int id_Sujeto)
        {
            return @"SELECT CODIGO_VARIABLE, UPPER(DESCRIPCION_BREVE) DESCRIPCION_BREVE FROM GRTA_VARIABLES\n" +
                            " WHERE SUJETO_RIESGO = TO_NUMBER(" + id_Sujeto + ") AND TIPO_VARIABLE =57 AND\n" +
                            " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%'\n";
        }

        public static String MgrCompendioDetalleComboReferencia(int id_compendio)
        {
            return @"SELECT ID_DETALLE CODIGO, NOMBRE  DESCRIPCION FROM GRTA_COMPENDIO_DETALLE\n" +
            " WHERE ID_COMPENDIO=" + id_compendio + "  AND REFERENCIA1=1 AND \n" +
            " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE)";
        }
        public static String MgrTipoRespValorCombo(int sujeto_riesgo)
        {
            return @"SELECT ID_DETALLE CODIGO, NOMBRE DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=38 AND" +
                        " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";
        }
        public static String MgrTipoRespCodigoValidacionCombo(int sujeto_riesgo)
        {
            return @"SELECT ID_DETALLE CODIGO, DESCRIPCION  FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=100 AND SUJETO_RIESGO=TO_NUMBER(" + sujeto_riesgo + ") AND" +
                                        " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";
        }
        public static String MgrCompendioDetalleCombo(int id_compendio)
        {
            return @"SELECT ID_DETALLE CODIGO, NOMBRE DESCRIPCION  FROM GRTA_COMPENDIO_DETALLE\n" +
            " WHERE ID_COMPENDIO=" + id_compendio + " AND  \n" +
            " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE)";
        }
        public static String MgrUnidadMedicionCombo(int id_grupo)
        {
            return @"SELECT ID_SUBDETALLE codigo, NOMBRE descripcion \n" +
            "FROM GRTA_COMPENDIO_SUBDETALLE \n" +
            "WHERE ID_DETALLE_GRUPO=TO_NUMBER(" + id_grupo + ") AND \n" +
            "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE)";
        } 
        public static String MgrCompendioSujetoRiesgoCombo(int id_compendio, int sujero_riesgo)
        {
            return @"SELECT ID_DETALLE CODIGO , NOMBRE as DESCRIPCION \n" +
             "FROM GRTA_COMPENDIO_DETALLE \n" +
             "WHERE ID_COMPENDIO= " + id_compendio + " AND SUJETO_RIESGO= TO_NUMBER( " + sujero_riesgo + ") AND \n" +
             "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE)";
        }
        public static String MgrCompendioxTipoTablaCombo(int id_tabla)
        {
            return @"SELECT GEN.ID_COMPENDIO CODIGO,GEN.NOMBRE DESCRIPCION \n" +
                "FROM GRTA_COMPENDIO_GENERAL GEN \n" +
                "WHERE GEN.TIPO_TABLA=" + id_tabla + "";
        }
        public static String MgrVariableExternaCombo(int id_tabla)
        {
            return @"SELECT CODIGO_VARIABLE CODIGO, DESCRIPCION_BREVE DESCRIPCION \n" +
            "FROM GRTA_VARIABLES \n" +
            "WHERE SUJETO_RIESGO=TO_NUMBER(" + id_tabla + ") AND VARIABLE_CATALOGO IS NOT NULL \n" +
            "AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE)";
        } 
        public static String MgrCategoriaSimbolosCombo(int sujeto_riesgo)
        {
            return @"SELECT OPERADOR.TIPO_OPERADOR CODIGO, COMPENDIO.NOMBRE DESCRIPCION \n"
            + "FROM GRTA_OPERADOR_MATEMATICO OPERADOR,GRTA_COMPENDIO_DETALLE COMPENDIO \n"
            + "WHERE COMPENDIO.ID_DETALLE=OPERADOR.TIPO_OPERADOR \n"
            + "AND SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND COALESCE(COMPENDIO.FECHA_FIN_VIGENCIA, SYSDATE) \n"
            + "GROUP BY OPERADOR.TIPO_OPERADOR,COMPENDIO.NOMBRE";
        }
         
        public static String MgrCategoriaCategoriaVocabularioNegocioCombo(int sujeto_riesgo)
        {
            return @"SELECT COMPENDIO.ID_DETALLE codigo,COMPENDIO.NOMBRE descripcion \n"
            + "FROM GRTA_VARIABLES VARIABLES, GRTA_CATEGORIA_VARIABLES CATEGORIAS, GRTA_COMPENDIO_DETALLE COMPENDIO \n"
            + "WHERE VARIABLES.TIPO_VARIABLE=57 AND VARIABLES.SUJETO_RIESGO= " + sujeto_riesgo + " \n"
            + "AND SYSDATE BETWEEN VARIABLES.FECHA_INICIO_VIGENCIA AND COALESCE(VARIABLES.FECHA_FIN_VIGENCIA, SYSDATE) \n"
            + "AND CATEGORIAS.CODIGO_VARIABLE=VARIABLES.CODIGO_VARIABLE "
            + "AND SYSDATE BETWEEN CATEGORIAS.FECHA_INICIO_VIGENCIA AND COALESCE(CATEGORIAS.FECHA_FIN_VIGENCIA, SYSDATE) \n"
            + "AND COMPENDIO.ID_DETALLE=CATEGORIAS.CATEGORIA_DATO \n"
            + "GROUP BY COMPENDIO.ID_DETALLE,COMPENDIO.NOMBRE \n"
            + "ORDER BY COMPENDIO.NOMBRE";
        }
         
        public static String MgrIndicadoresOperadorMatematico(int operador)
        {
            return @"SELECT indicador_suministrado, indicador_parametro, indicador_lista, "+
                   " indicador_sin_valor FROM GRTA_OPERADOR_MATEMATICO  WHERE OPERADOR_MATEMATICO = TO_NUMBER(" + operador + ")";
        }
        public static String MgrTipoValorListaConCategoria(int operador, int variable)
        {
            return @"SELECT DETALLE.ID_DETALLE codigo, DETALLE.NOMBRE nombre, DETALLE.DESCRIPCION descripcion \n"
            + "FROM GRTA_COMPENDIO_GENERAL GENERAL, GRTA_COMPENDIO_DETALLE DETALLE, GRTA_VARIABLES VARIABLES \n"
            + "WHERE GENERAL.TIPO_TABLA = 3 AND DETALLE.ID_COMPENDIO = GENERAL.ID_COMPENDIO \n"
            + "AND DETALLE.SUJETO_RIESGO = " + operador + " \n"
            + "AND SYSDATE BETWEEN DETALLE.FECHA_INICIO_VIGENCIA AND COALESCE(DETALLE.FECHA_FIN_VIGENCIA, SYSDATE) \n"
            + "AND VARIABLES.CODIGO_VARIABLE = " + variable + " \n"
            + "AND (DETALLE.VARIABLE_CATALOGO = VARIABLES.VARIABLE_CATALOGO OR  DETALLE.VARIABLE_CATALOGO IS NULL) \n"
            + "ORDER BY DETALLE.NOMBRE\n";
        }
        
   
        public static String MgrOperadorMatematicoComboFC(int tipo_operador)
        {
            return @"SELECT OPERADOR_MATEMATICO codigo, SIMBOLO_USUARIO simboloUsuario, 0 cantidad \n" +
                "FROM GRTA_OPERADOR_MATEMATICO \n" +
                "WHERE TIPO_OPERADOR = " + tipo_operador + "";
        }
        public static String MgrFuncionCombo(int grupo_funcion)
        {
            return @"SELECT FUNCION funcion, SIMBOLO_USUARIO simboloUsuario \n" +
                    "FROM GRTA_FUNCION FUNCION \n" +
                    "WHERE FUNCION.GRUPO_FUNCION = " + grupo_funcion + "";
        }
        public static String MgrVariaciones(string nombreTabla, string claveRegistro)
        {
            return @"SELECT TO_CHAR(VARIACIONES.FECHA_INGRESO,'DD/MM/YYYY HH24:MI') \'fecha_ingreso\', \n" +
            "SESSIONES.CODIGO_USUARIO||'-'||(SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=63 AND UPPER(CODIGO_ALTERNO)=UPPER(SESSIONES.CODIGO_USUARIO)) \"nombre_usuario\", \n" +
            "VARIACIONES.NOMBRE_CAMPO \'nombre_campo\', DECODE(VARIACIONES.VALOR_ANTIGUO,' ','EN BLANCO',NULL,'EN BLANCO',SUBSTR(VARIACIONES.VALOR_ANTIGUO,1,15)) \"valor_antiguo\", SUBSTR(VARIACIONES.VALOR_NUEVO,1,15) \"valor_nuevo\" \n" +
            "FROM GRTA_VARIACIONES VARIACIONES, GRTA_SESSION SESSIONES\n" +
            "WHERE\n" +
            "VARIACIONES.NOMBRE_TABLA='"+ nombreTabla  + "' AND\n" +
            "VARIACIONES.CLAVE_REGISTRO='" + claveRegistro + "' AND\n" +
            "SESSIONES.ID_SESSION=VARIACIONES.ID_SESSION\n" +
            "ORDER BY VARIACIONES.FECHA_INGRESO DESC";
        }
    

        public static String MgrTipoValorParametroConCategoria(int categoria, int sujeto_riesgo)
        {
            return @"SELECT PARAMETROS.CLASE_PARAMETRO codigo_categoria, COMPENDIO.NOMBRE descripcion_categoria, \n"
            + "PARAMETROS.ID_PARAMETRO codigo, PARAMETROS.DESCRIPCION_BREVE descripcion \n"
            + "FROM GRTA_PARAMETROS PARAMETROS, GRTA_COMPENDIO_DETALLE COMPENDIO \n"
            + "WHERE PARAMETROS.CLASE_PARAMETRO=COMPENDIO.ID_DETALLE \n"
            + "AND (? IS NULL OR COMPENDIO.ID_DETALLE = ?) \n"
            + "AND (PARAMETROS.SUJETO_RIESGO=? OR PARAMETROS.SUJETO_RIESGO IS NULL) \n"
            + "AND SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND COALESCE(COMPENDIO.FECHA_FIN_VIGENCIA,SYSDATE)";
        }
     

        public static String MgrSujetoRiesgoUsuario(int sessionId) {
            return @"SELECT SUJ.SUJETO_RIESGO CODIGO, SUJ.DESCRIPCION_BREVE DESCRIPCION  " +
              "FROM GRTA_SUJETO_RIESGO SUJ, GRTA_SESSION SESION, GRTA_COMPENDIO_DETALLE COMPENDIO, GRTA_COMPENDIO_SUBDETALLE SUBDETALLE " +
              "WHERE  " +
              "SESION.ID_SESSION=TO_NUMBER(" + sessionId + ") AND " +
              "COMPENDIO.ID_COMPENDIO=63 AND " +
              "COMPENDIO.CODIGO_ALTERNO=SESION.CODIGO_USUARIO AND " +
              "SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND NVL(COMPENDIO.FECHA_FIN_VIGENCIA,SYSDATE) AND " +
              "SUBDETALLE.ID_DETALLE_GRUPO=COMPENDIO.ID_DETALLE AND " +
              "SYSDATE BETWEEN SUBDETALLE.FECHA_INICIO_VIGENCIA AND NVL(SUBDETALLE.FECHA_FIN_VIGENCIA,SYSDATE) AND " +
              "SUJ.SUJETO_RIESGO=TO_NUMBER(SUBDETALLE.CODIGO_ALTERNO) AND " +
              "SYSDATE BETWEEN SUJ.FECHA_INICIO_VIGENCIA AND NVL(SUJ.FECHA_FIN_VIGENCIA,SYSDATE) " +
              "ORDER BY SUJ.DESCRIPCION_BREVE";
        }
        public static String MgrClaseMedidaCombo(int id_sujeto_riesgo)
        {
            return @"SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE " +
                        " WHERE ID_COMPENDIO=2 AND SUJETO_RIESGO = TO_NUMBER(:id_sujeto_riesgo) AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) " +
                        " ORDER BY REFERENCIA1";
        }
        public static String obtenerCompendioGeneral(int id_compendio)
        {
            return @"select * from grta_compendio_general where  id_compendio =" + id_compendio + ""; 
        }
        public static String MgrFuenteDatos1821767ConCategoriaCombo(int id_sujeto_riesgo)
        {
            return @"SELECT COMPENDIO.ID_DETALLE codigo_categoria, COMPENDIO.NOMBRE descripcion_categoria, \n"
            + "COALESCE(VARIABLES.TABLA_TRANSACCIONAL, 0) codigo, (CASE WHEN VARIABLES.TABLA_TRANSACCIONAL IS NOT NULL THEN GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(VARIABLES.TABLA_TRANSACCIONAL) || '('||GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_TABLA(VARIABLES.TABLA_TRANSACCIONAL)||')' ELSE 'FUNCIONES' END) descripcion, \n"
            + "COUNT(0) cantidad \n"
            + "FROM GRTA_VARIABLES VARIABLES, GRTA_CATEGORIA_VARIABLES CATEGORIAS, GRTA_COMPENDIO_DETALLE COMPENDIO \n"
            + "WHERE VARIABLES.CODIGO_VARIABLE = CATEGORIAS.CODIGO_VARIABLE AND COMPENDIO.ID_DETALLE = CATEGORIAS.CATEGORIA_DATO \n"
            + "AND SYSDATE BETWEEN CATEGORIAS.FECHA_INICIO_VIGENCIA AND COALESCE(CATEGORIAS.FECHA_FIN_VIGENCIA,SYSDATE) \n"
            + "AND VARIABLES.SUJETO_RIESGO = " + id_sujeto_riesgo + " AND (? IS NULL OR COMPENDIO.ID_DETALLE = ?) AND VARIABLES.TIPO_VARIABLE = 57 AND VARIABLES.MODO_USO = 22 AND VARIABLES.NUMERO_OCURRENCIAS = 55 AND VARIABLES.EXPRESION_CONSOLIDACION IS NOT NULL \n"
            + "AND VARIABLES.EXPRESION_CONSOLIDACION NOT LIKE '%NO DEFINIDO%' AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(VARIABLES.EXPRESION_CONSOLIDACION))=0 \n"
            + "AND SYSDATE BETWEEN VARIABLES.FECHA_INICIO_VIGENCIA AND COALESCE(VARIABLES.FECHA_FIN_VIGENCIA, SYSDATE) \n"
            + "GROUP BY COMPENDIO.ID_DETALLE, COMPENDIO.NOMBRE, VARIABLES.TABLA_TRANSACCIONAL \n"
            + "ORDER BY 4 \n";
        }

        public static String MgrFuenteDatos1719Combo(int id_sujeto_riesgo)
        {
            return @"SELECT NVL(TABLA_TRANSACCIONAL,0) codigo,\n" +
                "(CASE WHEN TABLA_TRANSACCIONAL IS NOT NULL THEN GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(TABLA_TRANSACCIONAL) || '('||GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_TABLA(TABLA_TRANSACCIONAL)||')' ELSE 'FUNCIONES' END) descripcion,\n" +
                "COUNT(0) cantidad\n" +
                "FROM GRTA_VARIABLES  \n" +
                "WHERE SUJETO_RIESGO =  " + id_sujeto_riesgo + " AND -- \n" +
                "TIPO_VARIABLE = 57 AND MODO_USO = 22 AND NUMERO_OCURRENCIAS = 55 AND \n" +
                "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND \n" +
                "EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" +
                "GROUP BY TABLA_TRANSACCIONAL ORDER BY 2";
        }
        public static String MgrFuenteDatos1719ConCategoriaCombo(int id_sujeto_riesgo)
        {
            return @"SELECT COMPENDIO.ID_DETALLE codigo_categoria, COMPENDIO.NOMBRE descripcion_categoria, \n"
            + "COALESCE(VARIABLES.TABLA_TRANSACCIONAL, 0) codigo, (CASE WHEN VARIABLES.TABLA_TRANSACCIONAL IS NOT NULL THEN GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(VARIABLES.TABLA_TRANSACCIONAL) || '('||GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_TABLA(VARIABLES.TABLA_TRANSACCIONAL)||')' ELSE 'FUNCIONES' END) descripcion, \n"
            + "COUNT(0) cantidad \n"
            + "FROM GRTA_VARIABLES VARIABLES, GRTA_CATEGORIA_VARIABLES CATEGORIAS, GRTA_COMPENDIO_DETALLE COMPENDIO \n"
            + "WHERE VARIABLES.CODIGO_VARIABLE = CATEGORIAS.CODIGO_VARIABLE AND COMPENDIO.ID_DETALLE = CATEGORIAS.CATEGORIA_DATO \n"
            + "AND SYSDATE BETWEEN CATEGORIAS.FECHA_INICIO_VIGENCIA AND COALESCE(CATEGORIAS.FECHA_FIN_VIGENCIA,SYSDATE) \n"
            + "AND VARIABLES.SUJETO_RIESGO = ? AND (? IS NULL OR COMPENDIO.ID_DETALLE = ?) AND VARIABLES.TIPO_VARIABLE = 57 AND VARIABLES.MODO_USO = 22 AND VARIABLES.NUMERO_OCURRENCIAS = 55 \n"
            + "AND VARIABLES.EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' AND SYSDATE BETWEEN VARIABLES.FECHA_INICIO_VIGENCIA AND COALESCE(VARIABLES.FECHA_FIN_VIGENCIA, SYSDATE) \n"
            + "GROUP BY COMPENDIO.ID_DETALLE, COMPENDIO.NOMBRE, VARIABLES.TABLA_TRANSACCIONAL \n"
            + "ORDER BY 4 \n";
        }

        public static String MgrFuenteDatosLineaDiferenteCeroConCategoriaCombo(int id_sujeto_riesgo)
        {
            return @"SELECT COMPENDIO.ID_DETALLE codigo_categoria, COMPENDIO.NOMBRE descripcion_categoria, \n"
            + "COALESCE(VARIABLES.TABLA_TRANSACCIONAL, 0) codigo, (CASE WHEN VARIABLES.TABLA_TRANSACCIONAL IS NOT NULL THEN GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(VARIABLES.TABLA_TRANSACCIONAL) || '('||GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_TABLA(VARIABLES.TABLA_TRANSACCIONAL)||')' ELSE 'FUNCIONES' END) descripcion, \n"
            + "COUNT(0) cantidad \n"
            + "FROM GRTA_VARIABLES VARIABLES, GRTA_CATEGORIA_VARIABLES CATEGORIAS, GRTA_COMPENDIO_DETALLE COMPENDIO \n"
            + "WHERE VARIABLES.CODIGO_VARIABLE = CATEGORIAS.CODIGO_VARIABLE AND COMPENDIO.ID_DETALLE = CATEGORIAS.CATEGORIA_DATO \n"
            + "AND SYSDATE BETWEEN CATEGORIAS.FECHA_INICIO_VIGENCIA AND COALESCE(CATEGORIAS.FECHA_FIN_VIGENCIA,SYSDATE) \n"
            + "AND VARIABLES.SUJETO_RIESGO = ? AND (? IS NULL OR COMPENDIO.ID_DETALLE = ?) AND VARIABLES.TIPO_VARIABLE = 57 AND VARIABLES.MODO_USO = 22 \n"
            + "AND VARIABLES.EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' AND SYSDATE BETWEEN VARIABLES.FECHA_INICIO_VIGENCIA AND COALESCE(VARIABLES.FECHA_FIN_VIGENCIA, SYSDATE) \n"
            + "GROUP BY COMPENDIO.ID_DETALLE, COMPENDIO.NOMBRE, VARIABLES.TABLA_TRANSACCIONAL \n"
            + "ORDER BY 4 \n";
        }
        /*Usado 17-12-2017 WMarcia*/
        /*MgrUsuarioConsulta("SELECT DISTINCT usuario.iddga , rolusuario.IDROL" + 
        " FROM" + 
        " seg_usuarios usuario," + 
        " SEG_ROLUSER rolusuario," + 
        " SEG_ROLES rolez" + 
        " WHERE" + 
        " usuario.IDDGA = rolusuario.IDDGA" + 
        " AND rolusuario.IDROL = rolez.IDROL" + 
        " AND rolez.IDAPLICACION = 13" + 
        " AND rolez.ESTADO = 1" + 
        " AND LOWER(usuario.usuario) = LOWER(:pUsuario)" + 
        " AND usuario.clave = GRPK_OPERACIONES_COMUNES.GRFN_ENCRIPTAR(:pUsuario,:pPass)"),*/
        public const String MgrUsuarioConsulta = "SELECT USUARIO.PRIMER_NOMBRE, USUARIO.SEGUNDO_NOMBRE, USUARIO.PRIMER_APELLIDO, USUARIO.SEGUNDO_APELLIDO, USUARIO.EMAIL, USUARIO.FOTO, USUARIO.USUARIO_ADMINISTRADOR " +
               "FROM GRTA_USUARIO USUARIO WHERE USUARIO.ID_USUARIO = :pIdUsuario";
        /*Usado 22-12-2017 WMarcia*/
        public const String MgrUsuarioRol = "SELECT DISTINCT INITCAP(ROL.NOMBRE) ROL " +
            "FROM GRTA_ROL ROL INNER JOIN GRTA_USUARIO_ROL USUARIO_ROL ON ROL.ID_ROL = USUARIO_ROL.ID_ROL " +
            "WHERE ROL.ESTADO = 1 AND USUARIO_ROL.ESTADO = 1 AND USUARIO_ROL.ID_USUARIO = :pIdUsuario";
        /*Usado 17-12-2017 WMarcia*/
        public const String MgrUsuarioFuncionalidad = "SELECT FUNCIONALIDAD_.ID_FUNCIONALIDAD, FUNCIONALIDAD_.NOMBRE, FUNCIONALIDAD_.DESCRIPCION, FUNCIONALIDAD_.ID_FUNCIONALIDAD_PADRE, FUNCIONALIDAD_.TIPO, FUNCIONALIDAD_.MAPEO_APLICACION, FUNCIONALIDAD_.COMPONENTE, " +
            "FUNCIONALIDAD_.RUTA, FUNCIONALIDAD_.ICONO, FUNCIONALIDAD_.ORDEN " +
            "FROM GRTA_FUNCIONALIDAD FUNCIONALIDAD_ " +
            "WHERE FUNCIONALIDAD_.ID_FUNCIONALIDAD IN ( " +
                "SELECT DISTINCT ID_FUNCIONALIDAD " +
                "FROM ( " +
                    "SELECT FUNCIONALIDAD.ID_FUNCIONALIDAD " +
                    "FROM GRTA_ROL ROL INNER JOIN GRTA_USUARIO_ROL USUARIO_ROL ON ROL.ID_ROL = USUARIO_ROL.ID_ROL " +
                    "INNER JOIN GRTA_ROL_FUNCIONALIDAD ROL_FUNCIONALIDAD ON ROL.ID_ROL = ROL_FUNCIONALIDAD.ID_ROL " +
                    "INNER JOIN GRTA_FUNCIONALIDAD FUNCIONALIDAD ON FUNCIONALIDAD.ID_FUNCIONALIDAD = ROL_FUNCIONALIDAD.ID_FUNCIONALIDAD " +
                    "WHERE ROL.ESTADO = 1 AND USUARIO_ROL.ESTADO = 1 AND USUARIO_ROL.ESTADO = 1 AND ROL_FUNCIONALIDAD.ESTADO = 1 " +
                    "AND FUNCIONALIDAD.ESTADO = 1 AND USUARIO_ROL.ID_USUARIO = :pIdUsuario " +
                    "UNION ALL " +
                    "SELECT FUNCIONALIDAD.ID_FUNCIONALIDAD " +
                    "FROM GRTA_FUNCIONALIDAD FUNCIONALIDAD INNER JOIN GRTA_USUARIO_FUNCIONALIDAD USUARIO_FUNCIONALIDAD ON FUNCIONALIDAD.ID_FUNCIONALIDAD = USUARIO_FUNCIONALIDAD.ID_FUNCIONALIDAD " +
                    "WHERE FUNCIONALIDAD.ESTADO = 1 AND USUARIO_FUNCIONALIDAD.ESTADO = 1 AND USUARIO_FUNCIONALIDAD.ID_USUARIO = :pIdUsuario)) " +
                "ORDER BY FUNCIONALIDAD_.ID_FUNCIONALIDAD";
        /*Usado 25-12-2017 WMarcia*/
        public const String MgrUsuarioFuncionalidadDerecho = "SELECT FUNCIONALIDAD_EXTERNA.ID_FUNCIONALIDAD, FUNCIONALIDAD_EXTERNA.NOMBRE, FUNCIONALIDAD_EXTERNA.TIPO, NVL(USUARIO_FUNCIONALIDAD_DERECHO.DERECHO, 0) DERECHO " +
            "FROM GRTA_FUNCIONALIDAD FUNCIONALIDAD_EXTERNA, " +
                "(SELECT DISTINCT FUNCIONALIDAD.ID_FUNCIONALIDAD, 1 DERECHO " +
                "FROM GRTA_FUNCIONALIDAD FUNCIONALIDAD INNER JOIN GRTA_ROL_FUNCIONALIDAD ROL_FUNCIONALIDAD ON FUNCIONALIDAD.ID_FUNCIONALIDAD = ROL_FUNCIONALIDAD.ID_FUNCIONALIDAD " +
                "INNER JOIN GRTA_ROL ROL ON ROL.ID_ROL = ROL_FUNCIONALIDAD.ID_ROL " +
                "INNER JOIN GRTA_USUARIO_ROL USUARIO_ROL ON ROL.ID_ROL = USUARIO_ROL.ID_ROL " +
                "INNER JOIN GRTA_USUARIO USUARIO ON USUARIO.ID_USUARIO = USUARIO_ROL.ID_USUARIO " +
                "WHERE FUNCIONALIDAD.ESTADO = 1 AND ROL_FUNCIONALIDAD.ESTADO = 1 AND ROL.ESTADO = 1 AND USUARIO_ROL.ESTADO = 1 AND USUARIO.ESTADO = 1 " +
                "AND FUNCIONALIDAD.MAPEO_APLICACION = :pCodigoFuncionalidad AND USUARIO.USUARIO = :pUsuario " +
                "UNION ALL " +
                "SELECT DISTINCT FUNCIONALIDAD.ID_FUNCIONALIDAD, 1 DERECHO " +
                "FROM GRTA_FUNCIONALIDAD FUNCIONALIDAD INNER JOIN GRTA_USUARIO_FUNCIONALIDAD USUARIO_FUNCIONALIDAD ON FUNCIONALIDAD.ID_FUNCIONALIDAD = USUARIO_FUNCIONALIDAD.ID_FUNCIONALIDAD " +
                "INNER JOIN GRTA_USUARIO USUARIO ON USUARIO.ID_USUARIO = USUARIO_FUNCIONALIDAD.ID_USUARIO " +
                "WHERE FUNCIONALIDAD.ESTADO = 1 AND USUARIO_FUNCIONALIDAD.ESTADO = 1 AND USUARIO.ESTADO = 1 " +
                "AND FUNCIONALIDAD.MAPEO_APLICACION = :pCodigoFuncionalidad AND USUARIO.USUARIO = :pUsuario) USUARIO_FUNCIONALIDAD_DERECHO " +
            "WHERE FUNCIONALIDAD_EXTERNA.ID_FUNCIONALIDAD = USUARIO_FUNCIONALIDAD_DERECHO.ID_FUNCIONALIDAD (+) " +
            "AND FUNCIONALIDAD_EXTERNA.ESTADO = 1 AND FUNCIONALIDAD_EXTERNA.MAPEO_APLICACION = :pCodigoFuncionalidad";
        /*Usado 25-12-2017 WMarcia*/
        public const String MgrUsuarioFuncionalidadOperacionDerecho = "SELECT FUNC_OPERACION_EXTERNA.ID_OPERACION, FUNC_OPERACION_EXTERNA.ID_FUNCIONALIDAD, FUNC_OPERACION_EXTERNA.NOMBRE, NVL(USR_FUNC_OPERACION_DERECHO.DERECHO, 0) DERECHO " +
            "FROM GRTA_FUNCIONALIDAD FUNCIONALIDAD_EXTERNA, GRTA_FUNCIONALIDAD_OPERACION FUNC_OPERACION_EXTERNA, " +
            "( SELECT DISTINCT FUNCIONALIDAD_OPERACION.ID_OPERACION, 1 DERECHO " +
            "FROM GRTA_FUNCIONALIDAD FUNCIONALIDAD INNER JOIN GRTA_ROL_FUNCIONALIDAD ROL_FUNCIONALIDAD ON FUNCIONALIDAD.ID_FUNCIONALIDAD = ROL_FUNCIONALIDAD.ID_FUNCIONALIDAD " +
            "INNER JOIN GRTA_FUNCIONALIDAD_OPERACION FUNCIONALIDAD_OPERACION ON FUNCIONALIDAD.ID_FUNCIONALIDAD = FUNCIONALIDAD_OPERACION.ID_FUNCIONALIDAD " +
            "INNER JOIN GRTA_ROL ROL ON ROL.ID_ROL = ROL_FUNCIONALIDAD.ID_ROL " +
            "INNER JOIN GRTA_ROL_FUNC_OPERACION ROL_FUNC_OPERACION ON ROL_FUNC_OPERACION.ID_FUNCIONALIDAD = FUNCIONALIDAD_OPERACION.ID_FUNCIONALIDAD AND ROL_FUNC_OPERACION.ID_OPERACION = FUNCIONALIDAD_OPERACION.ID_OPERACION AND ROL_FUNC_OPERACION.ID_ROL = ROL.ID_ROL " +
            "INNER JOIN GRTA_USUARIO_ROL USUARIO_ROL ON ROL.ID_ROL = USUARIO_ROL.ID_ROL " +
            "INNER JOIN GRTA_USUARIO USUARIO ON USUARIO.ID_USUARIO = USUARIO_ROL.ID_USUARIO " +
            "WHERE FUNCIONALIDAD.ESTADO = 1 AND ROL_FUNCIONALIDAD.ESTADO = 1 AND FUNCIONALIDAD_OPERACION.ESTADO = 1 AND ROL.ESTADO = 1 AND ROL_FUNC_OPERACION.ESTADO = 1 AND USUARIO_ROL.ESTADO = 1 AND USUARIO.ESTADO = 1 " +
            "AND FUNCIONALIDAD.MAPEO_APLICACION = :pCodigoFuncionalidad AND USUARIO.USUARIO = :pUsuario " +
            "UNION ALL " +
            "SELECT DISTINCT FUNCIONALIDAD.ID_FUNCIONALIDAD, 1 DERECHO " +
            "FROM GRTA_FUNCIONALIDAD FUNCIONALIDAD INNER JOIN GRTA_USUARIO_FUNCIONALIDAD USUARIO_FUNCIONALIDAD ON FUNCIONALIDAD.ID_FUNCIONALIDAD = USUARIO_FUNCIONALIDAD.ID_FUNCIONALIDAD " +
            "INNER JOIN GRTA_FUNCIONALIDAD_OPERACION FUNCIONALIDAD_OPERACION ON FUNCIONALIDAD.ID_FUNCIONALIDAD = FUNCIONALIDAD_OPERACION.ID_FUNCIONALIDAD " +
            "INNER JOIN GRTA_USUARIO USUARIO ON USUARIO.ID_USUARIO = USUARIO_FUNCIONALIDAD.ID_USUARIO " +
            "INNER JOIN GRTA_USUARIO_FUNC_OPERACION USUARIO_FUNC_OPERACION ON USUARIO_FUNC_OPERACION.ID_FUNCIONALIDAD = FUNCIONALIDAD_OPERACION.ID_FUNCIONALIDAD AND USUARIO_FUNC_OPERACION.ID_OPERACION = FUNCIONALIDAD_OPERACION.ID_OPERACION AND USUARIO_FUNC_OPERACION.ID_USUARIO = USUARIO.ID_USUARIO " +
            "WHERE FUNCIONALIDAD.ESTADO = 1 AND USUARIO_FUNCIONALIDAD.ESTADO = 1 AND FUNCIONALIDAD_OPERACION.ESTADO = 1 AND USUARIO.ESTADO = 1 AND USUARIO_FUNC_OPERACION.ESTADO = 1 " +
            "AND FUNCIONALIDAD.MAPEO_APLICACION = :pCodigoFuncionalidad AND USUARIO.USUARIO = :pUsuario) USR_FUNC_OPERACION_DERECHO " +
            "WHERE FUNCIONALIDAD_EXTERNA.ID_FUNCIONALIDAD = FUNC_OPERACION_EXTERNA.ID_FUNCIONALIDAD " +
            "AND FUNC_OPERACION_EXTERNA.ID_OPERACION = USR_FUNC_OPERACION_DERECHO.ID_OPERACION (+) " +
            "AND FUNCIONALIDAD_EXTERNA.ESTADO = 1 AND FUNC_OPERACION_EXTERNA.ESTADO = 1 AND FUNCIONALIDAD_EXTERNA.MAPEO_APLICACION = :pCodigoFuncionalidad " +
            "ORDER BY FUNC_OPERACION_EXTERNA.ID_OPERACION";
        /*Usado 17-12-2017 WMarcia*/
        //@author: RTrujillo, @version; 2.0, @descripcion: 
        public const String MgrRedIdSessionUsr = "SELECT MAX(ID_SESSION) ID_SESSION FROM GRTA_SESSION\n" +
                    "WHERE\n" +
                    "CODIGO_USUARIO=UPPER(?) AND DIRECCION_IP=?";

        //@author: RTrujillo, @version; 2.0, @descripcion: Recupera el numero para la siguente medida
        public const String MgrSiguienteMedida = "SELECT x.id_medida + 1 medida FROM( " +
        "SELECT (CASE WHEN MAX(t.ID_MEDIDA) IS NULL THEN 0 ELSE MAX(t.ID_MEDIDA) END) id_medida FROM GRTA_MEDIDAS t) x";

        //@author: RTrujillo, @version; 2.0, @descripcion: 
        public const String MgrVerificarPermiso = "SELECT DISTINCT GLUSAMBID, GLUSAMBNVL  FROM GLUSUA1 WHERE GLUSID = :pIdUser AND GLUSAMBID = :pParticula AND GLUSAMBNVL = :pAccion"
                + " AND SYSDATE BETWEEN GLUSAMBFINI AND DECODE(GLUSAMBFFIN,'',SYSDATE,GLUSAMBFFIN) ";

        //------------------------------------------------------------------------------------------------------------------------    

        //---Wilbert

        // Recupera el nombre y el contenido de un archivo adjunto
        public const String MgrArchivoAnexo = "SELECT ARCHIVOS.nombre_archivo,ARCHIVOS.contenido" +
                       " FROM GRTA_POLITICA_ARCHIVOS POLITICA, GRTA_ARCHIVOS_ANEXOS ARCHIVOS" +
                   " WHERE POLITICA.id_politica=TO_NUMBER(?) AND POLITICA.FECHA_FIN_VIGENCIA IS NULL AND ARCHIVOS.id_archivo=POLITICA.id_archivo";

        //TODO: Cambio por la version
        //@author: RTrujillo, @version; 2.0, @descripcion: Recupera el Numero para el siguiente Canal de Selectividad
        public const String MgrSiguienteNivelRef = "SELECT y.nivel + 1 nivel FROM " +
        "(SELECT (CASE WHEN MAX(n.ORDEN_NIVEL) IS NULL THEN 0 ELSE MAX(n.ORDEN_NIVEL) END) nivel FROM GRTA_TMP_NIVEL_REF_MED n " +
        "WHERE n.ID_MEDIDA = ? AND n.version_medida = ?) y";
        //@author: WMarcia, @version: 1.0 13/10/2009, @descripcion: Insertar registros retenidos a temporales de canales
        public const String MgrSiguienteVersion = "SELECT MAX(version_medida) + 1 nuevaVersion FROM GRTA_MEDIDAS WHERE id_medida = TO_NUMBER(?)";
        // Recupera el nuevo codigo de componente de una medida de control SQL
        public const String MgrSiguienteComponente = "SELECT (NVL(MAX(codigo_componente),0) + 1) nuevoCodComp" +
         " FROM GRTA_TMP_REGLAFIJA " +
         " WHERE id_medida = TO_NUMBER(?) AND version_medida = TO_NUMBER(?)";
        //Ocamacho 17MAY2012: se recupera el nombre de la tabla cat�logo
        public const String MgrNombreVariableCatalogo = "SELECT CODIGO_VARIABLE, DESCRIPCION_BREVE FROM GRTA_VARIABLES \n" +
           "WHERE \n" +
           "CODIGO_VARIABLE IN \n" +
           "(SELECT VARIABLE_CATALOGO FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE=?)";

        //@Autor: WMarcia; @Version: 1.0, 10/09/2010; @Descripcion: Envia correo
        public const String MgrEnviaCorreo = "SELECT LOWER(COMPENDIO.DESCRIPCION) DIR_CORREO FROM GRTA_COMPENDIO_DETALLE COMPENDIO WHERE COMPENDIO.id_compendio=63 AND COMPENDIO.REFERENCIA1 = 'ENVIA'\n" +
                      "AND TRUNC(SYSDATE) BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,TRUNC(SYSDATE))";
        //@Autor: WMarcia; @Version: 1.0, 10/09/2010; @Descripcion: Copia correo
        public const String MgrCopiaCorre = "SELECT LOWER(COMPENDIO.DESCRIPCION) DIR_CORREO FROM GRTA_COMPENDIO_DETALLE COMPENDIO WHERE COMPENDIO.id_compendio=63 AND COMPENDIO.REFERENCIA1 = 'COPIA' " +
                      "AND TRUNC(SYSDATE) BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,TRUNC(SYSDATE))";
        //@Autor: WMarcia; @Version: 1.0, 12/07/2010; @Descripcion: Recupera el usuario creado



        public const String MgrVerificaContrasenna = "SELECT DECODE(USUARIO.USUARIO, NULL, 0, 1) FROM SEG_USUARIOS usuario WHERE USUARIO.CLAVE =GRPK_OPERACIONES_COMUNES.GRFN_ENCRIPTAR(?,?)" +
       " AND USUARIO.IDDGA = ?";

     

        //mvalle nuevo para los compendios por tipo de tabla
        public const String CompendioxTipoTablaCombo = "SELECT GEN.ID_COMPENDIO codigo,GEN.NOMBRE descripcion \n" +
           "FROM GRTA_COMPENDIO_GENERAL GEN \n" +
           "WHERE GEN.TIPO_TABLA=?\n";

        //nuevo, para parametrizar este tipó de consultas
        public const String CompendioDetalleComboMasReferencia = "SELECT REFERENCIA1 codigoCadena, ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE\n" +
                   "WHERE ID_COMPENDIO=? AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)\n" +
                   "ORDER BY TO_NUMBER(REFERENCIA1)";

        //Define si un usuario tiene acceso a determinada opcion del modulo, primer nivel del menu
        public const String NivelMenu = "SELECT DISTINCT so.idpermiso, so.nombre_corto, so.orden \n" +
           "FROM \n" +
           "SEG_OPCIONES so, \n" +
           "SEG_OPCIONESROLL sor, \n" +
           "Seg_Roles sr\n" +
           "WHERE \n" +
           "sor.idpermiso = so.idpermiso \n" +
           "AND sor.idroll = sr.idrol \n" +
           "AND so.idaplicacion = '13' \n" +
           "AND so.estadomenu = 1\n" +
           "AND sor.idroll IN(SELECT sru.idrol FROM Seg_Roluser sru WHERE sru.iddga = ? AND SRU.BORRADO = 0) \n" +
           "AND so.opcion_funcional = 0\n" +
           "AND TRUNC(SYSDATE) BETWEEN TO_DATE(sor.FECHA_AUTORIZACION,'DD/MM/YYYY') AND  NVL(sor.FECHA_BAJA,TRUNC(SYSDATE))\n" +
           "AND sr.estado = 1 AND sor.borrado = 0 \n" +
           "ORDER BY so.orden";

        //Define si un usuario tiene acceso a determinada opcion del modulo, sub nivel del menu    
        public const String SubNivelMenu = "SELECT DISTINCT so.nombre_corto, so.url, so.orden \n" +
           "FROM \n" +
           "SEG_OPCIONES so, \n" +
           "SEG_OPCIONESROLL sor\n" +
           "WHERE \n" +
           "sor.idpermiso = so.idpermiso " +
           "AND so.idaplicacion = '13' \n" +
           "AND sor.idroll IN(SELECT sru.idrol FROM Seg_Roluser sru WHERE sru.iddga = :pIdUser AND SRU.BORRADO = 0)\n" +
           "AND TRUNC(SYSDATE) BETWEEN TO_DATE(sor.FECHA_AUTORIZACION, 'DD/MM/YYYY') AND NVL(sor.FECHA_BAJA,TRUNC(SYSDATE))\n" +
           "AND so.estadomenu = 1 \n" +
           "AND so.opcion_funcional = 1 \n" +
           "AND SO.URL IS NOT NULL \n" +
           "AND so.idpermisosmenu IS NOT NULL \n" +
           "AND so.orden IS NOT NULL\n" +
           "AND sor.borrado = 0 \n" +
           "AND so.idpermisosmenu = TO_NUMBER(:pIdMenu) \n" +
           "ORDER BY TO_NUMBER( so.orden)";

        // Recupera el código y la descripción de las Aduanas
        public const String AduanaCombo = "SELECT CUO.CUO_COD codigo, CUO.CUO_NAM descripcion  FROM AWUNADM.UNCUOTAB cuo" +
              " WHERE CUO.CUO_CTY = 'NI'" +
              " AND SYSDATE BETWEEN CUO.VALID_FROM AND NVL(CUO.VALID_TO, SYSDATE)";

        // Recupera el código y la descripción de los regimenes
        public const String RegimenCombo = "SELECT t.REFERENCIA1 id_detalle, t.REFERENCIA1||'-'||t.DESCRIPCION descripcion FROM GRTA_COMPENDIO_DETALLE t " +
                                      "WHERE t.ID_COMPENDIO=48 " +
                                      "AND (SYSDATE BETWEEN t.FECHA_INICIO_VIGENCIA AND (CASE WHEN t.FECHA_FIN_VIGENCIA IS NULL THEN SYSDATE ELSE t.FECHA_FIN_VIGENCIA END)) " +
                                      "ORDER BY 2";

        /*//Recupera el código y la descripción de las Variables Dependientes
     public const String VarDepenCombo="SELECT t.CODIGO_VARIABLE, t.DESCRIPCION FROM GRTA_VARIABLES t " +
                         "WHERE t.TIPO_VARIABLE=61 " +
                         "AND (SYSDATE BETWEEN t.FECHA_INICIO_VIGENCIA AND (CASE WHEN t.FECHA_FIN_VIGENCIA IS NULL THEN SYSDATE ELSE t.FECHA_FIN_VIGENCIA END)) " +
                         "AND t.SUJETO_RIESGO = ? " + 
                         "ORDER BY t.CODIGO_VARIABLE"),*/

        //Recupera el código y la descripción de las Variables Independientes    
        public const String VarIndepenCombo = "SELECT t.CODIGO_VARIABLE, t.DESCRIPCION FROM GRTA_VARIABLES t " +
                               "WHERE t.TIPO_VARIABLE=60 " +
                               "AND (SYSDATE BETWEEN t.FECHA_INICIO_VIGENCIA AND (CASE WHEN t.FECHA_FIN_VIGENCIA IS NULL THEN SYSDATE ELSE t.FECHA_FIN_VIGENCIA END)) " +
                               "AND t.SUJETO_RIESGO = ? " +
                               "ORDER BY 2";

        //@author: RTrujillo, @version: 2.0, 03/10/2009, @descripcion: Obtiene los tipos de medida pe(regla fija, modelo aleatorio, etc)
        /*MgrTipoMedidaCombo="SELECT t.id_detalle codTipoMedida,t.nombre tipoMedida FROM GRTA_COMPENDIO_DETALLE t " +
        "WHERE t.id_compendio = 7 AND t.referencia1 = 1 " +
        "AND (SYSDATE between t.FECHA_INICIO_VIGENCIA AND NVL(t.FECHA_FIN_VIGENCIA,SYSDATE))"),	*/


        public const String EstadoMedidaLista = "SELECT t.id_detalle codigo,t.nombre descripcion FROM GRTA_COMPENDIO_DETALLE t"
                           + " WHERE t.id_compendio = 12 AND t.referencia1 = 1"
                           + " AND (SYSDATE between  t.FECHA_INICIO_VIGENCIA AND NVL(t.FECHA_FIN_VIGENCIA,SYSDATE))";
        /* Ocamacho 14MAY2012: Se realizó la consulta de elementos del compendio:*/
        /*
    MgrCompendioDetTabla="SELECT id_detalle \"Codigo\", descripcion \"Descripcion\","
            + " NVL(TO_CHAR(fecha_inicio_vigencia, 'DD/MM/YYYY'),' ') \"V. Desde\", NVL(TO_CHAR(FECHA_FIN_VIGENCIA, 'DD/MM/YYYY'),' ')  \"V. Hasta\","
            + " NVL(referencia1, ' ') \"Referencia 1\", NVL(referencia2,' ') \"Referencia 2\", NVL(referencia3,' ') \"Referencia 3\", ' ' \"Operaciones\""
            + " FROM grta_compendio_detalle"
            + " WHERE  id_compendio = :pIdCompendio"),
    */
        //@author: Ocamacho 14MAY2012: Consulta de la tabla compendio
        public const String Compendio = "SELECT id_compendio, DECODE(TIPO_TABLA,'1','CATALOGO','3','LISTA','2','VALIDACION') tipo_tabla, " +
                               "decode(tipo_codificacion,'1','Asignado por Sistema','Asignado por MGR') tipo_codificacion, UPPER(nombre) nombre, UPPER(descripcion) descripcion  "
                   + " FROM GRTA_COMPENDIO_GENERAL"
                   + " WHERE id_compendio=?";

        //Ocamacho 15MAR2012: consulta del elemento:
        public const String CompendioDet = "SELECT detalle.id_detalle, detalle.nombre, detalle.descripcion, TO_CHAR(detalle.fecha_inicio_vigencia,'DD/MM/YYYY') desde, TO_CHAR(detalle.fecha_inicio_vigencia,'DD/MM/YYYY HH24:MI') desde_militar, NVL(TO_CHAR(detalle.fecha_fin_vigencia,'DD/MM/YYYY'),' ') hasta,\n" +
           "NVL(detalle.referencia1,' ') referencia1, NVL(detalle.referencia2,' ') referencia2, NVL(detalle.codigo_alterno,' ') codigo_alterno, \n" +
           "detalle.sujeto_riesgo,\n" +
           "(select sujeto.descripcion_breve from grta_sujeto_riesgo sujeto where sujeto.sujeto_riesgo=detalle.sujeto_riesgo  AND ROWNUM=1) sujeto_descrip,\n" +
           "detalle.individual_grupal,decode(detalle.individual_grupal,'I','Individual','Grupal') indiv_grupal_descrip, detalle.variable_catalogo, \n" +
           "nvl((select descripcion_breve from grta_variables where codigo_variable=detalle.variable_catalogo ), 'CATALOGO NO DEFINIDO') variable_descrip \n" +
           "FROM GRTA_COMPENDIO_DETALLE detalle WHERE  detalle.id_detalle=?";

        //Ocamacho 15MAR2012: consulta del elemento:
        public const String CompendioSubDet = "SELECT detalle.id_subdetalle, detalle.nombre, detalle.descripcion, TO_CHAR(detalle.fecha_inicio_vigencia,'DD/MM/YYYY') desde, NVL(TO_CHAR(detalle.fecha_fin_vigencia,'DD/MM/YYYY'),' ') hasta,\n" +
           "detalle.codigo_alterno, detalle.id_detalle_grupo id_detalle \n" +
           "FROM GRTA_COMPENDIO_SUBDETALLE detalle WHERE  detalle.id_subdetalle=?";

        //Listas------------------

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: Obtiene los valores de la tabla grta_lista_general
        public const String ListaGeneralTabla = "SELECT id_lista \"Codigo\", UPPER(descripcion) \"Descripcion\", "
                   + " TO_CHAR(fecha_inicio_vigencia,'DD/MM/YYYY') \"V. Desde\","
                   + " NVL(TO_CHAR(fecha_fin_vigencia,'DD/MM/YYYY'), ' ') \"V. Hasta\", ' ' \"Operaciones\""
                   + " FROM GRTA_LISTA_GENERAL"
                   + " WHERE UPPER(descripcion) LIKE '%'||UPPER(:pDescripcion)||'%' OR :pDescripcion IS NULL"
                   + " AND  (fecha_inicio_vigencia BETWEEN  NVL(:pFechaInicio,fecha_inicio_vigencia) AND NVL(:pFechaFin, SYSDATE))"
                   + " ORDER BY 1";

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: Obtiene los valores de la tabla grta_lista_detalle
        public const String ListaDetTabla = "SELECT TO_CHAR(id_lista)||'-'||TO_CHAR(secuencia_detalle) \"Nro\","
                   + " valor_elemento \"Valor\", descripcion \"Descripcion\","
                   + "NVL(TO_CHAR(fecha_inicio_vigencia, 'DD/MM/YYYY'),' ')  \"V. Desde\","
                   + "NVL(TO_CHAR(fecha_fin_vigencia,'DD/MM/YYYY'),' ')  \"V. Hasta\","
                   + " ' ' \"Operaciones\" FROM GRTA_LISTA_DETALLE WHERE id_lista = TO_NUMBER(:pIdLista) ORDER BY 1 DESC";

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: Obtiene el registro especifico de una lista
        public const String Lista = "SELECT l.ID_LISTA, UPPER(l.DESCRIPCION) DESCRIPCION, NVL(TO_CHAR(l.FECHA_INICIO_VIGENCIA,'DD/MM/YYYY'), ' ') FECHA_INICIO_VIGENCIA,"
                   + " NVL(TO_CHAR(l.FECHA_FIN_VIGENCIA, 'DD/MM/YYYY'), ' ') FECHA_FIN_VIGENCIA FROM GRTA_LISTA_GENERAL l"
                   + " WHERE l.ID_LISTA = TO_NUMBER(?)";

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: obtiene el registro del detalle de la lista especificada
        public const String ListaDetalle = " SELECT l.ID_LISTA, l.SECUENCIA_DETALLE, l.VALOR_ELEMENTO, UPPER(l.DESCRIPCION) DESCRIPCION,"
                   + " NVL(TO_CHAR(l.FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY'),' ') FECHA_INICIO_VIGENCIA,"
                   + " NVL(TO_CHAR(l.FECHA_FIN_VIGENCIA, 'DD/MM/YYYY'),' ') FECHA_FIN_VIGENCIA"
                   + " FROM GRTA_LISTA_DETALLE l"
                   + " WHERE l.ID_LISTA = TO_NUMBER(:pIdLista) AND l.SECUENCIA_DETALLE = TO_NUMBER(:pSecuencia)";

        //-------Perfil de Riesgo-------------------

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: Recupera los registros de las agencias             
        public const String AgenciaList = "SELECT DEC_COD, UPPER(dec_nam) FROM UNDECTAB WHERE LST_OPE='U' AND UPPER(dec_nam) LIKE UPPER('%'||?||'%')";

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: Recupera los registros de los contribuyentes
        public const String ContribuyenteList = "SELECT CMP_COD, UPPER(t.cmp_nam) FROM uncmptab t WHERE t.LST_OPE='U' AND UPPER(t.cmp_nam) LIKE UPPER('%'||?||'%')";

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: Recupera los registros de los regimenes
        public const String RegimenList = "SELECT referencia1, descripcion FROM grta_compendio_detalle WHERE id_compendio = 36 AND UPPER(descripcion) LIKE UPPER('%'||?||'%')";

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: Recupera la descripcion del agante de aduanas
        public const String ConsultaDescAgencia = "SELECT UPPER(dec_nam) FROM UNDECTAB WHERE LST_OPE='U' AND DEC_COD=TO_CHAR(?)";

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: Recupera la descripcion del contribuyente
        public const String ConsultaDescContribuyente = "SELECT  UPPER(t.cmp_nam) FROM uncmptab t WHERE t.LST_OPE='U' AND t.CMP_COD= TO_CHAR(?)";

        //@author: RTrujillo; @version: 2.0, 10/2009; @descripcion: Recupera la descripcion del regimen
        public const String ConsultaDescRegimen = "SELECT descripcion FROM grta_compendio_detalle WHERE id_compendio = 36 AND referencia1 = TO_CHAR(?)";

        //@author: RTrujillo; @version: 1.0, 10/2009; @descripcion: obtiene el tipo de la medida especificada
        public const String ConsultaTipoMedida = "SELECT m.tipo_medida FROM grta_medidas m WHERE m.id_medida = TO_NUMBER(?) AND m.version_medida = TO_NUMBER(?)";

        //@author: RTrujillo; @version: 1.0, 10/2009; @descripcion: Recupera los registro del detalle del perfil de una declaracion para Regla fija y modelo aleatorio    
        public const String DetPerfilDuaModReglaFija_AleatorioTabla = "SELECT NVL(medidas.expresion_logica,' ') \"Expresión Lógica\","
                   + " replace(replace(replace(replace(replace(replace(perfilMedida.expresion_booleano,'0','falso'),'1','verdadero'),'+',' o '),'*',' y '),'(','['),')',']') || ' -> VERDADERO' \"Resultado de la Expresión\" "
                   + " FROM grta_declaraciones_perfil perfil,"
                   + " grta_declaraciones_medida perfilMedida,"
                   + " grta_medidas medidas"
                   + " WHERE perfil.id_perfil= :pPerfil"
                   + " AND perfil.indicador_activo=1"
                   + " AND perfilMedida.id_perfil=perfil.id_perfil"
                   + " AND perfilMedida.id_medida = TO_NUMBER(:pIdMedida)"
                   + " AND perfilMedida.version_medida = TO_NUMBER(:pVersion)"
                   + " AND perfilMedida.SECUENCIA_LINEA = TO_NUMBER(:pSerie)"
                   + " AND medidas.id_medida=perfilMedida.id_medida"
                   + " AND medidas.version_medida = perfilMedida.version_medida";

        //--------Dossier Aduanero-----------------------------

        //@author: RTrujillo; @version 2.0, 10/2009, @descripcion: obtiene la consulta de las descripciones para los valores de concepto
        public const String QueryFiltro = "SELECT GRPK_CONSULTAS.GRFN_AUTOCOMPLETE(:pConcepto, :pValor) FROM DUAL";

        //@author: RTrujillo; @version 2.0, 10/2009, @descripcion: devuelve el query que permite obtener la descripcion de los códigos
        public const String ConsultaDescConcepto = "SELECT GRPK_CONSULTAS.GRFN_DESCRIPCION_CODIGO(:pConcepto,:pValor) FROM DUAL";

        //@author: RTrujillo; @version 2.0, 10/2009, @descripcion: Obtiene los valores código y descripción de los filtros para la busqueda en el dossier 
        public const String FiltroOperadorCombo = "SELECT CODIGO_ALTERNO, NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=293 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA, SYSDATE)";

        //--------Proceso Evaluacion Medidas-----------------------------

        //@author: Ocamacho 05JUN2012: Se incorporó el sujeto de riesgo:
        public const String MedidaEvaluacionTabla = "SELECT ' ' \"Opcion\", TO_CHAR(X.ID_MEDIDA) ||'-'||TO_CHAR(x.VERSION_MEDIDA) \"No. Medida\", GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(X.TIPO_MEDIDA) \"Tipo Medida\",\n" +
           "X.NOMBRE_MEDIDA  \"Nombre Medida\", GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(X.ESTADO_MEDIDA) \"Estado\"\n" +
           "FROM GRTA_MEDIDAS X, GRTA_COMPENDIO_DETALLE COMPENDIO \n" +
           "WHERE  X.SUJETO_RIESGO=TO_NUMBER(:pSujetoRiesgo) AND X.CLASE_MEDIDA=TO_NUMBER(:pClaseMedida) AND (X.ESTADO_MEDIDA IN (39,40)\n" +
           "OR (X.ESTADO_MEDIDA=42) ) AND\n" +
           "COMPENDIO.ID_COMPENDIO=2 AND\n" +
           "COMPENDIO.ID_DETALLE=X.CLASE_MEDIDA AND\n" +
           "COMPENDIO.SUJETO_RIESGO=X.SUJETO_RIESGO AND\n" +
           "SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND DECODE(COMPENDIO.FECHA_FIN_VIGENCIA,'',SYSDATE,COMPENDIO.FECHA_FIN_VIGENCIA)\n" +
           "ORDER BY X.ID_MEDIDA DESC, X.VERSION_MEDIDA";

        //@author: RTrujillo; @version 2.0, 10/2009, @descripcion: Obtiene los valores del ambitos de la media: Aduana
        public const String AmbitoAduanaProcCombo = "SELECT REFERENCIA1 codigo, REFERENCIA1||'-'||ADUANA.DESCRIPCION aduanas FROM GRTA_PROCESO_AMBITOS AMBITOS, GRTA_COMPENDIO_DETALLE ADUANA WHERE AMBITOS.ID_PROCESO=TO_NUMBER(:pProceso) AND"
                   + " AMBITOS.TIPO_AMBITO=11 AND ADUANA.ID_COMPENDIO=35 AND ADUANA.REFERENCIA1=AMBITOS.VALOR_AMBITO AND SYSDATE BETWEEN ADUANA.FECHA_INICIO_VIGENCIA AND NVL(ADUANA.FECHA_FIN_VIGENCIA, SYSDATE)";

        //@author: RTrujillo; @version 2.0, 10/2009, @descripcion: Obtiene los valores del ambitos de la media: Regimen
        public const String AmbitoRegimenProcCombo = "SELECT REFERENCIA1 codigo, REFERENCIA1||'-'||REGIMEN.DESCRIPCION grupo_regimen FROM GRTA_PROCESO_AMBITOS AMBITOS, GRTA_COMPENDIO_DETALLE REGIMEN WHERE AMBITOS.ID_PROCESO=TO_NUMBER(:pProceso)"
                   + " AND AMBITOS.TIPO_AMBITO=31709 AND REGIMEN.ID_COMPENDIO=48 AND REGIMEN.REFERENCIA1=AMBITOS.VALOR_AMBITO AND SYSDATE BETWEEN REGIMEN.FECHA_INICIO_VIGENCIA AND NVL(REGIMEN.FECHA_FIN_VIGENCIA,SYSDATE)";

        //@author: Ocamacho: actualización de la consulta:
        public const String MedidaSeleccionadasTabla = " SELECT EVALUACION.INDICADOR_SELECCION \"Opcion\",EVALUACION.ID_MEDIDA||'-'||EVALUACION.VERSION_MEDIDA \"No Medida\", \n" +
           " GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MEDIDAS.TIPO_MEDIDA) \"Tipo Medida\",\n" +
           " MEDIDAS.NOMBRE_MEDIDA \"Nombre Medida\", GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MEDIDAS.ESTADO_MEDIDA) \"Estado\"\n" +
           " FROM GRTA_EVALUACION_MEDIDA EVALUACION, GRTA_MEDIDAS MEDIDAS\n" +
           " WHERE\n" +
           " EVALUACION.ID_EVALUACION=TO_NUMBER(:pTesteo) AND\n" +
           " MEDIDAS.ID_MEDIDA=EVALUACION.ID_MEDIDA AND\n" +
           " MEDIDAS.VERSION_MEDIDA=EVALUACION.VERSION_MEDIDA\n" +
           " ORDER BY EVALUACION.INDICADOR_SELECCION DESC,MEDIDAS.ID_MEDIDA";

        //@author: RTrujillo; @version 2.0, 10/2009, @descripcion: Obtiene los valores generales del proceso de evaluacion
        public const String ProcesoEvaluacionGeneral = " SELECT EVALUACION.ID_EVALUACION TESTEO,(SELECT DESCRIPCION_BREVE FROM GRTA_SUJETO_RIESGO WHERE SUJETO_RIESGO=EVALUACION.SUJETO_RIESGO) SUJETO,\n" +
           " GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(CLASE_MEDIDA) CLASE_MEDIDA," +
               " TO_CHAR(EVALUACION.FECHA_INICIO_ANALISIS,'DD/MM/YYYY') FECHA_INICIO, TO_CHAR(EVALUACION.FECHA_FIN_ANALISIS,'DD/MM/YYYY') FECHA_FIN,\n" +
           " EVALUACION.ESTADO_EVALUACION, GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(EVALUACION.ESTADO_EVALUACION) ESTADO,\n" +
           " UPPER(EVALUACION.DESCRIPCION) descripcion, NVL(GRPK_OPERACIONES_MEDIDA.GRFN_CONSULTAR_EXPRESIO_FILTRO(EVALUACION.ID_EVALUACION),' ') filtros \n" +
           " FROM GRTA_EVALUACION EVALUACION\n" +
           " WHERE\n" +
           " EVALUACION.ID_EVALUACION= TO_NUMBER(?)";

        //Ocamacho 11JUN
        public const String ResultadoGenProcesoEvalTabla = " SELECT UPPER(GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(RESPUESTA.CANAL_SELECTIVIDAD)) \"Canal\", \n" +
           " TO_CHAR(TRUNC(COUNT(DISTINCT PERFIL.IDENTIFICADOR_DOCUMENTO)*100/ \n" +
           " (SELECT COUNT(ID_PERFIL) FROM GRTA_DOCUMENTO_PERFIL_PROC where ID_EVALUACION=PERFIL.ID_EVALUACION),1))||'%' \"%Canal\", \n" +
           " COUNT(DISTINCT PERFIL.IDENTIFICADOR_DOCUMENTO) \"# Sujetos\",\n" +
           " ROUND(SUM(CASE MAESTRO.TIPO_MEDIDA WHEN 17 THEN 1 ELSE 0 END)*100/COUNT(MEDIDAS.DOCUMENTO_MEDIDA),1)||'%' \"% Criterio Experto\", \n" +
           " ROUND(SUM(CASE MAESTRO.TIPO_MEDIDA WHEN 18 THEN 1 ELSE 0 END)*100/COUNT(MEDIDAS.DOCUMENTO_MEDIDA),1)||'%' \"% Probabilístico\",\n" +
           " ROUND(SUM(CASE MAESTRO.TIPO_MEDIDA WHEN 21767 THEN 1 ELSE 0 END)*100/COUNT(MEDIDAS.DOCUMENTO_MEDIDA),1)||'%' \"% Red Neuronal\",\n" +
           " ROUND(SUM(CASE MAESTRO.TIPO_MEDIDA WHEN 19 THEN 1 ELSE 0 END)*100/COUNT(MEDIDAS.DOCUMENTO_MEDIDA),1)||'%' \"% M.Excepción\"\n" +
           " from GRTA_DOCUMENTO_PERFIL_PROC PERFIL, GRTA_DOCUMENTO_LINEA_PROC LINEA, GRTA_DOCUMENTO_MEDIDAS_PROC MEDIDAS, GRTA_DOCUMENTO_RESPUESTA_EVAL RESPUESTA, GRTA_MEDIDAS MAESTRO \n" +
           " where \n" +
           " PERFIL.ID_EVALUACION=TO_NUMBER(:pTesteo) AND \n" +
           " LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND\n" +
           " MEDIDAS.DOCUMENTO_LINEA=LINEA.DOCUMENTO_LINEA AND\n" +
           " MEDIDAS.RESULTADO_MEDIDA=9296 AND \n" +
           " RESPUESTA.ID_PERFIL=PERFIL.ID_PERFIL AND \n" +
           " RESPUESTA.NIVEL_RESPUESTA=540 AND\n" +
           " MAESTRO.ID_MEDIDA=MEDIDAS.ID_MEDIDA AND\n" +
           " MAESTRO.VERSION_MEDIDA=MEDIDAS.VERSION_MEDIDA          \n" +
           " GROUP BY RESPUESTA.CANAL_SELECTIVIDAD,PERFIL.ID_EVALUACION      \n" +
           " UNION\n" +
           " SELECT 'Sin Canal' \"Canal\",TO_CHAR(TRUNC(COUNT(PERF.ID_PERFIL)*100/ \n" +
           " (SELECT COUNT(ID_PERFIL) FROM GRTA_DOCUMENTO_PERFIL_PROC where ID_EVALUACION=PERF.ID_EVALUACION),1))||'%' \"%Canal\", \n" +
           " COUNT(DISTINCT PERF.IDENTIFICADOR_DOCUMENTO) \"# Sujetos\", '0%' \"% Criterio Experto\",'0%' \"% Probabilístico\", '0%' \"% Red Neuronal\", '0%' \"% M.Excepción\" \n" +
           " FROM GRTA_DOCUMENTO_PERFIL_PROC PERF\n" +
           " WHERE\n" +
           " PERF.ID_EVALUACION=TO_NUMBER(:pTesteo) AND \n" +
           " PERF.ID_PERFIL NOT IN \n" +
           " (\n" +
           "     SELECT PERFIL.ID_PERFIL FROM \n" +
           "     GRTA_DOCUMENTO_PERFIL_PROC PERFIL, GRTA_DOCUMENTO_RESPUESTA_EVAL RESPUESTA\n" +
           "     WHERE \n" +
           "     PERFIL.ID_EVALUACION=TO_NUMBER(:pTesteo) AND\n" +
           "     RESPUESTA.NIVEL_RESPUESTA=540 AND       \n" +
           "     RESPUESTA.ID_PERFIL=PERFIL.ID_PERFIL \n" +
           " )\n" +
           " GROUP BY PERF.ID_EVALUACION \n" +
           " ORDER BY 2 DESC  ";

        //@author: RTrujillo; @version 2.0, 10/2009, @descripcion:
        public const String ResultadoEspecificoProcEvalTabla = "SELECT TIPO.DESCRIPCION \"T.Medida\",\n" +
                                       "COUNT(DISTINCT PERFIL.ID_DECLARACION) \"DMs.Coincid.\",\n" +
                                       "COUNT(DISTINCT (CASE MEDIDAS.RESULTADO_MEDIDA WHEN 230 THEN NULL ELSE PERFIL.ID_DECLARACION END)) \"DMs.Aplic.\",\n" +
                                       "COUNT(DISTINCT (CASE MEDIDAS.RESULTADO_MEDIDA WHEN 232 THEN PERFIL.ID_DECLARACION ELSE NULL END)) \"DMs.Predom.\",\n" +
                                       "TO_CHAR(TRUNC(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,DECODE(MEDIDAS.NIVEL_REFERENCIA,6,PERFIL.ID_DECLARACION,NULL),NULL))*100\n" +
                                       "/(COUNT(DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,PERFIL.ID_DECLARACION,NULL))),1))||'%' \"%Rojo\",\n" +
                                       "TO_CHAR(TRUNC(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,DECODE(MEDIDAS.NIVEL_REFERENCIA,7,PERFIL.ID_DECLARACION,NULL),NULL))*100\n" +
                                       "/(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,PERFIL.ID_DECLARACION,NULL))),1))||'%' \"%Amar.\",\n" +
                                       "TO_CHAR(TRUNC(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,DECODE(MEDIDAS.NIVEL_REFERENCIA,8,PERFIL.ID_DECLARACION,NULL),NULL))*100\n" +
                                       "/(COUNT(DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,PERFIL.ID_DECLARACION,NULL))),1))||'%' \"%Verde\"\n" +
                                       "from grta_declara_perfil_tmp perfil, grta_declara_linea_tmp linea, grta_declara_medidas_tmp medidas,\n" +
                                       "GRTA_MEDIDAS maestro, GRTA_COMPENDIO_DETALLE tipo\n" +
                                       "where perfil.id_proceso=:pProceso AND \n" +
                                       "LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND \n" +
                                       "MEDIDAS.ID_PERFIL=LINEA.ID_PERFIL AND \n" +
                                       "MEDIDAS.SECUENCIA_LINEA=LINEA.SECUENCIA_LINEA AND\n" +
                                       "MAESTRO.ID_MEDIDA=MEDIDAS.ID_MEDIDA AND \n" +
                                       "MAESTRO.VERSION_MEDIDA=MEDIDAS.VERSION_MEDIDA AND \n" +
                                       "TIPO.ID_COMPENDIO=7 AND TIPO.ID_DETALLE=MAESTRO.TIPO_MEDIDA\n" +
                                       "GROUP BY TIPO.DESCRIPCION";

        //@author: RTrujillo; @version 2.0, 10/2009, @descripcion:
        public const String ResultadoEspecificoProcEvalTabla2 = "SELECT to_char(medidas.ID_MEDIDA)||'-'||to_char(medidas.VERSION_MEDIDA) \"Medida\",\n" +
           " TIPO.DESCRIPCION \"T.Medida\", maestro.NOMBRE_MEDIDA \"Nombre\",\n" +
           " COUNT(DISTINCT PERFIL.ID_DECLARACION) \"DMs.Coincid.\",\n" +
           " COUNT(DISTINCT (CASE MEDIDAS.RESULTADO_MEDIDA WHEN 230 THEN NULL ELSE PERFIL.ID_DECLARACION END)) \"DMs.Aplic.\",\n" +
           " COUNT(DISTINCT (CASE MEDIDAS.RESULTADO_MEDIDA WHEN 232 THEN PERFIL.ID_DECLARACION ELSE NULL END)) \"DMs.Predom.\",                                                         \n" +
           " TO_CHAR(TRUNC(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,DECODE(MEDIDAS.NIVEL_REFERENCIA,6,PERFIL.ID_DECLARACION,NULL),NULL))*100\n" +
           " /(DECODE(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,PERFIL.ID_DECLARACION,NULL)),0,1,COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,PERFIL.ID_DECLARACION,NULL)))),1))||'%' \"%Rojo\",\n" +
           " TO_CHAR(TRUNC(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,DECODE(MEDIDAS.NIVEL_REFERENCIA,7,PERFIL.ID_DECLARACION,NULL),NULL))*100\n" +
           " /(DECODE(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,PERFIL.ID_DECLARACION,NULL)),0,1,COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,PERFIL.ID_DECLARACION,NULL)))),1))||'%' \"%Amar\",\n" +
           " TO_CHAR(TRUNC(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,DECODE(MEDIDAS.NIVEL_REFERENCIA,8,PERFIL.ID_DECLARACION,NULL),NULL))*100\n" +
           " /(DECODE(COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,PERFIL.ID_DECLARACION,NULL)),0,1,COUNT( DISTINCT DECODE(MEDIDAS.RESULTADO_MEDIDA,232,PERFIL.ID_DECLARACION,NULL)))),1))||'%' \"%Verde\"\n" +
           " --/(DECODE(SUM(DECODE(MEDIDAS.RESULTADO_MEDIDA,232,1,0)),0,1,SUM(DECODE(MEDIDAS.RESULTADO_MEDIDA,232,1,0)))),1))||'%' \"%Verde\"\n" +
           " from grta_declara_perfil_tmp perfil, grta_declara_linea_tmp linea, grta_declara_medidas_tmp medidas,\n" +
           " GRTA_MEDIDAS maestro, GRTA_COMPENDIO_DETALLE tipo\n" +
           " where perfil.id_proceso=:pProceso AND LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND MEDIDAS.ID_PERFIL=LINEA.ID_PERFIL AND\n" +
           " MEDIDAS.SECUENCIA_LINEA=LINEA.SECUENCIA_LINEA AND MAESTRO.ID_MEDIDA=MEDIDAS.ID_MEDIDA AND MAESTRO.VERSION_MEDIDA=MEDIDAS.VERSION_MEDIDA AND\n" +
           " TIPO.ID_COMPENDIO=7 AND TIPO.ID_DETALLE=MAESTRO.TIPO_MEDIDA\n" +
           " GROUP BY TO_CHAR(medidas.ID_MEDIDA)||'-'||TO_CHAR(medidas.VERSION_MEDIDA),TIPO.DESCRIPCION, maestro.NOMBRE_MEDIDA";

        //-----------------------------------------------------------------------------------------------------------------

        // Recupera el detalle de la politica SQL
        public const String PoliticaDetalle = "SELECT POLITICA.descripcion_breve, POLITICA.descripcion_completa, \n" +
           "(SELECT COUNT(0) FROM GRTA_POLITICA_ARCHIVOS ARCHIVOS WHERE ARCHIVOS.id_politica=POLITICA.id_politica AND FECHA_FIN_VIGENCIA IS NULL) id_archivo,    \n" +
           "TO_CHAR(POLITICA.fecha_inicio_vigencia,'DD/MM/YYYY') fecha_inicio_vigencia, TO_CHAR(POLITICA.fecha_fin_vigencia,'DD/MM/YYYY') fecha_fin_vigencia, \n" +
           "TO_CHAR(POLITICA.fecha_aprobacion,'DD/MM/YYYY') fecha_aprobacion,\n" +
           "PRECEDENTE.id_politica id_politicapres,NVL(TO_CHAR(PRECEDENTE.id_politica) || '-' || PRECEDENTE.descripcion_breve,'') descripcion_precedente \n" +
           "FROM GRTA_POLITICA_INSTITUCIONAL POLITICA\n" +
           "LEFT JOIN GRTA_POLITICA_INSTITUCIONAL PRECEDENTE ON PRECEDENTE.id_politica=POLITICA.politica_precedente                                  \n" +
           "WHERE POLITICA.id_politica=TO_NUMBER(?)";

        // Recupera los posibles controles donde se va introducir el valor del componente SQL
        public const String ControlOperador = "SELECT indicador_suministrado,indicador_parametro,indicador_lista,indicador_sin_valor" +
                            " FROM GRTA_OPERADOR_MATEMATICO" +
                               " WHERE codigo_operador = TO_NUMBER(?)";

        // Recupera el código y la descripción de los organos que formulan SQL
        public const String OrganoFomulaCombo = "SELECT id_detalle, codigo_alterno descripcion" +
                              " FROM GRTA_COMPENDIO_DETALLE" +
                                 " WHERE id_compendio = 23 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                                 " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el código y la descripción de los tipos de reglas Fijas SQL
        public const String TipoReglaCombo = "SELECT id_detalle, referencia1" +
                              " FROM GRTA_COMPENDIO_DETALLE" +
                              " WHERE id_compendio = '2' AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                              " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el código y la descripción de los tipos de variables en regla fija SQL
        public const String VariableConsulta = "SELECT codigo_variable, descripcion_breve descripcion" +
                           " FROM GRTA_VARIABLES" +
                           " WHERE SYSDATE BETWEEN fecha_inicio_vigencia" +
                           " AND NVL(fecha_fin_vigencia,SYSDATE)" +
                           " AND tipo_variable=57 AND modo_uso=22 AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' ORDER BY 2";

        // Recupera el código y la descripción de los tipos de variables en regla fija SQL
        public const String VariableCombo = "SELECT codigo_variable, descripcion_breve descripcion" +
                          " FROM GRTA_VARIABLES" +
                             " WHERE SYSDATE BETWEEN fecha_inicio_vigencia" +
                             " AND NVL(fecha_fin_vigencia,SYSDATE)" +
                             " AND clase_variable=57 AND modo_uso=22" +
                             " AND sujeto_riesgo=TO_NUMBER(?) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' ORDER BY 2";
        // Recupera la descripcion del canal de selectividad SQL
        public const String DescCanal = "SELECT referencia1,referencia2" +
                         " FROM GRTA_COMPENDIO_DETALLE" +
                         " WHERE id_compendio = 3 and SYSDATE BETWEEN fecha_inicio_vigencia" +
                         " AND NVL(fecha_fin_vigencia,SYSDATE)" +
                         " AND id_detalle = TO_NUMBER(?)";
        // Recupera el código y la descripción de los tipos de variables en regla fija SQL
        /*Usado WMarcia 29-01-2018*/
        public const String VariableCalCombo = "SELECT CODIGO_VARIABLE codigo, (CASE WHEN LENGTH(DESCRIPCION_BREVE||EXPRESION_CONSOLIDACION)>90 THEN " +
                                          " SUBSTR(DESCRIPCION_BREVE||': '||EXPRESION_CONSOLIDACION,1,90)||'..' ELSE DESCRIPCION_BREVE||': '||EXPRESION_CONSOLIDACION END) descripcion" +
                                " FROM GRTA_VARIABLES" +
                                " WHERE SYSDATE BETWEEN fecha_inicio_vigencia" +
                                " AND NVL(fecha_fin_vigencia,SYSDATE)" +
                                " AND tipo_variable=59 AND sujeto_riesgo=?";
        // Recupera el código y la descripción de los Operadores Matematicos SQL
        public const String OperadorCombo = "SELECT codigo_operador,simbolo_usuario" +
                                                    " FROM GRTA_OPERADOR_MATEMATICO" +
                                                    " WHERE codigo_operador NOT IN(10,11,12,13)";

        // Recupera el código y la descripción de las listas de valores SQL
        public const String ListaGeneralCombo = "SELECT id_lista,descripcion" +
           " FROM GRTA_LISTA_GENERAL" +
           " WHERE SYSDATE BETWEEN fecha_inicio_vigencia" +
           " AND NVL(fecha_fin_vigencia,SYSDATE)";

        // Recupera el código y la descripción de las listas de parametros SQL
        public const String ParametroCombo = "SELECT id_parametro,descripcion_breve" +
           " FROM GRTA_PARAMETRO" +
           " WHERE SYSDATE BETWEEN fecha_inicio_vigencia" +
           " AND NVL(fecha_fin_vigencia, SYSDATE)";

        // Recupera el código y la descripción de las listas de parametros SQL
        public const String ParametroSuministrado = "SELECT id_parametro,descripcion_breve" +
           " FROM GRTA_PARAMETROS" +
           " WHERE CLASE_PARAMETRO=62 AND" +
           " SYSDATE BETWEEN fecha_inicio_vigencia" +
           " AND NVL(fecha_fin_vigencia, SYSDATE)";

        // Recupera el código y la descripción de los canales de Selectividad
        public const String CanalCombo = "SELECT t.ID_DETALLE, t.REFERENCIA2 FROM GRTA_COMPENDIO_DETALLE t " +
           "WHERE t.ID_COMPENDIO=3 " +
           "AND id_detalle <> 10 AND SYSDATE BETWEEN t.FECHA_INICIO_VIGENCIA AND NVL(t.FECHA_FIN_VIGENCIA,SYSDATE)";


        // Recupera el código y la descripción de los canales de Selectividad sin el Azul SQL
        public const String CanalComboSinAzul = "SELECT id_detalle,referencia2" +
           " FROM GRTA_COMPENDIO_DETALLE" +
           " WHERE id_compendio = 3 and SYSDATE BETWEEN fecha_inicio_vigencia" +
           " AND NVL(fecha_fin_vigencia, SYSDATE)" +
           " AND id_detalle NOT IN (9,10)";



        // Recupera el código y la descripción de los Tipos de Valores SQL
        /*MgrTipoValorCombo="SELECT id_detalle,descripcion" + 
        " FROM GRTA_COMPENDIO_DETALLE" + 
        " WHERE id_compendio = 13 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
        " AND NVL(fecha_fin_vigencia,SYSDATE)"),
        */
        //Recupera los valores de referencia de los canales para la validacion
        public const String TempNivelRefTabla = "SELECT DISTINCT t.nivel_referencia cod_canal, c.descripcion desc_canal, t.porcentaje_esperado porcentaje,'' operacion " +
                                "FROM GRTA_TMP_NIVEL_REF_MED t, GRTA_COMPENDIO_DETALLE c " +
                                "WHERE t.nivel_referencia=c.id_detalle AND c.id_compendio=3 AND t.id_medida = ? AND t.version_medida = ?";

        // Recupera la expresion logica que se va formando
        public const String ExpresionLogica = "SELECT desc_usuario" +
                              " FROM GRTA_TMP_REGLAFIJA" +
                                 " WHERE id_medida = TO_NUMBER(:pIdMedida) AND version_medida = TO_NUMBER(:pVersion) AND codigo_componente = (SELECT MAX(codigo_componente)" +
                                 " FROM GRTA_TMP_REGLAFIJA" +
                                 " WHERE id_medida = TO_NUMBER(:pIdMedida) AND version_medida = TO_NUMBER(:pVersion) )";

        // Recupera los registros de los niveles de referencia o canales de selectividad de las medidas    		   			                        
        public const String NivelRefAllTabla = "SELECT u.referencia1 Código,u.referencia2 Descripción,t.porcentaje_esperado \"% Rev.\"," +
                               "NVL((CASE t.orden_nivel WHEN 1 THEN '<'||TO_CHAR(TRUNC(t.rango_base,3))||' ; ' ||TO_CHAR(TRUNC(t.rango_techo,3))|| ']' ELSE '[' ||TO_CHAR(TRUNC(t.rango_base,3)) || ' ; ' || TO_CHAR(TRUNC(t.rango_techo,3)) || '>' END),'') \"[Desde;Hasta]\" " +
                               "FROM GRTA_NIVEL_REFERENCIA_MEDIDA t, GRTA_COMPENDIO_DETALLE u " +
                               "WHERE t.id_medida = TO_NUMBER(:pIdMedida) AND t.version_medida = TO_NUMBER(:pVersion) AND u.id_compendio = 3 AND u.id_detalle = t.nivel_referencia " +
                               "ORDER BY  t.orden_nivel";
        // Recupera los registros de los niveles de referencia o canales de selectividad de las medidas    		   			                        
        public const String NivelRefRetAllTabla = "SELECT u.referencia1 Código,u.referencia2 Descripción,t.porcentaje_esperado \"% Rev.\"," +
                               "FROM GRTA_RET_NIVEL_REF_MED t, GRTA_COMPENDIO_DETALLE u " +
                               "WHERE t.id_medida = TO_NUMBER(:pIdMedida) AND t.version_medida = TO_NUMBER(:pVersion) AND u.id_compendio = 3 AND u.id_detalle = t.nivel_referencia " +
                               "ORDER BY  t.orden_nivel";
        // Recupera la sumatoria de los canales ingresados temporalmente SQL
        public const String CanalSuma = "SELECT NVL(SUM(DISTINCT porcentaje_esperado),0) suma " +
                      " FROM GRTA_TMP_NIVEL_REF_MED WHERE id_medida = TO_NUMBER(?) AND version_medida = TO_NUMBER(?)";


        // Recupera las variables predictivas de un modelo
        public const String VarPredictivasTabla = " SELECT distinct 0 \"Coeficiente\",u.descripcion \"Descripcion\" " +
           " FROM GRTA_MEDIDAS m,GRTA_ENTIDADES_MEDIDA t,GRTA_VARIABLES u" +
           " WHERE m.id_medida = :pIdMedida AND m.version_medida = :pVersion AND m.estado_medida < 41 AND t.id_medida=m.id_medida" +
           " AND u.codigo_variable = t.codigo_variable AND u.clase_variable=58" +
           " AND u.tipo_variable=60" +
           " UNION" +
           " SELECT DISTINCT v.valor_coeficiente \"Coeficiente\",u.descripcion \"Descripcion\" \n" +
           "             FROM GRTA_ENTIDADES_MEDIDA t,GRTA_VARIABLES u,GRTA_TERMINOS_PROBABILISTICO v\n" +
           "             WHERE t.id_medida = 684 AND t.version_medida = :pVersion AND \n" +
           "             v.id_medida = t.id_medida AND v.version_medida = t.version_medida AND V.ID_ENTIDAD=T.ID_ENTIDAD AND\n" +
           "             u.codigo_variable = t.codigo_variable AND u.clase_variable=58 AND u.tipo_variable=60 ORDER BY 1 desc";

        // Recupera la lista de Las Aduanas (Ambito 11) de una medida de control SQL
        public const String AduanaMedidaCombo = "SELECT  t.valor_ambito secuencia,t.valor_ambito || '-' || u.descripcion nombre_Aduana" +
           " FROM GRTA_AMBITOS_MEDIDA t, GRTA_COMPENDIO_DETALLE u" +
           " WHERE u.id_compendio=35 AND t.valor_ambito=u.referencia1 AND t.id_medida=TO_NUMBER(:pIdMedida) AND t.version_medida = TO_NUMBER(:pVersion)";

        // Recupera la lista de los regimen (Ambito 11) de una medida de control SQL   
        public const String RegimenMedidaCombo = "SELECT t.valor_ambito secuencia, u.referencia1||'-'||u.descripcion nombre_regimen" +
           " FROM GRTA_AMBITOS_MEDIDA t, GRTA_COMPENDIO_DETALLE u" +
           " WHERE t.id_medida=TO_NUMBER(:pIdMedida) AND t.version_medida = TO_NUMBER(:pVersion) AND u.id_compendio=48 AND u.referencia1=t.valor_ambito";

        // @RTrujillo_2012 | Modificado el 04/07/2012
        // Recupera el registro de la medida solicitada
        /*Usado en Nueva Versión/ Botoón Detalle*/
        public const String MedidaDetalle = "SELECT M.ID_MEDIDA, M.VERSION_MEDIDA," +
                           " M.SUJETO_RIESGO id_sujeto_riesgo," +
                           " (SELECT SR.DESCRIPCION_BREVE FROM GRTA_SUJETO_RIESGO  sr WHERE SR.SUJETO_RIESGO  = M.SUJETO_RIESGO) sujeto_riesgo," +
                           " M.TIPO_MEDIDA id_tipo," +
                           " (SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=7 AND REFERENCIA1=1 AND ID_DETALLE = M.TIPO_MEDIDA ) tipo," +
                           " M.ESTADO_MEDIDA id_estado," +
                           " (SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=12 AND REFERENCIA1=1 AND ID_DETALLE = M.ESTADO_MEDIDA) estado," +
                           " M.ID_POLITICA," +
                           " (SELECT DESCRIPCION_BREVE FROM GRTA_POLITICA_INSTITUCIONAL WHERE ID_POLITICA = M.ID_POLITICA) politica," +
                           " M.CLASE_MEDIDA id_clase," +
                           " (SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=2 AND SUJETO_RIESGO= M.SUJETO_RIESGO AND ID_DETALLE = M.CLASE_MEDIDA) clase," +
                           "  M.JERARQUIA_MEDIDA id_jerarquia," +
                           " (SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=1 AND ID_DETALLE = M.JERARQUIA_MEDIDA) jerarquia," +
                           " M.NOMBRE_MEDIDA," +
                           " NVL( (SELECT DISTINCT v.CODIGO_VARIABLE FROM GRTA_VARIABLES v, GRTA_ENTIDADES_MEDIDAS e WHERE v.CODIGO_VARIABLE = E.CODIGO_VARIABLE AND TIPO_VARIABLE = 58 AND E.ID_MEDIDA = M.ID_MEDIDA AND E.VERSION_MEDIDA = M.VERSION_MEDIDA ), '0' ) id_variable_dependiente," +
                           " NVL( (SELECT DISTINCT v.DESCRIPCION_BREVE FROM GRTA_VARIABLES v, GRTA_ENTIDADES_MEDIDAS e WHERE v.CODIGO_VARIABLE = E.CODIGO_VARIABLE AND TIPO_VARIABLE=58 AND E.ID_MEDIDA = M.ID_MEDIDA AND E.VERSION_MEDIDA = M.VERSION_MEDIDA ), ' ' ) variable_dependiente," +
                           " NVL( TO_CHAR( M.FECHA_INICIO_ANALISIS, 'DD/MM/YYYY' ), ' ' ) FECHA_INICIO_ANALISIS, NVL( TO_CHAR(M.FECHA_FIN_ANALISIS, 'DD/MM/YYYY'), ' ' ) FECHA_FIN_ANALISIS," +
                           " NVL( TO_CHAR( M.VALOR_FRECUENCIA ), ' ') frecuencia," +
                           " M.TIPO_FRECUENCIA id_tipo_frecuencia," +
                           " NVL( (SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=36 AND ID_DETALLE = M.TIPO_FRECUENCIA), ' ' ) TIPO_FRECUENCIA," +
                           " NVL(TO_CHAR( M.FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY' ),' ') FECHA_INICIO_VIGENCIA, NVL( TO_CHAR( M.FECHA_FIN_VIGENCIA, 'DD/MM/YYYY HH12:MI:SS AM' ), ' ' ) FECHA_FIN_VIGENCIA," +
                           " M.DESCRIPCION," +
                           " GRPK_OPERACIONES_COMUNES.GRFN_CONSULTAR_EXPRESIO_FILTRO( M.ID_MEDIDA, M.VERSION_MEDIDA, NULL, 'P') filtros_medida," +
                           " NVL( TO_CHAR( M.MEDIDA_PRECEDENTE ), ' ' ) MEDIDA_PRECEDENTE, NVL( TO_CHAR( M.VERSION_PRECEDENTE ), ' ' ) VERSION_PRECEDENTE," +
                           " NVL( (SELECT PRECEDENTE.NOMBRE_MEDIDA FROM GRTA_MEDIDAS precedente WHERE PRECEDENTE.ID_MEDIDA = M.MEDIDA_PRECEDENTE AND PRECEDENTE.VERSION_MEDIDA = M.VERSION_PRECEDENTE ), ' ' ) NOMBRE_PRECEDENTE," +
                           " ( SELECT COUNT( C.CONDICION_MEDIDAS)  FROM GRTA_CONDICION_MEDIDAS c WHERE C.ID_MEDIDA = M.ID_MEDIDA AND C.VERSION_MEDIDA = M.VERSION_MEDIDA) cant_condiciones," +
                           " ( SELECT COUNT( O.ORIENTACION_MEDIDAS) FROM GRTA_ORIENTACION_MEDIDAS o WHERE O.ID_MEDIDA = M.ID_MEDIDA AND O.VERSION_MEDIDA = M.VERSION_MEDIDA) cant_banderas," +
                           " ( SELECT COUNT(t.MENSAJE_MEDIDAS) FROM GRTA_MENSAJE_MEDIDAS t WHERE t.ID_MEDIDA = M.ID_MEDIDA AND t.VERSION_MEDIDA = M.VERSION_MEDIDA ) cant_destinatarios_correo," +
                           " ( SELECT COUNT(0) FROM GRTA_OPERACIONES_MEDIDAS t WHERE t.ID_MEDIDA = m.ID_MEDIDA AND t.VERSION_MEDIDA = m.VERSION_MEDIDA AND t.TIPO_OPERACION = 511 ) cant_operaciones_prob," +
                           " NVL( TO_CHAR( M.RITMO_APRENDIZAJE ), ' ') RITMO_APRENDIZAJE," +
                           " NVL( TO_CHAR( M.TERMINO_MOMENTO ), ' ') TERMINO_MOMENTO," +
                           " M.FUNCION_ACTIVACION ID_FUNCION_ACTIVACION," +
                           " NVL( ( SELECT  NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE = M.FUNCION_ACTIVACION AND ID_COMPENDIO=274), ' ' ) funcion_activacion" +
                           " FROM GRTA_MEDIDAS m" +
                           " WHERE M.ID_MEDIDA = TO_NUMBER(:pIdMedida)" +
                           " AND M.VERSION_MEDIDA = TO_NUMBER(:pVersionMedida)";
        // Recupera los canales de las medida retenida                     
        public const String CanalesRetenidos = "SELECT CANAL.nivel_referencia,COMPENDIO.referencia1,COMPENDIO.descripcion,CANAL.porcentaje_esperado " +
                                  "FROM GRTA_RET_NIVEL_REF_MED CANAL,GRTA_COMPENDIO_DETALLE COMPENDIO " +
                                  "WHERE CANAL.nivel_referencia = COMPENDIO.id_detalle AND" +
                                  " CANAL.id_medida = TO_NUMBER(:pIdMedida) AND " +
                                  " CANAL.version_medida = TO_NUMBER(:pVersion) " +
                                  "ORDER BY orden_nivel";
        // Recupera los registros de los niveles de referencia o canales de selectividad de las medidas    		   			                        
        public const String NivelRefAll = "SELECT t.nivel_referencia,u.referencia1,u.descripcion Descripción,t.porcentaje_esperado " +
                           "FROM GRTA_NIVEL_REFERENCIA_MEDIDA t, GRTA_COMPENDIO_DETALLE u " +
                           "WHERE t.id_medida = TO_NUMBER(:pIdMedida) AND t.version_medida = TO_NUMBER(:pVersion) AND u.id_compendio = 3 AND u.id_detalle = t.nivel_referencia " +
                           "ORDER BY  t.orden_nivel";
        // Recupera el codigo y la descripcion de los tipos de datos
        /*No usado WMarcia 17-01-2018 por cambio de forma*/
        public const String TipoDatoCombo = "SELECT id_detalle,nombre" +
                            " FROM GRTA_COMPENDIO_DETALLE" +
                            " WHERE id_compendio = 28 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                            " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el codigo y la descripcion de los tipos de parametros
        /*No usado WMarcia 21-01-2018 por cambio de forma*/
        public const String ClaseParametroCombo = "SELECT id_detalle,nombre descripcion " +
                                  "FROM GRTA_COMPENDIO_DETALLE " +
                                  "WHERE id_compendio = 22 AND SYSDATE BETWEEN fecha_inicio_vigencia " +
                                  "AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el detalle del parametro seleccionado por el usuario , Funcion Convertida: GRFN_NOMBRE_CODIGO_VARIABLE, GRFN_NOMBRE_SUJETO_RIESGO
        public const String ParametroDetalle = "SELECT PARAM.id_parametro,PARAM.clase_parametro codclaseparam,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(PARAM.clase_parametro) claseparam,NVL(PARAM.valor_suministrado,' ') valor_suministrado,NVL(TO_CHAR(PARAM.valor_codigo_variable),' ') codvalorvar," +
                               "CASE WHEN PARAM.valor_codigo_variable IS NULL THEN 'Indefinido' ELSE UPPER(VAR.DESCRIPCION_BREVE) END valorvar,NVL(TO_CHAR(PARAM.sujeto_riesgo),' ') codsujriesgo,CASE WHEN PARAM.sujeto_riesgo IS NULL THEN 'Indefinido' ELSE UPPER(SUJETO_RIES.DESCRIPCION_BREVER) sujetoRiesgo,NVL(TO_CHAR(PARAM.valor_identificacion_sujeto),' ') identificacionSujeto," +
                               "GRPK_OPERACIONES_COMUNES.GRFN_DESCRIP_IDENTIFICACION(PARAM.VALOR_IDENTIFICACION_SUJETO) descIdentificador,PARAM.descripcion_breve," +
                               "TO_CHAR(PARAM.fecha_inicio_vigencia,'DD/MM/YYYY') fecha_inicio_vigencia,NVL(TO_CHAR(PARAM.fecha_fin_vigencia,'DD/MM/YYYY'),' ') fecha_fin_vigencia,PARAM.tipo_dato codTipoDato,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(PARAM.tipo_dato) tipoDato " +
                               "FROM GRTA_PARAMETROS PARAM RIGHT OUTER JOIN GRTA_IDENTIFICADOR_SUJETO SUJETO ON SUJETO.IDENTIFICACION_SUJETO=PARAM.VALOR_IDENTIFICACION_SUJETO" +
                               "LEFT JOIN GRTA_VARIABLES VAR ON VAR.CODIGO_VARIABLE=PARAM.valor_codigo_variable" +
                               "LEFT JOIN GRTA_SUJETO_RIESGO SUJETO_RIES ON SUJETO_RIES.SUJETO_RIESGO=PARAM.sujeto_riesgo " +
                               "WHERE PARAM.id_parametro = TO_NUMBER(?)";

        // Recupera el codigo especifico del sujeto de riesgo.           
        public const String IdentificadorSujeto = "SELECT sujeto_riesgo" +
                                  " FROM GRTA_IDENTIFICADOR_SUJETO" +
                                  " WHERE secuencia_identificacion = ?";
        // Recupera el codigo y la descripcion de los tipos de variable
        /*No usado WMarcia 17-01-2018 por cambio de forma*/
        public const String TipoVariableCombo = "SELECT id_detalle, nombre descripcion" +
                                " FROM GRTA_COMPENDIO_DETALLE" +
                                " WHERE id_compendio = 19 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                                " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el codigo y la descripcion de las clases de variables
        public const String ClaseVariableCombo = "SELECT id_detalle,referencia1 desc_clase" +
                                 " FROM GRTA_COMPENDIO_DETALLE" +
                                 " WHERE id_compendio = 18 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                                 " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el codigo y la descripcion de los modos de obtencion de variables
        /*No usado WMarcia 17-01-2018 por cambio de forma*/
        public const String ModoObtencionCombo = "SELECT id_detalle,nombre" +
                                 " FROM GRTA_COMPENDIO_DETALLE" +
                                 " WHERE id_compendio = 64 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                                 " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el codigo y la descripcion de los modos de uso de variables
        /*No usado WMarcia 17-01-2018 por cambio de forma*/
        public const String ModoUsoCombo = "SELECT id_detalle,nombre" +
                           " FROM GRTA_COMPENDIO_DETALLE" +
                           " WHERE id_compendio = 8 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                           " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el codigo y la descripcion de los numeros de ocurrencia
        /*No usado WMarcia 17-01-2018 por cambio de forma*/
        public const String NroOcurrenciaCombo = "SELECT id_detalle,nombre" +
                                 " FROM GRTA_COMPENDIO_DETALLE" +
                                 " WHERE id_compendio = 17 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                                 " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el codigo y la descripcion de las tablas del compendio general
        /*Usado WMarcia 17-01-2018*/
        public const String TblValidacion = "SELECT id_compendio codigo, nombre descripcion from GRTA_COMPENDIO_GENERAL where tipo_tabla=1";
        // Recupera el codigo y la descripcion de los Indicadores de Presencia
        /*No usado WMarcia 17-01-2018 por cambio de forma*/
        public const String IndPresenciaCombo = "SELECT id_detalle,nombre" +
                                " FROM GRTA_COMPENDIO_DETALLE" +
                                " WHERE id_compendio = 33 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                                " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el codigo y la descripcion de los Indicadores de Presencia
        public const String TablaPivotCombo = "SELECT tabla_pivot,nombre_tabla" +
                              " FROM GRTA_TABLA_PIVOT";
        // Recupera el id correspondiente a la variable del modelo probabilistico
        public const String EntidadId = "SELECT ENTIDADES.ID_ENTIDAD" +
                        " FROM GRTA_VARIABLES VARIABLE, GRTA_ENTIDADES_MEDIDA ENTIDADES" +
                        " WHERE UPPER(VARIABLE.DESCRIPCION)=UPPER(?) AND ENTIDADES.ID_MEDIDA=TO_NUMBER(?) AND ENTIDADES.VERSION_MEDIDA = TO_NUMBER(?) AND ENTIDADES.CODIGO_VARIABLE=VARIABLE.CODIGO_VARIABLE";
        //TODO Nuevas y Modificacicones realizadas
        // Recupera las variables que se usan para crear un parametro
        /*Usado WMarcia 10-04-2018*/
        public const String VariableParametroCombo = "SELECT codigo_variable codigo,descripcion_breve descripcion " +
                                     "FROM GRTA_VARIABLES " +
                                     "WHERE (sujeto_riesgo=? or ? is null) and tipo_variable=57 AND " +
                                     "SYSDATE BETWEEN fecha_inicio_vigencia AND COALESCE(fecha_fin_vigencia,SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' ORDER BY 2";
        //Variables para la consulta:   
        /* Usado en Consultar Perfil Casos Riesgo/ Combo Primer Filtro y Segundo Filtro*/
        public const String VariableConsultas = "SELECT CODIGO_VARIABLE CODIGO,DESCRIPCION_BREVE DESCRIPCION\n" +
                   "FROM GRTA_VARIABLES \n" +
                   "WHERE \n" +
                   "SUJETO_RIESGO=:pSujetoRiesgo AND TIPO_VARIABLE=57 AND\n" +
                   "TABLA_TRANSACCIONAL IS NOT NULL AND\n" +
                   "(TABLA_CODIFICACION IS NOT NULL OR VARIABLE_CATALOGO IS NOT NULL) AND\n" +
                   "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" +
                   "ORDER BY 2";

        //@Autor: WMarcia; @Version: 1.0, 10/11/2009; @Descripcion: Recupera los parametros
        //Función Convertida: GRFN_NOMBRE_ELEMENTO, GRFN_NOMBRE_SUJETO_RIESGO
        //TODO, por probar xq no tengo valores para los parametros
        public const String ParametroTabla = "SELECT PARAMETROS.ID_PARAMETRO id_parametro, " +
                   "PARAMETROS.CLASE_PARAMETRO cod_clase_parametro, CASE WHEN PARAMETROS.CLASE_PARAMETRO IS NULL THEN 'Indefinido' ELSE COMPENDIO_DETALLE.NOMBRE END clase_parametro, " +
                   "PARAMETROS.SUJETO_RIESGO cod_sujeto_riesgo, CASE WHEN PARAMETROS.SUJETO_RIESGO IS NULL THEN 'INDEFINIDO' ELSE UPPER(SUJETO_RIESGO.DESCRIPCION_BREVE) END sujeto_riesgo, " +
                   "PARAMETROS.DESCRIPCION_BREVE descripcion, PARAMETROS.FECHA_INICIO_VIGENCIA fecha_inicio_vigencia, PARAMETROS.FECHA_FIN_VIGENCIA fecha_fin_vigencia " +
                        "FROM GRTA_PARAMETROS PARAMETROS LEFT JOIN GRTA_COMPENDIO_DETALLE COMPENDIO_DETALLE ON COMPENDIO_DETALLE.ID_DETALLE=PARAMETROS.CLASE_PARAMETRO " +
                        "LEFT JOIN GRTA_SUJETO_RIESGO SUJETO_RIESGO ON SUJETO_RIESGO.SUJETO_RIESGO=PARAMETROS.SUJETO_RIESGO " +
                        "WHERE PARAMETROS.id_parametro = NVL(:pIdParametro, PARAMETROS.id_parametro) " +
                        "AND (PARAMETROS.SUJETO_RIESGO = :pSujetoRiesgo or :pSujetoRiesgo is null) " +
                        "AND PARAMETROS.clase_parametro = NVL(:pClaseParametro, PARAMETROS.clase_parametro) " +
                        "AND UPPER(PARAMETROS.descripcion_breve) LIKE '%'||NVL(:pDescripcion, UPPER(PARAMETROS.descripcion_breve))||'%'";
        // Recupera el detalle de la variable seleccionada: Ocamacho 23JUN.
        /*Usado WMarcia 30-01-2018*/
        public const String VariableDetalle = "SELECT CODIGO_VARIABLE,SUJETO_RIESGO, GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_SUJETO_RIESGO(SUJETO_RIESGO) NOMBRE_SUJETO,\n" +
                                   "TIPO_VARIABLE,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(TIPO_VARIABLE) NOMBRE_TIPO_VARIABLE,\n" +
                                   "DESCRIPCION_BREVE,TO_CHAR(FECHA_INICIO_VIGENCIA,'DD/MM/YYYY') FECHA_INICIO, TO_CHAR(FECHA_FIN_VIGENCIA,'DD/MM/YYYY') FECHA_FIN, \n" +
                                   "DESCRIPCION_COMPLETA,TIPO_DATO, GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(TIPO_DATO) NOMBRE_TIPO_DATO,\n" +
                                   "MODO_USO,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MODO_USO) NOMBRE_USO,\n" +
                                   "NUMERO_OCURRENCIAS, GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(NUMERO_OCURRENCIAS) NOMBRE_OCURRENCIAS,\n" +
                                   "INDICADOR_PRESENCIA, GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(INDICADOR_PRESENCIA) NOMBRE_INDICADOR,\n" +
                                   "MODO_OBTENCION,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MODO_OBTENCION) NOMBRE_OBTENCION,\n" +
                                   "TABLA_CODIFICACION, NVL((SELECT COMPENDIO.NOMBRE FROM GRTA_COMPENDIO_GENERAL COMPENDIO WHERE COMPENDIO.ID_COMPENDIO=TABLA_CODIFICACION),'Indefinido') NOMBRE_INTERNO,\n" +
                                   "VARIABLE_CATALOGO,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_CODIGO_VARIABLE(VARIABLE_CATALOGO) NOMBRE_EXTERNO,\n" +
                                   "TABLA_TRANSACCIONAL,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(TABLA_TRANSACCIONAL) NOMBRE_TRANSACCIONAL,EXPRESION_TRANSACCIONAL,\n" +
                                   "TABLA_CONSOLIDACION,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(TABLA_CONSOLIDACION) NOMBRE_CONSOLIDACION,EXPRESION_CONSOLIDACION, QUERY_AUTOGENERADO\n" +
                                   "FROM GRTA_VARIABLES\n" +
                                   "WHERE\n" +
                                   "CODIGO_VARIABLE=TO_NUMBER(?)";
        // Recupera las restricciones de las Variables
        /*Usado WMarcia 31-01-2018*/
        public const String RestriccionTrasaccional = "SELECT ROWNUM orden, FILTRO.CAMPO_FILTRO cod_campo,CASE WHEN FILTRO.CAMPO_FILTRO IS NULL THEN 'Indefinido' ELSE com_sub.descripcion END campo, "
                   + " FILTRO.OPERADOR_MATEMATICO cod_operador,CASE  WHEN FILTRO.OPERADOR_MATEMATICO IS NULL THEN 'Indefinido' ELSE op.simbolo_usuario END operador, "
                   + " FILTRO.CLASE_PARAMETRO cod_clase_parametro,CASE  WHEN FILTRO.CLASE_PARAMETRO IS NULL THEN 'Indefinido' ELSE com_det.nombre END  clase_parametro, "
                   + " FILTRO.ID_PARAMETRO cod_valor_parametro, CASE  WHEN FILTRO.ID_PARAMETRO IS NULL THEN 'Indefinido' ELSE UPPER(par.DESCRIPCION_BREVE) END valor_parametro "
                   + " FROM GRTA_VARIABLES VARIABLES INNER JOIN GRTA_FILTRO_VARIABLES FILTRO on FILTRO.CODIGO_VARIABLE=VARIABLES.CODIGO_VARIABLE "
                   + " LEFT JOIN  GRTA_COMPENDIO_SUBDETALLE  com_sub   ON  com_sub.ID_SUBDETALLE=FILTRO.CAMPO_FILTRO "
                   + " LEFT JOIN GRTA_OPERADOR_MATEMATICO op ON op.OPERADOR_MATEMATICO=FILTRO.OPERADOR_MATEMATICO "
                   + " LEFT JOIN GRTA_COMPENDIO_DETALLE com_det ON com_det.ID_DETALLE=FILTRO.CLASE_PARAMETRO "
                   + " LEFT JOIN GRTA_PARAMETROS par ON par.ID_PARAMETRO=TO_NUMBER(FILTRO.ID_PARAMETRO)"
                   + " WHERE"
                   + " VARIABLES.CODIGO_VARIABLE=TO_NUMBER(?) AND"
                   + " FILTRO.CODIGO_VARIABLE=VARIABLES.CODIGO_VARIABLE AND"
                   + " SYSDATE BETWEEN FILTRO.FECHA_INICIO_VIGENCIA AND DECODE(FILTRO.FECHA_FIN_VIGENCIA,'',SYSDATE,FILTRO.FECHA_FIN_VIGENCIA) AND"
                   + " FILTRO.ORIGEN_FUENTE=504"
                   + " ORDER BY SECUENCIA_FILTRO";

        //Funciones reemplazadas
        //GRFN_NOMBRE_SUBELEMENTO, Función Reemplazada dentro de la consulta
        //GRFN_OPERADOR_USUARIO, Función Reemplazada dentro de la consulta
        //GRFN_NOMBRE_ELEMENTO, Función Reemplazada dentro de la consulta
        //GRFN_NOMBRE_PARAMETRO, Función Reemplazada dentro de la consulta
        /*Usado WMarcia 31-01-2018*/
        public const String RestriccionConsolidacion = "SELECT ROWNUM orden, CAMPO_FILTRO cod_campo,CASE WHEN FILTRO.CAMPO_FILTRO IS NULL THEN 'Indefinido' ELSE com_sub.descripcion END campo,\n" +
                   "OPERADOR_MATEMATICO cod_operador, CASE  WHEN FILTRO.OPERADOR_MATEMATICO IS NULL THEN 'Indefinido' ELSE op.simbolo_usuario END operador,\n" +
                   "CLASE_PARAMETRO cod_clase_parametro, CASE  WHEN FILTRO.CLASE_PARAMETRO IS NULL THEN 'Indefinido' ELSE com_det.nombre END  clase_parametro, \n" +
                   "ID_PARAMETRO cod_valor_parametro, CASE  WHEN FILTRO.ID_PARAMETRO IS NULL THEN 'Indefinido' ELSE UPPER(par.DESCRIPCION_BREVE) END valor_parametro \n" +
                   "FROM GRTA_VARIABLES VARIABLES INNER JOIN GRTA_FILTRO_VARIABLES FILTRO on FILTRO.CODIGO_VARIABLE=VARIABLES.CODIGO_VARIABLE \n" +
                   "LEFT JOIN  GRTA_COMPENDIO_SUBDETALLE  com_sub   ON  com_sub.ID_SUBDETALLE=FILTRO.CAMPO_FILTRO \n" +
                   "LEFT JOIN GRTA_OPERADOR_MATEMATICO op ON op.OPERADOR_MATEMATICO=FILTRO.OPERADOR_MATEMATICO \n" +
                   "LEFT JOIN GRTA_COMPENDIO_DETALLE com_det ON com_det.ID_DETALLE=FILTRO.CLASE_PARAMETRO \n" +
                   "LEFT JOIN GRTA_PARAMETROS par ON par.ID_PARAMETRO=TO_NUMBER(FILTRO.ID_PARAMETRO) \n" +
                   "WHERE \n" +
                   "VARIABLES.CODIGO_VARIABLE=TO_NUMBER(?) AND\n" +
                   "SYSDATE BETWEEN FILTRO.FECHA_INICIO_VIGENCIA AND DECODE(FILTRO.FECHA_FIN_VIGENCIA,'',SYSDATE,FILTRO.FECHA_FIN_VIGENCIA) AND\n" +
                   "FILTRO.ORIGEN_FUENTE=505\n" +
                   "ORDER BY FILTRO.SECUENCIA_FILTRO";
        //Funciones reemplazadas
        //GRFN_NOMBRE_SUBELEMENTO, Función Reemplazada dentro de la consulta
        //GRFN_OPERADOR_USUARIO, Función Reemplazada dentro de la consulta
        //GRFN_NOMBRE_ELEMENTO, Función Reemplazada dentro de la consulta
        //GRFN_NOMBRE_PARAMETRO, Función Reemplazada dentro de la consulta
        // Recupera las variables calificadoras
        //*Usado WMarcia 31-01-2018*/	 	
        public const String VarCalTabla = "SELECT ROWNUM orden, VARIABLES.CODIGO_VARIABLE codigo_variable,\n" +
                   "(CASE WHEN LENGTH(VARIABLES.DESCRIPCION_BREVE||VARIABLES.EXPRESION_CONSOLIDACION)>90 THEN \n" +
                   "SUBSTR(VARIABLES.DESCRIPCION_BREVE||': '||VARIABLES.EXPRESION_CONSOLIDACION,1,90)||'...' ELSE  \n" +
                   "VARIABLES.DESCRIPCION_BREVE||': '||VARIABLES.EXPRESION_CONSOLIDACION END) variable_expresion \n" +
                  "FROM GRTA_VARIABLES VARIABLES, GRTA_CALIFICADORAS_VARIABLES CALIFICADORAS\n" +
                  "WHERE\n" +
                  "CALIFICADORAS.VARIABLE_PREDICTORA=TO_NUMBER(?) AND\n" +
                  "VARIABLES.CODIGO_VARIABLE=CALIFICADORAS.VARIABLE_CALIFICADORA\n" +
                  "ORDER BY CALIFICADORAS.VARIABLE_CALIFICADORA";

        // Recupera las politicas registradas SQL
        public const String PoliticaTabla = "SELECT (SELECT COUNT(0) FROM GRTA_POLITICA_ARCHIVOS WHERE ID_POLITICA=POLITICA.id_politica AND FECHA_FIN_VIGENCIA IS NULL) \"Adj\",\n" +
                               "         POLITICA.id_politica \"Politica\", POLITICA.descripcion_breve \"Descripcion\",\n" +
                               "         (SELECT COUNT(0) FROM GRTA_MEDIDAS WHERE ID_POLITICA=POLITICA.id_politica) \"#Med.\",\n" +
                               "         (SELECT COUNT(0) FROM GRTA_MEDIDAS WHERE ID_POLITICA=POLITICA.id_politica AND ESTADO_MEDIDA=42 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) ) \"#Med.Vig.\",\n" +
                               " TO_CHAR(POLITICA.FECHA_INICIO_VIGENCIA,'DD/MM/YYYY HH24:MI:SS')||' - '||DECODE(POLITICA.FECHA_FIN_VIGENCIA,'','Indefinido',TO_CHAR(POLITICA.FECHA_FIN_VIGENCIA,'DD/MM/YYYY HH24:MI:SS')) \"F. Vigencia\", ' ' \"Operacion\" \n" +
                               " FROM \n" +
                               " GRTA_POLITICA_INSTITUCIONAL POLITICA,GRTA_OPERACIONES_POLITICA OPERACIONES\n" +
                               " WHERE \n" +
                               " OPERACIONES.ID_POLITICA= POLITICA.id_politica AND OPERACIONES.TIPO_OPERACION=75 AND \n" +
                               " TO_DATE(TO_CHAR(OPERACIONES.fecha_operacion,'DD/MM/YYYY'),'DD/MM/YYYY') \n" +
                               " BETWEEN TO_DATE(NVL(:pFechaInicio,TO_CHAR(OPERACIONES.fecha_operacion,'DD/MM/YYYY')),'DD/MM/YYYY') AND \n" +
                               "        TO_DATE(NVL(:pFechaFin,TO_CHAR(OPERACIONES.fecha_operacion,'DD/MM/YYYY')),'DD/MM/YYYY') AND\n" +
                               " POLITICA.id_politica=NVL(:pPolitica,POLITICA.id_politica) AND\n" +
                               " (POLITICA.descripcion_completa LIKE '%' ||NVL(UPPER(:pDescripcion),POLITICA.descripcion_completa)||'%' OR \n" +
                               " POLITICA.descripcion_breve LIKE '%' ||NVL(UPPER(:pDescripcion),POLITICA.descripcion_breve)||'%')\n" +
                               " ORDER BY POLITICA.id_politica DESC";

        // Recupera la tabla de la tabla del estado de medida
        public const String MedidaEstadoTabla = "SELECT to_char(MEDIDA.id_medida)||'-'||to_char(MEDIDA.version_medida) \"Medida Control\",TIPOMEDIDA.descripcion \"Tipo Medida\",NVL(MEDIDA.nombre_medida,'') \"Nombre Medida\",NVL(TO_CHAR(MEDIDA.fecha_inicio_vigencia,'DD/MM/YYYY'),'') \"F. Inicio\"," +
                                "NVL(TO_CHAR(MEDIDA.fecha_fin_vigencia,'DD/MM/YYYY'),'') \"F. Fin\",NVL(SUJETO.descripcion,'') \"Suj. Riesgo\",ESTADOMEDIDA.descripcion \"Estado\",'' \"Detalle\"" +
                                 " FROM GRTA_MEDIDAS MEDIDA LEFT JOIN GRTA_SUJETO_RIESGO SUJETO ON MEDIDA.sujeto_riesgo = SUJETO.sujeto_riesgo" +
                                 " LEFT JOIN (SELECT id_detalle id_tipo,descripcion FROM GRTA_COMPENDIO_DETALLE WHERE id_compendio = 7 AND SYSDATE BETWEEN fecha_inicio_vigencia AND NVL(fecha_fin_vigencia,SYSDATE)) TIPOMEDIDA" +
                                " ON MEDIDA.tipo_medida = TIPOMEDIDA.id_tipo LEFT JOIN" +
                                 " (SELECT id_detalle id_estado,referencia1 descripcion FROM GRTA_COMPENDIO_DETALLE WHERE id_compendio = 12 AND SYSDATE BETWEEN fecha_inicio_vigencia AND NVL(fecha_fin_vigencia,SYSDATE)) ESTADOMEDIDA" +
                                " ON MEDIDA.estado_medida = ESTADOMEDIDA.id_estado" +
                                " WHERE MEDIDA.id_medida = NVL(:pIdMedida,MEDIDA.id_medida) AND" +
                                " MEDIDA.version_medida = NVL(:pVersion,MEDIDA.version_medida) AND" +
                                " NVL(MEDIDA.nombre_medida,'') LIKE '%'||NVL(UPPER(:pDescripcion),NVL(MEDIDA.nombre_medida,''))||'%' AND" +
                                " TIPOMEDIDA.id_tipo = NVL(:pTipoMedida,TIPOMEDIDA.id_tipo) AND" +
                                " ESTADOMEDIDA.id_estado IN(:pEC,:pERC,:pEA,:pER) " +
                                "ORDER BY MEDIDA.id_medida ";
        // Recupera la consulta de variable: Cambiado por un stored:
        /*
public const String VariableTabla="SELECT VARIABLE.codigo_variable \"Nro\",SUJETO.descripcion \"Sujeto\"," +
                 "(SELECT descripcion FROM GRTA_COMPENDIO_DETALLE WHERE id_compendio = 18 AND id_detalle = VARIABLE.clase_variable) \"Clase Variable\"," +
                 "NVL((SELECT descripcion FROM GRTA_COMPENDIO_DETALLE WHERE id_compendio = 19 AND id_detalle = VARIABLE.tipo_variable),'') \"Tipo Variable\"," +
                 "VARIABLE.descripcion \"Descripción\"," +
                 "TO_CHAR(VARIABLE.fecha_inicio_vigencia,'DD/MM/YYYY') \"Inicio Vigencia\"," +
                 "NVL(TO_CHAR(VARIABLE.fecha_fin_vigencia,'DD/MM/YYYY'),' ') \"Fin Vigencia\",'' \"Operación\" " +
                 "FROM GRTA_VARIABLES VARIABLE INNER JOIN GRTA_SUJETO_RIESGO SUJETO ON " +
                 "VARIABLE.sujeto_riesgo = SUJETO.sujeto_riesgo " +
                 "WHERE VARIABLE.clase_variable = NVL(:pClaseVar,VARIABLE.clase_variable) AND VARIABLE.codigo_variable = NVL(TO_NUMBER(:pIdVariable),VARIABLE.codigo_variable) AND UPPER(VARIABLE.descripcion) LIKE '%' ||NVL(:pDescripcion,UPPER(VARIABLE.descripcion))||'%' " +
                 "ORDER BY VARIABLE.codigo_variable"),
        */
        // Recupera la consulta de la tabla de analisis: 28 JUNIO Ocamacho
        /*
public const String TblAnalisisTabla="SELECT TABLA.tabla_pivot \"Nro\", SUJETO.descripcion \"Suj. Riesgo\",TABLA.nombre_tabla \"Nombre Tabla\",TO_CHAR(TABLA.fecha_inicio_vigencia,'DD/MM/YYYY') \"Inicio Vigencia\"," +
                    "NVL(TO_CHAR(TABLA.fecha_fin_vigencia,'DD/MM/YYYY'),' ') \"Fin Vigencia\",'' \"Operación\" " +
                    "FROM GRTA_TABLA_PIVOT TABLA INNER JOIN GRTA_SUJETO_RIESGO SUJETO " +
                    "ON TABLA.sujeto_riesgo = sujeto.sujeto_riesgo " +
                    "WHERE TABLA.tabla_pivot = NVL(TO_NUMBER(:pIdTablaAna),TABLA.tabla_pivot) AND TABLA.sujeto_riesgo = NVL(TO_NUMBER(:pSujRiesgo),TABLA.sujeto_riesgo) AND UPPER(TABLA.nombre_tabla) LIKE '%'||NVL(:pNombreTabla,UPPER(TABLA.nombre_tabla))||'%' " +
                    "ORDER BY TABLA.tabla_pivot"),
        */
        // Recupera el codigo y la descripcion del tipo de ambito
        public const String TipoAmbitoCombo = "SELECT id_detalle,referencia1 desc_ambito" +
                              " FROM GRTA_COMPENDIO_DETALLE" +
                              " WHERE id_compendio = 4 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                              " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el detalle de tabla de analisis seleccionada
        public const String TblAnalisisDetalle = "SELECT SUJETO.TABLAS_SUJETO \"tablasSujeto\",SUJETO.SUJETO_RIESGO \"codigoSujeto\", GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_SUJETO_RIESGO(SUJETO.SUJETO_RIESGO) \"nombreSujeto\",\n" +
                                       "SUJETO.ORIGEN_FUENTE \"codigoOrigen\", GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(SUJETO.ORIGEN_FUENTE) \"nombreOrigen\",\n" +
                                       "SUJETO.TIPO_TABLA \"codigoTipoTabla\", GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(SUJETO.TIPO_TABLA) \"nombreTipoTabla\",\n" +
                                       "SUJETO.TABLA_DATOS \"codigoTabla\",NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE=SUJETO.TABLA_DATOS),'Indefinido') \"nombreTabla\",\n" +
                                       "SUJETO.CAMPO_CANAL \"codigoCanal\", NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=SUJETO.CAMPO_CANAL),'Indefinido') \"nombreCanal\",\n" +
                                       "SUJETO.CAMPO_FECHA \"codigoFecha\", NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=SUJETO.CAMPO_FECHA),'Indefinido') \"nombreFecha\",\n" +
                                       "SUJETO.CAMPO_FECHA_ANIO_MES \"codigoFechaAnioMes\", NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=SUJETO.CAMPO_FECHA_ANIO_MES),'Indefinido') \"nombreFechaAnioMes\",\n" +
                                       "SUJETO.CAMPO_FECHA_ANIO_SEMANA \"codigoFechaAnioSemana\", NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=SUJETO.CAMPO_FECHA_ANIO_SEMANA),'Indefinido') \"nombreFechaAnioSemana\",\n" +
                                       "(SELECT TABLA_DATOS FROM GRTA_TABLAS_SUJETO WHERE TABLAS_SUJETO=SUJETO.TABLAS_SUJETO_PADRE) \"codigoTablaPadre\", NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE IN \n" +
                                       "(SELECT TABLA_DATOS FROM GRTA_TABLAS_SUJETO WHERE TABLAS_SUJETO=SUJETO.TABLAS_SUJETO_PADRE)),'Indefinido') \"nombreTablaPadre\",\n" +
                                       "TO_CHAR(SUJETO.FECHA_INICIO_VIGENCIA,'DD/MM/YYYY') \"fechaInicio\",TO_CHAR(SUJETO.FECHA_FIN_VIGENCIA,'DD/MM/YYYY') \"fechaFin\"\n" +
                                       "FROM GRTA_TABLAS_SUJETO SUJETO\n" +
                                       "WHERE\n" +
                                       "SUJETO.TABLAS_SUJETO=TO_NUMBER(?)";

        // Recupera la consulta de los ambito de la tabla de analisis
        public const String AmbitoPivotTabla = "SELECT ROWNUM \"#\",NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=SUJETO.CAMPO_TABLA_HIJA),'Indefinido') \"Nombre Campo\", \n" +
                               "(SELECT SIMBOLO_MATEMATICO FROM GRTA_OPERADOR_MATEMATICO WHERE  OPERADOR_MATEMATICO=SUJETO.OPERADOR_MATEMATICO) \"Simb.\",\n" +
                               "DECODE(SUJETO.CAMPO_TABLA_PADRE,'',\n" +
                               "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_PARAMETRO(SUJETO.ID_PARAMETRO),\n" +
                               "NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=SUJETO.CAMPO_TABLA_PADRE),'Indefinido')) \"Campo / Parámetro\"   \n" +
                               "FROM GRTA_JOIN_SUJETO SUJETO\n" +
                               "WHERE\n" +
                               "SUJETO.TABLAS_SUJETO_HIJA=TO_NUMBER(:pIdTablaAna) AND \n" +
                               "SYSDATE BETWEEN SUJETO.FECHA_INICIO_VIGENCIA AND DECODE(SUJETO.FECHA_FIN_VIGENCIA,'',SYSDATE,SUJETO.FECHA_FIN_VIGENCIA)\n" +
                               "ORDER BY ROWNUM";
        // Recupera la consulta de los ambito de la tabla de analisis
        //Ocamacho 13AGO2012: Se coloca el identificador de la tabla:
        public const String AmbitoPivot = "SELECT ROWNUM,SUJETO.CAMPO_TABLA_HIJA \"codigoCampo\",NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=SUJETO.CAMPO_TABLA_HIJA),'Indefinido') \"nombreCampo\", \n" +
                                   "SUJETO.OPERADOR_MATEMATICO \"codigoOperador\", (SELECT SIMBOLO_MATEMATICO FROM GRTA_OPERADOR_MATEMATICO WHERE  OPERADOR_MATEMATICO=SUJETO.OPERADOR_MATEMATICO) \"simboloOperador\",\n" +
                                   "SUJETO.TIPO_JOIN,\n" +
                                   "DECODE(SUJETO.CAMPO_TABLA_PADRE,'',SUJETO.ID_PARAMETRO,SUJETO.CAMPO_TABLA_PADRE) \"codigoCampoParametro\",\n" +
                                   "DECODE(SUJETO.CAMPO_TABLA_PADRE,'',\n" +
                                   "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_PARAMETRO(SUJETO.ID_PARAMETRO),\n" +
                                   "NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=SUJETO.CAMPO_TABLA_PADRE),'Indefinido')) \"nombreCampoParametro\", SUJETO.TABLAS_SUJETO_HIJA \n" +
                                   "FROM GRTA_JOIN_SUJETO SUJETO\n" +
                                   "WHERE\n" +
                                   "SUJETO.TABLAS_SUJETO_HIJA=TO_NUMBER(:pIdTablaAna) AND \n" +
                                   "SYSDATE BETWEEN SUJETO.FECHA_INICIO_VIGENCIA AND DECODE(SUJETO.FECHA_FIN_VIGENCIA,'',SYSDATE,SUJETO.FECHA_FIN_VIGENCIA)\n" +
                                   "ORDER BY ROWNUM";
        // Recupera la consulta de la declaracion de la tabla pivot
        public const String PivotDeclaracionTabla = "SELECT SECUENCIA_SUJETO \"#\", \n" +
                                           "NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=CAMPO_SUJETO),'Indefinido') \"nombreCampo\"\n" +
                                           "FROM GRTA_IDENTIFICADOR_SUJETO\n" +
                                           "WHERE\n" +
                                           "TABLAS_SUJETO=TO_NUMBER(:pIdTablaAna) AND \n " +
                                           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)\n" +
                               "ORDER BY SECUENCIA_SUJETO";

        //Ocamacho 14AGO2012: Se modifica para que coloque el identificador de la tabla:                            
        public const String MgrPivotDeclaracionTablaEdit = "SELECT ROWNUM \"orden\", CAMPO_SUJETO \"codigoCampo\", \n" +
                             "NVL((SELECT CODIGO_ALTERNO||'- ('||DESCRIPCION||')' FROM GRTA_COMPENDIO_SUBDETALLE WHERE ID_SUBDETALLE=CAMPO_SUJETO),'Indefinido') \"nombreCampo\", TABLAS_SUJETO\n" +
                             "FROM GRTA_IDENTIFICADOR_SUJETO\n" +
                             "WHERE\n" +
                             "TABLAS_SUJETO=TO_NUMBER(:pIdTablaAna) \n " +
                             "AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)\n" +
                             "ORDER BY IDENTIFICACION_SUJETO";

        //@Version: 1.0, 26/08/2012; @Descripcion: Ocupada para aquellos formularios que tiene la funcionalidad de diferentes Bean
        public const String MgrCompendioModificacionBean = "SELECT TO_CHAR(VARIACIONES.FECHA_INGRESO,'DD/MM/YYYY HH24:MI') \"Fecha Modificación\", \n" +
                             "SESSIONES.CODIGO_USUARIO||'-'||(SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=63 AND CODIGO_ALTERNO=SESSIONES.CODIGO_USUARIO) \"Nombre Usuario\", \n" +
                             "VARIACIONES.NOMBRE_CAMPO \"Casilla Modificada\", DECODE(VARIACIONES.VALOR_ANTIGUO,' ','EN BLANCO',NULL,'EN BLANCO',SUBSTR(VARIACIONES.VALOR_ANTIGUO,1,15)) \"Valor Antiguo\", SUBSTR(VARIACIONES.VALOR_NUEVO,1,15) \"Valor Nuevo\" \n" +
                             "FROM GRTA_VARIACIONES VARIACIONES, GRTA_SESSION SESSIONES\n" +
                             "WHERE\n" +
                             "VARIACIONES.NOMBRE_TABLA=:pNombreTabla AND\n" +
                             "VARIACIONES.CLAVE_REGISTRO=:pRegistro AND\n" +
                             "SESSIONES.ID_SESSION=VARIACIONES.ID_SESSION\n" +
                             "ORDER BY VARIACIONES.FECHA_INGRESO DESC";

        // Recupera la consulta del Sujeto de Riesgo
        //TODO MODIFICADO : PESCARCENA
        //Funcion convertida:GRFN_NOMBRE_ELEMENTO
        public const String SujRiesgoTabla = "SELECT SUJETO.SUJETO_RIESGO sujeto_riesgo, SUJETO.DESCRIPCION_BREVE descripcion_breve, " +
                                   "CASE WHEN SUJETO.FASE_CONTROL IS NULL THEN 'Indefinido' ELSE com_det.nombre END  fase_control_text, " +
                                   "CASE WHEN SUJETO.TIPO_SELECCION IS NULL THEN 'Indefinido' ELSE com_det.nombre END tipo_seleccion_text, " +
                                   "SUJETO.FECHA_INICIO_VIGENCIA fecha_inicio_vigencia, CASE WHEN SUJETO.FECHA_FIN_VIGENCIA IS NULL THEN NULL ELSE SUJETO.FECHA_FIN_VIGENCIA END fecha_fin_vigencia, '' periodo_vigencia, ''  operaciones " +
                                   "FROM GRTA_SUJETO_RIESGO SUJETO " +
                                   "LEFT JOIN GRTA_COMPENDIO_DETALLE com_det ON com_det.ID_DETALLE=SUJETO.TIPO_SELECCION " +
                                   "WHERE " +
                                   "(SUJETO.SUJETO_RIESGO=:pSujetoRiesgo OR :pSujetoRiesgo IS NULL) AND " +
                                   "(SUJETO.FASE_CONTROL=:pFaseControl OR :pFaseControl IS NULL) AND " +
                                   "(SUJETO.DESCRIPCION_BREVE LIKE '%'+UPPER(:pDescripcion)+'%' OR SUJETO.DESCRIPCION_COMPLETA LIKE '%'+UPPER(:pDescripcion)+'%' OR :pDescripcion IS NULL) " +
                           "ORDER BY SUJETO_RIESGO DESC";
        // Recupera el codigo y la descricion de la fase control
        public const String FaseControlCombo = "SELECT id_detalle,nombre descripcion" +
                               " FROM GRTA_COMPENDIO_DETALLE" +
                               " WHERE id_compendio = 9 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                               " AND NVL(fecha_fin_vigencia,SYSDATE)";


        public const String TipoSeleccion = "SELECT id_detalle,nombre descripcion" +
                       " FROM GRTA_COMPENDIO_DETALLE" +
                       " WHERE id_compendio = 24 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                       " AND NVL(fecha_fin_vigencia,SYSDATE)";

        // Recupera el codigo y la descripcion de la funcion de regresion        
        public const String FuncRegresionCombo = "SELECT id_detalle,referencia1 desc_funcion" +
                                 " FROM GRTA_COMPENDIO_DETALLE" +
                                 " WHERE id_compendio = 20 AND SYSDATE BETWEEN fecha_inicio_vigencia" +
                                 " AND NVL(fecha_fin_vigencia,SYSDATE)";
        // Recupera el detalle del sujeto riesgo seleccionado
        //Función Convertida: GRFN_NOMBRE_ELEMENTO
        //TODO, prueba no completada por falta de datos
        public const String SujRiesgoDetalle = "SELECT SUJETO.SUJETO_RIESGO sujeto_riesgo, SUJETO.ORGANO_INSTITUCIONAL organo_institucional, " +
                                       "(SELECT CODIGO_ALTERNO FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE=SUJETO.ORGANO_INSTITUCIONAL) organo_institucional_text, " +
                                       "SUJETO.FASE_CONTROL fase_control,CASE  WHEN SUJETO.FASE_CONTROL IS NULL THEN 'Indefinido' ELSE com_det.nombre END fase_control_text, " +
                                       "SUJETO.DESCRIPCION_BREVE descripcion_breve, SUJETO.TIPO_SELECCION tipo_seleccion,CASE  WHEN SUJETO.TIPO_SELECCION IS NULL THEN 'Indefinido' ELSE com_det.nombre END tipo_seleccion_text, " +
                                       "SUJETO.DESCRIPCION_COMPLETA descripcion_completa, SUJETO.FECHA_INICIO_VIGENCIA fecha_inicio_vigencia, SUJETO.FECHA_FIN_VIGENCIA fecha_fin_vigencia " +
                                       "FROM GRTA_SUJETO_RIESGO SUJETO " +
                                       "LEFT JOIN GRTA_COMPENDIO_DETALLE com_det ON com_det.ID_DETALLE=SUJETO.TIPO_SELECCION " +
                                       "WHERE " +
                                       "SUJETO.SUJETO_RIESGO = ?";
        // Recupera la consulta de los identificadores del sujeto de riesgo
        public const String IdentSujRiesgoTabla = "SELECT secuencia_identificacion \"Orden\",nombre_campo \"Nombre Campo\", descripcion \"Descripción Campo\" " +
                                  "FROM GRTA_IDENTIFICADOR_SUJETO " +
                                     "WHERE sujeto_riesgo = TO_NUMBER(:pSujetoRiesgo) " +
                                     "AND SYSDATE BETWEEN  fecha_inicio_vigencia AND NVL(fecha_fin_vigencia,SYSDATE) " +
                                     "ORDER BY secuencia_identificacion";
        // Recupera los ambitos del sujeto de riesgo seleccionado
        public const String AmbitoSujRiesgo = "SELECT sujeto_riesgo \"sujeto_riesgo\",tipo_ambito \"tipo_ambito\",(SELECT referencia1 FROM GRTA_COMPENDIO_DETALLE WHERE id_compendio = 4 AND id_detalle = tipo_ambito) \"tipo_ambito_text\"," +
                              "nombre_campo \"nombre_campo\",nombre_tabla \"nombre_tabla\",(CASE condicion_exigencia WHEN 0 THEN 'No Obligatorio' ELSE 'Obligatorio' END) \"condicion_exigencia\" " +
                              "FROM GRTA_AMBITOS_SUJETO " +
                              "WHERE sujeto_riesgo = TO_NUMBER(?) AND SYSDATE BETWEEN fecha_inicio_vigencia AND NVL(fecha_fin_vigencia,SYSDATE) " +
                              "ORDER BY tipo_ambito";
        // Recupera la restriccion del ambito
        public const String RestriccionAmbito = "SELECT RESTRICCION.secuencia_filtro \"secuencia_filtro\", RESTRICCION.nombre_campo \"nombre_campo\",OPERADOR.simbolo_usuario \"simbolo_usuario\", PARAMETRO.descripcion_breve \"descripcion_breve\"" +
                                "FROM GRTA_RESTRICCIONES_AMBITO RESTRICCION,griesgo.GRTA_OPERADOR_MATEMATICO OPERADOR,griesgo.GRTA_PARAMETRO PARAMETRO " +
                                "WHERE RESTRICCION.sujeto_riesgo = TO_NUMBER(?) AND RESTRICCION.tipo_ambito = TO_NUMBER(?) " +
                                "AND RESTRICCION.codigo_operador = OPERADOR.codigo_operador AND RESTRICCION.id_parametro = PARAMETRO.id_parametro " +
                                "ORDER BY RESTRICCION.secuencia_filtro";
        /*Usado WMarcia 10-04-2018*/
        // Recupera los identificadores de sujeto
        public const String IdentificadorSujetoCombo = "SELECT TO_CHAR(IDENTIFICADOR.identificacion_sujeto) codigo, GRPK_OPERACIONES_COMUNES.GRFN_DESCRIP_IDENTIFICACION(IDENTIFICADOR.identificacion_sujeto) descripcion\n" +
                                       "FROM GRTA_TABLAS_SUJETO SUJETO,GRTA_IDENTIFICADOR_SUJETO IDENTIFICADOR \n" +
                                       "WHERE (SUJETO.SUJETO_RIESGO=? OR ? IS NULL) AND\n" +
                                       "SUJETO.ORIGEN_FUENTE=504 AND SUJETO.TIPO_TABLA IN (506,507) AND\n" +
                                       "IDENTIFICADOR.TABLAS_SUJETO=SUJETO.TABLAS_SUJETO AND\n" +
                                       "SYSDATE BETWEEN IDENTIFICADOR.fecha_inicio_vigencia AND COALESCE(IDENTIFICADOR.fecha_fin_vigencia,SYSDATE)\n" +
                                       "ORDER BY IDENTIFICADOR.SECUENCIA_SUJETO";
        // Recupera las restricciones de las Variables
        public const String RestriccionVar = "SELECT * FROM (SELECT X.SECUENCIA_FILTRO,X.CAMPO_FILTRO,X.OPERADOR_MATEMATICO,X.CLASE_PARAMETRO,X.ID_PARAMETRO,X.TIPO_TABLA\n" +
                                   "FROM \n" +
                                   "(\n" +
                                   "SELECT FILTRO.SECUENCIA_FILTRO,CAMPO_FILTRO,OPERADOR_MATEMATICO,CLASE_PARAMETRO,ID_PARAMETRO,'T' TIPO_TABLA\n" +
                                   "FROM GRTA_VARIABLES VARIABLES, GRTA_FILTRO_VARIABLES FILTRO\n" +
                                   "WHERE\n" +
                                   "VARIABLES.CODIGO_VARIABLE=TO_NUMBER(:pVariable) AND\n" +
                                   "FILTRO.CODIGO_VARIABLE=VARIABLES.CODIGO_VARIABLE AND\n" +
                                   "SYSDATE BETWEEN FILTRO.FECHA_INICIO_VIGENCIA AND DECODE(FILTRO.FECHA_FIN_VIGENCIA,'',SYSDATE,FILTRO.FECHA_FIN_VIGENCIA) AND\n" +
                                   "FILTRO.ORIGEN_FUENTE=504\n" +
                                   "UNION\n" +
                                   "SELECT FILTRO.SECUENCIA_FILTRO,CAMPO_FILTRO,OPERADOR_MATEMATICO,CLASE_PARAMETRO,ID_PARAMETRO,'C' TIPO_TABLA\n" +
                                   "FROM GRTA_VARIABLES VARIABLES, GRTA_FILTRO_VARIABLES FILTRO\n" +
                                   "WHERE\n" +
                                   "VARIABLES.CODIGO_VARIABLE=TO_NUMBER(:pVariable) AND\n" +
                                   "FILTRO.CODIGO_VARIABLE=VARIABLES.CODIGO_VARIABLE AND\n" +
                                   "SYSDATE BETWEEN FILTRO.FECHA_INICIO_VIGENCIA AND DECODE(FILTRO.FECHA_FIN_VIGENCIA,'',SYSDATE,FILTRO.FECHA_FIN_VIGENCIA) AND\n" +
                                   "FILTRO.ORIGEN_FUENTE=505\n" +
                                   ") X\n" +
                                   "ORDER BY X.SECUENCIA_FILTRO)";

        // Recupera las variables calificadoras
        public const String VarCal = "SELECT PREDICCAL.variable_calificadora,VARCALIFICADORA.descripcion \"Descripción\"" +
                     "FROM GRTA_PREDICTORAS_CALIFICADORAS PREDICCAL INNER JOIN GRTA_VARIABLES VARIABLE " +
                     "ON PREDICCAL.variable_predictora = VARIABLE.codigo_variable INNER JOIN GRTA_VARIABLES VARCALIFICADORA " +
                     "ON PREDICCAL.variable_calificadora = VARCALIFICADORA.codigo_variable " +
                     "WHERE PREDICCAL.variable_predictora = TO_NUMBER(:pVariable)  AND SYSDATE BETWEEN PREDICCAL.FECHA_INICIO_VIGENCIA AND NVL(PREDICCAL.FECHA_FIN_VIGENCIA,SYSDATE)  ";
        //@Autor: WMarcia; @Version: 1.0, 27/11/2009; @Descripcion: Recupera los Ambitos del Sujeto de Riesgo
        public const String AmbitoSujRiesgoTabla = "SELECT TO_CHAR(AMB.codsujriesgo)||'-'||TO_CHAR(AMB.codtipoambito) \"Ident\",AMB.sujriesgo \"Suj. Riesgo\",AMB.tipoambito \"Tipo Ambito\",AMB.nombre_campo \"Campo\",AMB.nombre_tabla \"Tabla\",AMB.desde \"Inicio Vigencia\",AMB.hasta \"Fin Vigencia\",'' \"Operación\"" +
                                   "FROM (SELECT AMBITO.sujeto_riesgo codsujriesgo,SUJETO.descripcion sujriesgo,COMPENDIO.id_detalle codtipoambito,COMPENDIO.referencia1 tipoambito,AMBITO.nombre_campo,AMBITO.nombre_tabla," +
                                   "TO_CHAR(AMBITO.fecha_inicio_vigencia,'DD/MM/YYYY') desde,NVL(TO_CHAR(AMBITO.fecha_fin_vigencia,'DD/MM/YYYY'),' ') hasta " +
                                   "FROM GRTA_AMBITOS_SUJETO AMBITO,GRTA_SUJETO_RIESGO SUJETO,GRTA_COMPENDIO_DETALLE COMPENDIO " +
                                   "WHERE AMBITO.sujeto_riesgo = SUJETO.sujeto_riesgo AND AMBITO.tipo_ambito = COMPENDIO.id_detalle AND AMBITO.sujeto_riesgo = NVL(:pSujRiesgo,AMBITO.sujeto_riesgo) AND AMBITO.tipo_ambito = NVL(:pTipoAmbito,AMBITO.tipo_ambito) AND UPPER(AMBITO.nombre_campo) LIKE '%'||NVL(:pDescripcion,UPPER(AMBITO.nombre_campo))||'%' " +
                                   "ORDER BY AMBITO.sujeto_riesgo,AMBITO.tipo_ambito) AMB";
        //@Autor: WMarcia; @Version: 1.0, 04/12/2009; @Descripcion: Recupera el detalle del Ambito del Sujeto de Riesgo
        public const String AmbitoSujRiesgoDetalle = "SELECT AMBITO.sujeto_riesgo codsujriesgo,SUJETO.descripcion sujriesgo,COMPENDIO.id_detalle codtipoambito,COMPENDIO.referencia1 tipoambito," +
                                     "TO_CHAR(AMBITO.fecha_inicio_vigencia,'DD/MM/YYYY') fechainivig, NVL(TO_CHAR(AMBITO.fecha_fin_vigencia,'DD/MM/YYYY'),' ') fechafinvig, AMBITO.condicion_exigencia codcondexigencia," +
                                     "(CASE AMBITO.condicion_exigencia WHEN 1 THEN 'Obligatorio' ELSE 'No Obligatorio' END) condexigencia,AMBITO.nombre_campo nombrecampo,AMBITO.nombre_tabla nombretabla " +
                                     "FROM GRTA_AMBITOS_SUJETO AMBITO,GRTA_SUJETO_RIESGO SUJETO,GRTA_COMPENDIO_DETALLE COMPENDIO " +
                                     "WHERE AMBITO.sujeto_riesgo = SUJETO.sujeto_riesgo AND AMBITO.tipo_ambito = COMPENDIO.id_detalle " +
                                     "AND AMBITO.sujeto_riesgo = TO_NUMBER(:pSujRiesgo) AND AMBITO.tipo_ambito = TO_NUMBER(:pTipoAmbito)";
        //@Autor: WMarcia; @Version: 1.0, 04/12/2009; @Descripcion: Recupera las restricciones del Ambito del Sujeto de Riesgo
        public const String RestAmbitoSujRiesgoTabla = "SELECT RESTRICCIONES.secuencia_filtro \"Orden\",RESTRICCIONES.nombre_campo \"Campo\",OPERADOR.simbolo_usuario \"Operador\",PAR.descripcion_breve \"Parametro\" " +
                                       "FROM GRTA_RESTRICCIONES_AMBITO RESTRICCIONES INNER JOIN GRTA_PARAMETRO PAR ON RESTRICCIONES.id_parametro = PAR.id_parametro " +
                                       "INNER JOIN GRTA_OPERADOR_MATEMATICO OPERADOR ON RESTRICCIONES.codigo_operador = OPERADOR.codigo_operador " +
                                       "WHERE RESTRICCIONES.sujeto_riesgo = TO_NUMBER(:pSujRiesgo) AND RESTRICCIONES.tipo_ambito = TO_NUMBER(:pTipoAmbito) " +
                                       "ORDER BY RESTRICCIONES.secuencia_filtro";
        //@Autor: WMarcia; @Version: 1.0, 04/12/2009; @Descripcion: Recupera las restricciones del Ambito del Sujeto de Riesgo
        public const String RestAmbitoSujRiesgo = "SELECT RESTRICCIONES.secuencia_filtro,RESTRICCIONES.nombre_campo,RESTRICCIONES.codigo_operador,OPERADOR.simbolo_usuario,RESTRICCIONES.id_parametro,PAR.descripcion_breve " +
                                  "FROM GRTA_RESTRICCIONES_AMBITO RESTRICCIONES INNER JOIN GRTA_PARAMETRO PAR ON RESTRICCIONES.id_parametro = PAR.id_parametro " +
                                  "INNER JOIN GRTA_OPERADOR_MATEMATICO OPERADOR ON RESTRICCIONES.codigo_operador = OPERADOR.codigo_operador " +
                                  "WHERE RESTRICCIONES.sujeto_riesgo = TO_NUMBER(:pSujRiesgo) AND RESTRICCIONES.tipo_ambito = TO_NUMBER(:pTipoAmbito) " +
                                  "ORDER BY RESTRICCIONES.secuencia_filtro";

        //TODO Revisar el id del compendio 	tabla de referencia de variable(componente regla fija)    		   
        //@Autor: WMarcia; @Version: 1.0, 16/12/2009; @Descripcion: Recupera los campos para realizar el script para obtener la descripcion de un determinado catalogo de referencia
        //MgrCatalogosReferencia="SELECT REFERENCIA1 campo, REFERENCIA2 tabla,REFERENCIA3 condicion FROM GRTA_COMPENDIO_DETALLE WHERE id_compendio = 70 AND descripcion=?");
        public const String CatalogosReferencia = "SELECT DETALLE.ID_DETALLE CONCEPTO FROM GRTA_VARIABLES VARIABLES, GRTA_COMPENDIO_DETALLE DETALLE WHERE DETALLE.ID_COMPENDIO=40 AND VARIABLES.TABLA_VALIDACION=TO_NUMBER(DETALLE.REFERENCIA2) AND VARIABLES.CODIGO_VARIABLE=? AND ROWNUM=1";


        public const String SujetoBenfordCombo = "SELECT CODIGO_VARIABLE SUJETO,DESCRIPCION FROM GRTA_VARIABLES" +
                                   " WHERE CODIGO_VARIABLE IN (1,6) ORDER BY 2";


        public const String ProcesoBenfordConsulta = "SELECT  BENFORD.ID_PROCESO, SUJETO.DESCRIPCION SUJETO_DESCRIPCION, VARIABLE.DESCRIPCION VARIABLE_DESCRIPCION, \n" +
           "TO_CHAR(BENFORD.FECHA_INICIO_ANALISIS,'DD/MM/YYYY') FECHA_INICIO_ANALISIS, TO_CHAR(BENFORD.FECHA_FIN_ANALISIS,'DD/MM/YYYY') FECHA_FIN_ANALISIS,\n" +
           "TO_CHAR(BENFORD.FECHA_INICIO_VIGENCIA,'DD/MM/YYYY') FECHA_INICIO_VIGENCIA, TO_CHAR(BENFORD.FECHA_FIN_VIGENCIA,'DD/MM/YYYY') FECHA_FIN_VIGENCIA,\n" +
           "BENFORD.ESTADO_PROCESO,ESTADO.DESCRIPCION ESTADO_DESCRIPCION\n" +
           "FROM GRTA_BENFORD BENFORD, GRTA_VARIABLES SUJETO, GRTA_COMPENDIO_DETALLE VARIABLE, GRTA_COMPENDIO_DETALLE ESTADO\n" +
           "WHERE\n" +
           "BENFORD.ID_PROCESO=TO_NUMBER(?) AND\n" +
           "SUJETO.CODIGO_VARIABLE=BENFORD.CODIGO_VARIABLE AND\n" +
           "VARIABLE.ID_DETALLE=BENFORD.VALOR_ANALISIS AND\n" +
           "ESTADO.ID_DETALLE=BENFORD.ESTADO_PROCESO";


        public const String BenfordGeneralTabla = "SELECT A.ID_PROCESO \"Nro Proceso\", (SELECT DESCRIPCION FROM GRTA_VARIABLES WHERE CODIGO_VARIABLE=A.CODIGO_VARIABLE) \"Sujeto\"," +
           "(SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE=A.VALOR_ANALISIS) \"Variable\"," +
           "TO_CHAR(A.FECHA_INICIO_ANALISIS,'DD/MM/YYYY') \"F.Inicio Análisis\", TO_CHAR(A.FECHA_FIN_ANALISIS,'DD/MM/YYYY') \"F. Fin Análisis\"," +
           "(SELECT REFERENCIA1 FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE=A.ESTADO_PROCESO) \"Estado\"" +
           " FROM GRTA_BENFORD A" +
           " WHERE" +
           " (A.ID_PROCESO=:pProceso OR :pProceso IS NULL) AND" +
           " (A.CODIGO_VARIABLE=:pSujeto OR :pSujeto IS NULL) AND" +
           " (A.VALOR_ANALISIS=:pVariable OR :pVariable IS NULL) AND" +
           " (A.ESTADO_PROCESO=:pEstado OR :pEstado IS NULL)" +
           " ORDER BY A.ID_PROCESO DESC";


        public const String BenfordResultadosTabla = "SELECT ROWNUM \"Nro\",X.VALOR_ELEMENTO \"Código\",X.DESCRIPCION \"Descripcion\", X.TOTAL_REGISTROS \"Nro Registros\", X.BONDAD_INCORRECTA \"Incumplimientos\" FROM  \n" +
           "(\n" +
           "SELECT ELEMENTOS.VALOR_ELEMENTO,NVL(DECODE(BENFORD.CODIGO_VARIABLE,1,CMP_NAM,DEC_NAM),'SIN DESCRIPCION') DESCRIPCION,\n" +
           "ELEMENTOS.TOTAL_REGISTROS, ELEMENTOS.BONDAD_INCORRECTA\n" +
           "FROM GRTA_BENFORD BENFORD, GRTA_ELEMENTOS_BENFORD ELEMENTOS, \n" +
           "UNCMPTAB IMPO,UNDECTAB AGENTE \n" +
           "WHERE\n" +
           "BENFORD.ID_PROCESO=:pProceso AND\n" +
           "ELEMENTOS.ID_PROCESO=BENFORD.ID_PROCESO AND\n" +
           "IMPO.CMP_COD(+)=ELEMENTOS.VALOR_ELEMENTO AND \n" +
           "IMPO.LST_OPE(+)='U' AND\n" +
           "AGENTE.DEC_COD(+)=ELEMENTOS.VALOR_ELEMENTO AND \n" +
           "AGENTE.LST_OPE(+)='U'\n" +
           "ORDER BY ELEMENTOS.BONDAD_INCORRECTA DESC\n" +
           ") X";


        public const String ConsultarCuadroBenford = "SELECT TO_CHAR(digito),DECODE(digito,0,' ',TO_CHAR(valor_toerico1*100,'99.99')||'%') val_teorico1, " +
           "DECODE(digito,0,' ',TO_CHAR(valor_observado1*100,'99.99')||'%') val_observado1, " +
           "TO_CHAR(valor_toerico2*100,'99.99')||'%' val_teorico2,TO_CHAR(valor_observado2*100,'99.99')||'%' val_observado2, " +
           "TO_CHAR(valor_toerico3*100,'99.99')||'%' val_teorico3,TO_CHAR(valor_observado3*100,'99.99')||'%' val_observado3 " +
           "FROM GRTA_BENFORD_TMP " +
           "WHERE IDENTIFICADOR=TO_NUMBER(?)";
        /* 
     public const String MedidaSeleccionadasTabla="SELECT TO_CHAR(X.ID_MEDIDA) ||'-'||TO_CHAR(X.VERSION_MEDIDA) \"No. Medida\", \n" + 
        "(SELECT REFERENCIA2 FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=7 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE) AND ID_DETALLE=X.TIPO_MEDIDA) \"Tipo Medida\", X.NOMBRE_MEDIDA  \"Nombre Medida\", \n" + 
        "(SELECT REFERENCIA1 FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=12 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE) AND ID_DETALLE=X.ESTADO_MEDIDA) \"Estado\" \n" + 
        "FROM GRTA_MEDIDAS X, GRTA_PROCESO_MEDIDA Y \n" + 
        "WHERE \n" + 
        "Y.ID_PROCESO=:pProceso AND\n" + 
        "X.ID_MEDIDA=Y.ID_MEDIDA AND\n" + 
        "X.VERSION_MEDIDA=Y.VERSION_MEDIDA \n" + 
        "ORDER BY X.ID_MEDIDA DESC, X.VERSION_MEDIDA"),
        */
        public const String ResultadoPorAduanaTabla = "SELECT \n" +
           "substr(perfil.id_declaracion,instr(perfil.id_declaracion,'@',1,1)+1,instr(perfil.id_declaracion,'@',1,2)-instr(perfil.id_declaracion,'@',1,1)-1) \"Código\",\n" +
           "aduana.descripcion \"Descripción\", count(0) \"Total DMs\", \n" +
           "to_char(trunc(sum(decode(perfil.nivel_referencia,6,1,0))*100/count(0),1))||'%' \"Porc.Rojo\",\n" +
           "to_char(trunc(sum(decode(perfil.nivel_referencia,7,1,0))*100/count(0),1))||'%' \"Porc.Amarillo\",\n" +
           "to_char(trunc(sum(decode(perfil.nivel_referencia,8,1,0))*100/count(0),1))||'%' \"Porc.Verde\"\n" +
           "from grta_declara_perfil_tmp perfil,GRTA_COMPENDIO_DETALLE aduana\n" +
           "where \n" +
           "perfil.id_proceso=:pProceso AND \n" +
           "aduana.id_compendio=35 AND \n" +
           "aduana.referencia1=substr(perfil.id_declaracion,instr(perfil.id_declaracion,'@',1,1)+1,instr(perfil.id_declaracion,'@',1,2)-instr(perfil.id_declaracion,'@',1,1)-1)\n" +
           "group by substr(perfil.id_declaracion,instr(perfil.id_declaracion,'@',1,1)+1,instr(perfil.id_declaracion,'@',1,2)-instr(perfil.id_declaracion,'@',1,1)-1),\n" +
           "aduana.descripcion";


        public const String ResultadoPorRegimenTabla = "SELECT \n" +
           "gen.sad_typ_proc \"Código\",\n" +
           "regimen.descripcion \"Descripción\", count(0) \"Total DMs\", \n" +
           "to_char(trunc(sum(decode(perfil.nivel_referencia,6,1,0))*100/count(0),1))||'%' \"Porc.Rojo\",\n" +
           "to_char(trunc(sum(decode(perfil.nivel_referencia,7,1,0))*100/count(0),1))||'%' \"Porc.Amarillo\",\n" +
           "to_char(trunc(sum(decode(perfil.nivel_referencia,8,1,0))*100/count(0),1))||'%' \"Porc.Verde\"\n" +
           "from grta_declara_perfil_tmp perfil,SAD_GEN GEN,\n" +
           "GRTA_COMPENDIO_DETALLE regimen\n" +
           "where \n" +
           "perfil.id_proceso=:pProceso AND\n" +
           "gen.KEY_YEAR= substr(perfil.id_declaracion,1,4) AND\n" +
           "gen.KEY_CUO= substr(perfil.id_declaracion,instr(perfil.id_declaracion,'@',1,1)+1,instr(perfil.id_declaracion,'@',1,2)-instr(perfil.id_declaracion,'@',1,1)-1) AND \n" +
           "gen.KEY_DEC= substr(perfil.id_declaracion,instr(perfil.id_declaracion,'@',1,2)+1,instr(perfil.id_declaracion,'@',1,3)-instr(perfil.id_declaracion,'@',1,2)-1) AND\n" +
           "gen.KEY_NBER= substr(perfil.id_declaracion,instr(perfil.id_declaracion,'@',1,3)+1,length(perfil.id_declaracion)-instr(perfil.id_declaracion,'@',1,3)) AND\n" +
           "gen.sad_num=0 AND\n" +
           "gen.lst_ope='U' AND        \n" +
           "regimen.id_compendio=48 AND \n" +
           "regimen.referencia1=gen.sad_typ_proc\n" +
           "group by gen.sad_typ_proc,regimen.descripcion";

        public const String CompendioEdit = "SELECT id_compendio,tipo_tabla,tipo_codificacion, UPPER(nombre) nombre, UPPER(descripcion) descripcion  "
                           + " FROM GRTA_COMPENDIO_GENERAL"
                           + " WHERE id_compendio=?";


        public const String CompendioElemento = "SELECT id_compendio, UPPER(nombre) nombre"
                           + " FROM GRTA_COMPENDIO_GENERAL WHERE TIPO_TABLA=1";
        /*WMarcia 05-12-2017 Usado*/
        public const String VariableCatalogo = "     SELECT CODIGO_VARIABLE codigo, DESCRIPCION_BREVE descripcion FROM GRTA_VARIABLES\n" +
           "     WHERE TIPO_VARIABLE=116 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)";

        /*
         * Modificado por MV ( alias)
         *  MgrCompendioModificacion="SELECT TO_CHAR(VARIACIONES.FECHA_INGRESO,'DD/MM/YYYY HH24:MI') \"Fecha Modificación\", \n" + 
                                     "SESSIONES.CODIGO_USUARIO||'-'||(SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=63 AND UPPER(CODIGO_ALTERNO)=UPPER(SESSIONES.CODIGO_USUARIO)) \"Nombre Usuario\", \n" + 
                                     "VARIACIONES.NOMBRE_CAMPO \"Casilla Modificada\", DECODE(VARIACIONES.VALOR_ANTIGUO,' ','EN BLANCO',NULL,'EN BLANCO',SUBSTR(VARIACIONES.VALOR_ANTIGUO,1,15)) \"Valor Antiguo\", SUBSTR(VARIACIONES.VALOR_NUEVO,1,15) \"Valor Nuevo\" \n" + 
                                     "FROM GRTA_VARIACIONES VARIACIONES, GRTA_SESSION SESSIONES\n" + 
                                     "WHERE\n" + 
                                     "VARIACIONES.NOMBRE_TABLA=:pNombreTabla AND\n" + 
                                     "VARIACIONES.CLAVE_REGISTRO=:pRegistro AND\n" + 
                                     "SESSIONES.ID_SESSION=VARIACIONES.ID_SESSION\n" + 
                                     "ORDER BY VARIACIONES.FECHA_INGRESO DESC"),    
         */
        public const String CompendioModificacion = "SELECT TO_CHAR(VARIACIONES.FECHA_INGRESO,'DD/MM/YYYY HH24:MI') \"fecha_ingreso\", \n" +
                                    "SESSIONES.CODIGO_USUARIO||'-'||(SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=63 AND UPPER(CODIGO_ALTERNO)=UPPER(SESSIONES.CODIGO_USUARIO)) \"nombre_usuario\", \n" +
                                    "VARIACIONES.NOMBRE_CAMPO \"nombre_campo\", DECODE(VARIACIONES.VALOR_ANTIGUO,' ','EN BLANCO',NULL,'EN BLANCO',SUBSTR(VARIACIONES.VALOR_ANTIGUO,1,15)) \"valor_antiguo\", SUBSTR(VARIACIONES.VALOR_NUEVO,1,15) \"valor_nuevo\" \n" +
                                    "FROM GRTA_VARIACIONES VARIACIONES, GRTA_SESSION SESSIONES\n" +
                                    "WHERE\n" +
                                    "VARIACIONES.NOMBRE_TABLA=:nombreTabla AND\n" +
                                    "VARIACIONES.CLAVE_REGISTRO=:claveRegistro AND\n" +
                                    "SESSIONES.ID_SESSION=VARIACIONES.ID_SESSION\n" +
                                    "ORDER BY VARIACIONES.FECHA_INGRESO DESC";
        /*WMarcia 12-12-2017 Usado*/
        public const String Variacion = "SELECT TO_CHAR(VARIACIONES.FECHA_INGRESO,'DD/MM/YYYY HH24:MI') \"fecha_ingreso\", \n" +
                   "SESSIONES.CODIGO_USUARIO||'-'||(SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=63 AND UPPER(CODIGO_ALTERNO)=UPPER(SESSIONES.CODIGO_USUARIO)) \"nombre_usuario\", \n" +
                   "VARIACIONES.NOMBRE_CAMPO \"nombre_campo\", DECODE(VARIACIONES.VALOR_ANTIGUO,' ','EN BLANCO',NULL,'EN BLANCO',SUBSTR(VARIACIONES.VALOR_ANTIGUO,1,15)) \"valor_antiguo\", SUBSTR(VARIACIONES.VALOR_NUEVO,1,15) \"valor_nuevo\" \n" +
                   "FROM GRTA_VARIACIONES VARIACIONES, GRTA_SESSION SESSIONES\n" +
                   "WHERE\n" +
                   "VARIACIONES.NOMBRE_TABLA=:nombreTabla AND\n" +
                   "VARIACIONES.CLAVE_REGISTRO=:claveRegistro AND\n" +
                   "SESSIONES.ID_SESSION=VARIACIONES.ID_SESSION\n" +
                   "ORDER BY VARIACIONES.FECHA_INGRESO DESC";

        /*Aumentado, Usado para cargar el combo filtro en base a sujeto Riesgo en Medidas/Nueva Versión*/
        public const String VariablesSujetoRiesgoExtraccionComboMed = "SELECT CODIGO_VARIABLE CODIGO, UPPER(DESCRIPCION_BREVE)  DESCRIPCION FROM GRTA_VARIABLES WHERE SUJETO_RIESGO = TO_NUMBER(?) AND\n" +
                   " GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL))=0 AND \n" +
                   " TIPO_VARIABLE=57 AND TIPO_DATO IN (80,81) AND MODO_USO=22 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' ORDER BY DESCRIPCION_BREVE";

        //------------------------------------------------------------------------------------------------------------------------            
        //@author: RTrujillo | @version: 1.0, 02/04/2012 | @descripcion: Recupera el código y la descripción del sujeto de riesgo.
        //TODO: modificado por pescarcena
        /*WMarcia 05-12-2017 Usado*/
        public const String SujetoRiesgoCombo = "SELECT SUJETO_RIESGO codigo, DESCRIPCION_BREVE descripcion FROM GRTA_SUJETO_RIESGO\n" +
                               " WHERE SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE) ORDER BY SUJETO_RIESGO ASC";

        /* MODIFICADO MVALLE, se usa para los combos de registro de medida Usado */
        public const String SujetoRiesgoUsuario = "SELECT SUJ.SUJETO_RIESGO codigo, SUJ.DESCRIPCION_BREVE descripcion \n" +
           "FROM GRTA_SUJETO_RIESGO SUJ, GRTA_SESSION SESION, GRTA_COMPENDIO_DETALLE COMPENDIO, GRTA_COMPENDIO_SUBDETALLE SUBDETALLE\n" +
           "WHERE \n" +
           "SESION.ID_SESSION=TO_NUMBER(?) AND\n" +
           "COMPENDIO.ID_COMPENDIO=63 AND\n" +
           "COMPENDIO.CODIGO_ALTERNO=SESION.CODIGO_USUARIO AND\n" +
           "SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND NVL(COMPENDIO.FECHA_FIN_VIGENCIA,SYSDATE) AND\n" +
           "SUBDETALLE.ID_DETALLE_GRUPO=COMPENDIO.ID_DETALLE AND\n" +
           "SYSDATE BETWEEN SUBDETALLE.FECHA_INICIO_VIGENCIA AND NVL(SUBDETALLE.FECHA_FIN_VIGENCIA,SYSDATE) AND\n" +
           "SUJ.SUJETO_RIESGO=TO_NUMBER(SUBDETALLE.CODIGO_ALTERNO) AND\n" +
           "SYSDATE BETWEEN SUJ.FECHA_INICIO_VIGENCIA AND NVL(SUJ.FECHA_FIN_VIGENCIA,SYSDATE)\n" +
           "ORDER BY SUJ.DESCRIPCION_BREVE";


        public const String SujetoRiesgoComboInd = "SELECT SUJETO_RIESGO, DESCRIPCION_BREVE FROM GRTA_SUJETO_RIESGO\n" +
                               " WHERE TIPO_SELECCION=67 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE) ORDER BY SUJETO_RIESGO ASC";
        //mvalle usado   
        public const String SujetoRiesgoGrupal = "SELECT SUJETO_RIESGO codigo, DESCRIPCION_BREVE descripcion \n" +
           "FROM GRTA_SUJETO_RIESGO\n" +
           "WHERE \n" +
           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND \n" +
           "NVL(FECHA_FIN_VIGENCIA,SYSDATE) AND \n" +
          //"TIPO_SELECCION=68 ORDER BY SUJETO_RIESGO ASC"),                        
          "TIPO_SELECCION=? ORDER BY SUJETO_RIESGO ASC";

        //@author: RTrujillo | @version: 1.0, 02/04/2012 | @descripcion:
        //mv en desuso por MgrCompendioDetalleComboReferencia
        //MgrTipoMedidaCombo="SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE\n" + 
        //                    " WHERE ID_COMPENDIO=7 AND REFERENCIA1=1 AND\n" + 
        //                   " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE)"),

        //@author: RTrujillo | @version: 1.0, 02/04/2012 | @descripcion: Recupera el código y la descripción de las politicas de Control
        //mvalle usado
        public const String PoliticaCombo = "SELECT ID_POLITICA codigo, DESCRIPCION_BREVE descripcion FROM GRTA_POLITICA_INSTITUCIONAL\n" +
                              " WHERE SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)\n";


        public const String ClaseMedidaCombo = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE\n" +
                               " WHERE ID_COMPENDIO=2 AND SUJETO_RIESGO = TO_NUMBER(?) AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)\n" +
                               " ORDER BY REFERENCIA1";


        public const String JerarquiaMedCombo = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE\n" +
                               " WHERE ID_COMPENDIO=1 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";
        //Ocamacho 31JUL: Se agregó el AND antes del sujeto de riesgo:
        public const String VarDepenCombo = "SELECT CODIGO_VARIABLE codigo, DESCRIPCION_BREVE descripcion FROM GRTA_VARIABLES" +
                              " WHERE TIPO_VARIABLE = 58 AND SUJETO_RIESGO = TO_NUMBER(?) AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";


        public const String FrecuenciaCombo = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE\n" +
                               "WHERE ID_COMPENDIO=36 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";

        //Ocamacho: 07 AGO; se hace referencia específicamente al tipo de variable 57.
        //usado 16/01/2018
        // Programa Fiscalización/Modificar : Combo Variable Filtro
        public const String VariableFiltroCombo = "SELECT CODIGO_VARIABLE, UPPER(DESCRIPCION_BREVE) DESCRIPCION_BREVE FROM GRTA_VARIABLES\n" +
                                   " WHERE SUJETO_RIESGO = TO_NUMBER(:id_Sujeto) AND TIPO_VARIABLE =57 AND\n" +
                                   " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%'\n";

        //mvalle usado en flitro de registrar nueva medida
        public const String ExprLogicaFiltroMedida = "SELECT DESC_USUARIO descUsuario FROM GRTA_TMP_FILTROS_MED_COND WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivo) AND ID_FILTROS = TO_NUMBER(:pIdFiltro)";

        //nuevo



        public const String ExprLogicaFiltroCondMedida = "SELECT DESC_USUARIO FROM GRTA_TMP_FILTROS_MED_COND WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivo) AND ID_FILTROS = TO_NUMBER(:pIdFiltro)";

        //WMarcia 28-01-2018 Nuevo


        //Ocamacho: 07 AGO; se hace referencia específicamente al tipo de variable 57.
        //usado 16/01/2018   

        public const String OperadorMatematicoCombo = "SELECT OPERADOR_MATEMATICO codigo, SIMBOLO_USUARIO simbolo  FROM GRTA_OPERADOR_MATEMATICO WHERE TIPO_OPERADOR = 54";

        /*Usado WMarcia 21-01-2018*/
        public const String SimboloMatematicoCombo = "SELECT OPERADOR_MATEMATICO codigo, SIMBOLO_USUARIO descripcion FROM GRTA_OPERADOR_MATEMATICO WHERE OPERADOR_MATEMATICO IN (1,2,3,4,5,6,7,8,9,14,15,16,17,22) ORDER BY 1";


        public const String SimboloMatematico = "SELECT OPERADOR_MATEMATICO codigo, SIMBOLO_MATEMATICO simbolo FROM GRTA_OPERADOR_MATEMATICO WHERE OPERADOR_MATEMATICO IN (1,2,3,4,5,6,9,14,17,22) ORDER BY 1";


        public const String JoinCombo = "SELECT ID_DETALLE,NOMBRE \n" +
                       "   FROM GRTA_COMPENDIO_DETALLE\n" +
                       "   WHERE\n" +
                       "   ID_COMPENDIO=44 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA) ORDER BY 1";

        // Recupera el código y la descripción de los Tipos de Valores SQL
        public const String TipoValorCombo = "SELECT id_detalle codigo, nombre FROM GRTA_COMPENDIO_DETALLE WHERE id_compendio = 13 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";


        public const String IndicadoresOperadorMatematico = "SELECT indicador_suministrado, indicador_parametro, indicador_lista, indicador_sin_valor FROM GRTA_OPERADOR_MATEMATICO  WHERE OPERADOR_MATEMATICO = TO_NUMBER(?)";


        public const String TipoValorListaCombo = "SELECT DETALLE.ID_DETALLE codigo, DETALLE.NOMBRE descripcion\n" +
               " FROM GRTA_COMPENDIO_GENERAL GENERAL, GRTA_COMPENDIO_DETALLE DETALLE, GRTA_VARIABLES VARIABLES \n" +
               " WHERE GENERAL.TIPO_TABLA=3 AND\n" +
               " DETALLE.ID_COMPENDIO=GENERAL.ID_COMPENDIO AND \n" +
               " DETALLE.SUJETO_RIESGO = :pSujetoRiesgo AND\n" +
               " SYSDATE BETWEEN DETALLE.FECHA_INICIO_VIGENCIA AND DECODE(DETALLE.FECHA_FIN_VIGENCIA,'',SYSDATE) AND\n" +
               " VARIABLES.CODIGO_VARIABLE = :pCodigoVariable AND\n" +
               " (DETALLE.VARIABLE_CATALOGO=VARIABLES.VARIABLE_CATALOGO OR  DETALLE.VARIABLE_CATALOGO IS NULL)\n" +
               " ORDER BY DETALLE.NOMBRE\n";


        public const String TipoValorParametroCombo = "SELECT ID_PARAMETRO codigo, DESCRIPCION_BREVE descripcion FROM GRTA_PARAMETROS WHERE SUJETO_RIESGO = TO_NUMBER(?)  AND CLASE_PARAMETRO IN (62,63) AND\n" +
                                       " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";


        public const String AccionesMedidaCombo = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=6 AND" +
                                   " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";


        public const String TipoRespuestaCombo = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=11 AND" +
                                   " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";


        public const String TipoBanderaCombo = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=15 AND" +
                                   " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";

        //mvalle -> medidas-condiciones y acciones - %revision
        public const String TipoRespCanalSelectividadCombo = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=3 AND SUJETO_RIESGO=TO_NUMBER(?) AND CODIGO_ALTERNO IS NOT NULL AND" +
                                               " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";


        public const String TipoPeriodoCombo = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=18 AND " +
                                               " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";


        public const String CanalDeclaracionCombo = "SELECT CODIGO_ALTERNO codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=3 AND SUJETO_RIESGO=1 AND CODIGO_ALTERNO IS NOT NULL AND" +
                                               " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";

        //mvalle -> medidas-condiciones y acciones - CODIGO VALIDACION
        public const String TipoRespCodigoValidacionCombo = "SELECT ID_DETALLE codigo, DESCRIPCION  FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=100 AND SUJETO_RIESGO=TO_NUMBER(?) AND" +
                                               " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";

        //mvalle -> medidas-condiciones y acciones - VALOR
        public const String TipoRespValorCombo = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=38 AND" +
                               " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";


        public const String DatosTipoRespuestaTemp = "SELECT CM.CONDICION_MEDIDAS condicion, CM.ID_COMPONENTES componente, \n" +
           " ( CASE CM.TIPO_RESPUESTA WHEN 27 THEN CM.CANAL_SELECTIVIDAD WHEN 28 THEN CM.CODIGO_VALIDACION WHEN 29 THEN CM.VALOR_RESPUESTA END ) codigo_tipo," +
           " ( CASE CM.TIPO_RESPUESTA WHEN 27 THEN (SELECT D.NOMBRE FROM GRTA_COMPENDIO_DETALLE d WHERE D.ID_COMPENDIO = 3 AND D.ID_DETALLE = CM.CANAL_SELECTIVIDAD)" +
           "   WHEN 28 THEN  ' ' WHEN 29 THEN (SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=38 AND ID_DETALLE = CM.VALOR_RESPUESTA) END ) valor_tipo," +
           " ( CASE CM.TIPO_RESPUESTA WHEN 27 THEN TO_CHAR(CM.VALOR) WHEN 28 THEN" +
           " ( SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=100 AND ID_DETALLE = CM.CODIGO_VALIDACION ) ELSE TO_CHAR(CM.VALOR) END ) valor," +
           " ( CASE CM.TIPO_RESPUESTA WHEN 29 THEN TO_CHAR(CM.CANTIDAD) ELSE ' ' END) cantidad," +
           " ( CASE CM.TIPO_RESPUESTA WHEN 29 THEN (SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=18 AND ID_DETALLE = CM.PERIODO) ELSE ' ' END) periodo" +
           " FROM GRTA_TMP_COMPONENTES_MED cm, GRTA_TMP_CONDICIONES_MEDIDA cond WHERE CM.CONDICION_MEDIDAS = COND.CONDICION_MEDIDAS" +
           " AND CM.TIPO_SALIDA = :pTipoSalida AND COND.ID_SESSION = :pIdSession AND COND.CONSECUTIVO_SESSION = :pConsecutivoSession" +
           " AND COND.LINEA_CONDICION = :pLineaCondicion ORDER BY  CM.ID_COMPONENTES ASC";

        //mvalle usado lista de oritencion de medidas
        /*
     public const String ObtenerBanderasMedidaTemp="SELECT ORIENTACION_MEDIDAS \"Empty\"," + 
                                        " NVL(TO_CHAR(CONDICION_MEDIDAS), ' ') \"#Cond.\", DECODE(TO_CHAR(TIPO_SALIDA), '88', 'SI', '89', 'NO', ' ') \"Tipo Salida\"," +
                                        " (SELECT T.NOMBRE  FROM GRTA_COMPENDIO_DETALLE t WHERE T.ID_DETALLE =  TIPO_ORIENTACION AND t.ID_COMPENDIO=15" + 
                                        " AND TO_DATE(m.FECHA_REGISTRO, 'DD/MM/YYYY HH24:MI:SS' ) BETWEEN  TO_DATE(t.FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY HH24:MI:SS') AND TO_DATE(DECODE(t.FECHA_FIN_VIGENCIA,'',SYSDATE), 'DD/MM/YYYY HH24:MI:SS') )   \"Tipo Orientación\"," + 
                                        " DESCRIPCION \"Descripción\", NVL(TO_CHAR( FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY HH12:MI:SS AM'), 'Indefinido')||' - '||NVL(TO_CHAR(FECHA_FIN_VIGENCIA, 'DD/MM/YYYY HH12:MI:SS AM'), ' ') \"Periodo Vigencia\" " + 
                                        " FROM GRTA_TMP_ORIENTACION_MEDIDAS m WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivoSession)" +
                                        " ORDER BY ORIENTACION_MEDIDAS"),
        */
        //usado
        public const String ObtenerBanderasMedidaTemp = "SELECT ORIENTACION_MEDIDAS orientacionMedidas," +
                   " NVL(TO_CHAR(CONDICION_MEDIDAS), ' ') condicion, "
                   + "DECODE(TO_CHAR(TIPO_SALIDA), '88', 'SI', '89', 'NO', ' ') tipoSalida," +
                   " (SELECT T.NOMBRE  FROM GRTA_COMPENDIO_DETALLE t WHERE T.ID_DETALLE =  TIPO_ORIENTACION AND t.ID_COMPENDIO=15" +
                   " AND TO_DATE(m.FECHA_REGISTRO, 'DD/MM/YYYY HH24:MI:SS' ) BETWEEN  TO_DATE(t.FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY HH24:MI:SS') AND TO_DATE(DECODE(t.FECHA_FIN_VIGENCIA,'',SYSDATE), 'DD/MM/YYYY HH24:MI:SS') )   tipoOrientacion," +
                   " DESCRIPCION descripcion, "
                   + "NVL(TO_CHAR( FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY HH12:MI:SS AM'), 'Indefinido')||' - '||NVL(TO_CHAR(FECHA_FIN_VIGENCIA, 'DD/MM/YYYY HH12:MI:SS AM'), ' ') periodoVigencia " +
                   " FROM GRTA_TMP_ORIENTACION_MEDIDAS m WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivoSession)" +
                   " ORDER BY ORIENTACION_MEDIDAS";

        //mvalle autocomplete medidas "para"
        public const String ConsultarMedidasPrecedentesAutoComplete = "SELECT ID_MEDIDA||'-'||VERSION_MEDIDA codigo, NOMBRE_MEDIDA descripcion FROM GRTA_MEDIDAS" +
                                                       " WHERE UPPER(NOMBRE_MEDIDA) LIKE UPPER('%' ||:pNombreMedida || '%')" +
                                                       " AND SUJETO_RIESGO=TO_NUMBER(:pSujetoRiesgo) AND TIPO_MEDIDA=TO_NUMBER(:pTipoMedida) AND ESTADO_MEDIDA=42";

        //usado
        public const String ConsultarDescripcionMedidaPrecedente = "SELECT NOMBRE_MEDIDA nombre_medida FROM GRTA_MEDIDAS WHERE  ID_MEDIDA||'-'||VERSION_MEDIDA = :pIdMedidaVersion" +
                                                   " AND SUJETO_RIESGO=TO_NUMBER(:pSujetoRiesgo) AND TIPO_MEDIDA=TO_NUMBER(:pTipoMedida) AND ESTADO_MEDIDA=42";

        //mvalle autocomplete medidas "para"
        public const String ConsultarDestinatarioAutoComplete = "SELECT ID_DETALLE codigo, DESCRIPCION||' ('||NOMBRE||')' descripcion FROM GRTA_COMPENDIO_DETALLE" +
                                                   " WHERE ( UPPER(DESCRIPCION) LIKE UPPER('%'||:pParametro||'%') OR UPPER(NOMBRE) LIKE UPPER('%'||:pParametro||'%') ) AND" +
                                                   " ID_COMPENDIO=63 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";
        public const String ConsultarValorParametrosCombo = "SELECT ID_PARAMETRO, DESCRIPCION_BREVE FROM GRTA_PARAMETROS WHERE SUJETO_RIESGO = ? AND CLASE_PARAMETRO IN (62, 63) AND" +
                                               " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";

        //mv consultar operaciones en el momento de obtener el detalle de la medida
        public const String ConsultarOperacionesMedidaTabla = "SELECT ROWNUM numero, x.operacion operacion, x.fecha_operacion fecha, x.codigo_usuario||' - '||x.usuario usuario, \n"
                   + "x.comentario comentario \n"
                   + "FROM (SELECT (SELECT T.DESCRIPCION FROM GRTA_COMPENDIO_DETALLE t WHERE T.ID_DETALLE = O.TIPO_OPERACION AND T.ID_COMPENDIO = 27 ) operacion, TO_CHAR(O.FECHA_OPERACION, 'DD/MM/YYYY HH12:MI:SS AM') fecha_operacion, \n"
                   + "S.CODIGO_USUARIO, (SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=63 AND CODIGO_ALTERNO=S.CODIGO_USUARIO AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)) usuario, \n"
                   + "COALESCE(O.COMENTARIO, 'SIN COMENTARIOS') COMENTARIO FROM GRTA_OPERACIONES_MEDIDAS O, GRTA_SESSION S \n"
                   + "WHERE O.SESSION_OPERACION = S.ID_SESSION AND O.ID_MEDIDA = ? AND O.VERSION_MEDIDA = ? \n"
                   + "ORDER BY O.OPERACIONES_MEDIDAS ASC) x \n";


        public const String ConsultarTabs = "SELECT ID_DETALLE,NOMBRE,CODIGO_ALTERNO,REFERENCIA1\n" +
                            "FROM GRTA_COMPENDIO_DETALLE\n" +
                            "WHERE\n" +
                            "ID_COMPENDIO = 43 AND\n" +
                            "(SUJETO_RIESGO IN (SELECT SUJETO_RIESGO FROM GRTA_EVALUACION WHERE ID_EVALUACION=TO_NUMBER(?))\n" +
                            "OR SUJETO_RIESGO IS NULL) AND\n" +
                            "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,NULL,SYSDATE,FECHA_FIN_VIGENCIA)\n" +
                            "ORDER BY TO_NUMBER(REFERENCIA1)";


        public const String ContenidoTab = "SELECT GRPK_OPERACIONES_MEDIDA.GRFN_CONTENIDO_TAB(:pTesteo,:pCodigoTab) FROM DUAL";


        public const String ConsultarBanderasMedidaTabla = "SELECT ROWNUM \"#\", x.condicion_medidas \"#Cond.\", x.tipo_salida \"Tipo Salida\", x.tipo_orientacion \"Tipo Orientación\", x.descripcion \"Descripción\",x.fecha_ini||' - '||x.fecha_fin \"Periodo Vigencia\"  \n" +
                                              "FROM ( SELECT" +
                                              " NVL( (SELECT TO_CHAR(c.ORDEN_CONDICION) FROM GRTA_CONDICION_MEDIDAS c WHERE C.ID_MEDIDA = O.ID_MEDIDA AND C.VERSION_MEDIDA = O.VERSION_MEDIDA AND C.CONDICION_MEDIDAS = O.CONDICION_MEDIDAS), ' ') condicion_medidas, " +
                                              " DECODE(TO_CHAR(TIPO_SALIDA), '88', 'SI', '89', 'NO', ' ') tipo_salida," +
                                              " ( SELECT T.NOMBRE  FROM GRTA_COMPENDIO_DETALLE t WHERE T.ID_DETALLE = o.TIPO_ORIENTACION AND t.ID_COMPENDIO=15 ) TIPO_ORIENTACION," +
                                              " O.DESCRIPCION," +
                                              " NVL(TO_CHAR( o.FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY HH12:MI:SS AM'), ' ') FECHA_INI," +
                                              " NVL(TO_CHAR(o.FECHA_FIN_VIGENCIA, 'DD/MM/YYYY HH12:MI:SS AM'), ' ') FECHA_FIN" +
                                              " FROM GRTA_ORIENTACION_MEDIDAS o, GRTA_MEDIDAS m" +
                                              " WHERE O.VERSION_MEDIDA = M.VERSION_MEDIDA AND O.ID_MEDIDA = M.ID_MEDIDA" +
                                              " AND O.VERSION_MEDIDA = :pVersionMedida AND O.ID_MEDIDA = :pIdMedida" +
                                              " ORDER BY O.ORIENTACION_MEDIDAS ASC" +
                                              " ) x";

        /*Usado WMarcia 18-03-2018*/
        public const String ConsultarDestinatariosMedidaTabla = "SELECT ROWNUM numero, x.cod_destinatario, x.destinatario FROM (\n" +
                                               " SELECT MM.ID_PERSONA cod_destinatario,\n" +
                                               " ( SELECT t.DESCRIPCION||' ('||t.NOMBRE||')' descripcion FROM GRTA_COMPENDIO_DETALLE t WHERE T.ID_DETALLE = MM.ID_PERSONA AND t.ID_COMPENDIO=63 ) destinatario\n" +
                                               " FROM GRTA_MENSAJE_MEDIDAS MM, GRTA_MEDIDAS M\n" +
                                               " WHERE MM.ID_MEDIDA = M.ID_MEDIDA AND MM.VERSION_MEDIDA = M.VERSION_MEDIDA\n" +
                                               " AND M.ID_MEDIDA = ? AND M.VERSION_MEDIDA = ?\n" +
                                               " ORDER BY MM.MENSAJE_MEDIDAS ASC\n" +
                                               " ) x\n";


        public const String ConsultarCondicionesAccionesMedida = "SELECT x.CONDICION_MEDIDAS, x.ORDEN_CONDICION,  NVL(filtro_condicion, ' ') filtro_condicion,  \n" +
                                               "MAX(ACCION_SI) TIPO_ACCION_SI, \n" +
                                               "NVL(GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAX(ACCION_SI)),' ') ACCION_SI,\n" +
                                               "MAX(ACCION_NO)  TIPO_ACCION_NO,\n" +
                                               "NVL(GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAX(ACCION_NO)),' ') ACCION_NO, \n" +
                                               "MAX(RESPUESTA_SI) TIPO_RESPUESTA_SI, \n" +
                                               "NVL(GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAX(RESPUESTA_SI)),' ') RESPUESTA_SI,\n" +
                                               "MAX(RESPUESTA_NO) TIPO_RESPUESTA_NO,\n" +
                                               "NVL(GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAX(RESPUESTA_NO)), ' ') RESPUESTA_NO\n" +
                                               "FROM (\n" +
                                               "SELECT C.CONDICION_MEDIDAS, C.ORDEN_CONDICION,  \n" +
                                                  "GRPK_OPERACIONES_COMUNES.GRFN_CONSULTAR_EXPRESIO_FILTRO( C.ID_MEDIDA, C.VERSION_MEDIDA, C.CONDICION_MEDIDAS, 'P') filtro_condicion, \n" +
                                               "DECODE(COMP.TIPO_SALIDA, 88, COMP.TIPO_ACCION, 0) ACCION_SI ,\n" +
                                               "DECODE(COMP.TIPO_SALIDA, 89, COMP.TIPO_ACCION, 0) ACCION_NO, \n" +
                                               "DECODE(COMP.TIPO_SALIDA, 88,COMP.TIPO_RESPUESTA, 0) RESPUESTA_SI , \n" +
                                               "DECODE(COMP.TIPO_SALIDA, 89, COMP.TIPO_RESPUESTA, 0) RESPUESTA_NO\n" +
                                               "FROM GRTA_CONDICION_MEDIDAS c,  GRTA_COMPONENTE_MEDIDAS comp, GRTA_MEDIDAS m \n" +
                                               "WHERE C.CONDICION_MEDIDAS = COMP.CONDICION_MEDIDAS\n" +
                                               "AND C.VERSION_MEDIDA = M.VERSION_MEDIDA AND C.ID_MEDIDA = M.ID_MEDIDA \n" +
                                               "AND M.VERSION_MEDIDA = :pVersionMedida\n" +
                                               "AND M.ID_MEDIDA = :pIdMedida\n" +
                                               "GROUP BY C.CONDICION_MEDIDAS, C.ORDEN_CONDICION,C.ID_MEDIDA, C.VERSION_MEDIDA, C.CONDICION_MEDIDAS,COMP.TIPO_SALIDA,COMP.TIPO_ACCION,COMP.TIPO_RESPUESTA   \n" +
                                               ") x\n" +
                                                  "GROUP BY x.CONDICION_MEDIDAS, x.ORDEN_CONDICION, x.filtro_condicion" +
                                                  " ORDER BY x.CONDICION_MEDIDAS, x.ORDEN_CONDICION";
        /*Usado en Implementar Medida/Detalle/ Valor Respuesta*/
        public const String ConsultarValoresTipoRespuestaMed = "SELECT ROWNUM No , x.* FROM (\n" +
                                               " SELECT \n" +
                                               " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN comp.CANAL_SELECTIVIDAD WHEN 28 THEN comp.CODIGO_VALIDACION WHEN 29 THEN comp.VALOR_RESPUESTA END ) codigo_tipo,  \n" +
                                               " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN (SELECT D.NOMBRE FROM GRTA_COMPENDIO_DETALLE d WHERE D.ID_COMPENDIO = 3 AND D.ID_DETALLE = comp.CANAL_SELECTIVIDAD)  \n" +
                                               " WHEN 28 THEN  ' ' WHEN 29 THEN (SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=38 AND ID_DETALLE = comp.VALOR_RESPUESTA) END ) valor_tipo,  \n" +
                                               " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN TO_CHAR(COMP.VALOR) WHEN 28 THEN  \n" +
                                              " ( SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=100 AND ID_DETALLE = COMP.CODIGO_VALIDACION ) ELSE NVL(TO_CHAR(COMP.VALOR), GRPK_OPERACIONES_COMUNES.GRFN_CONS_EXPRESION_VALCOMPUES(COMP.ID_COMPONENTES, COND.CONDICION_MEDIDAS, COMP_MED.TIPO_SALIDA, 'P') ) END ) valor  ,\n" +
                                               " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN  ( '['|| TO_CHAR(NVL(COMP.RANGO_BASE, 0))||' ; ' ||TO_CHAR(NVL(COMP.rango_techo, 0))|| ']' )  ELSE ' ' END ) rango,\n" +
                                              " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 29 THEN TO_CHAR(COMP.CANTIDAD) ELSE ' ' END) cantidad," +
                                              " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 29 THEN (SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=18 AND ID_DETALLE = COMP.PERIODO) ELSE ' ' END) periodo" +
                                               " FROM GRTA_MEDIDAS med, GRTA_CONDICION_MEDIDAS cond, GRTA_COMPONENTE_MEDIDAS comp_med, GRTA_COMPONENTES comp\n" +
                                               " WHERE MED.VERSION_MEDIDA = COND.VERSION_MEDIDA AND MED.ID_MEDIDA = COND.ID_MEDIDA \n" +
                                               " AND COND.CONDICION_MEDIDAS = COMP_MED.CONDICION_MEDIDAS \n" +
                                               " AND COMP_MED.ID_COMPONENTES = COMP.ID_COMPONENTES\n" +
                                               " AND COMP_MED.TIPO_SALIDA = :pTipoSalida\n" +
                                               " AND cond.CONDICION_MEDIDAS = :pCondicionMedida\n" +
                                               " AND MED.VERSION_MEDIDA = :pVersionMedida\n" +
                                               " AND MED.ID_MEDIDA = :pIdMedida\n" +
                                               " ORDER BY COND.ORDEN_CONDICION, COMP_MED.COMPONENTE_MEDIDAS, COMP.ID_COMPONENTES\n" +
                                               " ) x";
        /*Usado WMarcia 17-01-2018*/
        public const String TablaDatos = "SELECT SUJETO.TABLA_DATOS codigo, DETALLE.CODIGO_ALTERNO||' ('||DETALLE.NOMBRE||')' descripcion\n" +
                           "FROM GRTA_TABLAS_SUJETO SUJETO, GRTA_COMPENDIO_DETALLE DETALLE\n" +
                           "WHERE\n" +
                           "SUJETO.SUJETO_RIESGO=? AND\n" +
                           "SUJETO.ORIGEN_FUENTE=DECODE(?,'T',504,505) AND\n" +
                           "SYSDATE BETWEEN SUJETO.FECHA_INICIO_VIGENCIA AND DECODE(SUJETO.FECHA_FIN_VIGENCIA,'',SYSDATE,SUJETO.FECHA_FIN_VIGENCIA) AND\n" +
                           "DETALLE.ID_DETALLE=SUJETO.TABLA_DATOS";


        public const String TablaDatosCatalogo = "SELECT ID_DETALLE TABLA, NOMBRE FROM GRTA_COMPENDIO_DETALLE \n" +
                           "WHERE\n" +
                           "ID_COMPENDIO=26 AND\n" +
                           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA) AND\n" +
                           "REFERENCIA1='A'";
        //OCamacho 22JUL2012: Filtro para los operadores de comercio:                     
        public const String OperadorComercio = "SELECT VARIABLES.CODIGO_VARIABLE,DECODE(COMPENDIO.ID_DETALLE,120,'NIT Operador',VARIABLES.DESCRIPCION_BREVE) DESCRIPCION_BREVE\n" +
                           "FROM GRTA_COMPENDIO_DETALLE COMPENDIO, GRTA_VARIABLES VARIABLES\n" +
                           "WHERE\n" +
                           "COMPENDIO.ID_COMPENDIO=26 AND \n" +
                           "COMPENDIO.REFERENCIA1='A' AND\n" +
                           "COMPENDIO.REFERENCIA2='1' AND\n" +
                           "VARIABLES.TABLA_TRANSACCIONAL=COMPENDIO.ID_DETALLE AND\n" +
                           "VARIABLES.TIPO_VARIABLE=116 AND--CATALOGO\n" +
                           "SYSDATE BETWEEN VARIABLES.FECHA_INICIO_VIGENCIA AND DECODE(VARIABLES.FECHA_FIN_VIGENCIA,'',SYSDATE,VARIABLES.FECHA_FIN_VIGENCIA)";


        public const String FuenteDatosCatalogo = "SELECT ID_DETALLE, CODIGO_ALTERNO||' ('||NOMBRE||')' NOMBRE FROM GRTA_COMPENDIO_DETALLE \n" +
                           "WHERE\n" +
                           "ID_COMPENDIO=26 AND (REFERENCIA1 IS NULL OR REFERENCIA1<>'A') AND\n" +
                           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)";
        /*Usado WMarcia 21-01-2018*/
        public const String CamposTabla = "SELECT ID_SUBDETALLE codigo, CODIGO_ALTERNO||' ('||DESCRIPCION||')' descripcion \n" +
                       "FROM GRTA_COMPENDIO_SUBDETALLE\n" +
                       "WHERE\n" +
                       "ID_DETALLE_GRUPO=? AND \n" +
                       "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)";


        public const String TablaPadre = "SELECT SUJETO.TABLA_DATOS, \n" +
                       "   (SELECT CODIGO_ALTERNO||' ('||NOMBRE||')'\n" +
                       "   FROM GRTA_COMPENDIO_DETALLE\n" +
                       "   WHERE\n" +
                       "   ID_DETALLE=SUJETO.TABLA_DATOS AND\n" +
                       "   SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)) NOMBRE \n" +
                       "   FROM GRTA_TABLAS_SUJETO SUJETO\n" +
                       "   WHERE\n" +
                       "   SUJETO.SUJETO_RIESGO=? AND \n" +
                       "   SUJETO.ORIGEN_FUENTE=? AND\n" +
                       "   SYSDATE BETWEEN SUJETO.FECHA_INICIO_VIGENCIA AND DECODE(SUJETO.FECHA_FIN_VIGENCIA,'',SYSDATE,SUJETO.FECHA_FIN_VIGENCIA) ORDER BY 2";
        /*Usado WMarcia 21-01-2018*/
        public const String ValorParametroCombo = "SELECT ID_PARAMETRO codigo, DESCRIPCION_BREVE descripcion \n" +
                       "FROM GRTA_PARAMETROS\n" +
                       "WHERE\n" +
                       "CLASE_PARAMETRO=? AND\n" +
                       "(SUJETO_RIESGO=? OR ? IS NULL OR SUJETO_RIESGO IS NULL) AND \n" +
                       "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)";


        public const String ValorParametroFuente = "SELECT ID_PARAMETRO,DESCRIPCION_BREVE FROM GRTA_PARAMETROS\n" +
           "WHERE\n" +
           "CLASE_PARAMETRO IN (62,538) AND\n" +
           "(SUJETO_RIESGO IS NULL OR SUJETO_RIESGO=?) AND\n" +
           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)";

        public const String ValorParametroSujeto = "SELECT PARAMETROS.ID_PARAMETRO, PARAMETROS.DESCRIPCION_BREVE \n" +
           "FROM GRTA_TABLAS_SUJETO SUJETO, GRTA_PARAMETROS PARAMETROS\n" +
           "WHERE\n" +
           "SUJETO.TABLA_DATOS=? AND --\n" +
           "SUJETO.ORIGEN_FUENTE=504 AND\n" +
           "SUJETO.TIPO_TABLA=506 AND\n" +
           "PARAMETROS.SUJETO_RIESGO=SUJETO.SUJETO_RIESGO AND\n" +
           "SYSDATE BETWEEN PARAMETROS.FECHA_INICIO_VIGENCIA AND DECODE(PARAMETROS.FECHA_FIN_VIGENCIA,'',SYSDATE,PARAMETROS.FECHA_FIN_VIGENCIA)\n" +
           "UNION \n" +
           "SELECT ID_PARAMETRO,DESCRIPCION_BREVE \n" +
           "FROM GRTA_PARAMETROS\n" +
           "WHERE\n" +
           "SUJETO_RIESGO IS NULL AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)\n" +
           "ORDER BY 1";
        /*            
    public const String ValorParametroIdSujeto ="SELECT ID_PARAMETRO, DESCRIPCION_BREVE \n" + 
       "FROM GRTA_PARAMETROS PARAMETROS, GRTA_IDENTIFICADOR_SUJETO SUJETO\n" + 
       "WHERE\n" + 
       "SUJETO.TABLAS_SUJETO IN (SELECT TABLAS_SUJETO FROM GRTA_TABLAS_SUJETO WHERE TABLA_DATOS=?) AND\n" + 
       "SYSDATE BETWEEN SUJETO.FECHA_INICIO_VIGENCIA AND DECODE(SUJETO.FECHA_FIN_VIGENCIA,'',SYSDATE,SUJETO.FECHA_FIN_VIGENCIA) AND\n" + 
       "PARAMETROS.VALOR_IDENTIFICACION_SUJETO=SUJETO.IDENTIFICACION_SUJETO AND\n" + 
       "PARAMETROS.CLASE_PARAMETRO=? AND\n" + 
       "(PARAMETROS.SUJETO_RIESGO=? OR ? IS NULL) AND \n" + 
       "SYSDATE BETWEEN PARAMETROS.FECHA_INICIO_VIGENCIA AND DECODE(PARAMETROS.FECHA_FIN_VIGENCIA,'',SYSDATE,PARAMETROS.FECHA_FIN_VIGENCIA)"),  
       */
        /*Usado WMarcia 22-01-2018*/
        //Se actualizó el parámetro para que presente los Id sujeto de la tabla seleccionada y también de la tabla principal.
        public const String ValorParametroIdSujeto = "SELECT PARAMETROS.ID_PARAMETRO codigo, PARAMETROS.DESCRIPCION_BREVE descripcion  \n" +
           "FROM GRTA_PARAMETROS PARAMETROS, GRTA_IDENTIFICADOR_SUJETO SUJETO \n" +
           "WHERE\n" +
           "SUJETO.TABLAS_SUJETO IN (SELECT TABLAS_SUJETO FROM GRTA_TABLAS_SUJETO WHERE TABLA_DATOS=?) AND \n" +
           "SYSDATE BETWEEN SUJETO.FECHA_INICIO_VIGENCIA AND NVL(SUJETO.FECHA_FIN_VIGENCIA,SYSDATE) AND \n" +
           "PARAMETROS.VALOR_IDENTIFICACION_SUJETO=SUJETO.IDENTIFICACION_SUJETO AND\n" +
           "PARAMETROS.CLASE_PARAMETRO=? AND\n" +
           "(PARAMETROS.SUJETO_RIESGO=? OR ? IS NULL) AND \n" +
           "SYSDATE BETWEEN PARAMETROS.FECHA_INICIO_VIGENCIA AND NVL(PARAMETROS.FECHA_FIN_VIGENCIA,SYSDATE)\n" +
           "UNION  \n" +
           "SELECT PARAMETROS.ID_PARAMETRO, PARAMETROS.DESCRIPCION_BREVE  \n" +
           "FROM GRTA_PARAMETROS PARAMETROS, GRTA_TABLAS_SUJETO SUJETO, GRTA_IDENTIFICADOR_SUJETO IDENTIFICADOR \n" +
           "WHERE\n" +
           "SUJETO.SUJETO_RIESGO=? AND\n" +
           "SUJETO.ORIGEN_FUENTE=DECODE(?,'T',504,504) AND --TRANSACCIONAL, CONSOLIDACION\n" +
           "SUJETO.TIPO_TABLA IN (506,507) AND --GENERAL\n" +
           "IDENTIFICADOR.TABLAS_SUJETO=SUJETO.TABLAS_SUJETO AND\n" +
           "SYSDATE BETWEEN IDENTIFICADOR.FECHA_INICIO_VIGENCIA AND NVL(IDENTIFICADOR.FECHA_FIN_VIGENCIA,SYSDATE) AND\n" +
           "PARAMETROS.VALOR_IDENTIFICACION_SUJETO=IDENTIFICADOR.IDENTIFICACION_SUJETO AND\n" +
           "PARAMETROS.CLASE_PARAMETRO=? AND\n" +
           "SYSDATE BETWEEN PARAMETROS.FECHA_INICIO_VIGENCIA AND NVL(PARAMETROS.FECHA_FIN_VIGENCIA,SYSDATE)\n" +
           "ORDER BY 1";


        public const String ConsultarVariablesCoeficientesMedidaTabla = "SELECT NVL(TRUNC(ENTIDADES.VALOR_COEFICIENTE,4),0) \"Coeficiente\",  \n" +
           "ENTIDADES.CODIGO_VARIABLE||'-'||GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_CODIGO_VARIABLE(ENTIDADES.CODIGO_VARIABLE) \"Descripcion\",  \n" +
           "(CASE WHEN VARIABLES.TABLA_CONSOLIDACION IS NOT NULL THEN GRPK_OPERACIONES_COMUNES.GRFN_ALTERNO_DETALLE(VARIABLES.TABLA_CONSOLIDACION) ELSE ' ' END) \"Tabla\",VARIABLES.EXPRESION_CONSOLIDACION \"Fórmula o Expresión\"\n" +
           "FROM GRTA_ENTIDADES_MEDIDAS ENTIDADES, GRTA_VARIABLES VARIABLES\n" +
           "WHERE   \n" +
           "ENTIDADES.ID_MEDIDA =:pIdMedida AND   \n" +
           "ENTIDADES.VERSION_MEDIDA = :pVersionMedida AND  \n" +
           "ENTIDADES.CODIGO_VARIABLE IS NOT NULL AND\n" +
           "VARIABLES.CODIGO_VARIABLE=ENTIDADES.CODIGO_VARIABLE AND VARIABLES.TIPO_VARIABLE=57  \n" +
           "ORDER BY abs(ENTIDADES.VALOR_COEFICIENTE)";


        public const String ConsultarMatrizCorrelacionMedidaTabla = "SELECT NVL(CORRELACION.NOMBRE,' ') \"Predictoras (Xi)\", NVL(VAR1,' ') \"X (1)\", NVL(VAR2,' ') \"X (2)\",  \n" +
            "    NVL(VAR3,' ') \"X (3)\", NVL(VAR4,' ') \"X (4)\",NVL(VAR5,' ') \"X (5)\",NVL(VAR6,' ') \"X (6)\",  \n" +
            "    NVL(VAR7,' ') \"X (7)\", NVL(VAR8,' ') \"X (8)\",NVL(VAR9,' ') \"X (9)\",NVL(VAR10,' ') \"X (10)\", \n" +
            "    NVL(VAR11,' ') \"X (11)\", NVL(VAR12,' ') \"X (12)\",NVL(VAR13,' ') \"X (13)\", NVL(VAR14,' ') \"X (14)\", \n" +
            "    NVL(VAR15,' ') \"X (15)\", NVL(VAR16,' ') \"X (16)\",NVL(VAR17,' ') \"X (17)\", NVL(VAR18,' ') \"X (18)\",     \n" +
            "    NVL(VAR19,' ') \"X (19)\", NVL(VAR20,' ') \"X (20)\",NVL(VAR21,' ') \"X (21)\", NVL(VAR22,' ') \"X (22)\",     \n" +
            "    NVL(VAR23,' ') \"X (23)\", NVL(VAR24,' ') \"X (24)\",NVL(VAR25,' ') \"X (25)\", NVL(VAR26,' ') \"...\"     \n" +
            "    FROM GRTA_ANALISIS_EST ANALISIS, GRTA_MAPA_CORRELACIONES_EST CORRELACION  \n" +
            "    WHERE \n" +
            "    ANALISIS.ID_MEDIDA=:pIdMedida AND\n" +
            "    ANALISIS.VERSION_MEDIDA=:pVersionMedida AND\n" +
            "    ANALISIS.RESULTADO_ANALISIS=GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO AND\n" +
            "    CORRELACION.ID_ANALISIS=ANALISIS.ID_ANALISIS AND\n" +
            "    CORRELACION.TIPO_MAPA=48 \n" +
            "    order by CORRELACION.mapa_correlacion";

        public const String ConsultarMatrizEstadisticoMedidaTabla = "SELECT NVL(CORRELACION.NOMBRE,' ') \"Predictoras (Xi)\", NVL(VAR1,' ') \"X (1)\", NVL(VAR2,' ') \"X (2)\",  \n" +
           "    NVL(VAR3,' ') \"X (3)\", NVL(VAR4,' ') \"X (4)\",NVL(VAR5,' ') \"X (5)\",NVL(VAR6,' ') \"X (6)\",  \n" +
           "    NVL(VAR7,' ') \"X (7)\", NVL(VAR8,' ') \"X (8)\",NVL(VAR9,' ') \"X (9)\",NVL(VAR10,' ') \"X (10)\", \n" +
           "    NVL(VAR11,' ') \"X (11)\", NVL(VAR12,' ') \"X (12)\",NVL(VAR13,' ') \"X (13)\", NVL(VAR14,' ') \"X (14)\", \n" +
           "    NVL(VAR15,' ') \"X (15)\", NVL(VAR16,' ') \"X (16)\",NVL(VAR17,' ') \"X (17)\", NVL(VAR18,' ') \"X (18)\",     \n" +
           "    NVL(VAR19,' ') \"X (19)\", NVL(VAR20,' ') \"X (20)\",NVL(VAR21,' ') \"X (21)\", NVL(VAR22,' ') \"X (22)\",     \n" +
           "    NVL(VAR23,' ') \"X (23)\", NVL(VAR24,' ') \"X (24)\",NVL(VAR25,' ') \"X (25)\", NVL(VAR26,' ') \"...\"     \n" +
           "    FROM GRTA_ANALISIS_EST ANALISIS, GRTA_MAPA_CORRELACIONES_EST CORRELACION  \n" +
           "    WHERE \n" +
           "    ANALISIS.ID_MEDIDA=:pIdMedida AND\n" +
           "    ANALISIS.VERSION_MEDIDA=:pVersionMedida AND\n" +
           "    ANALISIS.RESULTADO_ANALISIS=GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO AND\n" +
           "    CORRELACION.ID_ANALISIS=ANALISIS.ID_ANALISIS AND\n" +
           "    CORRELACION.TIPO_MAPA=49 \n" +
           "    order by CORRELACION.mapa_correlacion";

        public const String ConsultarListaRiesgoTabla = "SELECT * FROM ( \n" +
           "      SELECT ROWNUM \"No\", x.VALOR_VARIABLE \"Codigo\", NVL(x.Descripcion,'SIN DESCRIPCION') \"Descripcion\",x.REGISTROS_ANALIZADOS \"#Registros\", \n" +
           "      x.REGISTROS_HALLAZGO \"#Hallazgos\", x.INDICE_NORMALIZADO \"Indice\", x.grado \"Grado\"    \n" +
           "      FROM (  \n" +
           "            SELECT DOMINIO.VALOR_VARIABLE, \n" +
           "          GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('D',VARIABLES.VARIABLE_CATALOGO,VARIABLES.TABLA_CODIFICACION,UPPER(DOMINIO.VALOR_VARIABLE)) Descripcion,  \n" +
           "           DOMINIO.REGISTROS_ANALIZADOS, DOMINIO.REGISTROS_HALLAZGO,DOMINIO.INDICE_NORMALIZADO,GRADO.NOMBRE GRADO \n" +
           "           FROM GRTA_ENTIDADES_MEDIDAS MEDIDAS, GRTA_DOMINIO_VARIABLE DOMINIO, GRTA_VARIABLES VARIABLES,  \n" +
           "           GRTA_COMPENDIO_DETALLE GRADO  \n" +
           "           WHERE MEDIDAS.ID_MEDIDA = :pIdMedida AND  \n" +
           "           MEDIDAS.VERSION_MEDIDA = :pVersionMedida AND  \n" +
           "           MEDIDAS.CODIGO_VARIABLE = :pCodigoVariable AND  \n" +
           "           DOMINIO.ENTIDADES_MEDIDAS = MEDIDAS.ENTIDADES_MEDIDAS AND  \n" +
           "           (DOMINIO.GRADO_RIESGO=:pGradoRiesgo OR :pGradoRiesgo IS NULL) AND       \n" +
           "           DOMINIO.ENTIDADES_MEDIDAS = MEDIDAS.ENTIDADES_MEDIDAS AND       \n" +
           "           (DOMINIO.VALOR_VARIABLE = :pCodigoElemento OR :pCodigoElemento IS NULL) AND  \n" +
           "           VARIABLES.CODIGO_VARIABLE=MEDIDAS.CODIGO_VARIABLE AND  \n" +
           "           GRADO.ID_DETALLE=DOMINIO.GRADO_RIESGO \n" +
           "           ORDER BY DOMINIO.INDICE_NORMALIZADO DESC, GRADO.REFERENCIA1 DESC \n" +
           "      ) x WHERE (x.Descripcion LIKE '%' || :pDescripcionElemento || '%' OR :pDescripcionElemento IS NULL) \n" +
           "      ) WHERE ROWNUM<=250";


        public const String ConsultarListaRiesgoAll = "SELECT * FROM (\n" +
           "SELECT ROWNUM \"No\", x.VALOR_VARIABLE \"Codigo\", NVL(x.Descripcion,'SIN DESCRIPCION') \"Descripcion\",\n" +
           "x.REGISTROS_ANALIZADOS \"#Registros\",x.REGISTROS_HALLAZGO \"#Hallazgos\", \n" +
           "x.INDICE_NORMALIZADO \"Indice\", x.grado \"Grado\"\n" +
           "FROM\n" +
           "(     \n" +
           "SELECT DOMINIO.VALOR_VARIABLE,\n" +
           "CONTRI.CMP_NAM descripcion,  \n" +
           "DOMINIO.REGISTROS_ANALIZADOS, DOMINIO.REGISTROS_HALLAZGO,DOMINIO.INDICE_NORMALIZADO,GRADO.NOMBRE GRADO \n" +
           "FROM GRTA_ENTIDADES_MEDIDAS MEDIDAS, GRTA_DOMINIO_VARIABLE DOMINIO, \n" +
           "GRTA_VARIABLES VARIABLES, GRTA_VARIABLES CATALOGO, GRTA_COMPENDIO_DETALLE GRADO, UNCMPTAB CONTRI\n" +
           "WHERE MEDIDAS.ID_MEDIDA = :pIdMedida AND\n" +
           "MEDIDAS.VERSION_MEDIDA = :pVersionMedida AND \n" +
           "MEDIDAS.CODIGO_VARIABLE = :pCodigoVariable AND \n" +
           "DOMINIO.ENTIDADES_MEDIDAS = MEDIDAS.ENTIDADES_MEDIDAS AND\n" +
           "VARIABLES.CODIGO_VARIABLE=MEDIDAS.CODIGO_VARIABLE AND \n" +
           "CATALOGO.CODIGO_VARIABLE=VARIABLES.VARIABLE_CATALOGO AND \n" +
           "GRADO.ID_DETALLE=DOMINIO.GRADO_RIESGO AND\n" +
           "CONTRI.CMP_COD=DOMINIO.VALOR_VARIABLE AND\n" +
           "CONTRI.LST_OPE='U'\n" +
           "ORDER BY DOMINIO.INDICE_NORMALIZADO DESC, GRADO.REFERENCIA1 DESC\n" +
           ") X\n" +
           ")";


        public const String DestinatariosMedidaModTabla = "SELECT MM.ID_PERSONA \"Código\"," +
                                       " (SELECT t.DESCRIPCION||' ('||t.NOMBRE||')' descripcion FROM GRTA_COMPENDIO_DETALLE t WHERE T.ID_DETALLE = Mm.ID_PERSONA AND t.ID_COMPENDIO=63 ) \"Destinarario\"," +
                                       " MM.ID_PERSONA \"Op.\"" +
                                       " FROM GRTA_MENSAJE_MEDIDAS mm, GRTA_MEDIDAS m" +
                                       " WHERE MM.ID_MEDIDA = M.ID_MEDIDA AND MM.VERSION_MEDIDA = M.VERSION_MEDIDA" +
                                       " AND M.VERSION_MEDIDA = :pVersionMedida AND M.ID_MEDIDA = :pIdMedida" +
                                       " ORDER BY Mm.MENSAJE_MEDIDAS ASC";
        /*Usado en Nueva versión/ Boton detalle*/
        public const String ConsultarCondicionesAccionesMedidaTmp = "SELECT x.CONDICION_MEDIDAS, x.ORDEN_CONDICION,  filtro_condicion,\n" +
                                                    " MAX(ACCION_SI) TIPO_ACCION_SI,\n" +
                                                    " GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAX(ACCION_SI)) ACCION_SI,\n" +
                                                    " MAX(ACCION_NO)  TIPO_ACCION_NO,  \n" +
                                                    " GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAX(ACCION_NO)) ACCION_NO,\n" +
                                                    " MAX(RESPUESTA_SI) TIPO_RESPUESTA_SI,   \n" +
                                                    " GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAX(RESPUESTA_SI)) RESPUESTA_SI,\n" +
                                                    " MAX(RESPUESTA_NO) TIPO_RESPUESTA_NO,  \n" +
                                                    " GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAX(RESPUESTA_NO)) RESPUESTA_NO\n" +
                                                    " FROM (\n" +
                                                    " SELECT C.CONDICION_MEDIDAS, C.ORDEN_CONDICION,\n" +
                                                    " GRPK_OPERACIONES_COMUNES.GRFN_CONSULTAR_EXPRESIO_FILTRO( C.ID_SESSION, C.CONSECUTIVO_SESSION, C.CONDICION_MEDIDAS, 'T') filtro_condicion,\n" +
                                                    " DECODE(COMP.TIPO_SALIDA, 88, NVL(COMP.TIPO_ACCION, 0), 0) ACCION_SI ,\n" +
                                                    " DECODE(COMP.TIPO_SALIDA, 89, NVL(COMP.TIPO_ACCION, 0), 0) ACCION_NO,\n" +
                                                    " DECODE(COMP.TIPO_SALIDA, 88, NVL(COMP.TIPO_RESPUESTA, 0), 0) RESPUESTA_SI ,\n" +
                                                    " DECODE(COMP.TIPO_SALIDA, 89, NVL(COMP.TIPO_RESPUESTA, 0), 0) RESPUESTA_NO\n" +
                                                    " FROM GRTA_TMP_CONDICIONES_MEDIDA  c,  GRTA_TMP_COMPONENTES_MED comp\n" +
                                                    " WHERE C.CONDICION_MEDIDAS = COMP.CONDICION_MEDIDAS  \n" +
                                                    " AND C.CONSECUTIVO_SESSION  =  :pComplementoIdent\n" +
                                                    " AND C.ID_SESSION = :pIdentificador\n" +
                                                    " GROUP BY C.CONDICION_MEDIDAS, C.ORDEN_CONDICION,  C.ID_SESSION, C.CONSECUTIVO_SESSION,  COMP.TIPO_SALIDA,COMP.TIPO_ACCION,COMP.TIPO_RESPUESTA     \n" +
                                                    " ) x  \n" +
                                                    " GROUP BY x.CONDICION_MEDIDAS, x.ORDEN_CONDICION, x.filtro_condicion  \n" +
                                                    " ORDER BY x.ORDEN_CONDICION";


        public const String ConsultarValoresTipoRespuestaMedTmp = "SELECT ROWNUM No , x.* FROM (\n" +
                                               " SELECT comp_med.ID_COMPONENTES,\n" +
                                               " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN comp_med.CANAL_SELECTIVIDAD WHEN 28 THEN comp_med.CODIGO_VALIDACION WHEN 29 THEN comp_med.VALOR_RESPUESTA END ) codigo_tipo,\n" +
                                               " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN  GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(comp_med.CANAL_SELECTIVIDAD)\n" +
                                               " WHEN 28 THEN  ' ' WHEN 29 THEN GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(comp_med.VALOR_RESPUESTA) END ) valor_tipo,\n" +
                                               " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN TO_CHAR(comp_med.VALOR) WHEN 28 THEN\n" +
                                               " GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO (COMP_MED.CODIGO_VALIDACION ) ELSE NVL(TO_CHAR(COMP_MED.VALOR), GRPK_OPERACIONES_COMUNES.GRFN_CONS_EXPRESION_VALCOMPUES(comp_med.ID_COMPONENTES, COND.CONDICION_MEDIDAS, comp_med.TIPO_SALIDA, 'T') ) END ) valor,\n" +
                                               " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 29 THEN TO_CHAR(COMP_MED.CANTIDAD) ELSE ' ' END) cantidad," +
                                               " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 29 THEN (SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=18 AND ID_DETALLE = COMP_MED.PERIODO) ELSE ' ' END) periodo" +
                                               " FROM GRTA_TMP_CONDICIONES_MEDIDA cond, GRTA_TMP_COMPONENTES_MED comp_med\n" +
                                               " WHERE COND.CONDICION_MEDIDAS = COMP_MED.CONDICION_MEDIDAS  \n" +
                                               " AND COMP_MED.TIPO_SALIDA = :pTipoSalida  \n" +
                                               " AND COND.CONDICION_MEDIDAS = :pCondicionMedida \n" +
                                               " AND COND.CONSECUTIVO_SESSION  =  :pComplementoIdent\n" +
                                               " AND COND.ID_SESSION = :pIdentificador\n" +
                                               " ORDER BY COND.ORDEN_CONDICION, comp_med.ID_COMPONENTES  \n" +
                                               " ) x";


        public const String ConsultarComponente = "SELECT ROWNUM No , x.* FROM (\n" +
                   " SELECT comp_med.ID_COMPONENTES,\n" +
                   " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN comp_med.CANAL_SELECTIVIDAD WHEN 28 THEN comp_med.CODIGO_VALIDACION WHEN 29 THEN comp_med.VALOR_RESPUESTA END ) codigo_tipo,\n" +
                   " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN  GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(comp_med.CANAL_SELECTIVIDAD)\n" +
                   " WHEN 28 THEN  ' ' WHEN 29 THEN GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(comp_med.VALOR_RESPUESTA) END ) valor_tipo,\n" +
                   " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 27 THEN TO_CHAR(comp_med.VALOR) WHEN 28 THEN\n" +
                   " GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO (COMP_MED.CODIGO_VALIDACION ) ELSE NVL(TO_CHAR(COMP_MED.VALOR), GRPK_OPERACIONES_COMUNES.GRFN_CONS_EXPRESION_VALCOMPUES(comp_med.ID_COMPONENTES, COND.CONDICION_MEDIDAS, comp_med.TIPO_SALIDA, 'T') ) END ) valor,\n" +
                   " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 29 THEN TO_CHAR(COMP_MED.CANTIDAD) ELSE ' ' END) cantidad," +
                   " ( CASE COMP_MED.TIPO_RESPUESTA WHEN 29 THEN (SELECT DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=18 AND ID_DETALLE = COMP_MED.PERIODO) ELSE ' ' END) periodo" +
                   " FROM GRTA_TMP_CONDICIONES_MEDIDA cond, GRTA_TMP_COMPONENTES_MED comp_med\n" +
                   " WHERE COND.CONDICION_MEDIDAS = COMP_MED.CONDICION_MEDIDAS  \n" +
                   " AND COMP_MED.TIPO_SALIDA = :pTipoSalida  \n" +
                   " AND COND.CONDICION_MEDIDAS = :pCondicionMedida \n" +
                   " AND COND.CONSECUTIVO_SESSION  =  :pComplementoIdent\n" +
                   " AND COND.ID_SESSION = :pIdentificador\n" +
                   " ORDER BY COND.ORDEN_CONDICION, comp_med.ID_COMPONENTES  \n" +
                   " ) x";

        public const String ExpresionLogicaTipoValCompuesto = "SELECT DSC_USUARIO FROM GRTA_TMP_DET_COMPONENTES WHERE DETALLE_COMPONENTES = (\n" +
                                               " SELECT MAX(DETALLE_COMPONENTES) FROM GRTA_TMP_DET_COMPONENTES dt, GRTA_TMP_COMPONENTES_MED comp, GRTA_TMP_CONDICIONES_MEDIDA cond\n" +
                                               " WHERE Comp.ID_COMPONENTES = DT.ID_COMPONENTES AND COND.CONDICION_MEDIDAS = COMP.CONDICION_MEDIDAS\n" +
                                               " AND COND.ID_SESSION = :pIdSession AND COND.CONSECUTIVO_SESSION = :pConsecutivoSession AND COND.LINEA_CONDICION = :pLineaCondicion\n" +
                                               " AND COMP.TIPO_SALIDA = :pTipoSalida)";


        public const String OrigenFuenteCombo = "SELECT ID_DETALLE, NOMBRE \n" +
                           "   FROM GRTA_COMPENDIO_DETALLE\n" +
                           "   WHERE\n" +
                           "   ID_COMPENDIO=45 AND\n" +
                           "   SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)";


        public const String TipoTablaCombo = "SELECT ID_DETALLE, NOMBRE \n" +
                           "   FROM GRTA_COMPENDIO_DETALLE\n" +
                           "   WHERE\n" +
                           "   ID_COMPENDIO=46 AND\n" +
                           "   SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA)";


        public const String ExitenciaTabla = "   SELECT COUNT(0) FROM GRTA_TABLAS_SUJETO\n" +
                           "     WHERE\n" +
                           "     SUJETO_RIESGO=? AND\n" +
                           "     ORIGEN_FUENTE=? AND\n" +
                           "     TIPO_TABLA=? AND\n" +
                           "     TABLA_DATOS=? ";
        //@Autor: WMarcia; @Version: 1.0, 19/06/2012; @Descripcion: Recupera el código y la descripción del tipo de actuacion
        public const String TipoActuacionCombo = "SELECT ID_DETALLE CODIGO, NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=70 AND " +
                                 "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";
        //@Autor: WMarcia; @Version: 1.0, 25/06/2012; @Descripcion: Recupera el código y la descripción del estado inicial del programa de fiscalizacion
        // MgrEstadoPrograma="SELECT ID_DETALLE CODIGO, NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=69 AND ID_DETALLE=98 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)"),
        //Se parametrizo, usado  para consultar Programa Fiscalizacion luego de grabar
        //MgrEstadoPrograma="SELECT ID_DETALLE , NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=:pID_COMPENDIO AND ID_DETALLE=:pID_DETALLE AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)"),
        //@Autor: WMarcia; @Version: 1.0, 25/06/2012; @Descripcion: Recupera el código y la descripción de los meses calendarios
        //MgrMesCalendario="SELECT REFERENCIA1 CODIGO_FISICO, ID_DETALLE CODIGO_LOGICO, NOMBRE FROM GRTA_COMPENDIO_DETALLE\n" +
        //
        //usado 13/01/2017
        //Ya no va xq se esta parametrizando
        /* MgrMesCalendario="SELECT REFERENCIA1 codigoCadena, ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE\n" +
                          "WHERE ID_COMPENDIO=71 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)\n" + 
                          "ORDER BY TO_NUMBER(REFERENCIA1)"),*/
        //@Autor: WMarcia; @Version: 1.0, 28/06/2012; @Descripcion: Recupera el registro del Programa de Fiscalizacion
        //usado en Programa Fiscalizacion /Consultar después de grabar
        public const String ProgramaFiscalizacionMaestro = "SELECT GRTA_PROGRAM.ID_PROGRAMA id, GRTA_PROGRAM.CODIGO_PROGRAMA codigo, GRTA_PROGRAM.NOMBRE nombre, GRTA_PROGRAM.ANNO_PROGRAMA anio, GRTA_PROGRAM.CASOS_REQUERIDOS casos, GRTA_PROGRAM.DESCRIPCION descripcion,\n" +
                                           "TIPO_ACTUACION cod_actuacion, GRTA_TIPO_ACTUACION.DESCRIPCION tipo_actuacion, GRTA_PROGRAM.ESTADO_PROGRAMA cod_est_programa, GRTA_ESTADO.DESCRIPCION estado_programa,\n" +
                                           "GRTA_PROGRAM.SUJETO_RIESGO cod_suj_riesgo, GRTA_SUJ_RIESGO.DESCRIPCION_BREVE suj_riesgo\n" +
                                           "FROM GRTA_COMPENDIO_DETALLE GRTA_TIPO_ACTUACION, GRTA_COMPENDIO_DETALLE GRTA_ESTADO, GRTA_PROGRAMA GRTA_PROGRAM, GRTA_SUJETO_RIESGO GRTA_SUJ_RIESGO\n" +
                                           "WHERE GRTA_TIPO_ACTUACION.ID_DETALLE = GRTA_PROGRAM.TIPO_ACTUACION AND GRTA_ESTADO.ID_DETALLE = GRTA_PROGRAM.ESTADO_PROGRAMA AND GRTA_PROGRAM.SUJETO_RIESGO = GRTA_SUJ_RIESGO.SUJETO_RIESGO\n" +
                                           "AND GRTA_PROGRAM.ID_PROGRAMA = ?";
        //@Autor: WMarcia; @Version: 1.0, 28/06/2012; @Descripcion: Recupera el registro del Programa Mensual
        //usado en Programa Fiscalización/ Consultar después de grabar 
        public const String ProgramaFiscalizacionDetalle = "SELECT GRTA_PROG_MENSUAL.PROGRAMA_MENSUAL programa_mensual, GRTA_PROG_MENSUAL.ID_PROGRAMA id, GRTA_PROG_MENSUAL.MES_CALENDARIO cod_mes_calendario, GRTA_MES.DESCRIPCION mes, \n" +
           "GRTA_PROG_MENSUAL.CASOS_REQUERIDOS casos, GRTA_PROG_MENSUAL.ESTADO_MENSUAL cod_estado, GRTA_ESTADO.NOMBRE estado_mensual, GRTA_PROG_MENSUAL.SESSION_REGISTRO sesion,\n" +
           "(SELECT '('||TO_CHAR(COUNT(0))||')' FROM GRTA_SELECCION_CASOS WHERE PROGRAMA_MENSUAL= GRTA_PROG_MENSUAL.PROGRAMA_MENSUAL) cantidad\n" +
           "FROM GRTA_COMPENDIO_DETALLE GRTA_MES, GRTA_COMPENDIO_DETALLE GRTA_ESTADO, GRTA_PROGRAMA_MENSUAL GRTA_PROG_MENSUAL\n" +
           "WHERE GRTA_MES.ID_DETALLE = GRTA_PROG_MENSUAL.MES_CALENDARIO AND GRTA_ESTADO.ID_DETALLE = GRTA_PROG_MENSUAL.ESTADO_MENSUAL AND \n" +
           "GRTA_PROG_MENSUAL.ID_PROGRAMA = :pIdPrograma AND GRTA_PROG_MENSUAL.MES_CALENDARIO =:pMes";
        //@Autor: WMarcia; @Version: 1.0, 02/07/2012; @Descripcion: Recupera los Programas de Fiscalizacion para posiblemente ser consultados
        public const String ProgramaFiscalizacionConsTabla = "SELECT * FROM (SELECT TBL_PROGRAMA.ID_PROGRAMA \"Prog.\", TBL_PROGRAMA.DESCRIPCION_BREVE \"Sujeto\",TO_CHAR(TBL_PROGRAMA.FECHA_REGISTRO,'DD/MM/YYYY') \"F.Registro\", TBL_PROGRAMA.ANNO_PROGRAMA \"Año\", NVL(TBL_PROGRAMA.CODIGO_PROGRAMA,' ') \"Código\",\n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(TBL_PROGRAMA.TIPO_ACTUACION) \"T.Actuación\" ,TBL_PROGRAMA.NOMBRE \"Nombre\",  TBL_PROGRAMA.CASOS_REQUERIDOS \"#Casos\",'' \"Operación\" \n" +
           "FROM (\n" +
           "SELECT PROGRAMA.ID_PROGRAMA, SUJETO_RIESGO.DESCRIPCION_BREVE, PROGRAMA.CODIGO_PROGRAMA, PROGRAMA.CASOS_REQUERIDOS,PROGRAMA.TIPO_ACTUACION,PROGRAMA.ANNO_PROGRAMA,\n" +
           "PROGRAMA.NOMBRE, GRPK_OPERACIONES_PROGRAMA.GRFN_CONSULTAR_EXPRESIO_FILTRO(PROGRAMA.ID_PROGRAMA) EXPRESION_FILTRO, PROGRAMA.SUJETO_RIESGO, \n" +
           "PROGRAMA.DESCRIPCION, PROGRAMA.FECHA_REGISTRO \n" +
           "FROM GRTA_PROGRAMA PROGRAMA, GRTA_SUJETO_RIESGO SUJETO_RIESGO \n" +
           "WHERE PROGRAMA.SUJETO_RIESGO = SUJETO_RIESGO.SUJETO_RIESGO AND PROGRAMA.SUJETO_RIESGO = NVL(:pSujetoRiesgo, PROGRAMA.SUJETO_RIESGO)\n" +
           ") TBL_PROGRAMA\n" +
           " WHERE TBL_PROGRAMA.ID_PROGRAMA =  NVL(:pIdPrograma, TBL_PROGRAMA.ID_PROGRAMA) \n" +
           "AND (TBL_PROGRAMA.CODIGO_PROGRAMA = UPPER(:pCodigoPrograma) OR :pCodigoPrograma IS NULL) AND TBL_PROGRAMA.DESCRIPCION LIKE '%' || NVL(UPPER(:pDescripcionPrograma), TBL_PROGRAMA.DESCRIPCION) || '%' \n" +
           "AND (UPPER(TBL_PROGRAMA.EXPRESION_FILTRO) LIKE '%' ||UPPER(:pVariableFiltro)|| '%' OR :pVariableFiltro IS NULL)\n" +
           "AND (UPPER(TBL_PROGRAMA.EXPRESION_FILTRO) LIKE '%' ||UPPER(:pValorVariableFiltro)|| '%' OR :pValorVariableFiltro IS NULL) \n" +
           "AND TRUNC(TBL_PROGRAMA.FECHA_REGISTRO) BETWEEN NVL(TO_DATE(:pFechaInicio, 'DD/MM/YYYY'), TRUNC(TBL_PROGRAMA.FECHA_REGISTRO)) AND NVL(TO_DATE(:pFechaFin, 'DD/MM/YYYY'), TRUNC(SYSDATE)) \n" +
           "ORDER BY 1 DESC)";
        //@Autor: WMarcia; @Version: 1.0, 02/07/2012; @Descripcion: Recupera los Programas de Fiscalizacion para posiblemente ser modificados
        //usado muestra datos de la tabla del programa d fiscalizacion buscado en el modulo MODIFICAR
        //Función Convertida: GRFN_NOMBRE_ELEMENTO
        //TODO, Prueba no completada por falta de datos 
        public const String ProgramaFiscalizacionModTabla = "SELECT * FROM (SELECT TBL_PROGRAMA.ID_PROGRAMA ID, TBL_PROGRAMA.DESCRIPCION_BREVE DESCRIPCION,TBL_PROGRAMA.ANNO_PROGRAMA ANIO, NVL(TBL_PROGRAMA.CODIGO_PROGRAMA,' ') CODIGO, " +
                   "CASE  WHEN TBL_PROGRAMA.TIPO_ACTUACION IS NULL THEN 'Indefinido' ELSE com_det.nombre END TIPO_ACTUACION ,TBL_PROGRAMA.NOMBRE NOMBRE,  TBL_PROGRAMA.CASOS_REQUERIDOS CASOS_REQ, NVL(TBL_PROGRAMA.CASOS_PROPUESTOS, 0) CASOS_PROP, NVL(TBL_PROGRAMA.CASOS_SELECCIONADOS, 0) CASOS_SELEC, NVL(TBL_PROGRAMA.CASOS_TRANSFERIDOS, 0) CASOS_TRANS " +
                   "FROM ( " +
                   "SELECT PROGRAMA.ID_PROGRAMA, SUJETO_RIESGO.DESCRIPCION_BREVE, PROGRAMA.CODIGO_PROGRAMA, PROGRAMA.CASOS_REQUERIDOS, PROGRAMA.CASOS_PROPUESTOS, PROGRAMA.CASOS_SELECCIONADOS, PROGRAMA.CASOS_TRANSFERIDOS, PROGRAMA.TIPO_ACTUACION,PROGRAMA.ANNO_PROGRAMA, " +
                   "PROGRAMA.NOMBRE, GRPK_OPERACIONES_PROGRAMA.GRFN_CONSULTAR_EXPRESIO_FILTRO(PROGRAMA.ID_PROGRAMA) EXPRESION_FILTRO, PROGRAMA.SUJETO_RIESGO," +
                   "PROGRAMA.DESCRIPCION, PROGRAMA.FECHA_REGISTRO " +
                   "FROM GRTA_PROGRAMA PROGRAMA, GRTA_SUJETO_RIESGO SUJETO_RIESGO " +
                   "WHERE PROGRAMA.SUJETO_RIESGO = SUJETO_RIESGO.SUJETO_RIESGO AND PROGRAMA.ESTADO_PROGRAMA IN(98, 99)) TBL_PROGRAMA " +
                   "LEFT JOIN GRTA_COMPENDIO_DETALLE com_det ON com_det.ID_DETALLE=TBL_PROGRAMA.TIPO_ACTUACION " +
                   "WHERE TBL_PROGRAMA.ID_PROGRAMA =  NVL(:pIdPrograma, TBL_PROGRAMA.ID_PROGRAMA) AND TBL_PROGRAMA.SUJETO_RIESGO = NVL(:pSujetoRiesgo, TBL_PROGRAMA.SUJETO_RIESGO) " +
                   "AND (TBL_PROGRAMA.CODIGO_PROGRAMA = UPPER(:pCodigoPrograma) OR :pCodigoPrograma IS NULL) AND TBL_PROGRAMA.DESCRIPCION LIKE '%' || NVL(UPPER(:pDescripcionPrograma), TBL_PROGRAMA.DESCRIPCION) || '%' " +
                   "AND (UPPER(TBL_PROGRAMA.EXPRESION_FILTRO) LIKE '%' ||UPPER(:pVariableFiltro)|| '%' OR :pVariableFiltro IS NULL) " +
                   "AND (UPPER(TBL_PROGRAMA.EXPRESION_FILTRO) LIKE '%' ||UPPER(:pValorVariableFiltro)|| '%' OR :pValorVariableFiltro IS NULL) " +
                   "AND TRUNC(TBL_PROGRAMA.FECHA_REGISTRO) BETWEEN NVL(TO_DATE(:pFechaInicio, 'DD/MM/YYYY'), TRUNC(TBL_PROGRAMA.FECHA_REGISTRO)) AND NVL(TO_DATE(:pFechaFin, 'DD/MM/YYYY'), TRUNC(SYSDATE)) " +
                   "ORDER BY 1 DESC)";
        //@Autor: WMarcia; @Version: 1.0, 03/07/2012; @Descripcion: Recupera las medidas registradas vigentes
        public const String RelacionMedidasTabla = "SELECT '' \"Opc\", MEDIDAS.ID_MEDIDA \"Medida\",MEDIDAS.VERSION_MEDIDA \"Versión\", \n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MEDIDAS.TIPO_MEDIDA) \"Tipo Medida\", \n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MEDIDAS.JERARQUIA_MEDIDA) \"Jerarquía\", \n" +
           "MEDIDAS.NOMBRE_MEDIDA \"Nombre Medida\" \n" +
           "FROM GRTA_MEDIDAS MEDIDAS, GRTA_PROGRAMA_MENSUAL MENSUAL, GRTA_PROGRAMA PROGRAMA\n" +
           "WHERE \n" +
           "MENSUAL.PROGRAMA_MENSUAL=:pProgramaMensual AND\n" +
           "PROGRAMA.ID_PROGRAMA=MENSUAL.ID_PROGRAMA AND\n" +
           "MEDIDAS.SUJETO_RIESGO=PROGRAMA.SUJETO_RIESGO AND\n" +
           "MEDIDAS.ESTADO_MEDIDA IN (39,40,42) AND\n" +
           "SYSDATE BETWEEN NVL(MEDIDAS.FECHA_INICIO_VIGENCIA,SYSDATE) AND NVL(MEDIDAS.FECHA_FIN_VIGENCIA, SYSDATE)";

        //@Autor: WMarcia; @Version: 1.0, 04/07/2012; @Descripcion: Recupera el registro del proceso de seleccion de casos
        public const String SeleccionCasosMaestro = "SELECT CASOS.SELECCION_CASOS, CASOS.PROGRAMA_MENSUAL, TO_CHAR(CASOS.FECHA_INICIO_ANALISIS, 'DD/MM/YYYY') FECHA_INICIO_ANALISIS, TO_CHAR(CASOS.FECHA_FIN_ANALISIS, 'DD/MM/YYYY') FECHA_FIN_ANALISIS, CASOS.DESCRIPCION," +
                                   "CASOS.ESTADO_SELECCION COD_ESTADO_SELECCION, GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(CASOS.ESTADO_SELECCION) ESTADO_SELECCION\n" +
                                    "FROM GRTA_SELECCION_CASOS CASOS WHERE CASOS.SELECCION_CASOS = TO_NUMBER(?)";
        //@Autor: WMarcia; @Version: 1.0, 04/07/2012; @Descripcion: Recupera el registro del proceso de seleccion de casos
        public const String RelacionMedidasSeleccionTabla = "SELECT SELECCION.INDICADOR_SELECCION \"Opc\", MEDIDAS.ID_MEDIDA \"Medida\", MEDIDAS.VERSION_MEDIDA \"Versión\", GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MEDIDAS.TIPO_MEDIDA) \"Tipo Medida\", \n" +
                                           "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MEDIDAS.JERARQUIA_MEDIDA) \"Jerarquía\",MEDIDAS.NOMBRE_MEDIDA \"Nombre Medida\"\n" +
                                            "FROM GRTA_SELECCION_MEDIDAS SELECCION, GRTA_MEDIDAS MEDIDAS\n" +
                                            "WHERE SELECCION.ID_MEDIDA = MEDIDAS.ID_MEDIDA AND SELECCION.VERSION_MEDIDA=MEDIDAS.VERSION_MEDIDA AND SELECCION.SELECCION_CASOS = TO_NUMBER(:pSeleccionCasos)\n" +
                                            "ORDER BY SELECCION.INDICADOR_SELECCION DESC, 2 DESC";
        //@Autor: WMarcia; @Version: 1.0, 04/07/2012; @Descripcion: Recupera los casos seleccionados
        public const String CasosSeleccionadosTabla = "SELECT ROWNUM \"#\",PERFIL.IDENTIFICADOR_DECLARACION \"Identificación\", \n" +
           "PERFIL.SUJETO_RIESGO||'-'||PERFIL.ID_PERFIL||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',691,NULL,PERFIL.IDENTIFICADOR_DECLARACION) \"Descripcion\",\n" +
           "TO_CHAR(CONSOLIDADO.TOTAL_DUAS,'999,999,999,999') \"DUAs\",TO_CHAR(CONSOLIDADO.TOTAL_VALOR_ADUANAS,'999,999,999,999') \"Valor ($)\",TO_CHAR(CONSOLIDADO.TOTAL_TRIBUTOS,'999,999,999,999') \"Tributos ($)\", \n" +
           "TO_CHAR(ROUND(CONSOLIDADO.TOTAL_ROJOS*100/CONSOLIDADO.TOTAL_DUAS,1))||' %' \"%Rojo\",\n" +
           "TO_CHAR(ROUND(CONSOLIDADO.TOTAL_AMARILLOS*100/CONSOLIDADO.TOTAL_DUAS,1))||' %' \"%Amari.\",\n" +
           "TO_CHAR(ROUND(CONSOLIDADO.TOTAL_VERDES*100/CONSOLIDADO.TOTAL_DUAS,1))||' %' \"%Verde\",\n" +
           "TO_CHAR(CONSOLIDADO.TOTAL_DUAS_INCIDENCIA,'999,999,999,999') \"#Aciertos\",TO_CHAR(ROUND(CONSOLIDADO.TOTAL_DUAS_INCIDENCIA*100/CONSOLIDADO.TOTAL_DUAS,1))||' %' \"%Aciertos\",\n" +
           "TO_CHAR(CONSOLIDADO.TOTAL_MULTAS,'999,999,999,999') \"Hallazgo (L)\",TO_CHAR(CONSOLIDADO.TOTAL_MULTAS,'999,999,999,999') \"Multa(L)\" \n" +
           "FROM GRTA_DECLARACIONES_PERFIL PERFIL,GRTA_CONSOLIDADO_OPERADOR CONSOLIDADO\n" +
           "WHERE\n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND CONSOLIDADO.TIPO_OPERADOR=22152 AND \n" +
           "CONSOLIDADO.CODIGO_OPERADOR=PERFIL.IDENTIFICADOR_DECLARACION";


        public const String CasosSeleccionadosGeneral = "SELECT ROWNUM \"#\", X.IDENTIFICADOR_DECLARACION \"Identificación\", X.DESCRIPCION \"Descripcion\",\n" +
           "X.IMPORTANCIA \"Importancia\", X.CLASE \"Clase\", X.ADMINISTRACION \"Administración\", X.MEDIDAS \"Medidas\", X.INDICE \"Indice\",\n" +
           "X.GRADO \"Grado\",X.ACTIVIDAD \"Actividad Económica\"\n" +
           "FROM\n" +
           "(\n" +
           "    SELECT PERFIL.IDENTIFICADOR_DECLARACION,PERFIL.PERFIL_RIESGO,  \n" +
           "    PERFIL.SUJETO_RIESGO||'-'||PERFIL.ID_PERFIL||'-'||DECODE(RUC.C_CLASE,'J',RUC.S_1APE_RASOC,RUC.S_2APE_ABREV||' '||RUC.S_NOMBRES ) DESCRIPCION,  \n" +
           "    GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',105,NULL,RUC.C_IMPORTANCIA) IMPORTANCIA,  \n" +
           "    GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',106,NULL,RUC.C_CLASE) CLASE,  \n" +
           "    GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',120,NULL,RUC.C_ADM_TRIB) ADMINISTRACION,  \n" +
           "    (SELECT COUNT(MEDIDA.ID_MEDIDA )  \n" +
           "    FROM GRTA_DECLARACIONES_LINEA LINEA, GRTA_DECLARACIONES_MEDIDA MEDIDA \n" +
           "    WHERE \n" +
           "    LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND \n" +
           "    MEDIDA.DECLARACION_LINEA=LINEA.DECLARACION_LINEA_NEW AND \n" +
           "    MEDIDA.RESULTADO_MEDIDA=9296) MEDIDAS,                                   \n" +
           "    TO_CHAR(PERFIL.PERFIL_RIESGO,'999,999,990.0999') INDICE, GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(PERFIL.GRADO_RIESGO) GRADO,  \n" +
           "    GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',108,NULL,ACTIVIDAD.C_ACT_ECO) ACTIVIDAD \n" +
           "    FROM GRTA_DECLARACIONES_PERFIL PERFIL,RC_RUC RUC, RC_ACT_ECO ACTIVIDAD    \n" +
           "    WHERE     \n" +
           "    PERFIL.SELECCION_CASOS=:pSeleccionCasos AND    \n" +
           "    PERFIL.INDICADOR_SELECCION=1 AND   \n" +
           "    RUC.NIT=PERFIL.IDENTIFICADOR_DECLARACION AND \n" +
           "    ACTIVIDAD.NIT=RUC.NIT AND \n" +
           "    SYSDATE BETWEEN ACTIVIDAD.FI_ACT_ECO AND DECODE(ACTIVIDAD.FF_ACT_ECO,NULL,SYSDATE,SYSDATE-1) AND \n" +
           "    ACTIVIDAD.ORDEN=1 \n" +
           "    ORDER BY PERFIL.PERFIL_RIESGO DESC\n" +
           ") X";


        public const String CasosMercanciaGeneral = "SELECT DETALLE.CODIGO_ARANCELARIO \"Arancel\",GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',524,NULL,DETALLE.CODIGO_ARANCELARIO) \"Descripción\", \n" +
           "TO_CHAR(SUM(DETALLE.VALOR_ADUANAS*GENERAL.TIPO_CAMBIO+DETALLE.PAGADO),'999,999,999,999') \"Total Importación (LPS)\",\n" +
           "TO_CHAR(SUM(DETALLE.VALOR_ADUANAS),'999,999,999,999') \"Valor Aduanas (USD)\",TO_CHAR(SUM(DETALLE.PAGADO),'999,999,999,999') \"Impuesto (LPS)\",\n" +
           "TO_CHAR(DECODE(DETALLE.ESTADO_MERCANCIA,'US',DETALLE.VALOR_ADUANAS*GENERAL.TIPO_CAMBIO+DETALLE.PAGADO,0),'999,999,999,999') \"Mercancía Usada (LPS)\",\n" +
           "TO_CHAR(DECODE(DETALLE.ESTADO_MERCANCIA,'US',0,DETALLE.VALOR_ADUANAS*GENERAL.TIPO_CAMBIO+DETALLE.PAGADO),'999,999,999,999') \"Mercancía Nueva (LPS)\"\n" +
           "FROM   \n" +
           "GRTA_DECLARACIONES_PERFIL PERFIL, GRTA_CONSOLIDADO_GENERAL GENERAL, GRTA_CONSOLIDADO_DETALLE DETALLE  \n" +
           "WHERE  \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND  \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND\n" +
           "GENERAL.CONTRIBUYENTE=PERFIL.IDENTIFICADOR_DECLARACION AND\n" +
           "GENERAL.FECHA_OFICIALIZACION_IDX>=TO_DATE('01/01/2014','DD/MM/YYYY') AND \n" +
           "GENERAL.FECHA_OFICIALIZACION_IDX<=TO_DATE('31/12/2014','DD/MM/YYYY') AND\n" +
           "DETALLE.ID_EXTERNA=GENERAL.ID_EXTERNA\n" +
           "GROUP BY DETALLE.CODIGO_ARANCELARIO,DETALLE.ESTADO_MERCANCIA,GENERAL.TIPO_CAMBIO,DETALLE.PAGADO,DETALLE.VALOR_ADUANAS\n" +
           "ORDER BY 3 DESC";


        public const String CasosModalidadGeneral = "SELECT RUC.C_IMPORTANCIA \"Código\",\n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',105,NULL,RUC.C_IMPORTANCIA) \"Nombre\",\n" +
           "TO_CHAR(COUNT(0),'999,999,999,999') \"Total\",\n" +
           "TO_CHAR(SUM(DECODE(PERFIL.GRADO_RIESGO,510,1,0)),'999,999,999,999') \"Mínimo\",  \n" +
           "TO_CHAR(SUM(DECODE(PERFIL.GRADO_RIESGO,282,1,0)),'999,999,999,999') \"Moderado\",\n" +
           "TO_CHAR(SUM(DECODE(PERFIL.GRADO_RIESGO,107,1,0)),'999,999,999,999') \"Severo\"\n" +
           "FROM GRTA_DECLARACIONES_PERFIL PERFIL,RC_RUC RUC  \n" +
           "WHERE   \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND  \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND \n" +
           "RUC.NIT=PERFIL.IDENTIFICADOR_DECLARACION\n" +
           "GROUP BY RUC.C_IMPORTANCIA\n" +
           "ORDER BY 3 DESC";


        public const String CasosAdministracionGeneral = "SELECT RUC.C_ADM_TRIB \"Código\",\n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',120,NULL,RUC.C_ADM_TRIB) \"Nombre\",\n" +
           "TO_CHAR(COUNT(0),'999,999,999,999') \"Total\",\n" +
           "TO_CHAR(SUM(DECODE(PERFIL.GRADO_RIESGO,510,1,0)),'999,999,999,999') \"Mínimo\",  \n" +
           "TO_CHAR(SUM(DECODE(PERFIL.GRADO_RIESGO,282,1,0)),'999,999,999,999') \"Moderado\",\n" +
           "TO_CHAR(SUM(DECODE(PERFIL.GRADO_RIESGO,107,1,0)),'999,999,999,999') \"Severo\"\n" +
           "FROM GRTA_DECLARACIONES_PERFIL PERFIL,RC_RUC RUC  \n" +
           "WHERE   \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND  \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND \n" +
           "RUC.NIT=PERFIL.IDENTIFICADOR_DECLARACION\n" +
           "GROUP BY RUC.C_ADM_TRIB\n" +
           "ORDER BY 3 DESC";


        public const String CasosActividadGeneral = "SELECT ACTIVIDAD.C_ACT_ECO \"Código\",\n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',108,NULL,ACTIVIDAD.C_ACT_ECO) \"Nombre\",\n" +
           "TO_CHAR(COUNT(0),'999,999,999,999') \"Total\",\n" +
           "TO_CHAR(SUM(DECODE(PERFIL.GRADO_RIESGO,510,1,0)),'999,999,999,999') \"Mínimo\",  \n" +
           "TO_CHAR(SUM(DECODE(PERFIL.GRADO_RIESGO,282,1,0)),'999,999,999,999') \"Moderado\",\n" +
           "TO_CHAR(SUM(DECODE(PERFIL.GRADO_RIESGO,107,1,0)),'999,999,999,999') \"Severo\"\n" +
           "FROM GRTA_DECLARACIONES_PERFIL PERFIL,RC_RUC RUC, RC_ACT_ECO ACTIVIDAD  \n" +
           "WHERE   \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND  \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND \n" +
           "RUC.NIT=PERFIL.IDENTIFICADOR_DECLARACION AND " +
           "ACTIVIDAD.NIT=RUC.NIT AND\n" +
           "SYSDATE BETWEEN ACTIVIDAD.FI_ACT_ECO AND DECODE(ACTIVIDAD.FF_ACT_ECO,NULL,SYSDATE,SYSDATE-1) AND\n" +
           "ACTIVIDAD.ORDEN=1\n" +
           "GROUP BY ACTIVIDAD.C_ACT_ECO\n" +
           "ORDER BY 3 DESC";


        public const String CasosMedidas = "SELECT GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAESTRO.TIPO_MEDIDA) \"T.Medida\",\n" +
           "MEDIDA.ID_MEDIDA||'-'||MEDIDA.VERSION_MEDIDA \"Medida\",\n" +
           "PERFIL.SELECCION_CASOS||'@'||MAESTRO.NOMBRE_MEDIDA \"Nombre\",\n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAESTRO.JERARQUIA_MEDIDA) \"Jerarquía\",\n" +
           "COUNT(DISTINCT PERFIL.IDENTIFICADOR_DECLARACION) \"# Contribuyentes\"\n" +
           "FROM GRTA_DECLARACIONES_PERFIL PERFIL, GRTA_DECLARACIONES_LINEA LINEA, GRTA_DECLARACIONES_MEDIDA MEDIDA, GRTA_MEDIDAS MAESTRO   \n" +
           "WHERE  \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND PERFIL.INDICADOR_SELECCION=1 AND\n" +
           "LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND\n" +
           "MEDIDA.DECLARACION_LINEA=LINEA.DECLARACION_LINEA_NEW AND\n" +
           "MEDIDA.RESULTADO_MEDIDA=9296 AND  \n" +
           "MAESTRO.ID_MEDIDA=MEDIDA.ID_MEDIDA AND  \n" +
           "MAESTRO.VERSION_MEDIDA=MEDIDA.VERSION_MEDIDA\n" +
           "GROUP BY MAESTRO.TIPO_MEDIDA,MEDIDA.ID_MEDIDA, MEDIDA.VERSION_MEDIDA,PERFIL.SELECCION_CASOS,MAESTRO.NOMBRE_MEDIDA,MAESTRO.JERARQUIA_MEDIDA\n" +
           "ORDER BY 5 DESC";


        public const String CasosMedidasContribuyente = "SELECT ROWNUM \"#\",PERFIL.IDENTIFICADOR_DECLARACION \"Identificación\", \n" +
           "DECODE(RUC.C_CLASE,'J',RUC.S_1APE_RASOC,RUC.S_2APE_ABREV||' '||RUC.S_NOMBRES ) \"Descripcion\", \n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',105,NULL,RUC.C_IMPORTANCIA) \"Importancia\", \n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',106,NULL,RUC.C_CLASE) \"Clase\", \n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',120,NULL,RUC.C_ADM_TRIB) \"Administración\", \n" +
           "TO_CHAR(PERFIL.PERFIL_RIESGO,'0.99999') \"Indice\",GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(PERFIL.GRADO_RIESGO) \"Grado\", \n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',108,NULL,ACTIVIDAD.C_ACT_ECO) \"Actividad Económica\"  \n" +
           "FROM GRTA_DECLARACIONES_PERFIL PERFIL,RC_RUC RUC,RC_ACT_ECO ACTIVIDAD,\n" +
           "GRTA_DECLARACIONES_LINEA LINEA, GRTA_DECLARACIONES_MEDIDA MEDIDA \n" +
           "WHERE    \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND   \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND  \n" +
           "RUC.NIT=PERFIL.IDENTIFICADOR_DECLARACION AND ACTIVIDAD.NIT=RUC.NIT AND\n" +
           "SYSDATE BETWEEN ACTIVIDAD.FI_ACT_ECO AND DECODE(ACTIVIDAD.FF_ACT_ECO,NULL,SYSDATE,SYSDATE-1) AND\n" +
           "ACTIVIDAD.ORDEN=1 AND\n" +
           "LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND\n" +
           "MEDIDA.DECLARACION_LINEA=LINEA.DECLARACION_LINEA_NEW AND\n" +
           "MEDIDA.RESULTADO_MEDIDA=9296 AND --Medidas Finales.     \n" +
           "MEDIDA.ID_MEDIDA=:pIdMedida AND\n" +
           "MEDIDA.VERSION_MEDIDA=:pVersionMedida";


        public const String CasosSeleccionadosAduana = "SELECT X.CODIGO \"Código\",GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',517,NULL,X.CODIGO) \"Descripción\", \n" +
           "TO_CHAR(X.DUAS,'999,999,999,999') \"DUAs\",TO_CHAR(X.VALOR,'999,999,999,999') \"Valor (m$)\",TO_CHAR(X.TRIBUTOS,'999,999,999,999') \"Tributos (mL)\",  \n" +
           "X.ROJO \"%Rojo\",X.AMARILLO \"%Amarillo\",X.VERDE \"%Verde\",TO_CHAR(X.NUMERO_ACIERTOS,'999,999,999,999') \"#Aciertos\",X.ACIERTOS \"%Aciertos\",TO_CHAR(X.HALLAZGO,'999,999,999,999') \"Hallazgo(mL)\",TO_CHAR(X.HALLAZGO,'999,999,999,999') \"Multa(mL)\" \n" +
           "FROM  \n" +
           "( \n" +
           "SELECT GENERAL.ADUANA_REGISTRO CODIGO, \n" +
           "COUNT(0) DUAS,TRUNC(SUM(GENERAL.TOTAL_VALOR_ADUANAS/GENERAL.TIPO_CAMBIO)/100) VALOR,TRUNC(SUM(GENERAL.TOTAL_PAGADO)/100) TRIBUTOS,  \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'R',1,0))*100/COUNT(0),1))||' %' ROJO, \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'A',1,0))*100/COUNT(0),1))||' %' AMARILLO, \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'V',1,0))*100/COUNT(0),1))||' %' VERDE, \n" +
           "SUM(GENERAL.INDICADOR_VARIACION) NUMERO_ACIERTOS,TO_CHAR(ROUND(SUM(GENERAL.INDICADOR_VARIACION)*100/COUNT(0),1)) ACIERTOS, \n" +
           "TRUNC(SUM(DECODE(GENERAL.INDICADOR_VARIACION,1,(GENERAL.TOTAL_VARIACION),0))/100) HALLAZGO  \n" +
           "FROM  \n" +
           "GRTA_DECLARACIONES_PERFIL PERFIL, GRTA_DECLARACIONES_LINEA LINEA, GRTA_CONSOLIDADO_GENERAL GENERAL \n" +
           "WHERE \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND \n" +
           "LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND \n" +
           "GENERAL.ID_EXTERNA=LINEA.IDENTIFICADOR_LINEA \n" +
           "GROUP BY GENERAL.ADUANA_REGISTRO \n" +
           ") X \n" +
           "ORDER BY 2";


        public const String CasosSeleccionadosRegimen = "SELECT X.CODIGO \"Código\",GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',NULL,221,X.CODIGO) \"Descripción\", \n" +
           "TO_CHAR(X.DUAS,'999,999,999,999') \"DUAs\",TO_CHAR(X.VALOR,'999,999,999,999') \"Valor (m$)\",TO_CHAR(X.TRIBUTOS,'999,999,999,999') \"Tributos (mL)\",  \n" +
           "X.ROJO \"%Rojo\",X.AMARILLO \"%Amarillo\",X.VERDE \"%Verde\",TO_CHAR(X.NUMERO_ACIERTOS,'999,999,999,999') \"#Aciertos\",X.ACIERTOS \"%Aciertos\",TO_CHAR(X.HALLAZGO,'999,999,999,999') \"Hallazgo(mL)\",TO_CHAR(X.HALLAZGO,'999,999,999,999') \"Multa(mL)\" \n" +
           "FROM  \n" +
           "( \n" +
           "SELECT GENERAL.DESTINACION_GENERAL CODIGO, \n" +
           "COUNT(0) DUAS,TRUNC(SUM(GENERAL.TOTAL_VALOR_ADUANAS/GENERAL.TIPO_CAMBIO)/100) VALOR,TRUNC(SUM(GENERAL.TOTAL_PAGADO)/100) TRIBUTOS,  \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'R',1,0))*100/COUNT(0),1))||' %' ROJO, \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'A',1,0))*100/COUNT(0),1))||' %' AMARILLO, \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'V',1,0))*100/COUNT(0),1))||' %' VERDE, \n" +
           "SUM(GENERAL.INDICADOR_VARIACION) NUMERO_ACIERTOS,TO_CHAR(ROUND(SUM(GENERAL.INDICADOR_VARIACION)*100/COUNT(0),1)) ACIERTOS, \n" +
           "TRUNC(SUM(DECODE(GENERAL.INDICADOR_VARIACION,1,(GENERAL.TOTAL_VARIACION),0))/100) HALLAZGO  \n" +
           "FROM  \n" +
           "GRTA_DECLARACIONES_PERFIL PERFIL, GRTA_DECLARACIONES_LINEA LINEA, GRTA_CONSOLIDADO_GENERAL GENERAL \n" +
           "WHERE \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND \n" +
           "LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND \n" +
           "GENERAL.ID_EXTERNA=LINEA.IDENTIFICADOR_LINEA \n" +
           "GROUP BY GENERAL.DESTINACION_GENERAL \n" +
           ") X \n" +
           "ORDER BY 2";


        public const String CasosSeleccionadosOperacion = "SELECT X.CODIGO \"Código\",DECODE(X.CODIGO,'I','Importación','E','Exportación', 'T','Tránsito') \"Descripción\", \n" +
           "TO_CHAR(X.DUAS,'999,999,999,999') \"DUAs\",TO_CHAR(X.VALOR,'999,999,999,999') \"Valor (m$)\",TO_CHAR(X.TRIBUTOS,'999,999,999,999') \"Tributos (mL)\",  \n" +
           "X.ROJO \"%Rojo\",X.AMARILLO \"%Amarillo\",X.VERDE \"%Verde\",TO_CHAR(X.NUMERO_ACIERTOS,'999,999,999,999') \"#Aciertos\",X.ACIERTOS \"%Aciertos\",TO_CHAR(X.HALLAZGO,'999,999,999,999') \"Hallazgo(mL)\",TO_CHAR(X.HALLAZGO,'999,999,999,999') \"Multa(mL)\" \n" +
           "FROM  \n" +
           "( \n" +
           "SELECT GENERAL.IMPO_EXPO CODIGO, \n" +
           "COUNT(0) DUAS,TRUNC(SUM(GENERAL.TOTAL_VALOR_ADUANAS/GENERAL.TIPO_CAMBIO)/100) VALOR,TRUNC(SUM(GENERAL.TOTAL_PAGADO)/100) TRIBUTOS,  \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'R',1,0))*100/COUNT(0),1))||' %' ROJO, \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'A',1,0))*100/COUNT(0),1))||' %' AMARILLO, \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'V',1,0))*100/COUNT(0),1))||' %' VERDE, \n" +
           "SUM(GENERAL.INDICADOR_VARIACION) NUMERO_ACIERTOS,TO_CHAR(ROUND(SUM(GENERAL.INDICADOR_VARIACION)*100/COUNT(0),1)) ACIERTOS, \n" +
           "TRUNC(SUM(DECODE(GENERAL.INDICADOR_VARIACION,1,(GENERAL.TOTAL_VARIACION),0))/100) HALLAZGO  \n" +
           "FROM  \n" +
           "GRTA_DECLARACIONES_PERFIL PERFIL, GRTA_DECLARACIONES_LINEA LINEA, GRTA_CONSOLIDADO_GENERAL GENERAL \n" +
           "WHERE \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND \n" +
           "LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND \n" +
           "GENERAL.ID_EXTERNA=LINEA.IDENTIFICADOR_LINEA \n" +
           "GROUP BY GENERAL.IMPO_EXPO \n" +
           ") X \n" +
           "ORDER BY 2";


        public const String CasosSeleccionadosAgencia = "SELECT X.CODIGO \"Código\",GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',507,NULL,X.CODIGO) \"Descripción\", \n" +
           "TO_CHAR(X.DUAS,'999,999,999,999') \"DUAs\",TO_CHAR(X.VALOR,'999,999,999,999') \"Valor (m$)\",TO_CHAR(X.TRIBUTOS,'999,999,999,999') \"Tributos (mL)\",  \n" +
           "X.ROJO \"%Rojo\",X.AMARILLO \"%Amarillo\",X.VERDE \"%Verde\",TO_CHAR(X.NUMERO_ACIERTOS,'999,999,999,999') \"#Aciertos\",X.ACIERTOS \"%Aciertos\",TO_CHAR(X.HALLAZGO,'999,999,999,999') \"Hallazgo(mL)\",TO_CHAR(X.HALLAZGO,'999,999,999,999') \"Multa(mL)\" \n" +
           "FROM  \n" +
           "( \n" +
           "SELECT GENERAL.AGENTE_ADUANAS CODIGO, \n" +
           "COUNT(0) DUAS,TRUNC(SUM(GENERAL.TOTAL_VALOR_ADUANAS/GENERAL.TIPO_CAMBIO)/100) VALOR,TRUNC(SUM(GENERAL.TOTAL_PAGADO)/100) TRIBUTOS,  \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'R',1,0))*100/COUNT(0),1))||' %' ROJO, \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'A',1,0))*100/COUNT(0),1))||' %' AMARILLO, \n" +
           "TO_CHAR(ROUND(SUM(DECODE(GENERAL.CANAL_SELECTIVIDAD,'V',1,0))*100/COUNT(0),1))||' %' VERDE, \n" +
           "SUM(GENERAL.INDICADOR_VARIACION) NUMERO_ACIERTOS,TO_CHAR(ROUND(SUM(GENERAL.INDICADOR_VARIACION)*100/COUNT(0),1)) ACIERTOS, \n" +
           "TRUNC(SUM(DECODE(GENERAL.INDICADOR_VARIACION,1,(GENERAL.TOTAL_VARIACION),0))/100) HALLAZGO  \n" +
           "FROM  \n" +
           "GRTA_DECLARACIONES_PERFIL PERFIL, GRTA_DECLARACIONES_LINEA LINEA, GRTA_CONSOLIDADO_GENERAL GENERAL \n" +
           "WHERE \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND \n" +
           "LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND \n" +
           "GENERAL.ID_EXTERNA=LINEA.IDENTIFICADOR_LINEA \n" +
           "GROUP BY GENERAL.AGENTE_ADUANAS \n" +
           ") X \n" +
           "ORDER BY 2";

        //@Autor: WMarcia; @Version: 1.0, 04/07/2012; @Descripcion: Recupera la descripcion del compendio detalle
        public const String EstadoInicial = "SELECT GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(?) FROM DUAL";
        //@Autor: WMarcia; @Version: 1.0, 04/07/2012; @Descripcion: Recupera la expresion logica del filtro del programa
        //usado 16/01/2018 en Programa Fiscalizacion / Consultar después de grabar
        public const String ExpresionLogicaFiltroPrograma = "SELECT NVL(GRPK_OPERACIONES_PROGRAMA.GRFN_CONSULTAR_EXPRESIO_FILTRO(?), ' ') descripcion FROM DUAL";
        //@Autor: WMarcia; @Version: 1.0, 08/04/2013; @Descripcion: Recupera la expresion logica del filtro de la seleccion de caso
        public const String ExpresionLogicaFiltroSelCas = "SELECT NVL(GRPK_OPERACIONES_PROGRAMA.GRFN_CONSULTAR_EXPRESIO_SELCAS(?), ' ') FROM DUAL";


        public const String HistorialOperacionesPolitica = "SELECT ROWNUM \"#\", X.* FROM (SELECT UPPER(SUBSTR(GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(POLITICA.TIPO_OPERACION),1,3)) \"Oper\",TO_CHAR(POLITICA.FECHA_OPERACION,'DD/MM/YYYY HH24:MI') \"Fecha y Hora\",\n" +
           "SESSIONES.CODIGO_USUARIO||'-'||(SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=63 AND CODIGO_ALTERNO=SESSIONES.CODIGO_USUARIO) \"Nombre Usuario\",\n" +
           "NVL(VARIACIONES.NOMBRE_CAMPO,'EN BLANCO') \"Casilla Modificada\",DECODE(VARIACIONES.VALOR_ANTIGUO,'','EN BLANCO',VARIACIONES.VALOR_ANTIGUO) \"Valor Antiguo\", DECODE(VARIACIONES.VALOR_NUEVO,'','EN BLANCO',VARIACIONES.VALOR_NUEVO) \"Valor Nuevo\"  \n" +
           "FROM GRTA_OPERACIONES_POLITICA POLITICA, GRTA_SESSION SESSIONES, GRTA_VARIACIONES VARIACIONES\n" +
           "WHERE\n" +
           "POLITICA.ID_POLITICA=:pPolitica AND\n" +
           "SESSIONES.ID_SESSION=POLITICA.SESSION_OPERACION AND\n" +
           "VARIACIONES.ID_SESSION(+)=POLITICA.SESSION_OPERACION AND\n" +
           "VARIACIONES.NOMBRE_TABLA(+)='GRTA_POLITICA_INSTITUCIONAL' AND\n" +
           "VARIACIONES.CLAVE_REGISTRO(+)=POLITICA.ID_POLITICA AND\n" +
           "VARIACIONES.FECHA_INGRESO(+)=POLITICA.FECHA_OPERACION\n" +
           "ORDER BY POLITICA.FECHA_OPERACION DESC) X ";

        /*Usado en consultar perfil casos riesgos/ Combo Grado*/
        public const String GradoRiesgoCombo = "SELECT ID_DETALLE CODIGO, NOMBRE DESCRIPCION FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=5 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE,FECHA_FIN_VIGENCIA) ORDER BY REFERENCIA1 DESC";

        /*Usado en Consultar Perfil Riesgo Casos/ Combo Programa*/
        public const String ProgramaFiscalizacion = "SELECT ID_PROGRAMA CODIGO, ID_PROGRAMA||'-'||NOMBRE DESCRIPCION FROM GRTA_PROGRAMA WHERE SUJETO_RIESGO=TO_NUMBER(?) ORDER BY ID_PROGRAMA";

        /*Usado en Consultar Perfil Riesgo Casos/ Combo Mes*/
        public const String ProgramaFiscalizacionMes = "SELECT MENSUAL.MES_CALENDARIO CODIGO, MES.NOMBRE  DESCRIPCION\n" +
           "FROM GRTA_PROGRAMA_MENSUAL MENSUAL, GRTA_COMPENDIO_DETALLE MES\n" +
           "WHERE\n" +
           "MENSUAL.ID_PROGRAMA=TO_NUMBER(?) AND\n" +
           "MES.ID_DETALLE=MENSUAL.MES_CALENDARIO";

        /*Usado en Consultar Perfil Riesgo Casos/ Combo Casos*/
        public const String ProgramaFiscalizacionCasos = "SELECT CASOS.SELECCION_CASOS CODIGO, '('||MES.NOMBRE||'): '||CASOS.DESCRIPCION DESCRIPCION\n" +
           "FROM GRTA_SELECCION_CASOS CASOS, GRTA_PROGRAMA_MENSUAL MENSUAL, GRTA_COMPENDIO_DETALLE MES\n" +
           "WHERE\n" +
           "MENSUAL.ID_PROGRAMA=TO_NUMBER(?) AND\n" +
           "CASOS.PROGRAMA_MENSUAL=MENSUAL.PROGRAMA_MENSUAL AND\n" +
           "MES.ID_DETALLE=MENSUAL.MES_CALENDARIO\n" +
           "ORDER BY 2";

        /*Aumentado*/
        /*Usado en Proceso de Selección de Casos/ Segundo Combo Filtro Lista*/
        public const String OperadorMatematicoInNotInCombo = "SELECT OPERADOR_MATEMATICO codigo, SIMBOLO_USUARIO descripcion  FROM GRTA_OPERADOR_MATEMATICO WHERE OPERADOR_MATEMATICO IN (14, 17)";

        /*Aumentado*/
        /*Usado en Proceso de Selección de Casos / Tercer Combo Filtro Lista*/
        public const String ProcesoSeleccionCasoListaCombo = "SELECT DET.ID_DETALLE CODIGO, DET.NOMBRE DESCRIPCION \n" +
                   "FROM GRTA_COMPENDIO_GENERAL GEN, GRTA_COMPENDIO_DETALLE DET \n" +
                   "WHERE GEN.ID_COMPENDIO = 324 \n" +
                   "AND GEN.TIPO_TABLA = 3 \n" +
                   "AND DET.ID_COMPENDIO = GEN.ID_COMPENDIO \n" +
                   "AND SYSDATE BETWEEN DET.FECHA_INICIO_VIGENCIA AND NVL(DET.FECHA_FIN_VIGENCIA,SYSDATE)";

        /*WMarcia 05-12-2017 uasada*/
        public const String CargaMasivaDescCodigoSubElemento = "SELECT GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO (:pTipoBusqueda,:pVariableCatalogo,:pCompendioSubdetalle,:pCodigoElemento) VALOR FROM DUAL";


        public const String PerfilCalificadoras = "SELECT MAESTRO.DESCRIPCION_BREVE \"Concepto\", SUBSTR(MAESTRO.EXPRESION_CONSOLIDACION,1,60)||'..' \"Expresión de Cálculo\", \n" +
           "     TO_CHAR(COEFICIENTE.COEFICIENTE_VARIABLE,'0.99999999') \"Peso (Coef.)\",  \n" +
           "     TO_CHAR(CALIFICADORAS.VALOR_INDICE,'0.99999999') \"Calificación\"  \n" +
           "     FROM GRTA_ENTIDADES_MEDIDAS ENTIDADES, GRTA_DOMINIO_VARIABLE DOMINIO,  \n" +
           "     GRTA_DETALLE_DOMINIO CALIFICADORAS,\n" +
           "     GRTA_VARIABLES MAESTRO,\n" +
           "     GRTA_ANALISIS_EST ANALISIS, GRTA_ENTIDADES_EST ENTIDAD_ES,\n" +
           "     GRTA_VARIABLE_CALIFICADORA_EST COEFICIENTE\n" +
           "     WHERE  \n" +
           "     ENTIDADES.ID_MEDIDA=:pIdMedida AND --Parámetro Medida \n" +
           "     ENTIDADES.VERSION_MEDIDA=:pVersionMedida AND --Parámetro Versión  \n" +
           "     ENTIDADES.CODIGO_VARIABLE=:pCodigoVariable AND --Parámetro Codigo Variable predictora  \n" +
           "     DOMINIO.ENTIDADES_MEDIDAS=ENTIDADES.ENTIDADES_MEDIDAS AND  \n" +
           "     DOMINIO.VALOR_VARIABLE=:pCodigoOperador AND --Parámetro: Código del Operador \n" +
           "     ANALISIS.ID_MEDIDA=ENTIDADES.ID_MEDIDA AND\n" +
           "     ANALISIS.VERSION_MEDIDA=ENTIDADES.VERSION_MEDIDA AND\n" +
           "     ANALISIS.RESULTADO_ANALISIS=GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO AND\n" +
           "     ENTIDAD_ES.ID_ANALISIS=ANALISIS.ID_ANALISIS AND\n" +
           "     ENTIDAD_ES.ENTIDADES_MEDIDAS=ENTIDADES.ENTIDADES_MEDIDAS AND\n" +
           "     COEFICIENTE.ENTIDADES_EST=ENTIDAD_ES.ENTIDADES_EST AND     \n" +
           "     CALIFICADORAS.VARIABLE_CALIFICADORA=COEFICIENTE.VARIABLE_CALIFICADORA AND  \n" +
           "     CALIFICADORAS.ENTIDADES_MEDIDAS=DOMINIO.ENTIDADES_MEDIDAS AND  \n" +
           "     CALIFICADORAS.SECUENCIA_DOMINIO=DOMINIO.SECUENCIA_DOMINIO AND\n" +
           "     MAESTRO.CODIGO_VARIABLE=COEFICIENTE.VARIABLE_CALIFICADORA                \n" +
           "     ORDER BY CALIFICADORAS.VALOR_INDICE DESC";


        public const String MedidasProgramas = "SELECT '' \"Opcion\", MEDIDAS.ID_MEDIDA || '-' || MEDIDAS.VERSION_MEDIDA \"Medida\", \n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MEDIDAS.TIPO_MEDIDA) \"Tipo Medida\", \n" +
           "MEDIDAS.NOMBRE_MEDIDA \"Nombre Medida\" \n" +
           "FROM GRTA_MEDIDAS MEDIDAS\n" +
           "WHERE\n" +
           "MEDIDAS.ESTADO_MEDIDA=42 AND\n" +
           "SYSDATE BETWEEN MEDIDAS.FECHA_INICIO_VIGENCIA AND DECODE(MEDIDAS.FECHA_FIN_VIGENCIA,'',SYSDATE) AND\n" +
           "MEDIDAS.SUJETO_RIESGO IN \n" +
           "(\n" +
           "SELECT PROGRAMA.SUJETO_RIESGO \n" +
           "FROM GRTA_PROGRAMA_MENSUAL MENSUAL, GRTA_PROGRAMA PROGRAMA\n" +
           "WHERE\n" +
           "MENSUAL.PROGRAMA_MENSUAL=:pProgramaMensual AND\n" +
           "PROGRAMA.ID_PROGRAMA=MENSUAL.ID_PROGRAMA\n" +
           ")";

        //@Autor: WMarcia; @Version: 1.0, 22/07/2012; @Descripcion: Recupera el código y la descripción de las variables para el analisis de Benford
        public const String VariableBenfordCombo = "SELECT CODIGO_VARIABLE, DESCRIPCION_BREVE FROM GRTA_VARIABLES WHERE SUJETO_RIESGO = TO_NUMBER(?)\n" +
                                   "AND TIPO_VARIABLE = 117 AND TIPO_DATO=79 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";
        //@Autor: WMarcia; @Version: 1.0, 24/07/2012; @Descripcion: Recupera el código y la descripción de las variables para el analisis de Benford
        public const String CriterioBenfordCombo = "SELECT VARIABLES.CODIGO_VARIABLE, COMPENDIO.NOMBRE \n" +
                               "FROM GRTA_VARIABLES VARIABLES, GRTA_COMPENDIO_DETALLE COMPENDIO\n" +
                               "WHERE\n" +
                               "VARIABLES.SUJETO_RIESGO=? AND\n" +
                               "VARIABLES.TIPO_VARIABLE=57 AND\n" +
                               "SYSDATE BETWEEN VARIABLES.FECHA_INICIO_VIGENCIA AND DECODE(VARIABLES.FECHA_FIN_VIGENCIA,'',SYSDATE,VARIABLES.FECHA_FIN_VIGENCIA) AND\n" +
                               "COMPENDIO.ID_COMPENDIO=82 AND\n" +
                               "COMPENDIO.SUJETO_RIESGO=VARIABLES.SUJETO_RIESGO AND\n" +
                               "COMPENDIO.CODIGO_ALTERNO=VARIABLES.CODIGO_VARIABLE AND\n" +
                               "SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND DECODE(COMPENDIO.FECHA_FIN_VIGENCIA,'',SYSDATE,COMPENDIO.FECHA_FIN_VIGENCIA)\n" +
                               "ORDER BY COMPENDIO.NOMBRE";
        //@Autor: WMarcia; @Version: 1.0, 24/07/2012; @Descripcion: Recupera el registro de Analisis de Benford
        //@Autor: WMarcia; @Version: 1.0, 24/07/2012; @Descripcion: Recupera el registro de Analisis de Benford, Funcion Convertida GRFN_NOMBRE_CODIGO_VARIABLE
        public const String BenfordMaestro = "SELECT BENFORD.ID_BENFORD, BENFORD.CRITERIO_ANALISIS COD_CRITERIO_ANALISIS, CASE WHEN BENFORD.CRITERIO_ANALISIS IS NULL THEN 'Indefinido' ELSE UPPER(VAR.DESCRIPCION_BREVE) END CRITERIO_ANALISIS, \n" +
                           "TO_CHAR(BENFORD.FECHA_INICIO_ANALISIS, 'DD/MM/YYYY') FECHA_INICIO_ANALISIS, TO_CHAR(BENFORD.FECHA_FIN_ANALISIS, 'DD/MM/YYYY') FECHA_FIN_ANALISIS, \n" +
                           "NVL(TO_CHAR(BENFORD.FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY'),' ') FECHA_INICIO_VIGENCIA, NVL(TO_CHAR(BENFORD.FECHA_FIN_VIGENCIA, 'DD/MM/YYYY'), ' ') FECHA_FIN_VIGENCIA, \n" +
                           "BENFORD.ESTADO_PROCESO COD_ESTADO_BENFORD, GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(BENFORD.ESTADO_PROCESO) ESTADO_BENFORD, BENFORD.DESCRIPCION, \n" +
                           "BENFORD.SUJETO_RIESGO COD_SUJETO_RIESGO,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_SUJETO_RIESGO(BENFORD.SUJETO_RIESGO) SUJETO_RIESGO\n" +
                           "FROM \n" +
                           "GRTA_BENFORD BENFORD \n" +
                           "LEFT JOIN GRTA_VARIABLES VAR ON VAR.CODIGO_VARIABLE=BENFORD.CRITERIO_ANALISIS \n" +
                           "WHERE  \n" +
                           "BENFORD.ID_BENFORD=TO_NUMBER(?)";
        //@Autor: WMarcia; @Version: 1.0, 24/07/2012; @Descripcion: Recupera la expresion logica de filtro
        public const String ExpresionLogicaFiltroBenford = "SELECT NVL(GRPK_OPERACIONES_BENFORD.GRFN_CONSULTAR_EXPRESIO_FILTRO(?), ' ') FROM DUAL";
        //@Autor: WMarcia; @Version: 1.0, 24/07/2012; @Descripcion: Recupera los registros de variables de benford
        public const String BenfordVariablesBenford = "SELECT VARIABLES_BENFORD, ID_BENFORD, CODIGO_VARIABLE\n" +
                                      "FROM GRTA_VARIABLES_BENFORD VARIABLES_BENFORD\n" +
                                      "WHERE VARIABLES_BENFORD.ID_BENFORD = TO_NUMBER(?)";
        //@Autor: WMarcia; @Version: 1.0, 22/07/2012; @Descripcion: Recupera el código y la descripción de las variables para el analisis de Benford
        public const String VariablesBenfordComboT = "SELECT BENFORD.CODIGO_VARIABLE, VARIABLES.DESCRIPCION_BREVE \n" +
                           "FROM GRTA_VARIABLES_BENFORD BENFORD, GRTA_VARIABLES VARIABLES\n" +
                           "WHERE\n" +
                           "ID_BENFORD=TO_NUMBER(:pIdBenford) AND\n" +
                           "VARIABLES.CODIGO_VARIABLE=BENFORD.CODIGO_VARIABLE";
        //@Autor: WMarcia; @Version: 1.0, 24/07/2012; @Descripcion: Recupera los Programas de Fiscalizacion para posiblemente ser consultados
        //Funcion Convertida: GRFN_NOMBRE_CODIGO_VARIABLE
        public const String BenfordConsTabla = "SELECT  BENFORD.ID_BENFORD \"No Análisis\", BENFORD.DESCRIPCION \"Descripcion\", GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_SUJETO_RIESGO(BENFORD.SUJETO_RIESGO) \"Sujeto\",\n" +
                           "CASE WHEN BENFORD.CRITERIO_ANALISIS IS NULL THEN 'Indefinido' ELSE UPPER(VAR.DESCRIPCION_BREVE) END \"Criterio\", TO_CHAR(BENFORD.FECHA_REGISTRO,'DD/MM/YYYY HH24:MI') \"F.Registro\", \n" +
                           "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(BENFORD.ESTADO_PROCESO) \"Estado\"\n" +
                           "FROM GRTA_BENFORD BENFORD\n" +
                           "LEFT JOIN GRTA_VARIABLES VAR ON VAR.CODIGO_VARIABLE=BENFORD.CRITERIO_ANALISIS\n" +
                           "WHERE\n" +
                           "(BENFORD.ID_BENFORD= TO_NUMBER(:pIdBenford) OR :pIdBenford IS NULL) AND\n" +
                           "(BENFORD.SUJETO_RIESGO= TO_NUMBER(:pSujetoRiesgo) OR :pSujetoRiesgo IS NULL) AND\n" +
                           "(BENFORD.CRITERIO_ANALISIS=TO_NUMBER(:pCriterio) OR :pCriterio IS NULL) AND\n" +
                           "(BENFORD.ESTADO_PROCESO=TO_NUMBER(:pEstado) OR :pEstado IS NULL) AND\n" +
                           "(BENFORD.DESCRIPCION='%'||UPPER(:pDescripcion)||'%' OR :pDescripcion IS NULL) AND\n" +
                           "((TO_DATE(TO_CHAR(FECHA_REGISTRO,'DD/MM/YYYY'))>=TO_DATE(:pFechaInicio,'DD/MM/YYYY') AND   \n" +
                           "TO_DATE(TO_CHAR(FECHA_REGISTRO,'DD/MM/YYYY'))<=TO_DATE(:pFechaFin,'DD/MM/YYYY')) OR (:pFechaInicio IS NULL AND :pFechaFin IS NULL))\n" +
                           "ORDER BY BENFORD.ID_BENFORD DESC";
        //@Autor: WMarcia; @Version: 1.0, 25/07/2012; @Descripcion: Recupera los Estados que puede tener un Analisis de Benford
        public const String EstadoBenfordCombo = "SELECT ID_DETALLE CODIGO, NOMBRE ESTADO FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=69";
        //@Autor: WMarcia; @Version: 1.0, 24/07/2012; @Descripcion: Recupera los Programas de Fiscalizacion para posiblemente ser modificados
        //Función Convertida: GRFN_NOMBRE_CODIGO_VARIABLE
        public const String BenfordModTabla = "SELECT  BENFORD.ID_BENFORD \"No Análisis\", BENFORD.DESCRIPCION \"Descripcion\", GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_SUJETO_RIESGO(BENFORD.SUJETO_RIESGO) \"Sujeto\",\n" +
                           "CASE WHEN BENFORD.CRITERIO_ANALISIS IS NULL THEN 'Indefinido' ELSE UPPER(VAR.DESCRIPCION_BREVE) END \"Criterio\", TO_CHAR(BENFORD.FECHA_REGISTRO,'DD/MM/YYYY HH24:MI') \"F.Registro\", \n" +
                           "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(BENFORD.ESTADO_PROCESO) \"Estado\",'' \"Operacion\"\n" +
                           "FROM GRTA_BENFORD BENFORD\n" +
                           "LEFT JOIN GRTA_VARIABLES VAR ON VAR.CODIGO_VARIABLE=BENFORD.CRITERIO_ANALISIS\n" +
                           "WHERE\n" +
                           "(BENFORD.ID_BENFORD= TO_NUMBER(:pIdBenford) OR :pIdBenford IS NULL) AND\n" +
                           "(BENFORD.SUJETO_RIESGO= TO_NUMBER(:pSujetoRiesgo) OR :pSujetoRiesgo IS NULL) AND\n" +
                           "(BENFORD.CRITERIO_ANALISIS=TO_NUMBER(:pCriterio) OR :pCriterio IS NULL) AND\n" +
                           "(BENFORD.ESTADO_PROCESO=TO_NUMBER(:pEstado) OR :pEstado IS NULL) AND\n" +
                           "(BENFORD.DESCRIPCION='%'||UPPER(:pDescripcion)||'%' OR :pDescripcion IS NULL) AND\n" +
                           "((TO_DATE(TO_CHAR(FECHA_REGISTRO,'DD/MM/YYYY'))>=TO_DATE(:pFechaInicio,'DD/MM/YYYY') AND   \n" +
                           "TO_DATE(TO_CHAR(FECHA_REGISTRO,'DD/MM/YYYY'))<=TO_DATE(:pFechaFin,'DD/MM/YYYY')) OR (:pFechaInicio IS NULL AND :pFechaFin IS NULL))\n" +
                           "ORDER BY BENFORD.ID_BENFORD DESC";
        //@Autor: WMarcia; @Version: 1.0, 26/07/2012; @Descripcion: Recupera el resultado general del Analisis de Benford
        public const String BenfordResultadoGeneralTabla = "SELECT X.CODIGO \"Código\", GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('D',CATALOGO.VARIABLE_CATALOGO,CATALOGO.TABLA_CODIFICACION,UPPER(x.VALOR_ELEMENTO)) \"Descripcion\",\n" +
           "  TO_CHAR(x.NUMERO_REGISTROS,'999,999,999,999') \"# Registros\",X.BONDAD_INCORRECTA \"# Incumplimientos\"       \n" +
           "FROM\n" +
           "(\n" +
           "    SELECT ELEMENTOS.VALOR_ELEMENTO || ';'  || VARIABLES.VARIABLES_BENFORD || ';' || ELEMENTOS.SEQ_ELEMENTO  CODIGO,ELEMENTOS.NUMERO_REGISTROS,BENFORD.CRITERIO_ANALISIS,\n" +
           "    ELEMENTOS.VALOR_ELEMENTO,ELEMENTOS.BONDAD_INCORRECTA\n" +
           "    FROM GRTA_BENFORD BENFORD, GRTA_VARIABLES_BENFORD VARIABLES, GRTA_ELEMENTOS_BENFORD ELEMENTOS\n" +
           "    WHERE\n" +
           "    BENFORD.ID_BENFORD=TO_NUMBER(:pIdBenford) AND \n" +
           "    VARIABLES.ID_BENFORD=BENFORD.ID_BENFORD AND \n" +
           "    ELEMENTOS.VARIABLES_BENFORD=VARIABLES.VARIABLES_BENFORD\n" +
           ") X, GRTA_VARIABLES CATALOGO\n" +
           "WHERE\n" +
           "CATALOGO.CODIGO_VARIABLE=X.CRITERIO_ANALISIS\n" +
           "ORDER BY X.BONDAD_INCORRECTA DESC";

        //@Autor: WMarcia; @Version: 1.0, 31/07/2012; @Descripcion: Recupera el resultado general del Analisis de Benford
        public const String BenfordResultadoDetalleTabla = "SELECT TO_CHAR(digito) \"Digito\", DECODE(digito,0,' ',TO_CHAR(valor_teorico1*100,'999.99')||'%')  \"D(1): Teórico\", DECODE(digito,0,' ',TO_CHAR(valor_observado1*100,'999.99')||'%') \"D(1): Observado\",\n" +
                                           "TO_CHAR(valor_teorico2*100,'999.99')||'%' \"D(2): Teórico\", TO_CHAR(valor_observado2*100,'999.99')||'%' \"D(2): Observado\", TO_CHAR(valor_teorico3*100,'999.99')||'%' \"D(3): Teórico\",\n" +
                                           "TO_CHAR(valor_observado3*100,'999.99')||'%' \"D(3): Observado\"\n" +
                                           "FROM GRTA_RESULTADOS_BENFORD\n" +
                                           "WHERE VARIABLES_BENFORD=TO_NUMBER(?) AND SEQ_ELEMENTO=TO_NUMBER(?)";


        public const String PerfilItemsMedidas = "SELECT COMPENDIO.NOMBRE \"T.Resultado\", \n" +
                                                "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAESTRO.TIPO_MEDIDA) \"T.Medida\",\n" +
                                                "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(MAESTRO.JERARQUIA_MEDIDA) \"Jerarquía\",\n" +
                                                "MEDIDA.ID_MEDIDA||'-'||MEDIDA.VERSION_MEDIDA \"Medida\", MAESTRO.NOMBRE_MEDIDA \"Nombre\",\n" +
                                                "GRPK_OPERACIONES_MONITOREO.GRFN_CONSULTAR_MED_ITEMS(LINEA.ID_PERFIL,MEDIDA.ID_MEDIDA,MEDIDA.VERSION_MEDIDA,MEDIDA.RESULTADO_MEDIDA) \"Relación Items\" \n" +
                                                "FROM GRTA_DECLARACIONES_LINEA LINEA,GRTA_DECLARACIONES_MEDIDA MEDIDA, GRTA_MEDIDAS MAESTRO,\n" +
                                                "GRTA_COMPENDIO_DETALLE COMPENDIO \n" +
                                                "WHERE\n" +
                                                "LINEA.ID_PERFIL=:pDeclaracion AND\n" +
                                                "MEDIDA.DECLARACION_LINEA=LINEA.DECLARACION_LINEA_NEW AND\n" +
                                                "MAESTRO.ID_MEDIDA=MEDIDA.ID_MEDIDA AND\n" +
                                                "MAESTRO.VERSION_MEDIDA=MEDIDA.VERSION_MEDIDA AND\n" +
                                                "COMPENDIO.ID_DETALLE=MEDIDA.RESULTADO_MEDIDA\n" +
                                                "GROUP BY COMPENDIO.NOMBRE,MAESTRO.TIPO_MEDIDA,MAESTRO.JERARQUIA_MEDIDA,MEDIDA.ID_MEDIDA,MEDIDA.VERSION_MEDIDA,MAESTRO.NOMBRE_MEDIDA,COMPENDIO.REFERENCIA1,LINEA.ID_PERFIL,MEDIDA.RESULTADO_MEDIDA\n" +
                                                "ORDER BY 1";

        public const String PerfilMomentos = "SELECT TO_CHAR(PERFIL.FECHA_REGISTRO,'DD/MM/YYYY HH24:MI:SS') FECHA,NVL(COMPENDIO.CODIGO_ALTERNO,PERFIL.TIPO_MOMENTO) CODIGO,COMPENDIO.NOMBRE,\n" +
           "(SELECT COMPENDIO.NOMBRE FROM GRTA_DECLARACIONES_RESPUESTA RESPUESTA, GRTA_COMPENDIO_DETALLE COMPENDIO \n" +
           "WHERE RESPUESTA.ID_PERFIL=PERFIL.ID_PERFIL AND RESPUESTA.NIVEL_RESPUESTA=540 AND COMPENDIO.ID_DETALLE=RESPUESTA.CANAL_SELECTIVIDAD) CANAL, \n" +
           "NVL(CASE WHEN USUARIO.CODIGO_USUARIO IS NULL THEN 'MOTOR MGR' ELSE GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_USUARIO(USUARIO.CODIGO_USUARIO) END,'MOTOR MGR') USUARIO\n" +
           "FROM GRTA_DECLARACIONES_PERFIL PERFIL, GRTA_COMPENDIO_DETALLE COMPENDIO,GRTA_SELECTIVIDAD_ASINCRONA SELECTIVIDAD, GRTA_SESSION USUARIO\n" +
           "WHERE\n" +
           "PERFIL.SUJETO_RIESGO=:pSujetoRiesgo AND\n" +
           "PERFIL.ID_PERFIL=:pDeclaracion AND\n" +
           "COMPENDIO.ID_DETALLE=PERFIL.TIPO_MOMENTO AND\n" +
           "SELECTIVIDAD.IDENTIFICADOR_SUJETO(+)=PERFIL.IDENTIFICADOR_DECLARACION AND\n" +
           "SELECTIVIDAD.CLASE_MEDIDA(+)=PERFIL.CLASE_MEDIDA AND\n" +
           "USUARIO.ID_SESSION(+)=SELECTIVIDAD.ID_SESSION\n" +
           "ORDER BY 1";


        public const String PerfilItemsContenedores = " SELECT ROWNUM \"Nro\",X.NUMERO_CONTENEDOR \"Contenedor\" FROM\n" +
                                    " (\n" +
                                    " SELECT DISTINCT CONTENEDOR.NUMERO_CONTENEDOR \n" +
                                    " FROM GRTA_DECLARACIONES_PERFIL PERFIL,GRTA_DECLARACIONES_LINEA LINEA,\n" +
                                    " GRTA_DECLARACIONES_CONTENEDOR CONTENEDOR\n" +
                                    " WHERE\n" +
                                    " PERFIL.ID_PERFIL=:pDeclaracion AND \n" +
                                    " PERFIL.INDICADOR_ACTIVO=1 AND\n" +
                                    " (PERFIL.CLASE_MEDIDA=:pClaseMedida OR :pClaseMedida IS NULL) AND\n" +
                                    " PERFIL.SUJETO_RIESGO=:pSujetoRiesgo AND\n" +
                                    " LINEA.ID_PERFIL=PERFIL.ID_PERFIL AND\n" +
                                    " CONTENEDOR.DECLARACION_LINEA=LINEA.DECLARACION_LINEA\n" +
                                    " ) X";

        //Función Convertida: GRFN_NOMBRE_CODIGO_VARIABLE
        public const String PerfilItemsPerformance = "SELECT X.CODIGO_VARIABLE \"Código Variable\", CASE WHEN X.CODIGO_VARIABLE IS NULL THEN 'Indefinido' ELSE UPPER(VAR.DESCRIPCION_BREVE) END \"Descripción Variable\",\n" +
                                   "X.VALOR \"Contenido\",\n" +
                                   "DECODE(X.VALOR,'Varios Valores',' ',NVL(GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_CATALOGO(X.CODIGO_VARIABLE,X.VALOR),' ')) \"Descripción Contenido\",\n" +
                                   "TO_CHAR(X.TIEMPO,'9,999,9990.999') \"Tiempo Total (seg)\"\n" +
                                   "FROM\n" +
                                   "(\n" +
                                   "select VARIABLES.CODIGO_VARIABLE,(CASE WHEN VARIABLES.IDENTIFICADOR_LINEA IS NOT NULL THEN NVL(VARIABLES.VALOR,'SIN VALOR') WHEN (VARIABLES.IDENTIFICADOR_LINEA IS NULL AND VARIABLES.VALOR IS NOT NULL) THEN NVL(VARIABLES.VALOR,'SIN VALOR')\n" +
                                   " ELSE 'Varios Valores' END) VALOR,\n" +
                                   "SUM(TO_NUMBER(TO_CHAR(VARIABLES.FECHA_TERMINO,'sssss.ff5'),'99999.99999')-TO_NUMBER(TO_CHAR(VARIABLES.FECHA_INICIO,'sssss.ff5'),'99999.99999')) TIEMPO \n" +
                                   "from GRTA_DECLARACIONES_PERFIL PERFIL, GRTA_VALOR_VARIABLES VARIABLES\n" +
                                   "where\n" +
                                   "PERFIL.ID_PERFIL=:pDeclaracion AND\n" +
                                   "PERFIL.SUJETO_RIESGO=:pSujetoRiesgo AND\n" +
                                   "PERFIL.INDICADOR_ACTIVO=1 AND\n" +
                                   "(PERFIL.CLASE_MEDIDA=:pClaseMedida OR :pClaseMedida IS NULL) AND\n" +
                                   "VARIABLES.ID_PERFIL=PERFIL.ID_PERFIL\n" +
                                   "GROUP BY VARIABLES.CODIGO_VARIABLE,VARIABLES.IDENTIFICADOR_LINEA,VARIABLES.VALOR\n" +
                                   ") X\n" +
                                   "LEFT JOIN GRTA_VARIABLES VAR ON VAR.CODIGO_VARIABLE=X.CODIGO_VARIABLE\n" +
                                   "ORDER BY 2";


        public const String PerfilMedidas = "SELECT GRPK_OPERACIONES_COMUNES.GRFN_ALTERNO_DETALLE(X.RESULTADO_MEDIDA) \"T.Resultado\",   \n" +
           "       GRPK_OPERACIONES_COMUNES.GRFN_ALTERNO_DETALLE(X.TIPO_MEDIDA) \"T.Medida\",   \n" +
           "        X.ID_MEDIDA||'-'||X.VERSION_MEDIDA \"Medida\", X.NOMBRE_MEDIDA \"Nombre Medida\",   \n" +
           "       NVL((SELECT REPLACE(SUBSTR(GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(RESPUESTA.CANAL_SELECTIVIDAD),1,1),'I','S')   \n" +
           "       FROM GRTA_DECLARACIONES_RESPUESTA RESPUESTA   \n" +
           "       WHERE RESPUESTA.DECLARACION_CONDICION=X.DECLARACION_CONDICION AND RESPUESTA.NIVEL_RESPUESTA=543 AND ROWNUM=1    \n" +
           "       ),' ') \"Canal\",                      \n" +
           "        DECODE(X.CONDICION,NULL,NULL,(SELECT TO_CHAR(ORDEN_CONDICION)||': ' FROM GRTA_CONDICION_MEDIDAS WHERE CONDICION_MEDIDAS=X.CONDICION))||    \n" +
           "       NVL(GRPK_OPERACIONES_COMUNES.GRFN_CONSULTAR_EXPRESIO_FILTRO(X.ID_MEDIDA,X.VERSION_MEDIDA,X.CONDICION,'P'),'EN BLANCO') \"Filtro / Condición\",   \n" +
           "        DECODE(X.CONDICION,NULL,NVL(X.EXPRESION_DETALLADA,' '),NVL(X.EXPRESION_DETALLADA_CONDICION,' ')) \"Detalle Filtro / Condición\",                                     \n" +
           "       NVL(DECODE(X.CONDICION,NULL,(X.EXPRESION_FILTRO||' (No Cumple)'),    \n" +
           "       (X.EXPRESION_FILTRO_CONDICION||' ('||GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(X.TIPO_SALIDA)||'): '||X.PERFIL_RIESGO)),' ') \"Resultado Filtro\",   \n" +
           "       GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(X.JERARQUIA_MEDIDA) \"Jerarquía\"   \n" +
           "       FROM   \n" +
           "       (    \n" +
           "           SELECT Y.RESULTADO_MEDIDA,Y.TIPO_MEDIDA,Y.JERARQUIA_MEDIDA,Y.ID_MEDIDA, Y.VERSION_MEDIDA,Y.NOMBRE_MEDIDA,Y.DECLARACION_MEDIDA,  \n" +
           "           Y.PERFIL_RIESGO,Y.EXPRESION_FILTRO,Y.EXPRESION_DETALLADA,Y.CONDICION,               \n" +
           "           CONDICION.TIPO_SALIDA,CONDICION.DECLARACION_CONDICION,CONDICION.EXPRESION_DETALLADA EXPRESION_DETALLADA_CONDICION, \n" +
           "           CONDICION.EXPRESION_FILTRO EXPRESION_FILTRO_CONDICION    \n" +
           "           FROM \n" +
           "           ( \n" +
           "               SELECT MEDIDA.RESULTADO_MEDIDA,MAESTRO.TIPO_MEDIDA,MAESTRO.JERARQUIA_MEDIDA,MEDIDA.ID_MEDIDA, MEDIDA.VERSION_MEDIDA,MAESTRO.NOMBRE_MEDIDA NOMBRE_MEDIDA,MEDIDA.DECLARACION_MEDIDA,  \n" +
           "               MEDIDA.PERFIL_RIESGO,MEDIDA.EXPRESION_FILTRO,MEDIDA.EXPRESION_DETALLADA, \n" +
           "               (CASE WHEN MEDIDA.RESULTADO_MEDIDA=140 THEN NULL \n" +
           "                WHEN MEDIDA.PERFIL_RIESGO IS NOT NULL \n" +
           "                THEN (SELECT CONDICION.CONDICION_MEDIDAS FROM GRTA_DECLARACIONES_CONDICION CONDICION WHERE CONDICION.DECLARACION_MEDIDA=MEDIDA.DECLARACION_MEDIDA AND CONDICION.CONDICION_APLICADA=1)\n" +
           "                ELSE (SELECT MAX(CONDICION.CONDICION_MEDIDAS) FROM GRTA_DECLARACIONES_CONDICION CONDICION WHERE CONDICION.DECLARACION_MEDIDA=MEDIDA.DECLARACION_MEDIDA) END \n" +
           "               ) CONDICION  \n" +
           "               FROM GRTA_DECLARACIONES_MEDIDA MEDIDA, GRTA_MEDIDAS MAESTRO    \n" +
           "               WHERE   \n" +
           "               MEDIDA.DECLARACION_LINEA=:pDeclaracionLinea AND    \n" +
           "               MAESTRO.ID_MEDIDA=MEDIDA.ID_MEDIDA AND   \n" +
           "               MAESTRO.VERSION_MEDIDA=MEDIDA.VERSION_MEDIDA \n" +
           "           ) Y, GRTA_DECLARACIONES_CONDICION CONDICION  \n" +
           "           WHERE \n" +
           "           CONDICION.DECLARACION_MEDIDA(+)=Y.DECLARACION_MEDIDA AND \n" +
           "           CONDICION.CONDICION_MEDIDAS(+)=Y.CONDICION   \n" +
           "       ) X   \n" +
           "       ORDER BY 1,5 DESC";


        public const String PerfilMedidasFiscalizacion = "SELECT GRPK_OPERACIONES_COMUNES.GRFN_ALTERNO_DETALLE(X.RESULTADO_MEDIDA) \"T.Resultado\",  \n" +
           "     GRPK_OPERACIONES_COMUNES.GRFN_ALTERNO_DETALLE(X.TIPO_MEDIDA) \"T.Medida\",   \n" +
           "      X.ID_MEDIDA||'-'||X.VERSION_MEDIDA \"Medida\", X.NOMBRE_MEDIDA \"Nombre Medida\",                      \n" +
           "     DECODE(X.CONDICION,NULL,NULL,(SELECT TO_CHAR(ORDEN_CONDICION)||': ' FROM GRTA_CONDICION_MEDIDAS WHERE CONDICION_MEDIDAS=X.CONDICION))||    \n" +
           "     NVL(GRPK_OPERACIONES_COMUNES.GRFN_CONSULTAR_EXPRESIO_FILTRO(X.ID_MEDIDA,X.VERSION_MEDIDA,X.CONDICION,'P'),'EN BLANCO') \"Filtro / Condición\",   \n" +
           "      DECODE(X.CONDICION,NULL,NVL(X.EXPRESION_DETALLADA,' '),NVL(X.EXPRESION_DETALLADA_CONDICION,' ')) \"Detalle Filtro / Condición\",                                     \n" +
           "     NVL(DECODE(X.CONDICION,NULL,(X.EXPRESION_FILTRO||' (No Cumple)'),    \n" +
           "     (X.EXPRESION_FILTRO_CONDICION||' ('||GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(X.TIPO_SALIDA)||') ')),' ') \"Resultado Filtro\",\n" +
           "     X.VALOR_DECLARADO \"Valor Declarado\", X.PERFIL_RIESGO \"Valor Estimado\", \n" +
           "     GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(X.JERARQUIA_MEDIDA) \"Jerarquía\"   \n" +
           "     FROM   \n" +
           "     (    \n" +
           "         SELECT Y.RESULTADO_MEDIDA,Y.TIPO_MEDIDA,Y.JERARQUIA_MEDIDA,Y.ID_MEDIDA, Y.VERSION_MEDIDA,Y.NOMBRE_MEDIDA,Y.DECLARACION_MEDIDA,  \n" +
           "         Y.PERFIL_RIESGO,Y.EXPRESION_FILTRO,Y.EXPRESION_DETALLADA,Y.CONDICION,               \n" +
           "         CONDICION.TIPO_SALIDA,CONDICION.DECLARACION_CONDICION,CONDICION.EXPRESION_DETALLADA EXPRESION_DETALLADA_CONDICION, \n" +
           "        CONDICION.EXPRESION_FILTRO EXPRESION_FILTRO_CONDICION, Y.VALOR_DECLARADO    \n" +
           "         FROM \n" +
           "         ( \n" +
           "             SELECT MEDIDA.RESULTADO_MEDIDA,MAESTRO.TIPO_MEDIDA,MAESTRO.JERARQUIA_MEDIDA,MEDIDA.ID_MEDIDA, MEDIDA.VERSION_MEDIDA,MAESTRO.NOMBRE_MEDIDA NOMBRE_MEDIDA,MEDIDA.DECLARACION_MEDIDA,  \n" +
           "             MEDIDA.PERFIL_RIESGO,MEDIDA.EXPRESION_FILTRO,MEDIDA.EXPRESION_DETALLADA, \n" +
           "             (CASE WHEN MEDIDA.RESULTADO_MEDIDA=140 THEN NULL \n" +
           "              WHEN MEDIDA.PERFIL_RIESGO IS NOT NULL \n" +
           "              THEN (SELECT CONDICION.CONDICION_MEDIDAS FROM GRTA_DECLARACIONES_CONDICION CONDICION WHERE CONDICION.DECLARACION_MEDIDA=MEDIDA.DECLARACION_MEDIDA AND CONDICION.CONDICION_APLICADA=1)  \n" +
           "              ELSE (SELECT MAX(CONDICION.CONDICION_MEDIDAS) FROM GRTA_DECLARACIONES_CONDICION CONDICION WHERE CONDICION.DECLARACION_MEDIDA=MEDIDA.DECLARACION_MEDIDA) END \n" +
           "             ) CONDICION,\n" +
           "             (CASE WHEN MEDIDA.RESULTADO_MEDIDA=9296 THEN (\n" +
           "                    SELECT DISTINCT VARIABLES.VALOR \n" +
           "                     FROM GRTA_ENTIDADES_MEDIDAS ENTIDADES, GRTA_VARIABLES MAESTRO, GRTA_VALOR_VARIABLES VARIABLES \n" +
           "                     WHERE \n" +
           "                     ENTIDADES.ID_MEDIDA=MEDIDA.ID_MEDIDA AND\n" +
           "                     ENTIDADES.VERSION_MEDIDA=MEDIDA.VERSION_MEDIDA AND\n" +
           "                     MAESTRO.CODIGO_VARIABLE=ENTIDADES.CODIGO_VARIABLE AND\n" +
           "                     MAESTRO.TIPO_VARIABLE=58 AND\n" +
           "                     VARIABLES.ID_PERFIL=LINEA.ID_PERFIL AND \n" +
           "                     VARIABLES.CODIGO_VARIABLE=MAESTRO.CODIGO_VARIABLE             \n" +
           "             ) \n" +
           "              ELSE NULL END \n" +
           "             ) VALOR_DECLARADO                 \n" +
           "             FROM GRTA_DECLARACIONES_LINEA LINEA, GRTA_DECLARACIONES_MEDIDA MEDIDA, GRTA_MEDIDAS MAESTRO    \n" +
           "             WHERE   \n" +
           "             LINEA.ID_PERFIL=:pPerfil  AND \n" +
           "             MEDIDA.DECLARACION_LINEA=LINEA.DECLARACION_LINEA_NEW AND    \n" +
           "             MAESTRO.ID_MEDIDA=MEDIDA.ID_MEDIDA AND   \n" +
           "             MAESTRO.VERSION_MEDIDA=MEDIDA.VERSION_MEDIDA \n" +
           "         ) Y, GRTA_DECLARACIONES_CONDICION CONDICION  \n" +
           "         WHERE \n" +
           "         CONDICION.DECLARACION_MEDIDA(+)=Y.DECLARACION_MEDIDA AND \n" +
           "         CONDICION.CONDICION_MEDIDAS(+)=Y.CONDICION   \n" +
           "     ) X   \n" +
           "     ORDER BY 1,5 DESC";


        public const String UsuariosIdentificados = "SELECT CODIGO_ALTERNO, NOMBRE DESCRIPCION FROM GRTA_COMPENDIO_DETALLE\n" +
                                   "WHERE\n" +
                                   "ID_COMPENDIO=63 AND INDIVIDUAL_GRUPAL='I' AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE)";
        public const String DetalleAccesosUsuario = "SELECT X.* FROM\n" +
                                   "( \n" +
                                   "SELECT '6. Modificar: '||NOMBRE_TABLA \"Concepto\", NOMBRE_CAMPO \"Concepto Especifico\", \n" +
                                   "REPLACE(CLAVE_REGISTRO,'@','-') \"Id. Registro\", VALOR_ANTIGUO \"Valor Antiguo\", VALOR_NUEVO \"Valor Nuevo\", TO_CHAR(FECHA_INGRESO,'DD/MM/YYYY HH24:MI:SS') \"Fecha\" \n" +
                                   "FROM GRTA_VARIACIONES\n" +
                                   "WHERE\n" +
                                   "ID_SESSION=:pSession\n" +
                                   "UNION\n" +
                                   "SELECT '1. Administrar Política' \"Concepto\" ,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(OPERACIONES.TIPO_OPERACION) \"Concepto Especifico\",\n" +
                                   "TO_CHAR(OPERACIONES.ID_POLITICA) \"Id. Registro\", ' ' \"Valor Antiguo\", ' ' \"Valor Nuevo\", TO_CHAR(OPERACIONES.FECHA_OPERACION,'DD/MM/YYYY HH24:MI:SS') \"Fecha\" \n" +
                                   "FROM GRTA_OPERACIONES_POLITICA OPERACIONES\n" +
                                   "WHERE\n" +
                                   "OPERACIONES.SESSION_OPERACION=:pSession\n" +
                                   "UNION\n" +
                                   "SELECT '2. Administrar Medida' \"Concepto\" ,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(OPERACIONES.TIPO_OPERACION) \"Concepto Especifico\",\n" +
                                   "TO_CHAR(OPERACIONES.ID_MEDIDA)||'-'||TO_CHAR(OPERACIONES.VERSION_MEDIDA) \"Id. Registro\", ' ' \"Valor Antiguo\", ' ' \"Valor Nuevo\", TO_CHAR(OPERACIONES.FECHA_OPERACION,'DD/MM/YYYY HH24:MI:SS') \"Fecha\" \n" +
                                   "FROM GRTA_OPERACIONES_MEDIDAS OPERACIONES\n" +
                                   "WHERE\n" +
                                   "OPERACIONES.SESSION_OPERACION=:pSession\n" +
                                   "UNION\n" +
                                   "SELECT '3. Testear Medidas' \"Concepto\" ,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(OPERACIONES.TIPO_OPERACION) \"Concepto Especifico\",\n" +
                                   "TO_CHAR(OPERACIONES.ID_EVALUACION) \"Id. Registro\", ' ' \"Valor Antiguo\", ' ' \"Valor Nuevo\", TO_CHAR(OPERACIONES.FECHA_OPERACION,'DD/MM/YYYY HH24:MI:SS') \"Fecha\" \n" +
                                   "FROM GRTA_OPERACIONES_EVALUACION OPERACIONES\n" +
                                   "WHERE\n" +
                                   "OPERACIONES.SESSION_OPERACION=:pSession\n" +
                                   "UNION\n" +
                                   "SELECT '4. Ley de Benford' \"Concepto\" ,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(OPERACIONES.TIPO_OPERACION) \"Concepto Especifico\",\n" +
                                   "TO_CHAR(OPERACIONES.ID_BENFORD) \"Id. Registro\", ' ' \"Valor Antiguo\", ' ' \"Valor Nuevo\", TO_CHAR(OPERACIONES.FECHA_OPERACION,'DD/MM/YYYY HH24:MI:SS') \"Fecha\" \n" +
                                   "FROM GRTA_OPERACIONES_BENFORD OPERACIONES\n" +
                                   "WHERE\n" +
                                   "OPERACIONES.SESSION_OPERACION=:pSession\n" +
                                   "UNION\n" +
                                   "SELECT '5. Programas Fiscalización' \"Concepto\" ,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(OPERACIONES.TIPO_OPERACION) \"Concepto Especifico\",\n" +
                                   "TO_CHAR(OPERACIONES.ID_PROGRAMA) \"Id. Registro\", ' ' \"Valor Antiguo\", ' ' \"Valor Nuevo\", TO_CHAR(OPERACIONES.FECHA_OPERACION,'DD/MM/YYYY HH24:MI:SS') \"Fecha\" \n" +
                                   "FROM GRTA_OPERACIONES_PROGRAMA OPERACIONES\n" +
                                   "WHERE\n" +
                                   "OPERACIONES.SESSION_OPERACION=:pSession\n" +
                                   ") X\n" +
                                   "ORDER BY 1,6,2,3";
        /*WMarcia 06-12-2017 usado*/
        public const String TablaExistenciaAgregar = "SELECT ID_COMPENDIO FROM GRTA_COMPENDIO_GENERAL COMPENDIO\n" +
                                     "WHERE COMPENDIO.TIPO_TABLA = ? AND COMPENDIO.NOMBRE = ?\n" +
                                     "AND COMPENDIO.TIPO_CODIFICACION = ?";
        //@Version: 1.0, 07/08/2012; @Descripcion: Comprueba existencia de la tabla compendio Operacion Editar
        public const String TablaExistenciaEditar = "SELECT ID_COMPENDIO FROM GRTA_COMPENDIO_GENERAL COMPENDIO\n" +
                                     "WHERE COMPENDIO.TIPO_TABLA = ? AND COMPENDIO.NOMBRE = ?\n" +
                                     "AND COMPENDIO.TIPO_CODIFICACION = ? AND COMPENDIO.ID_COMPENDIO NOT IN(?)";
        //@Version: 1.0, 07/08/2012; @Descripcion: Comprueba existencia del elemento en la tabla compendio Operacion Agregar
        /*WMarcia 10-12-2017 Usado*/
        public const String ElementoExistenciaAgregar = "SELECT ID_DETALLE FROM GRTA_COMPENDIO_DETALLE DETALLE\n" +
                                         "WHERE DETALLE.NOMBRE = :pNombre AND (DETALLE.CODIGO_ALTERNO = :pCodigoAlterno OR :pCodigoAlterno IS NULL)\n" +
                                         "AND DETALLE.INDIVIDUAL_GRUPAL = :pTipo\n" +
                                         "AND (DETALLE.SUJETO_RIESGO = TO_NUMBER(:pSujetoRiesgo) OR :pSujetoRiesgo IS NULL)\n" +
                                         "AND DETALLE.ID_COMPENDIO = TO_NUMBER(:pIdCompendio)\n" +
                                         "AND SYSDATE BETWEEN DETALLE.FECHA_INICIO_VIGENCIA AND NVL(DETALLE.FECHA_FIN_VIGENCIA, SYSDATE)";
        //@Version: 1.0, 07/08/2012; @Descripcion: Comprueba existencia del elemento en la tabla compendio Operacion Agregar
        /*WMarcia 10-12-2017 Usado*/
        public const String ElementoExistenciaEditar = "SELECT ID_DETALLE FROM GRTA_COMPENDIO_DETALLE DETALLE\n" +
                                              "WHERE DETALLE.NOMBRE = :pNombre AND DETALLE.CODIGO_ALTERNO = :pCodigoAlterno\n" +
                                              "AND DETALLE.INDIVIDUAL_GRUPAL = :pTipo\n" +
                                              "AND DETALLE.ID_DETALLE NOT IN (TO_NUMBER(:pIdDetalle))\n" +
                                              "AND (DETALLE.SUJETO_RIESGO = TO_NUMBER(:pSujetoRiesgo) OR :pSujetoRiesgo IS NULL)\n" +
                                              "AND DETALLE.ID_COMPENDIO = TO_NUMBER(:pIdCompendio)\n" +
                                              "AND SYSDATE BETWEEN DETALLE.FECHA_INICIO_VIGENCIA AND NVL(DETALLE.FECHA_FIN_VIGENCIA, SYSDATE)";
        //@Version: 1.0, 08/08/2012; @Descripcion: Comprueba existencia del subelemento en el elemento grupal Operacion Agregar
        public const String SubElementoExistenciaAgregar = "SELECT ID_SUBDETALLE FROM GRTA_COMPENDIO_SUBDETALLE SUBDETALLE\n" +
                                           "WHERE SUBDETALLE.CODIGO_ALTERNO = ? AND SUBDETALLE.ID_DETALLE_GRUPO = TO_NUMBER(?)" +
                                           "AND SYSDATE BETWEEN SUBDETALLE.FECHA_INICIO_VIGENCIA AND NVL(SUBDETALLE.FECHA_FIN_VIGENCIA, SYSDATE)";
        //@Version: 1.0, 08/08/2012; @Descripcion: Comprueba existencia del subelemento en el elemento grupal Operacion Editar
        public const String SubElementoExistenciaEditar = "SELECT ID_SUBDETALLE FROM GRTA_COMPENDIO_SUBDETALLE SUBDETALLE\n" +
                                          "WHERE SUBDETALLE.CODIGO_ALTERNO = ? AND SUBDETALLE.ID_SUBDETALLE NOT IN(?) AND SUBDETALLE.ID_DETALLE_GRUPO = ?\n" +
                                          "AND SYSDATE BETWEEN SUBDETALLE.FECHA_INICIO_VIGENCIA AND NVL(SUBDETALLE.FECHA_FIN_VIGENCIA, SYSDATE)";

        //@Version: 1.0, 06/08/2012; @Descripcion: Comprueba existencia del sujeto riesgo operacion Agregar
        public const String SujetoRiesgoExistenciaAgregar = "SELECT SUJETO_RIESGO FROM GRTA_SUJETO_RIESGO SUJETO\n" +
                                        "WHERE SUJETO.ORGANO_INSTITUCIONAL = TO_NUMBER(?) AND SUJETO.DESCRIPCION_BREVE = ? AND SUJETO.FASE_CONTROL = TO_NUMBER(?)\n" +
                                        "AND SUJETO.TIPO_SELECCION = TO_NUMBER(?)";
        //@Version: 1.0, 09/08/2012; @Descripcion: Comprueba existencia del sujeto riesgo operacion Editar
        public const String SujetoRiesgoExistenciaEditar = "SELECT SUJETO_RIESGO FROM GRTA_SUJETO_RIESGO SUJETO\n" +
                                       "WHERE SUJETO.ORGANO_INSTITUCIONAL = TO_NUMBER(?) AND SUJETO.DESCRIPCION_BREVE = ? AND SUJETO.FASE_CONTROL = TO_NUMBER(?)\n" +
                                       "AND SUJETO.TIPO_SELECCION = TO_NUMBER(?) AND SUJETO.SUJETO_RIESGO NOT IN(TO_NUMBER(?))";
        //@Version: 1.0, 06/08/2012; @Descripcion: Comprueba existencia del parametro input 62 operacion Agregar
        public const String ParametroExistenciaInput62Agregar = "SELECT ID_PARAMETRO FROM GRTA_PARAMETROS PARAMETRO\n" +
                                            "WHERE UPPER(PARAMETRO.DESCRIPCION_BREVE) = ? AND PARAMETRO.TIPO_DATO = TO_NUMBER(?) AND PARAMETRO.CLASE_PARAMETRO = TO_NUMBER(?)\n" +
                                            "AND PARAMETRO.SUJETO_RIESGO = TO_NUMBER(?) AND PARAMETRO.VALOR_SUMINISTRADO = ?";
        //@Version: 1.0, 09/08/2012; @Descripcion: Comprueba existencia del parametro input 62 operacion Editar
        public const String ParametroExistenciaInput62Editar = "SELECT ID_PARAMETRO FROM GRTA_PARAMETROS PARAMETRO\n" +
                                           "WHERE UPPER(PARAMETRO.DESCRIPCION_BREVE) = ? AND PARAMETRO.TIPO_DATO = TO_NUMBER(?) AND PARAMETRO.CLASE_PARAMETRO = TO_NUMBER(?)\n" +
                                           "AND PARAMETRO.SUJETO_RIESGO = TO_NUMBER(?) AND PARAMETRO.VALOR_SUMINISTRADO = ? AND PARAMETRO.ID_PARAMETRO NOT IN(TO_NUMBER(?))";
        //@Version: 1.0, 06/08/2012; @Descripcion: Comprueba existencia del parametro input 63 operacion Agregar
        public const String ParametroExistenciaInput63Agregar = "SELECT ID_PARAMETRO FROM GRTA_PARAMETROS PARAMETRO\n" +
                                            "WHERE UPPER(PARAMETRO.DESCRIPCION_BREVE) = ? AND PARAMETRO.TIPO_DATO = TO_NUMBER(?) AND PARAMETRO.CLASE_PARAMETRO = TO_NUMBER(?)\n" +
                                            "AND PARAMETRO.SUJETO_RIESGO = TO_NUMBER(?) AND PARAMETRO.VALOR_CODIGO_VARIABLE = TO_NUMBER(?)";
        //@Version: 1.0, 06/08/2012; @Descripcion: Comprueba existencia del parametro input 63 operacion Editar
        public const String ParametroExistenciaInput63Editar = "SELECT ID_PARAMETRO FROM GRTA_PARAMETROS PARAMETRO\n" +
                                           "WHERE UPPER(PARAMETRO.DESCRIPCION_BREVE) = ? AND PARAMETRO.TIPO_DATO = TO_NUMBER(?) AND PARAMETRO.CLASE_PARAMETRO = TO_NUMBER(?)\n" +
                                           "AND PARAMETRO.SUJETO_RIESGO = TO_NUMBER(?) AND PARAMETRO.VALOR_CODIGO_VARIABLE = TO_NUMBER(?) AND PARAMETRO.ID_PARAMETRO NOT IN(TO_NUMBER(?))";
        //@Version: 1.0, 06/08/2012; @Descripcion: Comprueba existencia del parametro input 64 operacion Agregar
        public const String ParametroExistenciaInput64Agregar = "SELECT ID_PARAMETRO FROM GRTA_PARAMETROS PARAMETRO\n" +
                                            "WHERE UPPER(PARAMETRO.DESCRIPCION_BREVE) = ? AND PARAMETRO.TIPO_DATO = TO_NUMBER(?)\n" +
                                            "AND PARAMETRO.CLASE_PARAMETRO = TO_NUMBER(?) AND PARAMETRO.SUJETO_RIESGO = TO_NUMBER(?)\n" +
                                            "AND PARAMETRO.VALOR_IDENTIFICACION_SUJETO = TO_NUMBER(?)";
        //@Version: 1.0, 06/08/2012; @Descripcion: Comprueba existencia del parametro input 64 operacion Agregar
        public const String ParametroExistenciaInput64Editar = "SELECT ID_PARAMETRO FROM GRTA_PARAMETROS PARAMETRO\n" +
                                          "WHERE UPPER(PARAMETRO.DESCRIPCION_BREVE) = ? AND PARAMETRO.TIPO_DATO = TO_NUMBER(?)\n" +
                                          "AND PARAMETRO.CLASE_PARAMETRO = TO_NUMBER(?) AND PARAMETRO.SUJETO_RIESGO = TO_NUMBER(?)\n" +
                                          "AND PARAMETRO.VALOR_IDENTIFICACION_SUJETO = TO_NUMBER(?) AND PARAMETRO.ID_PARAMETRO NOT IN(TO_NUMBER(?))";
        //@Version: 1.0, 06/08/2012; @Descripcion: Comprueba existencia de la fuente de datos operacion Agregar
        public const String FuenteDatosExistenciaAgregar = "SELECT TABLAS_SUJETO FROM GRTA_TABLAS_SUJETO SUJETO\n" +
                                           "WHERE SUJETO.SUJETO_RIESGO = TO_NUMBER(:pSujetoRiesgo) AND SUJETO.ORIGEN_FUENTE = TO_NUMBER(:pOrigenFuente)\n" +
                                           "AND SUJETO.TIPO_TABLA = TO_NUMBER(:pTipoTabla) AND SUJETO.TABLA_DATOS = TO_NUMBER(:pTablaFuenteHija)\n" +
                                           "AND SYSDATE BETWEEN SUJETO.FECHA_INICIO_VIGENCIA AND NVL(SUJETO.FECHA_FIN_VIGENCIA, SYSDATE)";
        //@Version: 1.0, 09/08/2012; @Descripcion: Comprueba existencia de la fuente de datos operacion Editar
        public const String FuenteDatosExistenciaEditar = "SELECT TABLAS_SUJETO FROM GRTA_TABLAS_SUJETO SUJETO \n" +
                                          "WHERE SUJETO.SUJETO_RIESGO = TO_NUMBER(:pSujetoRiesgo) AND SUJETO.ORIGEN_FUENTE = TO_NUMBER(:pOrigenFuente)\n" +
                                          "AND SUJETO.TIPO_TABLA = TO_NUMBER(:pTipoTabla) AND SUJETO.TABLA_DATOS = TO_NUMBER(:pTablaFuenteHija)\n" +
                                          "AND SUJETO.TABLAS_SUJETO NOT IN(TO_NUMBER(:pIdTablaAnalisis))\n" +
                                          "AND SYSDATE BETWEEN SUJETO.FECHA_INICIO_VIGENCIA AND NVL(SUJETO.FECHA_FIN_VIGENCIA, SYSDATE)";


        public const String ConsultarOperacionesTesteo = "SELECT  ROWNUM \"#\", x.operacion \"Operación\", x.fecha_operacion \"Fecha y Hora\", x.codigo_usuario||' - '||x.usuario \"Usuario\", x.comentario \"Comentario\" " +
                                               " FROM ( SELECT" +
                                               " (SELECT T.DESCRIPCION FROM  GRTA_COMPENDIO_DETALLE t WHERE T.ID_DETALLE = O.TIPO_OPERACION AND T.ID_COMPENDIO = 27 ) operacion,\n" +
                                               " TO_CHAR(O.FECHA_OPERACION, 'DD/MM/YYYY HH12:MI:SS AM') fecha_operacion,\n" +
                                               " S.CODIGO_USUARIO, (SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=63 AND CODIGO_ALTERNO=S.CODIGO_USUARIO AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)) usuario," +
                                               " NVL(O.COMENTARIO, 'SIN COMENTARIOS') COMENTARIO FROM GRTA_OPERACIONES_EVALUACION o, GRTA_SESSION s" +
                                               " WHERE O.SESSION_OPERACION = S.ID_SESSION AND O.ID_EVALUACION= :pTesteo" +
                                               " ORDER BY O.OPERACIONES_EVALUACION ASC" +
                                               " ) x";

        //@Autor: WMarcia; @Version: 1.0, 06/07/2010; @Descripcion: Recupera la consulta de los roles del usuario
        public const String RolUsuario = "SELECT ROL_GEN.ID_FILA, ROL_GEN.IDROL, ROL_GEN.DESCRIPCION, ROL_GEN.OBSERVACION, ROL_GEN.ESTADO" +
                             " FROM(SELECT ROL.IDROL ID_FILA, ROWNUM NUMERO_FILA, ROL.IDROL, ROL.DESCRIPCION, ROL.OBSERVACION, DECODE(ROLUSER.BORRADO,1,'Borrado',0,'Activo') ESTADO" +
                             " FROM SEG_ROLES ROL, SEG_ROLUSER ROLUSER" +
                             " WHERE ROL.IDROL = ROLUSER.IDROL" +
                             " AND ROL.IDAPLICACION = 13" +
                             " AND ROL.ESTADO = 1" +
                             " AND ROLUSER.IDDGA = :pIdDga) ROL_GEN";

        //@Autor: WMarcia; @Version: 1.0, 09/07/2010; @Descripcion: Recupera la consulta de la informacion del empleado
        public const String InfoEmpleado = "SELECT DET.NOMBRE nombreempleado, DET.CODIGO_ALTERNO" +
               " FROM GRTA_COMPENDIO_DETALLE DET" +
               " WHERE DET.ID_COMPENDIO=63 AND SYSDATE BETWEEN DET.FECHA_INICIO_VIGENCIA AND NVL(DET.FECHA_FIN_VIGENCIA,SYSDATE) AND DET.CODIGO_ALTERNO = ?";

        //@Autor: WMarcia; @Version: 1.0, 06/08/2010; @Descripcion: Recupera el detalle del usaurio seleccionada
        public const String UsuarioDetalle = "SELECT USUARIO.iddga," +
               " DET.CODIGO_ALTERNO codigoempleado," +
               " DET.CODIGO_ALTERNO nrocedula," +
               " DET.NOMBRE nombreempleado," +
               " USUARIO.usuario," +
               " TO_CHAR(USUARIO.vigenciaclave, 'dd/mm/yyyy') vigenciaclave," +
               " NVL(USUARIO.email, ' ') email," +
               " USUARIO.borrado estado" +
               " FROM SEG_USUARIOS USUARIO," +
               " GRTA_COMPENDIO_DETALLE DET" +
               " WHERE USUARIO.iddga = ?" +
               " AND USUARIO.IDDGA = DET.CODIGO_ALTERNO AND DET.ID_COMPENDIO=63 AND SYSDATE BETWEEN DET.FECHA_INICIO_VIGENCIA AND NVL(DET.FECHA_FIN_VIGENCIA,SYSDATE)";

        //@Autor: WMarcia; @Version: 1.0, 16/09/2010; @Descripcion: Recupera si el rol existe
        public const String RolUsuarioUnit = "SELECT ROLUSER.BORRADO FROM SEG_ROLES ROL, SEG_ROLUSER ROLUSER " +
                    "WHERE ROL.IDROL = ROLUSER.IDROL AND ROL.IDAPLICACION = 13 AND ROLUSER.IDROL = ? AND ROLUSER.IDDGA = ?";

        //@Autor: WMarcia; @Version: 1.0, 06/08/2010; @Descripcion: Recupera el estado del usuario 
        public const String EstadoUsuario = "SELECT borrado FROM SEG_USUARIOS WHERE iddga = ?";

        //@Autor: WMarcia; @Version: 1.0, 29/06/2010; @Descripcion: Recupera la consulta de los usuarios de MGR
        public const String UsuarioTabla = "SELECT USEER.IDDGA \"Usuario\"," +
               " DET.NOMBRE \"Nombres y Apellidos\"," +
               " COUNT(ROLUSER.idrol) \"Rol\"," +
               " DECODE(USEER.BORRADO,1,'Borrado',0,'Activo') \"Estado\"," +
               " '' \"Operaciones\"" +
               " FROM SEG_USUARIOS USEER," +
               " GRTA_COMPENDIO_DETALLE DET," +
               " SEG_ROLUSER ROLUSER," +
               " SEG_ROLES ROL" +
               " WHERE DET.ID_COMPENDIO=63 AND SYSDATE BETWEEN DET.FECHA_INICIO_VIGENCIA AND NVL(DET.FECHA_FIN_VIGENCIA,SYSDATE) AND " +
               " USEER.IDDGA = DET.CODIGO_ALTERNO AND USEER.IDDGA = ROLUSER.IDDGA AND ROL.IDROL = ROLUSER.IDROL AND ROL.IDAPLICACION = 13" +
               " AND UPPER(DET.NOMBRE) LIKE '%' || UPPER(NVL(:pNombre, DET.NOMBRE)) || '%'" +
               " AND UPPER(USEER.usuario) LIKE '%' || UPPER(NVL(:pUsuario, USEER.usuario)) || '%'" +
               " AND TRUNC(USEER.vigenciaclave) BETWEEN NVL(TO_DATE(:pDesde,'dd/mm/yyyy'),TRUNC(USEER.vigenciaclave)) AND NVL(TO_DATE(:pHasta,'dd/mm/yyyy'),TRUNC(USEER.vigenciaclave))" +
               " GROUP BY USEER.IDDGA, DET.NOMBRE, USEER.usuario, USEER.VIGENCIACLAVE, USEER.BORRADO" +
               " ORDER BY 1 ASC";

        //@Autor: WMarcia; @Version: 1.0, 23/07/2010; @Descripcion: Recupera la consulta de los roles de MGR
        public const String Rol = "SELECT ROL_GEN.ID_FILA, ROL_GEN.IDROL, ROL_GEN.DESCRIPCION, ROL_GEN.OBSERVACION" +
                      " FROM(SELECT ROL.IDROL ID_FILA, ROWNUM NUMERO_FILA, ROL.IDROL, ROL.DESCRIPCION, ROL.OBSERVACION" +
                      " FROM SEG_ROLES ROL" +
                      " WHERE ROL.IDAPLICACION = 13" +
                      " AND ROL.ESTADO = 1) ROL_GEN";
        //@Autor: WMarcia; @Version: 1.0, 26/07/2010; @Descripcion: Recupera la consulta del total de roles de MGR
        public const String RolCount = "SELECT COUNT(ROL.idrol)" +
               " FROM SEG_ROLES ROL" +
               " WHERE ROL.IDAPLICACION = 13" +
               " AND ROL.estado = 1";

        //@Autor: WMarcia; @Version: 1.0, 23/07/2010; @Descripcion: Recupera la consulta de los roles de MGR
        public const String RolOpcion = "SELECT OPCIONES.idpermiso, OPCIONES.descripcion, OPCIONES.nombre_corto, TO_CHAR(OPCIONES.fecha_creacion, 'dd/mm/yyyy HH12:MI:SS am') fecha_creacion" +
               " FROM SEG_OPCIONESROLL OPCIONESROLL, SEG_OPCIONES OPCIONES" +
               " WHERE OPCIONESROLL.idpermiso = OPCIONES.IDPERMISO" +
               " AND OPCIONESROLL.idroll = ? AND OPCIONES.OPCION_FUNCIONAL=1";

        //@Autor: WMarcia; @Version: 1.0, 10/08/2010; @Descripcion: Recupera la consulta del total de roles del usuario
        public const String RolUsuarioCount = "SELECT COUNT(ROL.IDROL)" +
               " FROM SEG_ROLES ROL, SEG_ROLUSER ROLUSER" +
               " WHERE ROL.IDROL = ROLUSER.IDROL" +
               " AND ROL.IDAPLICACION = 13" +
               " AND ROLUSER.IDDGA = :pIdDga";

        //@author: RTrujillo 11/04/2013. @descripcion: Recupera los valores de la funcion de activacion para medidas del tipo red neuronal.
        public const String FuncionActivacion = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE" +
           " WHERE ID_COMPENDIO=274 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)";

        //@author: RTrujillo 11/04/2013. @descripcion: Variables seleccionables de la medida propabilistica o red neuronal.
        //mvalle Usado
        public const String VariablesCombo = "SELECT VAR.CODIGO_VARIABLE codigo, VAR.DESCRIPCION_BREVE descripcion  \n" +
           "FROM GRTA_VARIABLES VAR   \n" +
           "WHERE  \n" +
           "VAR.SUJETO_RIESGO =TO_NUMBER(?)  AND  \n" +
           "VAR.TIPO_VARIABLE = 57 AND VAR.EXPRESION_CONSOLIDACION IS NOT NULL AND  \n" +
           "SYSDATE BETWEEN VAR.FECHA_INICIO_VIGENCIA AND NVL(VAR.FECHA_FIN_VIGENCIA,SYSDATE) AND  \n" +
           "VAR.TIPO_DATO=79\n" +
           "ORDER BY 2";
        /*Usado en Nueva Versión/ Botón detalle*/
        public const String VariablesMedidaCombo = "SELECT V.CODIGO_VARIABLE codigo, v.DESCRIPCION_BREVE descripcion FROM GRTA_VARIABLES v, GRTA_ENTIDADES_MEDIDAS e WHERE v.CODIGO_VARIABLE = E.CODIGO_VARIABLE" +
           " AND TIPO_VARIABLE=57 AND E.ID_MEDIDA = :pIdMedida AND E.VERSION_MEDIDA = :pVersionMedida AND (E.NEURONA_OCULTA=1 OR E.NEURONA_OCULTA IS NULL)";

        /*Usado en Reporte Estado Medida*/
        public const String CriteriosAgrupacionCombo = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE\n" +
           " ID_COMPENDIO = 285 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE)";


        public const String ConsDescripcionEstadoMedida = "SELECT NVL(GRPK_OPERACIONES_COMUNES.GRFN_ALTERNO_DETALLE(TO_NUMBER(?)), 'SIN DESCRIPCION') estado FROM DUAL";


        public const String ConsDescElementoCompendio = "SELECT NVL(GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(TO_NUMBER(?)), 'SIN DESCRIPCION') descripcion FROM DUAL";


        public const String ConsCriterioAgrupacion = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE (SUJETO_RIESGO = :pSujetoRiesgo OR SUJETO_RIESGO IS NULL) AND\n" +
           "ID_COMPENDIO=287 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE)\n";


        public const String ConsHomogeneidadObsTabla = "SELECT SUBSTR(DETALLE.PERIODO,1,4)||'-'||SUBSTR(DETALLE.PERIODO,5,2) \"Año - Semana\", DETALLE.SIN_INCIDENCIA \"# Sin Incidencia\",\n" +
               "TO_CHAR(TRUNC(DETALLE.SIN_INCIDENCIA*100/DETALLE.TOTAL_FILA,2))||' %' \"% Sin Incidencia\",\n" +
               "DETALLE.CON_INCIDENCIA \"# Con Incidencia\", TO_CHAR(TRUNC(CON_INCIDENCIA*100/DETALLE.TOTAL_FILA,2))||' %' \"% Con Incidencia\",\n" +
               "DETALLE.TOTAL_FILA \"Total\"\n" +
               "FROM GRTA_HOMOGENEIDAD_EST PRINCIPAL, GRTA_TABLA_HOMOGENEIDAD_EST DETALLE\n" +
               "WHERE PRINCIPAL.ID_ANALISIS IN (\n" +
               "SELECT ID_ANALISIS FROM GRTA_ANALISIS_EST WHERE\n" +
               "ID_MEDIDA= :pIdMedida AND VERSION_MEDIDA = :pVersionMedida) " +
               "AND DETALLE.ID_HOMOGENEIDAD=PRINCIPAL.ID_HOMOGENEIDAD AND DETALLE.TABLA_RESIDUOS=21836 ORDER BY 1";


        public const String ConsHomogeneidadResTabla = "SELECT SUBSTR(DETALLE.PERIODO,1,4)||'-'||SUBSTR(DETALLE.PERIODO,5,2) \"Año - Semana\", DETALLE.SIN_INCIDENCIA \"# Sin Incidencia\",\n" +
               "DETALLE.CON_INCIDENCIA \"# Con Incidencia\",DECODE(DETALLE.RESULTADO_ANALISIS,GRPK_CONSTANTES.BIFN_GET_VN_NO_SELECCIONADO,'Eliminado',' ') \"Resultado\"\n" +
               "FROM GRTA_HOMOGENEIDAD_EST PRINCIPAL, GRTA_TABLA_HOMOGENEIDAD_EST DETALLE\n" +
               "WHERE PRINCIPAL.ID_ANALISIS IN (\n" +
               "SELECT ID_ANALISIS FROM GRTA_ANALISIS_EST WHERE ID_MEDIDA = :pIdMedida AND VERSION_MEDIDA = :pVersionMedida ) AND\n" +
               "DETALLE.ID_HOMOGENEIDAD=PRINCIPAL.ID_HOMOGENEIDAD AND DETALLE.TABLA_RESIDUOS = 21840  ORDER BY 1";


        public const String ConsIndependTestFarTabla = "SELECT FARRAR_EST||'@'||ORDEN_FARRAR \"Orden\", NUMERO_VARIABLES \"Nro Variables\", TRUNC(VALOR_DETERMINANTE,3) \"Valor Determinante\", \n" +
               "DECODE(RESULTADO_ANALISIS,GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO,GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(RESULTADO_ANALISIS),' ') \"Resultado\"\n" +
               "FROM GRTA_FARRAR_EST WHERE ID_ANALISIS IN ( SELECT ID_ANALISIS FROM GRTA_ANALISIS_EST WHERE ID_MEDIDA=:pIdMedida AND VERSION_MEDIDA=:pVersionMedida ) ORDER BY ORDEN_FARRAR";


        public const String ConsIndependTestFarNivel2Tabla = "SELECT ROWNUM \"#\", NVL(X.DESCRIPCION_BREVE,' ') \"Nombre Variable\", DECODE(X.ELEMENTOS,0,1,X.ELEMENTOS) \"Total Elementos\",X.CALIFICADORAS \"Nro Var.Calificadoras\",\n" +
               "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(X.INDICADOR_PRESENCIA) \"Tipo Presencia\"\n" +
               "FROM ( SELECT VARIABLES.CODIGO_VARIABLE, VARIABLES.DESCRIPCION_BREVE, (SELECT COUNT(ENTIDADES_EST) FROM GRTA_DOMINIO_VARIABLE_EST WHERE ENTIDADES_EST=FARRAR.ENTIDADES_EST) ELEMENTOS,\n" +
               "(SELECT COUNT(ENTIDADES_EST) FROM GRTA_VARIABLE_CALIFICADORA_EST WHERE ENTIDADES_EST=FARRAR.ENTIDADES_EST) CALIFICADORAS, VARIABLES.INDICADOR_PRESENCIA\n" +
               "FROM GRTA_ENTIDADES_FARRAR_EST FARRAR,GRTA_ENTIDADES_EST EST, GRTA_ENTIDADES_MEDIDAS ENTIDADES, GRTA_VARIABLES VARIABLES WHERE FARRAR.FARRAR_EST = TO_NUMBER(:pFarrar) \n" +
               "AND EST.ENTIDADES_EST=FARRAR.ENTIDADES_EST AND ENTIDADES.ENTIDADES_MEDIDAS=EST.ENTIDADES_MEDIDAS AND VARIABLES.CODIGO_VARIABLE=ENTIDADES.CODIGO_VARIABLE ) X ORDER BY 2";


        public const String ConsEvaluacionSelecTabla = "SELECT EVALUACION.ID_MODELO||'@'||EVALUACION.ORDEN_MODELO \"Orden\", EVALUACION.NUMERO_VARIABLES \"Nro Variables\", \n" +
               "TO_CHAR(EFECTIVIDAD.EFECTIVIDAD_MODELO)||' %' \"% Efectividad\", DECODE(EVALUACION.RESULTADO_GLOBAL,GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO,'Seleccionado',' ') \"Resultado\"\n" +
               "FROM GRTA_EVALUACION_MODELO_EST EVALUACION, GRTA_EFECTIVIDAD_MODELO_EST EFECTIVIDAD WHERE\n" +
               "EVALUACION.ID_ANALISIS IN ( SELECT ID_ANALISIS FROM GRTA_ANALISIS_EST WHERE ID_MEDIDA=:pIdMedida AND VERSION_MEDIDA=:pVersionMedida ) AND EFECTIVIDAD.ID_MODELO=EVALUACION.ID_MODELO AND\n" +
               "EFECTIVIDAD.TIPO_DATA=GRPK_CONSTANTES.BIFN_GET_VN_DATA_ANALISIS AND EFECTIVIDAD.RESULTADO_ANALISIS = GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO\n" +
               "ORDER BY EVALUACION.ORDEN_MODELO ";


        public const String ConsEvaluacionSelecNivel2Tabla = "SELECT ROWNUM \"#\", NVL(GRPK_OPERACIONES_COMUNES.GRFN_NOMBREVAR_ENTIDADES_EST(VARIABLES.ENTIDADES_EST),' ') \"Nombre Variable\", TRUNC(VARIABLES.VALOR_COEFICIENTE,4) \"Coeficiente\",\n" +
               "NVL(TRUNC(VARIABLES.DESVIACION_ESTANDAR,4),0) \"Desviación Estándar\", NVL(TRUNC(VARIABLES.ESTADISTICO_WALD,4),0) \"Estadístico Wald\" FROM GRTA_EVALUACION_MODELO_EST EVALUACION, GRTA_VARIABLE_MODELO_EST\n" +
               "VARIABLES WHERE EVALUACION.ID_MODELO=:pModelo AND VARIABLES.ID_MODELO=EVALUACION.ID_MODELO ORDER BY 3 DESC";


        public const String ConsEntrenamientoRedTabla = "SELECT ORDEN_MODELO \"#\", NUMERO_VARIABLES \"Nro Variables\", NEURONAS_OCULTAS \"Neuronas Ocultas\", TRUNC(ERROR_TEST,3) \"ECM (Data Test)\",\n" +
               "DECODE(RESULTADO_ANALISIS,GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO,'Seleccionado',' ') \"Resultado Parcial\",\n" +
               "DECODE(RESULTADO_GLOBAL,GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO,'Seleccionado',' ') \"Resultado Final\"\n" +
               "FROM GRTA_EVALUACION_MODELO_EST EVALUACION WHERE EVALUACION.ID_ANALISIS IN ( SELECT ID_ANALISIS FROM GRTA_ANALISIS_EST WHERE ID_MEDIDA=:pIdMedida AND VERSION_MEDIDA=:pVersionMedida)";


        public const String ConsRedNeuroEvalSeleccTabla = "SELECT ROWNUM \"#\", EVALUACION.ID_MODELO||'@'||EVALUACION.NUMERO_VARIABLES \"Nro Variables\",EVALUACION.NEURONAS_OCULTAS \"Neuronas Ocultas\", TRUNC(EVALUACION.ERROR_TEST,3) \"ECM (Data Test)\",\n" +
              "EFECTIVIDAD.ANALISIS_OBS0_ESP0 \"Obs(0) - Esp(0)\",EFECTIVIDAD.ANALISIS_OBS0_ESP1 \"Obs(0) - Esp(1)\", EFECTIVIDAD.ANALISIS_OBS1_ESP0 \"Obs(1) - Esp(0)\",EFECTIVIDAD.ANALISIS_OBS1_ESP1 \"Obs(1) - Esp(1)\",\n" +
              "TO_CHAR(EFECTIVIDAD.EFECTIVIDAD_MODELO)||' %' \"% Efectividad\", DECODE(EVALUACION.RESULTADO_GLOBAL,GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO,'Seleccionado',' ') \"Resultado Final\"\n" +
              "FROM GRTA_EVALUACION_MODELO_EST EVALUACION, GRTA_EFECTIVIDAD_MODELO_EST EFECTIVIDAD WHERE EVALUACION.ID_ANALISIS IN (SELECT ID_ANALISIS FROM GRTA_ANALISIS_EST\n" +
              "WHERE ID_MEDIDA=:pIdMedida AND VERSION_MEDIDA=:pVersionMedida ) AND EFECTIVIDAD.ID_MODELO=EVALUACION.ID_MODELO AND\n" +
              "EFECTIVIDAD.RESULTADO_ANALISIS=GRPK_CONSTANTES.BIFN_GET_VN_SELECCIONADO\n" +
              "ORDER BY EVALUACION.ORDEN_MODELO";


        public const String ConsDossierContextMenu = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=289 AND\n" +
              " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE) ORDER BY 2";


        public const String ConsDossierContextMenuGeneral = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=289 AND\n" +
              " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE) AND CODIGO_ALTERNO LIKE '%GEN.%' ORDER BY 2";


        public const String ConsAgrupadoPorCombo = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO = 290 AND SUJETO_RIESGO = :pSujetoRiesgo AND\n" +
           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE) ORDER BY 2\n";


        public const String ConsFiltroCombo = "SELECT CODIGO_ALTERNO, NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=292 AND SUJETO_RIESGO=:pSujetoRiesgo AND\n" +
           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA, SYSDATE) ORDER BY 2\n";


        public const String ConsValoresFiltroCombo = "SELECT GRPK_OPERACIONES_COMUNES.GRFN_ELEMENTOS_CATALOGO(:pCodigoVariable) FROM DUAL";


        public const String ConsCanalSelectContextMenu = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=291 AND\n" +
              "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE) ORDER BY 2\n";


        public const String ConsDossierFiltroIndividualCombo = "SELECT CODIGO_VARIABLE codigo, DESCRIPCION_BREVE descripcion FROM GRTA_VARIABLES WHERE SUJETO_RIESGO=1 AND TIPO_VARIABLE=57 AND \n" +
           " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE) AND TIPO_DATO IN (80,81) AND TABLA_CONSOLIDACION IS NOT NULL\n" +
           " ORDER BY DESCRIPCION_BREVE\n";


        public const String ConsDossierFiltroIndividualComboGeneral = "SELECT CODIGO_VARIABLE codigo, DESCRIPCION_BREVE descripcion FROM GRTA_VARIABLES WHERE SUJETO_RIESGO=1 AND TIPO_VARIABLE=57 AND \n" +
           " SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE) AND TIPO_DATO IN (80,81) AND TABLA_CONSOLIDACION IS NOT NULL AND NUMERO_OCURRENCIAS=55\n" +
           " ORDER BY DESCRIPCION_BREVE\n";
        /*Usado en Pprograma Fiscalizacion/ Consultar Procesos de Selección de Casos*/
        public const String ConsDossierFiltroMultiCombo = "SELECT CODIGO_ALTERNO codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE\n" +
           "WHERE ID_COMPENDIO=292 AND SUJETO_RIESGO=1 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA, SYSDATE) ORDER BY 2\n";


        public const String ConsDossierFiltroMultiComboGeneral = "SELECT DET.CODIGO_ALTERNO codigo, DET.NOMBRE descripcion \n" +
           "FROM GRTA_COMPENDIO_DETALLE DET, GRTA_VARIABLES VAR\n" +
           "WHERE \n" +
           "DET.ID_COMPENDIO=292 AND DET.SUJETO_RIESGO=1 AND \n" +
           "SYSDATE BETWEEN DET.FECHA_INICIO_VIGENCIA AND NVL(DET.FECHA_FIN_VIGENCIA, SYSDATE) AND \n" +
           "VAR.CODIGO_VARIABLE=DET.CODIGO_ALTERNO AND\n" +
           "VAR.NUMERO_OCURRENCIAS=55\n" +
           "ORDER BY 2";


        public const String ConsDossierAgrupadoPorCombo = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=293 AND\n" +
           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA, SYSDATE) ORDER BY 2";


        public const String ConsDossierAgrupadoPorComboGeneral = "SELECT ID_DETALLE codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=293 AND\n" +
           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA, SYSDATE) AND CODIGO_ALTERNO LIKE '%GEN.%' ORDER BY 2";

        /*Usado en Programa Fiscalización/ Consultar Procesos de Selección de Casos*/
        public const String ConsDossierFiltroEspecialCombo = "SELECT CODIGO_ALTERNO codigo, NOMBRE descripcion FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=294 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA, SYSDATE) ORDER BY 2";

        /*Usado en Programa Fiscalización/ Consultar Procesos de Selección de Casos*/
        public const String ConsDossierOperadorMatCombo = "SELECT OPERADOR_MATEMATICO codigo, SIMBOLO_MATEMATICO descripcion FROM GRTA_OPERADOR_MATEMATICO WHERE OPERADOR_MATEMATICO IN (2,3,4,5) ORDER BY 2";


        public const String ConsDossierValoresFiltroMultiCombo = "SELECT GRPK_OPERACIONES_COMUNES.GRFN_ELEMENTOS_CATALOGO(:pCodigoVariable) FROM DUAL";


        public const String ConsDossierMedidasCombo = "SELECT ID_MEDIDA||'-'||VERSION_MEDIDA MEDIDA, ID_MEDIDA||'-'||VERSION_MEDIDA||': '||NOMBRE_MEDIDA FROM GRTA_MEDIDAS WHERE SUJETO_RIESGO=1 AND\n" +
           "(TIPO_MEDIDA=:pTipoMedida OR :pTipoMedida IS NULL) AND ESTADO_MEDIDA=42 ORDER BY 2";



        public const String ConsultaTracking = "SELECT ROWNUM ORDEN,X.EVENTO, X.USUARIO,X.FECHA,X.COMENTARIO\n" +
           "FROM (\n" +
           "    SELECT \n" +
           "    (SELECT PTR.LPTRNAT FROM SFX.PTR\n" +
           "    WHERE PTR.CPTRNAT = EVT.CEVTEVT\n" +
           "    AND PTR.CTPT = 'TYPEVT') EVENTO,\n" +
           "    UPPER((SELECT AGE.CAGE||'-'||AGE.LAGE\n" +
           "    FROM SFX.AGE\n" +
           "    WHERE EVT.CEVTAGR = AGE.CAGE\n" +
           "    AND AGE.DAGEEFF <= EVT.DEVTDATINS\n" +
           "    AND EVT.DEVTDATINS < AGE.DAGEFIN)) USUARIO,\n" +
           "    TO_CHAR(DEVTDATINS,'DD/MM/YYYY HH24:MI') FECHA,\n" +
           "    NVL(UPPER(CEVTCMT),' ') COMENTARIO\n" +
           "    FROM SFX.EVT\n" +
           "    WHERE 1 = 1\n" +
           "    AND EVT.IEVTIDDT IN (SELECT IDDT FROM SFX.DDT WHERE IDDTEXTR=:pDeclaracion)\n" +
           "UNION ALL\n" +
           "    SELECT \n" +
           "    (SELECT PTR.LPTRNAT FROM SFX.PTR\n" +
           "    WHERE PTR.CPTRNAT = FAUCA_DUA_EVT.CEVTEVT\n" +
           "    AND PTR.CTPT = 'TYPEFAU') EVENTO,\n" +
           "    (SELECT AGE.CAGE||'-'||AGE.LAGE\n" +
           "    FROM SFX.AGE\n" +
           "    WHERE FAUCA_DUA_EVT.CEVTAGR = AGE.CAGE\n" +
           "    AND AGE.DAGEEFF <= FAUCA_DUA_EVT.DEVTDATINS\n" +
           "    AND FAUCA_DUA_EVT.DEVTDATINS < AGE.DAGEFIN) USUARIO,\n" +
           "    TO_CHAR(FAUCA_DUA_EVT.DEVTDATINS,'DD/MM/YYYY HH24:MI') FECHA,\n" +
           "    NVL(UPPER(FAUCA_DUA_EVT.CEVTCMT),' ') COMENTARIO\n" +
           "    FROM SFX.FAUCA_DUA_EVT, SFX.FAUCA_DUA_DDT\n" +
           "    WHERE FAUCA_DUA_DDT.IDFAUCA = FAUCA_DUA_EVT.IDFAUCA\n" +
           "    AND FAUCA_DUA_DDT.IDDT IN (SELECT IDDT FROM SFX.DDT WHERE IDDTEXTR=:pDeclaracion)\n" +
           "    ORDER BY 3\n" +
           ") X";


        public const String DeclaracionesItems = "SELECT NUMERO_ITEM \"Item\",CODIGO_ARANCELARIO||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',524,NULL,CODIGO_ARANCELARIO) \"Código Arancelario\",\n" +
           "DESCRIPCION_MERCANCIA \"Descripción Mercancía\",NVL(CANAL_SELECTIVIDAD,'V') \"Canal\",ESTADO_MERCANCIA \"Estado\",\n" +
           "PAIS_ORIGEN||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',508,NULL,PAIS_ORIGEN) \"País Origen\",\n" +
           "PAIS_ADQUISICION||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',508,NULL,PAIS_ADQUISICION) \"País Adquisición\",\n" +
           "CODIGO_VENTAJA||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',522,NULL,CODIGO_VENTAJA) \"Ventaja\",\n" +
           "NVL(NUMERO_VIN,' ') \"Número VIN\",NVL(NUMERO_MOTOR,' ') \"Número Motor\",NVL(NUMERO_CHASIS,' ') \"Número Chasis\",NVL(CILINDRAJE,' ') \"Cilindraje\",NVL(TIPO_COMBUSTIBLE,' ') \"Tipo Combustible\",\n" +
           "TO_CHAR(CANTIDAD_COMERCIAL,'999,999,999,999') \"Cant.Declar\",TO_CHAR(NVL(CANTIDAD_DISPONIBLE,0;'999,999,999,999') \"Cant.Dispo\",\n" +
           "TO_CHAR(PESO_BRUTO,'999,999,999,999') \"Peso Bruto\",TO_CHAR(VALOR_ADUANAS,'999,999,999,999') \"V.Aduanas(USD)\",TO_CHAR(FOB,'999,999,999,999') \"FOB(USD)\",TO_CHAR(PAGADO,'999,999,999,999') \"Pagado(L)\" \n" +
           "FROM GRTA_CONSOLIDADO_DETALLE\n" +
           "WHERE\n" +
           "ID_EXTERNA=:pDeclaracion";


        public const String MovimientoSaldos = "SELECT \n" +
           "OBJ.ID_EXTERNA DUA_SUCESORA,OBJ.CONTRIBUYENTE||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',510,NULL,OBJ.CONTRIBUYENTE) CONTRIBUYENTE,\n" +
           "OBJ.ADUANA_REGISTRO||''-''||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',517,NULL,OBJ.ADUANA_REGISTRO) ADUANA,\n" +
           "OBJ.DESTINACION,  \n" +
           "TO_CHAR(OBJ.FECHA_OFICIALIZACION,'DD/MM/YYYY') FECHA_REGISTRO, \n" +
           "TO_CHAR(OBJ.FECHA_SELECTIVIDAD,'DD/MM/YYYY HH24:MI:SS') FECHA_SELECTIVIDAD, \n" +
           "OBJ.CANAL_SELECTIVIDAD CANAL,\n" +
           "SUBSTR(ESTADO.LPTRNAT,1,25) ESTADO,OBJ_DET.NUMERO_ITEM,\n" +
           "OBJ_DET.CODIGO_ARANCELARIO||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',524,NULL,OBJ_DET.CODIGO_ARANCELARIO) ARANCEL,\n" +
           "OBJ_DET.DESCRIPCION_MERCANCIA,\n" +
           "OBJ_DET.CODIGO_VENTAJA||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',522,NULL,OBJ_DET.CODIGO_VENTAJA) VENTAJA,\n" +
           "TO_CHAR(OBJ_DET.PESO_BRUTO,'999,999,999,999'),TO_CHAR(OBJ_DET.CANTIDAD_COMERCIAL,'999,999,999,999'),\n" +
           "TO_CHAR(OBJ_DET.VALOR_ADUANAS,'999,999,999,999'),TO_CHAR(OBJ_DET.FOB,'999,999,999,999'), TO_CHAR(OBJ_DET.PAGADO,'999,999,999,999')\n" +
           "FROM GRTA_CONSOLIDADO_DETALLE OBJ_DET, GRTA_CONSOLIDADO_GENERAL OBJ, SFX.PTR ESTADO\n" +
           "WHERE\n" +
           "OBJ_DET.DUA_PRECEDENTE=:pDeclaracion AND\n" +
           "OBJ_DET.ITEM_PRECEDENTE=:pNumeroItem AND\n" +
           "OBJ.ID_EXTERNA=OBJ_DET.ID_EXTERNA AND\n" +
           "ESTADO.CTPT='ETADDT' AND \n" +
           "ESTADO.CPTRNAT=OBJ.ESTADO_DUA";


        public const String InformacionVehicular = "SELECT \n" +
           "DET.NUMERO_ITEM, DET.CODIGO_ARANCELARIO||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',524,NULL,DET.CODIGO_ARANCELARIO) ARANCEL,\n" +
           "DET.NUMERO_VIN \"Número VIN\",\n" +
           "DET.NUMERO_MOTOR \"Número Motor\",\n" +
           "DET.NUMERO_CHASIS \"Número Chasis\",\n" +
           "DET.CILINDRAJE \"Cilindraje\",\n" +
           "DET.TIPO_COMBUSTIBLE \"Tipo Combustible\",\n" +
           "DET.DESCRIPCION_MERCANCIA,\n" +
           "DET.PAIS_ORIGEN||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',508,NULL,DET.PAIS_ORIGEN), \n" +
           "DET.PAIS_ADQUISICION||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',508,NULL,DET.PAIS_ADQUISICION),\n" +
           "OTROS.NOMBRE_PROVEEDOR,DET.CODIGO_VENTAJA||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',522,NULL,DET.CODIGO_VENTAJA),\n" +
           "TO_CHAR(DET.FOB,'999,999,999'), TO_CHAR(DET.PAGADO,'999,999,999')\n" +
           "FROM GRTA_CONSOLIDADO_DETALLE DET, GRTA_CONSOLIDADO_OTROS OTROS\n" +
           "WHERE\n" +
           "DET.ID_EXTERNA=:pDeclaracion AND\n" +
           "DET.NUMERO_VIN IS NOT NULL AND\n" +
           "OTROS.ID_EXTERNA=DET.ID_EXTERNA\n" +
           "ORDER BY 1";


        public const String VehiculosSimilares = "SELECT * FROM\n" +
           "(\n" +
           "SELECT DET.NUMERO_VIN VIN, GEN.ID_EXTERNA,GEN.ADUANA_REGISTRO||''-''||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',517,NULL,GEN.ADUANA_REGISTRO) ADUANA,GEN.DESTINACION,\n" +
           "TO_CHAR(GEN.FECHA_AUTORIZACION,'DD/MM/YYYY') FECHA_REGISTRO, TO_CHAR(GEN.FECHA_SELECTIVIDAD,'DD/MM/YYYY HH24:MI:SS') FECHA_SELECTIVIDAD,\n" +
           "GEN.CONTRIBUYENTE||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',510,NULL,GEN.CONTRIBUYENTE) CONTRIBUYENTE,\n" +
           "CASE WHEN GEN.CODIGO_PROVEEDOR IS NOT NULL THEN GEN.CODIGO_PROVEEDOR||'-'||OTROS.NOMBRE_PROVEEDOR ELSE ' ' END PROVEEDOR,\n" +
           "GEN.AGENTE_ADUANAS||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',507,NULL,GEN.AGENTE_ADUANAS) AGENTE_ADUANAS,DET.DESCRIPCION_MERCANCIA, \n" +
           "DET.PAIS_ORIGEN||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',508,NULL,DET.PAIS_ORIGEN) PAIS_ORIGEN, \n" +
           "DET.PAIS_ADQUISICION||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',508,NULL,DET.PAIS_ADQUISICION) PAIS_ADQUISICION,\n" +
           "DET.CODIGO_VENTAJA||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',522,NULL,DET.CODIGO_VENTAJA),\n" +
           "TO_CHAR(DET.FOB,'999,999,999,999') FOB,TO_CHAR(DET.PAGADO,'999,999,999,999') PAGADO,DET.ESTADO_MERCANCIA\n" +
           "FROM GRTA_CONSOLIDADO_DETALLE DET, GRTA_CONSOLIDADO_GENERAL GEN, GRTA_CONSOLIDADO_OTROS OTROS\n" +
           "WHERE\n" +
           "DET.NUMERO_VIN LIKE SUBSTR(:pVin,1,11)||'%' AND\n" +
           "DET.ID_EXTERNA<>:pDeclaracion AND\n" +
           "GEN.ID_EXTERNA=DET.ID_EXTERNA AND\n" +
           "DET.PAGADO>0 AND\n" +
           "OTROS.ID_EXTERNA=GEN.ID_EXTERNA\n" +
           "ORDER BY GEN.FECHA_SELECTIVIDAD DESC\n" +
           ") X\n" +
           "WHERE\n" +
           "ROWNUM<=300";


        public const String HistorialTraspaso = "SELECT TRASPASO.TIPO_TRAMITE,TO_CHAR(TRASPASO.FECHA_TRAMITE,'DD/MM/YYYY') FECHA_TRASPASO,\n" +
           "CASE WHEN TRASPASO.RTN_ACTUAL IS NULL THEN TRASPASO.RTN_CARACTER ELSE TRASPASO.RTN_ACTUAL END RTN,\n" +
           "TRASPASO.NOMBRE_PROPIETARIO CONTRIBUYENTE,\n" +
           "TO_CHAR(TRASPASO.VALOR_VEHICULO,'999,999,999') VALOR,TRASPASO.PLACA_ACTUAL,\n" +
           "NVL(TRASPASO.DECLARACION_CANCELATORIA,' '),NVL((SELECT DESTINACION FROM GRTA_CONSOLIDADO_GENERAL WHERE ID_EXTERNA=:pDeclaracion),' ') \n" +
           "FROM \n" +
           "GRTA_CONSOLIDADO_DETALLE DET, GRTA_CONSOLIDADO_TRASPASO TRASPASO\n" +
           "WHERE \n" +
           "DET.ID_EXTERNA=:pDeclaracion AND\n" +
           "DET.NUMERO_ITEM=:pNumeroItem AND\n" +
           "DET.NUMERO_VIN=:pVin AND\n" +
           "TRASPASO.NUMERO_CHASIS=DET.NUMERO_VIN\n" +
           "ORDER BY 2";


        public const String FlujoDespacho = "SELECT \n" +
           "TRA.TIPO_DOCUMENTO_ORIGEN TIPO_ORIGEN,\n" +
           "CASE WHEN TRA.TIPO_DOCUMENTO_ORIGEN='DUA' THEN TRA.NUMERO_DOCUMENTO_ORIGEN\n" +
           "  ELSE SUBSTR(TRA.NUMERO_DOCUMENTO_ORIGEN,1,instr(TRA.NUMERO_DOCUMENTO_ORIGEN,'@',1,1)-1)\n" +
           "END DOCUMENTO_ORIGEN,\n" +
           "CASE WHEN TRA.TIPO_DOCUMENTO_ORIGEN='DUA' THEN \n" +
           "    (SELECT DESTINACION FROM GRTA_CONSOLIDADO_GENERAL WHERE ID_EXTERNA=TRA.NUMERO_DOCUMENTO_ORIGEN) \n" +
           "  ELSE SUBSTR(TRA.NUMERO_DOCUMENTO_ORIGEN,INSTR(TRA.NUMERO_DOCUMENTO_ORIGEN,'@',1,2)+1,LENGTH(TRA.NUMERO_DOCUMENTO_ORIGEN)-INSTR(TRA.NUMERO_DOCUMENTO_ORIGEN,'@',1,2))\n" +
           "END REGIMEN_ORIGEN,\n" +
           "TO_CHAR(TRA.FECHA_DOCUMENTO_ORIGEN,'DD/MM/YYYY HH24:MI') FECHA_ORIGEN,\n" +
           "CASE WHEN TRA.TIPO_DOCUMENTO_ORIGEN='DUA' THEN\n" +
           "    (SELECT CONTRIBUYENTE||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',510,NULL,CONTRIBUYENTE) FROM GRTA_CONSOLIDADO_GENERAL WHERE ID_EXTERNA=TRA.NUMERO_DOCUMENTO_ORIGEN)\n" +
           "  ELSE \n" +
           "    NVL((SELECT REPLACE(TIT.LTITDSTMAR,'\"','') FROM SFX.TIT TIT WHERE TIT.ITIT= substr(TRA.NUMERO_DOCUMENTO_ORIGEN,instr(TRA.NUMERO_DOCUMENTO_ORIGEN,'@',1,1)+1,instr(TRA.NUMERO_DOCUMENTO_ORIGEN,'@',1,2)-instr(TRA.NUMERO_DOCUMENTO_ORIGEN,'@',1,1)-1) AND \n" +
           "    TIT.ITITDECPER=SUBSTR(TRA.NUMERO_DOCUMENTO_ORIGEN,1,instr(TRA.NUMERO_DOCUMENTO_ORIGEN,'@',1,1)-1)),'N/D') \n" +
           "END CONTRIBUYENTE_ORIGEN,\n" +
           "'-->',\n" +
           "TRA.TIPO_DOCUMENTO_DESTINO,\n" +
           "CASE WHEN TRA.TIPO_DOCUMENTO_DESTINO='DUA' THEN TRA.NUMERO_DOCUMENTO_DESTINO\n" +
           "    ELSE SUBSTR(TRA.NUMERO_DOCUMENTO_DESTINO,1,instr(TRA.NUMERO_DOCUMENTO_DESTINO,'@',1,1)-1)\n" +
           "END DOCUMENTO_DESTINO,\n" +
           "CASE WHEN TRA.TIPO_DOCUMENTO_DESTINO='DUA' THEN \n" +
           "    (SELECT DESTINACION FROM GRTA_CONSOLIDADO_GENERAL WHERE ID_EXTERNA=TRA.NUMERO_DOCUMENTO_DESTINO) \n" +
           "   ELSE (SELECT TDSODEC FROM SFX.DSO \n" +
           "    WHERE IDSO=SUBSTR(TRA.NUMERO_DOCUMENTO_DESTINO,1,instr(TRA.NUMERO_DOCUMENTO_DESTINO,'@',1,1)-1)) \n" +
           "END REGIMEN_DESTINO,\n" +
           "TO_CHAR(TRA.FECHA_DOCUMENTO_DESTINO,'DD/MM/YYYY HH24:MI') FECHA_DESTINO,\n" +
           "CASE WHEN TRA.TIPO_DOCUMENTO_DESTINO='DUA' THEN\n" +
           "    (SELECT CONTRIBUYENTE||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',510,NULL,CONTRIBUYENTE) FROM GRTA_CONSOLIDADO_GENERAL WHERE ID_EXTERNA=TRA.NUMERO_DOCUMENTO_DESTINO)\n" +
           "  ELSE NVL((SELECT REPLACE(TIT.LTITDSTMAR,'\"','') FROM SFX.TIT TIT WHERE TIT.ITIT= substr(TRA.NUMERO_DOCUMENTO_DESTINO,instr(TRA.NUMERO_DOCUMENTO_DESTINO,'@',1,1)+1,instr(TRA.NUMERO_DOCUMENTO_DESTINO,'@',1,2)-instr(TRA.NUMERO_DOCUMENTO_DESTINO,'@',1,1)-1) AND \n" +
           "    TIT.ITITDECPER=SUBSTR(TRA.NUMERO_DOCUMENTO_DESTINO,1,instr(TRA.NUMERO_DOCUMENTO_DESTINO,'@',1,1)-1)),'N/D')\n" +
           "END CONTRIBUYENTE_DESTINO,\n" +
           "CASE WHEN TRA.TIPO_DOCUMENTO_DESTINO='DUA' THEN \n" +
           "    (SELECT ADUANA_REGISTRO||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',517,NULL,ADUANA_REGISTRO) FROM GRTA_CONSOLIDADO_GENERAL WHERE ID_EXTERNA=TRA.NUMERO_DOCUMENTO_DESTINO) \n" +
           "   ELSE (SELECT CDSOCODBUR||'-'||GRPK_OPERACIONES_COMUNES.GRFN_DESCRIPCION_MASIVO('DT',517,NULL,CDSOCODBUR) FROM SFX.DSO \n" +
           "    WHERE IDSO=SUBSTR(TRA.NUMERO_DOCUMENTO_DESTINO,1,instr(TRA.NUMERO_DOCUMENTO_DESTINO,'@',1,1)-1)) \n" +
           "END ADUANA_DESTINO,\n" +
           "CASE WHEN TRA.TIPO_DOCUMENTO_DESTINO='DUA' THEN \n" +
           "    NVL((SELECT CANAL_SELECTIVIDAD FROM GRTA_CONSOLIDADO_GENERAL WHERE ID_EXTERNA=TRA.NUMERO_DOCUMENTO_DESTINO),' ') \n" +
           "  ELSE ' ' \n" +
           "END CANAL_DESTINO,\n" +
           "CASE WHEN (TRA.FECHA_DOCUMENTO_DESTINO-TRA.FECHA_DOCUMENTO_ORIGEN)>0 THEN TO_CHAR(TRA.FECHA_DOCUMENTO_DESTINO-TRA.FECHA_DOCUMENTO_ORIGEN,'999,999,990.0') ELSE '('||TO_CHAR(ABS(TRA.FECHA_DOCUMENTO_DESTINO-TRA.FECHA_DOCUMENTO_ORIGEN),'999,999,990.0')||')' END TIEMPO_TRANSCURRIDO\n" +
           "FROM GRTA_CONSOLIDADO_TRACKING TRA\n" +
           "WHERE\n" +
           "TRA.IDENTIFICADOR_UNICO IN\n" +
           "    (SELECT TRA2.IDENTIFICADOR_UNICO FROM GRTA_CONSOLIDADO_TRACKING TRA2 \n" +
           "    WHERE \n" +
           "    TRA2.TIPO_DOCUMENTO_DESTINO='DUA' AND TRA2.NUMERO_DOCUMENTO_DESTINO=:pDeclaracion \n" +
           "    UNION ALL\n" +
           "    SELECT TRA2.IDENTIFICADOR_UNICO FROM GRTA_CONSOLIDADO_TRACKING TRA2 \n" +
           "    WHERE \n" +
           "    TRA2.TIPO_DOCUMENTO_ORIGEN='DUA' AND TRA2.NUMERO_DOCUMENTO_ORIGEN=:pDeclaracion)\n" +
           "ORDER BY TRA.FECHA_DOCUMENTO_ORIGEN,TRA.FECHA_DOCUMENTO_DESTINO";


        public const String SujetoResultadosModelo = "SELECT ROWNUM \"No\",Y.* FROM (SELECT DISTINCT DOCUMENTO.NUMERO_DOCUMENTO \"Identificación\",DECODE(RUC.C_CLASE,'J',RUC.S_1APE_RASOC,RUC.S_2APE_ABREV||' '||RUC.S_NOMBRES) \"Nombre o Razón Social\", \n" +
           "TO_CHAR(DOCUMENTO.VALOR_REAL,'999,999,999,990.9999') \"Valor Real\",\n" +
           "TO_CHAR(DOCUMENTO.VALOR_ESTIMADO,'999,999,999,990.9999') \"Valor Estimado\",\n" +
           "TO_CHAR(DOCUMENTO.VALOR_ESTIMADO-DOCUMENTO.VALOR_REAL,'999,999,999,990.99') \"Diferencia\", \n" +
           "TO_CHAR(TRUNC(DECODE(DOCUMENTO.VALOR_REAL,0,DECODE(DOCUMENTO.VALOR_ESTIMADO,0,0,100),(DOCUMENTO.VALOR_ESTIMADO-DOCUMENTO.VALOR_REAL)*100/DOCUMENTO.VALOR_REAL)),'999,999,990')||'%' \"% Diferencia\" \n" +
           "FROM GRTA_EVALUACION_MODELO_EST EVALUACION, GRTA_DOCUMENTO_EST DOCUMENTO, RC_RUC RUC \n" +
           "WHERE \n" +
           "EVALUACION.ID_ANALISIS IN (SELECT ID_ANALISIS FROM GRTA_ANALISIS_EST WHERE ID_MEDIDA=:pIdMedida AND VERSION_MEDIDA=:pVersionMedida) AND\n" +
           "DOCUMENTO.ID_MODELO=EVALUACION.ID_MODELO AND \n" +
           "RUC.NIT=DOCUMENTO.NUMERO_DOCUMENTO AND DOCUMENTO.VALOR_ESTIMADO>=0 AND DOCUMENTO.PERFIL_RIESGO IS NOT NULL\n" +
           ") Y WHERE ROWNUM<=2000 ORDER BY 5 DESC";


        public const String CasosSeleccionadosTabla22 = "SELECT ROWNUM \"#\",PERFIL.IDENTIFICADOR_DECLARACION \"Identificación\",DECODE(RUC.C_CLASE,'J',RUC.S_1APE_RASOC,RUC.S_NOMBRES||' '||RUC.S_2APE_ABREV) \"Nombre o Razón Social\",\n" +
           "(SELECT D_IMPORTANCIA FROM TB_IMPORTANCIA WHERE C_IMPORTANCIA=RUC.C_IMPORTANCIA) \"Importancia\",\n" +
           "(SELECT D_ADM_TRIB FROM TB_ADM_TRIB WHERE C_ADM_TRIB=RUC.C_ADM_TRIB) \"Adm. Tributaria\",\n" +
           " PERFIL.PERFIL_RIESGO \"Indice\", \n" +
           "GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(PERFIL.GRADO_RIESGO) \"Grado\"\n" +
           "FROM GRTA_DECLARACIONES_PERFIL PERFIL,RC_RUC RUC \n" +
           "WHERE  \n" +
           "PERFIL.SELECCION_CASOS=:pSeleccionCasos AND \n" +
           "PERFIL.INDICADOR_SELECCION=1 AND\n" +
           "RUC.NIT(+)=PERFIL.IDENTIFICADOR_DECLARACION";


        public const String TipoEventoCombo = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE \n" +
           "WHERE \n" +
           "ID_COMPENDIO=275 AND \n" +
           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA, SYSDATE) \n" +
           "ORDER BY 2";

        public const String TipoCategoriaCombo = "SELECT ID_DETALLE, NOMBRE FROM GRTA_COMPENDIO_DETALLE \n" +
           "WHERE \n" +
           "ID_COMPENDIO=276 AND \n" +
           "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA, SYSDATE) \n" +
           "ORDER BY 2";


        public const String MgrElementoFiltroCombo = "SELECT ID_DETALLE, NOMBRE \n" +
         "FROM GRTA_COMPENDIO_DETALLE \n" +
         "WHERE ID_COMPENDIO=327 AND \n" +
         "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE) \n" +
         "ORDER BY ID_DETALLE";


        public const String MgrVisualizacionOperador = "SELECT OPERADOR_MATEMATICO.OPERADOR_MATEMATICO IDENTIFICADOR, OPERADOR_MATEMATICO.SIMBOLO_VISUALIZAR \n" +
             "FROM GRTA_OPERADOR_MATEMATICO OPERADOR_MATEMATICO \n" +
             "WHERE OPERADOR_MATEMATICO.INDICADOR_VISUALIZACION = 1 \n" +
             "ORDER BY OPERADOR_MATEMATICO.ORDEN_VISUALIZACION ASC";


        public static String MgrTipoOperadorCombo = "SELECT ID_DETALLE CODIGO, NOMBRE DESCRIPCION \n" +
             "FROM GRTA_COMPENDIO_DETALLE \n" +
             "WHERE ID_COMPENDIO=16 AND \n" +
             "SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE)";


        public static String MgrFuenteDatosCombo(int sujeto_riesgo)
        {
            return @"SELECT SUJETO.TABLA_DATOS CODIGO, TABLA.NOMBRE||' ('||TABLA.CODIGO_ALTERNO||')' DESCRIPCION \n" +
                "FROM GRTA_TABLAS_SUJETO SUJETO, GRTA_COMPENDIO_DETALLE TABLA \n" +
            "WHERE TABLA.ID_DETALLE = SUJETO.TABLA_DATOS AND \n" +
            "SUJETO.ORIGEN_FUENTE = 504 AND SUJETO.SUJETO_RIESGO = ?";
        }

        public static String MgrVariablesSujetoRiesgoExtraccionCombo(int sujeto_riesgo, int itemElemento)
        {
            return @"SELECT CODIGO_VARIABLE codigo, UPPER(DESCRIPCION_BREVE) descripcion FROM GRTA_VARIABLES WHERE SUJETO_RIESGO = TO_NUMBER(" + sujeto_riesgo + ") AND\n" +
            " TABLA_TRANSACCIONAL = " + itemElemento + " AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL))=0 AND \n" +
            " TIPO_VARIABLE=57 AND MODO_USO=22 AND NUMERO_OCURRENCIAS=55 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' ORDER BY DESCRIPCION_BREVE";
        }
        public static String MgrVariablesSujetoRiesgoExtraccionCombo2(int sujeto_riesgo, int itemElemento)
        {
            return @"SELECT CODIGO_VARIABLE codigo, UPPER(DESCRIPCION_BREVE) descripcion FROM GRTA_VARIABLES WHERE SUJETO_RIESGO = TO_NUMBER(" + sujeto_riesgo + ") AND\n" +
             " TABLA_TRANSACCIONAL = " + itemElemento + " AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL))=0 AND \n" +
             " TIPO_VARIABLE=57 AND MODO_USO=22 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' ORDER BY DESCRIPCION_BREVE" ;
        }
        public static String MgrVariablesSujRiesgoConsolidacion(int sujeto_riesgo, int itemElemento)
        {
            return @"SELECT CODIGO_VARIABLE codigo, UPPER(DESCRIPCION_BREVE) descripcion FROM GRTA_VARIABLES WHERE SUJETO_RIESGO = TO_NUMBER(" + sujeto_riesgo + ") AND\n" +
             " TABLA_TRANSACCIONAL = " + itemElemento + " AND TIPO_VARIABLE =57 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND\n" +
             " GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_CONSOLIDACION))=0 AND EXPRESION_CONSOLIDACION IS NOT NULL ORDER BY DESCRIPCION_BREVE";
        }
        public static String MgrVariablesSujetoRiesgoCombo2(int sujeto_riesgo, int itemElemento)
        {
            return @"SELECT CODIGO_VARIABLE codigo, UPPER(DESCRIPCION_BREVE) descripcion \n" +
            "FROM GRTA_VARIABLES \n" +
            "WHERE TIPO_VARIABLE = 57 AND MODO_USO = 22 AND SUJETO_RIESGO = TO_NUMBER(" + sujeto_riesgo + ") \n" +
            "AND TABLA_TRANSACCIONAL = " + itemElemento + " AND EXPRESION_CONSOLIDACION IS NOT NULL \n" +
            "AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL)) = 0 \n" +
            "AND NUMERO_OCURRENCIAS = 55 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE (FECHA_FIN_VIGENCIA, '', SYSDATE, FECHA_FIN_VIGENCIA) \n" +
            "ORDER BY DESCRIPCION_BREVE";
        }
        public static String MgrVariablesSujetoRiesgoCombo3(int sujeto_riesgo, int itemElemento)
        {
            return @"SELECT CODIGO_VARIABLE codigo, UPPER(DESCRIPCION_BREVE) descripcion \n" +
            "FROM GRTA_VARIABLES \n" +
            "WHERE SUJETO_RIESGO = TO_NUMBER(?) AND TABLA_TRANSACCIONAL = TO_NUMBER(" + itemElemento + " ) AND \n" +
            "TIPO_VARIABLE = 57 AND MODO_USO = 22 AND NUMERO_OCURRENCIAS = 55 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" +
            "ORDER BY DESCRIPCION_BREVE";
        }
        public static String MgrVariablesSujetoRiesgoCombo(int sujeto_riesgo, int itemElemento)
        {
            return @"SELECT CODIGO_VARIABLE codigo, UPPER(DESCRIPCION_BREVE) descripcion \n" +
            "FROM GRTA_VARIABLES \n" +
            "WHERE SUJETO_RIESGO = TO_NUMBER(?) AND TABLA_TRANSACCIONAL = TO_NUMBER(?) AND \n" +
            "TIPO_VARIABLE = 57 AND MODO_USO = 22 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" +
            "ORDER BY DESCRIPCION_BREVE";
        }
        public static String MgrFuenteDatosProcesoEvaluacionCombo(int sujeto_riesgo)
        {
            return @"SELECT SUJETO.TABLA_DATOS CODIGO, TABLA.NOMBRE||' ('||TABLA.CODIGO_ALTERNO||')' DESCRIPCION, \n" +
                    "(SELECT COUNT(CODIGO_VARIABLE) \n" +
                    "FROM GRTA_VARIABLES \n" +
                    "WHERE TIPO_VARIABLE=57 AND MODO_USO=22 AND SUJETO_RIESGO = SUJETO.SUJETO_RIESGO \n" +
                    "AND TABLA_TRANSACCIONAL = SUJETO.TABLA_DATOS AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" +
                    "AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL))=0 \n" +
                    "AND NUMERO_OCURRENCIAS=55 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)) CANTIDAD \n" +
                     "FROM GRTA_TABLAS_SUJETO SUJETO, GRTA_COMPENDIO_DETALLE TABLA \n" +
                     "WHERE TABLA.ID_DETALLE = SUJETO.TABLA_DATOS AND SUJETO.ORIGEN_FUENTE = 504 AND SUJETO.SUJETO_RIESGO =  " + sujeto_riesgo + "";
        }
        public static String MgrFuenteDatosProgramaFizcalizacionCombo(int sujeto_riesgo)
        {
            return @"SELECT SUJETO.TABLA_DATOS CODIGO, TABLA.NOMBRE || ' (' || TABLA.CODIGO_ALTERNO || ')' DESCRIPCION, \n" +

                        "(SELECT COUNT(0) \n" +
                        "FROM GRTA_VARIABLES \n" +
                        "WHERE TIPO_VARIABLE=57 AND MODO_USO=22 AND SUJETO_RIESGO = SUJETO.SUJETO_RIESGO \n" +
                        "AND TABLA_TRANSACCIONAL = SUJETO.TABLA_DATOS AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" +
                        "AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL))=0 \n" +
                        "AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE)) CANTIDAD \n" +
                     "FROM GRTA_TABLAS_SUJETO SUJETO, GRTA_COMPENDIO_DETALLE TABLA \n" +
                     "WHERE TABLA.ID_DETALLE = SUJETO.TABLA_DATOS AND SUJETO.ORIGEN_FUENTE = 504 AND SUJETO.SUJETO_RIESGO =  " + sujeto_riesgo + "";
        }
        public static String MgrFuenteDatosrBenfordCombo(int sujeto_riesgo) {
            return @"SELECT SUJETO.TABLA_DATOS CODIGO, TABLA.NOMBRE||' ('||TABLA.CODIGO_ALTERNO||')' DESCRIPCION, \n" +
                   "(SELECT COUNT(0) \n" +
                   "FROM GRTA_VARIABLES \n" +
                   "WHERE SUJETO_RIESGO = SUJETO.SUJETO_RIESGO  AND TIPO_VARIABLE =57 \n" +
                   "AND TABLA_TRANSACCIONAL = SUJETO.TABLA_DATOS AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_CONSOLIDACION))=0 \n" +
                   "AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_CONSOLIDACION IS NOT NULL) CANTIDAD \n" +
               "FROM GRTA_TABLAS_SUJETO SUJETO, GRTA_COMPENDIO_DETALLE TABLA \n" +
               "WHERE TABLA.ID_DETALLE = SUJETO.TABLA_DATOS AND SUJETO.ORIGEN_FUENTE = 504 AND SUJETO.SUJETO_RIESGO  =  " + sujeto_riesgo + "";
        }


        public static String MgrFuenteDatos1821767Combo(int sujeto_riesgo)
        {
            return @"SELECT SUJETO.TABLA_DATOS CODIGO, TABLA.NOMBRE||' ('||TABLA.CODIGO_ALTERNO||')' DESCRIPCION, \n" +
                  "(SELECT COUNT(0) \n" +
                  "FROM GRTA_VARIABLES \n" +
                  "WHERE TIPO_VARIABLE = 57 AND MODO_USO = 22 AND SUJETO_RIESGO = SUJETO.SUJETO_RIESGO \n" +
                  "AND TABLA_TRANSACCIONAL = SUJETO.TABLA_DATOS AND EXPRESION_CONSOLIDACION IS NOT NULL \n" +
                  "AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL)) = 0 \n" +
                  "AND NUMERO_OCURRENCIAS = 55 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE (FECHA_FIN_VIGENCIA, '', SYSDATE, FECHA_FIN_VIGENCIA)) CANTIDAD \n" +
               "FROM GRTA_TABLAS_SUJETO SUJETO, GRTA_COMPENDIO_DETALLE TABLA \n" +
               "WHERE TABLA.ID_DETALLE = SUJETO.TABLA_DATOS AND SUJETO.ORIGEN_FUENTE = 504 AND SUJETO.SUJETO_RIESGO =  " + sujeto_riesgo + "";
        }





        public static String MgrFuenteDatosLineaDiferenteCeroCombo(int sujeto_riesgo)
        {
            return @"SELECT SUJETO.TABLA_DATOS CODIGO, TABLA.NOMBRE||' ('||TABLA.CODIGO_ALTERNO||')' DESCRIPCION, \n" +
                         "(SELECT COUNT(0) \n" +
                     "FROM GRTA_VARIABLES \n" +
                     "WHERE SUJETO_RIESGO = SUJETO.SUJETO_RIESGO AND TABLA_TRANSACCIONAL = SUJETO.TABLA_DATOS AND \n" +
                     "TIPO_VARIABLE = 57 AND MODO_USO = 22 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%') CANTIDAD \n" +
                      "FROM GRTA_TABLAS_SUJETO SUJETO, GRTA_COMPENDIO_DETALLE TABLA \n" +
                      "WHERE TABLA.ID_DETALLE = SUJETO.TABLA_DATOS AND SUJETO.ORIGEN_FUENTE = 504 AND SUJETO.SUJETO_RIESGO =  " + sujeto_riesgo + "";

        }


        public const String MgrTipoOperadorMatematico = "SELECT OPERADOR_MATEMATICO, TIPO_OPERADOR \n" +
                  "FROM GRTA_OPERADOR_MATEMATICO \n" +
              "WHERE OPERADOR_MATEMATICO = ?";


        public static String MgrGrupoFuncionCombo(int sujeto_riesgo)
        {
            return @"SELECT ID_DETALLE CODIGO, NOMBRE DESCRIPCION \n" +
		 "FROM GRTA_COMPENDIO_DETALLE \n" + 
		 "WHERE ID_COMPENDIO=328 \n" + 
		 "AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE)";
        }
 

  public const String FuncionElementoCombo="SELECT POSICION, SIMBOLO_USUARIO \n" + 
                             "FROM GRTA_FUNCION_ELEMENTO \n" + 
                             "WHERE FUNCION = TO_NUMBER(?) \n" + 
                             "ORDER BY POSICION ASC";


  public const String TipoValorFuncionElementoCombo="SELECT ID_DETALLE CODIGO, NOMBRE DESCRIPCION \n" +
                                      "FROM GRTA_COMPENDIO_DETALLE \n" + 
                                      "WHERE ID_COMPENDIO=329 \n" + 
                                      "AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA, '', SYSDATE)";


  public const String IndicadoresFuncionElemento="SELECT indicador_variable, indicador_suministrado, indicador_parametro, indicador_estructura \n" +
                                   "FROM GRTA_FUNCION_ELEMENTO \n" +
                                   "WHERE FUNCION = TO_NUMBER(?) AND POSICION = TO_NUMBER(?)";


  public const String VariablesSujetoRiesgoExtraccionComboFE="SELECT CODIGO_VARIABLE, UPPER(DESCRIPCION_BREVE) DESCRIPCION_BREVE \n" + 
                                               "FROM GRTA_VARIABLES \n" + 
                                               "WHERE TIPO_VARIABLE=57 AND MODO_USO=22 AND SUJETO_RIESGO = TO_NUMBER(?) \n" + 
                                               "AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" + 
                                               "AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL))=0 \n"+
                                               "AND NUMERO_OCURRENCIAS=55 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) \n" +  
                                               "ORDER BY DESCRIPCION_BREVE";


  public const String VariablesSujetoRiesgoExtraccionCombo2FE="SELECT CODIGO_VARIABLE, UPPER(DESCRIPCION_BREVE) DESCRIPCION_BREVE \n" + 
                                                "FROM GRTA_VARIABLES \n" + 
                                                "WHERE TIPO_VARIABLE=57 AND MODO_USO=22 AND SUJETO_RIESGO = TO_NUMBER(?) \n" + 
                                                "AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" + 
                                                "AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL))=0 \n"+
                                                "AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) \n" + 
                                                "ORDER BY DESCRIPCION_BREVE";


  public const String VariablesSujRiesgoConsolidacionFE="SELECT CODIGO_VARIABLE, UPPER(DESCRIPCION_BREVE) DESCRIPCION_BREVE \n" + 
                                          "FROM GRTA_VARIABLES \n" + 
                                          "WHERE SUJETO_RIESGO = TO_NUMBER(?) AND TIPO_VARIABLE =57 \n" + 
                                          "AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_CONSOLIDACION))=0 \n" + 
                                          "AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_CONSOLIDACION IS NOT NULL ORDER BY DESCRIPCION_BREVE";


  public const String VariablesSujetoRiesgoCombo2FE="SELECT CODIGO_VARIABLE, UPPER(DESCRIPCION_BREVE) DESCRIPCION_BREVE \n" +
                                      "FROM GRTA_VARIABLES \n" +
                                      "WHERE TIPO_VARIABLE = 57 AND MODO_USO = 22 AND SUJETO_RIESGO = TO_NUMBER(?) \n" + 
                                      "AND EXPRESION_CONSOLIDACION IS NOT NULL \n" +
                                      "AND GRPK_OPERACIONES_COMUNES.GRPR_VERIFICA_AGRUPACION(UPPER(EXPRESION_TRANSACCIONAL)) = 0 \n" +
                                      "AND NUMERO_OCURRENCIAS = 55 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE (FECHA_FIN_VIGENCIA, '', SYSDATE, FECHA_FIN_VIGENCIA) \n" + 
                                      "ORDER BY DESCRIPCION_BREVE";


  public const String VariablesSujetoRiesgoCombo3FE="SELECT CODIGO_VARIABLE, UPPER(DESCRIPCION_BREVE) DESCRIPCION_BREVE \n" +
                                      "FROM GRTA_VARIABLES \n" + 
                                      "WHERE SUJETO_RIESGO = TO_NUMBER(?) \n" + 
                                      "AND TIPO_VARIABLE = 57 AND MODO_USO = 22 AND NUMERO_OCURRENCIAS = 55 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" + 
                                      "ORDER BY DESCRIPCION_BREVE";


  public const String VariablesSujetoRiesgoComboFE="SELECT CODIGO_VARIABLE, UPPER(DESCRIPCION_BREVE) DESCRIPCION_BREVE \n" +
                                     "FROM GRTA_VARIABLES \n" +
                                     "WHERE SUJETO_RIESGO = TO_NUMBER(?) \n" + 
                                     "AND TIPO_VARIABLE = 57 AND MODO_USO = 22 AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND DECODE(FECHA_FIN_VIGENCIA,'',SYSDATE) AND EXPRESION_TRANSACCIONAL NOT LIKE '%NO DEFINIDO%' \n" +
                                     "ORDER BY DESCRIPCION_BREVE";
     //mvalle usado en flitro de registrar nueva medida
  public const String MaximoIdFiltroMedida="SELECT NVL(MAX(ID_FILTROS), 0) idFiltros FROM GRTA_TMP_FILTROS_MED_COND WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivo) AND FUNCION_ESTRUCTURA IS NULL AND LINEA_CONDICION IS NULL";

     //mvalle usado en flitro de registrar nueva medida
  public const String MaximoIdFiltroMedidaFuncElemento="SELECT NVL(MAX(ID_FILTROS), 0) idFiltros FROM GRTA_TMP_FILTROS_MED_COND WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivo) AND FUNCION_CONSECUTIVO = TO_NUMBER(:pFuncionConsecutivo) AND FUNCION = TO_NUMBER(:pFuncion) AND FUNCION_ESTRUCTURA = TO_NUMBER(:pFuncionEstructura) AND LINEA_CONDICION IS NULL";

     //mvalle usado en flitro de registrar nueva medida
  public const String MaximoIdFiltroCondMedida="SELECT NVL(MAX(ID_FILTROS), 0) idFiltros FROM GRTA_TMP_FILTROS_MED_COND WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivo) AND FUNCION_ESTRUCTURA IS NULL AND LINEA_CONDICION = TO_NUMBER(:pLineaCondicion)";

     //mvalle usado en flitro de registrar nueva medida
  public const String MaximoIdFiltroCondMedidaFuncElemento="SELECT NVL(MAX(ID_FILTROS), 0) idFiltros FROM GRTA_TMP_FILTROS_MED_COND WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivo) AND LINEA_CONDICION = TO_NUMBER(:pLineaCondicion) AND FUNCION_CONSECUTIVO = TO_NUMBER(:pFuncionConsecutivo) AND FUNCION = TO_NUMBER(:pFuncion) AND FUNCION_ESTRUCTURA = TO_NUMBER(:pFuncionEstructura)";


  public const String MaximoConsecutivoFuncElementoMedida="SELECT NVL(MAX(FUNCION_CONSECUTIVO), 0) + 1 FROM GRTA_TMP_FILTROS_MED_COND WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivo) AND FUNCION = TO_NUMBER(:pFuncion) AND LINEA_CONDICION IS NULL";


  public const String MaximoConsecutivoFuncElementoCondMedida="SELECT NVL(MAX(FUNCION_CONSECUTIVO), 0) + 1 FROM GRTA_TMP_FILTROS_MED_COND WHERE ID_SESSION = TO_NUMBER(:pIdSession) AND CONSECUTIVO_SESSION = TO_NUMBER(:pConsecutivo) AND FUNCION = TO_NUMBER(:pFuncion) AND LINEA_CONDICION = TO_NUMBER(:pLineaCondicion)";


  public const String IdsFuncionFiltro="SELECT ID_FILTROS, FUNCION_CONSECUTIVO, FUNCION, FUNCION_ESTRUCTURA FROM GRTA_TMP_FILTROS_MED_COND WHERE ID_FILTROS = TO_NUMBER(?)";


  public const String OperadorNoNecesitaVariable="SELECT OPERADOR_MATEMATICO FROM GRTA_OPERADOR_MATEMATICO WHERE INDICADOR_SUMINISTRADO = 0 AND INDICADOR_PARAMETRO = 0 AND INDICADOR_LISTA = 0 AND INDICADOR_SIN_VALOR = 0";


    //mvalle medidas
 public const String DatosTipoRespuestaTempListaValores="SELECT SUBCONSULTA_TABLA.* \n" +
            "FROM (SELECT CM.CONDICION_MEDIDAS CONDICION, CM.ID_COMPONENTES COMPONENTE, GRPK_OPERACIONES_COMUNES.GRFN_NOMBRE_ELEMENTO(CM.ORIGEN) ORIGEN, \n" + 
            "CM.COD_VARIABLE, (CASE CM.ORIGEN WHEN 567 THEN (SELECT GEN.NOMBRE FROM GRTA_COMPENDIO_GENERAL GEN WHERE GEN.TIPO_TABLA=1 AND GEN.ID_COMPENDIO = CM.COD_VARIABLE) \n" + 
            "WHEN 568 THEN (SELECT VARIA.DESCRIPCION_BREVE FROM GRTA_VARIABLES VARIA WHERE VARIA.CODIGO_VARIABLE = CM.COD_VARIABLE) END) NOMBRE_VARIABLE, \n" + 
            "CM.VALOR_CADENA \n" + 
            "FROM GRTA_TMP_COMPONENTES_MED CM, GRTA_TMP_CONDICIONES_MEDIDA COND WHERE CM.CONDICION_MEDIDAS = COND.CONDICION_MEDIDAS \n" + 
            "AND CM.TIPO_SALIDA = :pTipoSalida AND COND.ID_SESSION = :pIdSession AND COND.CONSECUTIVO_SESSION = :pConsecutivoSession \n" + 
            "AND COND.LINEA_CONDICION = :pLineaCondicion \n" +
            "ORDER BY CM.ID_COMPONENTES ASC) SUBCONSULTA_TABLA \n";

  //mvalle medidas 
  /*Funciones Reemplazadas*/
  //GRFN_NOMBRE_ELEMENTO , GRFN_NOMBRE_SUBELEMENTO
 public const String DatosTipoRespuestaTempMedicion="SELECT SUBCONSULTA_TABLA.* \n" + 
            "FROM (SELECT CM.CONDICION_MEDIDAS CONDICION, CM.ID_COMPONENTES COMPONENTE, CASE  WHEN CM.UNIDAD_MEDICION IS NULL THEN 'Indefinido' ELSE com_det.nombre END  TIPO_MEDICION, \n" + 
            "NVL((CASE WHEN CM.UNIDAD_MEDICION IS NULL THEN 'Indefinido' ELSE com_sub.descripcion END ), ' ') UNIDAD_MEDICION, CM.VALOR \n" + 
            "FROM GRTA_TMP_COMPONENTES_MED CM INNER JOIN GRTA_TMP_CONDICIONES_MEDIDA COND ON CM.CONDICION_MEDIDAS = COND.CONDICION_MEDIDAS \n"+
            "LEFT JOIN GRTA_COMPENDIO_DETALLE com_det ON com_det.ID_DETALLE=CM.TIPO_MEDICION \n"+
            "LEFT JOIN  GRTA_COMPENDIO_SUBDETALLE  com_sub   ON  com_sub.ID_SUBDETALLE=CM.UNIDAD_MEDICION \n"+
            "WHERE" + 
            "CM.TIPO_SALIDA = :pTipoSalida AND COND.ID_SESSION = :pIdSession AND COND.CONSECUTIVO_SESSION = :pConsecutivoSession \n" + 
            "AND COND.LINEA_CONDICION = :pLineaCondicion \n" + 
            "ORDER BY CM.ID_COMPONENTES ASC) SUBCONSULTA_TABLA \n";   

    //mvalle medidas 
    //Funcion reemplazada GRFN_NOMBRE_ELEMENTO
 public const String DatosTipoRespuestaTempReglasNegocio="SELECT SUBCONSULTA_TABLA.* \n" + 
            "FROM (SELECT CM.CONDICION_MEDIDAS CONDICION, CM.ID_COMPONENTES COMPONENTE,CASE  WHEN CM.TIPO_MEDIDA IS NULL THEN 'Indefinido' ELSE com_det.nombre END  TIPO_MEDIDA, \n" + 
            "CM.VALOR_CADENA \n" + 
            "FROM GRTA_TMP_COMPONENTES_MED CM INNER JOIN GRTA_TMP_CONDICIONES_MEDIDA COND ON CM.CONDICION_MEDIDAS = COND.CONDICION_MEDIDAS \n" + 
            "LEFT JOIN GRTA_COMPENDIO_DETALLE com_det ON com_det.ID_DETALLE=CM.TIPO_MEDIDA \n"+
            "WHERE  CM.TIPO_SALIDA = :pTipoSalida AND COND.ID_SESSION = :pIdSession AND COND.CONSECUTIVO_SESSION = :pConsecutivoSession \n" + 
            "AND COND.LINEA_CONDICION = :pLineaCondicion \n" + 
            "ORDER BY CM.ID_COMPONENTES ASC) SUBCONSULTA_TABLA \n";

    /*Usado WMarcia 11-02-2018*/
 public const String VariableExternaCombo="SELECT CODIGO_VARIABLE codigo, DESCRIPCION_BREVE descripcion \n" + 
    "FROM GRTA_VARIABLES \n" + 
    "WHERE SUJETO_RIESGO=TO_NUMBER(?) AND VARIABLE_CATALOGO IS NOT NULL \n" +
    "AND SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE)";

    //mvalle medidas
 public const String VariableExternaValorCombo="SELECT GRPK_OPERACIONES_COMUNES.GRFN_ELEMENTOS_CATALOGO (CODIGO_VARIABLE) valor \n" + 
    "FROM GRTA_VARIABLES \n" + 
    "WHERE CODIGO_VARIABLE=TO_NUMBER(?)\n";

    /*Usado WMarcia 05-02-2018*/
    //Funcion covertida
    //GRFN_NOMBRE_CODIGO_VARIABLE, GRFN_OPERADOR_USUARIO, GRFN_NOMBRE_PARAMETRO, GRFN_NOMBRE_ELEMENTO
    //TODO Revisar FROM GRTA_FILTROS FILTRO LEFT OUTER JOIN GRTA_FILTROS_MEDIDAS F ON F.ID_FILTROS = FILTRO.ID_FILTROS tenia (+) al final

    /*Usado WMarcia 11-02-2018*/
 public const String UnidadMedicionCombo="SELECT ID_SUBDETALLE codigo, NOMBRE descripcion \n" + 
    		"FROM GRTA_COMPENDIO_SUBDETALLE \n" + 
    		"WHERE ID_DETALLE_GRUPO=TO_NUMBER(?) AND \n" + 
    		"SYSDATE BETWEEN FECHA_INICIO_VIGENCIA AND NVL(FECHA_FIN_VIGENCIA,SYSDATE)";
    /*Usado WMarcia 11-02-2018*/
 public const String MedidaPorTipo="SELECT ID_MEDIDA||'-'||VERSION_MEDIDA codigocadena, ID_MEDIDA||'-'||VERSION_MEDIDA||': '||NOMBRE_MEDIDA descripcion \n" + 
    		"FROM GRTA_MEDIDAS \n" + 
    		"WHERE TIPO_MEDIDA = TO_NUMBER(?) AND SUJETO_RIESGO = TO_NUMBER(?) \n" + 
    		"AND SYSDATE BETWEEN COALESCE(FECHA_INICIO_VIGENCIA, SYSDATE) AND COALESCE(FECHA_FIN_VIGENCIA, SYSDATE) \n" + 
    		"ORDER BY ID_MEDIDA, VERSION_MEDIDA \n";
    /*Usado WMarcia 11-02-2018*/
 public const String ComponenteMedidaTmp="SELECT TIPO_RESPUESTA FROM GRTA_TMP_COMPONENTES_MED comp WHERE COMP.ID_COMPONENTES = TO_NUMBER(?)";
    /*Usado WMarcia 22-02-2018*/
 public const String CategoriaCategoriaVocabularioNegocioCombo="SELECT COMPENDIO.ID_DETALLE codigo,COMPENDIO.NOMBRE descripcion \n"
    		+ "FROM GRTA_VARIABLES VARIABLES, GRTA_CATEGORIA_VARIABLES CATEGORIAS, GRTA_COMPENDIO_DETALLE COMPENDIO \n"
    		+ "WHERE VARIABLES.TIPO_VARIABLE=57 AND VARIABLES.SUJETO_RIESGO=? \n"
    		+ "AND SYSDATE BETWEEN VARIABLES.FECHA_INICIO_VIGENCIA AND COALESCE(VARIABLES.FECHA_FIN_VIGENCIA, SYSDATE) \n"
    		+ "AND CATEGORIAS.CODIGO_VARIABLE=VARIABLES.CODIGO_VARIABLE "
    		+ "AND SYSDATE BETWEEN CATEGORIAS.FECHA_INICIO_VIGENCIA AND COALESCE(CATEGORIAS.FECHA_FIN_VIGENCIA, SYSDATE) \n"
    		+ "AND COMPENDIO.ID_DETALLE=CATEGORIAS.CATEGORIA_DATO \n"
    		+ "GROUP BY COMPENDIO.ID_DETALLE,COMPENDIO.NOMBRE \n"
    		+ "ORDER BY COMPENDIO.NOMBRE";
    /*Usado WMarcia 22-02-2018*/
 public const String CategoriaSimbolosCombo="SELECT OPERADOR.TIPO_OPERADOR codigo, COMPENDIO.NOMBRE descripcion \n"
    		+ "FROM GRTA_OPERADOR_MATEMATICO OPERADOR,GRTA_COMPENDIO_DETALLE COMPENDIO \n"
    		+ "WHERE COMPENDIO.ID_DETALLE=OPERADOR.TIPO_OPERADOR \n"
    		+ "AND SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND COALESCE(COMPENDIO.FECHA_FIN_VIGENCIA, SYSDATE) \n"
    		+ "GROUP BY OPERADOR.TIPO_OPERADOR,COMPENDIO.NOMBRE";
    /*Usado WMarcia 22-02-2018*/
 public const String CategoriaParametrosCombo="SELECT PARAMETROS.CLASE_PARAMETRO codigo, COMPENDIO.NOMBRE descripcion \n"
    		+ "FROM GRTA_PARAMETROS PARAMETROS, GRTA_COMPENDIO_DETALLE COMPENDIO \n"
    		+ "WHERE PARAMETROS.CLASE_PARAMETRO=COMPENDIO.ID_DETALLE \n"
    		+ "AND (PARAMETROS.SUJETO_RIESGO=? OR PARAMETROS.SUJETO_RIESGO IS NULL) \n"
    		+ "AND SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND COALESCE(COMPENDIO.FECHA_FIN_VIGENCIA,SYSDATE) \n"
    		+ "GROUP BY PARAMETROS.CLASE_PARAMETRO,COMPENDIO.NOMBRE\n";
    /*Usado WMarcia 23-02-2018*/
 public const String TipoValorListaConCategoria="SELECT DETALLE.ID_DETALLE codigo, DETALLE.NOMBRE nombre, DETALLE.DESCRIPCION descripcion \n"
    		+ "FROM GRTA_COMPENDIO_GENERAL GENERAL, GRTA_COMPENDIO_DETALLE DETALLE, GRTA_VARIABLES VARIABLES \n"
    		+ "WHERE GENERAL.TIPO_TABLA = 3 AND DETALLE.ID_COMPENDIO = GENERAL.ID_COMPENDIO \n"
    		+ "AND DETALLE.SUJETO_RIESGO = :pSujetoRiesgo \n"
    		+ "AND SYSDATE BETWEEN DETALLE.FECHA_INICIO_VIGENCIA AND COALESCE(DETALLE.FECHA_FIN_VIGENCIA, SYSDATE) \n"
    		+ "AND VARIABLES.CODIGO_VARIABLE = :pVariable \n"
    		+ "AND (DETALLE.VARIABLE_CATALOGO = VARIABLES.VARIABLE_CATALOGO OR  DETALLE.VARIABLE_CATALOGO IS NULL) \n"
    		+ "ORDER BY DETALLE.NOMBRE\n";
    /*Usado WMarcia 23-02-2018*/
 public const String TipoValorParametroConCategoria="SELECT PARAMETROS.CLASE_PARAMETRO codigo_categoria, COMPENDIO.NOMBRE descripcion_categoria, \n"
    		+ "PARAMETROS.ID_PARAMETRO codigo, PARAMETROS.DESCRIPCION_BREVE descripcion \n"
    		+ "FROM GRTA_PARAMETROS PARAMETROS, GRTA_COMPENDIO_DETALLE COMPENDIO \n"
    		+ "WHERE PARAMETROS.CLASE_PARAMETRO=COMPENDIO.ID_DETALLE \n"
    		+ "AND (? IS NULL OR COMPENDIO.ID_DETALLE = ?) \n"
    		+ "AND (PARAMETROS.SUJETO_RIESGO=? OR PARAMETROS.SUJETO_RIESGO IS NULL) \n"
    		+ "AND SYSDATE BETWEEN COMPENDIO.FECHA_INICIO_VIGENCIA AND COALESCE(COMPENDIO.FECHA_FIN_VIGENCIA,SYSDATE)\n";

    /*Usado WMarcia 04-03-2018*/
 public const String MedidaDetalleDao="SELECT M.ID_MEDIDA, M.VERSION_MEDIDA," + 
                    "M.SUJETO_RIESGO id_sujeto_riesgo," + 
                    "(SELECT SR.DESCRIPCION_BREVE FROM GRTA_SUJETO_RIESGO  sr WHERE SR.SUJETO_RIESGO  = M.SUJETO_RIESGO) sujeto_riesgo," + 
                    "M.TIPO_MEDIDA id_tipo_medida," + 
                    "(SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=7 AND REFERENCIA1=1 AND ID_DETALLE = M.TIPO_MEDIDA ) tipo_medida," + 
                    "M.CLASE_MEDIDA id_clase_medida," + 
                    "(SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=2 AND SUJETO_RIESGO= M.SUJETO_RIESGO AND ID_DETALLE = M.CLASE_MEDIDA) clase_medida," + 
                    "M.ID_POLITICA id_politica," + 
                    "(SELECT DESCRIPCION_BREVE FROM GRTA_POLITICA_INSTITUCIONAL WHERE ID_POLITICA = M.ID_POLITICA) politica," + 
                    "M.ESTADO_MEDIDA id_estado_medida," + 
                    "(SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=12 AND REFERENCIA1=1 AND ID_DETALLE = M.ESTADO_MEDIDA) estado_medida," + 
                    "M.JERARQUIA_MEDIDA id_jerarquia_medida," + 
                    "(SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=1 AND ID_DETALLE = M.JERARQUIA_MEDIDA) jerarquia_medida," + 
                    "M.NOMBRE_MEDIDA," + 
                    "M.DESCRIPCION," + 
                    "(SELECT DISTINCT v.CODIGO_VARIABLE FROM GRTA_VARIABLES v, GRTA_ENTIDADES_MEDIDAS e WHERE v.CODIGO_VARIABLE = E.CODIGO_VARIABLE AND TIPO_VARIABLE = 58 AND E.ID_MEDIDA = M.ID_MEDIDA AND E.VERSION_MEDIDA = M.VERSION_MEDIDA ) id_variable_dependiente," + 
                    "(SELECT DISTINCT v.DESCRIPCION_BREVE FROM GRTA_VARIABLES v, GRTA_ENTIDADES_MEDIDAS e WHERE v.CODIGO_VARIABLE = E.CODIGO_VARIABLE AND TIPO_VARIABLE=58 AND E.ID_MEDIDA = M.ID_MEDIDA AND E.VERSION_MEDIDA = M.VERSION_MEDIDA ) variable_dependiente," + 
                    "TO_CHAR(M.FECHA_INICIO_VIGENCIA, 'DD/MM/YYYY') fecha_inicio_vigencia," +
                    "TO_CHAR(M.FECHA_FIN_VIGENCIA, 'DD/MM/YYYY') fecha_fin_vigencia," + 
                    "TO_CHAR( M.FECHA_INICIO_ANALISIS, 'DD/MM/YYYY') fecha_inicio_analisis," +
                    "TO_CHAR(M.FECHA_FIN_ANALISIS, 'DD/MM/YYYY') fecha_fin_analisis," + 
                    "M.VALOR_FRECUENCIA valor_frecuencia," + 
                    "M.TIPO_FRECUENCIA id_tipo_frecuencia," + 
                    "(SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_COMPENDIO=36 AND ID_DETALLE = M.TIPO_FRECUENCIA) tipo_frecuencia," + 
                    "M.RITMO_APRENDIZAJE ritmo_aprendizaje," + 
                    "M.TERMINO_MOMENTO termino_momento," +                      
                    "M.MEDIDA_PRECEDENTE medida_precedente," +
                    "M.VERSION_PRECEDENTE version_precedente," + 
                    "(SELECT PRECEDENTE.NOMBRE_MEDIDA FROM GRTA_MEDIDAS precedente WHERE PRECEDENTE.ID_MEDIDA = M.MEDIDA_PRECEDENTE AND PRECEDENTE.VERSION_MEDIDA = M.VERSION_PRECEDENTE) nombre_precedente," + 
                    "M.FUNCION_ACTIVACION id_funcion_activacion," + 
                    "(SELECT NOMBRE FROM GRTA_COMPENDIO_DETALLE WHERE ID_DETALLE = M.FUNCION_ACTIVACION AND ID_COMPENDIO=274) funcion_activacion,"  +
                    "(SELECT COUNT( C.CONDICION_MEDIDAS) FROM GRTA_CONDICION_MEDIDAS c WHERE C.ID_MEDIDA = M.ID_MEDIDA AND C.VERSION_MEDIDA = M.VERSION_MEDIDA) cantidad_condiciones," + 
                    "(SELECT COUNT( O.ORIENTACION_MEDIDAS) FROM GRTA_ORIENTACION_MEDIDAS o WHERE O.ID_MEDIDA = M.ID_MEDIDA AND O.VERSION_MEDIDA = M.VERSION_MEDIDA) cantidad_banderas," + 
                    "(SELECT COUNT(t.MENSAJE_MEDIDAS) FROM GRTA_MENSAJE_MEDIDAS t WHERE t.ID_MEDIDA = M.ID_MEDIDA AND t.VERSION_MEDIDA = M.VERSION_MEDIDA) cantidad_destinatarios_correo " +
                    "FROM GRTA_MEDIDAS M " + 
                    "WHERE M.ID_MEDIDA = TO_NUMBER(:pIdMedida) " + 
                    "AND M.VERSION_MEDIDA = TO_NUMBER(:pVersionMedida)";
    /*Usado WMarcia 04-03-2018*/
 public const String FiltroDescripcionElementos="SELECT VAR.CODIGO_VARIABLE codigo, VAR.DESCRIPCION_BREVE nombre, VAR.DESCRIPCION_COMPLETA descripcion, -1 indicador, 'V' tipo \n" + 
    		"FROM GRTA_VARIABLES VAR \n" +
    		"WHERE VAR.CODIGO_VARIABLE = :codigo_variable \n" + 
    		"UNION ALL \n" + 
    		"SELECT OPERADOR_MATEMATICO.OPERADOR_MATEMATICO codigo, OPERADOR_MATEMATICO.SIMBOLO_USUARIO, OPERADOR_MATEMATICO.DESCRIPCION descripcion, OPERADOR_MATEMATICO.VISUALIZAR_DESCRIPCION indicador, 'O' tipo \n" + 
    		"FROM GRTA_OPERADOR_MATEMATICO OPERADOR_MATEMATICO \n" + 
    		"WHERE OPERADOR_MATEMATICO.OPERADOR_MATEMATICO = :codigo_operador \n" + 
    		"UNION ALL \n" + 
    		"SELECT COMPENDIO_DETALLE.ID_DETALLE codigo, COMPENDIO_DETALLE.NOMBRE nombre, COMPENDIO_DETALLE.DESCRIPCION descripcion, -1 indicador, 'T' tipo \n" + 
    		"FROM GRTA_COMPENDIO_DETALLE COMPENDIO_DETALLE \n" + 
    		"WHERE COMPENDIO_DETALLE.ID_DETALLE = :tipo_valor \n" + 
    		"UNION ALL \n" + 
    		"SELECT PARAMETRO.ID_PARAMETRO codigo, '' nombre, PARAMETRO.DESCRIPCION_BREVE descripcion, -1 indicador, 'P' tipo \n" + 
    		"FROM GRTA_PARAMETROS PARAMETRO \n" + 
    		"WHERE PARAMETRO.ID_PARAMETRO = :parametro \n" + 
    		"UNION ALL \n" + 
    		"SELECT COMPENDIO_DETALLE.ID_DETALLE codigo, COMPENDIO_DETALLE.NOMBRE nombre, COMPENDIO_DETALLE.DESCRIPCION descripcion, -1 indicador, 'L' \n" + 
    		"FROM GRTA_COMPENDIO_DETALLE COMPENDIO_DETALLE \n" + 
    		"WHERE COMPENDIO_DETALLE.ID_DETALLE = :lista \n"; 
    /*Usado WMarcia 11-03-2018*/
 public const String DescripcionElementoComponente="SELECT COMPENDIO_GENERAL.NOMBRE descripcion \n" + 
    		"FROM GRTA_COMPENDIO_GENERAL COMPENDIO_GENERAL \n" + 
    		"WHERE 'COMPENDIO_GENERAL' = :pElemento \n" + 
    		"AND COMPENDIO_GENERAL.TIPO_TABLA = 1 \n" + 
    		"AND COMPENDIO_GENERAL.ID_COMPENDIO = :pCodigo \n" + 
    		"UNION ALL \n" + 
    		"SELECT COMPENDIO_DETALLE.NOMBRE descripcion \n" + 
    		"FROM GRTA_COMPENDIO_DETALLE COMPENDIO_DETALLE \n" + 
    		"WHERE 'COMPENDIO_DETALLE' = :pElemento \n" + 
    		"AND COMPENDIO_DETALLE.ID_DETALLE = :pCodigo \n" + 
    		"UNION ALL \n" + 
    		"SELECT VARIABLES.DESCRIPCION_BREVE descripcion \n" + 
    		"FROM GRTA_VARIABLES VARIABLES \n" + 
    		"WHERE 'VARIABLE' = :pElemento \n" + 
    		"AND VARIABLES.CODIGO_VARIABLE = :pCodigo \n" + 
    		"UNION ALL \n" + 
    		"SELECT NOMBRE descripcion \n" + 
    		"FROM GRTA_COMPENDIO_SUBDETALLE \n" + 
    		"WHERE 'COMPENDIO_SUBDETALLE' = :pElemento \n" + 
    		"AND ID_SUBDETALLE = :pCodigo \n";
    }
}
