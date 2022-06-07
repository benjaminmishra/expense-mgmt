using System.ComponentModel.DataAnnotations;


namespace ExpenseMgmt.Data;

public class EmployeeRole
{
    [Key]
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
}

