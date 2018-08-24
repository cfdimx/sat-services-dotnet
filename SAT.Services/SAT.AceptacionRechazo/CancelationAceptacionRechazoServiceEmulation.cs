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
            
            
        
       
        public AcuseAceptacionRechazo ProcesarRespuesta(SolicitudAceptacionRechazo SolicitudAceptacionRechazo)
        {
            AcuseAceptacionRechazo ar = new AcuseAceptacionRechazo();
            try
            {
                if (SolicitudAceptacionRechazo == null || string.IsNullOrEmpty(SolicitudAceptacionRechazo.RfcReceptor))
                {
                    ar.CodEstatus = "301";
                    return ar;
                }
                if (!Core.Helpers.FiscalHelper.IsTaxIdValid(SolicitudAceptacionRechazo.RfcReceptor))
                {
                    ar.CodEstatus = "305";
                    return ar;
                }
                Guid uuid = Guid.Empty;
                if(SolicitudAceptacionRechazo.Folios.Any(w => !Guid.TryParse(w.UUID, out uuid)))
                {
                    ar.CodEstatus = "300";
                    return ar;
                }
                X509Certificate2 certificate = new X509Certificate2(SolicitudAceptacionRechazo.Signature.KeyInfo.X509Data.X509Certificate);
                if (certificate == null)
                {
                    ar.CodEstatus = "305";
                    return ar;
                }
                if (!IsValidIssuer(SolicitudAceptacionRechazo))
                {
                    ar.CodEstatus = "300";
                    return ar;
                }

                var xmlDocument = XmlMessageSerializer.SerializeDocumentToXml(SolicitudAceptacionRechazo);
                bool isValid = SAT.Core.Helpers.SignatureHelper.ValidateSignatureXml(xmlDocument);
                if (!isValid)
                {
                    ar.CodEstatus = "300";
                    return ar;
                }
                List<AcuseAceptacionRechazoFolios> folios = new List<AcuseAceptacionRechazoFolios>();
                foreach (var f in SolicitudAceptacionRechazo.Folios)
                {
                    folios.Add(new AcuseAceptacionRechazoFolios { UUID = f.UUID, EstatusUUID = "1000" });
                }
                ar.Folios = folios.ToArray();
                ar.Signature = SolicitudAceptacionRechazo.Signature;
                ar.Fecha = SolicitudAceptacionRechazo.Fecha;
                ar.RfcPac = SolicitudAceptacionRechazo.RfcPacEnviaSolicitud;
                ar.RfcReceptor = SolicitudAceptacionRechazo.RfcReceptor;
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

        private string ToXML<T>(T solicitudAceptacionRechazo)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringwriter, solicitudAceptacionRechazo);
            return stringwriter.ToString();
        }


    }
}
