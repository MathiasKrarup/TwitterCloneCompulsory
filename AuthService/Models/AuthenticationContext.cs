using Domain;
using Microsoft.EntityFrameworkCore;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Models;

public class AuthenticationContext : DbContext
{
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) { }
    
    public DbSet<Login> Logins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1440;Database=AuthDB;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;"
);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UserName).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
        });
    }

    
}