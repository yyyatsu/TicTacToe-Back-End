using System.ComponentModel.DataAnnotations;

namespace TTT.Data.Entities
{
  public class User : IEntity
  {
    public static object Identity { get; set; }

    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public Statistics Statistics { get; set; }
  }
}
