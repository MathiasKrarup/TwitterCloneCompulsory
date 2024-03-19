using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostApplication.Interfaces;
using SharedMessage;

namespace PostService.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostCrud _postCrud;
    private readonly MessageClient _messageClient;

    public PostController(IPostCrud postCrud, MessageClient messageClient)
    {
        _postCrud = postCrud;
        _messageClient = messageClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        try
        {
            _messageClient.Send(new PostMessage { Message = "Got All Posts!" },"post-message");
            return Ok(await _postCrud.GetPostsAsync());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{postId}")]
    public async Task<IActionResult> GetPostById([FromRoute] int postId)
    {
        try
        {
            var post = await _postCrud.GetPostAsync(postId);
            if (post == null)
            {
                return NotFound($"Post with ID {postId} was not found.");
            }
            _messageClient.Send(new PostMessage { Message = $"Got Post with ID: {postId}!" }, "post-message");
            return Ok(post);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPost([FromBody] PostDto dto)
    {
        try
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("invalid token");
            }

            await _postCrud.AddPostAsync(dto, userId);
            _messageClient.Send(new PostMessage { Message = "New Post was Created!" }, "post-message");
            return StatusCode(201, "Post added succesfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{postId}")]
    [Authorize]
    public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody] PostDto postDto)
    {
        try
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!int.TryParse(userIdClaim, out var userIdFromToken))
            {
                return Unauthorized("UserId claim missing");
            }

            await _postCrud.UpdatePostAsync(postId, postDto, userIdFromToken);
            _messageClient.Send(new PostMessage { Message = $"Post with ID: {postId} was Updated!" }, "post-message");
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{postId}")]
    [Authorize]
    public async Task<IActionResult> DeletePost([FromRoute] int postId)
    {
        try
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("invalid token");
            }

            var success = await _postCrud.DeletePostAsync(postId, userId);
            if (!success)
            {
                return Forbid("You do not have permission to delete this post.");
            }
            _messageClient.Send(new PostMessage { Message = $"Post with ID: {postId} was Deleted!" }, "post-message");
            return Ok();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}