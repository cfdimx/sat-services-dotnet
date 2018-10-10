using SAT.Core.DL.Entities;
using SAT.Core.DL.Implements.SQL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;



namespace SAT.Core.DL.Implements.SQL.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbSet<T> DataTable;
        private DbContext _dataContext;
        public Repository(DbContext dataContext)
        {
            _dataContext = dataContext;
            DataTable = dataContext.Set<T>();
        }

        #region IRepository<T> Members

        public void Insert(T entity)
        {
            DataTable.Add(entity);
            _dataContext.SaveChanges();
         

        }

        public void Delete(T entity)
        {
            DataTable.Remove(entity);
            //_dataContext.SaveChanges();
        }

     

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dataContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            
        }

        

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
           
            return DataTable.Where(whereCondition);
        }



        #endregion
    }
}
