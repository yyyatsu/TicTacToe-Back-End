using Microsoft.EntityFrameworkCore;
using TTT.Data.Entities;
using TTT.Data.Enums;
using TTT.Data.Repository;
using TTT.Domain.Services.Interfaces;

namespace TTT.Domain.Services.Implementations
{
  public class StatisticsService : IStatisticsService
  {
    private readonly IRepository<Statistics> statisticsRepository;

    public StatisticsService(IRepository<Statistics> statisticsRepository)
    {
      this.statisticsRepository = statisticsRepository;
    }

    public async Task<Statistics?> GetStatisticsAsync(string name)
    {
      Statistics? statistics =
        await statisticsRepository.GetAll().FirstOrDefaultAsync(s => s.User.Name == name);

      return statistics;
    }

    public async Task<Statistics?> UpdateStatisticsAsync(string name, GameResult gameResult)
    {
      Statistics? statistics = await GetStatisticsAsync(name);
      statistics?.UpdateStatistics(gameResult);

      var result = await statisticsRepository.UpdateAsync(statistics);
      await statisticsRepository.SaveChangesAsync();

      return result;
    }
  }
}

