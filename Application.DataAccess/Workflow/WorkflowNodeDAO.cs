using Application.ValueObjects.Workflow;
using Dapper;
using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataAccess.Workflow
{
    public class WorkflowNodeDAO : AbstractDAO<WorkflowNode>
    {
        #region Constructor
        static WorkflowNodeDAO() { }
        private WorkflowNodeDAO() { }
        #endregion Constructor

        public override WorkflowNode Get(IDbSession dbSession, dynamic id)
        {
            using (var connection = dbSession.DbConnection)
            {
                string sql = @"select Id, WorkflowId, Name, Description, " +
                     "NodeTypeEnum, dbo.GetEnumStrVal('NodeType', NodeTypeEnum) as NodeType, NodeFromId, NodeToId," +
                     "StepId, Action " +
                     "from WorkflowNode WHERE Id=@id";

                var entity = connection.QueryFirstOrDefault<WorkflowNode>(sql, new { Id = id });

                return entity;
            }
        }




    }
}
