using Domain;
using Domain.DTOs;

namespace UserApplication.Interfaces;

public interface IUserCrud
{
    Task<User> AddUserAsync(UserDto userDto);
    Task DeleteUserAsync(int userId);
    Task<User> GetUserAsync(int userId);
    Task UpdateUserAsync(UpdateUserDto updateUserDto);
    Task<User> GetUserByEmail(string Email);
    public void Rebuild();
}