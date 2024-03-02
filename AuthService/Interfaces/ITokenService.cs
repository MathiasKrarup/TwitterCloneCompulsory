using Domain;

namespace TwitterCloneCompulsory.Interfaces;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(User user);
    Task<bool> ValidateTokenAsync(string token);
}
