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
        public IManager<T> GetManager(IDbSession session, string daoClassName = null)
        {
            var dao = DataAccessObjectFactory<T>.Instance.GetDAO(daoClassName);
            object[] args = { session, dao };  // For parametered Constructor 
            try
            {
                return Activator.CreateInstance(typeof(Manager<T>),
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
