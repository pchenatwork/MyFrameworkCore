using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Utilities
{
   public class TimeoffRequestUtility
   {
        #region	Private Members
        // *************************************************************************
        //				 Private Members
        // *************************************************************************
        /// <summary>
        /// logging component
        /// </summary>
       // private static ILog _logger;
        #endregion

        #region	Constructors
        // *************************************************************************
        //				 constructors
        // *************************************************************************
        static TimeoffRequestUtility()
        {
            //_logger = LoggingUtility.GetLogger(typeof(SupplierUtility).FullName);
        }

        private TimeoffRequestUtility()
        {
        }
        #endregion

        #region	Public Methods
        //	*************************************************************************
        //				   public methods
        //	*************************************************************************
        public static BaseSupplier CreateObject()
        {
            SupplierManager supplierManager = (SupplierManager)_supplierManagerFactory.CreateInstance();

            return (BaseSupplier)supplierManager.CreateObject();
        }

        public static bool Create(string dataSourceName, BaseSupplier supplier)
        {
            SupplierManager supplierManager = (SupplierManager)_supplierManagerFactory.CreateInstance(dataSourceName);

            return supplierManager.Create(supplier);
        }

        #endregion
    }
}
