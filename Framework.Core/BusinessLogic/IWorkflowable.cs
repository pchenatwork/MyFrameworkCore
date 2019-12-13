﻿using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.BusinessLogic
{
    public interface IWorkflowable
    {
        public IValueObject HeaderWorkflow { get; set; }
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
        int Create(IDbSession session, string user, string note=null);
       
        bool ExecuteAction(IDbSession session, int TransactionId, string ActionName, string User, string Note, ref List<string> msg);
    }
}
