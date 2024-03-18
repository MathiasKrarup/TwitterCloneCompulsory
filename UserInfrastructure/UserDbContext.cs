using Domain;
using Microsoft.EntityFrameworkCore;

namespace UserInfrastructure;

public class UserDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    

    public UserDbContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1440;Database=User;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;"

);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }
    }
    
    
}