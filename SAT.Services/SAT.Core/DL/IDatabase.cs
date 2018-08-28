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
        Document GetDocument(string uuid);
        
    }
}
