using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    /// <summary>
    /// IRepository Pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataAccessObjectAsyn<T> where T : IValueObject
    {
        Task<int> CreateAsyn(IDbSession dbSession, T entity);
        Task<bool> UpdateAsyn(IDbSession dbSession, T entity);
        Task<bool> DeleteAsyn(IDbSession dbSession, dynamic Id);
        Task<T> GetAsync(IDbSession dbSession, dynamic id);
        Task<IEnumerable<T>> GetAllAsync(IDbSession dbSession);
        Task<IEnumerable<T>> FindByCriteriaAsync(IDbSession dbSession, string finderType, params object[] criteria);

        /// <summary>
        /// This is a generic function to invoke any function of a DAO.
        /// Note: IDbSession must be the first parameter of  parameters array.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<object> InvokeByMethodNameAsync(IDbSession dbSession, string methodName, params object[] parameters);
    }
}
