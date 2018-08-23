﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAT.CfdiConsultaRelacionados;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CfdiConsultaRelacionadosService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CfdiConsultaRelacionadosService.svc or CfdiConsultaRelacionadosService.svc.cs at the Solution Explorer and start debugging.
    public class CfdiConsultaRelacionadosService : ICfdiConsultaRelacionadosService
    {
        ICfdiConsultaRelacionadosServiceEmulation _service;
        public CfdiConsultaRelacionadosService()
        {
            _service = new CfdiConsultaRelacionadosServiceEmulation();
        }
        public ConsultaRelacionados ProcesarRespuesta(PeticionConsultaRelacionados solicitud)
        {
           return _service.ProcesarRespuesta(solicitud);
        }
    }
}
