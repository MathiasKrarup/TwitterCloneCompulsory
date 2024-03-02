using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Interfaces;

public interface IAuthenticationRepo
{
    /// <summary>
    /// Gets the user by their token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Login GetUserByToken(string token);
    
    /// <summary>
    /// Gets the user by their username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Login GetUserByUsername(string username);
    
}