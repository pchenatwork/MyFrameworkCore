using Framework.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.DataAccess
{
    public sealed class DbSessionFactory : FactoryBase<DbSessionFactory>
    {
        public IDbSession GetSession(string dbName)
        {
            IDbSession session = new DbSession("System.Data.SqlClient", "Data Source = MSSQL1; Initial Catalog = Northwind; Integrated Security = true" );
            return session;
        }
    }
}
