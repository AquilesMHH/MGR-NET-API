using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class UsuarioDto
    {
        public String primer_nombre { get; set; }

        public String segundo_nombre { get; set; }

        public String primer_apellido { get; set; }

        public String segundo_apellido { get; set; }

        public String email { get; set; }

        public byte[] foto { get; set; }

        public bool usuario_administrador { get; set; }
    }
}
