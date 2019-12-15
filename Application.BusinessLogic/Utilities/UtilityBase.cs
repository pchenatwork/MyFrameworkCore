using Application.BusinessLogic.Workflow;
using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Utilities
{
    public class UtilityBase<T> where T: IValueObject
    {
        //	*************************************************************************
        //				   public methods
        //	*************************************************************************
        public static T CreateObject()
        {
            return ValueObjectFactory<T>.Instance.Create();
        }
        public static bool Update(DbSession session, T entity)
        {
            return ManagerFactory<T>.Instance.GetManager(session).Update(entity);
        }
    }
}
