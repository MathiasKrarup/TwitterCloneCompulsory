using Domain;
using System.Collections;

namespace PostInfrastructure.Interfaces;

public interface IPostRepository
{
    /// <summary>
    /// Creates a Post
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    Task<int> CreatePostAsync(Post post);

    /// <summary>
    /// Gets a Post with Id
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<Post> GetPostAsync(int postId);

    /// <summary>
    /// Gets a list of posts
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Post>> GetPostsAsync();

    /// <summary>
    /// Updates a Post
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    Task UpdatePostAsync(Post post);

    /// <summary>
    /// Deletes a Post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task DeletePostAsync(int postId);

    void Rebuild();
}