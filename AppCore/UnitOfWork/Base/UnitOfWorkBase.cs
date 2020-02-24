using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Repository.Base;

namespace AppCore.UnitOFWork.Base
{
    public abstract class UnitOfWorkBase : IDisposable
    {
        protected DbContext db;

        public UnitOfWorkBase(DbContext _db)
        {
            db = _db;
        }

       
        public virtual int SaveChanges()
        {
            try
            {
                int result = db.SaveChanges();
                return result;
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
