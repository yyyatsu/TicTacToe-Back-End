using System.ComponentModel.DataAnnotations;

namespace TTT.Data.Entities
{
  public class Room : IEntity
  {
    [Key]
    public string Name { get; set; }

    public int BoardSize { get; set; }

    public bool IsFilled { get; set; }
  }
}
