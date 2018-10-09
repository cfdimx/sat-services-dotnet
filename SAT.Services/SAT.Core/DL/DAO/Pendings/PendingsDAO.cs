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

        private DateTime ConvertToMexicanDate(DateTime da)
        {
            da = da.ToUniversalTime();
            TimeZoneInfo setTimeZoneInfo;
            setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTime(da, setTimeZoneInfo);
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

        public void SendPending(Guid uuid)
        {
            var doc = _db.GetDocumentByUUID(uuid);
            PendingCancelation pending = new PendingCancelation()
            {
                UUID = doc.UUID,
                RfcReceptor = doc.RfcReceptor,
                FechaSolicitud = ConvertToMexicanDate(DateTime.Now)
            };

            _db.SavePending(pending);
        }

        public IEnumerable<PendingCancelation> GetPendings(string rfc)
        {
            return _db.GetPendingCancelations(rfc);
        }

        public PendingCancelation GetPendingByUUID(Guid uuid)
        {
            return _db.GetPendingCancelationsByUUID(uuid).FirstOrDefault();
        }
        public void DeletePending(Guid uuid)
        {
            _db.DeletePending(uuid);
            _db.Save();
        }

        public bool HasPendings(string rfc)
        {
            return _db.GetPendingCancelations(rfc).ToArray().Length != 0;
        }

    }
}
