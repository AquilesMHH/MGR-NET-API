using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Persistence.com.pe.mgr.dao.impl;
using MGR_Common.com.pe.mgr.common.constants;

namespace MGR_Business.com.pe.mgr.service.impl
{
    public class MedidasServiceImpl : MedidasService
    {
        private MedidaDaoImpl _medidaDaoImpl;
        public MedidasServiceImpl()
        {
            _medidaDaoImpl = new MedidaDaoImpl();
        }
        public string mplementarMedidaMultipleDao(string  metodo, int idSession, List<MedidaRevImpRq> lstMedidaRevImpRq)
        {
            try
            {
                bool revisar = false;

                EnumTipoOperacion tipoOperacion = 0;

                StringBuilder sbRespuesta = new StringBuilder();

                int i = 0;

                foreach (MedidaRevImpRq medidaRevImpRq in lstMedidaRevImpRq)
                {
                    if (i == 0)
                    {
                        if (metodo.Equals("revisar"))
                        {
                            if (medidaRevImpRq.CONFORME)
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Revisada Conforme");
                            }
                            else
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Revisada No Conforme");
                            }
                        }
                        else if (metodo.Equals("implementar"))
                        {
                            if (medidaRevImpRq.APROBADA)
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Aprobada(s)");
                            }
                            else
                            {
                                sbRespuesta.Append("La(s) regla(s) ha(n) sido Rechazada(s)");
                            }
                        }
                    }

                    if (metodo.Equals("revisar"))
                    {
                        revisar = true;

                        tipoOperacion = EnumTipoOperacion.REVISION;

                        if (medidaRevImpRq.CONFORME)
                        {
                            medidaRevImpRq.ESTADO=40; // Revision Conforme
                        }
                        else
                        {
                            medidaRevImpRq.ESTADO = 41 ; // Revision No Conforme
                        }
                    }
                    else if (metodo.Equals("implementar"))
                    {
                        revisar = false;

                        tipoOperacion = EnumTipoOperacion.APROBACION;

                        if (medidaRevImpRq.APROBADA)
                        {
                            medidaRevImpRq.ESTADO=42; // Aprobada
                        }
                        else
                        {
                            medidaRevImpRq.ESTADO=43; // Rechazada
                        }
                    }

                    medidaRevImpRq.IDSESSION=idSession;

                    if ((medidaRevImpRq.FECHAINICIOVIGENCIA  != null) && (!string.IsNullOrEmpty(medidaRevImpRq.FECHAINICIOVIGENCIA)))
                    {
                       /* Date fechaInicioVigencia = this.serviceUtil.parseDateWithInit(medidaRevImpRq.getFechaInicioVigencia());
                        Date fechaActual = this.serviceUtil.dateNowWithoutTime();

                        if (fechaInicioVigencia.compareTo(fechaActual) == 0)
                        {
                            fechaInicioVigencia = this.serviceUtil.dateNow();
                        }

                        medidaRevImpRq.setDatFechaInicioVigencia(fechaInicioVigencia);*/
                    }

                   // medidaRevImpRq.setDatFechaFinVigencia(this.serviceUtil.parseDateWithFinish(medidaRevImpRq.getFechaFinVigencia()));

                   // this.medidasDAO.updateRevImp(revisar, medidaRevImpRq);

                   // this.insertarOperacionMedida(medidaRevImpRq.getId_medida(), medidaRevImpRq.getVersion_medida(),
                     //       medidaRevImpRq.getComentarios(), idSession, tipoOperacion.getCodigo());

                    i++;
                }

               return sbRespuesta.ToString() ;
            }
            catch (Exception e)
            {
               // this.logger.error(e.getMessage(), e);

               // throw new MgrServiceException(e.getErrorCode(), "Error al ".concat(metodo).concat(" la(s) regla(s)"));
            }
            return null;
             
        }
    }
}
