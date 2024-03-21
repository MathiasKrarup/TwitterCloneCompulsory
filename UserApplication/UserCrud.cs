using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using UserApplication.Interfaces;
using UserInfrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace UserApplication;

public class UserCrud : IUserCrud
{
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _authServiceUrl;
    private readonly string _postServiceUrl;
    private readonly string _timelineServiceUrl;
    private readonly IHttpContextAccessor _httpContextAccessor;



    public UserCrud(IUserRepo userRepo, IMapper mapper, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _userRepo = userRepo;
        _mapper = mapper;
        _authServiceUrl = "http://authservice:80/Auth";
        _postServiceUrl = "http://postservice:80/Post";
        _timelineServiceUrl = "http://timelineservice:80/Timeline";
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    private async Task<bool> DeleteLoginHttpRequest(int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.DeleteAsync($"{_authServiceUrl}/deleteLogin/{userId}");
        return response.IsSuccessStatusCode;
    }

    private async Task<bool> DeletePostHttpRequest(int postId, int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.DeleteAsync($"{_postServiceUrl}/{postId}");
        return response.IsSuccessStatusCode;
    }


    private async Task<List<int>> GetUserPostsHttpRequest(int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync($"{_postServiceUrl}/GetByUserId/{userId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to retrieve posts for the user");
        }

        var content = await response.Content.ReadAsStringAsync();
        var postIds = JsonConvert.DeserializeObject<List<int>>(content);
        return postIds;
    }

    private async Task<bool> RemovePostFromTimelineHttpRequest(int postId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.DeleteAsync($"{_timelineServiceUrl}/{postId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<User> AddUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        return await _userRepo.CreateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int userId, int requesterUserId)
    {
        var user = await _userRepo.GetUserAsync(userId);
        if (user == null || userId != requesterUserId)
        {
            return false;
        }

        var userPostsIds = await GetUserPostsHttpRequest(userId);

        foreach (var postId in userPostsIds)
        {
            var removedFromTimeline = await RemovePostFromTimelineHttpRequest(postId);
            if (!removedFromTimeline)
            {
                return false;
            }

            var deletedPost = await DeletePostHttpRequest(postId, userId);
            if (!deletedPost)
            {
                return false;
            }
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



    public async Task<bool> CheckIfUserExistsAsync(int userId)
    {
        return await _userRepo.GetUserAsync(userId) != null;
    }


}