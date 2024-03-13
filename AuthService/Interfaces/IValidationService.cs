using Domain;
using Domain.DTOs;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Interfaces;

public interface IValidationService
{
    Task<bool> ValidateUserByCredentialsAsync(string username, string password);
    Task<bool> ValidateUserByTokenAsync(string token);
    Task<string> GenerateTokenForLoginAsync(Login login);
    Task<bool> RegisterAsync(ExtendedLoginDto extendedLoginDto);

}
