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

    public async Task<int> AddPostAsync(PostDto postDto)
    {
        var post = new Post {
            Content = postDto.Content,
            UserId = postDto.UserId,
        };
        return await _postRepo.CreatePostAsync(_mapper.Map<Post>(postDto));
    }

    public async Task<Post> GetPostAsync(int postId)
    {
        return await _postRepo.GetPostAsync(postId);
    }

    public async Task UpdatePostAsync(int postId, PostDto postDto)
    {
        var post = await _postRepo.GetPostAsync(postDto.PostId);

        if (post == null)
        {
            throw new KeyNotFoundException("The Post was not found");
        }

        post.Content = postDto.Content;
        post.UserId = postDto.UserId;

        await _postRepo.UpdatePostAsync(postId, _mapper.Map<Post>(postDto));
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