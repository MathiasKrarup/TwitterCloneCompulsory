using PostService.Models;
using Domain.DTOs;

namespace PostApplication.Interfaces;

public interface IPostCrud
{
    Task<int> AddPostAsync(PostDto postDto);
    Task<Post> GetPostAsync(int postId);
    Task UpdatePostAsync(PostDto postDto);
    Task DeletePostAsync(int postId);
}