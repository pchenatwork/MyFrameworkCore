using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.BusinessLogic
{
    public interface IWorkflowable
    {
        int NewWorkflow(IDbSession session, int workflowId, string user);
       
        bool ExecuteAction(IDbSession session, int WorkflowId, int TransactionId, string ActionName, string User, string Note, ref List<string> msg);
    }
}
