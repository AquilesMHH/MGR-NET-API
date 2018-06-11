using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Persistence.com.pe.mgr.dao
{
    public interface CompendioGeneralDao
    {
        CompendioGeneral guardarCompendioGeneral(CompendioGeneral obj);
        CompendioGeneral obtenerCompendioGeneral(int id_compendio);
        List<CompendioGeneralBean> consultarPorParametro(CompendioGeneral obj);
        CompendioGeneral actualizarCompendioGeneral(CompendioGeneral obj);
        CompendioGeneral eliminarCompendioGeneral(int id_compendio);
        List<CompendioDetalleDto> listarClaseMedidas(int id_sujeto_riesgo);
        List<CompendioGeneralBean> listarCompendioDetalles(int id_compendio);
        List<CompendioGeneralBean> listarTipoTabla(int id_sujeto_riesgo);
        CompendioGeneral get(int id_compendio);
    }
}
