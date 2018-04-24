using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Business.com.pe.mgr.service
{

    public interface ComboService : IDisposable
    {
        List<ComboBoxDto> listarCompendioDetalleCatalogoComboReferencia(int id_compendio);
        List<ComboBoxDto> listarCompendioDetalleCatalogoCombo(int id_compendio);
    }
}
