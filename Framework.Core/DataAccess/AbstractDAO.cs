using Framework.Core.ValueObjects;
using Framework.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace Framework.Core.DataAccess
{
    public abstract class AbstractDAO<T> : IRepository<T> where T : IValueObject // class, IValueObject, new()
    {
        #region constructor
        protected AbstractDAO()
        {
        }
        static AbstractDAO()
        {

        }
        #endregion constructor

        protected string ClassName { get; } = typeof(T).Name + "DAO";

        public virtual int Create(IDbSession dbSession, T newObject)
        {
            throw new NotImplementedException();
        }
        public virtual bool Delete(IDbSession dbSession, dynamic Id)
        {
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> FindByCriteria(IDbSession dbSession, string finderType, params object[] criteria)
        {
            throw new NotImplementedException();
        }
        public virtual T Get(IDbSession dbSession, dynamic id)
        {
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> GetAll(IDbSession dbSession)
        {
            throw new NotImplementedException();
        }
        public virtual bool Update(IDbSession dbSession, T obj)
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
        /// <summary>
        /// </summary>
        /// <param name="dbSession"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters">parameters[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);</param>
        /// <returns></returns>
        protected XmlReader ExecuteXmlReader(IDbSession dbSession, string commandText, SqlParameter[] parameters)
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
        /// <returns></returns>
        protected int ExecuteNonQuery(IDbSession dbSession, string commandText, SqlParameter[] parameters)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = (SqlConnection)dbSession.DbConnection;
                if (dbSession.Transaction != null)
                    cmd.Transaction = (SqlTransaction)dbSession.Transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = commandText;

                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                object returnValue = cmd.ExecuteNonQuery();

                return (int)cmd.Parameters[0].Value;
            }

        }

        //protected int ExecuteNonQuery(IDbSession dbSession, string commandText, List<SqlParameter> parameters)
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = (SqlConnection)dbSession.DbConnection;
        //        if (dbSession.Transaction != null)
        //            cmd.Transaction = (SqlTransaction)dbSession.Transaction;

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = commandText;

        //        foreach (SqlParameter p in parameters)
        //        {
        //            cmd.Parameters.Add(p);
        //        }

        //        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
        //        object returnValue = cmd.ExecuteNonQuery();

        //        return (int)cmd.Parameters[0].Value;
        //    }

        //}

        protected T Deserialize(XmlReader reader)
        {
            return XMLUtility.Deserialize<T>(reader);
            //return (T)result;
        }
        ////protected ICollection<T> DeserializeCollection_ORG(XmlReader reader)
        ////{
        ////    return XMLUtility.Deserialize<ValueObjectCollection<T>>(reader);
        ////}
        protected IEnumerable<T> DeserializeCollection(XmlReader reader)
        {
          //  return XMLUtility.Deserialize<Collection<T>>(reader);
            return XMLUtility.Deserialize<T[]>(reader);
        }
    }
}
