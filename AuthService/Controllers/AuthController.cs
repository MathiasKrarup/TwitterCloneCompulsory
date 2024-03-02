using Microsoft.AspNetCore.Mvc;
using TwitterCloneCompulsory.Interfaces;

namespace TwitterCloneCompulsory.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationRepo _authRepo;

    public AuthController(IAuthenticationRepo authRepo)
    {
        _authRepo = authRepo;
    }
}