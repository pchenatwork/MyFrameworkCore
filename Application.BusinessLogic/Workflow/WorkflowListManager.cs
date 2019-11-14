using System;
using System.Collections.Generic;
using System.Text;
using Application.ValueObjects.Workflow;
using Framework.Core.BusinessLogic;

namespace Application.BusinessLogic.Workflow
{
    public class WorkflowListManager : Manager<WorkflowList>
    {
        internal const string DAO_CLASS_NAME = "Application.DataAccess.Workflow.WorkflowListDAO";

        public WorkflowListManager() 
        {
        }
    }
}
