using Microsoft.EntityFrameworkCore;
using PostInfrastructure.Interfaces;
using Domain;
using System.Collections;
using System.Xml.Linq;

namespace PostInfrastructure;

public class PostRepository : IPostRepository
{
    private readonly PostDBContext _context;

    public PostRepository(PostDBContext context)
    {
        _context = context;
    }
    
    public async Task<int> CreatePostAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post.PostId;
    }

    public async Task<Post> GetPostAsync(int postId)
    {
        return await _context.Posts.FindAsync(postId);
    }

    public async Task UpdatePostAsync(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePostAsync(int postId)
    {
        var post = await GetPostAsync(postId);
        if (post != null)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public void Rebuild()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

}