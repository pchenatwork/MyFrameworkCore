using Framework.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Framework.Core.DataAccess
{
    public interface IDbSession : IDisposable
    {
        DbContext DbContext { get; }
        DbConnection DbConnection { get; }
        IRepository<T> GetRepository<T>() where T : IValueObject<T>;
        int SaveChanges();
        int ExecuteSqlCommand(string sql, params object[] paramters);
        IDbContextTransaction BeginTrans();
        void Commit();
        void Rollback();
    }
}
