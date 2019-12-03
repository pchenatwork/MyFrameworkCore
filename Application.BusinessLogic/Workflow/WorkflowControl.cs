using Application.DataAccess.Workflow;
using Application.ValueObjects.Workflow;
using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public class WorkflowControl
    {
        /// <summary>
        /// Call when TransactionID=0, this will create the first entry in [WorkflowHistory]
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="user"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static int NewTransaction(IDbSession session, int workflowId, string user, string comment = null)
        {
            // Get 'Start' WorkflowNode
            var mgrWorkflowNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session);
            WorkflowNode startNode = (mgrWorkflowNode.FindByCriteria(WorkflowNodeDAO.FIND_BY_WORKFLOWID, new object[] { workflowId }))
                .AsQueryable()
                .Where(i => i.NodeType == "Start").FirstOrDefault(); ;

            WorkflowHistory history = ValueObjectFactory<WorkflowHistory>.Instance.Create();
            history.WorkflowId = workflowId;
            history.NodeId = startNode.Id;
            history.LastUpdateBy = user;
            history.Comment = comment;
            history.IsCurrent = true;

            // Create WorkflowHistory
            var mgrWorkflowHistory = ManagerFactory<WorkflowHistory>.Instance.GetManager(session);
            int id= mgrWorkflowHistory.Create(history);

            // Get TransactionID from newly created WorkflowHistory
            history = mgrWorkflowHistory.Get(id);
            return history.TransactionId;
        }
    }
}
