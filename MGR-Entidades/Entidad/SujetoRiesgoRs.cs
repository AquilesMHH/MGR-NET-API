using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class SujetoRiesgoRs
    {
        public SujetoRiesgoDto sujetoRiesgo { get; set; }
        public  List<AmbitosSujetoDto> listAmbito { get; set; }
    }
}
