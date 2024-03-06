using Domain;
using Microsoft.EntityFrameworkCore;
using TwitterCloneCompulsory.Business_Entities;
using TwitterCloneCompulsory.Interfaces;
using TwitterCloneCompulsory.Models;

namespace TwitterCloneCompulsory.Repo;

/// <summary>
/// Class containing operations related to authentication.
/// </summary>
public class AuthRepo : IAuthRepo
{
    private readonly AuthenticationContext _context;

    public AuthRepo(AuthenticationContext context)
    {
        _context = context;
    }

    

    public async Task<Login> GetUsersByUsernameAsync(string username)
    {
        try
        {
            return await _context.Logins.FirstOrDefaultAsync(u => u.UserName == username)
                   ?? throw new Exception("The user was not found");
        }
        catch (Exception exception)
        {
            throw new Exception("There was an error getting the user by their username: " + exception.Message);
        }
    }

    public async Task<Login> RegisterUserAsync(Login newLogin)
    {
        await _context.Logins.AddAsync(newLogin);
        await _context.SaveChangesAsync();
        return newLogin;
    }

    public async Task<Login> GetUsersByUserId(int userId)
    {
        var login = await _context.Logins.FirstOrDefaultAsync(l => l.UserId == userId);
        return login ?? throw new KeyNotFoundException("No login found for the provided user ID.");
    }

    public void Rebuild()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
    
}
