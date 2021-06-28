using System;
using System.Collections.Generic;
using System.Text;

namespace AppBase.Core.Interfaces
{
    public interface IManager<T> where T : IValueObject
    {
        #region Manager
        IDbSession DbSession { get; }
        IDataAccessObject<T> DAO { set; }
        #endregion Manager

        #region	IPersistence
        /// <summary>
        /// Create an object in the database.
        /// </summary>
        /// <returns></returns>
        int Create(T newObject);
        /// <summary>
        /// Update the object in the database.
        /// </summary>
        /// <param name="existingObject"></param>
        /// <returns></returns>
        bool Update(T existingObject);
        /// <summary>
        /// Remove the object from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        #endregion

        #region	IFinder
        //	*************************************************************************
        //				   public methods
        //	*************************************************************************
        /// <summary>
        /// Get a value object from database by its ID.
        /// </summary>
        T Get(int id);
        /// <summary>
        /// Gets all the value objects.
        /// </summary>
        /// <returns>a valid non-empty collection or null</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Generic search function to get a list of value objects by certain criteria.
        /// </summary>
        /// <param name="finderType">a defined finder type</param>
        /// <param name="criteria">the list of arguments as criteria to the finder method</param>
        /// <returns>a valid non-empty collection or null</returns>
        IEnumerable<T> FindByCriteria(string finderType, object[] criteria);
        #endregion        
    }
}
