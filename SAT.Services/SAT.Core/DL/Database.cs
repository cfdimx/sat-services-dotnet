using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAT.Core.DL.Entities;
using SAT.Core.DL.Implements.SQL.Repository;

namespace SAT.Core.DL
{
    public class Database: IDisposable
    {
        private IDatabase _db;

        public Database(IDatabase db)
        {
            _db = db;
        }

        
        public DocumentBase GetDocumentByUUID(Guid uuid)
        {
            return _db.GetDocument(uuid);
        }
        public DocumentBase GetDocument(Guid uuid, string total, string rfcReceptor, string rfcEmisor)
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
        public IEnumerable<Relation> GetRelationsParents(Guid uuid)
        {
            return _db.GetRelationsParents(uuid);
        }

        public IEnumerable<Relation> GetRelationsChildren(Guid uuid)
        {
            return _db.GetRelationsChildren(uuid);
        }
      



        public void SavePending(PendingCancelation pendingCancelation)
        {
            _db.SavePendingCancelation(pendingCancelation);
        }

        public void DeletePending(Guid uuid)
        {
            var pend = _db.GetPendingCancelationsByUUID(uuid).FirstOrDefault() ;
            _db.DeletePendingCancelation(pend);
        }

        public IEnumerable<PendingCancelation> GetPendingCancelations(string rfcReceptor)
        {
            return _db.GetPendingCancelations(rfcReceptor);
        }

        public IEnumerable<PendingCancelation> GetPendingCancelationsByUUID(Guid uuid)
        {
            return _db.GetPendingCancelationsByUUID(uuid);
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.
                _db.Close();
                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        ~Database()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(false);
        }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            GC.SuppressFinalize(this);
        }
        #endregion



    }
}
