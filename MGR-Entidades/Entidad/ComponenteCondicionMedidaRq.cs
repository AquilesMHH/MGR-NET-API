using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class ComponenteCondicionMedidaRq
    {
        public string  tipo_salida { get; set; }
        public int tipo_accion { get; set; }

        public int tipo_respuesta { get; set; }

        public List<ComponenteRq> lstComponenteRq { get; set; }

        public List<OrientacionMedidaRq> lstOrientacionMedidaRq { get; set; }

        public ComponenteCondicionMedidaRq()
        {

        }
 

    }
}
