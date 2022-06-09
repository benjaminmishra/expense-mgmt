using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web.Models;

public class MonthlyTotalExpensePaidViewModel
{
    [DisplayName("Year")]
    public int  Year { get; set; }

    [DisplayName("Month")]
    public int Month { get; set; }

    [DisplayName("Total Expense Paid")]
    public int TotalExpensePaid { get; set; }
}
