using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.DAO.Workflow;
using Application.ValueObjects.Workflow;
using Framework.Core.DataAccess;

namespace Application.BusinessLogic.Workflow
{
    public sealed class TimeoffWorkflow : WorkflowBase
    {
        protected override void _setupWorkflowIds()
        {
            _workflowIds = new int[]
            {
                WorkflowEnum.TimeoffMainFlow.Id, WorkflowEnum.TimeoffHRFlow.Id
            };
        }

        protected override void _customActions(IDbSession session, int StatusNodeId, int TransactionId, string User, ref List<string> msg)
        {
            var i = 2;
        }

        protected override void _updateHeaderTransaction(IDbSession session, WorkflowHistory hist)
        {
            var tManager =  ManagerFactory<TimeoffRequest>.Instance.GetManager(session);
            TimeoffRequest requst = tManager.FindByCriteria(TimeoffRequestDAO.FIND_BY_TRANSACTION, new object[] { hist.TransactionId })
                .FirstOrDefault();
            bool tobeUpdated = false;
            if (WorkflowEnum.GetById(hist.WorkflowId) == WorkflowEnum.TimeoffMainFlow)
            {
                requst.StatusId = hist.NodeId;
                requst.LastUpdateBy = hist.LastUpdateBy;
                tobeUpdated = true;
            } 
            else if (WorkflowEnum.GetById(hist.WorkflowId) == WorkflowEnum.TimeoffHRFlow)
            {
                requst.HRStatusId = hist.NodeId;
                requst.LastUpdateBy = hist.LastUpdateBy;
                tobeUpdated = true;
            }

            if (tobeUpdated)
            {
                tManager.Update(requst);
            }
        }
    }
}
