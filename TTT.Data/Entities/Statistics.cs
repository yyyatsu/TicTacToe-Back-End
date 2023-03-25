using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TTT.Data.Enums;
using System.Text.Json.Serialization;

namespace TTT.Data.Entities
{
  public class Statistics : IEntity
  {
    [Key]
    [JsonIgnore]
    public int Id { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    public int CountOfWins { get; set; }

    public int CountOfLoses { get; set; }

    public int CountOfDraws { get; set; }

    [NotMapped]
    public int CountOfGames
    {
      get
      {
        return CountOfWins + CountOfDraws + CountOfLoses;
      }
    }

    [NotMapped]
    public decimal PercentOfWins
    {
      get
      {
        if (CountOfWins == 0)
        {
          return 0;
        }
        return ((decimal)CountOfWins / (decimal)CountOfGames) * 100m;
      }
    }

    public void UpdateStatistics(GameResult gameResult)
    {
      switch (gameResult)
      {
        case GameResult.Win:
        {
          CountOfWins++;
          break;
        }

        case GameResult.Draw:
        {
          CountOfDraws++;
          break;
        }

        case GameResult.Lose:
        {
          CountOfLoses++;
          break;
        }
      }
    }
  }
}
