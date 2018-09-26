using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAT.Core.DL.Entities;
using SAT.Core.DL.Implements.SQL.Repository;

namespace SAT.Core.DL
{
    public class Database
    {
        private IDatabase _db;

        public Database(IDatabase db)
        {
            _db = db;
        }

        
        public DocumentBase GetDocumentByUUID(string uuid)
        {
            return _db.GetDocument(uuid);
        }
        public DocumentBase GetDocument(string uuid, string total, string rfcReceptor, string rfcEmisor)
        {
            return _db.GetDocumentbyParams(uuid, total,rfcReceptor, rfcEmisor);
        }
        public void SaveDocument(DocumentBase document)
        {
            _db.SaveDocument(document);
        }

        public void Save()
        {
            _db.Save();
        }

        public void Delete(DocumentBase document)
        {
            _db.Delete(document);
        }

        public void Update(DocumentBase document)
        {
            _db.UpdateDocument(document);
        }


        public void SaveRelation(Relation relation)
        {
            _db.SaveRelation(relation);
        }
        public void DeleteRelations(Relation relation)
        {
            _db.DeleteRelation(relation);
        }
        public IEnumerable<Relation> GetRelationsParents(string uuid)
        {
            return _db.GetRelationsParents(uuid);
        }

        public IEnumerable<Relation> GetRelationsChildren(string uuid)
        {
            return _db.GetRelationsChildren(uuid);
        }
      



        public void SavePending(PendingCancelation pendingCancelation)
        {
            _db.SavePendingCancelation(pendingCancelation);
        }

        public void DeletePending(string uuid)
        {
            var pend = _db.GetPendingCancelationsByUUID(uuid).FirstOrDefault() ;
            _db.DeletePendingCancelation(pend);
        }

        public IEnumerable<PendingCancelation> GetPendingCancelations(string rfcReceptor)
        {
            return _db.GetPendingCancelations(rfcReceptor);
        }

        public IEnumerable<PendingCancelation> GetPendingCancelationsByUUID(string uuid)
        {
            return _db.GetPendingCancelationsByUUID(uuid);
        }



    }
}
