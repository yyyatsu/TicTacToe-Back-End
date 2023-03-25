using System.ComponentModel.DataAnnotations;

namespace TTT.Domain.Models
{
  public class RegistrationModel
  {
    public string Name { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password incorrect")]
    public string ConfirmPassword { get; set; }
  }
}
