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
    public partial class GRTA_MENSAJES_SUJETO
    {
        public long ID_MENSAJE_SUJETO { get; set; }
        public Nullable<byte> SUJETO_RIESGO { get; set; }
        public Nullable<long> ID_PERFIL { get; set; }
        public Nullable<int> ID_MEDIDA { get; set; }
        public Nullable<byte> VERSION_MEDIDA { get; set; }
        public Nullable<int> CONDICION_MEDIDAS { get; set; }
        public string IDENTIFICADOR_DECLARACION { get; set; }
        public string INDICADOR_PROCESO { get; set; }
    
        public virtual GRTA_DECLARACIONES_PERFIL GRTA_DECLARACIONES_PERFIL { get; set; }
        public virtual GRTA_MEDIDAS GRTA_MEDIDAS { get; set; }
    }
    
}
