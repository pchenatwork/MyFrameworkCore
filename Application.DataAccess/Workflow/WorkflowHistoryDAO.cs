
using Application.ValueObjects.Workflow;
using Dapper;
using Framework.Core.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Application.DataAccess.Workflow
{
    public class WorkflowHistoryDAO : AbstractDAO<WorkflowHistory>
    {
        private WorkflowHistoryDAO() { }

        public override WorkflowHistory Get(IDbSession dbSession, dynamic id)
        {
            string sql = @"select Id, TransactionId, WorkflowId, CurrentNodeId, " +
                "ApprovalUserId, ApprovalDate, PrevHistoryId, " +
                "case when IsCurrent = 'Y' Then 1 else 0 end as IsActive, " +
                "Comment, CreateBy, CreateDate, LastUpdateBy, LastUpdateDate " +
                "from WorkflowHistory WHERE Id=@id";

            return dbSession.DbConnection.QueryFirstOrDefault<WorkflowHistory>(sql, new { Id = id });
        }
        public override int Create(IDbSession dbSession, WorkflowHistory newObject)
        {
            return base.Create(dbSession, newObject);
        }


        #region private method
        private int _UpSert(IDbSession dbSession, WorkflowHistory newObject)
        {
            string methodName = ClassName + "." + (newObject.Id == 0 ? "Create" : "Update");
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.ReturnValue;
                parameters[1] = new SqlParameter("@id", SqlDbType.Int);
                parameters[1].Value = newObject.Id;
                parameters[2] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
                parameters[2].Value = newObject.;
                parameters[3] = new SqlParameter("@description", SqlDbType.NVarChar, 200);
                parameters[3].Value = newObject.Description;
                parameters[4] = new SqlParameter("@IsCurrent", SqlDbType.Char, 1);
                parameters[4].Value = newObject.IsActive ? "Y" : "N";

                return ExecuteNonQuery(dbSession, "WorkflowHistoryUpSert", parameters);
            }
            catch (System.Exception e)
            {
                // _logger.Error(methodName, e);
                return -1;
            }
        }

        #endregion
    }
}
