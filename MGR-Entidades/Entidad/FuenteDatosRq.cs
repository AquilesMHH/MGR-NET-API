using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{ 
    public class FuenteDatosRq  
    {
        public int sujeto_riesgo { get; set; }
        public int id_parametro { get; set; }
        public int tipo_tabla { get; set; }
        public int origen_fuente { get; set; }
        public string nombre_tabla { get; set; }
        public string nombre_campo { get; set; }
    }
}
