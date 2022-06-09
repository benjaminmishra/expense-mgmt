using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMgmt.Data;

public class ExpenseMgmtDbContext : DbContext
{

    public ExpenseMgmtDbContext(DbContextOptions<ExpenseMgmtDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<ExpensesHistory> ExpensesHistories { get; set; }
    public DbSet<ExpenseStatusType> ExpenseStatusTypes { get; set; }
    public DbSet<ExceptionLogs> ExceptionLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<EmployeeRole>().HasData(new EmployeeRole
        {
            Id = 1,
            Name = "Employee",
            Description = "Employee"
        },
        new EmployeeRole
        {
            Id = 2,
            Name = "Manager",
            Description = "Manager"
        },
        new EmployeeRole
        {
            Id = 3,
            Name = "Accountant",
            Description = "Acc"
        },
        new EmployeeRole
        {
            Id = 4,
            Name = "Admin",
            Description = "Admin"
        });

        builder.Entity<ExpenseStatusType>().HasData(
            new ExpenseStatusType
            {
                Id = 1,
                Name = "Pending for approval",
                Description = "Pending for approval"
            },
            new ExpenseStatusType
            {
                Id = 2,
                Name = "Pending to be paid",
                Description = "Approved by manager, Pending with accountant"
            },
            new ExpenseStatusType
            {
                Id = 3,
                Name = "Paid",
                Description = "Paid by accountant"
            },
            new ExpenseStatusType
            {
                Id = 4,
                Name = "Rejected",
                Description = "Rejected by manager"
            },
            new ExpenseStatusType
            {
                Id = 5,
                Name = "Review",
                Description = "Sent back by manager"
            });

        builder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                FullName = "Jon Doe",
                RoleId = 1,
                ManagerId = 2,
                IsActive = true,
                Password = "xyz"
            },
            new Employee
            {
                Id = 5,
                FullName = "Employee Joe",
                RoleId = 1,
                ManagerId = 6,
                IsActive = true,
                Password = "xyz"
            },
            new Employee
            {
                Id = 2,
                FullName = "Jane Doe",
                RoleId = 2,
                ManagerId = 0,
                IsActive = true,
                Password = "abc"
            },
            new Employee
            {
                Id = 6,
                FullName = "Manager Doe",
                RoleId = 2,
                ManagerId = 0,
                IsActive = true,
                Password = "abc"
            },
            new Employee
            {
                Id = 3,
                FullName = "Accountant Doe",
                RoleId = 3,
                ManagerId = 2,
                IsActive = true,
                Password = "abc"
            },
            new Employee
            {
                Id = 4,
                FullName = "Admin Doe",
                RoleId = 4,
                ManagerId = 2,
                IsActive = true,
                Password = "abc"
            },
            new Employee
            {
                Id = 7,
                FullName = "Admin Joe2",
                RoleId = 4,
                ManagerId = 2,
                IsActive = true,
                Password = "abc"
            }
            );
    }
}

