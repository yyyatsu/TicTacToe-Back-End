using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TTT.Data.Entities;

namespace TTT.Data.EntityConfigurations
{
  public class RoomConfiguration : IEntityTypeConfiguration<Room>
  {
    public void Configure(EntityTypeBuilder<Room> builder)
    {
      builder.ToTable("Rooms");
      builder.HasKey(room => room.Name);
    }
  }
}
