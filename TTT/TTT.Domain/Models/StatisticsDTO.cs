using TTT.Data.Entities;

namespace TTT.Domain.Models
{
  public class StatisticsDTO
  {
    public string Name { get; set; }
    public int CountOfWins { get; set; }
    public int CountOfLoses { get; set; }
    public int CountOfDraws { get; set; }

    public decimal PercentOfWins { get; set; }

    public StatisticsDTO(Statistics statistics, string name)
    {
      Name = name;
      CountOfWins  = statistics.CountOfWins;
      CountOfLoses = statistics.CountOfLoses;
      CountOfDraws = statistics.CountOfDraws;
      PercentOfWins = Decimal.Round(statistics.PercentOfWins, 2);
    }
  }
}
