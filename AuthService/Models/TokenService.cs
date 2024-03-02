using Domain;
using TwitterCloneCompulsory.Interfaces;

namespace TwitterCloneCompulsory.Models;

public class TokenService : ITokenService
{
    private readonly IAuthRepo _authRepo;
    private readonly IConfiguration _configuration;

    public TokenService(IAuthRepo authRepo, IConfiguration configuration)
    {
        _authRepo = authRepo;
        _configuration = configuration;
    }


    public Task<string> GenerateTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ValidateTokenAsync(string token)
    {
        throw new NotImplementedException();
    }
}
