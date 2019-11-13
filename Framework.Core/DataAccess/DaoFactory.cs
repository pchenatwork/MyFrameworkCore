using Framework.Core.Common;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.Core.DataAccess
{
    public class DaoFactory<T> : FactoryBase<DaoFactory<T>> where T : IValueObject
    {
        public IRepository<T> GetDAO(string daoClassName)
        {
            Type type = Type.GetType(daoClassName, true);
            // return (IRepository<T>) Activator.CreateInstance(type);

            //           var a = new Application.DataAccessObject.Workflow.WorkflowListDAO();  
            //         var x = Activator.CreateInstance(Type.GetType(daoClassName), daoClassName);
            return Activator.CreateInstance(Type.GetType(daoClassName),
                     BindingFlags.NonPublic | BindingFlags.Instance, null,
                     null, null) as IRepository<T>;
        }
    }
}

