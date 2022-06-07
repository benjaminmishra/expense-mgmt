using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using ExpenseMgmt.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers;

public class ExpenseController : Controller
{
    private readonly ExpenseMgmtDbContext _context;
    private readonly Microsoft.AspNetCore.Http.ISession session;

    public ExpenseController(ExpenseMgmtDbContext context, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
    {
        //ExpenseController
        _context = context;
        this.session = httpContextAccessor.HttpContext.Session;
    }

    // GET: /<controller>/
    //public IActionResult Index()
    //{
    //    return View();
    //}

    public async Task<IActionResult> Index(User user)
    {
        var expenses = _context.Expenses.Include(e => e.Employee)
            .Include(e=>e.History
                            .Where(h=>h.IsLatest==true)
                            );

        IEnumerable<Expense> expenseQuery =  new List<Expense>();

        int userrole = 0;
        int userid = 0;
        
        if (HttpContext.Session.TryGetValue("UserRole", out var value) &&
            HttpContext.Session.TryGetValue("UserId", out var uid))
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(value);
                Array.Reverse(uid);
            }
            userrole = BitConverter.ToInt32(value,0);
            userid = BitConverter.ToInt32(uid, 0);
        }
        if (userrole==0)
        {
            return Redirect("Home/Index");
        }

        ViewData["userrole"] = userrole;

        List<ExpenseViewModel> expenseDtos = new List<ExpenseViewModel>();

        switch (userrole)
        {
            case 1: // employee
                expenseQuery = expenses.Where(e => e.CreatedBy == userid);
                break;
            case 2: // manager
                expenseQuery = expenses.Where(e => e.Employee.ManagerId == userid);
                break;

            case 3: // accountant
                expenseQuery = _context.ExpensesHistories.Where(eh => eh.IsLatest && eh.StatusId == 3).Select(x => x.Expense);
                break;

            case 4: // admin - revist this query , for now fetch everything in the system
                expenseQuery = _context.Expenses;
                break;
        }

        foreach (var e in expenseQuery)
        {
            expenseDtos.Add(new ExpenseViewModel
            {
                Id = e.Id,
                Currency = e.Currency,
                ModifiedOn = e.ModifiedOn,
                CreatedBy = e.CreatedBy,
                CreatedOn = e.CreatedOn,
                Purpose = e.History.First().Purpose
            });
        }

        return View(expenseDtos);
    }

    // GET: Expense/Create
    public IActionResult AddorEdit(int id = 0)
    {
        if (id == 0)
            return View(new ExpenseViewModel());
        else
        {
            var expense = _context.Expenses.Include(e => e.History
                            .Where(h => h.IsLatest == true)).SingleOrDefault(e=>e.Id ==id);

            if (expense == default)
                return NotFound();

            var viewModel = new ExpenseViewModel { 
                Id = expense.Id,
                CreatedBy = expense.CreatedBy,
                CreatedOn = expense.CreatedOn,
                Currency = expense.Currency,
                ModifiedOn = expense.ModifiedOn,
                Purpose = expense.History.First().Purpose
            };
            return View(viewModel);
        }
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddorEdit([Bind("Id,Currency,Purpose")] ExpenseViewModel expenseViewModel)
    {
        int UserId = 0;
        if (HttpContext.Session.TryGetValue("UserId", out var value))
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value);
            UserId = BitConverter.ToInt32(value, 0);
        }

        if (ModelState.IsValid)
        {
            if (expenseViewModel.Id == 0)
            {
                Expense expense = new Expense {
                    Currency = expenseViewModel.Currency,
                    CreatedOn = DateTime.Now,
                    CreatedBy = UserId,
                    ModifiedOn = DateTime.Now,
                };
                
                _context.Expenses.Add(expense);
                await _context.SaveChangesAsync();
                
                ExpensesHistory history = new ExpensesHistory { 
                    ExpenseId  = expense.Id,
                    CreatedOn = DateTime.Now,
                    StatusId = 1,
                    Purpose = expenseViewModel.Purpose,
                    IsLatest = true,
                    ModifiedOn = DateTime.Now,

                };

                _context.ExpensesHistories.Add(history);
                await _context.SaveChangesAsync();
            }
            //else
            //    _context.Update(expense);

            
            return RedirectToAction(nameof(Index));
        }
        return View(expenseViewModel);
    }

    // GET: Employee/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
