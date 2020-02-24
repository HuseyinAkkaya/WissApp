using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Repository;
using AppCore.Repository.Base;
using AppCore.UnitOfWork.Base;

namespace AppCore.Service.Base
{
    public abstract class ServiceBase<TEntity> : IDisposable where TEntity : class, new()
    {
        private DbContext db;
        protected RepositoryBase<TEntity> repository;
        protected UnitOfWorkBase unitOfWork;

        public ServiceBase(DbContext _db)
        {
            db = _db;
            repository = new Repository<TEntity>(db);
            unitOfWork = new UnitOfWork.UnitOfWork(db);
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                unitOfWork?.Dispose();
                repository?.Dispose();
                db?.Dispose();

            }
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
