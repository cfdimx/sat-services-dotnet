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
        private Repository<DocumentSQL> _db = null;
        public SQLDatabase(string connString)
        {
            if(_db == null)
            {
                _db = new Repository<DocumentSQL>(new Context(connString));
            }
            
        }
        public Document GetDocument(string uuid)
        {
            return _db.GetById(uuid);
        }

        public void SaveDocument(IEntity document)
        {
            _db.Insert((DocumentSQL)document);
        }
    }
}
