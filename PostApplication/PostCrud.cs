using AutoMapper;
using Domain;
using Domain.DTOs;
using PostApplication.Interfaces;
using PostInfrastructure;
using PostInfrastructure.Interfaces;

namespace PostApplication;

public class PostCrud : IPostCrud
{
    private readonly IPostRepository _postRepo;
    private readonly IMapper _mapper;

    public PostCrud(IPostRepository postRepo, IMapper mapper)
    {
        _postRepo = postRepo;
        _mapper = mapper;
    }

    public async Task<int> AddPostAsync(PostDto postDto, int userId)
    {
        var post = _mapper.Map<Post>(postDto);
        post.UserId = userId; 
        return await _postRepo.CreatePostAsync(post);
    }

    public async Task<Post> GetPostAsync(int postId)
    {
        return await _postRepo.GetPostAsync(postId);
    }

    public async Task UpdatePostAsync(int postId, PostDto postDto, int userId)
    {
        var post = await _postRepo.GetPostAsync(postId);

        if (post == null || post.UserId != userId)
        {
            throw new KeyNotFoundException("The Post was not found or you do not have access to update this post");
        }

        _mapper.Map(postDto, post);

        await _postRepo.UpdatePostAsync(post);
    }

    public async Task DeletePostAsync(int postId)
    {
        await _postRepo.DeletePostAsync(postId);
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _postRepo.GetPostsAsync();
    }

    public void Rebuild()
    {
        _postRepo.Rebuild();
    }
}