using Domain;
using Domain.DTOs;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Interfaces;

public interface IValidationService
{
    Task<bool> ValidateUserByCredentialsAsync(string username, string password);
    Task<string> GenerateTokenForLoginAsync(LoginDto loginDto);
    Task<bool> RegisterAsync(ExtendedLoginDto extendedLoginDto);
    Task<bool> DeleteLoginAsync(int userId);

}
