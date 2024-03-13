using System.Text;
using System.Text.Json.Serialization;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using TwitterCloneCompulsory.Business_Entities;
using TwitterCloneCompulsory.Interfaces;
using TwitterCloneCompulsory.Repo;

namespace TwitterCloneCompulsory.Controllers;


[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IValidationService _validationService;
    private readonly IAuthRepo _authrepo;

    public AuthController(IValidationService validationService, IAuthRepo authrepo, IConfiguration configuration)
    {
        _validationService = validationService;
        _authrepo = authrepo;
    }


    [HttpGet]
    [Route("rebuild")]
    public IActionResult Rebuild()
    {
            _authrepo.Rebuild();
            return Ok();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] ExtendedLoginDto extendedLoginDto)
    {
        var success = await _validationService.RegisterAsync(extendedLoginDto);
        if (!success)
        {
            return BadRequest("Failed to register the user.");
        }

        return Ok(new { Message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _validationService.GenerateTokenForLoginAsync(loginDto);
            return Ok(new { Token = token });
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
        catch (UnauthorizedAccessException unauthorizedException)
        {
            return Unauthorized(unauthorizedException.Message);
        }
    }

    [HttpGet("{userId}/hasActiveToken")]
    public async Task<IActionResult> HasActiveToken(int userId)
    {
        var isActive = await _validationService.UserHasActiveTokenAsync(userId);
        return Ok(new { IsActive = isActive });
    }


}
