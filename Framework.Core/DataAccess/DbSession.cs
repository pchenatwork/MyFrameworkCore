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
        private IDbConnection _dbConn;
        private IDbTransaction _dbTran;
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

                    _dbConn = factory.CreateConnection();
                    _dbConn.ConnectionString = connectionString;
                }
                catch (Exception ex)
                {
                    // Set the connection to null if it was created.
                    if (_dbConn != null)
                    {
                        _dbConn = null;
                    }
                }
            }
        }
        internal DbSession(IDbConnection conn)
        {
            _dbConn = conn;
        }
        internal DbSession(IDbTransaction trans)
        {
            _dbConn = trans.Connection;
            _dbTran = trans;
        }
        #endregion Constructor
        
        #region Properties
        public IDbConnection DbConnection => _dbConn;
        public IDbTransaction Transaction => _dbTran;
        #endregion Properties
        
        public IDbTransaction BeginTrans(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            return _dbTran = _dbConn.BeginTransaction(isolation);
        }
        public void Commit()
        {
            _dbTran.Commit();
            _dbTran = null;
        }

        public void Dispose()
        {
            if (_dbConn.State != ConnectionState.Closed)
            {
                if (_dbTran != null)
                {
                    //_transaction.Rollback();
                    _dbTran.Dispose();
                    _dbTran = null;

                }
                _dbConn.Close();
                _dbConn = null;
            }
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            _dbTran.Rollback();
            _dbTran = null;
        }
    }
}
