using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostApplication;
using PostApplication.Interfaces;
using System.Security.Claims;

namespace PostService.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostCrud _postCrud;
    private readonly HttpClient _client = new HttpClient();

    public PostController(IPostCrud postCrud)
    {
        _postCrud = postCrud;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        try
        {
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
            return Ok(await _postCrud.GetPostAsync(postId));
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPost([FromBody] PostDto dto)
    {
       try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("invalid token");
            }

            await _postCrud.AddPostAsync(dto, userId);
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
    public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody]PostDto postDto)
    {
        try
        {
            var userIdClaim = User.FindFirstValue("UserId");
            if (!int.TryParse(userIdClaim, out var userIdFromToken))
            {
                return Unauthorized("UserId claim missing");
            }

            await _postCrud.UpdatePostAsync(postId, postDto, userIdFromToken);
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
    public async Task<IActionResult> DeletePost([FromRoute] int postId)
    {
        try
        {
            await _postCrud.DeletePostAsync(postId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}