using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Persistence.com.pe.mgr.dao
{
  
    public interface ComboDao : IDisposable
    {
        List<ComboBoxDto> ListarCompendioDetalleCatalogoComboReferencia(int id_compendio) ;
        List<ComboBoxDto> listarCompendioDetalleCatalogoCombo(int id_compendio);
        List<ComboBoxDto> listarCategoriaVocabularioNegocioCombo(int sujeto_riesgo);
        List<ComboBoxDto> listarCategoriaParametrosCombo(int sujeto_riesgo);
        List<ComboBoxDto> listarFiltroCombo(int sujeto_riesgo);
        List<ComboBoxDto> listarSujetoRiesgoCombo();
        List<ComboBoxDto> listarValoresVigentesCompendioCombo();
        List<ComboBoxDto> listarVariableCatalogoCombo();
    }
}
