using SAT.AceptacionRechazo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICancelationAceptacionRechazoService" in both code and config file together.
    [ServiceContract(Namespace = "http://cancelacfd.sat.gob.mx")]
    [XmlSerializerFormat]    
    public interface IAceptacionRechazoService
    {
        [OperationContract]
        AcuseAceptacionRechazo ProcesarRespuesta(SolicitudAceptacionRechazo solicitud);
        [OperationContract]
        AcusePeticionesPendientes ObtenerPeticionesPendientes(string rfcReceptor);
    }
}
