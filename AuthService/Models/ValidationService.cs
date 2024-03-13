using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitterCloneCompulsory.Business_Entities;
using TwitterCloneCompulsory.Interfaces;

namespace TwitterCloneCompulsory.Models;

public class ValidationService : IValidationService
{
    private readonly string _jwtKey;
    private readonly IAuthRepo _authRepo;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;
    private readonly string _userServiceUrl;


    public ValidationService(IMapper mapper, IAuthRepo authRepo, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _authRepo = authRepo;
        _mapper = mapper;
        _userServiceUrl = "https://localhost:7057/User";
        _httpClient = httpClientFactory.CreateClient();
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
            var passwordHasher = new PasswordHasher<Login>();
            var verificationResult = passwordHasher.VerifyHashedPassword(login, login.PasswordHash, password);
            return verificationResult == PasswordVerificationResult.Success;
        }

        return false;
    }

    public async Task<bool> ValidateUserByTokenAsync(string token)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateTokenForLoginAsync(LoginDto loginDto)
    {
        var login = await _authRepo.GetUsersByUsernameAsync(loginDto.Username);

        if (login == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var passwordHasher = new PasswordHasher<Login>();
        var verifyPassword = passwordHasher.VerifyHashedPassword(login, login.PasswordHash, loginDto.Password);

        if (verifyPassword != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        var userExists = await VerifyUserExists(login.UserId);
            if (!userExists)
            {
            throw new KeyNotFoundException("User not found in UserService");
            }
        

        var jwtToken = GenerateJwtToken(login);

        var token = new Token
        {
            Value = jwtToken,
            TokenExpiryTime = DateTime.UtcNow.AddHours(3), 
            IsActive = true,
            UserId = login.UserId
        };

        await _authRepo.SaveTokenAsync(token);

        return jwtToken;
    }

    private async Task<bool> VerifyUserExists(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_userServiceUrl}/{userId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private string GenerateJwtToken(Login login)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim("UserId", login.UserId.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public void Rebuild()
    {
        _authRepo.Rebuild();
    }

    public async Task<bool> RegisterAsync(ExtendedLoginDto extendedLoginDto)
    {
        var userDto = _mapper.Map<UserDto>(extendedLoginDto);
        var userDtoContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json");
        var createUserResponse = await _httpClient.PostAsync($"{_userServiceUrl}", userDtoContent);

        if (!createUserResponse.IsSuccessStatusCode)
        {
            return false; 
        }

        var createdUserContent = await createUserResponse.Content.ReadAsStringAsync();
        var createdUser = JsonConvert.DeserializeObject<User>(createdUserContent);

        var login = _mapper.Map<Login>(extendedLoginDto);
        login.UserId = createdUser.Id;

        var passwordHasher = new PasswordHasher<Login>();
        login.PasswordHash = passwordHasher.HashPassword(login, extendedLoginDto.Password);

        await _authRepo.RegisterUserAsync(login);

        return true;
    }

    public async Task<bool> UserHasActiveTokenAsync(int userId)
    {
        return await _authRepo.IsTokenActiveAsync(userId);
    }

    public async Task<bool> DeleteLoginAsync(int userId)
    {
        try
        {
            await _authRepo.DeleteLoginAsync(userId);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteTokensAsync(int userId)
    {
        try
        {
            await _authRepo.DeleteTokenAsync(userId);
            return true;
        }
        catch
        {
           
            return false;
        }
    }
}
