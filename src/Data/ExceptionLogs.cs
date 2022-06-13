using System.ComponentModel.DataAnnotations.Schema;
namespace ExpenseMgmt.Data;

public class ExceptionLogs
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public string Message { get; set; }
	public string InnerException { get; set; }
	public string StackTrace { get; set; }
	public DateTime CreatedOn { get; set; }
}

