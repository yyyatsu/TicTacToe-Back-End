using TTT.Data.Entities;
using TTT.Data.Enums;

namespace TTT.Domain.Services.Interfaces
{
  public interface IStatisticsService
  {
    Task<Statistics?> GetStatisticsAsync(string name);
    Task<Statistics?> UpdateStatisticsAsync(string name, GameResult gameResult);
  }
}
