using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Repository.Base;

namespace AppCore.Repository
{
    public class Repository<TEntity>:RepositoryBase<TEntity> where TEntity:class,new()
    {
        public Repository(DbContext dbContext) : base(dbContext)
        {
        }

    }
}
