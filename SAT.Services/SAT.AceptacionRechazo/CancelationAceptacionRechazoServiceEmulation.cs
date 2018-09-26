using SAT.Core.DL.DAO.Cancelation;
using SAT.Core.DL.DAO.Pendings;
using SAT.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SAT.AceptacionRechazo
{
    public class CancelationAceptacionRechazoServiceEmulation : ICancelationAceptacionRechazoServiceEmulation
    {
        PendingsDAO _pendings;
        CancelationDAO _cancelation;
        public CancelationAceptacionRechazoServiceEmulation(PendingsDAO pendings, CancelationDAO cancelation)
        {
            _pendings = pendings;
            _cancelation = cancelation;
        }

        public AcusePeticionesPendientes ObtenerPeticionesPendientes(string rfcReceptor)
        {          
            AcusePeticionesPendientes response = new AcusePeticionesPendientes();
            if (!Core.Helpers.FiscalHelper.IsTaxIdValid(rfcReceptor))
            {
                response.CodEstatus = "305";
                return response;
            }
            List<string> uuids = new List<string>();
            var pendings = _pendings.GetPendings(rfcReceptor);
            if(pendings.ToArray().Length == 0)
            {
                response.CodEstatus = "1101";
            }
            else
            {
                response.CodEstatus = "1100";
            }
            foreach (var p in pendings)
            {
                uuids.Add(p.UUID);
            }
            
           
            
            response.UUID = uuids.ToArray();
            return response;
        }
        public AcuseAceptacionRechazo ProcesarRespuesta(SolicitudAceptacionRechazo solicitud)
        {
            AcuseAceptacionRechazo ar = new AcuseAceptacionRechazo();
            try
            {
                if (solicitud == null || string.IsNullOrEmpty(solicitud.RfcReceptor))
                {
                    ar.CodEstatus = "301";
                    return ar;
                }
                if (!Core.Helpers.FiscalHelper.IsTaxIdValid(solicitud.RfcReceptor))
                {
                    ar.CodEstatus = "301";
                    return ar;
                }
                Guid uuid = Guid.Empty;
                if(solicitud.Folios == null)
                {
                    ar.CodEstatus = "301";
                    return ar;
                }
                if(solicitud.Folios.Length < 1)
                {
                    ar.CodEstatus = "301";
                    return ar;
                }
                if (!_pendings.HasPendings(solicitud.RfcReceptor))
                {
                    ar.CodEstatus = "1101";
                    return ar;
                }
                
                if(solicitud.Folios.Any(w => !Guid.TryParse(w.UUID, out uuid)))
                {
                    List<AcuseAceptacionRechazoFolios> fls = new List<AcuseAceptacionRechazoFolios>();
                    ar.CodEstatus = "1000";
                    foreach (var f in solicitud.Folios)
                    {

                        if (!Guid.TryParse(f.UUID, out uuid))
                        {
                            f.UUID = f.UUID.ToUpper();
                            fls.Add(new AcuseAceptacionRechazoFolios() {UUID = f.UUID, EstatusUUID = "1005", Respuesta = f.Respuesta.ToString() });
                        }
                        
                    }

                    ar.Folios = fls.ToArray();
                    ar.Fecha = solicitud.Fecha;
                    return ar;
                }
                X509Certificate2 certificate = new X509Certificate2(solicitud.Signature.KeyInfo.X509Data.X509Certificate);
                if (certificate == null)
                {
                    ar.CodEstatus = "305";
                    return ar;
                }
                if (!Sign.IsValidIssuer(certificate, solicitud.RfcReceptor))
                {
                    ar.CodEstatus = "300";
                    return ar;
                }

                var xmlDocument = XmlMessageSerializer.SerializeDocumentToXml(solicitud);
                bool isValid = SAT.Core.Helpers.SignatureHelper.ValidateSignatureXml(xmlDocument);
                if (!isValid)
                {
                    ar.CodEstatus = "300";
                    return ar;
                }
                List<AcuseAceptacionRechazoFolios> folios = new List<AcuseAceptacionRechazoFolios>();
                foreach (var f in solicitud.Folios)
                {
                    f.UUID = f.UUID.ToUpper();
                    ExecuteAction(f);
                    folios.Add(new AcuseAceptacionRechazoFolios { UUID = f.UUID, EstatusUUID = "1000", Respuesta = f.Respuesta.ToString() });
                }
                ar.Folios = folios.ToArray();
                ar.Signature = solicitud.Signature;
                ar.Fecha = solicitud.Fecha;
                ar.RfcPac = solicitud.RfcPacEnviaSolicitud;
                ar.RfcReceptor = solicitud.RfcReceptor;
                ar.CodEstatus = "1000";
                return ar;
            }
            catch (Exception)
            {
                ar.CodEstatus = "305";
                return ar;
            }
            
            
        }

        private void ExecuteAction(SolicitudAceptacionRechazoFolios folio)
        {
            if(folio.Respuesta == TipoAccionPeticionCancelacion.Aceptacion)
            {
                _cancelation.CancelDocument(folio.UUID);
                
            }
            else
            {
                _cancelation.NormalizeDocument(folio.UUID);
               
            }
            _pendings.DeletePending(folio.UUID);
        }
     }
}
