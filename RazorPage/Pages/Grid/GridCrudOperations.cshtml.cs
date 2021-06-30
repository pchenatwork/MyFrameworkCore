using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppBase.Core.DataAccess;
using Application.BusinessLogic;
using Application.BusinessLogic.Workflow;
using Application.ValueObjects.Workflow; 
//using Framework.Core.DataAccess;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Models;

namespace RazorPage.Pages.Grid
{
    public class GridCrudOperationsModel : PageModel
    {
        public static IList<EmployeeVM> _emp;

        public void OnGet()
        {

            string sProviderName = "Microsoft.Data.SqlClient";
            string sConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetCurrentDirectory() + "\\Workflow.mdf;Integrated Security=True";

            using (var dbSession = DbSessionFactory.Instance.GetSession(sProviderName, sConnectionString))
            {
                var NodeManager = (WorkflowNodeManager)ManagerFactory<WorkflowNode>.Instance.GetManager(dbSession);

            }
       //     WorkflowNodeManager mgrWFNode = ManagerFactory<WorkflowNode>.Instance.GetManager(dbSession);

      //      var x = mgrWFNode.FindByCriteria()

            if (_emp == null)
            {
                _emp = new List<EmployeeVM>();
                _emp.Add(new EmployeeVM() { Id = 1, FirstName = "Bobb", LastName = "Ross" });
                _emp.Add(new EmployeeVM() { Id = 2, FirstName = "Pradeep", LastName = "Raj" });
                _emp.Add(new EmployeeVM() { Id = 3, FirstName = "Arun", LastName = "Kumar" });
                _emp.Add(new EmployeeVM() { Id = 4, FirstName = "Paul", LastName = "Chen" });
            }
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(_emp.ToDataSourceResult(request));
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, EmployeeVM order)
        {
            order.Id = _emp.Count + 2;
            _emp.Add(order);

            return new JsonResult(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, EmployeeVM order)
        {
            _emp.Where(x => x.Id == order.Id).Select(x => order);

            return new JsonResult(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, EmployeeVM order)
        {
            _emp.Remove(_emp.FirstOrDefault(x => x.Id == order.Id));

            return new JsonResult(new[] { order }.ToDataSourceResult(request, ModelState));
        }

    }
}
