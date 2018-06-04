using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class ComponenteRq
    {
        private int canal_selectividad{ get; set; }

        private int valor_respuesta{ get; set; }

        private Decimal valor{ get; set; }

        private int codigo_validacion{ get; set; }

        private int cantidad{ get; set; }

        private int periodo{ get; set; }

        private int origen{ get; set; }

        private int cod_variable{ get; set; }

        private String valor_cadena{ get; set; }

        private int tipo_medida{ get; set; }

        private int tipo_medicion{ get; set; }

        private int unidad_medicion{ get; set; }

       private List<FiltroRq> lstFiltroFormula{ get; set; }

        public ComponenteRq()
        {
        }
 
    }
}
