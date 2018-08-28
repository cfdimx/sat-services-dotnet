using SAT.Core.DL.Implements.SQL.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.Implements.SQL.Repository
{
    public class Context: DbContext
    {
        public Context(string connString):base(connString)
        {

        }
        public DbSet<Document> Documents { get; set; }

       
    }
}
