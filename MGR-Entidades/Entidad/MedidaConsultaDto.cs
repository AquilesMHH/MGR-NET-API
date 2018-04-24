using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class MedidaConsultaDto
    {
        public int ID_MEDIDA{ get; set; }

        public int VERSION{ get; set; }

        public int COD_TIPO_MEDIDA{ get; set; }

        public string TIPO_MEDIDA{ get; set; }

        public string NOMBRE_MEDIDA{ get; set; }

        public string PERIODO{ get; set; }

        public int COD_SUJETO_RIESGO{ get; set; }

        public string SUJETO_RIESGO{ get; set; }

        public int COD_ESTADO_MEDIDA{ get; set; }

        public string ESTADO_MEDIDA{ get; set; }

        public int CANTIDAD_CONDICIONES{ get; set; }
    }
}
