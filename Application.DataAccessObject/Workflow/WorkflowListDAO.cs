using Framework.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Application.ValueObjects.Workflow;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml;

namespace Application.DataAccessObject.Workflow
{
    public class WorkflowListDAO : AbstractDAO<WorkflowList>
    {
        #region	Constructors
        private WorkflowListDAO()
        {
        }
        #endregion constructors

        #region IRepository
        public override int Create(IDbSession dbSession, WorkflowList entity)
        {
            return base.Create(dbSession, entity);
        }

        public override WorkflowList Get(IDbSession dbSession, dynamic id)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            parameters[1] = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.ReturnValue;

            XmlReader reader = ExecuteXmlReader(dbSession, "GetWorkflowXML", parameters);
            return Deserialize(reader);
        }

        #endregion IRepository
    }
}
