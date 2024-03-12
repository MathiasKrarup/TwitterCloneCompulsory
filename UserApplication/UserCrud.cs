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

    public async Task<User> AddUserAsync(UserDto userDto)
    {
        var user = new User
        {
            Email = userDto.Email,
            Firstname = userDto.Firstname,
            Lastname = userDto.Lastname,
            Age = userDto.Age,
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
        user.Firstname = updateUserDto.Firstname;
        user.Lastname = updateUserDto.Lastname;
        user.Age = updateUserDto.Age;
        

        await _userRepo.UpdateUserAsync(user);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _userRepo.GetUserByEmailAsync(email);
    }
}