using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class FiltroRq
    {
        private int codigo_variable{ get; set; }

        private int operador_matematico{ get; set; }

        private int tipo_valor{ get; set; }

        private String valor_suministrador{ get; set; }

        private int valor_parametro{ get; set; }

        private int valor_lista{ get; set; }

        private int funcion{ get; set; }

        private int funcion_estructura{ get; set; }

        private int funcion_consecutivo{ get; set; }

        private int orden_filtro{ get; set; }

        public FiltroRq()
        {

        }
    }
}
