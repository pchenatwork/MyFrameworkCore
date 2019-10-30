using System;
using System.Collections.Generic;
using System.Data.Common;
using Framework.Core.ValueObjects;
using System.Data;

namespace Framework.Core.DataAccess
{
    public interface IDbSession : IDisposable
    {
        IDbConnection DbConnection { get; }
        IDbTransaction Transaction { get; }
        IDbTransaction BeginTrans(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }
    public class DbSession : IDbSession
    {
        #region Private members        
        private IDbConnection _dbConnection;
        private IDbTransaction _transaction;
        #endregion private members

        #region Constructor
        internal DbSession(string providerName, string connectionString)
        {
            // Create the DbProviderFactory and DbConnection.
            /*
<configuration>  
  <connectionStrings>  
    <clear/>  
    <add name="NorthwindSQL"   
     providerName="System.Data.SqlClient"   
     connectionString=  
     "Data Source=MSSQL1;Initial Catalog=Northwind;Integrated Security=true"  
    />  
  
    <add name="NorthwindAccess"   
     providerName="System.Data.OleDb"   
     connectionString=  
     "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Data\Northwind.mdb;"  
    />  
  </connectionStrings>  
</configuration> 
             * */
            if (connectionString != null)
            {
                try
                {
                    DbProviderFactory factory =
                        DbProviderFactories.GetFactory(providerName);

                    _dbConnection = factory.CreateConnection();
                    _dbConnection.ConnectionString = connectionString;
                }
                catch (Exception ex)
                {
                    // Set the connection to null if it was created.
                    if (_dbConnection != null)
                    {
                        _dbConnection = null;
                    }
                }
            }
        }
        internal DbSession(IDbConnection conn)
        {
            _dbConnection = conn;
        }
        internal DbSession(IDbTransaction trans)
        {
            _dbConnection = trans.Connection;
            _transaction = trans;
        }
        #endregion Constructor
        
        #region Properties
        public IDbConnection DbConnection => _dbConnection;
        public IDbTransaction Transaction => _transaction;
        #endregion Properties
        
        public IDbTransaction BeginTrans(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            return _dbConnection.BeginTransaction(isolation);
        }
        public void Commit()
        {
            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_dbConnection.State != ConnectionState.Closed)
            {
                if (_transaction != null)
                {
                    //_transaction.Rollback();
                    _transaction.Dispose();
                    _transaction = null;

                }
                _dbConnection.Close();
                _dbConnection = null;
            }
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = null;
        }
    }
}
