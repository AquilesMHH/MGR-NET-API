using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MGR_Entidades.Entidad
{
   public class OperadorMatematico
    {
        public int OPERADOR_MATEMATICO{ get; set; }
        public int SESION_REGISTRO{ get; set; }
        public int INDICADOR_SIN_VALOR{ get; set; }
        public string SIMBOLO_USUARIO{ get; set; }
        public DateTime FECHA_REGISTRO{ get; set; }
        public int INDICADOR_PARAMETRO{ get; set; }
        public string SIMBOLO_MATEMATICO{ get; set; }
        public string SIMBOLO_VISUALIZAR{ get; set; }
        public int INDICADOR_VISUALIZACION{ get; set; }
        public string SUFIJO_VARIABLE{ get; set; }
        public int ORDEN_VISUALIZACION{ get; set; }
        public int TIPO_OPERADOR{ get; set; }
        public int INDICADOR_LISTA{ get; set; }
        public string PREFIJO_VARIABLE{ get; set; }
        public int INDICADOR_SUMINISTRADO{ get; set; }
      
    }
}
