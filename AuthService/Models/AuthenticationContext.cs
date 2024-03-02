using Microsoft.EntityFrameworkCore;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Models;

public class AuthenticationContext : DbContext
{
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) { }
    
    public DbSet<Login> Logins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Token).IsRequired();
            entity.Property(e => e.TokenExpiryTime).IsRequired();

            entity.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Login>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}