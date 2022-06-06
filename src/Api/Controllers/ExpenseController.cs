using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpenseMgmt.Data;
using Api.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseController : Controller
{

    private readonly ILogger<ExpenseController> _logger;
    private readonly ExpenseMgmtDbContext _dbContext;

    public ExpenseController(ILogger<ExpenseController> logger, ExpenseMgmtDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }


    [HttpGet("{employeeid}")]
    public IEnumerable<ExpenseDto> GetExpenseForEmployee(int employeeid)
    {
        List<ExpenseDto> expenseDtos = new List<ExpenseDto>();
        var expense = _dbContext.Expenses.Where(e => e.CreatedBy == employeeid);

        foreach (var e in expense)
        {
            expenseDtos.Add(new ExpenseDto {
                Id = e.Id,
                Currency = e.Currency,
                ModifiedOn = e.ModifiedOn,
                CreatedBy = e.CreatedBy,
                CreatedOn = e.CreatedOn,
            });
        }

        return expenseDtos;
    }


    [HttpGet("{employeeid}")]
    public IEnumerable<ExpenseDto> GetExpensesForEmployeesByManager(int managerid)
    {
        //var empQuery = _dbContext.Employees.Where(emp => emp.ManagerId == managerid);
        List<ExpenseDto> expenseDtos = new List<ExpenseDto>();

        var expense = _dbContext.Expenses.Where(e => e.Employee.ManagerId == managerid);

        foreach (var e in expense)
        {
            expenseDtos.Add(new ExpenseDto
            {
                Id = e.Id,
                Currency = e.Currency,
                ModifiedOn = e.ModifiedOn,
                CreatedBy = e.CreatedBy,
                CreatedOn = e.CreatedOn,
            });
        }

        return expenseDtos;
    }

    [HttpGet("PendingForPayment")]
    public IEnumerable<ExpenseDto> GetExpensesPendingForPayment()
    {
        List<ExpenseDto> expenseDtos = new List<ExpenseDto>();
        var expense = _dbContext.ExpensesHistories.Where(eh => eh.IsLatest && eh.StatusId == 1).Select(x=>x.Expense).ToList();
        foreach (var e in expense)
        {
            expenseDtos.Add(new ExpenseDto
            {
                Id = e.Id,
                Currency = e.Currency,
                ModifiedOn = e.ModifiedOn,
                CreatedBy = e.CreatedBy,
                CreatedOn = e.CreatedOn,
            });
        }

        return expenseDtos;
    }

    [HttpGet]
    public IEnumerable<ExpenseDto> GetAll()
    {
        return _dbContext.Expenses.Select(e=> new ExpenseDto {
            Id = e.Id,
            Currency = e.Currency,
            ModifiedOn = e.ModifiedOn,
            CreatedBy = e.CreatedBy,
            CreatedOn = e.CreatedOn,
        });
    }

    [HttpGet("{expenseid}")]
    public IEnumerable<ExpenseDetailsDto> GetExpenseDetails(int expenseid)
    {
        List<ExpenseDetailsDto> expenseDtos = new List<ExpenseDetailsDto>();
        List<BillDto> bills = new List<BillDto>();
        var expense = _dbContext.Expenses.Where(e => e.Id == expenseid);

        foreach (var e in expense)
        {

            foreach (var b in e.Bills)
            {
                bills.Add(new BillDto {
                    Id = b.Id,
                    Reason = b.Reason,
                    Amount = b.Amount,
                    IncurredOn = b.IncurredOn,
                    UploadedOn = b.UploadedOn,
                    Serial = b.Serial
                });
            }


            expenseDtos.Add(new ExpenseDetailsDto
            {
                Id = e.Id,
                Currency = e.Currency,
                ModifiedOn = e.ModifiedOn,
                CreatedBy = e.CreatedBy,
                CreatedOn = e.CreatedOn,
                Bills = bills
            });
        }

        return expenseDtos;
    }

    [HttpPost]
    public IActionResult CreateExpense(ExpenseCreateDto expenseDto)
    {
        try
        {
            var expense = new Expense
            {
                Currency = expenseDto.Currency,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                CreatedBy = 1
            };
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();
            return Ok();
        }
        catch(Exception ex)
        {
            _dbContext.ExceptionLogs.Add(new ExceptionLogs {
                InnerException = ex.InnerException!.Message,
                Message = ex.Message,
                CreatedOn = DateTime.Now,
                StackTrace = ex.StackTrace!
            });
            _dbContext.SaveChanges();

            return StatusCode(500,ex.Message);
        }
    }
}

