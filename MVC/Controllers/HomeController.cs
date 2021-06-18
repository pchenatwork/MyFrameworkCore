using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult EmployeeList([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<EmployeeVM> _emp = new List<EmployeeVM>();
                _emp.Add(new EmployeeVM() { Id = 1, FirstName = "Bobb", LastName = "Ross" }); 
                _emp.Add(new EmployeeVM() { Id = 2, FirstName = "Pradeep", LastName = "Raj" }); 
                _emp.Add(new EmployeeVM() { Id = 3, FirstName = "Arun", LastName = "Kumar" });
                _emp.Add(new EmployeeVM() { Id = 4, FirstName = "Paul", LastName = "Chen" });
                DataSourceResult result = _emp.ToDataSourceResult(request);
                var x = Json(result);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
