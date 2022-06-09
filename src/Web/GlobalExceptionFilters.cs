using Microsoft.AspNetCore.Mvc.Filters;
using ExpenseMgmt.Data;
using System.Threading.Tasks;

namespace Web;

public class GlobalExceptionFilters: IAsyncExceptionFilter
{
    private readonly ExpenseMgmtDbContext _context;
    public GlobalExceptionFilters(ExpenseMgmtDbContext context)
    {
        _context = context;
    }


    public async Task OnExceptionAsync(ExceptionContext context)
    {
        if (!context.ExceptionHandled)
        {
            var exception = context.Exception;

            ExceptionLogs record = new ExceptionLogs { 
                InnerException = exception.InnerException.Message,
                CreatedOn = System.DateTime.Now,
                Message = exception.Message,
                StackTrace = exception.StackTrace,
            };
            _context.ExceptionLogs.Add(record);
            await _context.SaveChangesAsync();
        }
    }
}
