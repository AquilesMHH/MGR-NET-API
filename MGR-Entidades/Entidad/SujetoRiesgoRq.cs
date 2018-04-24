using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MGR_Entidades.Entidad
{
    public class SujetoRiesgoRq
    {
        public int sujeto_riesgo{ get; set; }
        public int fase_control{ get; set; }
        public String descripcion_breve{ get; set; }
        public String fecha_inicio_vigencia{ get; set; }
        public String fecha_fin_vigencia{ get; set; }
        public int tipo_seleccion{ get; set; }
        public String descripcion_completa{ get; set; }
        public int organo_institucional{ get; set; }
        public int sesion{ get; set; }

    }
}
