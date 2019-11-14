using Application.DataAccess;
using Framework.Core.BusinessLogic;
using Framework.Core.Common;
using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Application.BusinessLogic
{
    public sealed class ManagerFactory<T> : FactoryBase<ManagerFactory<T>> where T : IValueObject
    {
        public IManager<T> GetManager(IDbSession session, string daoClassName)
        {
            //Type t = typeof(T);
            //string daoClass = t.Name.ToLower() + "dao";

            //Type objectType = (from asm in AppDomain.CurrentDomain.GetAssemblies()
            //                   from type in asm.GetTypes()
            //                   where type.IsClass && type.Name.ToLower().Equals(daoClass)
            //                   select type).Single();
            //object obj = Activator.CreateInstance(objectType);


            var dao = DataAccessObjectFactory<T>.Instance.GetDAO(daoClassName);

            return new Manager<T>(session, dao);
        }
        public IManager<T> GetManager(IDbSession session)
        {
            var dao = DataAccessObjectFactory<T>.Instance.GetDAO();

            IManager<T> mgr = null;
            object[] args = { session, dao};  // For parametered Constructor 


            Type t = Type.GetType("Framework.Core.BusinessLogic.Manager");

            try
            {
                mgr = Activator.CreateInstance(Type.GetType("Framework.Core.BusinessLogic.Manager"),
                    BindingFlags.NonPublic | BindingFlags.Instance, null,
                    args, null) as IManager<T>;

            }
            catch (TargetInvocationException e)
            {
                throw new SystemException(e.InnerException.Message, e.InnerException);
            }

            return mgr;

        }
    }
}
