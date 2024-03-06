using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using UserApplication.Interfaces;
using UserInfrastructure.Interfaces;

namespace UserApplication;

public class UserCrud : IUserCrud
{
    private readonly IUserRepo _userRepo;

    public UserCrud(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }
    
    public async Task<int> AddUserAsync(UserDto userDto)
    {
        var user = new User
        {
            Email = userDto.Email,
            Username = userDto.Username,
            DateCreated = DateTime.UtcNow,
        };
        return await _userRepo.CreateUserAsync(user);
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _userRepo.DeleteUserAsync(userId);
    }

    public async Task<User> GetUserAsync(int userId)
    {
        return await _userRepo.GetUserAsync(userId);
    }

    public void Rebuild()
    {
        _userRepo.Rebuild();
    }

    public async Task UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        var user = await _userRepo.GetUserAsync(updateUserDto.Id);

        if (user == null)
        {
            throw new KeyNotFoundException("The user was not found");
        }

        user.Email = updateUserDto.Email;
        user.Username = updateUserDto.Username;
        

        await _userRepo.UpdateUserAsync(user);
    }
}