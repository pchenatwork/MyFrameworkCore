using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Framework.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.Core.DataAccess
{
    public class DbSession : IDbSession
    {
        #region Private members
        private DbContext _dbContext;
        //private DbConnection _dbConnection;
        private IDbContextTransaction _transaction;
        private bool disposed = false;
        private Dictionary<Type, object> repositories;
        #endregion private members

        #region Constructor
        public DbSession(DbContext context)
        {
            _dbContext = context;
        }
        #endregion Constructor

        public DbContext DbContext
        {
            get => _dbContext;
        }

        public DbConnection DbConnection
        {
            get => _dbContext.Database.GetDbConnection();
        }

        public IDbContextTransaction BeginTrans()
        {
            _transaction = _dbContext.Database.BeginTransaction();
            return _transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (repositories != null)
                    {
                        repositories.Clear();
                    }
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public int ExecuteSqlCommand(string sql, params object[] paramters)
        {
            throw new NotImplementedException();
        }

        public IRepository<T> GetRepository<T>() where T : IValueObject<T>
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = null;
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
