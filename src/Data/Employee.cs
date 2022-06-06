using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseMgmt.Data;

public class Employee
{
    [Key]
    public int Id { get; set; }
    public string FullName { get; set; }
    public int RoleId { get; set; }
    public int ManagerId { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }

    public List<Expense> ExpensesLogged { get; set; }

    [ForeignKey("RoleId")]
    public EmployeeRole EmployeeRole { get; set; }
}

