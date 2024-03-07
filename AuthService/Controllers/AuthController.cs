using System.Text.Json.Serialization;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using TwitterCloneCompulsory.Business_Entities;
using TwitterCloneCompulsory.Interfaces;

namespace TwitterCloneCompulsory.Controllers;


[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IValidationService _validationService;
    private readonly IAuthRepo _authRepo;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _userServiceUrl;

    public AuthController(IValidationService validationService, IAuthRepo authRepo, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _validationService = validationService;
        _authRepo = authRepo;
        _httpClientFactory = httpClientFactory;
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

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var userServiceUrl = "http://localhost:8090/User";

        var response = await httpClient.GetAsync($"{userServiceUrl}/getByUsername/{registerDto.Username}");

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("User does not exist");
        }

        
        var userContent = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<User>(userContent);

        var existingLogin = await _authRepo.GetUsersByUsernameAsync(registerDto.Username);
        if (existingLogin != null)
        {
            return BadRequest("A login for this user already exists");
        }

        var passwordHasher = new PasswordHasher<Login>();
        var hashedPassword = passwordHasher.HashPassword(null, registerDto.Password);

        var login = new Login
        {
            UserId = user.Id,
            UserName = registerDto.Username,
            PasswordHash = hashedPassword
        };

        await _authRepo.RegisterUserAsync(login);
        
        return Ok(new { message = "User registered successfully." });
    }
}
