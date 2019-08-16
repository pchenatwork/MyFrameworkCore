using Framework.Core.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.DataAccess
{
    public sealed class DbSessionFactory : FactoryBase<DbSessionFactory>
    {
        public IDbSession Create(DbContext dbContext)
        {
            IDbSession session = new DbSession(dbContext);
            return session;
        }
    }
}
