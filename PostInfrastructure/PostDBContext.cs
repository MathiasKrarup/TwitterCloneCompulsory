using Domain;
using Microsoft.EntityFrameworkCore;

namespace PostInfrastructure;

public class PostDBContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    public PostDBContext()
    {

    }    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=Post-db,1436;Database=Post;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");
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