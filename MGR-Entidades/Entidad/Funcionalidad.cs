using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class Funcionalidad
    {
        public static String TABLA = "grta_funcionalidad";

        private Int32 id_funcionalidad { get; set; }

        private String nombre { get; set; }

        private String descripcion { get; set; }

        private Int32 id_funcionalidad_padre { get; set; }

        private String tipo { get; set; }

        private String mapeoAplicacion { get; set; }

        private String componente { get; set; }

        private String ruta { get; set; }

        private String icono { get; set; }
        private int orden { get; set; }

        private int estado { get; set; }

        private DateTime fecha_registro { get; set; }

        private DateTime fecha_eliminacion { get; set; }

        private DateTime fecha_modificacion { get; set; }

        private List<Funcionalidad> lst_funcionalidad_hijas { get; set; }

        private List<FuncionalidadOperacion> lst_funcionalidad_operacion { get; set; }

    }
}
