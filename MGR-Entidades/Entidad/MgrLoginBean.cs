using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class MgrLoginBean  
    {

        public int StrIdUsuario { get; set; }

        public string StrUsuario { get; set; }

        public string StrContrasenna { get; set; }

        public GRTA_USUARIO usuario { get; set; }

        public int strIdSession { get; set; }

        public int consecutivoSession { get; set; }

        public int intValido { get; set; }

        public List<string> lstRoles { get; set; }
        public Dictionary<string, List<GrtaFuncionalidad>> Permiso { get => permiso; set => permiso = value; }

        private Dictionary<string, List<GrtaFuncionalidad>> permiso;

        public string token { get; set; }

    }
}
