using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAT.AceptacionRechazo;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CancelationAceptacionRechazoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CancelationAceptacionRechazoService.svc or CancelationAceptacionRechazoService.svc.cs at the Solution Explorer and start debugging.
    public class AceptacionRechazoServiceClient : IAceptacionRechazoService
    {

        ICancelationAceptacionRechazoServiceEmulation _service;
        public AceptacionRechazoServiceClient()
        {
            _service = new CancelationAceptacionRechazoServiceEmulation();
        }
        public AcuseAceptacionRechazo ProcesarRespuesta(SolicitudAceptacionRechazo SolicitudAceptacionRechazo)
        {
            return _service.ProcesarRespuesta(SolicitudAceptacionRechazo);
        }
        public AcusePeticionesPendientes ObtenerPeticionesPendientes(string rfcReceptor)
        {
            return _service.ObtenerPeticionesPendientes(rfcReceptor);
        }
    }
}
