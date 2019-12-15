using Application.ValueObjects.Workflow;
using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public interface IWorkflowable
    {
        /// <summary>
        /// Array of WorkflowId that this workflow will work on
        /// </summary>
        public int[] WorkflowIds { get; }
        /// <summary>
        /// Return TransactionID
        /// </summary>
        /// <param name="session"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        WorkflowHistory Create(IDbSession session, string user, string note = null);

        bool ExecuteAction(IDbSession session, int TransactionId, string ActionName, string User, string Note, ref List<string> msg);
    }
}

