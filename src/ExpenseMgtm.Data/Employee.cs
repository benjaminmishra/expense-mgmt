using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace ExpenseMgtm.Data;

public class Employee
{
	public int Id { get; set; }
	public string FullName { get; set; }
	public int RoleId { get; set; }
	public int ManagerId { get; set; }
	public string Password { get; set; }
	public bool IsActive { get; set; }
}

