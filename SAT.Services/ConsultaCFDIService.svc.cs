using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAT.ConsultaCFDI;
using SAT.Core.DL.DAO.Cancelation;
using SAT.Core.DL.DAO.Pendings;
using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.DAO.Relations;
using SAT.Core.DL.Implements.SQL;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ConsultaCFDIService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ConsultaCFDIService.svc or ConsultaCFDIService.svc.cs at the Solution Explorer and start debugging.
    public class ConsultaCFDIService : IConsultaCFDIService
    {
        public SAT.ConsultaCFDI.IConsultaCFDIServiceEmulation _service;
        public ConsultaCFDIService()
        {
            
            string emulation_db = System.Environment.GetEnvironmentVariable("EMULATION_DB");
            ReceptionDAO reception;
            CancelationDAO cancelation;
            PendingsDAO pendings;
            RelationsDAO relations;
            using (SAT.Core.DL.Database _sql = new SAT.Core.DL.Database(new SQLDatabase(emulation_db)))
            {
                reception = new ReceptionDAO(_sql);
                cancelation = new CancelationDAO(_sql);
                pendings = new PendingsDAO(_sql);
                relations = new RelationsDAO(_sql);                
            }
            _service = new ConsultaCFDIServiceEmulation(emulation_db);
        }
        public Acuse Consulta(string expresionImpresa)
        {
            return _service.Consulta(expresionImpresa);
        }
    }
}
