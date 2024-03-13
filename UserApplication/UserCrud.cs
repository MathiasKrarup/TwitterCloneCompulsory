using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using UserApplication.Interfaces;
using UserInfrastructure.Interfaces;

namespace UserApplication;

public class UserCrud : IUserCrud
{
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;

    public UserCrud(IUserRepo userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public async Task<User> AddUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
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