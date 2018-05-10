using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Persistence.com.pe.mgr.dao
{
    public interface OperacionComunDao
    {
        String consultarDescripcionCatalogo(String tipo_busqueda, int variable, String codigo)  ;
        String consultarDescripcionFiltro(String tipo_busqueda, int variable, String codigo)  ;
      //  List<AutocompleteDto> autocompletar(String tipo_busqueda, int variable, String valor)  ;
    }
}
