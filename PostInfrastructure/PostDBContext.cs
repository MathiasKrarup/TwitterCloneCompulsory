using Microsoft.EntityFrameworkCore;
using PostService.Models;

namespace PostInfrastructure;

public class PostDBContext : DbContext
{
    public PostDBContext(DbContextOptions<PostDBContext> options) : base(options) { }
    
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=Post-db;Database=Post;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");
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