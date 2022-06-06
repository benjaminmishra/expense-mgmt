using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpenseMgmt.Data;


public class Bill
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Serial { get; set; }
    public int Amount { get; set; }
    public DateTime UploadedOn { get; set; }
    public DateTime IncurredOn { get; set; }
    public string Reason { get; set; }

    public int ExpenseId { get; set; }
    [ForeignKey("ExpenseId")]
    public Expense Expense { get; set; }
}

