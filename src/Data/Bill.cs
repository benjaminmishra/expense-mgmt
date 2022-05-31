using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpenseMgmt.Data;


public class Bill
{
    public int Id { get; set; }
    public string Serial { get; set; }
    public int Amount { get; set; }
    public DateTime UploadedOn { get; set; }
    public DateTime IncurredOn { get; set; }
    public string Reason { get; set; }

    public int ExpenseId { get; set; }
    public Expense Expense { get; set; }
}

