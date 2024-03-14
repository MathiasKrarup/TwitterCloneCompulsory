﻿using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using UserApplication.Interfaces;
using UserInfrastructure.Interfaces;

namespace UserApplication;

public class UserCrud : IUserCrud
{
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _authServiceUrl;


    public UserCrud(IUserRepo userRepo, IMapper mapper, IHttpClientFactory httpClientFactory)
    {
        _userRepo = userRepo;
        _mapper = mapper;
        _authServiceUrl = "http://authservice:80/Auth";
        _httpClientFactory = httpClientFactory;
    }

    public async Task<User> AddUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        return await _userRepo.CreateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int userId, int requesterUserId)
    {
        var user = await _userRepo.GetUserAsync(userId);
        if (user == null)
        {
            return false;
        }

        var successLoginDeletion = await DeleteLoginHttpRequest(userId);
        if (!successLoginDeletion)
        {
            return false;
        }

        await _userRepo.DeleteUserAsync(userId);
        return true;
    }


    public async Task<User> GetUserAsync(int userId)
    {
        return await _userRepo.GetUserAsync(userId);
    }

    public void Rebuild()
    {
        _userRepo.Rebuild();
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
       var user = await _userRepo.GetUserAsync(id);
        if (user == null) 
        {
            throw new KeyNotFoundException("The user was not found");
        }
        _mapper.Map(updateUserDto, user);
        await _userRepo.UpdateUserAsync(user);
        
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _userRepo.GetUserByEmailAsync(email);
    }



    private async Task<bool> DeleteLoginHttpRequest(int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.DeleteAsync($"{_authServiceUrl}/deleteLogin/{userId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CheckIfUserExistsAsync(int userId)
    {
        return await _userRepo.GetUserAsync(userId) != null;
    }


}