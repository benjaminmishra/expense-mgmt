using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web.Models;

public class ExpenseViewModel
{
	public int Id { get; set; }
	[Required(ErrorMessage = "Currency is required")]
	public string Currency { get; set; }
	public string CreatedByName { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }
	public int Status { get; set; }
	public string StatusType { get; set; }
	[Required(ErrorMessage ="Purpose is required")]
	public string Purpose { get; set; }
	public string Remark { get; set; }
	public int TotalAmount { get; set; }
}