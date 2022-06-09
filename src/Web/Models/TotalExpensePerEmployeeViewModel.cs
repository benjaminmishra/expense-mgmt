using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web.Models;

public class TotalExpensePerEmployeeViewModel
{
    [DisplayName("Employee Id")]
    public int EmployeeId { get; set; }
    [DisplayName("Full Name")]
    public string EmployeeFullName { get; set; }
    [DisplayName("Total Expense")]
    public int TotalExpense { get; set; }
    [DisplayName("Manager Id")]
    public int EmployeeManagerId { get; set; }
}
