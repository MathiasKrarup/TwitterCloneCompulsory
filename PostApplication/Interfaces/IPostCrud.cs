using Domain.DTOs;
using Domain;

namespace PostApplication.Interfaces;

public interface IPostCrud
{
    Task<int> AddPostAsync(PostDto postDto);
    Task<Post> GetPostAsync(int postId);
    Task UpdatePostAsync(int postId, PostDto postDto);
    Task DeletePostAsync(int postId);
    Task<IEnumerable<Post>> GetPostsAsync();
    void Rebuild();
}