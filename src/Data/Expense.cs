using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseMgmt.Data;

public class Expense
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public string Currency { get; set; }
	public int CreatedBy { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }

	public List<Bill> Bills { get; set; }
	public List<ExpensesHistory> History { get; set; }

    [ForeignKey("CreatedBy")]
	public Employee Employee { get; set; }
}

