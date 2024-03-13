using Domain;
using Domain.DTOs;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Interfaces;

public interface IValidationService
{
    Task<bool> ValidateUserByCredentialsAsync(string username, string password);
    Task<bool> ValidateUserByTokenAsync(string token);
    Task<string> GenerateTokenForLoginAsync(LoginDto loginDto);
    Task<bool> RegisterAsync(ExtendedLoginDto extendedLoginDto);
    Task<bool> UserHasActiveTokenAsync(int userId);
    Task<bool> DeleteLoginAsync(int userId);
    Task<bool> DeleteTokensAsync(int userId);

}
