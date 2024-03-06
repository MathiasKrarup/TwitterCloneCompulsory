using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TwitterCloneCompulsory.Business_Entities;
using TwitterCloneCompulsory.Interfaces;

namespace TwitterCloneCompulsory.Controllers;


[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IValidationService _validationService;
    private readonly IAuthRepo _authRepo;

    public AuthController(IValidationService validationService, IAuthRepo authRepo)
    {
        _validationService = validationService;
        _authRepo = authRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var isValidUser = await _validationService.ValidateUserByCredentialsAsync(loginDto.Username, loginDto.Password);
        if (!isValidUser)
        {
            return Unauthorized("Invalid credentials.");
        }

        var login = await _authRepo.GetUsersByUsernameAsync(loginDto.Username);
        var token = await _validationService.GenerateTokenForLoginAsync(login);

        return Ok(new { Token = token });
    }

    [HttpGet]
    [Route("rebuild")]
    public IActionResult Rebuild()
    {
            _authRepo.Rebuild();
            return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingLogin = await _authRepo.GetUsersByUserId(registerDto.UserId);
        if (existingLogin != null)
        {
            return BadRequest("a login for this user already exists.");
        }

        var passwordHasher = new PasswordHasher<Login>();
        var hashedPassword = passwordHasher.HashPassword(null, registerDto.Password);

        var login = new Login
        {
            UserId = registerDto.UserId,
            UserName = registerDto.Username,
            PasswordHash = hashedPassword
        };

        await _authRepo.RegisterUserAsync(login);

        return Ok(new { message = "User registered succesfully" });
    }
}
