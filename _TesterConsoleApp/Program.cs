using Application.JobServices;
using Framework.Core.DataAccess;
using System;

namespace _TesterConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
          //  JobRunner.RunJob("Job1", "Hello World");
          //  JobRunner.RunJob("Job2", "Ni Hao");
          //  Console.ReadKey();

            //string providerName = "System.Data.SqlClient";
            string providerName = "Microsoft.Data.SqlClient";
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitHub\\Source\\Repos\\pchenatwork\\MyFrameworkCore\\Application.DB\\Workflow.mdf;Integrated Security=True";
            IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString);


            Console.ReadKey();

        }
    }
}
