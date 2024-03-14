using Domain;
using Microsoft.EntityFrameworkCore;

namespace PostInfrastructure;

public class PostDBContext : DbContext
{
    public PostDBContext(DbContextOptions<PostDBContext> options) : base(options) { }
    
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1440;Database=PostDB;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        {
            modelBuilder.Entity<Post>()
                .Property(p => p.PostId)
                .ValueGeneratedOnAdd();
        }
    }
}