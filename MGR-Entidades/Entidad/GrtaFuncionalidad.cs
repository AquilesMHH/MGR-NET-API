using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
   public class GrtaFuncionalidad
    {
        public Nullable<int> ID_FUNCIONALIDAD { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<int> ID_FUNCIONALIDAD_PADRE { get; set; }
        public string TIPO { get; set; }  
        public string MAPEO_APLICACION { get; set; }  
        public string COMPONENTE  { get; set; }
        public string RUTA { get; set; }
        public Nullable<int> ICONO { get; set; }
        public Nullable<int> ORDEN  { get; set; }
        public List<GrtaFuncionalidad> Lst_funcionalidad_hijas { get => lst_funcionalidad_hijas; set => lst_funcionalidad_hijas = value; }

        private List<GrtaFuncionalidad> lst_funcionalidad_hijas;
    }
}
