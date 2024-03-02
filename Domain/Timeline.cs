namespace Domain;

public class Timeline
{
    /// <summary>
    /// The id of the timeline
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The list of the posts on the timeline
    /// </summary>
    /// <returns></returns>
    public List<Post> Posts { get; set; } = new List<Post>();
    
    /// <summary>
    /// Id of the current user
    /// </summary>
    public int UserId { get; set; }
}