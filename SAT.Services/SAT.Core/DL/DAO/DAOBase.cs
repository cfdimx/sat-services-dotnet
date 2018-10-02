using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.DAO
{
    public class DAOBase : IDisposable
    {
        public Database _db;
        public DAOBase(Database db)
        {
            _db = db;
        }

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}
