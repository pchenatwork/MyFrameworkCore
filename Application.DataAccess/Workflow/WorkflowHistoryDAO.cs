using Application.ValueObjects.Workflow;
using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataAccess.Workflow
{
    public class WorkflowHistoryDAO : AbstractDAO<WorkflowHistory>
    {
        private WorkflowHistoryDAO() { }

        //public override WorkflowHistory Get(IDbSession dbSession, dynamic id)
        //{
        //    return (WorkflowHistory)base.Get(dbSession, id);
        //}
    }
}
