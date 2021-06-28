using System;
using System.Collections.Generic;
using System.Text;
using AppBase.Core.BusinessLogic;
using AppBase.Core.Interfaces;
using Application.DAO;
using Application.ValueObjects.Workflow;
//using Framework.Core.BusinessLogic;
//using Framework.Core.DataAccess;

namespace Application.BusinessLogic.Workflow
{
    public class WorkflowListManager : ManagerBase<WorkflowList>
    {
        //internal const string DAO_CLASS_NAME = "Application.DAO.Workflow.WorkflowListDAO";
        //private static readonly Lazy<T> instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);

        public const string METHOD_1 = "Method 1";
        public const string METHOD_2 = "Method 2";

        // private constructor to force using Factory
        private WorkflowListManager(IDbSession dbSession)
            : base(dbSession)
        {
        }

        protected override IDataAccessObject<WorkflowList> GetDefaultDAO()
        {
            // Can't move to "ManagerBase" cause reflection won't work
           return DataAccessObjectFactory<WorkflowList>.Instance.GetDAO();
        }

    }
}
