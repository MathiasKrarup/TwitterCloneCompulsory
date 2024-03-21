using System.Net;
using System.Security.Claims;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserApplication.Interfaces;
using UserService.Controllers;

namespace UserService.Tests;

public class TestUserServiceMethods
{
    /// <summary>
    /// Test to ensure that the CreateUser method successfully creates a user
    /// </summary>
    [Fact]
    public async Task CreateUserTest()
    {
        // Arrange
        var mockUserCrud = new Mock<IUserCrud>();
        var userDto = new UserDto
        {
            Email = "test@test.com",
            Firstname = "TestFirstname",
            Lastname = "TestLastname",
            Age = 23
        };
        var createdUser = new User
        {
            Id = 1, 
            Email = userDto.Email,
            Firstname = userDto.Firstname,
            Lastname = userDto.Lastname,
            Age = userDto.Age,
            DateCreated = DateTime.UtcNow 
        };
        
        mockUserCrud.Setup(service => service.AddUserAsync(It.IsAny<UserDto>()))
            .ReturnsAsync(createdUser);
        
        var controller = new UserController(mockUserCrud.Object);

        // Act
        var result = await controller.CreateUser(userDto);
        
        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<User>(createdAtActionResult.Value);
        Assert.Equal(createdUser.Id, returnValue.Id);
        Assert.Equal(createdUser.Email, returnValue.Email);
        Assert.Equal(createdUser.Firstname, returnValue.Firstname);
        Assert.Equal(createdUser.Lastname, returnValue.Lastname);
        Assert.Equal(createdUser.Age, returnValue.Age);
        Assert.Equal(createdUser.DateCreated, returnValue.DateCreated);
    }
    
    /// <summary>
    /// Test to ensure that the GetUser method retrieves the correct user by Id
    /// </summary>
    [Fact]
    public async Task GetUser()
    {
        // Arrange
        var expectedUser = new User
        {
            Id = 1,
            Email = "test@test.com",
            Firstname = "TestFirstname",
            Lastname = "TestLastname",
            Age = 23,
            DateCreated = System.DateTime.UtcNow
        };

        var mockUserCrud = new Mock<IUserCrud>();
        mockUserCrud.Setup(x => x.GetUserAsync(expectedUser.Id)).ReturnsAsync(expectedUser);

        var controller = new UserController(mockUserCrud.Object);

        // Act
        var result = await controller.GetUser(expectedUser.Id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<User>>(result);
        var returnValue = actionResult.Value;

        Assert.NotNull(returnValue);
        Assert.Equal(expectedUser.Id, returnValue.Id);
        Assert.Equal(expectedUser.Email, returnValue.Email);
        Assert.Equal(expectedUser.Firstname, returnValue.Firstname);
        Assert.Equal(expectedUser.Lastname, returnValue.Lastname);
        Assert.Equal(expectedUser.Age, returnValue.Age);
        Assert.Equal(expectedUser.DateCreated, returnValue.DateCreated);
    }
    
    /// <summary>
    /// Test to ensure the UpdateUser method returns a NoContentResult when a user is succesfully updated
    /// In the test it is also assumed that the requester exists and is authorized.
    /// </summary>
   [Fact]
    public async Task UpdateUser()
    {
        // Arrange
        var userId = 1; 
        var mockUserCrud = new Mock<IUserCrud>();
        var updateUserDto = new UpdateUserDto
        {
            Email = "test@test.com",
            Firstname = "TestFirstname",
            Lastname = "TestLastname",
            Age = 23
        };
        
        mockUserCrud.Setup(x => x.CheckIfUserExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        mockUserCrud.Setup(x => x.UpdateUserAsync(It.IsAny<int>(), It.IsAny<UpdateUserDto>()))
            .Returns(Task.CompletedTask);


        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim("UserId", userId.ToString()), 
        }, "Bearer")); 

        var controller = new UserController(mockUserCrud.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = userClaims }
            }
        };

        // Act
        var result = await controller.UpdateUser(userId, updateUserDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
    
    /// <summary>
    /// Test to ensure the DeleteUser method returns a NoContentResult when a user is succesfully deleted
    /// In the test it is also assumed that the requester exists and is authorized.
    /// </summary>
    [Fact]
    public async Task DeleteUser()
    {
        // Arrange
        var userIdToDelete = 1;
        var mockUserCrud = new Mock<IUserCrud>();

        mockUserCrud.Setup(x => x.CheckIfUserExistsAsync(userIdToDelete)).ReturnsAsync(true);
        mockUserCrud.Setup(x => x.DeleteUserAsync(userIdToDelete, userIdToDelete)).ReturnsAsync(true);

        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim("UserId", userIdToDelete.ToString()),
        }, "Bearer"));

        var controller = new UserController(mockUserCrud.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = userClaims }
            }
        };

        // Act
        var result = await controller.DeleteUser(userIdToDelete);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
    
}



