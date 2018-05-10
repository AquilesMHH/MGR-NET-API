using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    
    public partial class GRTA_FILTROS_MEDIDAS
    {
        public GRTA_FILTROS_MEDIDAS()
        {

        }
        public int FILTRO_MEDIDAS { get; set; }
        public int ID_MEDIDA { get; set; }
        public Nullable<byte> VERSION_MEDIDA { get; set; }
        public Nullable<int> ID_FILTROS { get; set; }
   
    }
}
