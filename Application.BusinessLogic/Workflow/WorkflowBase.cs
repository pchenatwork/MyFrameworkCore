using Framework.Core.BusinessLogic;
using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public abstract class WorkflowBase : IWorkflowable
    {
        public bool ExecuteAction(IDbSession session, int WorkflowId, int TransactionId, string ActionName, string User, string Note, ref List<string> msg)
        {
            throw new NotImplementedException();
        }

        public int NewWorkflow(IDbSession session, int workflowId, string user)
        {
            throw new NotImplementedException();
        }
    }
}
