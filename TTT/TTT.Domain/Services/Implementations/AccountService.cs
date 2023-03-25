using Microsoft.EntityFrameworkCore;
using TTT.Data.Context;
using TTT.Data.Entities;
using TTT.Data.Repository;
using TTT.Domain.Models;
using TTT.Domain.Services.Interfaces;

namespace TTT.Domain.Services.Implementations
{
  public class AccountService : IAccountService
  {
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Statistics> statisticsRepository;


    public AccountService(IRepository<User> userRepository, IRepository<Statistics> statisticsRepository)
    {
      this.userRepository = userRepository;
      this.statisticsRepository=statisticsRepository;
    }

    public async Task<User?> RegisterAsync(RegistrationModel registrationModel)
	{
	  User user = new()
	  {
		Name = registrationModel.Name,
		Password = registrationModel.Password,
	  };

	  Statistics statistics = new()
	  {
		User = user,
	  };

	  await statisticsRepository.AddAsync(statistics);
	  await statisticsRepository.SaveChangesAsync();

	  return await userRepository.GetAll().FirstOrDefaultAsync(us => us.Name == user.Name);
	}

    public async Task<User?> LoginAsync(LoginModel loginModel)
    {
	  User? user = await userRepository.GetAll().FirstOrDefaultAsync(user => user.Name == loginModel.Name 
	                                                      && user.Password == loginModel.Password);
	  return user;
    }
  }
}
