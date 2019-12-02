using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public abstract class WorkflowBase
    {
        public int CreateWorkflow(int workflowId, string user, string comment = null)
        {
            return 1; // Transaction Id
        }
    }
}
