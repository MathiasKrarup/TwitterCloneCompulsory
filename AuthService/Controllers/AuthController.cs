using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
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

}
