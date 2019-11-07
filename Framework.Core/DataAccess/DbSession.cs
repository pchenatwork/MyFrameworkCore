using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Framework.Core.ValueObjects;
using System.Data;
using System.Linq;

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
                    /***** Works
                    // In core, need to Microsoft.Data.SqlClient is not part of machine.config/GAC. In .NET Core, there is no GAC or Global Configuration anymore.
                    // This means you will have ot register your provider in your project first
                    // 1. Register the factor
                    DbProviderFactories.RegisterFactory("test", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
                    
                    // 2. Get the provider invariant names
                    IEnumerable<string> invariants = DbProviderFactories.GetProviderInvariantNames(); // => 1 result; 'test'

                    // 3. Get a factory using that name
                    DbProviderFactory factory = DbProviderFactories.GetFactory(invariants.FirstOrDefault());


                    DbProviderFactory factory2 = Microsoft.Data.SqlClient.SqlClientFactory.Instance;

                    /// Not Working ///
                    //DbProviderFactory factory2 =
                    //    DbProviderFactories.GetFactory(providerName);

                    _dbConn = factory.CreateConnection();
                    _dbConn.ConnectionString = connectionString;

                     **********/
                    switch (providerName.Split('.').Last().ToLower())
                    {
                        case "sqlclient":
                          _dbConn = Microsoft.Data.SqlClient.SqlClientFactory.Instance.CreateConnection(); break;
                        default:
                          _dbConn = null; break;
                    }
                    if (_dbConn!= null) _dbConn.ConnectionString = connectionString;
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
