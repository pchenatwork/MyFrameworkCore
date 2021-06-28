using Application.BusinessLogic.Workflow;
using Application.ValueObjects.Workflow;
//using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Utilities
{
//    public class TimeoffUtility : UtilityBase<TimeoffRequest>
//    {
//        #region	Private Members
//        // *************************************************************************
//        //				 Private Members
//        // *************************************************************************
//        /// <summary>
//        /// logging component
//        /// </summary>
//       // private static ILog _logger;
//        #endregion

//        #region	Constructors
//        // *************************************************************************
//        //				 constructors
//        // *************************************************************************
//        static TimeoffUtility()
//        {
//            //_logger = LoggingUtility.GetLogger(typeof(SupplierUtility).FullName);
//        }

//        private TimeoffUtility()
//        {
//        }
//        #endregion

//        #region	Public Methods
//        //	*************************************************************************
//        //				   public methods
//        //	*************************************************************************

//        public static int Create(DbSession session, TimeoffRequest newTimeoffRequest, string WorkflowNote = null)
//        {
//            var WFRunner = WorkflowFactory.Instance.GetWorkflow(WorkflowEnum.TimeoffMainFlow);
//            // Attach the transactionId to TransactionHeader
//            var x = WFRunner.Create(session, newTimeoffRequest.UpdatedBy, WorkflowNote);
//            newTimeoffRequest.TransactionId = x.TransactionId;
//            newTimeoffRequest.StatusId = x.NodeId;

//            return ManagerFactory<TimeoffRequest>.Instance.GetManager(session).Create(newTimeoffRequest);
//        }

//        public static bool Workflow(DbSession session, int transactionId, string ActionName, string user, string note, ref List<string> msg)
//        {
//            return WorkflowFactory.Instance.GetWorkflow(WorkflowEnum.TimeoffMainFlow)
//                .ExecuteAction(session, transactionId, ActionName, user, note, ref msg);
//        }
//        #endregion
//    }
}
