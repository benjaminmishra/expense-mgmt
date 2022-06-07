using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Web.Models;
using ExpenseMgmt.Data;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpenseMgmtDbContext _dbCOntext;
        private readonly Microsoft.AspNetCore.Http.ISession session;

        public HomeController(ILogger<HomeController> logger, ExpenseMgmtDbContext dbContext, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dbCOntext = dbContext;
            session = this.session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var employee = _dbCOntext.Employees.SingleOrDefault(e => e.Id == user.EmployeeId && e.IsActive && e.Password == user.Password);
                if (employee != default)
                {
                    HttpContext.Session.SetInt32("UserRole",employee.RoleId);
                    HttpContext.Session.SetInt32("UserId", user.EmployeeId);
                    TempData["userid"] = user.EmployeeId;
                    TempData["userName"] = employee.FullName;
                    TempData["roleid"] = employee.RoleId;
                    return Redirect("/Expense/Index");
                }
            }

            return RedirectToAction("Index",user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData.Clear();
            return Redirect("/Home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
