using Microsoft.AspNetCore.Mvc;
using ExpenseMgmt.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Web.Models;

namespace Web.Controllers;

[TypeFilter(typeof(GlobalExceptionFilters))]
public class ReportsController : Controller
{
    private readonly ExpenseMgmtDbContext _dbContext;

    public ReportsController(ExpenseMgmtDbContext context)
    { 
        _dbContext = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult TotalExpensePerEmployee()
    {
        var employees = _dbContext.Employees.Include(e => e.ExpensesLogged).ThenInclude(e => e.Bills).Where(e=>e.RoleId==1);
        var results = employees.Select(e => new TotalExpensePerEmployeeViewModel { 
            EmployeeId = e.Id,
            EmployeeFullName = e.FullName,
            EmployeeManagerId = e.ManagerId,
            TotalExpense = e.ExpensesLogged.SelectMany(x=>x.Bills).Sum(b=>b.Amount)
        });
       
        return View(results);
    }
}
