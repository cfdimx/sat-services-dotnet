using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SAT.Core.Helpers;

namespace SAT.AceptacionRechazo
{
    public class CancelationAceptacionRechazoServiceEmulation : ICancelationAceptacionRechazoServiceEmulation
    {
        public AcusePeticionesPendientes ObtenerPeticionesPendientes(string rfcReceptor)
        {
            AcusePeticionesPendientes response = new AcusePeticionesPendientes();
            response.CodEstatus = "1100";
            string[] uuids = { Guid.NewGuid().ToString() };
            response.UUID = uuids;
            return response;
        }
            
            
        
       
        public AcuseAceptacionRechazo ProcesarRespuesta(SolicitudAceptacionRechazo SolicitudAceptacionRechazo)
        {
            AcuseAceptacionRechazo ar = new AcuseAceptacionRechazo();
            X509Certificate2 certificate = new X509Certificate2(SolicitudAceptacionRechazo.Signature.KeyInfo.X509Data.X509Certificate);
            if(certificate == null)
            {
                ar.CodEstatus = "305";
                return ar;
            }
            if (!Sign.IsValidIssuer(certificate, SolicitudAceptacionRechazo.RfcReceptor))
            {
                ar.CodEstatus = "300";
                return ar;
            }
            
            List<AcuseAceptacionRechazoFolios> folios = new List<AcuseAceptacionRechazoFolios>();
            foreach (var f in SolicitudAceptacionRechazo.Folios)
            {
                folios.Add(new AcuseAceptacionRechazoFolios {UUID = f.UUID, EstatusUUID = "1000" });
            }
            ar.Folios = folios.ToArray();
            ar.Signature = SolicitudAceptacionRechazo.Signature;
            ar.Fecha = SolicitudAceptacionRechazo.Fecha;
            ar.RfcPac = SolicitudAceptacionRechazo.RfcPacEnviaSolicitud;
            ar.RfcReceptor = SolicitudAceptacionRechazo.RfcReceptor;
            ar.CodEstatus = "1000";
            return ar;
        }
    }
}
