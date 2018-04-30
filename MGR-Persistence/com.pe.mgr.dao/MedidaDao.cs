using MGR_Common.com.pe.mgr.common.util;
using MGR_Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Persistence.com.pe.mgr.dao
{
    public interface MedidaDao : IDisposable
    {
        string implementarMedidaMultipleDao(String metodo, int idSession, List<MedidaRevImpRq> lstMedidaRevImpRq);
        string revisarImplementarMedidaMultipleDao(string metodo, int idSession, List<MedidaRevImpRq> lstMedidaRevRq);
        int[] batchUpdateRevImp(bool revisar, List<MedidaRevImpRq> lstMedidaRevImpRq);
        int updateRevImp(bool revisar, MedidaRevImpRq medidaRevImpRq);
        List<Row> listarFormulaItems(int elemento, int sujetoRiesgo, String tipoMedida, int linea, String funcionalidad);
        List<Row> listarFormulaItemsConCategoria(int elemento, int sujetoRiesgo, string tipoMedida, int linea, string funcionalidad);
        List<Row> listarFormulaOpciones(int elemento, int itemElemento, int sujetoRiesgo, String tipoMedida, int linea, String funcionalidad);
        List<Row> listarFormulaOpcionesConCategoria(int categoria, int elemento, int itemElemento, int sujetoRiesgo, String tipoMedida, int linea, String funcionalidad, String cadena);
        List<Row> listarValorOperador(int categoria, int operador, int tipo_valor, int sujeto_riesgo, int variable);
        List<ComboBoxDto> obtenerTipoRespValores(int sujeto, int tipoRespuesta);
        List<ComboBoxDto> obtenerVariablePorOrigen(int sujetoRiesgo, int origen);
        List<ComboBoxDto> obtenerValorVariable(int origen, int id_detalle);
        List<ComboBoxDto> obtenerUnidadMedicion(int tipo_medicion);
        List<ComboBoxDto> obtenerMedidaPorTipo(int tipo_medida, int sujeto_riesgo);
        String consultarDescripcionMedPrec(int sujetoRiesgo, int tipoMedida, int idMedida, int versionMedida);

    }        
}
