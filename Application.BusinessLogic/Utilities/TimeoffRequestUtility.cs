using Application.BusinessLogic.Workflow;
using Application.ValueObjects.Workflow;
using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Utilities
{
    public class TimeoffRequestUtility : UtilityBase<TimeoffRequest>
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

        public static int Create(DbSession session, TimeoffRequest newTimeoffRequest, string WorkflowNote = null)
        {
            var WFRunner = WorkflowFactory.Instance.GetWorkflow(WorkflowListEnum.TimeoffWorkflow);
            newTimeoffRequest.TransactionId = WFRunner.Create(session, newTimeoffRequest.LastUpdateBy, WorkflowNote);

            return ManagerFactory<TimeoffRequest>.Instance.GetManager(session).Create(newTimeoffRequest);
        }
        public static bool Update(DbSession session, TimeoffRequest request)
        {
            return ManagerFactory<TimeoffRequest>.Instance.GetManager(session).Update(request);
        }
        public static bool Workflow(DbSession session, TimeoffRequest newTimeoffRequest, string ActionName, string User, string Note, ref List<string> msg)
        {
            return false;
        }
        #endregion
    }
}
