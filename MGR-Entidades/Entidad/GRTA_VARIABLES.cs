//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace MGR_Entidades.Entidad
{
    public partial class GRTA_VARIABLES
    {
        public short CODIGO_VARIABLE { get; set; }
        public Nullable<byte> SUJETO_RIESGO { get; set; }
        public string DESCRIPCION_BREVE { get; set; }
        public string DESCRIPCION_COMPLETA { get; set; }
        public Nullable<int> TIPO_VARIABLE { get; set; }
        public Nullable<int> TABLA_TRANSACCIONAL { get; set; }
        public string EXPRESION_TRANSACCIONAL { get; set; }
        public Nullable<int> TABLA_CONSOLIDACION { get; set; }
        public string EXPRESION_CONSOLIDACION { get; set; }
        public Nullable<int> TIPO_DATO { get; set; }
        public Nullable<int> MODO_USO { get; set; }
        public Nullable<int> NUMERO_OCURRENCIAS { get; set; }
        public Nullable<int> MODO_OBTENCION { get; set; }
        public Nullable<int> INDICADOR_PRESENCIA { get; set; }
        public Nullable<short> TABLA_CODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHA_FIN_VIGENCIA { get; set; }
        public Nullable<System.DateTime> FECHA_INICIO_VIGENCIA { get; set; }
        public string QUERY_AUTOGENERADO { get; set; }
        public Nullable<short> VARIABLE_CATALOGO { get; set; }
        public Nullable<System.DateTime> FECHA_REGISTRO { get; set; }
        public Nullable<int> SESSION_REGISTRO { get; set; }
        public Nullable<int> CLASIFICACION_DATA { get; set; }
    
        public virtual GRTA_SESSION GRTA_SESSION { get; set; }
        public virtual GRTA_VARIABLES GRTA_VARIABLES1 { get; set; }
        public virtual GRTA_VARIABLES GRTA_VARIABLES2 { get; set; }
    }
    
}
