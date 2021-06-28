﻿using AppBase.Core.DataAccess;
using AppBase.Core.Interfaces;
using Application.ValueObjects.Workflow;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace Application.DAO.Workflow
{
    public class WorkflowNodeDAO : AbstractDAO<WorkflowNode>
    {
        #region	Constants
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
            String methodName = "ClassName"  + ".Get() - " + id.ToString();

            string sql = SELECT_ALL + @" WHERE Id=@id";

            return dbSession.DbConnection.QueryFirstOrDefault<WorkflowNode>(sql, new { Id = id }, dbSession.Transaction);
        }

        public override IEnumerable<WorkflowNode> GetAll(IDbSession dbSession)
        {
            return dbSession.DbConnection.Query<WorkflowNode>(SELECT_ALL, null, dbSession.Transaction); 
        }
        
        public override IEnumerable<WorkflowNode> FindByCriteria(IDbSession dbSession, string finderType, params object[] criteria)
        {
            String methodName = "ClassName" + ".FindByCriteria() - " + finderType ;
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
    }
}
