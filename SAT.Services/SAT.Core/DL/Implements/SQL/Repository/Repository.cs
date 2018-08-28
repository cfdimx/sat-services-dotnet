using SAT.Core.DL.Implements.SQL.Repository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;



namespace SAT.Core.DL.Implements.SQL.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbSet<T> DataTable;

        public Repository(DbContext dataContext)
        {
            DataTable = dataContext.Set<T>();
        }

        #region IRepository<T> Members

        public void Insert(T entity)
        {
            DataTable.Add(entity);
        }

        public void Delete(T entity)
        {
            DataTable.Remove(entity);
        }

      


        public T GetById(string id)
        {

            return DataTable.Where(w => w.UUID == id).FirstOrDefault();
        }

        #endregion
    }
}
