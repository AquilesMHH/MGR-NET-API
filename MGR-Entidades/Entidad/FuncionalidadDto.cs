using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class FuncionalidadDto
    {
        private String nombre { get; set; }

        private bool derecho { get; set; }

        private List<FuncionalidadOperacionDto> lstOperacion { get; set; }

    }
}
