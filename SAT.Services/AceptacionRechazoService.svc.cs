using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAT.AceptacionRechazo;
using SAT.Core.DL.DAO.Cancelation;
using SAT.Core.DL.DAO.Pendings;
using SAT.Core.DL.Implements.SQL;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CancelationAceptacionRechazoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CancelationAceptacionRechazoService.svc or CancelationAceptacionRechazoService.svc.cs at the Solution Explorer and start debugging.
    public class AceptacionRechazoServiceClient : IAceptacionRechazoService
    {

        ICancelationAceptacionRechazoServiceEmulation _service;
        public AceptacionRechazoServiceClient()
        {
            PendingsDAO pendings = PendingsDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(Environment.GetEnvironmentVariable("EMULATION_DB"))));
            CancelationDAO cancelation = CancelationDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(Environment.GetEnvironmentVariable("EMULATION_DB"))));
            _service = new CancelationAceptacionRechazoServiceEmulation(pendings, cancelation);
        }
        public AcuseAceptacionRechazo ProcesarRespuesta(SolicitudAceptacionRechazo solicitud)
        {
            return _service.ProcesarRespuesta(solicitud);
        }
        public AcusePeticionesPendientes ObtenerPeticionesPendientes(string rfcReceptor)
        {
            return _service.ObtenerPeticionesPendientes(rfcReceptor);
        }
    }
}
