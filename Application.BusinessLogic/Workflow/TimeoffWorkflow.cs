using System;
using System.Collections.Generic;
using System.Text;
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
                WorkflowListEnum.TimeoffWorkflow.Id, WorkflowListEnum.TimeoffHRWorkflow.Id
            };
        }

        protected override void _customActions(IDbSession session, int StatusNodeId, int TransactionId, string User, ref List<string> msg)
        {
            var i = 2;
        }

        protected override void _updateHeaderTransaction(IDbSession session, WorkflowHistory hist)
        {
            throw new NotImplementedException();
        }
    }
}
