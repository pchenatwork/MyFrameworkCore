using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.DataAccess
{
    /// <summary>
    /// From VAS IRepository === DataAccessObject (DAO)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : IValueObject<T>
    {
        int Create(T entity);
        bool Update(T entity);
        bool Delete(dynamic Id);
        T Get(dynamic id);
        IEnumerable<T> GetAll();
        IEnumerable<T> FindByCriteria(string finderType, params object[] criteria);

        /// <summary>
        /// This is a generic function to invoke any function of a DAO.
        /// Note: IDataSource must be the first parameter of  parameters array.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object InvokeByMethodName(string methodName, params object[] parameters);
    }
}
