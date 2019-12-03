using Application.ValueObjects.Workflow;
using Dapper;
using Framework.Core.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace Application.DataAccess.Workflow
{
    public class WorkflowNodeDAO : AbstractDAO<WorkflowNode>
    {

        #region	Constants
        // *************************************************************************
        //				 constants
        // *************************************************************************
        public const string FIND_BY_ACTIONNAME = "WorkflowNodesFindByActionName";
        public const string FIND_BY_WORKFLOWID = "WorkflowNodesFindByWorkflowId";
        #endregion Constants

        #region Constructor
        static WorkflowNodeDAO() { }
        private WorkflowNodeDAO() { }
        #endregion Constructor

        public override WorkflowNode Get(IDbSession dbSession, dynamic id)
        {
            String methodName = ClassName + ".Get() - " + id.ToString();
            //using (var connection = dbSession.DbConnection)
            //{
            string sql = @"select Id, WorkflowId, Name, Description, " +
                     "NodeTypeEnum, dbo.GetEnumStrVal('NodeType', NodeTypeEnum) as NodeType, NodeFromId, NodeToId," +
                     "StepId, ActionName, " +
                     "CASE IsPermissioned WHEN 'Y' THEN 1 ELSE 0 END AS IsPermissioned, " +
                     "CASE IsAuto WHEN 'Y' THEN 1 ELSE 0 END AS IsAuto " +
                     "from WorkflowNode WHERE Id=@id";

                var entity = dbSession.DbConnection.QueryFirstOrDefault<WorkflowNode>(sql, new { Id = id }, dbSession.Transaction);

                return entity;
            //}
        }

        public override ICollection<WorkflowNode> FindByCriteria(IDbSession dbSession, string finderType, params object[] criteria)
        {
            //// 20191125 tested
            ///
            String methodName = ClassName + ".FindByCriteria() - " + finderType ;
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
                            int workflowId = (int)criteria[0];
                            string actionName = (string)criteria[1];

                            SqlParameter[] parameters = new SqlParameter[3];
                            parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                            parameters[0].Direction = ParameterDirection.ReturnValue;
                            parameters[1] = new SqlParameter("@workflowId", SqlDbType.Int);
                            parameters[1].Value = workflowId;
                            parameters[2] = new SqlParameter("@ActionName", SqlDbType.NVarChar, 50);
                            parameters[2].Value = actionName;                            
                            XmlReader reader = ExecuteXmlReader(dbSession, "WorkflowNodesFindByActionNameXml", parameters);
                            return DeserializeCollection(reader);
                        }
                    case FIND_BY_WORKFLOWID:
                        {
                            int workflowId = (int)criteria[0];
                            string sql = @"select Id, WorkflowId, Name, Description, " +
                                     "NodeTypeEnum, dbo.GetEnumStrVal('NodeType', NodeTypeEnum) as NodeType, NodeFromId, NodeToId," +
                                     "StepId, ActionName, " +
                                     "CASE IsPermissioned WHEN 'Y' THEN 1 ELSE 0 END AS IsPermissioned, " +
                                     "CASE IsAuto WHEN 'Y' THEN 1 ELSE 0 END AS IsAuto " +
                                     "from WorkflowNode WHERE WorkflowId=@id " ;

                            return (ICollection < WorkflowNode >) dbSession.DbConnection.Query<WorkflowNode>(sql, new { Id = workflowId }, dbSession.Transaction);
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
    }
}
