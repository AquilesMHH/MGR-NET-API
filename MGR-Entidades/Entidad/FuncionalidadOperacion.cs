using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class FuncionalidadOperacion  
    {

        public static String TABLA = "grta_funcionalidad_operacion";

        private int id_operacion { get; set; }

        private int id_funcionalidad { get; set; }

        private String nombre { get; set; }

        private String descripcion { get; set; }

        private int activo { get; set; }

        private DateTime fecha_registro { get; set; }

        private DateTime fecha_eliminacion { get; set; }

        private DateTime fecha_modificacion { get; set; }

        private String session { get; set; }


    }
}
