using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Application.ValueObjects.Workflow;

namespace Application.DataAccessObject.Workflow
{
    public class WorkflowListDAO : AbstractDAO<WorkflowList>
    {
        #region	Constructors
        public WorkflowListDAO()
        {
        }
        #endregion constructors

        #region IRepository
        public override int Create(IDbSession dbSession, WorkflowList entity)
        {
            return base.Create(dbSession, entity);
        }

        #endregion IRepository
    }
}
