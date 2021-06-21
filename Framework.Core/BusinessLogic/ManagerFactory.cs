using Framework.Core.Common;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.BusinessLogic
{
    public sealed class ManagerFactory2<T> : FactoryBase<ManagerFactory2<T>> where T: IValueObject
    {
        #region	Constructors
        static ManagerFactory2()
        {
         ///    _logger = LoggingUtility.GetLogger(typeof(ManagerFactory<T>).FullName);
        }
        #endregion Constructors

        #region	Public Methods
        /// <summary>
        /// Factory creation method for Manager instances
        /// </summary>
        /// <returns>
        /// an instance of Manager
        /// </returns>
        public IManager<T> GetManager()
        {
            return (IManager<T>)Activator.CreateInstance(typeof(IManager<T>));
        }

        /// <summary>
        /// Factory creation method for Manager instances
        /// </summary>
        /// <returns>
        /// an instance of Manager
        /// </returns>
        public IManager<T> GetManager(string dataSourceName)
        {
            return (IManager<T>)Activator.CreateInstance(typeof(IManager<T>), new object[] { dataSourceName });
            //return (T)Activator.CreateInstance(typeof(T),
            //    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
            //    new object[] { dataSourceName });
        }
        #endregion Public Methods

    }
}
