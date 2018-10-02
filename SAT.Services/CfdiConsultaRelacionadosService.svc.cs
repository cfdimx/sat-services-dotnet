using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAT.CfdiConsultaRelacionados;
using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.DAO.Relations;
using SAT.Core.DL.Implements.SQL;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CfdiConsultaRelacionadosService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CfdiConsultaRelacionadosService.svc or CfdiConsultaRelacionadosService.svc.cs at the Solution Explorer and start debugging.
    public class CfdiConsultaRelacionadosService : ICfdiConsultaRelacionadosService
    {
        ICfdiConsultaRelacionadosServiceEmulation _service;
        public CfdiConsultaRelacionadosService()
        {
            string emulation_db = System.Environment.GetEnvironmentVariable("EMULATION_DB");
            RelationsDAO relations;
            ReceptionDAO reception;

            SAT.Core.DL.Database _sql = new SAT.Core.DL.Database(new SQLDatabase(emulation_db));
            reception = new ReceptionDAO(_sql);

            relations = new RelationsDAO(_sql);

            _service = new CfdiConsultaRelacionadosServiceEmulation(relations, reception);
        }
        public ConsultaRelacionados ProcesarRespuesta(PeticionConsultaRelacionados solicitud)
        {
           return _service.ProcesarRespuesta(solicitud);
        }
    }
}
