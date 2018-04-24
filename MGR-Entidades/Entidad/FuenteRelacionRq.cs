using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class FuenteRelacionRq
    {
        private string campo { get; set; }
        private string operador { get; set; }
        private string tipoJoin { get; set; }
        private string campoParametro { get; set; }
    }
}
