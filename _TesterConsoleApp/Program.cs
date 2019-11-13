using Application.JobServices;
using Framework.Core.DataAccess;
using Application.ValueObjects.Workflow;

using System;

using Application.DataAccessObject.Workflow;
using Application.DataAccessObject;

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
            //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitHub\\Source\\Repos\\pchenatwork\\MyFrameworkCore\\Application.DB\\Workflow.mdf;Integrated Security=True";
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\_GitHub\\Source\\Repos\\pchenatwork\\MyFrameworkCore\\Application.DB\\Workflow.mdf;Integrated Security=True";
        
            using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
            {
                //WorkflowListDAO dao = new WorkflowListDAO();
                //var dao = DataAccessObjectFactory<WorkflowList>.Instance.GetDAO("Application.DataAccessObject.Workflow.WorkflowListDAO");
                var dao = DaoFactory<WorkflowList>.Instance.GetDAO("Application.DataAccessObject.Workflow.WorkflowListDAO");
                var x = dao.Get(session, 1);
            }



            Console.ReadKey();

        }
    }
}
