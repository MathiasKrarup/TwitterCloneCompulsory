namespace Domain;

public class Like
{
    /// <summary>
    /// Id of the comment
    /// </summary>
    public int LikeId { get; set; }
    
    /// <summary>
    /// Id of the post
    /// </summary>
    public int PostId { get; set; }
    
    /// <summary>
    /// The id of the user
    /// </summary>
    public int UserId { get; set; }
    
}