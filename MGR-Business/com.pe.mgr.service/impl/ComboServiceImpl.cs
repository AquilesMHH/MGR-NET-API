using MGR_Persistence.com.pe.mgr.dao.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;

namespace MGR_Business.com.pe.mgr.service.impl
{
    public class ComboServiceImpl : ComboService
    {
        private ComboDaoImpl _comboDaoImpl;
        public ComboServiceImpl()
        {
            _comboDaoImpl = new ComboDaoImpl();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<ComboBoxDto> listarCompendioDetalleCatalogoCombo(int id_compendio)
        {
            return _comboDaoImpl.listarCompendioDetalleCatalogoCombo(id_compendio);
        }

        public List<ComboBoxDto> listarCompendioDetalleCatalogoComboReferencia(int id_compendio)
        {
            return _comboDaoImpl.listarCompendioDetalleCatalogoComboReferencia(id_compendio);
        }

    }
}
