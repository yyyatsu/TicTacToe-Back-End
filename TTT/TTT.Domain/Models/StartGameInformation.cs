namespace TTT.Domain.Models
{
  public class StartGameInformation
  {
    public bool Turn { get; set; }
    public string Symbol { get; set; }

    public string OpponentName { get; set; }

    public StartGameInformation(bool turn, string symbol, string name)
    {
      Turn = turn;
      Symbol = symbol;
      OpponentName = name;
    }
  }
}
