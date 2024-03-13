namespace Domain;

public class Post
{
    /// <summary>
    /// The id of the post
    /// </summary>
    public int PostId { get; set; }
    
    /// <summary>
    /// The date the post is created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// The id of the user who created the post
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The content of the post
    /// </summary>
    public string Content { get; set; }

    
    /// <summary>
    /// Id of the current timeline
    /// </summary>
    public int TimelineID { get; set; }
}