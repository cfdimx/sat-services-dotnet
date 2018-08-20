using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAT.ConsultaCFDI;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ConsultaCFDIService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ConsultaCFDIService.svc or ConsultaCFDIService.svc.cs at the Solution Explorer and start debugging.
    public class ConsultaCFDIService : IConsultaCFDIService
    {
        public SAT.ConsultaCFDI.IConsultaCFDIServiceEmulation _service;
        public ConsultaCFDIService()
        {
            _service = new ConsultaCFDIServiceEmulation();
        }
        public Acuse Consulta(string expresionImpresa)
        {
            return _service.Consulta(expresionImpresa);
        }
    }
}
