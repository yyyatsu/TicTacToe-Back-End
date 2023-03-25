using Microsoft.EntityFrameworkCore;
using TTT.Data.Entities;
using TTT.Data.EntityConfigurations;

namespace TTT.Data.Context
{
  public class TTTContext : DbContext
  {

    public DbSet<Statistics> Statistics { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Room> Rooms { get; set; }

   // public DbSet<Photo> Photos { get; set; }


    public TTTContext(DbContextOptions<TTTContext> options)
    {
      Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new StatisticsConfiguration());
      modelBuilder.ApplyConfiguration(new RoomConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=TTTDb; Trusted_Connection=True");
    }
  }
}
