using System;
namespace Web.Models;

public class ExpenseViewModel
{
	public int Id { get; set; }
	public string Currency { get; set; }
	public string CreatedByName { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }
	public int Status { get; set; }
	public string StatusType { get; set; }
	public string Purpose { get; set; }
	public string Remark { get; set; }
	public int TotalAmount { get; set; }
}