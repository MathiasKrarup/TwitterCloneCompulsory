using PostService.Models;
using Domain.DTOs;
using PostApplication.Interfaces;
using PostInfrastructure;

namespace PostApplication;

public class PostCrud : IPostCrud
{
    private readonly PostRepository _postRepo;

    public PostCrud(PostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task<int> AddPostAsync(PostDto postDto)
    {
        var post = new Post {
            Content = postDto.Content,
            UserId = postDto.UserId,
        };
        return await _postRepo.CreatePostAsync(post);
    }

    public async Task<Post> GetPostAsync(int postId)
    {
        return await _postRepo.GetPostAsync(postId);
    }

    public async Task UpdatePostAsync(PostDto postDto)
    {
        var post = await _postRepo.GetPostAsync(postDto.PostId);

        if (post == null)
        {
            throw new KeyNotFoundException("The Post was not found");
        }

        post.Content = postDto.Content;
        post.UserId = postDto.UserId;

        await _postRepo.UpdatePostAsync(post);
    }

    public async Task DeletePostAsync(int postId)
    {
        await _postRepo.DeletePostAsync(postId);
    }
}