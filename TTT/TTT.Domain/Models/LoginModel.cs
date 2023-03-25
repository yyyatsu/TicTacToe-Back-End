using System.ComponentModel.DataAnnotations;

namespace TTT.Domain.Models
{
  public class LoginModel
  {
    [Required(ErrorMessage = "No Email")]
    public string Name { get; set; }

    [Required(ErrorMessage = "No password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}
