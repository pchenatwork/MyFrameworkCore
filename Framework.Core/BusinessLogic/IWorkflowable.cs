using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.BusinessLogic
{
    public interface IWorkflowable
    {
        bool TakeAction(IDbSession session, int WorkflowId, int TransactionId, string User, string Note, ref List<string> msg);
    }
}
