using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using ExpenseMgmt.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers;

public class ExpenseController : Controller
{
    private readonly ExpenseMgmtDbContext _context;

    public ExpenseController(ExpenseMgmtDbContext context)
    {
        //ExpenseController
        _context = context;
    }

    // GET: /<controller>/
    //public IActionResult Index()
    //{
    //    return View();
    //}

    // GET: Employee
    public async Task<IActionResult> Index()
    {
        return View(await _context.Employees.ToListAsync());
    }

    // GET: Employee/Create
    public IActionResult AddorEdit(int id = 0)
    {
        if (id == 0)
            return View(new Expense());
        else
            return View(_context.Expenses.Find(id));
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddorEdit([Bind("EmployeeId,FirstName,LastName,Department,EmpCode,Position,Email,OfficeLocation")] Expense expense)
    {
        if (ModelState.IsValid)
        {
            if (expense.Id == 0)
                _context.Add(expense);
            else
                _context.Update(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(expense);
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
