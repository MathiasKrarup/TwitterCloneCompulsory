namespace Domain;

public class Comment
{
    /// <summary>
    /// Id of the comment
    /// </summary>
    public int CommentId { get; set; }
    
    /// <summary>
    /// Id of the post
    /// </summary>
    public int PostId { get; set; }
    
    /// <summary>
    /// The id of the user
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Content of the comment
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// The time of the created comment
    /// </summary>
    public DateTime CreatedAt { get; set; }
}