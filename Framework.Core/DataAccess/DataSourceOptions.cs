using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.DataAccess
{
    /// <summary>
    /// Struct for holding Data Source Parameters
    /// </summary>
    public struct DataSourceOptions
    {
        public string Name;
        public string ProviderName;
        public string ConnectionString;
        public DataSourceOptions(string dsName, string providerName, string connString)
        {
            Name = dsName; ProviderName = providerName; ConnectionString = connString;
        }
    }
}