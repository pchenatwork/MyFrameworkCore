using Application.ValueObjects.Workflow;
using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace Application.DataAccess.Dapper.Workflow
{
    public class WorkflowHistoryDAO : AbstractDAO<WorkflowHistory>
    {
        /// <summary>
        /// private constructor to force using Factory
        /// </summary>
        private WorkflowHistoryDAO() { }

        public override WorkflowHistory Get(IDbSession dbSession, dynamic id)
        {
            using (var connection = dbSession.DbConnection)
            {
                    string sql = @"select Id, TransactionId, WorkflowId, CurrentNodeId, " +
                                "ApprovalUserId, ApprovalDate, PrevHistoryId, " +
"case when IsActive = 'Y' Then 1 else 0 end as IsActive, " +
"Comment, CreateBy, CreateDate, LastUpdateBy, LastUpdateDate " +
"from WorkflowHistory WHERE Id=@id";

                var orderDetail = connection.QueryFirstOrDefault<WorkflowHistory>(sql, new { Id = id});

                return orderDetail;
            }
        }
    }
}
