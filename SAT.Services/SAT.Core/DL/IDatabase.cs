using SAT.Core.DL.Entities;
using SAT.Core.DL.Implements.SQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL
{
    public interface IDatabase
    {
        void SaveDocument(IEntity document);
        void UpdateDocument(IEntity document);
        void Save();
        void Delete(IEntity document);
        DocumentBase GetDocument(Guid uuid);
        DocumentBase GetDocumentbyParams(Guid uuid, decimal total, string rfcReceptor, string rfcEmisor);
        void SaveRelation(IEntity document);
        void UpdateRelation(IEntity document);
        void DeleteRelation(IEntity document);
        IEnumerable<Relation> GetRelationsParents(Guid uuid);
        IEnumerable<Relation> GetRelationsChildren(Guid uuid);
        void SavePendingCancelation(IEntity pending);
        void UpdatePendingCancelation(IEntity pending);
        void DeletePendingCancelation(IEntity pending);
        IEnumerable<PendingCancelation> GetPendingCancelations(string rfcReceptor);
        IEnumerable<PendingCancelation> GetPendingCancelationsByUUID(Guid uuidB);
        void Close();
    }
}
