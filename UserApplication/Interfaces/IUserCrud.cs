using Domain;
using Domain.DTOs;

namespace UserApplication.Interfaces;

public interface IUserCrud
{
    Task<User> AddUserAsync(UserDto userDto);
    Task<bool> DeleteUserAsync(int userId, int requesterUserId);
    Task<User> GetUserAsync(int userId);
    Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task<bool> CheckIfUserExistsAsync(int  userId);
    public void Rebuild();
}