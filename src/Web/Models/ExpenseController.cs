using System;
namespace Api.Models;

public class ExpenseDto
{
	public int Id { get; set; }
	public string Currency { get; set; }
	public int CreatedBy { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }
}