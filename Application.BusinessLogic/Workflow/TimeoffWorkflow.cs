using System;
using System.Collections.Generic;
using System.Text;
using Framework.Core.DataAccess;

namespace Application.BusinessLogic.Workflow
{
    public sealed class TimeoffWorkflow : WorkflowBase
    {
        protected override void DoCustomActions(IDbSession session, int StatusNodeId, int TransactionId, string User, ref List<string> msg)
        {
            var i = 2;
        }

        protected override void UpdateMyTransaction(IDbSession session, int TransactionId, int WorkflowId, int StatusId, string User, ref List<string> msg)
        {
            var i = 2;
        }

    }
}
