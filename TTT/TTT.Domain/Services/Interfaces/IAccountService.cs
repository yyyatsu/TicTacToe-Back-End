using TTT.Data.Entities;
using TTT.Domain.Models;

namespace TTT.Domain.Services.Interfaces
{
  public interface IAccountService
  {
    Task<User?> RegisterAsync(RegistrationModel registrationModel);
    Task<User?> LoginAsync(LoginModel loginModel);
  }
}
