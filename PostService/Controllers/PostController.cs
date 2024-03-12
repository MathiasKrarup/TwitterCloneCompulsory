using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using PostApplication;
using PostApplication.Interfaces;

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
    public async Task<IActionResult> AddPost([FromBody] PostDto dto)
    {
        try
        {
            await _postCrud.AddPostAsync(dto);
            return StatusCode(201, "Post added successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{postId}")]
    public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody]PostDto postDto)
    {
        try
        {
            await _postCrud.UpdatePostAsync(postId, postDto);
            return Ok();
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