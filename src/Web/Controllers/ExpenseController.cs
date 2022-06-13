using ExpenseMgmt.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

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
        try
        {
            var expenses = await _context.Expenses
                .Include(e => e.Employee)
                .Include(e => e.Bills)
                .Include(e => e.History.Where(h => h.IsLatest == true))
                .ThenInclude(h => h.ExpenseStatusType).ToListAsync();

            IEnumerable<Expense> expenseQuery = new List<Expense>();

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
                userrole = BitConverter.ToInt32(value, 0);
                userid = BitConverter.ToInt32(uid, 0);
            }
            if (userrole == 0)
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
                    expenseQuery = expenses.Where(e => e.Employee.ManagerId == userid).Where(e => e.History.FirstOrDefault().StatusId == 1);
                    break;

                case 3: // accountant
                    expenseQuery = expenses.Where(e => e.History.FirstOrDefault().StatusId == 2);
                    break;

                case 4: // admin - revist this query , for now fetch everything in the system
                    expenseQuery = _context.Expenses;
                    break;
            }

            foreach (var e in expenseQuery)
            {
                string purpose = "";
                int statusId = 0;
                string statusType = "";
                int totalAmount = 0;
                string Remark = "";

                if (e.History == default)
                {
                    purpose = string.Empty;
                    statusId = 1;
                    statusType = "New";
                    Remark = "New";
                }
                else
                {
                    purpose = e.History.FirstOrDefault().Purpose;
                    statusId = e.History.FirstOrDefault().StatusId;
                    statusType = e.History.FirstOrDefault().ExpenseStatusType.Name;
                    Remark = e.History.FirstOrDefault().Remark;
                }

                if (e.Bills == null)
                {
                    totalAmount = 0;
                }
                else
                {
                    totalAmount = e.Bills.Sum(b => b.Amount);
                }


                if (e.History.FirstOrDefault() == default)
                    purpose = string.Empty;
                else
                    purpose = e.History.FirstOrDefault().Purpose;

                expenseDtos.Add(new ExpenseViewModel
                {
                    Id = e.Id,
                    Currency = e.Currency,
                    ModifiedOn = e.ModifiedOn,
                    CreatedByName = e.Employee.FullName,
                    CreatedOn = e.CreatedOn,
                    Purpose = purpose,
                    Status = statusId,
                    StatusType = statusType,
                    TotalAmount = totalAmount,
                    Remark = Remark
                });
            }

            return View(expenseDtos);
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return View();
        }
        finally
        {
            _context.SaveChanges();
        }
    }

    // GET: Expense/Create
    public IActionResult AddorEdit(int id = 0)
    {
        try
        {

            int UserRole = 0;
            if (HttpContext.Session.TryGetValue("roleid", out var value))
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(value);
                UserRole = BitConverter.ToInt32(value, 0);
            }

            if (id == 0)
                return View(new ExpenseViewModel());
            else
            {
                var expense = _context.Expenses.Include(e => e.History
                                .Where(h => h.IsLatest == true)).ThenInclude(h => h.ExpenseStatusType)
                                .Include(e => e.Employee)
                                .Include(e => e.Bills)
                                .SingleOrDefault(e => e.Id == id);

                if (expense == default)
                    return NotFound();

                ExpenseViewModel viewModel = new ExpenseViewModel();

                if (UserRole == 2 && expense.History.FirstOrDefault().StatusId == 1)
                {
                    viewModel = new ExpenseViewModel
                    {
                        Id = expense.Id,
                        CreatedByName = expense.Employee.FullName,
                        CreatedOn = expense.CreatedOn,
                        Currency = expense.Currency,
                        ModifiedOn = expense.ModifiedOn,
                        Purpose = expense.History.FirstOrDefault().Purpose,
                        Status = expense.History.FirstOrDefault().StatusId,
                        StatusType = expense.History.FirstOrDefault().ExpenseStatusType.Name,
                        Remark = string.Empty,
                        TotalAmount = expense.Bills.Sum(b => b.Amount),
                    };
                }
                else
                {
                    viewModel = new ExpenseViewModel
                    {
                        Id = expense.Id,
                        CreatedByName = expense.Employee.FullName,
                        CreatedOn = expense.CreatedOn,
                        Currency = expense.Currency,
                        ModifiedOn = expense.ModifiedOn,
                        Purpose = expense.History.FirstOrDefault().Purpose,
                        Status = expense.History.FirstOrDefault().StatusId,
                        StatusType = expense.History.FirstOrDefault().ExpenseStatusType.Name,
                        Remark = expense.History.FirstOrDefault().Remark,
                        TotalAmount = expense.Bills.Sum(b => b.Amount),
                    };
                }

                return View(viewModel);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return View();
        }
        finally
        {
            _context.SaveChanges();
        }


    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddorEdit([Bind("Id,Currency,Purpose,Remark")] ExpenseViewModel expenseViewModel, string submit)
    {
        try
        {
            int UserId = 0;
            if (HttpContext.Session.TryGetValue("UserId", out var value))
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(value);
                UserId = BitConverter.ToInt32(value, 0);
            }

            ModelState.Remove("Remark");
            if (ModelState.IsValid)
            {
                if (expenseViewModel.Id == 0)
                {
                    Expense expense = new Expense
                    {
                        Currency = expenseViewModel.Currency,
                        CreatedOn = DateTime.Now,
                        CreatedBy = UserId,
                        ModifiedOn = DateTime.Now,
                    };

                    _context.Expenses.Add(expense);
                    await _context.SaveChangesAsync();

                    ExpensesHistory history = new ExpensesHistory
                    {
                        ExpenseId = expense.Id,
                        CreatedOn = DateTime.Now,
                        StatusId = 1,
                        Purpose = expenseViewModel.Purpose,
                        IsLatest = true,
                        ModifiedOn = DateTime.Now,
                        Remark = "Created by Employee and pending for approval"
                    };

                    _context.ExpensesHistories.Add(history);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var expense = _context.Expenses.Include(e => e.History
                                .Where(h => h.IsLatest == true)).SingleOrDefault(e => e.Id == expenseViewModel.Id);

                    expense.Currency = expenseViewModel.Currency;
                    expense.ModifiedOn = DateTime.Now;

                    var existingHistory = expense.History.First();
                    existingHistory.IsLatest = false;
                    existingHistory.ModifiedOn = DateTime.Now;

                    ExpensesHistory newHistory = new ExpensesHistory();
                    newHistory.ExpenseId = expense.Id;
                    newHistory.CreatedOn = DateTime.Now;
                    newHistory.Purpose = expenseViewModel.Purpose;
                    newHistory.IsLatest = true;
                    newHistory.ModifiedOn = DateTime.Now;

                    if (submit == "Send Back For Review")
                    {
                        newHistory.StatusId = 5;
                        newHistory.Remark = "Modified by manager with remark : " + expenseViewModel.Remark;
                    }
                    else if (submit == "Approve")
                    {
                        newHistory.StatusId = 2;
                        newHistory.Remark = "Appoved by Manager with remark : " + expenseViewModel.Remark;
                    }
                    else if (submit == "Reject")
                    {
                        newHistory.StatusId = 4;
                        newHistory.Remark = "Rejected by manager with remark : " + expenseViewModel.Remark;
                    }
                    else
                    {
                        newHistory.StatusId = 1;
                        newHistory.Remark = "Modified by employee";
                    }

                    _context.Expenses.Update(expense);
                    _context.ExpensesHistories.Update(existingHistory);
                    _context.ExpensesHistories.Add(newHistory);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(expenseViewModel);
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return View(expenseViewModel);
        }
        finally
        {
            _context.SaveChanges();
        }
    }

    public async Task<IActionResult> Pay(int id = 0)
    {
        try
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
                var expense = _context.Expenses.Include(e => e.History
                            .Where(h => h.IsLatest == true)).SingleOrDefault(e => e.Id == id);

                expense.ModifiedOn = DateTime.Now;

                var existingHistory = expense.History.First();
                existingHistory.IsLatest = false;
                existingHistory.ModifiedOn = DateTime.Now;

                ExpensesHistory newHistory = new ExpensesHistory
                {
                    ExpenseId = expense.Id,
                    CreatedOn = DateTime.Now,
                    StatusId = 3,
                    Purpose = existingHistory.Purpose,
                    IsLatest = true,
                    ModifiedOn = DateTime.Now,
                    Remark = "Paied by accountant"
                };

                _context.Expenses.Update(expense);
                _context.ExpensesHistories.Update(existingHistory);
                _context.ExpensesHistories.Add(newHistory);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return View();
        }
        finally
        {
            _context.SaveChanges();
        }
    }

    public IActionResult History([Bind("ExpenseId")] IEnumerable<ExpenseHistoryViewModel> expenseHistoryViewModels, int id)
    {
        try
        {
            var expensesHistory = _context.ExpensesHistories.Include(x => x.ExpenseStatusType)
                .Include(x => x.Expense).ThenInclude(e => e.Employee)
                .Where(h => h.ExpenseId == id);

            var expenseHistoryVMs = expensesHistory.Select(e => new ExpenseHistoryViewModel
            {
                ExpenseId = e.ExpenseId,
                Status = e.ExpenseStatusType.Name,
                Purpose = e.Purpose,
                Remark = e.Remark,
                CreatedBy = e.Expense.Employee.FullName,
                CreatedOn = e.CreatedOn,
                ModifiedOn = e.ModifiedOn
            }).OrderByDescending(x=>x.ModifiedOn);

            return View(expenseHistoryVMs);
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return View();
        }
        finally
        {
            _context.SaveChanges();
        }
    }

    public async Task<IActionResult> AddorEditBill(int expenseid, int id = 0)
    {
        try
        {
            if (id == 0)
                return View(new BillViewModel());
            else
            {
                var bill = _context.Bills.Include(e => e.Expense).SingleOrDefault(bill => bill.Id == id);

                if (bill == default)
                    return NoContent();

                var viewModel = new BillViewModel
                {
                    Id = bill.Id,
                    Reason = bill.Reason,
                    Amount = bill.Amount,
                    ExpenseId = bill.ExpenseId,
                    IncurredOn = bill.IncurredOn,
                    Serial = bill.Serial,
                    UploadedOn = bill.UploadedOn,
                };
                return View(viewModel);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return View();
        }
        finally
        {
            _context.SaveChanges();
        }
    }

    // POST: Expense/Create or Update
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddorEditBill([Bind("Id,ExpenseId,Amount,IncurredOn,Serial,UploadedOn,Reason")] BillViewModel billViewModel, int expenseId)
    {
        try
        {
            int UserId = 0;
            if (HttpContext.Session.TryGetValue("UserId", out var value))
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(value);
                UserId = BitConverter.ToInt32(value, 0);
            }
            var realtedExpens = _context.Expenses.Include(e => e.Bills).Include(e => e.History.Where(h => h.IsLatest))
                .SingleOrDefault(x => x.Id == expenseId);

            var totalexpensesofar = realtedExpens.Bills.Sum(b => b.Amount);

            if ((totalexpensesofar + billViewModel.Amount) > 5000)
            {
                TempData["AmountValidationFailed"] = true;
                return View(billViewModel);
            }
            ModelState.Remove("Remark");
            if (ModelState.IsValid)
            {
                if (billViewModel.Id == 0)
                {
                    Bill bill = new Bill
                    {
                        Id = billViewModel.Id,
                        Reason = billViewModel.Reason,
                        Amount = billViewModel.Amount,
                        ExpenseId = expenseId,
                        IncurredOn = billViewModel.IncurredOn,
                        Serial = billViewModel.Serial,
                        UploadedOn = DateTime.Now,
                    };

                    _context.Bills.Add(bill);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var bill = _context.Bills.Include(e => e.Expense).SingleOrDefault(bill => bill.Id == billViewModel.Id);

                    bill.Amount = billViewModel.Amount;
                    bill.Reason = billViewModel.Reason;
                    bill.Serial = billViewModel.Serial;
                    bill.IncurredOn = billViewModel.IncurredOn;

                    _context.Bills.Update(bill);
                    await _context.SaveChangesAsync();
                }
                // update expense status also 
                var history = realtedExpens.History.SingleOrDefault();
                history.IsLatest = false;
                history.ModifiedOn = DateTime.Now;

                ExpensesHistory newHistory = new ExpensesHistory
                {
                    ExpenseId = realtedExpens.Id,
                    CreatedOn = DateTime.Now,
                    StatusId = 1,
                    Purpose = history.Purpose,
                    IsLatest = true,
                    ModifiedOn = DateTime.Now,
                    Remark = "Reviewd and sent for approval again"
                };

                _context.ExpensesHistories.Update(history);
                _context.ExpensesHistories.Add(newHistory);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ListBills), new { expenseid = expenseId });
            }
            return View(billViewModel);
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return View(billViewModel);
        }
        finally
        {
            _context.SaveChanges();
        }
    }

    public IActionResult ListBills([Bind("ExpenseId,Serial,Amount,Reason,IncurredOn,UploadedOn")] IEnumerable<BillViewModel> billViewModels, int expenseid)
    {
        try
        {

            var bills = _context.Bills
                .Include(x => x.Expense)
                .Where(h => h.ExpenseId == expenseid);

            if (bills != null && bills.Count() != 0)
            {
                var billVms = bills.Select(e => new BillViewModel
                {
                    Id = e.Id,
                    ExpenseId = e.ExpenseId,
                    Serial = e.Serial,
                    Amount = e.Amount,
                    IncurredOn = e.IncurredOn,
                    UploadedOn = e.UploadedOn,
                    Reason = e.Reason,
                }).ToList();

                return View(billVms);
            }
            return View();
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return View();
        }
        finally
        {
            _context.SaveChanges();
        }

    }

    // GET: Employee/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        try
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return RedirectToAction(nameof(Index));
        }
        finally
        {
            _context.SaveChanges();

        }
    }

    public async Task<IActionResult> DeleteBill(int? id)
    {
        try
        {
            var expense = await _context.Bills.FindAsync(id);
            _context.Bills.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ExceptionLogs log = new ExceptionLogs
            {
                CreatedOn = DateTime.Now,
                InnerException = ex.InnerException.Message,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            _context.ExceptionLogs.Add(log);
            return RedirectToAction(nameof(Index));
        }
        finally
        {
            _context.SaveChanges();
        }
    }
}
