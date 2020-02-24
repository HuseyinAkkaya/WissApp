using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Repository;
using AppCore.Repository.Base;
using AppCore.UnitOfWork.Base;

namespace AppCore.UnitOfWork
{
    public class UnitOfWork:UnitOfWorkBase
    {
        public UnitOfWork(DbContext _db) : base(_db)
        {
        }

    }
}
