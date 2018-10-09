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
        public void StartCancelDocument(Guid uuid)
        {
            var doc = _db.GetDocumentByUUID(uuid);
            doc.EstatusCancelacion = "En proceso";
            _db.Update(doc);
        }

        public void CancelDocument(Guid uuid)
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
            _db.Save();
        }

        public void NormalizeDocument(Guid uuid)
        {
            var doc = _db.GetDocumentByUUID(uuid);
            doc.EstatusCancelacion = "Solicitud rechazada";
            doc.Estado = "Vigente";
            doc.EsCancelable = "Cancelable con aceptación";
            _db.Update(doc);
        }
    }
}
