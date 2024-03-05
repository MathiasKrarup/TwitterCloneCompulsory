using Domain;
using Domain.DTOs;
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

        int userId = await _userCrud.AddUserAsync(userDto);
        return CreatedAtAction(nameof(GetUser), new { id = userId }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        updateUserDto.Id = id;

        await _userCrud.UpdateUserAsync(updateUserDto);

        return NoContent();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userCrud.GetUserAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userCrud.DeleteUserAsync(id);

        return NoContent();
    }
}