using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AppBase.Core.Interfaces
{
    public interface IDbSession : IDisposable
    {
        IDbConnection DbConnection { get; }
        IDbTransaction Transaction { get; }
        IDbTransaction BeginTrans(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }
}
