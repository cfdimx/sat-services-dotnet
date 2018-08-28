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
        public SQLDatabase(string connString)
        {
            if(_db == null)
            {
                _db = new Repository<Document>(new Context(connString));
            }
            
        }
        public DocumentBase GetDocument(string uuid)
        {
            return _db.GetById(uuid);
        }

        public void SaveDocument(IDocument document)
        {
            _db.Insert((Document)document);
        }

        public void Save()
        {
            _db.Save();
        }

        public void UpdateDocument(IDocument document)
        {
            _db.Update((Document)document);
        }
    }
}
