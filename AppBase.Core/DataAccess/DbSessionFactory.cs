using AppBase.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppBase.Core.DataAccess
{
    public sealed class DbSessionFactory : FactoryBase<DbSessionFactory>
    {
        public DbSession GetSession(string providerName, string connectionString)
        {
            DbSession session = new DbSession(providerName, connectionString);
            return session;
        }
    }
}
