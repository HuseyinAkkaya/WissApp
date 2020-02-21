using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Context;

namespace AppCore.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IDisposable where TEntity : class, new()
    {
        protected IDbContext db;
        public RepositoryBase(IDbContext dbContext)
        {
            db = dbContext;
        }


        public virtual List<TEntity> GetEntities()
        {
            try
            {
                return db.Set<TEntity>().ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }











        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //~RepositoryBase()
        //{
        //    Dispose(false);
        //}
        #endregion
    }
}
