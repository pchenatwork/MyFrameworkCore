using Framework.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.DataAccess
{
    public sealed class DbSessionFactory : FactoryBase<DbSessionFactory>
    {
        public IDbSession GetSession(string providerName, string connectionString)
        {
            IDbSession session = new DbSession(providerName, connectionString);
            return session;
        }
    }
}
