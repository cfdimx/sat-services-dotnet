using SAT.Core.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.DAO.Pendings
{
    public class PendingsDAO : DAOBase
    {
        public PendingsDAO(Database db) : base(db)
        {

        }

        private static Object _lock = new Object();
        private static PendingsDAO _instance = null;

        public static PendingsDAO Instance(Database db)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new PendingsDAO(db);
                    }
                }
            }

            return _instance;
        }

        public void SendPending(string uuid)
        {
            var doc = _db.GetDocumentByUUID(uuid);
            PendingCancelation pending = new PendingCancelation()
            {
                UUID = doc.UUID,
                RfcReceptor = doc.RfcReceptor,
                FechaSolicitud = DateTime.Now
            };

            _db.SavePending(pending);
        }

        public IEnumerable<PendingCancelation> GetPendings(string rfc)
        {
            return _db.GetPendingCancelations(rfc);
        }

        public PendingCancelation GetPendingByUUID(string uuid)
        {
            return _db.GetPendingCancelationsByUUID(uuid).FirstOrDefault();
        }
        public void DeletePending(string uuid)
        {
            _db.DeletePending(uuid);
        }

        public bool HasPendings(string rfc)
        {
            return _db.GetPendingCancelations(rfc).ToArray().Length != 0;
        }

    }
}
