using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.AceptacionRechazo
{
    public interface ICancelationAceptacionRechazoServiceEmulation
    {
        AcuseAceptacionRechazo ProcesarRespuesta(SolicitudAceptacionRechazo solicitud);
        AcusePeticionesPendientes ObtenerPeticionesPendientes(string rfcReceptor);
    }
}
