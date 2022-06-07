using System;

namespace Web.Models;

public class ExpenseHistoryViewModel
{
    public int ExpenseId { get; set; }
    public string Purpose { get; set; }
    public string? Remark { get; set; }
    public int StatusId { get; set; }
    public string Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}
