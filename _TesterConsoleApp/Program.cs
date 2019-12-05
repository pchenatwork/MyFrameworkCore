using Application.JobServices;
using Framework.Core.DataAccess;
using Application.ValueObjects.Workflow;

using System;

using Application.DataAccess.Workflow;
using Application.DataAccess;
using Framework.Core.ValueObjects;
using Framework.Core.BusinessLogic;
using Application.BusinessLogic;
using Framework.Core.Utilities;
using Application.BusinessLogic.Workflow;

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
           string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\GitHub\Source\Repos\pchenatwork\MyFrameworkCore\Application.DB\Workflow.mdf;Integrated Security=True";
           //  string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\_GitHub\Source\Repos\pchenatwork\MyFrameworkCore\Application.DB\Workflow.mdf;Integrated Security=True";
           //   string connectionString = @"Data Source=localhost;Initial Catalog=MyFramework;Integrated Security=True";

            ///    testDAO(providerName, connectionString);
            ///  testDapperDAO(providerName, connectionString); 
            /// TestWorkflowNodeDAO(providerName, connectionString);
            /// testWorkflowHistoryDAO(providerName, connectionString);
            /// testManagerFactory(providerName, connectionString);
            /// testValueObjectXML();

            testWorkflowControl(providerName, connectionString);

            /**** Working ****
            using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
            {

                string daoClassName = "Application.DataAccess.Workflow.WorkflowListDAO";
                var dao = DataAccessObjectFactory<WorkflowList>.Instance.GetDAO(daoClassName);

                //var mgr = new Manager<WorkflowList>(session, dao);

                var mgr = ManagerFactory<WorkflowList>.Instance.GetManager(session, daoClassName);

                var wf1 = mgr.Get(1);

                var wf = ValueObjectFactory<WorkflowList>.Instance.Create();
                wf.CopyFrom(wf1);

                session.BeginTrans();

                wf.Name = wf1.Name + "(updated rollback)";
                dao.Update(session, wf);
                wf.Name = wf1.Name + "(insert rollback)";
                wf.Id = 0;
                dao.Create(session, wf);

                session.Rollback();


                session.BeginTrans();
                wf.Name = wf1.Name + "(updated commit)";
                dao.Update(session, wf);
                wf.Name = wf1.Name + "(insert commit)";
                wf.Id = 0;
                dao.Create(session, wf);

                session.Commit();
            }

            **** Working ****/

            Console.ReadKey();

        }

        static private void testWorkflowControl(string providerName, string connectionString)
        {
            using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
            {
                WorkflowHistory hist; 
                WorkflowNode actionNode;
                int TransactionId = 0;
                //////////////////////////////
                actionNode = ValueObjectFactory<WorkflowNode>.Instance.Create();
                actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(11);
                var x = WorkflowControl.GetActionGroup(session, actionNode);

                actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(14);
                var y = WorkflowControl.GetActionGroup(session, actionNode);

                var b = x == y;



                //for (int i = 0; i<2; i++)
                //{  //tested
                //    hist = WorkflowControl.NewTransaction(session, 1, "NewWorkflowUser", "Blar blar blar ...");
                //    TransactionId = hist.TransactionId;
                //    actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(9); // Route to HR
                //    hist = WorkflowControl.DoActionNode(session, TransactionId, actionNode, "Route2HR_fail", "Route to HR should fail");
                //    actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(8); // User submit
                //    hist = WorkflowControl.DoActionNode(session, TransactionId, actionNode, "SubmitUser", "User submit plan");
                //    actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(9); // Route to HR
                //    hist = WorkflowControl.DoActionNode(session, TransactionId, actionNode, "Route2HR_OK", "Route to HR should success");
                // }
            }
        }

        static private void testManagerFactory(string providerName, string connectionString)
        {
            using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
            {                
                string daoClassName = "Application.DataAccess.Workflow.WorkflowListDAO";
                var mgr2 = ManagerFactory<WorkflowList>.Instance.GetManager(session, daoClassName);
                var mgr1 = ManagerFactory<WorkflowList>.Instance.GetManager(session);

                var wf1 = mgr1.Get(1);
                wf1 = mgr2.Get(1);

                var wf = ValueObjectFactory<WorkflowList>.Instance.Create();
                wf.CopyFrom(wf1);

                session.BeginTrans();

                wf.Name = wf1.Name + "(updated rollback 1)";
                mgr1.Update(wf);
                wf.Name = wf1.Name + "(updated rollback 2)";
                mgr1.Update(wf);
                wf.Name = wf1.Name + "(updated reset)";
                mgr1.Update(wf);

                session.Rollback();

                session.BeginTrans();

                wf.Name = wf1.Name + "(updated Commit 1)";
                mgr1.Update(wf);
                wf.Name = wf1.Name + "(updated Commit 2)";
                mgr1.Update(wf);
                wf.Name = wf1.Name + "(updated Commit)";
                mgr1.Update(wf);
                
                session.Commit();
            }
        }

        static private void testValueObjectXML()
        {
            var wfh = ValueObjectFactory<WorkflowHistory>.Instance.Create();
            wfh.ApprovalDate = new DateTime(2019,11,16);
            wfh.IsCurrent = false;
            wfh.Id = 123;
            wfh.NodeId = 9;
            wfh.Comment = "<How much> wood n/// would a <!-- --> woodchop chop?? ";
            var a = wfh.ToJson();
            var b = wfh.ToString();
        }

        static private void testWorkflowListDAO(string providerName, string connectionString)
        {

            using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
            {
                //WorkflowListDAO dao = new WorkflowListDAO();
               // var dao = DataAccessObjectFactory<WorkflowList>.Instance.GetDAO("Application.DataAccess.Workflow.WorkflowListDAO");
               // var dao = DataAccessObjectFactory<WorkflowList>.Instance.GetDAO("Application.DataAccess.Workflow.WorkflowListDAO");
                var dao2 = DataAccessObjectFactory<WorkflowList>.Instance.GetDAO();
               //var dao2 = DaoFactory<WorkflowList>.Instance.GetDAO();
                //var x1 = dao2.Get(session, 1);
                var x2 = dao2.Get(session, 1);

                string xml = x2.ToString();

                var wf = ValueObjectFactory<WorkflowList>.Instance.Create();
                wf.CopyFrom(x2);

                session.BeginTrans();

                wf.Name = x2.Name + "(updated)";
                dao2.Update(session, wf);
                wf.Name = x2.Name + "(insert)";
                wf.Id = 0;
                dao2.Create(session, wf);

                session.Commit();
                // session.Rollback();
            }
        }
        static private void testWorkflowHistoryDAO(string providerName, string connectionString)
        {
            using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
            {
                
                var o = ValueObjectFactory<WorkflowHistory>.Instance.Create();
                o.WorkflowId = 1;
                o.NodeId = 1;
                o.Comment = "asdf <  //n/ <!--> {{}} :: ";
                var dao = DataAccessObjectFactory<WorkflowHistory>.Instance.GetDAO();

                var aaa = dao.FindByCriteria(session, WorkflowHistoryDAO.FIND_BY_TRAN, new object[] { 1, 1 });

                //var bbb = dao.FindByCriteria(session, WorkflowHistoryDAO.FIND_BY_TRAN, new object[] { 1, -1 });

                var ccc = dao.FindByCriteria(session, WorkflowHistoryDAO.FIND_BY_TRAN, new object[] { 1 });
                var x = dao.Get(session, 11);
                
                session.BeginTrans();
                int lastid = 0;
                for (int j = 10; j<=15; j++)
                {
                    o.Id = 0; // make sure insert first
                    o.PrevHistoryId = lastid;
                    var i = dao.Create(session, o);
                    o = dao.Get(session, i);
                    o.NodeId = j;
                    var b = dao.Update(session, o);
                    o.NodeId = 0;
                    lastid = o.Id;
                }
                session.Commit();
                //session.Rollback();
            }
        }

        static private void TestWorkflowNodeDAO(string providerName, string connectionString)
        {
            using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
            {
                int workflowId = 1; string actionName = "SubmitPlan";

                var dao1 = DataAccessObjectFactory<WorkflowNode>.Instance.GetDAO();
                //var o = dao1.FindByCriteria(session, "WorkflowNodesFindByActionName", new object[] { workflowId, actionName });
                var z = dao1.FindByCriteria(session, WorkflowNodeDAO.FIND_BY_WORKFLOWID, new object[] { workflowId });
                var x = dao1.Get(session, 6);
                var y = dao1.GetAll(session);

            }
        }

        static private void testDapperDAO(string providerName, string connectionString)
        {

            using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
            {
               var dao = DataAccessObjectFactory<WorkflowHistory>.Instance.GetDAO();

                var x1 = dao.Get(session, 1);

                var dao2 = DataAccessObjectFactory<WorkflowNode>.Instance.GetDAO();
                var x2 = dao2.Get(session, 2);
            }
        }
    }
}
