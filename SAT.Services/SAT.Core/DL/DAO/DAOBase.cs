using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.DAO
{
    public class DAOBase
    {
        public Database _db;
        public DAOBase(Database db)
        {
            _db = db;
        }
    }
}
