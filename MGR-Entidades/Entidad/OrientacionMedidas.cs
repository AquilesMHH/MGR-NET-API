using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class OrientacionMedidas
    {
        public static String TABLA = "grta_orientacion_medidas";

        /**
         * inicio - campos de la base de datos
         */
        public int orientacion_medidas { get; set; }

        public int id_medida { get; set; }

        public int version_medida { get; set; }

        public int condicion_medidas { get; set; }

        public int tipo_salida { get; set; }

        public int tipo_orientacion { get; set; }

        public String descripcion { get; set; }
        
        public int fecha_inicio_vigencia { get; set; }
        
        public DateTime fecha_fin_vigencia { get; set; }
        
        public DateTime fecha_registro { get; set; }

        public int session_registro { get; set; }

        public String desc_tipo_orientacion { get; set; }

        public OrientacionMedidas()
        {
        }
    }
}
