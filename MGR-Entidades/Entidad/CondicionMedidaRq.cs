using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class CondicionMedidaRq
    {
        public int identificador{ get; set; }

        public int id_medida{ get; set; }

        public int version_medida{ get; set; }

        public int linea{ get; set; }

        public String expresion_filtro{ get; set; }

        public List<FiltroRq> lstFiltroCondicion{ get; set; }

        public ComponenteCondicionMedidaRq compCondicionMedidaSiRq{ get; set; }

        public ComponenteCondicionMedidaRq compCondicionMedidaNoRq{ get; set; }

        public CondicionMedidaRq()
        {
        }
    }
}
