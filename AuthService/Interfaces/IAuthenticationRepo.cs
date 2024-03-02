using Domain;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Interfaces;

    public interface IAuthRepo
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
    }
