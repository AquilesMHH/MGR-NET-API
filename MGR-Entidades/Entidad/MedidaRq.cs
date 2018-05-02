using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class MedidaRq
    {
        //cabecera
        public int CONSECUTIVO { get; set; }  //3

        //GENERALIDADES
        public int ID_MEDIDA{ get; set; }
        public int VERSION_MEDIDA{ get; set; }
        public int SUJETORIESGO{ get; set; }//11
        public int TIPOMEDIDA{ get; set; }//17
        public int POLITICA{ get; set; }//2
        public int CLASEMEDIDA{ get; set; }//9256
        public int ESTADO{ get; set; }
        public int JERARQUIA{ get; set; }// 1
        public string NOMBRE{ get; set; } //MEDIDA MV2
        public int VARIABLEDEPENDIENTE{ get; set; }// REVISAR EL TIPO DE DATO
        public string FECHAVIGENCIAINI{ get; set; }//16/01/2018
        public string FECHAVIGENCIAFIN{ get; set; }//31/01/2018
        public string FECHAANALISISINI{ get; set; }
        public string FECHAANALISISFIN{ get; set; }
        public Object VARIABLES{ get; set; }
        public int FRECUENCIA{ get; set; }
        public int TIPOFRECUENCIA{ get; set; }
        public string DESCRIPCION{ get; set; }//DESCRIPCION MV2
        public int RITMOAPRENDIZAJE{ get; set; }
        public int FUNCIONACTIVACION{ get; set; }
        public string TERMINOMOMENTO{ get; set; }
        public int MEDIDAPRECEDENTEIDMEDIDA{ get; set; }
        public int MEDIDAPRECEDENTEVERSION{ get; set; }

        //COMPLEMENTOS
        public string COMENTARIOS{ get; set; }//COMENTARIO MV
        public string REMITIRCORREO{ get; set; }//:SI
        public string DESTINATARIOS{ get; set; }//:124
        public DateTime FECHA_REGISTRO{ get; set; }
        public int SESSION_REGISTRO{ get; set; }
        public string EXPRESION_FILTRO{ get; set; }
     //   public LIST<FILTRORQ> LSTFILTROMEDIDA{ get; set; }
       // public LIST<CONDICIONMEDIDARQ> LSTCONDICIONMEDIDARQ{ get; set; }
    }
}
