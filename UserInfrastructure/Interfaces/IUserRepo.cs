using Domain;

namespace UserInfrastructure.Interfaces;

public interface IUserRepo
{
    /// <summary>
    /// Creates an user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<int> CreateUserAsync(User user);
    /// <summary>
    /// Gets an user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<User> GetUserAsync(int userId);
    /// <summary>
    /// Updates an user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task UpdateUserAsync(User user);
    /// <summary>
    /// Deletes an user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task DeleteUserAsync(int userId);
}