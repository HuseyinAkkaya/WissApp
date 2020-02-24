using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AppCore.Context;

namespace AppCore.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IDisposable where TEntity : class, new()
    {
        private DbContext db;
        public RepositoryBase(DbContext dbContext)
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

        public virtual List<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return db.Set<TEntity>().Where(predicate).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        public virtual IQueryable<TEntity> GetEntityQuary()
        {
            try
            {
                return db.Set<TEntity>().AsQueryable();
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        public virtual IQueryable<TEntity> GetEntityQuary(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return db.Set<TEntity>().AsQueryable().Where(predicate);
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        public virtual IQueryable<TEntity> GetEntityQuery(string [] entitesToInclude)
        {
            try
            {
                DbQuery<TEntity> entityQuery = db.Set<TEntity>();
                foreach (string entityToInclude in entitesToInclude)
                {
                    entityQuery = entityQuery.Include(entityToInclude);
                }

                return entityQuery;
            }
            catch (Exception exc)
            { 
                throw exc;
            }
        }

        public virtual TEntity GetEntity(int id)
        {
            try
            {
                return db.Set<TEntity>().Find(id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public virtual TEntity GetEntity(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return db.Set<TEntity>().SingleOrDefault(predicate);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Gönderilen Entity Veritabanına eklenir.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddEntity(TEntity entity)
        {
            try
            {
                db.Set<TEntity>().Add(entity);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        /// <summary>
        /// Gönderilen Entity veritabanda güncellenir.
        /// Entity veritabanında bir kayda karşılık gelen bir Id'ye sahip olmalıdır.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void UpdateEntity(TEntity entity)
        {
            try
            {
                db.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Id numarası verilen Entity "IsDeleted" sütununa sahipse
        /// bu sütun "True" olarak güncellenir.
        /// "IsDeleted" sütunu yoksa kaydı tamamen silinir.
        /// </summary>
        /// <param name="id"></param>
        public virtual void DeleteEntity(int id)
        {
            try
            {
                var entity = GetEntity(id);
                DeleteEntity(entity);

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Gönderilen Entity "IsDeleted" sütununa sahipse
        /// bu sütun "True" olarak güncellenir.
        /// "IsDeleted" sütunu yoksa kaydı tamamen silinir.
        /// </summary>
        /// <param name="id"></param>
        public virtual void DeleteEntity(TEntity entity)
        {
            try
            {
                if (entity.GetType().GetProperty("IsDeleted") != null)
                {
                    TEntity _entity = entity;
                    _entity.GetType().GetProperty("IsDeleted")?.SetValue(_entity, true);
                    UpdateEntity(_entity);
                }
                else
                {
                    db.Set<TEntity>().Remove(entity);
                }
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
