using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.DAO.Relations;
using SAT.Core.DL.Implements.SQL;
using SAT.RecibeCFDI;

namespace SAT.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "RecibeCFDIService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione RecibeCFDIService.svc o RecibeCFDIService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class RecibeCFDIService : IRecibeCFDIService
    {
        private IRecibeServiceEmulation _service;
        public RecibeCFDIService()
        {
            RelationsDAO relations = RelationsDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(Environment.GetEnvironmentVariable("EMULATION_DB"))));
            ReceptionDAO reception = ReceptionDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(Environment.GetEnvironmentVariable("EMULATION_DB"))));
            _service = new RecibeServiceEmulation(relations, reception, Environment.GetEnvironmentVariable("EMULATION_SAS"));
        }
        public AcuseRecepcion Recibe(CFDI cFDI)
        {
            return _service.Recibe(cFDI);
        }
    }
}
