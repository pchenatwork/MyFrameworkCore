using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.BusinessLogic
{
    public interface IManager<T> where T: IValueObject
    {
        #region Manager
        IDbSession dbSession { get;  }
        IRepository<T> dao { get;  }
        #endregion Manager

        T CreateObject();
        /// <returns></returns>
        int Create(T newObject);

        #region	IPersistence
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
        ICollection<T> GetAll();

        /// <summary>
        /// Generic search function to get a list of value objects by certain criteria.
        /// </summary>
        /// <param name="finderType">a defined finder type</param>
        /// <param name="criteria">the list of arguments as criteria to the finder method</param>
        /// <returns>a valid non-empty collection or null</returns>
        ICollection<T> FindByCriteria(string finderType, object[] criteria);
        #endregion        
    }
}
