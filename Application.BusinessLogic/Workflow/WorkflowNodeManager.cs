using AppBase.Core.BusinessLogic;
using AppBase.Core.Interfaces;
using Application.DAO;
using Application.ValueObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.Workflow
{
    public class WorkflowNodeManager : ManagerBase<WorkflowNode>
    {
        //internal const string DAO_CLASS_NAME = "Application.DAO.Workflow.WorkflowListDAO";
        //private static readonly Lazy<T> instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);

        public const string FIND_BY_ACTIONNAME = "WorkflowNodesFindByActionName";
        public const string FIND_BY_WORKFLOWID = "WorkflowNodesFindByWorkflowId";

        // private constructor to force using Factory
        private WorkflowNodeManager(IDbSession dbSession)
            : base(dbSession)
        {
        }

        protected override IDataAccessObject<WorkflowNode> GetDefaultDAO()
        {
            // Can't move to "ManagerBase" cause reflection won't work
            return DataAccessObjectFactory<WorkflowNode>.Instance.GetDAO();
        }

    }
}

