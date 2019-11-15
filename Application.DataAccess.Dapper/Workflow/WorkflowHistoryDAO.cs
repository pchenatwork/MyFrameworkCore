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
                string sql = "SELECT * FROM WorkflowHistory WHERE Id=@id";

                var orderDetail = connection.QueryFirstOrDefault<WorkflowHistory>(sql, new { Id = id});

                return orderDetail;
            }
        }
    }
}
