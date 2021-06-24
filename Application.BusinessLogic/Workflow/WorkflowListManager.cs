using System;
using System.Collections.Generic;
using System.Text;
using Application.ValueObjects.Workflow;
using Framework.Core.BusinessLogic;
using Framework.Core.DataAccess;

namespace Application.BusinessLogic.Workflow
{
    public class WorkflowListManager : Manager<WorkflowList>
    {
        internal const string DAO_CLASS_NAME = "Application.DAO.Workflow.WorkflowListDAO";

        public const string METHOD_1 = "Method 1";
        public const string METHOD_2 = "Method 2";

        private WorkflowListManager(IDbSession dbSession, IRepository<WorkflowList> dao)
            : base(dbSession, dao)
        {
        }
    }
}
