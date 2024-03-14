using Domain.DTOs;
using Domain;

namespace PostApplication.Interfaces;

public interface IPostCrud
{
    Task<int> AddPostAsync(PostDto postDto, int userId);
    Task<Post> GetPostAsync(int postId);
    Task UpdatePostAsync(int postId, PostDto postDto, int userId);
    Task DeletePostAsync(int postId);
    Task<IEnumerable<Post>> GetPostsAsync();
    void Rebuild();
}