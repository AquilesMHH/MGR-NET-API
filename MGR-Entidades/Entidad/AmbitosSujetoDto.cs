using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class AmbitosSujetoDto : BaseDto
    {
        public int sujeto_riesgo{ get; set; }
        public int tipo_ambito{ get; set; }
        public String tipo_ambito_text{ get; set; }
        public String nombre_campo{ get; set; }
        public String nombre_tabla{ get; set; }
        public String condicion_exigencia{ get; set; }
    }
}
