namespace ClientAPI.DTO;

public class AddPostDTO
{
    public int UserId { get; set; }
    
    public string Content { get; set; }
    
    public DateTime CreatedAt { get; set; }
}