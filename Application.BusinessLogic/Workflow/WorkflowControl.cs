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
        public static WorkflowHistory NewTransaction(IDbSession session, int workflowId, string user, string comment = null)
        {
            // Get 'Start' WorkflowNode
            var mgr_WorkflowNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session);
            WorkflowNode startNode = (mgr_WorkflowNode.FindByCriteria(WorkflowNodeDAO.FIND_BY_WORKFLOWID, new object[] { workflowId }))
                .AsQueryable()
                .Where(i => i.NodeType == "Start").FirstOrDefault(); ;

            WorkflowHistory history = ValueObjectFactory<WorkflowHistory>.Instance.Create();
            history.WorkflowId = workflowId;
            history.NodeId = startNode.Id;
            history.LastUpdateBy = user;
            history.Comment = comment;
            history.IsCurrent = true;

            // Create WorkflowHistory
            var mgr_WorkflowHistory = ManagerFactory<WorkflowHistory>.Instance.GetManager(session);
            int id= mgr_WorkflowHistory.Create(history);

            // Get TransactionID from newly created WorkflowHistory
            return mgr_WorkflowHistory.Get(id);
        }
        public static WorkflowHistory GetActiveNode(IDbSession session, int workflowId, int tranId)
        {
            //?????
            return ValueObjectFactory<WorkflowHistory>.Instance.Create();
        }

        /// return destination StatusNodeID, or 0 for failed
        public static int ExecAction(int WorkflowId, int TranId, string ActionName, ref string errmsg)
        {
            /****
            // 1. get FromNodeId from ActionName
            //      If lastest NodeId in [WorkflowHistory] doesn't match to FromNodeId, invalid operation, exit
            //      If lastest NodeId in [WorkflowHistory] matched to FromNodeId,
            //          1) Execution the Actions
                            if there is incoming or outgoing actions, execut
            
            //          2) update [WorkflowHistory]
            ****/
            return -1;
        }

        private static WorkflowHistory NextStatus(IDbSession session, WorkflowNode actionNode, int transactionid, string user, string comment)
        {
            WorkflowHistory newHist = ValueObjectFactory<WorkflowHistory>.Instance.Create();

            // Make sure actionNode.ToNode is an StatusNode
            WorkflowNode toNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(actionNode.NodeToId);
            if (toNode.NodeType.Equals("Action")) return newHist;  // skip the rest if is to an "ActionNode"

            bool wasInTransaction = session.Transaction == null;

            if (!wasInTransaction) session.BeginTrans(); // Start the transaction if not in one ye

            var manager = ManagerFactory<WorkflowHistory>.Instance.GetManager(session);

            // Find the existing current WorkflowHistory
            WorkflowHistory currhist =  manager
                .FindByCriteria(WorkflowHistoryDAO.FIND_BY_TRAN, new object[] { transactionid, actionNode.WorkflowId })
                .AsQueryable().Where(p=>p.IsCurrent).FirstOrDefault();
            // If existing current doesn't match to actionNode.FromId, invalid operation, then exit
            if (currhist == null || currhist.NodeId != actionNode.NodeFromId) return newHist; 

            currhist.ApprovalUser = currhist.LastUpdateBy = user;
            currhist.ApprovalDate = currhist.LastUpdateDate = DateTime.Now;
            currhist.IsCurrent = false;
            currhist.LastUpdateBy = user;
            currhist.LastUpdateDate = DateTime.Now;

            newHist.TransactionId = currhist.TransactionId;
            newHist.WorkflowId = currhist.WorkflowId;
            newHist.NodeId = actionNode.NodeToId;
            newHist.PrevHistoryId = currhist.Id;
            newHist.IsCurrent = true;
            newHist.Comment = comment;
            newHist.CreateBy = newHist.LastUpdateBy = user;
            newHist.CreateDate = newHist.LastUpdateDate = DateTime.Now;

            manager.Update(currhist);
            newHist.Id = manager.Create(newHist);

            if (!wasInTransaction) session.Commit();

            return newHist;
        }

    }
}
