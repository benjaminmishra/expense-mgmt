using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class User
{
    [Required(ErrorMessage = "This field is required")]
    [DisplayName("Employee Id")]
    public int EmployeeId { get; set; }


    [Required(ErrorMessage = "This field is required")]
    [DisplayName("Password")]
    public string Password { get; set; }
}
