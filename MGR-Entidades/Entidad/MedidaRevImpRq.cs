using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Entidades.Entidad
{
    public class MedidaRevImpRq
    {
        public int ID_MEDIDA;

        public int VERSION_MEDIDA;

        public string COMENTARIOS;

        public bool CONFORME;

        public bool APROBADA;

        public string FECHAINICIOVIGENCIA;

        public string FECHAFINVIGENCIA;

        public DateTime DATFECHAINICIOVIGENCIA;

        public DateTime DATFECHAFINVIGENCIA;

        public int ESTADO;

        public int IDSESSION;

    }
}
