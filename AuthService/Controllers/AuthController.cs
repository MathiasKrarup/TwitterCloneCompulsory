using Microsoft.AspNetCore.Mvc;
using TwitterCloneCompulsory.Interfaces;

namespace TwitterCloneCompulsory.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
}
