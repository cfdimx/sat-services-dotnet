using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ar.CodEstatus = "1100";
            return ar;
        }
    }
}
