using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAT.ConsultaCFDI;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IConsultaCFDIService" in both code and config file together.
    [ServiceContract]
    public interface IConsultaCFDIService
    {
        [OperationContract]
        Acuse Consulta(string expresionImpresa);
    }
}
