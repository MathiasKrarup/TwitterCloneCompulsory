using Domain;
using Microsoft.EntityFrameworkCore;
using TwitterCloneCompulsory.Interfaces;
using TwitterCloneCompulsory.Models;

namespace TwitterCloneCompulsory.Repo;

public class AuthRepo : IAuthRepo
{
    private readonly AuthenticationContext _context;

    public AuthRepo(AuthenticationContext context)
    {
        _context = context;
    }


    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ValidateUserCredentialsAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}
