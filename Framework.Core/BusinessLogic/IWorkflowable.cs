using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.BusinessLogic
{
    public interface IWorkflowable
    {
        public int WorkflowId { get; }
        /// <summary>
        /// Return TransactionID
        /// </summary>
        /// <param name="session"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        int Create(IDbSession session, string user, string note=null);
       
        bool ExecuteAction(IDbSession session, int WorkflowId, int TransactionId, string ActionName, string User, string Note, ref List<string> msg);
    }
}
