using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Common.com.pe.mgr.common.constants
{
    public class ErrorCodeConstant
    {

        /**
         * Errores que pueden ocurrir al ejecutar querys
         */
        public  static String ESQ_00000 = "ESQ0000";//Error generico que ocurre al ejecutar el service
        public  static String ESQ_00001 = "ESQ0001";//EL query no trajo ningun elemento, cuando deberia traer almenos uno
        public  static String ESQ_00002 = "ESQ0002";//Se eperaba que el query trajera 1 elemento, pero trajo mas de uno
        public  static String ESQ_00003 = "ESQ0003";//El query no actualizó la cantidad de registros esperado.

        public  static String DAO_00000 = "DAO0000";//Error cuando ocurre un error en el DAO.
        public  static String DAO_00001 = "DAO0001";//Error cuando el DAO generar un error en el query de consulta.

        /**
         * Errores que pueden ocurrir al ejecutar un procedure
         */
        public  static String EPR_00000 = "EPR0000";//Error generico que ocurre al ejecutar un procedure

        /**
         * Error de validacion de informacion de objetos recibidos en el request
         */
        public  static String ERO_00000 = "ERO0000";//Error generico que ocurre al ejecutar la validacion de objetos en el request
        public  static String ERO_00001 = "ERO0001";//Se esperaba que la lista tenga almenos un elemento

        /**
         * Errores que pueden ocurrir en el acceso a datos
         */
        public  static String EAD_00000 = "EPR0000";//Error generico en el acceso a datos

        /**
         * Este valor devuelve el procedure cuando no se ejecuta correctamente
         */
        public  static String ERROR_PROCEDURE_OUTPUT = "error";

        public  static String ERROR_VALIDATION_GENERICO = "ESQ0000";

        public  static String ERROR_VALIDATION_SERVICIOS = "ESV0000";

        /**
         * Errores que pueden ocurrir al ejecutar un servicio
         */
        public  static String SERV_PARAMETROS_INCORRECTOS = "SERV0001";//Error cuando el servicio recibe parametros incorrectos
        public  static String SERV_VALIDACION = "SERV0002";//Error cuando hay un  error de validacion
        public  static String SERV_ERROR = "SERV0003";//Error cuando no se ejecuto la transaccion correctamente en el sevicio

    }
}
