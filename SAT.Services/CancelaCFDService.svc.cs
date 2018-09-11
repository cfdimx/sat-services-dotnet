using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SAT.CancelaCFD;
using SAT.Core.DL.DAO.Cancelation;
using SAT.Core.DL.DAO.Pendings;
using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.DAO.Relations;
using SAT.Core.DL.Implements.SQL;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CancelaCFDBinding : ICancelaCFDBinding
    {
        SAT.CancelaCFD.ICancelaCFDBindingEmulation _service;
        public CancelaCFDBinding()
        {
            string emulation_db = System.Environment.GetEnvironmentVariable("EMULATION_DB");
            CancelationDAO cancelation = CancelationDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            PendingsDAO pendings = PendingsDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            ReceptionDAO reception = new ReceptionDAO(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            RelationsDAO relations = new RelationsDAO(new SAT.Core.DL.Database(new SQLDatabase(emulation_db)));
            _service = new CancelaCFDBindingEmulation(cancelation,pendings, reception, relations);
        }
        public Acuse CancelaCFD(Cancelacion Cancelacion)
        {
            return _service.CancelaCFD(Cancelacion);
        }
    }
}
