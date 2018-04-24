using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Persistence.com.pe.mgr.dao
{
    public interface SujetoRiesgoDao : IDisposable
    {
         List<SujetoRiesgoDto> listar(SujetoRiesgoRq sujetoRiesgoRq);

         SujetoRiesgoDto obtenerPorId(int sujeto_riesgo);

         SujetoRiesgoRs registrar(SujetoRiesgoRq sujetoRiesgoRq);

         SujetoRiesgoRs actualizar(int sujeto_riesgo, SujetoRiesgoRq sujetoRiesgoRq);

         SujetoRiesgoRs eliminar(int sujeto_riesgo);

         SujetoRiesgoRs obtenerAmbitoRestriccion(int sujeto_riesgo);

         List<ComboBoxDto> listarPorSession(int id_session);

         List<ComboBoxDto> listarCombo();

         List<ComboBoxDto> listarComboVFiltro(int id_sujeto_riesgo) ;

         List<ComboBoxDto> listarComboSujeto(int id_tipo_seleccion) ;

    }
}
 