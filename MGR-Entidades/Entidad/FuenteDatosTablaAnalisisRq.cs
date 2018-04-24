using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    [Serializable]
    public class FuenteDatosTablaAnalisisRq  
    {
        public int  identificacion_fuente  { get; set; }
        public int  identificacion_identificador  { get; set; }
        public int  identificacion_join_sujeto  { get; set; }
        public int  sujeto_riesgo  { get; set; }
        public int  origen_fuente  { get; set; }
        public int  tipo_tabla  { get; set; }
        public int  tabla_fuente  { get; set; }
        public string campo_canal  { get; set; }
        public string campo_fecha  { get; set; }
        public string campo_fecha_anio_mes  { get; set; }
        public string campo_fecha_anio_semana  { get; set; }
        public int  campo  { get; set; }
        public string operador  { get; set; }
        public int  tipo_join  { get; set; }
        public int  tabla_fuente_padre  { get; set; }
        public DateTime fecha_ini_vigencia  { get; set; }
        public DateTime fecha_fin_vigencia  { get; set; }
        public int  sesion  { get; set; }
        public int  codigo_tabla  { get; set; }
        public virtual ICollection<FuenteDatosRq> listFuenteRelacion { get; set; }
    }
}
