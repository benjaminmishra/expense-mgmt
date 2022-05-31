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
}

