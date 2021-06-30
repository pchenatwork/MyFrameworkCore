using AppBase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Data.SqlClient;
using System.Data;
using AppBase.Core.Utilities;
using System.Collections;
using System.Threading.Tasks;

namespace AppBase.Core.DataAccess
{
    public abstract class AbstractDAO<T> : IDataAccessObject<T>, IDataAccessObjectAsyn<T> where T : IValueObject // class, IValueObject, new()
    {
        #region constructor
        protected AbstractDAO()
        {
        }
        static AbstractDAO()
        {

        }
        #endregion constructor

        public virtual int Create(IDbSession dbSession, T newObject)
        {
            throw new NotImplementedException();
        }
        public async Task<int> CreateAsyn(IDbSession dbSession, T entity)
        {
            return await Task.Run(() => Create(dbSession, entity));
        }
        public virtual bool Update(IDbSession dbSession, T obj)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateAsyn(IDbSession dbSession, T entity)
        {
            return await Task.Run(() => Update(dbSession, entity));
        }
        public virtual bool Delete(IDbSession dbSession, dynamic Id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteAsyn(IDbSession dbSession, dynamic Id)
        {
            return await Task.Run(() => Delete(dbSession, Id));
        }


        public virtual IEnumerable<T> FindByCriteria(IDbSession dbSession, string finderType, params object[] criteria)
        {
            throw new NotImplementedException();
        }
        public virtual Task<IEnumerable<T>> FindByCriteriaAsync(IDbSession dbSession, string finderType, params object[] criteria)
        {
            throw new NotImplementedException();
        }
        public virtual T Get(IDbSession dbSession, dynamic id)
        {
            throw new NotImplementedException();
        }
        public async Task<T> GetAsync(IDbSession dbSession, dynamic id)
        {
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> GetAll(IDbSession dbSession)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<T>> GetAllAsync(IDbSession dbSession)
        {
            throw new NotImplementedException();
        }
        public virtual object InvokeByMethodName(IDbSession dbSession, string methodName, params object[] parameters)
        {
            Type type = this.GetType();
            return type.InvokeMember(methodName, BindingFlags.DeclaredOnly |
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                null, this, parameters);
        }
        public async Task<object> InvokeByMethodNameAsync(IDbSession dbSession, string methodName, params object[] parameters)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// </summary>
        /// <param name="dbSession"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters">parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);</param>
        /// <returns></returns>
        protected XmlReader ExecuteXmlReader(IDbSession dbSession, string commandText, IEnumerable<SqlParameter> parameters)
        {
            XmlReader reader;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = (SqlConnection)dbSession.DbConnection;
                if (dbSession.Transaction != null) cmd.Transaction = (SqlTransaction)dbSession.Transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = commandText;

                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                reader = cmd.ExecuteXmlReader();
            }
            return reader;
        }

        /// <summary>
        /// Make sure SqlParameter[0] is ParameterDirection.ReturnValue
        /// </summary>
        /// <param name="dbSession"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns>row count</returns>
        protected int ExecuteNonQuery(IDbSession dbSession, string commandText, IEnumerable<SqlParameter> parameters, out object retval)
        {
            using (SqlCommand cmd = new SqlCommand())
            {;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = commandText;

                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }

                if (dbSession.Transaction != null)
                {
                    cmd.Transaction = (SqlTransaction)dbSession.Transaction;
                    cmd.Connection = cmd.Transaction.Connection;
                }
                else 
                    cmd.Connection = (SqlConnection)dbSession.DbConnection;

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                int rowcount = cmd.ExecuteNonQuery();
                retval = cmd.Parameters["@ReturnValue"].Value;
                return rowcount;
            }
        }

        protected T Deserialize(XmlReader reader)
        {
            return XMLSerializer.Deserialize<T>(reader);
        }
        protected IEnumerable<T> DeserializeCollection(XmlReader reader)
        {
            return XMLSerializer.Deserialize<T[]>(reader);
        }
    }
}

