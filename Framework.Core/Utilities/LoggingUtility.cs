using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.Utilities
{
    public class LoggingUtility
    {
        static LoggingUtility()
        {
        }

        public static ILogger GetLogger(string sinkName)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
