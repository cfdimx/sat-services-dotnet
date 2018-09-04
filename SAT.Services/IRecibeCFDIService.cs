using SAT.RecibeCFDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SAT.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IRecibeCFDIService" en el código y en el archivo de configuración a la vez.
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICancelationAceptacionRechazoService" in both code and config file together.
    [ServiceContract(Namespace = "http://recibecfdi.sat.gob.mx")]
    [XmlSerializerFormat]
    public interface IRecibeCFDIService
    {
        [OperationContract]
        AcuseRecepcion Recibe(CFDI cFDI);
    }
}
