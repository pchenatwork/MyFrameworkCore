using Application.ValueObjects.Workflow;
using Dapper;
using Framework.Core.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Application.DAO.Workflow
{
    public class TimeoffRequestDAO : AbstractDAO<TimeoffRequest>
    {
        #region	Constants
        // *************************************************************************
        //				 constants
        // *************************************************************************
        public const string FIND_BY_USER = "TimeoffRequestsFindByActionName";
        public const string FIND_BY_TRANSACTION = "FIND_BY_TRANSACTION";
        private static string SELECT_SQL = @"" +
            "SELECT Id,TransactionId,User," +
            "TimeoffTypeEnum,dbo.GetEnumStrVal('TimeoffType', TimeoffTypeEnum) as TimeoffType," +
            "FromDate, ToDate,Note, StatusId, HRStatusId, " +
            "CASE IsActive WHEN 'Y' THEN 1 ELSE 0 END AS IsActive, " +
            "CreateBy, CreateDate, LastUpdateBy, LastUpdateDate " +
            "FROM TimeoffRequest ";
        #endregion Constants

        #region Constructor
        static TimeoffRequestDAO() { }
        private TimeoffRequestDAO() { }
        #endregion Constructor

        public override TimeoffRequest Get(IDbSession dbSession, dynamic id)
        {
            String methodName = ClassName + ".Get() - " + id.ToString();

            string sql = SELECT_SQL + @" WHERE Id=@id";

            return dbSession.DbConnection.QueryFirstOrDefault<TimeoffRequest>(sql, new { Id = id }, dbSession.Transaction);
        }

        public override IEnumerable<TimeoffRequest> GetAll(IDbSession dbSession)
        {
            return dbSession.DbConnection.Query<TimeoffRequest>(SELECT_SQL, null, dbSession.Transaction);
        }

        public override IEnumerable<TimeoffRequest> FindByCriteria(IDbSession dbSession, string finderType, params object[] criteria)
        {
            String methodName = ClassName + ".FindByCriteria() - " + finderType;
            //if (_logger.IsDebugEnabled)
            //{
            //    LoggingUtility.logMethodEntry(_logger, methodName);
            //}
            try
            {
                switch ((string)finderType)
                {
                    case FIND_BY_USER:
                        {
                            string username = (string)criteria[0];
                            string sql = SELECT_SQL + @"WHERE User=@id";

                            return dbSession.DbConnection.Query<TimeoffRequest>(sql, new { user = username }, dbSession.Transaction);
                        }
                    case FIND_BY_TRANSACTION:
                        {
                            int tranId = (int)criteria[0];
                            string sql = SELECT_SQL + @"WHERE TransactionId=@id";

                            return dbSession.DbConnection.Query<TimeoffRequest>(sql, new { id = tranId }, dbSession.Transaction);
                        }
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

        public override int Create(IDbSession dbSession, TimeoffRequest newObject)
        {
            // tested working 1129
            string methodName = ClassName + ".Create()" ;
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                param[0].Direction = ParameterDirection.ReturnValue;

                param[1] = new SqlParameter("@TransactionId", SqlDbType.Int);
                param[1].Value = newObject.TransactionId;

                param[2] = new SqlParameter("@TimeoffTypeEnum", SqlDbType.Int);
                param[2].Value = newObject.TimeoffTypeEnum ;

                param[3] = new SqlParameter("@User", SqlDbType.NVarChar, 50);
                //parameters[3].IsNullable = true;
                param[3].Value = newObject.User ?? string.Empty;

                param[4] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                param[4].IsNullable = true;
                param[4].Value = newObject.FromDate;

                param[5] = new SqlParameter("@ToDate", SqlDbType.DateTime);
                param[5].IsNullable = true;
                param[5].Value = newObject.ToDate;

                param[6] = new SqlParameter("@Note", SqlDbType.NVarChar, -1);  // VARCHAR(MAX)
                param[6].Value = newObject.Note;

                param[7] = new SqlParameter("@StatusId", SqlDbType.Int);
                param[7].Value = newObject.StatusId;

                param[8] = new SqlParameter("@HRStatusId", SqlDbType.Int);
                param[8].Value = newObject.HRStatusId;

                //parameters[8] = new SqlParameter("@IsActive", SqlDbType.Char, 1);
                //parameters[8].Value = newObject.IsActive ? "Y" : "N";
                param[9] = new SqlParameter("@ByUser", SqlDbType.NVarChar, 50);
                param[9].Value = newObject.LastUpdateBy ?? string.Empty;

                return ExecuteNonQuery(dbSession, "TimeoffRequestInsert", param);
            }
            catch (System.Exception e)
            {
                // _logger.Error(methodName, e);
                return -1;
            }
        }
        public override bool Update(IDbSession dbSession, TimeoffRequest o)
        {
            // tested working 1129
            string methodName = ClassName + ".Update()";
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];

                parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.ReturnValue;

                parameters[1] = new SqlParameter("@Id", SqlDbType.Int);
                parameters[1].Value = o.Id;
                parameters[2] = new SqlParameter("@TransactionId", SqlDbType.Int);
                parameters[2].Value = o.TransactionId;
                parameters[3] = new SqlParameter("@TimeoffTypeEnum", SqlDbType.Int);
                parameters[3].Value = o.TimeoffTypeEnum;

                parameters[4] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                parameters[4].IsNullable = true;
                parameters[4].Value = o.FromDate;

                parameters[5] = new SqlParameter("@ToDate", SqlDbType.DateTime);
                parameters[5].IsNullable = true;
                parameters[5].Value = o.ToDate;

                parameters[6] = new SqlParameter("@Note", SqlDbType.NVarChar, -1);  // VARCHAR(MAX)
                parameters[6].Value = o.Note;

                parameters[7] = new SqlParameter("@StatusId", SqlDbType.Int);
                parameters[7].Value = o.StatusId;

                parameters[8] = new SqlParameter("@HRStatusId", SqlDbType.Int);
                parameters[8].Value = o.HRStatusId;

                //parameters[8] = new SqlParameter("@IsActive", SqlDbType.Char, 1);
                //parameters[8].Value = o.IsActive ? "Y" : "N";
                parameters[9] = new SqlParameter("@ByUser", SqlDbType.NVarChar, 50);
                parameters[9].Value = o.LastUpdateBy ?? string.Empty;

                return ExecuteNonQuery(dbSession, "TimeoffRequestUpdate", parameters)>0;
            }
            catch (System.Exception e)
            {
                // _logger.Error(methodName, e);
                return false;
            }
        }
    }
}
