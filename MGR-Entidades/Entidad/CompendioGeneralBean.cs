using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class CompendioGeneralBean
    {
        public   static String TABLA = "grta_compendio_general";
        private int id_compendio { get; set; }
        private String descripcion { get; set; }
        private String fecha_registro { get; set; }
        private String tabla_reservada { get; set; }
        private String nombre { get; set; }
    }
}
