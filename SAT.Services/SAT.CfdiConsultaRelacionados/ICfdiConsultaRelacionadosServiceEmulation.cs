﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CfdiConsultaRelacionados
{
    public interface ICfdiConsultaRelacionadosServiceEmulation
    {
        ConsultaRelacionados ProcesarRespuesta(PeticionConsultaRelacionados solicitud);
    }
}
