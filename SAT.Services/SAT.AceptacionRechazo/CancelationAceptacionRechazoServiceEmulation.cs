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
            throw new NotImplementedException();
        }
    }
}
