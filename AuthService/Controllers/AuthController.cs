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
    public async Task<IActionResult> Register([FromBody] ExtendedLoginDto extendedLoginDto)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var userServiceUrl = "http://userservice:80";

        var userDtoContent = new StringContent(JsonConvert.SerializeObject(new UserDto
        {
            Email = extendedLoginDto.Email,
            Firstname = extendedLoginDto.Firstname,
            Lastname = extendedLoginDto.Lastname,
            Age = extendedLoginDto.Age
        }), Encoding.UTF8, "application/json");

        var createUserResponse = await httpClient.PostAsync($"{userServiceUrl}/User", userDtoContent);
        if (!createUserResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)createUserResponse.StatusCode, "Failed to create user in UserService.");
        }

        var createdUserContent = await createUserResponse.Content.ReadAsStringAsync();
        var createdUser = JsonConvert.DeserializeObject<User>(createdUserContent);
        var userId = createdUser.Id;


        var passwordHasher = new PasswordHasher<Login>();
        var hashedPassword = passwordHasher.HashPassword(null, extendedLoginDto.Password);

        var login = new Login
        {
            UserId = userId,
            UserName = extendedLoginDto.Username,
            PasswordHash = hashedPassword
        };

        await _authRepo.RegisterUserAsync(login);

        return Ok(new { Message = "User registered successfully." });

    }
}
