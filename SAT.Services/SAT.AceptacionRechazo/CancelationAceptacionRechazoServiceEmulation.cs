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
            throw new NotImplementedException();
        }

        public AcuseAceptacionRechazo ProcesarRespuesta(SolicitudAceptacionRechazo solicitud)
        {
            AcuseAceptacionRechazo ar = new AcuseAceptacionRechazo();
            List<AcuseAceptacionRechazoFolios> folios = new List<AcuseAceptacionRechazoFolios>();
            foreach (var f in solicitud.Folios)
            {
                folios.Add(new AcuseAceptacionRechazoFolios {UUID = f.UUID, EstatusUUID = "1000" });
            }
            ar.Folios = folios.ToArray();
            ar.Signature = solicitud.Signature;
            ar.Fecha = solicitud.Fecha;
            return ar;
        }
    }
}
