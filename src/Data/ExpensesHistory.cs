using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseMgmt.Data;

public class ExpensesHistory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public int ExpenseId { get; set; }
	public string Purpose { get; set; }
	public string? Remark { get; set; }
	public int StatusId { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }
	public bool IsLatest { get; set; }

    [ForeignKey("ExpenseId")]
	public Expense Expense { get; set; }

	[ForeignKey("StatusId")]
	public ExpenseStatusType ExpenseStatusType { get; set; }
}

