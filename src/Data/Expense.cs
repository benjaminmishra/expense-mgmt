using System;
namespace ExpenseMgmt.Data;

public class Expense
{
	public int Id { get; set; }
	public string Currency { get; set; }
	public int CreatedBy { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }

	public List<Bill> Bills { get; set; }
	public Employee Employee { get; set; }


}

