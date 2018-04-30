using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class OperacionesMedidas
    {
        public static String TABLA = "grta_operaciones_medidas";
        public int OPERACIONES_MEDIDAS { get; set; }
        public int ID_MEDIDA { get; set; }
        public int VERSION_MEDIDA { get; set; }
        public int SESSION_OPERACION { get; set; }
        public int TIPO_OPERACION { get; set; }
        public String COMENTARIO { get; set; }
        public DateTime FECHA_OPERACION { get; set; }
    }
}
