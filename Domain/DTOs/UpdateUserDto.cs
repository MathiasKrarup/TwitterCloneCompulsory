namespace Domain.DTOs;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string? NewPassword { get; set; }
}