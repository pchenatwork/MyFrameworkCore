
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
        public const string FIND_BY_TRAN = "WorkflowHistoryFindByTran";
        //public const string FIND_BY_WORKFLOW = "WorkflowHistoryFindByWorkflow";

        private static string SELECT_ALL = @"" +
            "SELECT Id, TransactionId, WorkflowId, NodeId, " +
            "ApprovalUser, ApprovalDate, PrevHistoryId, " +
            "CASE WHEN IsCurrent = 'Y' THEN 1 ELSE 0 END AS IsCurrent, " +
            "Comment, CreateBy, CreateDate, LastUpdateBy, LastUpdateDate " +
            "FROM WorkflowHistory";

        private WorkflowHistoryDAO() { }

        public override WorkflowHistory Get(IDbSession dbSession, dynamic id)
        {
            string sql = SELECT_ALL + @" WHERE Id=@id";

            return dbSession.DbConnection.QueryFirstOrDefault<WorkflowHistory>(sql, new { Id = id }, dbSession.Transaction );
        }
        public override int Create(IDbSession dbSession, WorkflowHistory newObject)
        {
            return _UpSert(dbSession, newObject);
        }
        public override bool Update(IDbSession dbSession, WorkflowHistory obj)
        {
            return _UpSert(dbSession, obj) >0 ? true : false;
        }

        public override ICollection<WorkflowHistory> FindByCriteria(IDbSession dbSession, string finderType, params object[] criteria)
        {
            try
            {
                switch ((string)finderType)
                {
                    case FIND_BY_TRAN:
                        {
                            int TranId = -1, WfId = -1;
                            try { 
                                TranId = (int)criteria[0];
                                WfId = (int)criteria[1];
                            }
                            catch (System.Exception e)
                            {
                                //return null;
                            }
                            string sql = SELECT_ALL + 
                                "WHERE TransactionId = @TranId " + 
                                "AND (WorkflowId=@WfId or @WfId=-1) " +
                                "ORDER BY CreateDate DESC";

                            return (ICollection<WorkflowHistory>)dbSession.DbConnection.Query<WorkflowHistory>(sql, new { TranId = TranId, WfId = WfId }, dbSession.Transaction);

                        }
                    //case FIND_BY_WORKFLOW:
                    //    {
                    //        int TranId = -1, WfId = -1;
                    //        try
                    //        {
                    //            TranId = (int)criteria[0];
                    //            WfId = (int)criteria[1];
                    //        }
                    //        catch (System.Exception e)
                    //        {
                    //            //return null;
                    //        }
                    //        string sql = SELECT_ALL +
                    //            "WHERE TransactionId = @TranId " +
                    //            //" (WfId >0 ? " AND WorkflowId = @Wfid " : "AND 1=1 ") +
                    //            "AND (WorkflowId=@WfId or @WfId=-1) " +
                    //            "ORDER BY CreateDate DESC";

                    //        return (ICollection<WorkflowHistory>)dbSession.DbConnection.Query<WorkflowHistory>(sql, new { TranId = TranId, WfId = WfId }, dbSession.Transaction);


                    //    }
                    default:
                        return null;
                }
            }
            catch (System.Exception e)
            {
                // _logger.Error(methodName, e);
                return null;
            }
        }

        #region private method
        private int _UpSert(IDbSession dbSession, WorkflowHistory newObject)
        {
            // tested working 1129
            string methodName = ClassName + (newObject.Id == 0 ? ".Create" : ".Update");
            try
            {
                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.ReturnValue;
                parameters[1] = new SqlParameter("@Id", SqlDbType.Int);
                parameters[1].Value = newObject.Id;
                parameters[2] = new SqlParameter("@TransactionId", SqlDbType.Int);
                parameters[2].Value = newObject.TransactionId;
                parameters[3] = new SqlParameter("@WorkflowId", SqlDbType.Int);
                parameters[3].Value = newObject.WorkflowId;
                parameters[4] = new SqlParameter("@NodeId", SqlDbType.Int);
                parameters[4].Value = newObject.NodeId;
                parameters[5] = new SqlParameter("@ApprovalUser", SqlDbType.NVarChar, 50);
                parameters[5].IsNullable = true;
                parameters[5].Value = newObject.ApprovalUser ?? string.Empty;
                parameters[6] = new SqlParameter("@ApprovalDate", SqlDbType.DateTime);
                parameters[6].IsNullable=true;
                parameters[6].Value = newObject.ApprovalDate;
                parameters[7] = new SqlParameter("@PrevHistoryId", SqlDbType.Int);
                parameters[7].Value = newObject.PrevHistoryId;
                parameters[8] = new SqlParameter("@IsCurrent", SqlDbType.Char, 1);
                parameters[8].Value = newObject.IsCurrent ? "Y" : "N";
                parameters[9] = new SqlParameter("@Comment", SqlDbType.NVarChar, 200);
                parameters[9].Value = newObject.Comment;
                parameters[10] = new SqlParameter("@User", SqlDbType.NVarChar, 200);
                parameters[10].Value = newObject.LastUpdateBy ?? string.Empty;

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
