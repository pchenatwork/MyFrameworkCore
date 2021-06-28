//using Framework.Core.Common;
//using Framework.Core.DataAccess;
//using Framework.Core.ValueObjects;
using AppBase.Core.Common;
using AppBase.Core.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace Application.DAO
{
    public sealed class DataAccessObjectFactory<T> : FactoryBase<DataAccessObjectFactory<T>> where T : IValueObject
    {
        /// <summary>
        /// if daoClassName not provided, daoClassName= {{T name}}dao
        /// </summary>
        /// <param name="daoClassName"></param>
        /// <returns></returns>
        public IDataAccessObject<T> GetDAO(string daoClassName = null)
        {
            if (string.IsNullOrEmpty(daoClassName))
            {
                // get DaoClassName, should by in the convention of "{ValueObject}DAO"
                daoClassName = typeof(T).Name.ToLower() + "dao";
                // Search type in current assembly
                Type type = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                where t.IsClass && t.Name.ToLower().Equals(daoClassName)
                                select t).Single();
                // create instance by reflection
                return Activator.CreateInstance(type,
                     BindingFlags.NonPublic | BindingFlags.Instance, null,
                     null, null) as IDataAccessObject<T>;
            }

            return Activator.CreateInstance(Type.GetType(daoClassName),
                     BindingFlags.NonPublic | BindingFlags.Instance, null,
                     null, null) as IDataAccessObject<T>;
        }

        ////public IRepository<T> GetDAO()
        ////{
        ////    string daoClassName = typeof(T).Name.ToLower() + "dao";
        ////    var a = System.Reflection.Assembly.GetExecutingAssembly();

        ////    Type typeObj = (from type in Assembly.GetExecutingAssembly().GetTypes()
        ////                    where type.IsClass && type.Name.ToLower().Equals(daoClassName)
        ////                    select type).Single();
        ////    ////Type typeObj = (from asm in AppDomain.CurrentDomain.GetAssemblies()
        ////    ////                from type in asm.GetTypes()
        ////    ////                where type.IsClass && type.Name.ToLower().Equals(daoClassName)
        ////    ////                select type).Single();

        ////    return Activator.CreateInstance(typeObj,
        ////             BindingFlags.NonPublic | BindingFlags.Instance, null,
        ////             null, null) as IRepository<T>;
        ////}

    }
}
