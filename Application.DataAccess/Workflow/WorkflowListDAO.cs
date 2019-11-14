using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Application.ValueObjects.Workflow;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml;

namespace Application.DataAccess.Workflow
{
    public class WorkflowListDAO : AbstractDAO<WorkflowList>
    {
        #region	Constructors
        private WorkflowListDAO()
        {
        }
        #endregion constructors

        #region IRepository
        public override int Create(IDbSession dbSession, WorkflowList newObject)
        {
            return _UpSert(dbSession, newObject);
        }

        public override bool Update(IDbSession dbSession, WorkflowList obj)
        {
            return obj.Id == _UpSert(dbSession, obj);
        }

        public override WorkflowList Get(IDbSession dbSession, dynamic id)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameters[0].Direction = ParameterDirection.ReturnValue;
            parameters[1] = new SqlParameter("@id", SqlDbType.Int);
            parameters[1].Value = id;

            XmlReader reader = ExecuteXmlReader(dbSession, "WorkflowGetXML", parameters);
            return Deserialize(reader);
        }

        #endregion IRepository


        #region private method
        private int _UpSert(IDbSession dbSession, WorkflowList newObject)
        {
            string methodName = newObject.Id ==0?  "Create" : "Update";
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.ReturnValue;
                parameters[1] = new SqlParameter("@id", SqlDbType.Int);
                parameters[1].Value = newObject.Id;
                parameters[2] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
                parameters[2].Value = newObject.Name;
                parameters[3] = new SqlParameter("@description", SqlDbType.NVarChar, 200);
                parameters[3].Value = newObject.Description;
                parameters[4] = new SqlParameter("@IsActive", SqlDbType.Char, 1);
                parameters[4].Value = newObject.IsActive ? "Y" : "N";

                return ExecuteNonQuery(dbSession, "WorkflowUpSert", parameters);
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
