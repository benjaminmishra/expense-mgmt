using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMgtm.Data;

public class ExpenseMgtmContext: DbContext
{
    public DbSet<Employee> Employees { get; set; }
}

