using Framework.Core.Common;
using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;
using System;
using System.Linq;
using System.Reflection;

namespace Application.DataAccess.Dapper
{
    public class DapperDaoFactory<T> : FactoryBase<DapperDaoFactory<T>> where T : IValueObject
    {
        public IRepository<T> GetDAO(string daoClassName = null)
        {
            if (string.IsNullOrEmpty(daoClassName))
            {
                // get DaoClassName, should by in the convention of "{ValueObject}DAO"
                daoClassName = typeof(T).Name.ToLower() + "dao";
                // Search type in current assembly
                Type typeObj = (from type in Assembly.GetExecutingAssembly().GetTypes()
                                where type.IsClass && type.Name.ToLower().Equals(daoClassName)
                                select type).Single();
                // create instance by reflection
                return Activator.CreateInstance(typeObj,
                     BindingFlags.NonPublic | BindingFlags.Instance, null,
                     null, null) as IRepository<T>;
            }

            return Activator.CreateInstance(Type.GetType(daoClassName),
                     BindingFlags.NonPublic | BindingFlags.Instance, null,
                     null, null) as IRepository<T>;
        }
    }
}
