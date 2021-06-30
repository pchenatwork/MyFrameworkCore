using AppBase.Core.DataAccess;
using AppBase.Core.Interfaces;
using Application.ValueObjects.Workflow;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Application.DAO.Workflow
{
    public class WorkflowNodeDAO : AbstractDAO<WorkflowNode>
    {
        #region	Constants
        private const string CLASS_NAME = nameof(WorkflowNodeDAO);
        // *************************************************************************
        //				 constants
        // *************************************************************************
        public const string FIND_BY_ACTIONNAME = "WorkflowNodesFindByActionName";
        public const string FIND_BY_WORKFLOWID = "WorkflowNodesFindByWorkflowId";
        private static string SELECT_ALL = @"" +
            "SELECT Id, WorkflowId, Name, Description, " +
            "NodeTypeEnum, dbo.GetEnumStrVal('NodeType', NodeTypeEnum) as NodeType, " +
            "NodeFromId, NodeToId, StepId, ActionName, " +
            "CASE IsPermissioned WHEN 'Y' THEN 1 ELSE 0 END AS IsPermissioned, " +
            "CASE IsAuto WHEN 'Y' THEN 1 ELSE 0 END AS IsAuto " +
            "FROM WorkflowNode";
        #endregion Constants

        #region Constructor
        static WorkflowNodeDAO() { }
        private WorkflowNodeDAO() { }
        #endregion Constructor

        public override WorkflowNode Get(IDbSession dbSession, dynamic id)
        {
            String methodName = CLASS_NAME + ".Get() - " + id.ToString();

            string sql = SELECT_ALL + @" WHERE Id=@id";

            return dbSession.DbConnection.QueryFirstOrDefault<WorkflowNode>(sql, new { Id = id }, dbSession.Transaction);
        }
        public override async Task<WorkflowNode> GetAsync(IDbSession dbSession, dynamic id)
        {
            return await Get(dbSession, id);
        }
        public override int Create(IDbSession dbSession, WorkflowNode newObject)
        {
            return _UpSert(dbSession, newObject);
        }
        public override bool Update(IDbSession dbSession, WorkflowNode obj)
        {
            return obj.Id == _UpSert(dbSession, obj);
        }

        public override IEnumerable<WorkflowNode> GetAll(IDbSession dbSession)
        {
            return dbSession.DbConnection.Query<WorkflowNode>(SELECT_ALL, null, dbSession.Transaction); 
        }
        
        public override IEnumerable<WorkflowNode> FindByCriteria(IDbSession dbSession, string finderType, params object[] criteria)
        {
            String methodName = CLASS_NAME + ".FindByCriteria() - " + finderType ;
            //if (_logger.IsDebugEnabled)
            //{
            //    LoggingUtility.logMethodEntry(_logger, methodName);
            //}
            try
            {
                switch ((string)finderType)
                {
                    case FIND_BY_ACTIONNAME:
                        {
                            string workflowIds = (string)criteria[0];
                            string actionName = (string)criteria[1];

                            SqlParameter[] parameters = new SqlParameter[3];
                            parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                            parameters[0].Direction = ParameterDirection.ReturnValue;
                            parameters[1] = new SqlParameter("@workflowIds", SqlDbType.VarChar, 100);
                            parameters[1].Value = workflowIds;
                            parameters[2] = new SqlParameter("@ActionName", SqlDbType.NVarChar, 50);
                            parameters[2].Value = actionName;                            
                            XmlReader reader = ExecuteXmlReader(dbSession, "WorkflowNodesFindByActionNameXml", parameters);
                            return DeserializeCollection(reader);
                        }
                    case FIND_BY_WORKFLOWID:
                        {
                            string workflowIds = (string)criteria[0];
                            // string sql = SELECT_ALL + @" WHERE WorkflowId in (@id)";
                            string sql = SELECT_ALL + @" WHERE EXISTS (SELECT 1 FROM STRING_SPLIT(@ids, ',') WHERE [value]=WorkflowNode.WorkflowId)";

                            return dbSession.DbConnection.Query<WorkflowNode>(sql, new { ids = workflowIds }, dbSession.Transaction);
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


        #region private methods
        private int _UpSert(IDbSession dbSession, WorkflowNode newObject)
        {
            string methodName = nameof(WorkflowNodeDAO) + (newObject.Id == 0 ? ".Create" : ".Update");
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });
                parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = newObject.Id });
                parameters.Add(new SqlParameter("@WorkflowId", SqlDbType.Int){Value = newObject.WorkflowId});
                parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100) { Value = newObject.Name });
                parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 250) { Value = newObject.Description });
                parameters.Add(new SqlParameter("@NodeTypeEnum", SqlDbType.Int) { Value = newObject.NodeTypeEnum });
                parameters.Add(new SqlParameter("@NodeFromId", SqlDbType.Int) { Value = newObject.NodeFromId });
                parameters.Add(new SqlParameter("@NodeToId", SqlDbType.Int) { Value = newObject.NodeToId });
                parameters.Add(new SqlParameter("@StepId", SqlDbType.Int) { Value = newObject.StepId });
                parameters.Add(new SqlParameter("@ActionName", SqlDbType.VarChar, 100) { Value = newObject.ActionName });
                parameters.Add(new SqlParameter("@IsPermissioned", SqlDbType.Char, 1) { Value = newObject.IsPermissioned? "Y": "N" });
                parameters.Add(new SqlParameter("@TimeToSkip", SqlDbType.Int) { Value = newObject.TimeToSkip });
                parameters.Add(new SqlParameter("@NodeValue", SqlDbType.VarChar, 50) { Value = newObject.NodeValue });
                parameters.Add(new SqlParameter("@NodeConditionEnum", SqlDbType.Int) { Value = newObject.NodeConditionEnum });
                parameters.Add(new SqlParameter("@IsAuto", SqlDbType.Char, 1) { Value = newObject.IsAuto ? "Y" : "N" });

                //SqlParameter[] parameters = new SqlParameter[7];
                //parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                //parameters[0].Direction = ParameterDirection.ReturnValue;
                //parameters[1] = new SqlParameter("@Id", SqlDbType.Int);
                //parameters[1].Value = newObject.Id;
                //parameters[2] = new SqlParameter("@WorkflowId", SqlDbType.Int);
                //parameters[2].Value = newObject.WorkflowId;
                //parameters[3] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
                //parameters[3].Value = newObject.Name;
                //parameters[4] = new SqlParameter("@description", SqlDbType.NVarChar, 200);
                //parameters[4].Value = newObject.Description;
                //parameters[5] = new SqlParameter("@NodeValue", SqlDbType.NVarChar, 100);
                //parameters[5].Value = newObject.NodeValue;
                //parameters[6] = new SqlParameter("@IsAuto", SqlDbType.Char, 1);
                //parameters[6].Value = newObject.IsAuto ? "Y" : "N";
                object retval;
                int rowcnt= ExecuteNonQuery(dbSession, "WorkflowNodeUpSert", parameters, out retval);
                return (int)retval;
            }
            catch (System.Exception e)
            {
                // _logger.Error(methodName, e);
                return -1;
            }
        }
        #endregion private methods    
    }
}
