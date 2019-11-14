using Framework.Core.Common;
using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Application.DataAccess
{
    public sealed class DataAccessObjectFactory<T> : FactoryBase<DataAccessObjectFactory<T>> where T : IValueObject
    {
        public IRepository<T> GetDAO(string daoClassFullName)
        {
            return Activator.CreateInstance(Type.GetType(daoClassFullName),
                     BindingFlags.NonPublic | BindingFlags.Instance, null,
                     null, null) as IRepository<T>;
        }

        public IRepository<T> GetDAO()
        {
            string daoClassName = typeof(T).Name.ToLower() + "dao";
            Type typeObj = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                               from type in asm.GetTypes()
                               where type.IsClass && type.Name.ToLower().Equals(daoClassName)
                               select type).Single();

            return Activator.CreateInstance(typeObj,
                     BindingFlags.NonPublic | BindingFlags.Instance, null,
                     null, null) as IRepository<T>;
        }

    }
}
