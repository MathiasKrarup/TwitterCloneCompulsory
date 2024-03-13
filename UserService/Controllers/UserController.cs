﻿using System.Security.Claims;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserApplication.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserCrud _userCrud;

    public UserController(IUserCrud userCrud)
    {
        _userCrud = userCrud;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userCrud.GetUserAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdUser = await _userCrud.AddUserAsync(userDto);

        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (!int.TryParse(userIdClaim, out var userIdFromToken))
        {
            return Unauthorized("invalid token");
        }

        if (id != userIdFromToken)
        {
            return Forbid("You do not have permission to update this user");
        }

        var canUpdate = await _userCrud.CanUserUpdateAsync(userIdFromToken);
        if (!canUpdate)
        {
            return Forbid("The token is not active");
        }

        await _userCrud.UpdateUserAsync(updateUserDto);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (!int.TryParse(userIdClaim, out var userIdFromToken))
        {
            return Unauthorized("Invalid token");
        }

        var success = await _userCrud.DeleteUserAsync(id, userIdFromToken);

        if (!success)
        {
            return Forbid("You do not have permission to delete this user or the token is not active");
        }

        return NoContent();
    }

    [HttpGet]
    [Route("rebuild")]
    public IActionResult Rebuild()
    {
        _userCrud.Rebuild();
        return Ok();
    }

    [HttpGet("getByEmail/{email}")]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
        var user = await _userCrud.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
}