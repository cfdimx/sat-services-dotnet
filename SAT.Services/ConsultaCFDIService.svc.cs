﻿using System;
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
            ReceptionDAO reception = new ReceptionDAO(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            CancelationDAO cancelation = new CancelationDAO(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            PendingsDAO pendings = new PendingsDAO(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            RelationsDAO relations = new RelationsDAO(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            _service = new ConsultaCFDIServiceEmulation(reception,cancelation, pendings, relations);
        }
        public Acuse Consulta(string expresionImpresa)
        {
            return _service.Consulta(expresionImpresa);
        }
    }
}
