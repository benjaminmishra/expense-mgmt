using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web.Models;

public class BillViewModel
{
    public int Id { get; set; }
    public int ExpenseId { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [DisplayName("Serial")]
    public string Serial { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [DisplayName("Amount")]
    [Range(1,4000)]
    public int Amount { get; set; }
    public DateTime UploadedOn { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [DisplayName("Incurred On")]
    public DateTime IncurredOn { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [DisplayName("Reason")]
    public string Reason { get; set; }
}
