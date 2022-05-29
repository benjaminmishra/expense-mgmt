using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMgtm.Data;

public class ExpenseMgtmContext: DbContext
{

    public ExpenseMgtmContext(DbContextOptions<ExpenseMgtmContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
}

