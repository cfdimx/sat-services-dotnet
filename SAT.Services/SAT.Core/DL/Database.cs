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
        public DocumentBase GetDocument(string uuid)
        {
            return _db.GetDocument(uuid);
        }

        public void SaveDocument(DocumentBase document)
        {
            _db.SaveDocument(document);
        }

        public void Save()
        {
            _db.Save();
        }
    }
}
