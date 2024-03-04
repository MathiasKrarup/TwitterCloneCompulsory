using Domain;
using Microsoft.EntityFrameworkCore;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Models;

public class AuthenticationContext : DbContext
{
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) { }
    
    public DbSet<AuthUser> AuthUsers { get; set; } 
    public DbSet<Login> Logins { get; set; }
    public DbSet<Token> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=Auth-db,1443;Database=Auth;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).IsRequired();
        });
        
        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LoginId);
            entity.Property(e => e.LoginId).ValueGeneratedNever();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.TokenExpiryTime).IsRequired();
            entity.HasOne(e => e.AuthUser)
                .WithOne() 
                .HasForeignKey<Login>(e => e.AuthUserId);
        });
        
        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.TokenId);
            entity.Property(e => e.TokenExpiryTime).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
            entity.HasOne(e => e.AuthUser)
                .WithMany(u => u.Tokens)
                .HasForeignKey(e => e.AuthUserId);
        });
    }

    
}