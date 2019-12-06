using Application.DataAccess.Workflow;
using Application.ValueObjects.Workflow;
using Framework.Core.DataAccess;
using Framework.Core.Utilities;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static WorkflowHistory DoActionNode(IDbSession session, int transactionid, WorkflowNode actionNode, string user, string comment, ref string msg)
        {
            // replace actionNode with Key ActionNode (ActionNode Status->Status)
            Collection<string> errs = new Collection<string>();

            WorkflowNode keyNode = GetActionKeyNode(session, actionNode);
            
            var managerHist = ManagerFactory<WorkflowHistory>.Instance.GetManager(session);

            var hist = managerHist.FindByCriteria(WorkflowHistoryDAO.FIND_BY_TRAN, new object[] { transactionid });
      
            foreach(WorkflowNode node in GetActionNodeGroup(session, keyNode))
            {
                // if [History] doesn't have a ActionNode.FromID, then it is invalid operation.
                if (!hist.Any(i => i.NodeId == node.NodeFromId && i.IsCurrent))
                {
                    errs.Add("Transaction: " + transactionid.ToString() + " Workflow: " + node.WorkflowId.ToString() + " Action: '" + node.Name + "' failed");
                }
            }

            if (errs.Count > 0) // Not all workflows are ready to move ahead
            {
                msg = errs.ToJson();
                return null; // 
            }    
                       
            bool wasInTransaction = session.Transaction == null;

            if (!wasInTransaction) session.BeginTrans(); // Start the transaction if not in one ye

            // Find the existing current WorkflowHistory from Key Action Node
            WorkflowHistory currhist =  managerHist
                .FindByCriteria(WorkflowHistoryDAO.FIND_BY_TRAN, new object[] { transactionid, keyNode.WorkflowId })
                .AsQueryable().Where(p=>p.IsCurrent).FirstOrDefault();
            // If existing current doesn't match to actionNode.FromId, invalid operation, then exit
            // if (currhist == null || currhist.NodeId != actionNode.NodeFromId) return newHist;

            WorkflowNode toNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(keyNode.NodeToId);
            if (toNode.WorkflowId == currhist.WorkflowId)
            {
                // if same workflow, put into history, otherwise stays current
                currhist.ApprovalUser = currhist.LastUpdateBy = user;
                currhist.ApprovalDate = currhist.LastUpdateDate = DateTime.Now;
                currhist.IsCurrent = false;
                managerHist.Update(currhist);
            }

            /// Create workflowhistory records
            WorkflowHistory newHist = ValueObjectFactory<WorkflowHistory>.Instance.Create();
            newHist.TransactionId = currhist.TransactionId;
            newHist.WorkflowId = toNode.WorkflowId; // currhist.WorkflowId;
            newHist.NodeId = toNode.Id; //.NodeToId;
            newHist.PrevHistoryId = currhist.Id;
            newHist.IsCurrent = true;
            newHist.Comment = comment;
            newHist.CreateBy = newHist.LastUpdateBy = user;
            newHist.CreateDate = newHist.LastUpdateDate = DateTime.Now;

            newHist.Id = managerHist.Create(newHist);

            if (!wasInTransaction) session.Commit();

            return newHist;
        }
        public static IEnumerable<WorkflowNode> GetActionNodeGroup(IDbSession session, WorkflowNode keyNode)
        {
            //var keyNode = Get_Key_ActionNode(session, ActionNode);
            var manager = ManagerFactory<WorkflowNode>.Instance.GetManager(session);
            // All ActionNode pointing to KEY ActionNOde
            var x = manager.GetAll().AsQueryable().Where(i => (i.NodeToId == keyNode.Id && i.IsAuto == true) || i.Id==keyNode.Id);
            return x; 
        }        

        public static WorkflowNode GetActionKeyNode(IDbSession session, WorkflowNode actionNode)
        {
            /* -------------------------------------------
             * Recurrsive function to get the KEY ActionNode (defined as "Status->Status")
             * ------------------------------------------- */
            if (!actionNode.NodeType.Equals("Action")) return null;

            WorkflowNode toNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(actionNode.NodeToId);

            if (toNode.NodeType.Equals("Action"))
                return GetActionKeyNode(session, toNode);
            else // toNode is to a status node
                return actionNode;
        }
    }
}
