using AppBase.Core.Common;
using AppBase.Core.Interfaces;
using AppBase.Core.BusinessLogic;
using Application.DAO;
//using Framework.Core.BusinessLogic;
//using Framework.Core.Common;
//using Framework.Core.DataAccess;
//using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Application.BusinessLogic
{
    public sealed class ManagerFactory<T> : FactoryBase<ManagerFactory<T>> where T : IValueObject
    {
        public IManager<T> GetManager(IDbSession session)
        {
            // get Manager ClassName, should by in the convention of "{{ValueObject}}Manager"
            string name = typeof(T).Name.ToLower() + "manager";

            // Search type in current assembly
            Type type = (from t in Assembly.GetExecutingAssembly().GetTypes()
                         where t.IsClass && t.Name.ToLower().Equals(name)
                         select t).Single();

            object[] args = { session};
            // create instance by reflection
            return Activator.CreateInstance(type,
                 BindingFlags.NonPublic | BindingFlags.Instance, null,
                 args, null) as IManager<T>;
        }

        public IManager<T> GetManager_Old_XX(IDbSession session, string daoClassName = null)
        {
            var dao = DataAccessObjectFactory<T>.Instance.GetDAO(daoClassName);
            object[] args = { session, dao };  // For parametered Constructor 
            try
            {
                return Activator.CreateInstance(typeof(ManagerBase<T>),
                    BindingFlags.NonPublic | BindingFlags.Instance, null,
                    args, null) as IManager<T>;
            }
            catch (TargetInvocationException e)
            {
                //throw new SystemException(e.InnerException.Message, e.InnerException);
                return null;
            }
        }
        ////public IManager<T> GetManager(IDbSession session)
        ////{
        ////    var dao = DataAccessObjectFactory<T>.Instance.GetDAO();

        ////    IManager<T> mgr = null;

        //// //   Type t = Type.GetType("Framework.Core.BusinessLogic.Manager");

        //// //   Type t2 = typeof(Manager<T>);

        ////    try
        ////    {
        ////        mgr = Activator.CreateInstance(typeof(Manager<T>),
        ////            BindingFlags.NonPublic | BindingFlags.Instance , null,
        ////            args, null) as IManager<T>;

        ////    }
        ////    catch (TargetInvocationException e)
        ////    {
        ////        throw new SystemException(e.InnerException.Message, e.InnerException);
        ////    }

        ////    return mgr;

        ////}
    }
}
