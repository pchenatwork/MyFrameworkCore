using Application.JobServices;
using Framework.Core.DataAccess;
using Application.ValueObjects.Workflow;

using System;

using Application.DataAccess.Workflow;
using Application.DataAccess;
using Framework.Core.ValueObjects;

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
           // string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitHub\\Source\\Repos\\pchenatwork\\MyFrameworkCore\\Application.DB\\Workflow.mdf;Integrated Security=True";
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\_GitHub\\Source\\Repos\\pchenatwork\\MyFrameworkCore\\Application.DB\\Workflow.mdf;Integrated Security=True";
        
            using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
            {
                //WorkflowListDAO dao = new WorkflowListDAO();
                var dao = DataAccessObjectFactory<WorkflowList>.Instance.GetDAO("Application.DataAccess.Workflow.WorkflowListDAO");
                //var dao = DaoFactory<WorkflowList>.Instance.GetDAO("Application.DataAccess.Workflow.WorkflowListDAO");
                var x = dao.Get(session, 1);

                string xml = x.ToString();

                var wf = ValueObjectFactory<WorkflowList>.Instance.Create();
                wf.CopyFrom(x);

                session.BeginTrans();

                wf.Name = x.Name + "(updated)";
                dao.Update(session, wf);
                wf.Name = x.Name + "(insert)";
                wf.Id = 0;
                dao.Create(session, wf);

                session.Commit();
               // session.Rollback();

            }



            Console.ReadKey();

        }
    }
}
