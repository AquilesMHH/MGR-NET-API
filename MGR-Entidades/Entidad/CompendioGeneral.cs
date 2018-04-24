using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class CompendioGeneral
    {
        private static String TABLA = "grta_compendio_general";
        private int id_compendio { get; set; }
        private int session_registro { get; set; }
        private int tipo_tabla { get; set; }
        private int tipo_codificacion { get; set; }
        private String descripcion { get; set; }
        private DateTime fecha_registro { get; set; }
        private String tabla_reservada { get; set; }
        private String nombre { get; set; }
        private String session { get; set; }
    }
}
