using System;
using System.Collections.Generic;
using System.Configuration;
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
            string emulation_db = System.Environment.GetEnvironmentVariable("EMULATION_DB");
            string emulation_sas = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(System.Environment.GetEnvironmentVariable("EMULATION_SAS")));
            RelationsDAO relations = RelationsDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            ReceptionDAO reception = ReceptionDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            _service = new RecibeServiceEmulation(relations, reception, emulation_sas);
        }
        public AcuseRecepcion Recibe(CFDI cFDI)
        {
            return _service.Recibe(cFDI);
        }
    }
}
