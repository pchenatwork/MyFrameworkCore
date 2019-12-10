using Application.DataAccess.Workflow;
using Application.ValueObjects.Workflow;
using Framework.Core.BusinessLogic;
using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public abstract class WorkflowBase : IWorkflowable
    {
        /// Return TransactionId.
        public int NewWorkflow(IDbSession session, int workflowId, string user)
        {
            // Get 'Start' WorkflowNode
            var NodeManager = ManagerFactory<WorkflowNode>.Instance.GetManager(session);
            WorkflowNode startNode = (NodeManager.FindByCriteria(WorkflowNodeDAO.FIND_BY_WORKFLOWID, new object[] { workflowId }))
                .Where(i => i.NodeType == "Start").FirstOrDefault(); 

            WorkflowHistory history = ValueObjectFactory<WorkflowHistory>.Instance.Create();
            history.WorkflowId = workflowId;
            history.NodeId = startNode.Id;
            history.LastUpdateBy = user;
            //history.Comment = comment;
            history.IsCurrent = true;

            // Create WorkflowHistory
            var HistManager = ManagerFactory<WorkflowHistory>.Instance.GetManager(session);
            int id = HistManager.Create(history);

            // Get TransactionID from newly created WorkflowHistory
            return HistManager.Get(id).TransactionId;
        }
        public bool ExecuteAction(IDbSession session, int WorkflowId, int TransactionId, string ActionName, string User, string Note, ref List<string> msg)
        {
            bool bOK = true; // Assume OK;

            // Step 1, Get ActionNodes from ActionName
            var actionNodes = ManagerFactory<WorkflowNode>.Instance.GetManager(session)
                .FindByCriteria(WorkflowNodeDAO.FIND_BY_ACTIONNAME, new object[] { WorkflowId, ActionName });
            foreach(WorkflowNode node in actionNodes)
            {
               bOK = bOK && _doActionNode(session, TransactionId, node, User, Note, ref msg);

            }
            return bOK;
        }


        #region Private Methods
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
        private bool _doActionNode(IDbSession session, int transactionid, WorkflowNode actionNode, string user, string comment, ref List<string> msgs)
        {
            bool bRunning = true; // Assume Successed

            var histManager = ManagerFactory<WorkflowHistory>.Instance.GetManager(session);
            var nodeManager = ManagerFactory<WorkflowNode>.Instance.GetManager(session);

            try             
            {
                #region Step 1 Check if the operation applies to current status
                /* -------------------------------------------- 
                 *  Step 1: Check if the operation is valid  *
                 ---------------------------------------------- */

                WorkflowNode keyNode = _getActionKeyNode(session, actionNode); // replace actionNode with Key ActionNode (ActionNode Status->Status)

                var hist = histManager.FindByCriteria(WorkflowHistoryDAO.FIND_BY_TRAN, new object[] { transactionid });

                foreach (WorkflowNode node in _getActionNodeGroup(session, keyNode))
                {
                    // if [History] doesn't have a ActionNode.FromID, then it is invalid operation.
                    if (!hist.Any(i => i.NodeId == node.NodeFromId && i.IsCurrent))
                    {
                        msgs.Add("Not A Valid Operation -- Transaction: " + transactionid.ToString() + " Workflow: " + node.WorkflowId.ToString() + " Action: '" + node.Name + "'");
                    }
                }

                if (msgs.Count > 0) // Not all workflows are ready to move ahead
                {
                    return false; 
                }

                #endregion Step 1

                /* -------------------------------------------- 
                 *  Step 2: Create [WorkflowHistory] record, update status in both [WorkflowHistory] and [{{WorkflowHeader}}] table
                 ---------------------------------------------- */
                #region Step 2 Update Status in both [WorkflowHistory] and [{{CustomWorkflowHeaderTable}}]
                bool TransactionExists = session.Transaction != null;

                if (!TransactionExists) session.BeginTrans(); // Start the transaction if not in one yet

                // Find the existing current WorkflowHistory from Key Action Node
                WorkflowHistory currhist = histManager
                    .FindByCriteria(WorkflowHistoryDAO.FIND_BY_TRAN, new object[] { transactionid, keyNode.WorkflowId })
                    .Where(p => p.IsCurrent).FirstOrDefault();
                // If existing current doesn't match to actionNode.FromId, invalid operation, then exit
                // if (currhist == null || currhist.NodeId != actionNode.NodeFromId) return newHist;

                WorkflowNode toNode = nodeManager.Get(keyNode.NodeToId);
                if (toNode.WorkflowId == currhist.WorkflowId)
                {
                    // if same workflow, put into history (IsCurrent = false), otherwise stays current
                    currhist.ApprovalUser = currhist.LastUpdateBy = user;
                    currhist.ApprovalDate = currhist.LastUpdateDate = DateTime.Now;
                    currhist.IsCurrent = false;
                    histManager.Update(currhist);
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

                newHist.Id = histManager.Create(newHist);
                
                // Update {{TransactionHeader}} table
                UpdateMyTransaction(session, transactionid, newHist.WorkflowId, newHist.NodeId, user, ref msgs);

                if (!TransactionExists) session.Commit();

                #endregion Step 2
                /* -------------------------------------------- 
                 *  Step 3: Workflow Status has been updated, now do the Actions (email, etc) related to the status (Defined in [WorkflowAction])
                 ---------------------------------------------- */
                DoCustomActions(session, newHist.NodeId, transactionid, user, ref msgs);

                /* -------------------------------------------- 
                 *  Step 4: Execute Auto-Steps, if there is any
                 ---------------------------------------------- */
                var ActionNodes = nodeManager.GetAll().Where(i => (i.NodeFromId == keyNode.NodeToId && i.IsAuto == true));
                foreach (WorkflowNode node in ActionNodes)
                {
                    bRunning = bRunning && _doActionNode(session, transactionid, node, user, "(Auto)", ref msgs);
                }

            }
            catch(Exception e) 
            {
                msgs.Add(e.Message); bRunning = false;
            }

            return bRunning;
        }


        /// <summary>
        /// Given an Action Node, find the Key Node (From Status to Status)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="actionNode"></param>
        /// <returns></returns>
        private WorkflowNode _getActionKeyNode(IDbSession session, WorkflowNode actionNode)
        {
            /* -------------------------------------------
             * Recurrsive function to get the KEY ActionNode (defined as "Status->Status")
             * ------------------------------------------- */
            if (!actionNode.NodeType.Equals("Action")) return null;

            WorkflowNode toNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(actionNode.NodeToId);

            if (toNode.NodeType.Equals("Action")) 
                // toNode is to an Action Node, Keep looking
                return _getActionKeyNode(session, toNode);
            else 
                // toNode is to a status node, then the node passed in is the Key Action Node
                return actionNode;
        }
        /// <summary>
        /// Given an Action KeyNode, find all linked Auto Actions
        /// </summary>
        /// <param name="session"></param>
        /// <param name="keyNode"></param>
        /// <returns></returns>
        private IEnumerable<WorkflowNode> _getActionNodeGroup(IDbSession session, WorkflowNode keyNode)
        {
            return ManagerFactory<WorkflowNode>.Instance.GetManager(session)
                .GetAll()
                .Where(i => (i.NodeToId == keyNode.Id && i.IsAuto == true) || i.Id == keyNode.Id);
        }
        
        /// <summary>
        /// Custom Node action like Email etc.. defined by each specify WorkFlow [WorkflowAction], should be overrided in there own specific implementation
        /// </summary>
        /// <param name="session"></param>
        /// <param name="StatusNodeId"></param>
        /// <param name="TransactionId"></param>
        /// <param name="User"></param>
        /// <param name="msg"></param>
        protected virtual void DoCustomActions(IDbSession session, int StatusNodeId, int TransactionId, string User, ref List<string> msg)
        {
        }
        /// <summary>
        /// To update status in Header transaction Table
        /// </summary>
        /// <param name="session"></param>
        /// <param name="TransactionId"></param>
        /// <param name="WorkflowId"></param>
        /// <param name="StatusId"></param>
        /// <param name="User"></param>
        /// <param name="msg"></param>
        protected virtual void UpdateMyTransaction(IDbSession session, int TransactionId, int WorkflowId, int StatusId, string User, ref List<string> msg)
        {
        }

        #endregion Private Methods
    }
}
