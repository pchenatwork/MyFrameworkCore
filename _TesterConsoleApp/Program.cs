
using System;
using System.Collections.Generic;

//using Framework.Core.BusinessLogic;
//using Framework.Core.DataAccess;
//using Framework.Core.Static;
//using Framework.Core.Utilities;
//using Framework.Core.ValueObjects;
using Application.BusinessLogic;
using Application.BusinessLogic.Workflow;
using Application.DAO;
using Application.JobServices;
using Application.ValueObjects.Workflow;
//using Application.BusinessLogic.Utilities;
using System.IO;
using Application.DAO.Workflow;
using AppBase.Core.Interfaces;
using AppBase.Core.ValueObjects;
using AppBase.Core.DataAccess;
using System.Threading;

namespace _TesterConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //DbParams.ProviderName = "Microsoft.Data.SqlClient";
            ////DbParams.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\GitHub\Source\Repos\pchenatwork\MyFrameworkCore\Application.DB\Workflow.mdf;Integrated Security=True";
            //DbParams.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetCurrentDirectory() + "\\Workflow.mdf;Integrated Security=True";


            var x = Directory.GetCurrentDirectory();
            var y = AppDomain.CurrentDomain.BaseDirectory;

            //  DbParams.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\_GitHub\Source\Repos\pchenatwork\MyFrameworkCore\Application.DB\Workflow.mdf;Integrated Security=True";
            //  DbParams.ConnectionString = @"Data Source=localhost;Initial Catalog=MyFramework;Integrated Security=True";
            //  JobRunner.RunJob("Job1", "Hello World");
            //  JobRunner.RunJob("Job2", "Ni Hao");
            //  Console.ReadKey();

            //string providerName = "System.Data.SqlClient";
            //  string providerName = "Microsoft.Data.SqlClient";
            //   string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\GitHub\Source\Repos\pchenatwork\MyFrameworkCore\Application.DB\Workflow.mdf;Integrated Security=True";
            //  string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\_GitHub\Source\Repos\pchenatwork\MyFrameworkCore\Application.DB\Workflow.mdf;Integrated Security=True";
            // string connectionString = @"Data Source=localhost;Initial Catalog=MyFramework;Integrated Security=True";

            //   testDAO(providerName, connectionString);
            ///  testDapperDAO(providerName, connectionString); 
            ///  TestWorkflowNodeDAO(providerName, connectionString);
            /// testWorkflowHistoryDAO(providerName, connectionString);
            /// testManagerFactory(providerName, connectionString);
            /// testValueObjectXML();

            ///testWorkflowControl(providerName, connectionString);

            /// testTimeOffWorkflow();
            // testTimeOffUtilities();

            /**** Working ****/
            using (IDbSession session = DbSessionFactory.Instance.GetSession(DbEnum.SQL.Name, DbEnum.SQL.Extra))
            {
                //session.BeginTrans();

                for (int i = 0; i < 100; i++)
                {
                    updateme(session);
                    Thread thr1 = new Thread(() => updateme(session));
                    Thread thr2 = new Thread(() => updateme(session));
                    //Thread thr3 = new Thread(() => updateme(session));
                    thr1.Start();
                    thr2.Start();
                    //thr3.Start();
                }

                //session.Commit();

                /*

                string daoClassName = "Application.DAO.Workflow.WorkflowListDAO";
                var dao = DataAccessObjectFactory<WorkflowList>.Instance.GetDAO(daoClassName);

                //var mgr = new Manager<WorkflowList>(session, dao);

                //var mgr = ManagerFactory<WorkflowList>.Instance.GetManager(session, daoClassName);

                var mgrNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session);
                //WorkflowNodeManager mgrNode = (WorkflowNodeManager)ManagerFactory<WorkflowNode>.Instance.GetManager(session);
                var list = mgrNode.GetAll();
                var list2 = mgrNode.FindByCriteria(WorkflowNodeManager.FIND_BY_WORKFLOWID, new object[] { "1" });

                WorkflowListManager mgr = (WorkflowListManager)ManagerFactory<WorkflowList>.Instance.GetManager(session);

                var wfl1 = mgr.Get(1);
                //var wfl = mgr.GetAll();

                var wfl_new = ValueObjectFactory<WorkflowList>.Instance.Create();
                wfl_new.CopyFrom(wfl1);

                session.BeginTrans();

                wfl_new.Name = wfl1.Name + "(updated committed)";
                mgr.Update(wfl_new);

                //dao.Update(session, wf);
                wfl_new.Name = wfl1.Name + "(insert committed)";
                wfl_new.Id = 0;
                mgr.Create(wfl_new);
                //dao.Create(session, wf);

                //session.Rollback();
                session.Commit();


                //session.BeginTrans();
                //wf.Name = wfl1.Name + "(updated commit)";
                //dao.Update(session, wf);
                //wf.Name = wfl1.Name + "(insert commit)";
                //wf.Id = 0;
                //dao.Create(session, wf);

                //session.Commit();
            }

            / **** Working ****/

                //Console.ReadKey();

            }


            static void updateme(IDbSession session)
            {
                var mgr = (WorkflowNodeManager)ManagerFactory<WorkflowNode>.Instance.GetManager(session);

                var wfn1 = mgr.Get(1);

                int.TryParse(wfn1.NodeValue, out int i);

                wfn1.NodeValue = (i+1).ToString();
                int.TryParse(wfn1.Description, out int j);
                wfn1.Description = (j + 1).ToString();

                Thread thread = Thread.CurrentThread;

                Console.WriteLine("Current Thread {0}, NodeValue = {1}, description = {2}", thread.ManagedThreadId.ToString(), wfn1.NodeValue, wfn1.Description);
                mgr.Update(wfn1);
            }

            //static private void testTimeOffUtilities()
            //{
            //    TimeoffRequest request = ValueObjectFactory<TimeoffRequest>.Instance.Create();

            //    request.User = "PChen";
            //    request.FromDate = new DateTime(2020, 1, 11);
            //    request.Note = "Not decided yet";
            //    request.UpdatedBy = "tmppch";

            //    using (DbSession session = DbSessionFactory.Instance.GetSession(DbParams.ProviderName, DbParams.ConnectionString))
            //    {

            //        //var tranId = TimeoffUtility.Create(session, request, "This is workflow note that shouldn't be part of TimeoffRequest");

            //        //int transactionId = WFRunner.Create(session, "User", "Create a new plan");

            //        List<string> messages = new List<string>();
            //        bool OK = false;
            //        //OK = TimeoffUtility.Workflow(session, 10, TimeoffAction.SubmitMoreInfo.Name, "Subbmitter", "Try to Submit with wrong ation", ref messages);
            //        //OK = TimeoffUtility.Workflow(session, 1, TimeoffAction.SubmitPlan.Name, "Subbmitter", "Try to Submit again", ref messages);

            //        //OK = TimeoffUtility.Workflow(session, 1, TimeoffAction.HRRequestMoreInfo.Name, "HR", "request More Indo", ref messages);
            //        //OK = TimeoffUtility.Workflow(session, 1, TimeoffAction.SubmitMoreInfo.Name, "User", "Submit More Info", ref messages);
            //        //OK = TimeoffUtility.Workflow(session, 1, TimeoffAction.HRApproval.Name, "HR", "HR Approved now", ref messages);
            //        OK = TimeoffUtility.Workflow(session, 1, TimeoffAction.ManagerApproval.Name, "Manager", "Manager Approved now", ref messages);
            //    }
            //}

            static void testTimeOffWorkflow()
            {
                var a = TimeoffAction.SubmitPlan.Name;
                var b = TimeoffAction.SubmitPlan.Extra;
                var c = TimeoffAction.SubmitPlan.Id;

                var WFRunner = WorkflowFactory.Instance.GetWorkflow(WorkflowEnum.TimeoffMainFlow);
                var workflowId = WFRunner.WorkflowIds;

                //using (IDbSession session = DbSessionFactory.Instance.GetSession(DbParams.ProviderName, DbParams.ConnectionString))

                using (IDbSession session = DbSessionFactory.Instance.GetSession(DbEnum.SQL.Name, DbEnum.SQL.Extra))
                {
                    //int transactionId = WFRunner.Create(session, "User", "Create a new plan");

                    List<string> messages = new List<string>();
                    bool OK = false;
                    // OK = WFRunner.ExecuteAction(session, 2, 2, TimeoffAction.SubmitMoreInfo.Name, "Subbmitter", "Try to Submit with wrong ation", ref messages);
                    //OK = WFRunner.ExecuteAction(session, 1, 2, TimeoffAction.SubmitPlan.Name, "Subbmitter", "Try to Submit again", ref messages);

                    //OK = WFRunner.ExecuteAction(session, 2, 2, TimeoffAction.HRRequestMoreInfo.Name, "HR", "request More Indo", ref messages);
                    //OK = WFRunner.ExecuteAction(session, 2, 2, TimeoffAction.SubmitMoreInfo.Name, "User", "Submit More Info", ref messages);
                    //OK = WFRunner.ExecuteAction(session, 2, 2, TimeoffAction.HRApproval.Name, "HR", "HR Approved now", ref messages);
                    OK = WFRunner.ExecuteAction(session, 2, TimeoffAction.ManagerApproval.Name, "Manager", "Manager Approved now", ref messages);
                }
            }

            static void testWorkflowControl(string providerName, string connectionString)
            {
                using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
                {
                    WorkflowHistory hist;
                    WorkflowNode actionNode;
                    int TransactionId = 0;
                    //
                    //WorkflowControl.NewTransaction(session, 1, "NewTran")

                    /*/////////////
                    actionNode = ValueObjectFactory<WorkflowNode>.Instance.Create();
                    actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(11);
                    actionNode = WorkflowControl.Get_Key_ActionNode(session, actionNode);
                    var x = WorkflowControl.GetActionGroup(session, actionNode);

                    actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(14);
                    var y = WorkflowControl.GetActionGroup(session, actionNode);

                    var b = x == y;
                    *////////////////



                    //for (int i = 0; i<2; i++)
                    //{  //tested
                    //hist = WorkflowControl.NewTransaction(session, 1, "NewWorkflowUser", "Blar blar blar ...");
                    //TransactionId = hist.TransactionId;
                    string msg = string.Empty;

                    actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager_Old_XX(session).Get(10); // Manager Approval
                    hist = WorkflowControl.DoActionNode(session, 1, actionNode, "manager", "Manager Approval ", ref msg);

                    //   actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(14); // HR Rount "Auto"
                    //   hist = WorkflowControl.DoActionNode(session, 1, actionNode, "Route2HR_finished", "Route to HR finished, should ", ref msg);



                    //    actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(8); // User submit
                    //    hist = WorkflowControl.DoActionNode(session, TransactionId, actionNode, "SubmitUser", "User submit plan");
                    //    actionNode = ManagerFactory<WorkflowNode>.Instance.GetManager(session).Get(9); // Route to HR
                    //    hist = WorkflowControl.DoActionNode(session, TransactionId, actionNode, "Route2HR_OK", "Route to HR should success");
                    // }
                }
            }

            static void testManagerFactory(string providerName, string connectionString)
            {
                using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
                {
                    string daoClassName = "Application.DAO.Workflow.WorkflowListDAO";
                    var mgr2 = ManagerFactory<WorkflowList>.Instance.GetManager_Old_XX(session, daoClassName);
                    var mgr1 = ManagerFactory<WorkflowList>.Instance.GetManager_Old_XX(session);

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

            static void testValueObjectXML()
            {
                var wfh = ValueObjectFactory<WorkflowHistory>.Instance.Create();
                wfh.ApprovalDate = new DateTime(2019, 11, 16);
                wfh.IsCurrent = false;
                wfh.Id = 123;
                wfh.NodeId = 9;
                wfh.Comment = "<How much> wood n/// would a <!-- --> woodchop chop?? ";
                //var a = wfh.ToJson();
                var b = wfh.ToString();
            }

            static void testWorkflowListDAO(string providerName, string connectionString)
            {

                using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
                {
                    //WorkflowListDAO dao = new WorkflowListDAO();
                    // var dao = DataAccessObjectFactory<WorkflowList>.Instance.GetDAO("Application.DAO.Workflow.WorkflowListDAO");
                    // var dao = DataAccessObjectFactory<WorkflowList>.Instance.GetDAO("Application.DAO.Workflow.WorkflowListDAO");
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
            static void testWorkflowHistoryDAO(string providerName, string connectionString)
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
                    for (int j = 10; j <= 15; j++)
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

            static void TestWorkflowNodeDAO(string providerName, string connectionString)
            {
                using (IDbSession session = DbSessionFactory.Instance.GetSession(providerName, connectionString))
                {
                    int workflowId = 1; string actionName = "SubmitPlan";

                    var dao1 = DataAccessObjectFactory<WorkflowNode>.Instance.GetDAO();
                    var o = dao1.FindByCriteria(session, WorkflowNodeDAO.FIND_BY_ACTIONNAME, new object[] { workflowId, actionName });
                    var z = dao1.FindByCriteria(session, WorkflowNodeDAO.FIND_BY_WORKFLOWID, new object[] { workflowId });
                    var x = dao1.Get(session, 6);
                    var y = dao1.GetAll(session);

                }
            }

            static void testDapperDAO(string providerName, string connectionString)
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
}
