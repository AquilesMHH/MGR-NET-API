using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class CompendioDetalleDto
    {   public int ID_DETALLE  { get; set; }
        public int ID_COMPENDIO  { get; set; }
        public int COMPENDIO_SUBDETALLE  { get; set; }
        public  string NOMBRE  { get; set; }
        public  string CODIGO_ALTERNO  { get; set; }
    
        public DateTime FECHA_REGISTRO  { get; set; }
        public int SESSION_REGISTRO  { get; set; }
    
        public DateTime FECHA_FIN_VIGENCIA  { get; set; }
  
        public DateTime FECHA_INICIO_VIGENCIA  { get; set; }
        public  string REFERENCIA2  { get; set; }
        public int VARIABLE_CATALOGO  { get; set; }
        public  string DESCRIPCION  { get; set; }
        public int SUJETO_RIESGO  { get; set; }
        public  string REFERENCIA1  { get; set; }
        public  string INDIVIDUAL_GRUPAL  { get; set; }
       
        public int NROELEMENTOS  { get; set; }
        public  string SUJETO  { get; set; }
        public  string FECHAINICIOFINVIGENCIA  { get; set; }
        public  string TABLARESERVADA  { get; set; }
    }
}
