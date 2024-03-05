using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;
using TwitterCloneCompulsory.Business_Entities;
using TwitterCloneCompulsory.Interfaces;

namespace TwitterCloneCompulsory.Models;

public class ValidationService : IValidationService
{
    private readonly string _jwtKey;
    private readonly IAuthRepo _authRepo;
    private readonly IConfiguration _configuration;

    public ValidationService(IAuthRepo authRepo, IConfiguration configuration)
    {
        _authRepo = authRepo;
        _configuration = configuration;
        _jwtKey = configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(_jwtKey))
        {
            throw new ArgumentException("JWT Key is not configured properly.");
        }
    }


    public async Task<bool> ValidateUserByCredentialsAsync(string username, string password)
    {
        var login = await _authRepo.GetUsersByUsernameAsync(username);

        if (login != null)
        {
            // Finde en løsning til password verification logik
            return login.Password == password;
        }

        return false;
    }

    public async Task<bool> ValidateUserByTokenAsync(string token)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateTokenForLoginAsync(Login login)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, login.AuthUser.Username),
                new Claim("UserId", login.AuthUserId.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2), 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }

    public void Rebuild()
    {
        _authRepo.Rebuild();
    }
    
}
