using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.DAO.Cancelation
{
    public class CancelationDAO: DAOBase
    {
        public CancelationDAO(Database db) :base(db)
        {
        }
        private static Object _lock = new Object();
        private static CancelationDAO _instance = null;

        public static CancelationDAO Instance(Database db)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CancelationDAO(db);
                    }
                }
            }

            return _instance;
        }

        public void StartCancelDocument(string uuid)
        {
            var doc = _db.GetDocumentByUUID(uuid);
            doc.EstatusCancelacion = "En proceso";
            _db.Update(doc);
        }

        public void CancelDocument(string uuid)
        {
            var doc = _db.GetDocumentByUUID(uuid);
            doc.EstatusCancelacion = null;
            doc.Estado = "Cancelado";

            _db.Update(doc);
            var relations = _db.GetRelationsChildren(uuid);
            if (relations != null)
            {
                foreach (var r in relations)
                {
                    _db.DeleteRelations(r);
                }
            }
        }

        public void NormalizeDocument(string uuid)
        {
            var doc = _db.GetDocumentByUUID(uuid);
            doc.EstatusCancelacion = "Solicitud rechazada";
            doc.Estado = "Vigente";
            doc.EsCancelable = "Cancelable con aceptación";
            _db.Update(doc);
        }
    }
}
