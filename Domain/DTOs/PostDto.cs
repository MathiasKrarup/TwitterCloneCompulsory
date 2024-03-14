namespace Domain.DTOs;

public class PostDto
{
    public int PostId { get; set; }

    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTime? CreatedAt { get; set; }
}