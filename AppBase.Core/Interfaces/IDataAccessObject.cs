using System;
using System.Collections.Generic;
using System.Text;

namespace AppBase.Core.Interfaces
{
    /// <summary>
    /// IRepository Pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataAccessObject<T> where T: IValueObject
    {
        int Create(IDbSession dbSession, T entity);
        bool Update(IDbSession dbSession, T entity);
        bool Delete(IDbSession dbSession, dynamic Id);
        T Get(IDbSession dbSession, dynamic id);
        IEnumerable<T> GetAll(IDbSession dbSession);
        IEnumerable<T> FindByCriteria(IDbSession dbSession, string finderType, params object[] criteria);

        /// <summary>
        /// This is a generic function to invoke any function of a DAO.
        /// Note: IDbSession must be the first parameter of  parameters array.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object InvokeByMethodName(IDbSession dbSession, string methodName, params object[] parameters);
    }
}
