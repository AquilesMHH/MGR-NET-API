using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Persistence.com.pe.mgr.dao.impl;
using MGR_Common.com.pe.mgr.common.util;

namespace MGR_Business.com.pe.mgr.service.impl
{

    public class CompendioGeneralServiceImpl : CompendioGeneralService
    {
        private CompendioGeneralDaoImpl _compendioGeneralDaoImpl;
        public CompendioGeneralServiceImpl()
        {
            _compendioGeneralDaoImpl = new CompendioGeneralDaoImpl();
        }

        public CompendioGeneral actualizarCompendioGeneral(CompendioGeneral obj)
        {
            throw new NotImplementedException();
        }

        public List<CompendioGeneralBean> consultarPorParametro(CompendioGeneral obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral eliminarCompendioGeneral(int id_compendio)
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral guardarCompendioGeneral(CompendioGeneral obj)
        {
            throw new NotImplementedException();
        }

        public List<CompendioDetalleDto> listarClaseMedidas(int id_sujeto_riesgo)
        {
            return _compendioGeneralDaoImpl.listarClaseMedidas(id_sujeto_riesgo);
        }

        public List<CompendioGeneralBean> listarCompendioDetalles(int id_compendio)
        {
            throw new NotImplementedException();
        }

        public List<CompendioGeneralBean> listarTipoTabla(int id_sujeto_riesgo)
        {
            throw new NotImplementedException();
        }

        public CompendioGeneral obtenerCompendioGeneral(int id_compendio)
        {
            throw new NotImplementedException();
        }
        public List<Row> getListarTipoTabla()
        {
            List<Row> lstRows = new List<Row>();

            Row row = new Row();

            row.Add("id", 1);
            row.Add("nombre", "Catálogo");

            lstRows.Add(row);

            row = new Row();
            row.Add("id", 2);
            row.Add("nombre", "Código validación");

            lstRows.Add(row);

            row = new Row();
            row.Add("id", 3);
            row.Add("nombre", "Lista");

            lstRows.Add(row);

            return lstRows;
        }

        public CompendioGeneral get(int id_compendio)
        {
            return _compendioGeneralDaoImpl.get(id_compendio);
        }

        public List<Row> getListarTipoModificacion()
        {
           List<Row> lstRows = new List<Row>();

            Row row = new Row();

            row.Add("id", 1);
            row.Add("nombre", "Asignado por Sistema");

            lstRows.Add(row);

            row = new Row();
            row.Add("id", 2);
            row.Add("nombre", "Asignado por Usuario");

            lstRows.Add(row);
 

            return lstRows;
        }
    }
}
