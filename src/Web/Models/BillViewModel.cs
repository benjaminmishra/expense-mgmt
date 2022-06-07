using System;

namespace Web.Models;

public class BillViewModel
{
    public int Id { get; set; }
    public string Serial { get; set; }
    public int Amount { get; set; }
    public DateTime UploadedOn { get; set; }
    public DateTime IncurredOn { get; set; }
    public string Reason { get; set; }
}
