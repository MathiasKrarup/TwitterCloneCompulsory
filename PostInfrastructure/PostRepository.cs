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

    public async Task UpdatePostAsync(int postId, Post UpdatePost)
    {

        var postToUpdate = await _context.Posts.FirstOrDefaultAsync(p=> p.PostId == UpdatePost.PostId);

        if(postId != UpdatePost.PostId)
        {
            throw new ArgumentException("The ids do not match");
        }

        postToUpdate.Content = UpdatePost.Content;
        _context.Posts.Update(postToUpdate);
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