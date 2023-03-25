using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TTT.Data.Entities;

namespace TTT.Data.EntityConfigurations
{
  public class StatisticsConfiguration : IEntityTypeConfiguration<Statistics>
  {
    public void Configure(EntityTypeBuilder<Statistics> builder)
    {
      builder.ToTable("Statistics");
      builder.HasKey(statistics => statistics.Id);

      builder.HasOne(statistics => statistics.User)
             .WithOne(user => user.Statistics)
             .HasForeignKey<Statistics>(statistics => statistics.Id)
             .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
