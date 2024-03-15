using Domain.DTOs;
using Domain;

namespace PostApplication.Interfaces;

public interface IPostCrud
{
    Task<int> AddPostAsync(PostDto postDto, int userId);
    Task<Post> GetPostAsync(int postId);
    Task UpdatePostAsync(int postId, PostDto postDto, int userId);
    Task<bool> DeletePostAsync(int postId, int userId);
    Task<IEnumerable<Post>> GetPostsAsync();
    void Rebuild();
}