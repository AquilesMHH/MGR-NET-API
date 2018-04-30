using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Common.com.pe.mgr.common.constants
{
    public class Constantes
    {
        public static int TIPO_MEDIDA_MODELO_PROBABILISTICO = 18;
        public static int TIPO_MEDIDA_RED_NEURONAL = 21767;
        public static int TIPO_MEDIDA_CRITERIO_EXPERTO = 17;
        public static int TIPO_MEDIDA_METODO_EXCEPCION = 19;

        public static String MEDIDA_FUNCIONALIDAD_EVALUACION = "mgrProcesoEvaluacion";
        public static String MEDIDA_FUNCIONALIDAD_FISCALIZACION = "mgrProgramaFizcalizacion";
        public static String MEDIDA_FUNCIONALIDAD_PROC_SELECCION_CASOS = "mgrProcesoSeleccionCasos";
        public static String MEDIDA_FUNCIONALIDAD_BENFORD = "mgrBenford";

        //medidas - CONDICIONES ACCIONES
        public static int MEDIDA_TIPO_RESPUESTA_SELECTIVIDAD_REVISION = 27;
        public static int MEDIDA_TIPO_RESPUESTA_MENSAJE_VALIDACION = 28;
        public static int MEDIDA_TIPO_RESPUESTA_VALOR = 29;
        public static int MEDIDA_TIPO_RESPUESTA_LISTA_VALORES = 572;
        public static int MEDIDA_TIPO_RESPUESTA_FORMULA = 573;
        public static int MEDIDA_TIPO_RESPUESTA_MEDICION = 574;
        public static int MEDIDA_TIPO_RESPUESTA_REGLA_NEGOCIO = 575;

        //Opciones de Administracion - Variables
        public static String TIPO_TABLA_TRANSACCIONAL = "T";
        public static String TIPO_TABLA_CONSOLIDADA = "C";

        public static int CLASE_PARAMETRO_ID_SUJETO_RIESGO = 64;

        public static String MENSAJE_RESPUESTA_POR_DEFECTO = "Ejecutado";

        public static String FORMATO_FECHA_HORA = "MM/DD/YYYY HH24:MI";
    }
}
