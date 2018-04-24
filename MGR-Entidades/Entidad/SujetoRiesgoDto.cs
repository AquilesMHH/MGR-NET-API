using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class SujetoRiesgoDto
    {
        public  int sujeto_riesgo { get; set; }
        public int fase_control { get; set; }
        public  string fase_control_text { get; set; }
        public string descripcion_breve { get; set; }
        public  DateTime fecha_inicio_vigencia { get; set; }
        public DateTime fecha_fin_vigencia { get; set; }
        public int tipo_seleccion { get; set; }
        public string tipo_seleccion_text { get; set; }
        public string descripcion_completa { get; set; }
        public int organo_institucional { get; set; }
        public string organo_institucional_text { get; set; }
        public string periodo_vigencia { get; set; }

    }
}
