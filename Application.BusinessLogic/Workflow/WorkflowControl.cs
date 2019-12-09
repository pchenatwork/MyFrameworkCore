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
            var NodeManager = ManagerFactory<WorkflowNode>.Instance.GetManager(session);
            WorkflowNode startNode = (NodeManager.FindByCriteria(WorkflowNodeDAO.FIND_BY_WORKFLOWID, new object[] { workflowId }))
                .AsQueryable()
                .Where(i => i.NodeType == "Start").FirstOrDefault(); ;

            WorkflowHistory history = ValueObjectFactory<WorkflowHistory>.Instance.Create();
            history.WorkflowId = workflowId;
            history.NodeId = startNode.Id;
            history.LastUpdateBy = user;
            history.Comment = comment;
            history.IsCurrent = true;

            // Create WorkflowHistory
            var HistManager = ManagerFactory<WorkflowHistory>.Instance.GetManager(session);
            int id= HistManager.Create(history);

            // Get TransactionID from newly created WorkflowHistory
            return HistManager.Get(id);
        }
        
        /// <summary>
        /// DoActionNode will move forward the workflow Status from FromNodeId to ToNodeId
        /// </summary>
        /// <param name="session"></param>
        /// <param name="transactionid"></param>
        /// <param name="actionNode"></param>
        /// <param name="user"></param>
        /// <param name="comment"></param>
        /// <param name="msgJson"></param>
        /// <returns></returns>
        public static WorkflowHistory DoActionNode(IDbSession session, int transactionid, WorkflowNode actionNode, string user, string comment, ref string msgJson)
        { 
            // replace actionNode with Key ActionNode (ActionNode Status->Status)
            Collection<string> errs = new Collection<string>();

            WorkflowNode keyNode = GetActionKeyNode(session, actionNode);
            
            var HistoryManager = ManagerFactory<WorkflowHistory>.Instance.GetManager(session);

            var hist = HistoryManager.FindByCriteria(WorkflowHistoryDAO.FIND_BY_TRAN, new object[] { transactionid });
      
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
                msgJson = errs.ToJson();
                return null; // 
            }    
                       
            bool TransactionExists = session.Transaction != null;

            if (!TransactionExists) session.BeginTrans(); // Start the transaction if not in one ye

            // Find the existing current WorkflowHistory from Key Action Node
            WorkflowHistory currhist =  HistoryManager
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
                HistoryManager.Update(currhist);
            }

            /// Create workflowhistory records
            WorkflowHistory newHist = ValueObjectFactory<WorkflowHistory>.Instance.Create();
            newHist.TransactionId = transactionid; // currhist.TransactionId;
            newHist.WorkflowId = toNode.WorkflowId; // currhist.WorkflowId;
            newHist.NodeId = toNode.Id; //.NodeToId;
            newHist.PrevHistoryId = currhist.Id;
            newHist.IsCurrent = true;
            newHist.Comment = comment;
            newHist.CreateBy = newHist.LastUpdateBy = user;
            newHist.CreateDate = newHist.LastUpdateDate = DateTime.Now;

            newHist.Id = HistoryManager.Create(newHist);

            if (!TransactionExists) session.Commit();


            string xxx = string.Empty;
            DoStatusNode(session, transactionid, toNode.Id, user, ref xxx);

            return newHist;
        }
       
        public static bool DoStatusNode(IDbSession session, int tranId, int nodeId,  string user, ref string msgJson)
        {
            Collection<string> errs = new Collection<string>();
            // 1 Execute Action defines in [WorkflowAction]
            // This should be virtual function that is specific to each workflow


            // 2 execute auto-ActionNodes(IsAuto==TRUE) that NodeFromId==nodeId, if there is any
            var NodeManager = ManagerFactory<WorkflowNode>.Instance.GetManager(session);
            var ActionNodes = NodeManager.GetAll().Where(i => (i.NodeFromId == nodeId && i.IsAuto == true));
            foreach(WorkflowNode node in ActionNodes)
            {
                string jsonOutput = string.Empty;
                var x = DoActionNode(session, tranId, node, user, "(Auto)", ref jsonOutput);
            }

            return true;
        }

        /// <summary>
        /// Given an Action KeyNode, find all linked Auto Actions
        /// </summary>
        /// <param name="session"></param>
        /// <param name="keyNode"></param>
        /// <returns></returns>
        public static IEnumerable<WorkflowNode> GetActionNodeGroup(IDbSession session, WorkflowNode keyNode)
        {
            //var keyNode = Get_Key_ActionNode(session, ActionNode);
            var NodeManager = ManagerFactory<WorkflowNode>.Instance.GetManager(session);
            // All ActionNode pointing to KEY ActionNOde
            var x = NodeManager.GetAll().Where(i => (i.NodeToId == keyNode.Id && i.IsAuto == true) || i.Id==keyNode.Id);
            return x; 
        }        
        /// <summary>
        /// Given an Action Node, find the Key Node (From Status to Status)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="actionNode"></param>
        /// <returns></returns>
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
        public static WorkflowNode GetActionKeyNode2(IDbSession session, int actionNodeId)
        {
            /* -------------------------------------------
             * Recurrsive function to get the KEY ActionNode (defined as "Status->Status")
             * ------------------------------------------- */
            WorkflowNode node = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(actionNodeId);
            if (node.NodeType.Equals("Action")) {
                return GetActionKeyNode2(session, node.NodeToId);
            } else
            {
                return node;
            }
        }
    }
}
