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
        public AcusePeticionesPendientes ObtenerPeticionesPendientes(string rfcReceptor)
        {

           
            AcusePeticionesPendientes response = new AcusePeticionesPendientes();
            if (!Core.Helpers.FiscalHelper.IsTaxIdValid(rfcReceptor))
            {
                response.CodEstatus = "305";
                return response;
            }
            
            response.CodEstatus = "1100";
            string[] uuids = { Guid.NewGuid().ToString() };
            response.UUID = uuids;
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
                
                if(solicitud.Folios.Any(w => !Guid.TryParse(w.UUID, out uuid)))
                {
                    List<AcuseAceptacionRechazoFolios> fls = new List<AcuseAceptacionRechazoFolios>();
                    ar.CodEstatus = "1000";
                    foreach (var f in solicitud.Folios)
                    {
                        if (!Guid.TryParse(f.UUID, out uuid))
                        {
                            fls.Add(new AcuseAceptacionRechazoFolios() {UUID = f.UUID, EstatusUUID = "1005", Respuesta = f.Respuesta.ToString() });
                        }
                        else
                        {
                            fls.Add(new AcuseAceptacionRechazoFolios() { UUID = f.UUID, EstatusUUID = "1000", Respuesta = f.Respuesta.ToString() });
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
                if (!IsValidIssuer(solicitud))
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
            catch (Exception e)
            {
                ar.CodEstatus = "305";
                return ar;
            }
            
            
        }

   

        private bool IsValidIssuer(SolicitudAceptacionRechazo SolicitudAceptacionRechazo)
        {
            X509Certificate2 certificate = new X509Certificate2(SolicitudAceptacionRechazo.Signature.KeyInfo.X509Data.X509Certificate);
            string taxIdCertificate = "";
            string[] subjects = certificate.SubjectName.Name.Trim().Split(',');
            foreach (var strTemp in subjects.ToList())
            {
                string[] strConceptoTemp = strTemp.Split('=');
                if (strConceptoTemp[0].Trim() == "OID.2.5.4.45")
                {
                    taxIdCertificate = strConceptoTemp[1].Trim().Split('/')[0];
                    
                    taxIdCertificate = taxIdCertificate.Replace("\"", "");
                    break;
                }
            }
            taxIdCertificate = taxIdCertificate.Trim().ToUpper();
            SolicitudAceptacionRechazo.RfcReceptor = SolicitudAceptacionRechazo.RfcReceptor.Trim().ToUpper();

            return taxIdCertificate == SolicitudAceptacionRechazo.RfcReceptor;
        }

       


    }
}
