using Framework.Core.Common;
using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;

namespace Framework.Core.BusinessLogic
{
    public sealed class ManagerFactory2<T> : FactoryBase<ManagerFactory2<T>> where T : IValueObject
    {
        ////public IManager<T> GetManager(IDbSession session, string daoClassName)
        ////{
        ////    var dao = DaoFactory<T>.Instance.GetDAO(daoClassName);
        ////    return new Manager<T>(session, dao);
        ////}
        ////public IManager<T> GetManager(IDbSession session)
        ////{
        ////    var dao = DataAccessObjectFactory<T>.Instance.GetDAO();

        ////    IManager<T> mgr = null;
        ////    object[] args = { session, dao };  // For parametered Constructor 


        ////    Type t = Type.GetType("Framework.Core.BusinessLogic.Manager");

        ////    try
        ////    {
        ////        mgr = Activator.CreateInstance(Type.GetType("Framework.Core.BusinessLogic.Manager"),
        ////            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null,
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

