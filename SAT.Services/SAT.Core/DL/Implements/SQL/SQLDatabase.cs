using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAT.Core.DL.Entities;
using SAT.Core.DL.Implements.SQL.Repository;
using SAT.Core.DL.Implements.SQL.Repository.Entities;

namespace SAT.Core.DL.Implements.SQL
{
    public class SQLDatabase : IDatabase
    {
        private Repository<Document> _db = null;
        private Repository<Relation> _dbRelation = null;
        private Repository<PendingCancelation> _dbPendings = null;
        private Context _cx = null;
        public SQLDatabase(string connString)
        {
            _cx = new Context(connString);
            _db = new Repository<Document>(_cx);
            _dbRelation = new Repository<Relation>(_cx);
            _dbPendings = new Repository<PendingCancelation>(_cx);
        }
        public DocumentBase GetDocument(Guid uuid)
        {

            return _db.GetAll(w => w.UUID == uuid).FirstOrDefault();
        }

        public DocumentBase GetDocumentbyParams(Guid uuid, string total, string rfcReceptor, string rfcEmisor)
        {
            var data = _db.GetAll(w => w.UUID == uuid && w.Total == total && w.RfcReceptor == rfcReceptor && w.RfcEmisor == rfcEmisor);
            return _db.GetAll(w => w.UUID == uuid && w.Total == total && w.RfcReceptor == rfcReceptor && w.RfcEmisor == rfcEmisor).FirstOrDefault();
        }

        public void SaveDocument(IEntity document)
        {
            _db.Insert((Document)document);
        }

        public void Save()
        {
            _db.Save();
        }

        public void UpdateDocument(IEntity document)
        {
            _db.Update((Document)document);
        }

        public void Delete(IEntity document)
        {
            _db.Delete((Document)document);
        }


        public void SaveRelation(IEntity document)
        {
            _dbRelation.Insert((Relation)document);
        }



        public void UpdateRelation(IEntity document)
        {
            _dbRelation.Update((Relation)document);
        }

        public void DeleteRelation(IEntity document)
        {
            _dbRelation.Delete((Relation)document);
        }

        public IEnumerable<Relation> GetRelationsParents(Guid uuid)
        {
            return _dbRelation.GetAll(w => w.UUID == uuid);
        }
        public IEnumerable<Relation> GetRelationsChildren(Guid uuid)
        {
            return _dbRelation.GetAll(w => w.ParentUUID == uuid);
        }

        public void SavePendingCancelation(IEntity pending)
        {
            _dbPendings.Insert((PendingCancelation)pending);
        }

        public void UpdatePendingCancelation(IEntity pending)
        {
            _dbPendings.Update((PendingCancelation)pending);
        }

        public void DeletePendingCancelation(IEntity pending)
        {
            _dbPendings.Delete((PendingCancelation)pending);
        }

        public IEnumerable<PendingCancelation> GetPendingCancelations(string rfcReceptor)
        {
            return _dbPendings.GetAll(w => w.RfcReceptor == rfcReceptor);
        }

        public IEnumerable<PendingCancelation> GetPendingCancelationsByUUID(Guid uuidB)
        {
            return _dbPendings.GetAll(w => w.UUID == uuidB);
        }

        public void Close()
        {
            _cx.Dispose();

        }
    }
}
