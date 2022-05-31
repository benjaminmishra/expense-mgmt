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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //modelBuilder.Entity<Expense>()
         //   .HasMany(b => b.Bills)
          //  .WithOne();
            //.HasForeignKey(p=>p.ExpenseId);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Bill> Bills { get; set; }
}

