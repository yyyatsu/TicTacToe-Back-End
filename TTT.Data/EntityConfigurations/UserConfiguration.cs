using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TTT.Data.Entities;

namespace TTT.Data.EntityConfigurations
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable("Users");
      builder.HasKey(user => user.Id);

      builder.HasOne(user => user.Statistics)
             .WithOne(statistics => statistics.User)
             .OnDelete(DeleteBehavior.Cascade);

      builder.Property(user => user.Id).ValueGeneratedOnAdd();
      builder.Property(user => user.Password).IsRequired().HasMaxLength(50);
    }
  }
}
